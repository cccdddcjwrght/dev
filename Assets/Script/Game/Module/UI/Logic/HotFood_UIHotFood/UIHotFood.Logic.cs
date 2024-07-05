﻿
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.HotFood;
    using System.Collections.Generic;
    using System;

    public partial class UIHotFood
	{
		EventHandleContainer m_Event = new EventHandleContainer();
		List<int> m_FoodList = new List<int>();

		Action<bool> m_Timer;
		//当前选中的id
		int m_SelectFoodId = 0;
		partial void InitLogic(UIContext context){
			m_view.m_list.itemRenderer = OnItemRenderer;
			m_view.m_list.onClickItem.Add(OnHotFoodItemClick);

			m_Event += EventManager.Instance.Reg((int)GameEvent.HOTFOOD_REFRESH, RefreshHotFood);
			RefreshHotFood();
		}

		void RefreshHotFood() 
		{
			var hotFoodData = DataCenter.Instance.hotFoodData;
			m_view.m_startBtn.enabled = hotFoodData.GetCdTime() <= 0;
			m_Timer?.Invoke(false);

			if (hotFoodData.IsForce())
			{
				m_SelectFoodId = hotFoodData.foodID;
				m_view.m_icon.SetIcon(Utils.GetItemIcon(1, hotFoodData.foodID));
				ConfigSystem.Instance.TryGet<GameConfigs.ItemRowData>(hotFoodData.foodID, out var cfg);
				m_view.m_des.SetTextByKey("ui_hotfood_info2", UIListener.Local(cfg.Name));
				m_view.m_hoting.selectedIndex = 1;

				m_Timer = Utils.Timer(hotFoodData.GetTime(), () =>
				{
					var t = HotFoodModule.Instance.HotDuration;
					var p = hotFoodData.GetTime();
					m_view.m_progress.fillAmount = (float)p / t;

					m_view.m_duration.SetText(Utils.TimeFormat(hotFoodData.GetTime()));
				}, m_view, completed:()=> 
				{
					m_SelectFoodId = 0;
					RefreshHotFood();
				});
			}
			else if (hotFoodData.GetCdTime() > 0)
			{
				m_view.m_hoting.selectedIndex = 2;
				m_Timer = Utils.Timer(hotFoodData.GetCdTime(), () =>
				{
					m_view.m_cdtime.SetText(Utils.TimeFormat(hotFoodData.GetCdTime()));
				}, m_view, completed: RefreshHotFood);
			}
			else
			{
				m_view.m_cdtime.SetText(Utils.TimeFormat(HotFoodModule.Instance.HotDuration));
				m_view.m_hoting.selectedIndex = 0;
			}
			
			m_FoodList = TableManager.Instance.GetOpenFoodTypes();
			m_view.m_list.numItems = m_FoodList.Count;
		}

		void OnItemRenderer(int index, GObject gObject) 
		{
			var item = (UI_HotFoodItem)gObject;
			var id = m_FoodList[index];
			gObject.data = id;

			item.m_icon.SetIcon(Utils.GetItemIcon(1, id));
			item.m_check.selectedIndex = id == m_SelectFoodId ? 1 : 0;
			if (m_view.m_hoting.selectedIndex == 1) 
				item.m_state.selectedIndex = id == m_SelectFoodId ? 0 : 1;
			else item.m_state.selectedIndex = 0;
		}

		void OnHotFoodItemClick(EventContext context) 
		{
			if (m_view.m_hoting.selectedIndex == 1) return;

			GObject item = (GObject)context.data;
			m_SelectFoodId = (int)item.data;
			RefreshHotFood();
		}

        partial void OnStartBtnClick(EventContext data)
        {
			if (m_SelectFoodId == 0) 
			{
				"@ui_hotfood_noselect_tip".Tips();
				return;
			}
			HotFoodModule.Instance.StartFood(m_SelectFoodId);
        }

        partial void OnStopBtnClick(EventContext data)
        {
			m_SelectFoodId = 0;
			HotFoodModule.Instance.StopFood();
        }

        partial void UnInitLogic(UIContext context){
			m_Event.Close();
			m_Event = null;
		}
	}
}
