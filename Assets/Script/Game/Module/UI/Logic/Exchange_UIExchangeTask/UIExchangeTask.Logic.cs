
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Exchange;
    using System.Collections.Generic;
    using SGame.UI.Shop;

    public partial class UIExchangeTask
	{
		List<TaskItem> m_TaskItems;
		List<int> m_TaskGoods;

		partial void InitLogic(UIContext context){
			m_view.m_taskList.SetVirtual();
			m_view.m_taskList.itemRenderer = OnTaskItemRenderer;
			m_view.m_taskList.onClickItem.Add(OnClickTaskGetReward);

			m_view.m_goodList.itemRenderer = OnGoodItemRenderer;
			RefreshTimer();
			RefreshTask();
			RefreshGood();
		}

		public void RefreshTimer() 
		{
			int time = DataCenter.TaskUtil.GetTaskActiveTime();
			if (time > 0) 
			{
				Utils.Timer(time, () =>
				{
					time = DataCenter.TaskUtil.GetTaskActiveTime();
					m_view.m_time.SetText(string.Format(UIListener.Local("ui_merchant_tips"), Utils.FormatTime(time)));
				}, m_view, completed: () => SGame.UIUtils.CloseUIByID(__id));
			}
			
		}

		public void RefreshCurrency() 
		{
			int currencyId = DataCenter.TaskUtil.GetCurCurrencyId();
			m_view.m_currency.SetIcon(Utils.GetItemIcon(1, currencyId));
			var num = PropertyManager.Instance.GetItem(currencyId).num;
			m_view.m_value.SetText(num.ToString());
		}

		public void RefreshTask() 
		{
			RefreshCurrency();
			RefreshReddot();
			m_TaskItems = DataCenter.Instance.taskData.taskItems;
			m_view.m_taskList.numItems = m_TaskItems.Count;
		}

		public void RefreshGood() 
		{
			RefreshCurrency();
			RefreshReddot();
			m_TaskGoods = DataCenter.Instance.taskData.taskGoods;
			m_view.m_goodList.numItems = m_TaskGoods.Count;
		}

		public void RefreshReddot() 
		{
			m_view.m_reddot1.visible = DataCenter.TaskUtil.CheckHasTaskIsGet();
			m_view.m_reddot2.visible = DataCenter.TaskUtil.CheckIsHasExchange();
		}

		public void OnTaskItemRenderer(int index, GObject gObject) 
		{
			var data = m_TaskItems[index];
			gObject.data = data.taskId;
			var item = (UI_ExchangeItem)gObject;
			if (ConfigSystem.Instance.TryGet<GameConfigs.MerchantMissionRowData>(data.taskId, out var cfg)) 
			{
				item.m_taskDes.SetText(UIListener.Local(cfg.TaskDes));
				item.m_currency.SetIcon(Utils.GetItemIcon(cfg.TaskReward(0), cfg.TaskReward(1)));
				item.m_taskIcon.SetIcon(cfg.TaskIcon);
				item.m_value.SetText(Utils.ConvertNumberStr(cfg.TaskReward(2)));

				item.m_progress.max = cfg.TaskValue;
				item.m_progress.value = data.value;

				item.m_progress.m_value.SetText(data.value + "/" + cfg.TaskValue);
				item.m_state.selectedIndex = data.state;
			}
		}

		public void OnGoodItemRenderer(int index, GObject gObject) 
		{
			var item = (UI_Goods)gObject;
			var id = m_TaskGoods[index];
			item.m_currency.selectedIndex = 2;
			item.m_hidebottom.selectedIndex = 1;
			item.m_desc.text = string.Empty;
			if (ConfigSystem.Instance.TryGet<GameConfigs.MerchantRewardRowData>(id, out var cfg)) 
			{
				//var val = cfg.GetItemIdArray();
				
				item.SetTextByKey(cfg.Name);
				item.SetIcon(cfg.Icon);

				var val = cfg.GetCostArray();
				item.m_price.SetText(Utils.ConvertNumberStr(val[2]));
				item.m_currency_2.SetIcon(Utils.GetItemIcon(val[0], val[1]));
			}

			item.m_click.onClick.Set(()=>OnClickExchangeGood(id));
		}

		public void OnClickTaskGetReward(EventContext context) 
		{
			RequestExcuteSystem.FinishTaskReq((int)((GObject)context.data).data);
		}

		public void OnClickExchangeGood(int id) 
		{
			RequestExcuteSystem.ExchangeReq(id);
		}

		partial void UnInitLogic(UIContext context){

		}
	}
}
