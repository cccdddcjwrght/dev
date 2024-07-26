
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Task;
    using System.Collections.Generic;
    using GameConfigs;

    public partial class UITask
	{
		EventHandleContainer m_EventHandle = new EventHandleContainer();
		float lockTime = GlobalDesginConfig.GetFloat("task_lock_time");

		UIContext m_Context;
		//当前任务id
		int m_CurTaskId;
		List<int[]> m_TaskRewardData;
		partial void InitLogic(UIContext context){

			m_Context = context;
			m_view.m_list.itemRenderer = OnItemRenderer;
			m_view.m_mask.onClick.Add(DoCloseUIClick);

			m_EventHandle += EventManager.Instance.Reg<int>((int)GameEvent.MAIN_TASK_UPDATE,(id)=> RefreshTaskData());
			RefreshTaskData();
		}

		public void RefreshTaskData() 
		{
			m_CurTaskId = DataCenter.TaskMainUtil.GetCurTaskCfgId();
			if (ConfigSystem.Instance.TryGet<GameConfigs.MainTaskRowData>(m_CurTaskId, out var cfg))
			{
				m_TaskRewardData = Utils.GetArrayList(true, cfg.GetTaskReward1Array, cfg.GetTaskReward2Array, cfg.GetTaskReward3Array);
				m_view.m_list.numItems = m_TaskRewardData.Count;
				m_view.m_icon.SetIcon(cfg.Icon);

				var max = cfg.TaskValue(1);
				var value = DataCenter.TaskMainUtil.GetTaskProgress(cfg.TaskType, cfg.GetTaskValueArray());

				m_view.m_progress.max = max;
				m_view.m_progress.value = value;
				m_view.m_progress.m_value.SetTextByKey("{0}/{1}", value, max);
				m_view.m_des.SetTextByKey(UIListener.Local(cfg.TaskDes), value, max);

				m_view.m_btn.SetTextByKey(value >= max ? "ui_task_btn2" : "ui_task_btn1");
				m_view.m_btn.GetController("bgColor").selectedIndex = value >= max ? 1 : 0;
			}
			else 
			{
				//查找不到任务配置/说明任务已经做完/关闭界面
				SGame.UIUtils.CloseUIByID(__id);
				EventManager.Instance.Trigger((int)GameEvent.GAME_MAIN_REFRESH);
			}
		}

		public void OnItemRenderer(int index, GObject gObject) 
		{
			var item = (UI_TaskRewardItem)gObject;
			var val = m_TaskRewardData[index];
			item.SetIcon(Utils.GetItemIcon(val[0], val[1]));
			item.SetText(Utils.ConvertNumberStr(val[2]));
		}

        partial void OnBtnClick(EventContext data)
        {
			//进度完成
			if (m_view.m_btn.GetController("bgColor").selectedIndex == 1)
			{
				ConfigSystem.Instance.TryGet<GameConfigs.MainTaskRowData>(m_CurTaskId, out var cfg);
				TransitionModule.Instance.PlayFlight(m_view.m_list, m_TaskRewardData, cfg.EffectType);
				m_view.m_content.visible = false;
				DataCenter.TaskMainUtil.FinishTaskId(m_CurTaskId);
				EventManager.Instance.Trigger((int)GameEvent.ON_UI_MASK_HIDE, m_Context);

				Utils.Timer(lockTime, null, m_view, completed: () => 
				{
					if (m_view == null) return;
					m_view.m_content.visible = true;
					EventManager.Instance.Trigger((int)GameEvent.ON_UI_MASK_SHOW, m_Context);
				});
			}
			else 
			{
				if (ConfigSystem.Instance.TryGet<GameConfigs.MainTaskRowData>(m_CurTaskId, out var cfg)) 
					GuideManager.Instance.StartGuide(cfg.GuideId);
				DoCloseUIClick(data);
			}
			
        }

        partial void UnInitLogic(UIContext context){
			m_EventHandle.Close();
			m_EventHandle = null;
		}
	}
}
