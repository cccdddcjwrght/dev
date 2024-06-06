using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FlatBuffers;
using SGame.Dining;
using UnityEngine;

namespace SGame
{
	/// <summary>
	/// id：3
	/// 工作台
	/// </summary>
	public class Condition_3_id : IConditonCalculator
	{

		public bool Do(IFlatbufferObject cfg, object target, string args)
		{
			if (target is GameObject go)
			{
				var mono = go.GetComponent<RegionHit>();
				var region = DataCenter.MachineUtil.GetWorktable(mono.region);
				if (region != null)
				{
					if (mono.transform.Find("scene_grid"))
						return DataCenter.MachineUtil.CheckCanActiveMachine(mono.place, false) == 0;
					else
					{
						if (region.type == 4)
						{
							SetRecruit(mono);
							return false;
						}
						return DataCenter.MachineUtil.IsAreaEnableByMachine(mono.place) && region.lv > 0 && DataCenter.MachineUtil.CheckCanUpLevel(region.id, 0) == 0;
					}
				}
				return false;
			}

			return false;
		}

		void SetRecruit(RegionHit mono)
		{
			if (mono)
			{
				var size = mono.collider.size;
				size.y = 5;
				mono.tag = ConstDefine.C_WORKER_TABLE_RECRUIT_TAG;
				mono.collider.size = size;
			}
		}
	}

	public class Condition_26_id : IConditonCalculator
	{
		Dictionary<int, SGame.Worktable> _datas = new Dictionary<int, Worktable>();

		public Condition_26_id()
		{
			EventManager.Instance.Reg<int>(((int)GameEvent.AFTER_ENTER_ROOM), (a) => _datas?.Clear());
		}

		public bool Do(IFlatbufferObject cfg, object target, string args)
		{
			if (target is GameObject go)
			{
				var mono = go.GetComponent<RegionHit>();
				if (!_datas.TryGetValue(mono.region, out var region))
					_datas[mono.region] = region = DataCenter.MachineUtil.GetWorktable(mono.region);
				if (region != null && DataCenter.MachineUtil.CheckCanUpLevel(region) == 0)
				{
					mono.Play("idle");
					return true;
				}
				else
					mono.Play(null);
			}
			return false;
		}

	}
}