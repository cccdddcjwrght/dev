using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FlatBuffers;
using GameConfigs;
using GameTools;
using log4net;
using SGame.UI;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace SGame
{

	public interface IRedText
	{
		string text { get; }
	}

	public interface IConditonCalculator
	{

		public bool Do(IFlatbufferObject cfg, object target, string args);

	}

	public interface IConditionSys
	{
		IConditonCalculator GetConditonCalculator(string key);

		IConditonCalculator GetConditonCalculator(object target, string ext, int token = 0);

	}


	[UpdateAfter(typeof(GameLogicAfterGroup))]
	public partial class RichRedpoint : RedpointSystem
	{
		private static ILog log = LogManager.GetLogger("redpoint");
		private static Dictionary<int, string> _keys = new Dictionary<int, string>();
		private static Entity G_LOCK = new Entity() { Index = -1 };


		private Dictionary<int, Entity> _hudID = new Dictionary<int, Entity>();


		protected override void OnCreate()
		{
			base.OnCreate();
			EventManager.Instance.Reg<string>(((int)GameEvent.UI_SHOW), OnUIShow);
			EventManager.Instance.Reg<string>(((int)GameEvent.UI_HIDE), OnUIHide);
			EventManager.Instance.Reg(((int)GameEvent.PREPARE_LEVEL_ROOM), OnLevelRoom);
		}

		#region Override

		protected override void InitCalculation()
		{
			OnCalculation = (c, t, a) =>
			{
				var cfg = (RedConfigRowData)c;
				var condition = ConditionUtil.conditonSys.GetConditonCalculator(c, null, cfg.Id);
				var ret = condition?.Do(c, t, a) == true;
				if (condition is IRedText txt)
					SetText(cfg.Id, txt.text);
				return ret;
			};
		}

		protected override void SetRedOnGameObject(GameObject child, bool status, RedConfigRowData data = default)
		{
			if (child == null) return;

			if (!string.IsNullOrEmpty(data.Res) && data.Res[0] == '*')
			{
				var id = child.GetInstanceID();
				if (status)
				{
					if (!_hudID.TryGetValue(id, out var e) || (e != G_LOCK && !EntityManager.Exists(e)))
					{
						_hudID[id] = G_LOCK;

						_hudID[id] = UIUtils.ShowHUD(data.Res.Substring(1), child.transform, new float3(data.Offset(0), data.Offset(1), data.Offset(2)));
						_hudID[id].SetParam(new object[] { child, data });
						/*this.Delay(() =>
						{
						}, 1);*/

					}
				}
				else if (_hudID.TryGetValue(id, out var e) && e != G_LOCK && e != default)
				{
					_hudID.Remove(id);
					UIUtils.CloseUI(e);
				}
				return;
			}

			base.SetRedOnGameObject(child, status, data);
		}

		#endregion

		#region UI Event

		private void OnUIShow(string ui)
		{
			if (DataCenter.Instance.IsInitAll)
			{
				Init();
				this.Delay(() => MarkRedpointGroup(ui, true), 1);
			}
		}

		private void OnUIHide(string ui)
		{
			if (!_isInited) return;
			this.Delay(() => MarkRedpointGroup(ui, false), 1);
		}

		private void OnLevelRoom()
		{
			if(_hudID?.Count > 0)
			{
				foreach (var item in _hudID)
				{
					if (item.Value!=default && EntityManager.Exists(item.Value))
						UIUtils.CloseUI(item.Value);
				}
				_hudID.Clear();
			}
		}

		#endregion

		#region Class

		private class NoCondition : IConditonCalculator
		{
			public bool Do(IFlatbufferObject flatbuffer, object target, string args)
			{
				return false;
			}
		}

		#endregion
	}


	public class ConditionUtil : IConditionSys
	{
		static IConditionSys _conditionSys;
		private static Dictionary<int, string> _keys = new Dictionary<int, string>();

		private Dictionary<string, object> _calcus = new Dictionary<string, object>();

		static public IConditionSys conditonSys
		{
			get
			{
				if (_conditionSys == null)
					_conditionSys = new ConditionUtil();
				return _conditionSys;
			}
		}




		#region Method

		protected ConditionUtil()
		{
			_calcus = GetType()
					.Assembly
					.GetTypes()
					.Where(t => !t.IsAbstract && typeof(IConditonCalculator).IsAssignableFrom(t))
					.ToDictionary(t => t.Name.ToLower(), t => (object)t);
		}

		public IConditonCalculator GetConditonCalculator(object target, string ext, int token = 0)
		{

			return GetConditonCalculator(GetConditionKey(target, ext, token));

		}

		public IConditonCalculator GetConditonCalculator(string key)
		{
			if (string.IsNullOrEmpty(key)) return default;
			var condition = default(IConditonCalculator);

			if (_calcus.TryGetValue(key, out var calcu))
			{
				condition = calcu as IConditonCalculator;
				if (condition == null)
				{
					if (calcu is Type type)
						_calcus[key] = condition = System.Activator.CreateInstance(type, true) as IConditonCalculator;
				}
			}

			return condition;
		}

		#endregion


		#region Static

		static public bool CheckCondition(object key, string ext = null, int token = 0)
		{
			var condition = conditonSys.GetConditonCalculator(key, ext, token);
			if (condition != null)
				return condition.Do(key as IFlatbufferObject, null, null);
			return true;
		}

		static public bool CheckFunction(object func, int time = 0, string args = null)
		{
			if (func != null)
			{
				if (func is FunctionConfigRowData cfg || (func is int id && ConfigSystem.Instance.TryGet(id, out cfg)))
				{
					if (cfg.IsValid())
					{
						var t = time <= 0 ? GameServerTime.Instance.serverTime : time;
						var key = cfg.OpenValLength > 0 ? cfg.OpenVal(0).ToString() : !string.IsNullOrEmpty(cfg.Uniqid) ? cfg.Uniqid : "func" + cfg.Id;

						if (cfg.OpenType == 3)
						{
							return conditonSys.GetConditonCalculator(key, null)?.Do(cfg, null, args) == true;
						}
						else if (cfg.Activity!=0)
						{
							//正数：具体活动ID；负数：活动类型
							var actType = cfg.Activity;
							var isopen = false;

							if (actType > 0)
								isopen = ActiveTimeSystem.Instance.IsActive(actType, t);
							else if (actType < 0)
							{
								isopen = ConfigSystem.Instance
									.Finds<ActivityTimeRowData>((c) => c.Value == -actType)
									.Any(c => ActiveTimeSystem.Instance.IsActive(c.Id, t));
							}

							if (isopen)
							{
								var c = conditonSys.GetConditonCalculator(key, null);
								if (c != null) return  c.Do(cfg, null, args);
							}
							return isopen;

						}
					}
				}

			}
			return true;
		}

		public static string GetConditionKey(object obj, string ext, int token = 0)
		{
			string key = null;
			if (token == 0 || !_keys.TryGetValue(token, out key))
			{
				if (obj is int || obj is string) key = obj.ToString() + "_" + (ext ?? "id");
				else if (obj is IFlatbufferObject)
				{
					var type = -1;
					var id = 0;
					if (obj is RedConfigRowData red)
					{
						id = red.Id;
						type = red.Type;
					}

					if (type >= 0)
					{
						if (type < 101) key = id + "_id";
						else key = type.ToString();
					}

				}
				key = "condition_" + key;
				if (token != 0)
					_keys[token] = key;
			}
			return key;
		}


		#endregion

	}

}
