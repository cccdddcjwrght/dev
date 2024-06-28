
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Cookbook;
	using System.Collections;
	using System.Collections.Generic;
	using Unity.Entities;

	public partial class UICookbook
	{
		private EventHandleContainer m_eventContainer = new EventHandleContainer();
		public enum CustomerBookState
		{
			UNLOCK	= 0, // 已解锁已领取
			CANTAKE = 1, // 已解锁未领取
			LOCK	= 2, // 未解锁
			FIRST   = 3, // 首次打开
		}
		void CustomerBookInit()
		{
			m_view.m_listCustomer.itemRenderer = OnRenderCustomer;
			CustomerBookModule.Instance.ReSort();
			m_view.m_listCustomer.numItems = CustomerBookModule.Instance.Datas.Count;
			m_eventContainer += EventManager.Instance.Reg((int)GameEvent.CUSTOMER_BOOK_UPDATE, OnCustomerBookUpdate);
		}

		void OnCustomerBookUpdate()
		{
			CustomerBookModule.Instance.ReSort();
			m_view.m_listCustomer.numItems = CustomerBookModule.Instance.Datas.Count;
		}

		void OnCustomerBookClick(EventContext data)
		{
			EntityManager mgr = World.DefaultGameObjectInjectionWorld.EntityManager;
			var customerData = (data.sender as GObject).data as CustomerBookData;
				
			string uiName = "customerbookup";
			if (!customerData.isOpened)
				uiName = "customerbookfirstup";
			Entity ui = UIRequest.Create(mgr, SGame.UIUtils.GetUI(uiName));
			ui.SetParam((data.sender as GObject).data);
		}
		
		void OnRenderCustomer(int index, GObject gObject)
		{
			var ui = gObject as UI_BookCustomer;
			var datas = CustomerBookModule.Instance.Datas;
			var data = datas[index];
			gObject.data = data;
			
			// 设置UI状态
			CustomerBookState state = CustomerBookState.LOCK;
			if (!data.isOpened && data.IsUnlock)
			{
				state = CustomerBookState.FIRST;
			}
			else if (!data.IsUnlock)
			{
				// 已锁
				state = CustomerBookState.LOCK;
			}
			else if (data.isRewarded)
			{
				// 判断是否领取奖励
				state = CustomerBookState.UNLOCK;
			}
			else
			{
				// 可领取奖励
				state = CustomerBookState.CANTAKE;
			}
			ui.m_state.selectedIndex = (int)state;
			
			if (state != CustomerBookState.LOCK)
			{
				// 设置图标
				ui.m_body.m_body.icon = data.Icon;
				
				// 设置名称
				ui.title = data.Name;
			}


			// 设置按钮响应
			if (state != CustomerBookState.LOCK)
			{
				ui.onClick.Set(OnCustomerBookClick);
			}
			else
			{
				ui.onClick.Clear();
			}
		}
	}
}
