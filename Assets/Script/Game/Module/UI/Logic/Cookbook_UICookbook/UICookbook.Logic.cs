
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
		private IList _itemDatas;

		#region Init
		partial void InitLogic(UIContext context)
		{
			m_view.m_list.itemRenderer = OnSetItemInfo;
			SwitchTabsPage(0);
			OnTabsChanged(null);
		}

		partial void UnInitLogic(UIContext context)
		{
			_itemDatas = null;
		}
		#endregion

		#region Tabs

		partial void OnTabsChanged(EventContext data)
		{
			switch (m_view.m_tabs.selectedIndex)
			{
				case 0:
					OnCookBookTab();
					break;
			}
		}

		void OnSetItemInfo(int index, GObject gObject)
		{

			switch (m_view.m_tabs.selectedIndex)
			{
				case 0:
					SetBookInfo(index, gObject);
					break;
			}

		}

		#endregion

		#region CookBook


		void OnCookBookTab()
		{
			m_view.m_list.RemoveChildrenToPool();
			_itemDatas = DataCenter.CookbookUtils.GetBookIDs();
			m_view.m_list.numItems = _itemDatas.Count;
		}

		void SetBookInfo(int index, GObject gObject)
		{
			var data = DataCenter.CookbookUtils.GetBook((int)_itemDatas[index]);
			gObject.data = data;
			if (data != null)
			{
				gObject.SetIcon(data.cfg.Icon);
				gObject.onClick.Clear();
				gObject.onClick.Add(OnBookClick);
				UIListener.SetControllerSelect(gObject, "__redpoint", data.CanUpLv(out _) ? 1 : 0, false);
			}
		}

		void OnBookClick(EventContext data)
		{
			var d = (data.sender as GObject)?.data;
			if (d != null)
			{
				DelayExcuter.Instance.OnlyWaitUIClose("cookbookup", OnCookBookTab , true);
				SGame.UIUtils.OpenUI("cookbookup", d);
			}
			else
				log.Error("数据为空");
		}

		#endregion

	}
}
