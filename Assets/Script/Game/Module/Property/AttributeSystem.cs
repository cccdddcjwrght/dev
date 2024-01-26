using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameConfigs;
using log4net;
using UnityEngine;

namespace SGame
{
	public struct BuffData
	{
		public int id;
		public int val;
		public int targetid;
		public int time;
		/// <summary>
		/// buff来源标识
		/// 可以通过这个清除buff
		/// </summary>
		public int from;

		public BuffData(int id, int val, int targetid = 0, int time = 0)
		{

			this.id = id;
			this.val = val;
			this.targetid = targetid;
			this.time = time;
			from = 0;
		}

	}

	/// <summary>
	/// 属性系统
	/// </summary>
	public class AttributeSystem : MonoSingleton<AttributeSystem>
	{
		static ILog log = LogManager.GetLogger("game.attribute");

		static EnumTarget _needResetType = EnumTarget.Player | EnumTarget.Cook | EnumTarget.Waiter | EnumTarget.Customer | EnumTarget.Investor;
		static EnumTarget _needRemoveType = EnumTarget.Machine;

		private Dictionary<int, Dictionary<int, AttributeList>> _groups = new Dictionary<int, Dictionary<int, AttributeList>>();

		private Action<int> onUpdate;

		#region Register

		public void Initalize()
		{
			//注册游戏全局属性
			Register(((int)EnumTarget.Game));
			//注册需要随关卡修改的组
			TypeRegister(_needResetType);
			//监听房间进入
			new WaitEvent<int>(((int)GameEvent.ENTER_ROOM)).Wait(OnEnterRoom);
			//监听工作台解锁
			new WaitEvent<int>(((int)GameEvent.WORK_TABLE_ENABLE)).Wait(OnWorkTableEnable);
			//监听buff添加
			new WaitEvent<BuffData>(((int)GameEvent.BUFF_TRIGGER)).Wait(OnBuffAdd);
			InitGlobalAttribute();
		}

		/// <summary>
		/// 注册属性
		/// </summary>
		/// <param name="type"></param>
		public void TypeRegister(EnumTarget type)
		{
			var es = type.StateToList();
			if (es == null) Register((int)type);
			else es.ForEach(e => Register((int)e));
		}

		/// <summary>
		/// 移除属性
		/// </summary>
		/// <param name="type"></param>
		public void TypeUnRegister(EnumTarget type)
		{
			var es = type.StateToList();
			if (es == null) UnRegister((int)type);
			else es.ForEach(e => UnRegister((int)e));
		}

		/// <summary>
		/// 重置属性
		/// </summary>
		/// <param name="type"></param>
		public void TypeResetAttribute(EnumTarget type)
		{
			var es = type.StateToList();
			if (es == null) ResetAttribute((int)type);
			else es.ForEach(e => ResetAttribute((int)e));
		}

		public AttributeList Register(int type, int id = 0, AttributeList attribute = default)
		{
			var g = GetGroup(type, true);
			if (!g.TryGetValue(id, out var a))
			{
				g[id] = a = attribute ?? new AttributeList();
#if DEBUG
				a.key = ((EnumTarget)type) + "-" + id;
#endif
				onUpdate += a.Refresh;
			}
			return a;
		}

		public void UnRegister(int type, int id = 0)
		{
			var g = GetGroup(type);
			if (g != null)
			{
				if (id == 0)
				{
					foreach (var item in g)
					{
						item.Value.Clear();
						onUpdate -= item.Value.Refresh;
					}
					g.Clear();
				}
				else if (g.ContainsKey(id))
				{
					var a = g[id];
					g.Remove(id);
					a.Clear();
					onUpdate -= a.Refresh;
				}
			}
		}

		public void ResetAttribute(int type, int id = 0)
		{
			var g = GetGroup(type);
			if (g != null)
			{
				if (id == 0)
				{
					foreach (var item in g)
						item.Value.Reset();
				}
				else if (g.TryGetValue(id, out var a))
					a.Reset();
			}
		}

		public AttributeList GetAttributeList(int type, int id = 0)
		{
			var g = GetGroup(type);
			if (g != null)
			{
				g.TryGetValue(id, out var list);
				return list;
			}
			return default;
		}

		public Dictionary<int, AttributeList> GetGroup(int type, bool create = false)
		{
			if (!_groups.TryGetValue(type, out var group))
				_groups[type] = group = new Dictionary<int, AttributeList>(); ;
			return group;
		}

		#endregion

		#region Method

		/// <summary>
		/// 使用一个buff
		/// </summary>
		/// <param name="id">buff</param>
		/// <param name="val">值</param>
		/// <param name="targetid">目标类型对应的个体id ， 现在只有工作台需要</param>
		/// <param name="time">生效时长：0永久，-1：当前关卡，>0:秒 </param>
		/// <param name="from">buff来源，可以依据这个from清除buff</param>
		public void AddBuff(int id, int val, int targetid = 0, int time = 0, int from = 0)
		{
			if (id > 0)
			{
				if (ConfigSystem.Instance.TryGet<BuffRowData>(id, out var cfg))
				{
					var targets = GetTargets(cfg.ID, targetid);
					if (targets?.Count > 0)
					{
						targets.ForEach(t =>
						{
							time = time != 0 ? time : cfg.Time;
							time = time > 0 ? GetCurrentTime() + time : time;
							t.Change(cfg.Attribute, val == 0 ? cfg.Value : val, cfg.AddType, time, from);
						});
					}
				}
			}
		}

