
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Club;
    using System.Collections.Generic;

    public partial class UIClubTask
	{
		int m_CurrencyId;
		List<ClubTaskData> m_TaskDatas;
		partial void InitLogic(UIContext context){
			m_view.m_list.itemRenderer = OnTaskItemRenderer;
			RefreshTaskList();	
		}

		public void RefreshTaskList() 
		{
			m_TaskDatas = DataCenter.ClubUtil.GetClubTaskData();
			m_view.m_list.numItems = m_TaskDatas.Count;

			m_CurrencyId = DataCenter.ClubUtil.GetClubCurrencyId();
			m_view.m_currencyIcon.SetIcon(Utils.GetItemIcon(1, m_CurrencyId));
		}

		public void OnTaskItemRenderer(int index, GObject gObject) 
		{
			var item = (UI_ClubTaskItem)gObject;
			var data = m_TaskDatas[index];

			item.m_currencyIcon.SetIcon(Utils.GetItemIcon(1, m_CurrencyId));
			if (ConfigSystem.Instance.TryGet<GameConfigs.ClubTaskRowData>(data.id, out var cfg)) 
			{
				//item.m_des.SetText(string.Format("taskId:{0}, value:{1}, max:{2}", data.id, data.value, data.max));
				item.m_des.SetText(string.Format(UIListener.Local(cfg.Description),data.value, data.limitNum) +
					string.Format(UIListener.Local("ui_club_task_finish"), data.finishNum));
				item.m_value.SetText(cfg.Reward(1).ToString());
			}
		}

		partial void UnInitLogic(UIContext context){

		}
	}
}
