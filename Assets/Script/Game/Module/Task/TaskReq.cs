
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame 
{
	partial class RequestExcuteSystem
	{
		[InitCall]
		static void InitTask()
		{
			DataCenter.TaskUtil.InitData();
		}

		public static void FinishTaskReq(int id) 
		{
			if (DataCenter.TaskUtil.CheckTaskIsGetReward(id)) 
			{
				if (ConfigSystem.Instance.TryGet<GameConfigs.MerchantMissionRowData>(id, out var cfg)) 
				{
					var val = cfg.GetTaskRewardArray();
					PropertyManager.Instance.Update(val[0], val[1], val[2]);

					if (DataCenter.Instance.taskData.taskDict.TryGetValue(id, out var data))
					{
						data.isGet = true;
						data.state = (int)TaskState.GET_REWARD;
					}
					DataCenter.TaskUtil.TaskStateSort();
					EventManager.Instance.Trigger((int)GameEvent.TASK_FINISH);
				}
			}
		}

		public static void ExchangeReq(int id) 
		{
			if (ConfigSystem.Instance.TryGet<GameConfigs.MerchantRewardRowData>(id, out var cfg)) 
			{
				var val = cfg.GetCostArray();
				if (!PropertyManager.Instance.CheckCount(val[1], val[2], val[0]))
				{
					"ui_merchant_tips2".Tips();
					return;
				}
				PropertyManager.Instance.Update(val[0], val[1], val[2], true);
				val = cfg.GetItemIdArray();
				PropertyManager.Instance.Update(val[0], val[1], val[2]);

				DataCenter.TaskUtil.RandomTaskGood(cfg.Group, id);
				DataCenter.TaskUtil.TaskStateSort();
				EventManager.Instance.Trigger((int)GameEvent.TASK_BUY_GOOD);
			}
		}
	}
}


