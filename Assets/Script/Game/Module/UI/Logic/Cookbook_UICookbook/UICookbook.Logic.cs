
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Cookbook;
	using System.Collections;
	using System.Collections.Generic;

	public interface ICookbookSub
	{
		public UI_CookbookUI view { get; set; }

		public void Enter();

		public void Update();

		public void OnSetListItem(int index, GObject gObject);

		public void Exit();

		public void Clear();

	}

	public partial class UICookbook
	{
		private IList _itemDatas;
		private Dictionary<int, ICookbookSub> _childs = new Dictionary<int, ICookbookSub>();
		private ICookbookSub _currentChild;

		#region Init
		partial void InitLogic(UIContext context)
		{
			m_view.m_list.itemRenderer = OnSetItemInfo;

			m_eventContainer += EventManager.Instance.Reg<string>(((int)GameEvent.UI_HIDE), OnCookbookUpClose);
			m_eventContainer += EventManager.Instance.Reg(((int)GameEvent.GAME_MAIN_REFRESH), OnViewRefresh);
			m_eventContainer += EventManager.Instance.Reg<int>(((int)GameEvent.WORKER_UPDAETE), id => OnViewRefresh());



			#region 注册

			_childs.Add(2, new CollectWorkerTab() { type = 2 });
			_childs.Add(3, new CollectWorkerTab() { type = 3 });


			#endregion

			m_view.m_waiter.visible = m_view.m_waiter.name.IsOpend(false);
			m_view.m_cooker.visible = m_view.m_cooker.name.IsOpend(false);
			
			OnViewRefresh();
			SwitchTabsPage(0);
			OnTabsChanged(null);

			CustomerBookInit();
		}

		partial void UnInitLogic(UIContext context)
		{
			_itemDatas = null;
			m_eventContainer.Close();
			m_eventContainer = null;
			foreach (var item in _childs)
				item.Value?.Clear();
			_childs.Clear();
		}
		#endregion

		#region Tabs

		partial void OnTabsChanged(EventContext data)
		{
			var index = m_view.m_tabs.selectedIndex;
			m_view.m_body.SetTextByKey("ui_cookbook_title_" + index);
			var child = GetChildTab(index);
			if (_currentChild != null)
			{
				_currentChild.Exit();
				_currentChild = null;
			}
			if (child != null)
			{
				child.view = m_view;
				child.Enter();
				_currentChild = child;
			}
			else
			{
				switch (m_view.m_tabs.selectedIndex)
				{
					case 0:
						OnCookBookTab();
						break;
				}
			}
		}

		void OnSetItemInfo(int index, GObject gObject)
		{
			if (_currentChild != null)
			{
				_currentChild.OnSetListItem(index, gObject);
				return;
			}
			switch (m_view.m_tabs.selectedIndex)
			{
				case 0:
					SetBookInfo(index, gObject);
					break;
			}

		}

		private ICookbookSub GetChildTab(int index)
		{
			if (_childs.TryGetValue(index, out var child)) return child;
			return default;
		}

		#endregion

		#region CookBook

		void OnViewRefresh()
		{
			m_view.m_waiter.m___redpoint.selectedIndex = DataCenter.WorkerDataUtils.Check(2) ? 1 : 0;
			m_view.m_cooker.m___redpoint.selectedIndex = DataCenter.WorkerDataUtils.Check(1) ? 1 : 0;

		}

		void OnCookbookUpClose(string name)
		{
			if (name == "cookbookup") OnCookBookTab();
		}

		void OnCookBookTab()
		{
			if (m_view == null || m_view.m_list == null) return;
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
				SGame.UIUtils.OpenUI("cookbookup", d);
			else
				log.Error("数据为空");
		}

		#endregion

	}
}
