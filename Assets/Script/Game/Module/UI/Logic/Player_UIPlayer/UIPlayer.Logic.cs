
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Player;
	using System.Collections.Generic;
	using System.Net.NetworkInformation;
	using GameConfigs;

	public partial class UIPlayer
	{

		private List<EquipItem> _eqs;

		private EquipItem _current;
		private List<EquipItem> _mats = new List<EquipItem>();

		partial void InitLogic(UIContext context)
		{
			m_view.m_eqTab.selectedIndex = -1;
			m_view.m_list.itemRenderer = OnSetEquipInfo;
			m_view.m_list.SetVirtual();

			InitEquipPage();
			InitUpQualityPage();
		}

		partial void DoShow(UIContext context)
		{
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
			switch (m_view.m_tabs.selectedIndex)
			{
				case 0: OnEquipPage(); break;
				case 1: OnUpQualityPage(); break;
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

		private void InitUpQualityPage()
		{
			m_view.m_EquipQuality.m_progress.max = ConstDefine.EQUIP_UP_QUALITY_MAT_COUNT;
		}

		private void OnUpQualityPage()
		{
			m_view.m_EquipQuality.m_progress.value = 0;
			SwitchEquipUpQuality_StatePage(0);
		}

		void SetPlayerEquipsInfo()
		{
			m_view.m_EquipPage
				.SetEquipInfo()
				.RefreshModel();
		}

		#endregion


		void SetEquipList(List<EquipItem> eqs = null)
		{
			_eqs = eqs ?? DataCenter.Instance.equipData.items;
			_eqs.Sort((a, b) =>
			{
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

		void OnSetEquipInfo(int index, GObject gObject)
		{

			(gObject as UI_Equip).SetEquipInfo(_eqs[index]);
			(gObject as UI_Equip).onClick.Clear();
			(gObject as UI_Equip).onClick.Add(() => OnEqClick(gObject, _eqs[index]));
			(gObject as UI_Equip).m_select.selectedIndex = 0;
		}

		void OnEqClick(GObject gObject, EquipItem data)
		{
			switch (m_view.m_eqTab.selectedIndex)
			{
				case 0: EquipOnOnOff(gObject, data); break;
				case 1: EquipSelectUp(gObject, data); break;
			}

		}

		void EquipOnOnOff(GObject gObject, EquipItem data)
		{
			if (data != null && data.cfgID > 0)
			{
				data.isnew = 0;
				SGame.UIUtils.OpenUI("eqtipsui", data);
				if (gObject != null)
					UIListener.SetControllerSelect(gObject, "__redpoint", 0, false);
			}
		}

		void EquipSelectUp(GObject gObject, EquipItem data)
		{
			var eq = gObject as UI_Equip;
			if (_current == null)
			{
				if (data.quality >= 7)
				{
					"@ui_equip_max_quality".Tips();
					return;
				}
				_current = data;
				SwitchEquipUpQuality_StatePage(1);
			}
			else
			{
				var s = eq.m_select.selectedIndex == 0 ? 1 : 0;
				if (s == 0) _mats.Remove(data);
				else
				{
					if (_mats.Count >= ConstDefine.EQUIP_UP_QUALITY_MAT_COUNT)
					{
						"@ui_equip_mat_max".Tips();
						return;
					};
					_mats.Add(data);
				}
				m_view.m_EquipQuality.m_progress.value = _mats.Count;
				eq.m_select.selectedIndex = s;

			}
		}

		partial void OnEquipUpQuality_StateChanged(EventContext data)
		{
			var eqs = default(List<EquipItem>);
			if (m_view.m_EquipQuality.m_state.selectedIndex == 1)
			{
				var effects = DataCenter.EquipUtil.GetEquipEffects(_current.cfg, _current.quality + 1);
				var next = _current.Clone();
				next.quality++;
				eqs = DataCenter.Instance.equipData.items.FindAll(e => e != _current && _current.quality == e.quality);
				m_view.m_EquipQuality.m_selecteq.SetEquipInfo(_current , true);
				m_view.m_EquipQuality.m_nexteq.SetEquipInfo(next , true);
				if (effects.Count > 0)
				{
					var cfg = effects[effects.Count - 1];
					if (ConfigSystem.Instance.TryGet<BuffRowData>(cfg[0], out var buff))
						SetEquipUpQuality_NextattrText("@ui_equip_add_attr".Local(null,buff.Describe.Local(null, cfg[1])));
				}
			}
			else
			{
				_current = null;
				m_view.m_EquipQuality.m_progress.value = 0;
				m_view.m_EquipQuality.m_nextattr.SetText(null, false);
				eqs = DataCenter.Instance.equipData.items;
				_mats?.Clear();
			}
			SetEquipList(eqs);
		}

		partial void OnEquipUpQuality_ClickClick(EventContext data)
		{
			if (_current == null)
			{ "@ui_equip_tips1".Tips(); return; }
			else if (_mats.Count < ConstDefine.EQUIP_UP_QUALITY_MAT_COUNT)
			{ "@ui_equip_tips2".Tips(); return; }
			RequestExcuteSystem.EquipUpQuality(_current, _mats);
			SwitchEquipUpQuality_StatePage(0);
		}
	}
}
