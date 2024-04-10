
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Player;
	using System.Collections.Generic;
	using GameConfigs;
	using System;
	using SGame.UI.Main;
	using System.Linq;

	public partial class UIPlayer
	{

		private List<EquipItem> _eqs;


		private EquipItem _current;
		private List<EquipItem> _mats = new List<EquipItem>();
		private List<object> _items = new List<object>();

		private List<EquipItem> _suitItems;

		private static ItemData.Value _emptyItem = new ItemData.Value();

		partial void InitLogic(UIContext context)
		{
			m_view.z = 500;
			m_view.m_eqTab.selectedIndex = -1;
			m_view.m_list.itemRenderer = OnSetEquipInfo;
			m_view.m_list.SetVirtual();

			InitEquipPage();
			InitUpQualityPage();

			m_view.m_eqTab.selectedIndex = 0;
			OnDataRefresh(false);
		}

		partial void UnInitLogic(UIContext context)
		{
			m_view.m_EquipPage.Uninit();
			DataCenter.EquipUtil.CancelAllNewFlag();
		}

		void OnDataRefresh(bool refreshtabs = true)
		{
			if (refreshtabs) OnEqTabChanged(null);
		}

		partial void OnEqTabChanged(EventContext data)
		{
			switch (m_view.m_eqTab.selectedIndex)
			{
				case 0: OnEquipPage(); break;
				case 1: OnUpQualityPage(); break;
				case 2: OnEquipSuitPage(); break;
			}
		}

		#region Tab

		private void InitEquipPage()
		{
			m_view.m_EquipPage.Init((i, g) => OnEqClick(g, DataCenter.Instance.equipData.equipeds[i]));
		}

		private void OnEquipPage()
		{
			var view = m_view.m_EquipPage;
			view.SetInfo(null);
			SetPlayerEquipsInfo();
			SetEquipList();
		}

		private void OnEquipSuitPage()
		{
			SetEquipList(GetSuitItems(), true);
		}

		private void InitUpQualityPage()
		{
		}

		private void OnUpQualityPage()
		{
			SetEquipList();
			SwitchEquipUpQuality_StatePage(0);
		}

		void SetPlayerEquipsInfo()
		{
			m_view.m_EquipPage
				.SetInfo(null)
				.SetEquipInfo()
				.RefreshModel();
		}

		#endregion

		#region Quality

		private void SetQualityNextInfo()
		{
			const string pstr = "+ {0}%";

			var next = _current.Clone().UpQuality();
			next.level = 1;

			m_view.m_EquipQuality.m_nexteq.SetEquipInfo(next, true);
			m_view.m_EquipQuality.m_curattr.SetText(string.Format(pstr, _current.GetAttrVal(false)), false);
			m_view.m_EquipQuality.m_nextattr.SetText(string.Format(pstr, next.GetAttrVal(false)), false);
			m_view.m_EquipQuality.m_addeffect.SetInfo(next);

		}

		private List<EquipItem> SetQualityMats()
		{
			var matcount = _current.qcfg.AdvanceValue;
			var eqs = default(List<EquipItem>);
			_items.Clear(); _items.Add(_current); _items.Add(0);

			switch (_current.qcfg.AdvanceType)
			{
				case 1:
					eqs = DataCenter.Instance.equipData.items.FindAll(e => e != _current && _current.quality == e.quality);
					break;
				case 2:
					eqs = DataCenter.Instance.equipData.items.FindAll(e => e != _current && _current.quality == e.quality && _current.type == e.type);
					break;
				case 3:
					matcount = 1;
					var item = PropertyManager.Instance.GetItem(ConstDefine.EQUIP_UPQUALITY_MAT);
					eqs = new List<EquipItem>();
					if (item.num > 0) eqs.Add(new EquipItem().Convert(item));
					break;
			}

			if (matcount > 0)
			{
				for (int i = 0; i < matcount; i++)
					_items.Add(null);
			}

			m_view.m_EquipQuality.m_list.RemoveChildrenToPool();
			SGame.UIUtils.AddListItems(m_view.m_EquipQuality.m_list, _items, (index, data, gObject) =>
			{

				if (data is BaseEquip eq)
				{
					(gObject as UI_Equip)?.SetEquipInfo(eq, true);
					gObject.onClick.Clear();
					gObject.onClick.Add(() => SwitchEquipUpQuality_StatePage(0));
				}
				else if (data is int)
					(gObject as UI_MatDiv).m_type.selectedIndex = _items.Count > 3 ? 0 : 1;
				else
					(gObject as UI_Equip)?.SetEquipInfo(null, true, 0);


			}, null, new Func<object, string>(GetMatRes));

			return eqs;
		}

		string GetMatRes(object data)
		{

			if (data is int) return "ui://Player/MatDiv";
			return "ui://Player/Equip";
		}

		void EquipSelectUp(GObject gObject, EquipItem data)
		{
			var eq = gObject as UI_Equip;
			if (_current == null)
			{
				if (data.quality >= 7) { "@ui_equip_max_quality".Tips(); return; }
				_current = data;
				SwitchEquipUpQuality_StatePage(1);
			}
			else
			{
				var s = !data.selected;
				var index = _items.IndexOf(data);
				var list = m_view.m_EquipQuality.m_list;
				if (!s)
				{
					_items[index] = null;
					_mats.Remove(data);
				}
				else
				{
					if ((index = _items.IndexOf(null)) < 0) { "@ui_equip_mat_max".Tips(); return; };
					_items[index] = data;
					_mats.Add(data);
				}
				list.GetChildAt(index).SetEquipInfo(_items[index] as EquipItem, true, needcount: _current.qcfg.AdvanceValue);
				data.selected = s;
				eq.m_select.selectedIndex = s ? 1 : 0;

			}
		}

		partial void OnEquipUpQuality_StateChanged(EventContext data)
		{

			var eqs = default(List<EquipItem>);
			if (m_view.m_EquipQuality.m_state.selectedIndex == 1)
			{
				SetQualityNextInfo();
				eqs = SetQualityMats();
			}
			else
			{
				_current = null;
				_mats?.Clear();
				_items?.Clear();
				m_view.m_EquipQuality.m_list.RemoveChildrenToPool();
			}
			SetEquipList(eqs);
		}

		partial void OnEquipUpQuality_ClickClick(EventContext data)
		{
			if (_current == null)
			{ "@ui_equip_tips1".Tips(); return; }
			else if (_items.IndexOf(null) >= 0)
			{ "@ui_equip_tips2".Tips(); return; }
			else if (
				_current.qcfg.AdvanceType == 3
				&& _current.qcfg.AdvanceValue > PropertyManager.Instance.GetItem(ConstDefine.EQUIP_UPQUALITY_MAT).num)
			{ "@ui_equip_tips2".Tips(); return; }
			RequestExcuteSystem.EquipUpQuality(_current, _mats);
			SwitchEquipUpQuality_StatePage(0);
		}

		#endregion

		#region Suit

		private List<EquipItem> GetSuitItems()
		{
			var list = DataCenter.Instance.equipData.items.FindAll(e => e.type > 4);
			if (_suitItems == null)
			{
				_suitItems = ConfigSystem.Instance
					.Finds<GameConfigs.ItemRowData>(c => c.Type == 6 || c.Type == 7)
					.Select(c => new EquipItem().Convert(c.ItemId, 0, 1))
					.ToList();
			}
			_suitItems.ForEach(i => i.Refresh());
			list.AddRange(_suitItems.FindAll(s => s.count > 0));
			return list;
		}

		#endregion

		#region Base
		void SetEquipList(List<EquipItem> eqs = null, bool nodef = false)
		{
			_eqs = eqs ?? (nodef ? null : DataCenter.Instance.equipData.items.FindAll(e => e.type < 5));
			if (_eqs != null)
			{
				_eqs.Sort((a, b) =>
				{
					a.selected = b.selected = false;
					var c = a.type.CompareTo(b.type);
					if (c == 0)
					{
						c = -a.quality.CompareTo(b.quality);
						if (c == 0) c = a.cfgID.CompareTo(b.cfgID);
					}
					return c;

				});

				m_view.m_list.numItems = _eqs.Count;
			}
			else
				m_view.m_list.numItems = 0;
		}

		void OnSetEquipInfo(int index, GObject gObject)
		{

			var info = _eqs[index];
			(gObject as UI_Equip).SetEquipInfo(info);
			(gObject as UI_Equip).onClick.Clear();
			(gObject as UI_Equip).onClick.Add(() => OnEqClick(gObject, info));
			(gObject as UI_Equip).m_select.selectedIndex = info.selected ? 1 : 0;
		}

		void OnEqClick(GObject gObject, EquipItem data)
		{
			switch (m_view.m_eqTab.selectedIndex)
			{
				case 0: EquipOnOrOff(gObject, data); break;
				case 1: EquipSelectUp(gObject, data); break;
			}

		}

		void EquipOnOrOff(GObject gObject, EquipItem data)
		{
			if (data != null && data.cfgID > 0)
			{
				data.isnew = 0;
				SGame.UIUtils.OpenUI("eqtipsui", data);
				if (gObject != null)
					UIListener.SetControllerSelect(gObject, "__redpoint", 0, false);
			}
		}
		#endregion


	}
}
