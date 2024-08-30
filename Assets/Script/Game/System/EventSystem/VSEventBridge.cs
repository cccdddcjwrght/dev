using System;
using System.Collections;
using System.Collections.Generic;
using log4net;

namespace SGame
{
	public interface IEventDispatcher
	{
		EHandler Listen(int id, Action<object> call);
		void UnListen(int id, Action<object> call);
	}


	public class EHandler : EventHanle
	{
		int eventType;
		Action<object> func;
		WeakReference<IEventDispatcher> managerReference;
		List<EHandler> list;

		public EHandler(IEventDispatcher dispatcher, int e, Action<object> f)
		{
			if (dispatcher != null)
			{
				managerReference = new WeakReference<IEventDispatcher>(dispatcher);
				eventType = e;
				func = f;
			}
		}

		public EHandler Add(EHandler handler)
		{
			if (list == null) list = new List<EHandler>();
			if (list.Contains(handler)) return this;
			list.Add(handler);
			return this;
		}

		public void Close()
		{
			if (managerReference != null)
			{
				if (managerReference.TryGetTarget(out IEventDispatcher mgr))
					mgr.UnListen(eventType, func);
				func = null;
				managerReference = null;
			}
			if (list != null && list.Count > 0)
			{
				for (int i = list.Count - 1; i >= 0; i--)
					list[i].Close();
				list.Clear();
			}
			list = null;
		}

		static public EHandler operator +(EHandler a, EHandler b)
		{
			if (a == null) a = new EHandler(null, 0, null);
			return a.Add(b);
		}

	}

	public class VSEventBridge : Singleton<VSEventBridge>, IEventNotify
	{
		private static ILog log = LogManager.GetLogger("gameevent");
		
		public void Init()
		{
			EventManager.Instance.SetEventNotify(this);
		}

		public delegate void EventType(params object[] values);
		Dictionary<int, EventType> _registers = new Dictionary<int, EventType>();


		public bool HasEvent(int id)
		{
			//log.LogDebug("trigger event =" + (GameEvent)id + " id=" + id);
			bool hasKey = _registers.ContainsKey(id);
			return hasKey;
		}

		public void OnCall(int id, params object[] values)
		{
			if (_registers.TryGetValue(id, out var c) && c != null)
			{
				c(values);
				//c.DynamicInvoke(values);
			}
		}
		
		public void Reg(int id, EventType call)
		{
			if (_registers.TryGetValue(id, out var c))
			{
				c += call;
			}
			else
			{
				c = call;
			}
			
			_registers[id] = c;
		}

		public void UnReg(int id, EventType call)
		{
			if (_registers.TryGetValue(id, out var c))
			{
				c -= call;
				if (c != null)
				{
					_registers[id] = c;
				}
				else
				{
					_registers.Remove(id);
				}
			}
		}

		public void Clear()
		{
			_registers.Clear();
		}
	}
}