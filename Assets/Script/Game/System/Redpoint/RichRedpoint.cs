using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FlatBuffers;
using GameConfigs;
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
	}


	[UpdateAfter(typeof(GameLogicAfterGroup))]
	public class RichRedpoint : RedpointSystem, IConditionSys
	{
		private static ILog log = LogManager.GetLogger("redpoint");
		private static Dictionary<int, string> _keys = new Dictionary<int, string>();


		private Dictionary<string, object> _calcus = new Dictionary<string, object>();
		private Dictionary<int, Entity> _hudID = new Dictionary<int, Entity>();

		protected override void OnCreate()
		{
			base.OnCreate();
			EventManager.Instance.Reg<string>(((int)GameEvent.UI_SHOW), OnUIShow);
			EventManager.Instance.Reg<string>(((int)GameEvent.UI_HIDE), OnUIHide);

			ConditionUtil.conditonSys = this;

			_calcus = GetType()
				.Assembly
				.GetTypes()
				.Where(t => !t.IsAbstract && typeof(IConditonCalculator).IsAssignableFrom(t))
				.ToDictionary(t => t.Name.ToLower(), t => (object)t);
		}

		#region Method

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

		#region Override

		protected override void InitCalculation()
		{
			OnCalculation = (c, t, a) =>
			{
				var cfg = (RedConfigRowData)c;
				var condition = GetConditonCalculator(GetConditionKey(c, null, cfg.Id));
				var ret = condition?.Do(c, t, a) == true;
				if (condition is IRedText txt)
				{
					SetText(cfg.Id, txt.text);
				}
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
					if (!_hudID.TryGetValue(id, out var e))
					{
						_hudID[id] = default;
						this.Delay(() =>
						{
							_hudID[id] = UIUtils.ShowHUD(data.Res.Substring(1), child.transform, new float3(data.Offset(0), data.Offset(1), data.Offset(2)));
						}, 1);

					}
				}
				else if (_hudID.TryGetValue(id, out var e))
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

		#endregion

		#region Static

		private static string GetConditionKey(object obj, string ext, int token = 0)
		{
			string key = null;
			if (token == 0 || !_keys.TryGetValue(token, out key))
			{
				if (obj is int || obj is string)
					key = obj.ToString() + "_" + (ext ?? "id");
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

		#region Class

		private class NoCondition : IConditonCalculator
		{
			public bool Do(IFlatbufferObject flatbuffer, object target, string args)
			{
				log.Error($"没有实现[ {GetConditionKey(flatbuffer, null)} ]条件");
				return false;
			}
		}

		#endregion
	}


	public static class ConditionUtil
	{
		static IConditionSys _conditionSys;

		static public IConditionSys conditonSys
		{
			get { return _conditionSys; }
			set {
				if (_conditionSys != null)
					return;
				_conditionSys = value;
			}
		}


	}

}