		/// <summary>
		/// 移除buff
		/// </summary>
		/// <param name="id"></param>
		/// <param name="from"></param>
		/// <param name="attributeID"></param>
		public void RemoveBuff(int id, int from, int attributeID = 0)
		{
			if (from != 0)
			{
				if (ConfigSystem.Instance.TryGet<BuffRowData>(id, out var cfg))
				{
					var targets = GetTargets(id);
					if (targets?.Count > 0)
						targets.ForEach(t => t.ResetByFrom(from, attributeID));
				}
			}
		}

		/// <summary>
		/// 获取属性值
		/// </summary>
		/// <param name="type">组</param>
		/// <param name="attributeID">属性ID</param>
		/// <param name="targetid"></param>
		/// <returns></returns>
		public double GetValue(EnumTarget type, EnumAttribute attributeID, int targetid = 0)
		{

			return GetValue(((int)type), ((int)attributeID), targetid);

		}

		/// <summary>
		/// 获取属性值
		/// </summary>
		/// <param name="type">组<see cref="EnumTarget"/> </param>
		/// <param name="attributeID">属性ID<see cref="EnumAttribute"/> </param>
		/// <param name="targetid">目标id</param>
		/// <returns></returns>
		public double GetValue(int type, int attributeID, int targetid = 0)
		{
			return GetAttributeList(type, targetid)[attributeID];
		}

		public List<AttributeList> GetTargets(int target, int targetid = 0)
		{
			if (target > 0)
			{
				var e = (EnumTarget)target;
				var ls = new List<AttributeList>();

				if (EnumTarget.AllMachine.IsInState(e))
				{
					var g = GetGroup((int)EnumTarget.Machine, false);
					if (g != null) ls.AddRange(g.Values);
				}

				foreach (var item in _groups)
				{
					if (item.Key.IsInState(e))
					{
						if (item.Value.TryGetValue(targetid, out var a)) ls.Add(a);
					}
				}

				return ls;
			}
			return default;
		}

		private int GetCurrentTime()
		{
			return (int)GlobalTime.passTime;
		}

		#endregion

		#region Mono

		System.Random _r = new System.Random();

		private void Update()
		{
			try
			{
				onUpdate?.Invoke(GetCurrentTime());
			}
			catch (Exception e)
			{
				log.Error(e.Message);
			}

			if (Input.GetKeyDown(KeyCode.A))
			{
				AddBuff(_r.Next(1, 70), _r.Next(-100, 100), 0, _r.Next(0, 20));
			}
		}

		#endregion

		#region private

		private void InitGlobalAttribute()
		{
			var attr = GetAttributeList(((int)EnumTarget.Game));
			attr[((int)EnumAttribute.DiamondRate)] = GlobalDesginConfig.GetInt("investor_gems_chance");
			attr[((int)EnumAttribute.Diamond)] = GlobalDesginConfig.GetInt("investor_gems");
			attr[((int)EnumAttribute.Gold)] = GlobalDesginConfig.GetInt("investor_coin_ratio");
			attr[((int)EnumAttribute.LevelGold)] = GlobalDesginConfig.GetInt("initial_coin");
			attr[((int)EnumAttribute.OfflineAddition)] = GlobalDesginConfig.GetInt("offline_ratio");
			attr[((int)EnumAttribute.OfflineTime)] = GlobalDesginConfig.GetInt("max_offline_time");
			attr[((int)EnumAttribute.AdAddition)] = GlobalDesginConfig.GetInt("ad_boost_ratio");
			attr[((int)EnumAttribute.AdTime)] = GlobalDesginConfig.GetInt("ad_boost_time");
		}

		#endregion

		#region Events

		private void OnEnterRoom(WaitEvent<int> wait)
		{
			TypeUnRegister(_needRemoveType);
			TypeResetAttribute(_needResetType);
			ReInitAllAttribute();
		}

		private void OnWorkTableEnable(WaitEvent<int> wait)
		{
			var id = wait.m_Value;
			Register(((int)EnumTarget.Machine), id);
		}

		private void OnBuffAdd(WaitEvent<BuffData> wait)
		{
			if (wait.m_Value.id > 0)
			{
				var data = wait.m_Value;
				var from = data.from;
				if (from != 0)
					RemoveBuff(data.id, from);
				AddBuff(data.id, data.val, data.targetid, data.time, data.from);
			}
		}

		/// <summary>
		/// 重置所有属性
		/// </summary>
		private void ReInitAllAttribute() { }

		#endregion
	}
}
