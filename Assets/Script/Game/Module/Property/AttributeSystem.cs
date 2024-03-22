using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameConfigs;
using log4net;
using Unity.Entities.UniversalDelegates;
using UnityEngine;

namespace SGame
{
	public struct BuffData
	{
		/// <summary>
		/// buff
		/// </summary>
		public int id;
		/// <summary>
		/// buff值
		/// </summary>
		public int val;
		/// <summary>
		/// 目标id
		/// </summary>
		public int targetid;
		/// <summary>
		/// 有效时间
		/// </summary>
		public int time;
		/// <summary>
		/// buff来源标识
		/// 可以通过这个清除buff
		/// </summary>
		public int from;
		public bool isremove;

		public BuffData(int id, int val, int targetid = 0, int time = 0)
		{

			this.id = id;
			this.val = val;
			this.targetid = targetid;
			this.time = time;
			this.isremove = false;
			from = 0;
		}

	}

	/// <summary>
	/// 属性系统
	/// </summary>
	public class AttributeSystem : MonoSingleton<AttributeSystem>
	{

		readonly static EnumTarget ALL_TARGET = EnumTarget.Player
			| EnumTarget.Equip
			| EnumTarget.Customer
			| EnumTarget.Investor
			| EnumTarget.Cook
			| EnumTarget.Waiter
			| EnumTarget.Machine
			| EnumTarget.Employee
			| EnumTarget.Game;

		static ILog log = LogManager.GetLogger("game.attribute");

		private Dictionary<int, Dictionary<int, AttributeList>> _groups = new Dictionary<int, Dictionary<int, AttributeList>>();
		private Dictionary<int, AttributeList> _bind = new Dictionary<int, AttributeList>();

		private Action<int> onUpdate;

		#region API


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
			var list = GetAttributeList(type, targetid);
			if (list != null)
				return list[attributeID];
			return 0;
		}

		/// <summary>
		/// 根据RoleID 获取属性
		/// </summary>
		/// <param name="roleID"></param>
		/// <param name="attributeID"></param>
		/// <param name="isemploy">是否雇佣关系</param>
		/// <returns></returns>
		public double GetValueByRoleID(int roleID, EnumAttribute attributeID, bool isemploy = false)
		{
			if (ConfigSystem.Instance.TryGet<RoleDataRowData>(roleID, out var cfg))
			{
				switch ((EnumRole)cfg.Type)
				{
					case EnumRole.Cook:
					case EnumRole.Waiter:
						return GetValue((EnumTarget)(1 << cfg.Type), attributeID);
					case EnumRole.Customer:
					case EnumRole.Car:
						return GetValue(EnumTarget.Customer, attributeID, roleID);
					case EnumRole.Player:
						return GetValue(EnumTarget.Player, attributeID, isemploy ? roleID : 0);
				}
			}
			return 0;
		}

		#endregion

		#region Register

		public void Initalize()
		{
			//注册游戏全局属性
			Register(((int)EnumTarget.Game));
			BuffSystem.Instance.Initalize();

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
			/*if (_bind.TryGetValue(type, out var a))
				return a;*/

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

		public AttributeList LinkAttributeToTarget(EnumTarget target, int targetID, EnumTarget to)
		{

			var a = GetAttributeList((int)target, targetID);
			if (a != null)
				_bind[(int)to] = a;
			return a;
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
					var targets = GetTargets(cfg.Target, targetid != 0 ? targetid : cfg.TargetID);
					var ct = GetCurrentTime();
					time = time != 0 ? time : cfg.Time;
					time = time > 0 ? ct + time : time;
					if (targets?.Count > 0)
						targets.ForEach(t => t.SetTime(ct).Change(cfg.Attribute, val == 0 ? cfg.Value : val, cfg.AddType, time, from));
				}
			}
		}

		/// <summary>
		/// 移除buff
		/// </summary>
		/// <param name="id"></param>
		/// <param name="from"></param>
		/// <param name="attributeID"></param>
		public void RemoveBuff(int id, int from, int targetid = 0, int attributeID = 0)
		{
			if (from != 0)
			{
				if (ConfigSystem.Instance.TryGet<BuffRowData>(id, out var cfg))
				{
					var targets = GetTargets(cfg.Target, targetid != 0 ? targetid : cfg.TargetID);
					if (targets?.Count > 0)
						targets.ForEach(t => t.ResetByFrom(from, attributeID > 0 ? attributeID : cfg.Attribute));
				}
			}
		}

		public void RemoveBuffByFrom(int from)
		{
			if (from != 0)
				GetTargets((int)ALL_TARGET)?.ForEach(t => t.ResetByFrom(from, 0));
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
						if (targetid != 0)
						{
							if (item.Value.TryGetValue(targetid, out var a))
								ls.Add(a);
						}
						else
							ls.AddRange(item.Value.Values);
					}
				}

				/*if (_bind.Count > 0)
				{
					foreach (var item in _bind)
					{
						if (item.Key.IsInState(e) && !ls.Contains(item.Value))
							ls.Add(item.Value);
					}
				}*/

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

		}

		#endregion

	}
}
