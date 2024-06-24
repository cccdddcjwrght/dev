
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Cookbook;
	using System.Collections;
	using System.Collections.Generic;

	public partial class UICookbook
	{
		public enum CustomerBookState
		{
			UNLOCK	= 0, // 已解锁已领取
			CANTAKE = 1, // 已解锁未领取
			LOCK	= 2, // 未解锁
		}
		void CustomerBookInit()
		{
			m_view.m_listCustomer.itemRenderer = OnRenderCustomer;
			m_view.m_listCustomer.numItems = CustomerBookModule.Instance.Datas.Count;
		}

		void OnCustomerBookClick(EventContext data)
		{
			log.Info("on click =");
		}
		
		void OnRenderCustomer(int index, GObject gObject)
		{
			var ui = gObject as UI_BookCustomer;
			var datas = CustomerBookModule.Instance.Datas;
			var data = datas[index];
			
			// 设置UI状态
			CustomerBookState state = CustomerBookState.LOCK;
			if (!data.IsUnlock)
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
			
			// 设置图标
			if (state != CustomerBookState.UNLOCK)
			{
				ui.m_body.m_body.icon = data.Icon;
			}

			// 设置按钮响应
			if (state == CustomerBookState.CANTAKE)
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
