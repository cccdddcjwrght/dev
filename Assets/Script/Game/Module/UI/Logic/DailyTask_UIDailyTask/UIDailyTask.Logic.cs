
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.DailyTask;
    using System.Collections.Generic;
    using System.Linq;

    public partial class UIDailyTask
	{
		private EventHandleContainer m_Event = new EventHandleContainer();
		private List<UI_DailyGiftItem> m_GiftItems = new List<UI_DailyGiftItem>();
		private List<int[]> m_PopItems;

		private bool _isPop = false;
		partial void InitLogic(UIContext context){
			m_view.m_taskList.SetVirtual();
			m_view.m_taskList.itemRenderer = OnTaskItemRenderer;
			m_view.m_rewardList.itemRenderer = OnTaskRewardItemRenderer;

			m_Event += EventManager.Instance.Reg((int)GameEvent.DAILY_TASK_UPDATE, RefreshTask);
			LoadGiftItem();

			RefreshTask();
			RefreshTime();
		}

		void LoadGiftItem() 
		{
			m_view.onClick.Add(()=> 
			{
				if (!_isPop) 
				{
					m_view.m_pop.visible = false;
				}
				_isPop = false;
			});
			m_view.m_pop.m_list.itemRenderer = OnPopItemRenderer;

			for (int i = 1; i <= 5; i++)
            {
				var giftItem = (UI_DailyGiftItem)m_view.GetChild("giftItem" + i);
				m_GiftItems.Add(giftItem);
				giftItem.onClick.Add(()=> 
				{
					if (giftItem.m_state.selectedIndex != 1)
					{
						m_PopItems = giftItem.GetRewardList();
						m_view.m_pop.m_list.numItems = m_PopItems.Count;
						m_view.m_pop.m_list.ResizeToFit();
						m_view.m_pop.SetPivot(1 - Mathf.Pow(0.5f, m_PopItems.Count), 1f, true);
						m_view.m_pop.xy = giftItem.xy;
						m_view.m_pop.visible = true;
						_isPop = true;
					}
					giftItem.OnClick();
					
				});
            }
		}

		void RefreshTask() 
		{
			var dailyTaskData = DataCenter.Instance.dailyTaskData;
			m_view.m_dayValue.SetText(dailyTaskData.dayLiveness.ToString());
			m_view.m_weekValue.SetText(dailyTaskData.weekLiveness.ToString());
			m_view.m_bar.fillAmount = dailyTaskData.dayLiveness / 100f;

            for (int i = 0; i < m_GiftItems.Count; i++)
            {
				var item = m_GiftItems[i];
				var dayReward = dailyTaskData.dayRewards[i];
				if (dayReward == null) break;

				item.SetData(dayReward);
				item.x = m_view.m_barbg.x + m_view.m_barbg.width * (dayReward.needLiveness / 100f);
			}

			dailyTaskData.tasks = dailyTaskData.tasks.OrderBy(v => v.isFinish)
				.ThenBy(v => !v.IsGet()).ToList();

			m_view.m_taskList.numItems = dailyTaskData.tasks.Count;
			m_view.m_rewardList.numItems = dailyTaskData.weekRewards.Count;
		}

		void RefreshTime() 
		{
			Utils.Timer(GameServerTime.Instance.nextDayInterval, () =>
			{
				m_view.m_dayTime.SetText(Utils.FormatTime(GameServerTime.Instance.nextDayInterval));
			}, m_view);

			Utils.Timer(DataCenter.DailyTaskUtil.nextWeekInterval, () =>
			{
				m_view.m_weekTime.SetText(Utils.FormatTime(DataCenter.DailyTaskUtil.nextWeekInterval, daylimit: 1));
			}, m_view);
		}

		void OnTaskItemRenderer(int index, GObject gObject) 
		{
			var item = (UI_DailyTaskItem)gObject;
			item.SetData(DataCenter.Instance.dailyTaskData.tasks[index]);
		}

		void OnTaskRewardItemRenderer(int index, GObject gObject) 
		{
			var item = (UI_DailyRewardItem)gObject;
			item.SetData(DataCenter.Instance.dailyTaskData.weekRewards[index]);
		}

		void OnPopItemRenderer(int index, GObject gObject) 
		{
			var v = m_PopItems[index];
			gObject.SetIcon(Utils.GetItemIcon(v[0], v[1]));
			gObject.SetText(Utils.ConvertNumberStrLimit3(v[2]));
		}

		partial void UnInitLogic(UIContext context){
			m_Event.Close();
			m_Event = null;
		}
	}
}

namespace SGame.UI.DailyTask 
{
    using FairyGUI;
    using SGame;
    using System.Collections.Generic;
    using UnityEngine;

    public partial class UI_DailyGiftItem 
	{
		private DailyReward m_DailyReward;
		public void SetData(DailyReward data) 
		{
			m_DailyReward = data;
			var dailyTaskData = DataCenter.Instance.dailyTaskData;
			this.SetText(data.needLiveness.ToString());
			if (data.isGet) m_state.selectedIndex = 2;
			else m_state.selectedIndex = dailyTaskData.dayLiveness >= data.needLiveness ? 1 : 0;
		}

		public void OnClick() 
		{
			if (m_state.selectedIndex == 1)
			{
				RequestExcuteSystem.DailyGiftGet(m_DailyReward.cfgId);
			}
		}

		public List<int[]> GetRewardList() 
		{
			var config = m_DailyReward.GetConfig();
			var list = Utils.GetArrayList(true,
				config.GetReward1Array,
				config.GetReward2Array,
				config.GetReward3Array);
			return list;
		}
	}

	public partial class UI_DailyTaskItem 
	{
		private GameConfigs.DailyTaskRowData m_Config;
		public void SetData(DailyTaskItem data) 
		{
			m_Config = data.GetConfig();
			m_value.SetText(m_Config.TaskReward.ToString());

			var max = m_Config.TaskValue(1);
			m_des.SetText(string.Format(UIListener.Local(m_Config.TaskDes), max));

			var value = data.GetValue();
			m_progress.max = max;
			m_progress.value = Mathf.Min(value, max);

			if (data.isFinish) m_state.selectedIndex = 2;
			else m_state.selectedIndex = value >= max ? 1 : 0;

			m_goBtn.onClick.Set(OnGoBtnClick);
			m_getBtn.onClick.Set(OnGetBtnClick);
		}

		public void OnGoBtnClick() 
		{
			GuideManager.Instance.StartGuide(m_Config.GuideId);
			SGame.UIUtils.CloseUIByName("dailytask");
		}

		public void OnGetBtnClick() 
		{
			if (m_state.selectedIndex == 1) 
			{
				RequestExcuteSystem.DailyTaskFinsih(m_Config.Id);
			}
		}
	}

	public partial class UI_DailyRewardItem 
	{
		private DailyReward m_DailyReward;
		public void SetData(DailyReward data) 
		{
			m_DailyReward = data;
			var config = data.GetConfig();
			m_value.SetText(data.needLiveness.ToString());
			var r = config.GetReward1Array();
			this.SetIcon(Utils.GetItemIcon(r[0], r[1]));
			this.m_num.SetText(r[2].ToString());

			var dailyTaskData = DataCenter.Instance.dailyTaskData;
			if (data.isGet) m_state.selectedIndex = 2;
			else m_state.selectedIndex = dailyTaskData.weekLiveness >= data.needLiveness ? 1 : 0;

			this.onClick.Set(OnClick);
		}

		public void OnClick() 
		{
			if(m_state.selectedIndex == 1) RequestExcuteSystem.DailyGiftGet(m_DailyReward.cfgId);
		}
	}
}
