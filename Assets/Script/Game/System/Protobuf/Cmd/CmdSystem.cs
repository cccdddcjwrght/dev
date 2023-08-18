using System;
using System.Collections.Generic;
using System.Linq;
using IPMessage = Google.Protobuf.IMessage;

namespace Cmd
{
	public class CmdExcute : System.Attribute
	{
		public int protocolID;

		public CmdExcute(int id)
		{
			protocolID = id;
		}

		public CmdExcute(ProtocolID id)
		{
			protocolID = (int)id;
		}

	}

	public interface ICmdExcute
	{
		void OnExcute(int msgID, byte[] gamePackage, int offset, int count, int sqid = 0);
	}

	/// <summary>
	/// 协议处理基类
	/// 监听到对应ID消息时会走对应的CmdExcute派生类来单独处理
	/// 用CmdExcute属性类标记
	/// </summary>
	/// <typeparam name="T">协议类</typeparam>
	public abstract class ICmdExcute<T> : ICmdExcute where T : class, Google.Protobuf.IMessage,new()
	{
		void ICmdExcute.OnExcute(int msgID, byte[] gamePackage, int offset, int count, int sqid)
		{
			var data = gamePackage;
			var cmd = Protocol.Deserialize<T>(data, offset, count);
			OnExcute(msgID, cmd);
			CommonCmd.Excute(msgID, cmd);
		}

		protected abstract void OnExcute(int msgID, T cmd);

	}

	/// <summary>
	/// 通用Cmd处理，监听
	/// 对于没有单独写CmdExcute的协议请注册一个监听来接收协议消息
	/// 否则不进行解析处理
	/// </summary>
	[CmdExcute(-1)]
	public class CommonCmd : ICmdExcute
	{
		abstract class Item
		{
			public Type type;

			public Func<byte[], int, int,object> decode;

			public abstract void OnExcute(int msgID, object msg);

			public virtual void Clear()
			{
				type = null;
			}
		}

		class Item<T> : Item
		{
			public Action<int, T> call;

			public override void OnExcute(int msgID, object msg)
			{
				call?.Invoke(msgID, msg == null ? default : (T)msg);
			}

			public override void Clear()
			{
				call = null;
				base.Clear();
			}

		}

		static private Dictionary<int, Item> _binds = new Dictionary<int, Item>();

		static public void AddListen<T>(int msgID, Action<int, T> call, bool once = false) where T : class, IPMessage, new()
		{
			static object Decode(byte[] bytes , int off,int len)
			{
				return Protocol.Deserialize<T>(bytes,off,len);
			}

			if (msgID > 0 && call != null)
			{
				if (_binds.TryGetValue(msgID, out var item))
				{
					if (typeof(T) != item.type)
					{
						GameDebug.LogError($"{msgID} 已经绑定协议类型{item.type}");
						return;
					}
				}
				else
				{
					item = new Item<T>() { type = typeof(T), decode = Decode };
					_binds[msgID] = item;
				}
				if (item is Item<T> it)
				{
					it.call -= call;
					if (once)
					{
						Action<int, T> c = call;
						call = new Action<int, T>((id, m) =>
						{
							it.call -= call;
							c(id, m);
						});
					}
					it.call += call;
				}
			}
		}

		static public void RemoveListen<T>(int msgID, Action<int, T> call)
		{
			if (msgID > 0)
			{
				if (_binds.TryGetValue(msgID, out var item))
				{
					if (call == null)
					{
						item.Clear();
						_binds.Remove(msgID);
					}
					else if (item is Item<T> it)
						it.call -= call;
				}
			}
		}

		static public void Excute(int msgID, object msg)
		{
			if (_binds.TryGetValue(msgID, out var type))
				Excute(msgID, msg, type);
		}



		static private void Excute(int msgID, object msg, Item listen)
		{
			if (listen != null)
				listen.OnExcute(msgID, msg);
		}

		void ICmdExcute.OnExcute(int msgID, byte[] gamePackage, int offset, int count, int sqid)
		{
			GameDebug.Log($"recv=> {msgID}");
			if (_binds.TryGetValue(msgID, out var type))
			{
				var msg = type.decode?.Invoke(gamePackage, offset, count);
				Excute(msgID, msg, type);
			}
		}

	}

	/// <summary>
	/// 协议处理类管理
	/// </summary>
	public class CmdSystem : SGame.Singleton<CmdSystem>
	{
		private Dictionary<int, object> excutes = new Dictionary<int, object>();

		public CmdSystem()
		{
			//获取全部绑定的ICmdExcute
			var tys = GetType()
				.Assembly
				.GetTypes()
				.Where(
					t => t.IsClass
					&& !t.IsAbstract
					&& typeof(ICmdExcute).IsAssignableFrom(t)
					&& System.Attribute.IsDefined(t, typeof(CmdExcute))
				).ToList();

			if (tys != null && tys.Count > 0)
			{
				foreach (var item in tys)
				{
					var ds = item.GetCustomAttributes(typeof(CmdExcute), false) as CmdExcute[];
					if (ds != null && ds.Length > 0)
					{
						System.Array.ForEach(ds, (t) =>
						{
							if (excutes.ContainsKey(t.protocolID))
								GameDebug.LogError("具有相同协议处理类:" + excutes[t.protocolID] + "====" + item);
							else
								excutes[t.protocolID] = item;
						});
					}
				}
			}
		}

		/// <summary>
		/// 分ID处理对应的协议
		/// 如果没有在ProtocolID里面定义的ID会走0绑定的ICmdExcute 方便推送到lua层
		/// 如果没有注册单独的处理类，会走到CommonCmd类来解析推送
		/// 注册了的会走对应CmdExcute
		/// </summary>
		/// <param name="msgID"></param>
		/// <param name="gamePackage"></param>
		/// <param name="offset"></param>
		/// <param name="count"></param>
		/// <param name="sqid"></param>
		public void Excute(int msgID, byte[] gamePackage, int offset, int count, int sqid = 0)
		{
			var id = (ProtocolID)msgID;
			if (id != ProtocolID.None || true)//在C#这边有明确定义的协议号
				(GetExcute(msgID) ?? GetExcute(-1))?.OnExcute(msgID, gamePackage, offset, count, sqid);
			else//走通用处理或者推送到lua
				GetExcute(0)?.OnExcute(msgID, gamePackage, offset, count, sqid);
		}

		private ICmdExcute GetExcute(int id)
		{
			if (excutes.TryGetValue(id, out var excute))
			{
				if (excute is System.Type t)
				{
					excute = excutes[id] = System.Activator.CreateInstance(excute as System.Type, true) as ICmdExcute;
				}
				return excute as ICmdExcute;
			}
			return null;
		}

	}



}