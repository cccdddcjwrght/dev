
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

			CustomerBookInit();
		}

		partial void UnInitLogic(UIContext context)
		{
			_itemDatas = null;
			m_eventContainer.Close();
			m_eventContainer = null;
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
			var bs = DataCenter.CookbookUtils.GetBooks();
			bs.Sort((b, a) =>
			{

				var v1 = a.IsEnable();
				var v2 = b.IsEnable();
				var ret = v1.CompareTo(v2);
				if (ret == 0)
				{
					v1 = a.CanUpLv(out _);
					v2 = b.CanUpLv(out _);
					ret = v1.CompareTo(v2);
					if (ret == 0)
						ret = -a.id.CompareTo(b.id);
				}
				return ret;

			});
			_itemDatas = bs;
			m_view.m_list.numItems = _itemDatas.Count;
		}

		void SetBookInfo(int index, GObject gObject)
		{
			var data = (CookBookItem)_itemDatas[index];
			var openFoodTypes = TableManager.Instance.GetOpenFoodTypes();

			gObject.name = index.ToString();
			gObject.data = data;
			if (data != null)
			{
				gObject.SetIcon(data.cfg.Icon);
				(gObject as UI_BookItem).m_level.SetTextByKey("ui_common_lv", data.level, data.maxLv);
				gObject.onClick.Clear();
				gObject.onClick.Add(OnBookClick);
				//gObject.grayed = !(openFoodTypes.Contains(data.id));

				var state = 0;
				if (!data.IsEnable())
				{
					state = 3;
					if (data.lvCfg.ConditionType == 3 && Utils.CheckItemCount(data.lvCfg.ConditionValue(0), data.lvCfg.ConditionValue(1), false))
						state = 2;
				}
				else if (data.CanUpLv(out _))
					state = 1;

				UIListener.SetControllerSelect(gObject, "state", state, false);
			}
		}

		void OnBookClick(EventContext data)
		{
			var d = (data.sender as GObject)?.data;
			if (d != null)
			{
				DelayExcuter.Instance.OnlyWaitUIClose("cookbookup", OnCookBookTab, true);
				SGame.UIUtils.OpenUI("cookbookup", d);
			}
			else
				log.Error("数据为空");
		}

		#endregion

	}
}
