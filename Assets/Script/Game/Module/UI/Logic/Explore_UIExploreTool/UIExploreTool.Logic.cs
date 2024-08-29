namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Explore;
	using GameConfigs;
	using System;
	using System.Collections;

	public partial class UIExploreTool
	{
		private int[] _currentData;
		private int[] _nextData;
		private ExploreData _data;
		private Action<bool> _timer;
		private double _price;

		private Controller _dCtr;
		private double _diamondCount;

		private EventHandleContainer eventHandle = new EventHandleContainer();

		const string c_explore_ad = "explore_ad";

		partial void InitLogic(UIContext context)
		{
			_dCtr = m_view.m_diamondbtn.GetController("gray");
			eventHandle += EventManager.Instance.Reg(((int)GameEvent.EXPLORE_TOOL_UP_LV), OnExploreToolUpLv);
			DoOpen(context);
		}

		partial void DoOpen(UIContext context)
		{
			_data = DataCenter.Instance.exploreData;
			var cfg = _data.exploreToolLevel;
			var next = _data.exploreToolNextLevel;

			_currentData = cfg.GetQualityWeightArray();
			_nextData = next.IsValid() ? next.GetQualityWeightArray() : _currentData;
			m_view.m_level.SetTextByKey("ui_common_lv", _data.toolLevel, _data.toolMaxLv);

			_diamondCount = PropertyManager.Instance.GetItem(2).num;
			m_view.m_type.selectedIndex = -1;
			m_view.m_adbtn.grayed = !DataCenter.AdUtil.IsAdCanPlay(c_explore_ad) || !NetworkUtils.IsNetworkReachability();
			SetInfo();
			SetList();
		}

		void SetInfo()
		{
			if (!_data.IsExploreToolMaxLv())
			{
				var state = _data.exploreToolLevel.MapId > 0 && DataCenter.Instance.roomData.roomID < _data.exploreToolLevel.MapId;
				if (state)
				{
					if (ConfigSystem.Instance.TryGet<RoomRowData>(_data.exploreToolLevel.MapId, out var scene))
						m_view.m_condition.SetTextByKey("ui_exploretool_condition", scene.Name.Local());
					else
						m_view.m_condition.SetTextByKey("ui_exploretool_condition", _data.exploreToolLevel.MapId);
				}
				else if (_data.exploreToolLevel.ExploreLevel > 0 && _data.exploreToolLevel.ExploreLevel > _data.level)
				{
					state = true;
					m_view.m_condition.SetTextByKey("ui_exploretool_condition1", _data.exploreToolLevel.ExploreLevel);
				}

				m_view.m_condition.visible = state;
				m_view.m_click.SetText(Utils.ConvertNumberStrLimit3(_data.exploreToolLevel.Cost(2)), false);
				RefreshState();
			}
			else
			{
				m_view.m_type.selectedIndex = 3;
			}
		}

		void SetAdBtnInfo()
		{
			var v = _data.exploreToolLevel.ADTime;
			if (v > 60)
				m_view.m_adbtn.SetTextByKey("ui_explore_ad_reduce_1", (_data.exploreToolLevel.ADTime / 60));
			else
				m_view.m_adbtn.SetTextByKey("ui_explore_ad_reduce_2", _data.exploreToolLevel.ADTime);
		}

		void SetCompleteBtnInfo()
		{
			var v = _price = _data.price * Math.Ceiling(_data.ToolUpRemaining() / 300f);
			if (v >= 0)
			{
				m_view.m_diamondbtn.SetText(Utils.ConvertNumberStrLimit3(v), false);
				if (_dCtr != null) _dCtr.selectedIndex = v > _diamondCount ? 1 : 0;
			}
		}

		void RefreshState()
		{
			var type = 0;
			if (_data.uplvtime > GameServerTime.Instance.serverTime) type = 1;
			if (type == 0) type = PropertyManager.Instance.CheckCountByArgs(_data.exploreToolLevel.GetCostArray()) ? 0 : 2;

			m_view.m_type.selectedIndex = type;
		}

		void RefreshTime()
		{
			IEnumerator Run()
			{
				while (_data != null && _data.ToolUpRemaining() > 0)
				{
					SetCompleteBtnInfo();
					m_view.m_time.text = Utils.FormatTime(_data.ToolUpRemaining());
					yield return 0;
				}
			}

			Run().Start();

		}

		partial void OnTypeChanged(EventContext data)
		{
			var s = m_view.m_type.selectedIndex;
			switch (s)
			{
				case 1:
					SetAdBtnInfo();
					RefreshTime();
					break;
			}
		}

		void SetList()
		{
			m_view.m_list.RemoveChildrenToPool();
			for (var i = EnumQualityType.White; i <= EnumQualityType.Max; i++)
				SGame.UIUtils.AddListItem(m_view.m_list, OnSetItemInfo);
		}

		void OnSetItemInfo(int index, object data, GObject gObject)
		{
			var view = gObject as UI_ExploreToolItem;
			var val = _currentData[index] * ConstDefine.C_PER_SCALE;
			var next = _nextData[index] * ConstDefine.C_PER_SCALE;
			view.m_quality.selectedIndex = index + 1;
			view.m_val.SetText(val + "%", false);
			view.m_next.SetText(next + "%", false);
			view.m_upcolor.selectedIndex = 1;
		}

		void OnExploreToolUpLv() { }

		partial void OnClickClick(EventContext data)
		{
			if (RequestExcuteSystem.ExploreBeginToolUpLv())
				SwitchTypePage(1);
		}

		partial void OnAdbtnClick(EventContext data)
		{
			var t = _data.exploreToolLevel.ADTime;
			AdModule.PlayAd(c_explore_ad, (s) =>
			{
				if (s)
				{
					_data.uplvtime -= t;
					if (_data.uplvtime <= 0)
					{
						_data.uplvtime = 0;
						_price = 0;
						OnDiamondbtnClick(null);
					}
				}
			});
		}

		partial void OnDiamondbtnClick(EventContext data)
		{
			if (RequestExcuteSystem.ExploreToolUpLv(true, _price))
				DoOpen(null);
		}

		partial void DoHide(UIContext context)
		{
			_data = null; _currentData = null; _nextData = null;
		}

		partial void UnInitLogic(UIContext context)
		{
			eventHandle?.Close();
			eventHandle = null;
		}
	}
}
