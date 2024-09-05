
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
	using System.Collections;
	using SGame.UI.Common;
	using SGame.UI.Pet;

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

			m_view.m_equipup.onClick.AddCapture(OnCheckTabOpen);

			context.onHide += DoHide;

		}

		partial void DoShow(UIContext context)
		{
			m_view.m_body.SetCurrency(2, "currency");
		}

		void DoHide(UIContext context)
		{
			DataCenter.EquipUtil.CancelAllNewFlag();
		}

		partial void UnInitLogic(UIContext context)
		{
			m_view.m_EquipPage.Uninit();
		}

		void OnDataRefresh(bool refreshtabs = true)
		{
			if (refreshtabs) OnEqTabChanged(null);
		}

		partial void OnEqTabChanged(EventContext data)
		{
			RefreshMergeState();
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
			m_view.m_EquipPage.Init(
				(i, g) => OnEqClick(g, DataCenter.Instance.equipData.equipeds[i]),
				DoUpLv
			);
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

		void DoUpLv(int i, GObject gObject)
		{
			RequestExcuteSystem.EquipUpLevel(DataCenter.Instance.equipData.equipeds[i], out var success);
			if (success)
			{
				EffectSystem.Instance.AddEffect(27, gObject);
			}
		}

		#endregion

		#region Quality

		private void SetQualityNextInfo()
		{
			const string pstr = "+ {0}%";
			var attr = m_view.m_EquipQuality.m_attr;
			var next = _current.Clone().UpQuality();
			var quality = next.quality;
			var qt = next.qType;
			next.level = 1;

			m_view.m_EquipQuality.m_nexteq.SetEquipInfo(next, true);
			m_view.m_EquipQuality.m_curattr.SetText(string.Format(pstr, _current.GetAttrVal(false)), false);
			m_view.m_EquipQuality.m_nextattr.SetText(string.Format(pstr, next.GetAttrVal(false)), false);

			var buff = DataCenter.EquipUtil.GetQualityUnlockBuff(_current.cfg, quality);
			attr.visible = false;
			if (buff != null)
			{
				attr.visible = true;
				attr.SetBuffItem(buff, qt, type: 1, usecolor: false);
			}

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
				case 4:
					eqs = DataCenter.Instance.equipData.items.FindAll(e => e != _current && _current.quality == e.quality && _current.cfg.Group == e.cfg.Group);
					break;
				case 3:
					matcount = 1;
					eqs = eqs ?? new List<EquipItem>();
					eqs.Clear();
					eqs.Add(new EquipItem().Convert(ConstDefine.EQUIP_UPQUALITY_MAT, 0, 1));
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
					gObject.SetEquipInfo(eq, true);
					UIListener.SetControllerSelect(gObject, "outside", 1, false);
					gObject.onClick.Clear();
					gObject.onClick.Add(() => SwitchEquipUpQuality_StatePage(0));
				}
				else if (data is int)
					(gObject as UI_MatDiv).m_type.selectedIndex = _items.Count > 3 ? 0 : 1;
				else
				{
					gObject.SetEquipInfo(_current.qcfg.AdvanceType == 3 ? null : _current, true);
					gObject.SetMerge(_current);
				}

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
			var eq = (gObject as UI_BigEquip);
			if (_current == null)
			{
				if (data.quality >= (int)EnumQuality.Max) { "@ui_equip_max_quality".Tips(); return; }
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
				var item = list.GetChildAt(index);
				data.selected = s;
				eq.m_select.selectedIndex = s ? 1 : 0;
				item.SetEquipInfo(
					s ? _items[index] as EquipItem : _current.qcfg.AdvanceType != 3 ? _current : null,
					true, needcount: _current.qcfg.AdvanceValue
				);
				if (!s) item.SetMerge(_current);
				else UIListener.SetControllerSelect(item, "outside", 1, false);

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

			if (RequestExcuteSystem.EquipUpQuality(_current, _mats, out var count))
			{
				SGame.UIUtils.OpenUI("upqualitytips", _current, count);
				SwitchEquipUpQuality_StatePage(0);
			}
		}

		partial void OnEquipUpQuality_MergeClick(EventContext data)
		{

			if (RequestExcuteSystem.EquipAutoUpQuality(out var eqs, out var recycle))
			{
				SwitchEquipUpQuality_StatePage(0);
				SGame.Utils.ShowRewards(title: "@ui_equip_merge_title", contentCall: (view) => ShowRewardContent(view, eqs, recycle).Start());
			}
			else
				"@ui_equip_merge_tips".Tips();
		}

		partial void OnEquipUpQuality_HelpClick(EventContext data)
		{
			SGame.UIUtils.OpenUI("equphelp");
		}

		IEnumerator ShowRewardContent(UI_CommonRewardBody view, List<BaseEquip> eqs, double recycle)
		{
			const string rich_text = "<img src='{0}' width='45%' height='45%' />";
			22.ToAudioID().PlayAudio();
			var effect = EffectSystem.Instance.AddEffect(28, view);
			view.touchable = false;

			yield return EffectSystem.Instance.WaitEffectLoaded(effect);
			yield return new WaitForSeconds(1.5f);

			if (recycle > 0)
			{
				var url = UIListener.GetUIResFromPkgs(Utils.GetItemIcon(1, ConstDefine.EQUIP_UPLV_MAT));
				view.m_tips.SetText("ui_equip_upquality_recycle".Local(null, Utils.ConvertNumberStr(recycle)) + string.Format(rich_text, url));
			}

			view.m_list.columnGap = 25;
			eqs.Sort((a, b) => -a.quality.CompareTo(b.quality));
			SGame.UIUtils.AddListItems(view.m_list, eqs, SetRewardEqView, res: "ui://Player/EquipBox");
			yield return new WaitForSeconds(0.5f);
			view.touchable = true;
			DataCenter.EquipUtil.CheckCanMerge();
		}

		void SetRewardEqView(int index, object data, GObject g)
		{
			g.asCom.GetChild("body").SetEquipInfo(data as BaseEquip, true);
		}

		#endregion

		#region Suit

		private void EquipClickOnSuit(GObject gObject, EquipItem data)
		{
			if (data != null && data.cfgID > 0)
			{
				if (data.pos > 0)
				{
					EquipOnOrOff(gObject, data);
					return;
				}
				if (data.cfg.IsValid())
				{
					SGame.UIUtils.OpenUI("suitui", data);
					HideRed(gObject, data);
				}
			}
		}

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

		void OnCheckTabOpen(EventContext context)
		{

			if (context != null && context.sender != null)
			{
				if ((context.sender as GObject)?.name.IsOpend() == false)
				{
					context.StopPropagation();
				}
			}
		}

		void SetEquipList(List<EquipItem> eqs = null, bool nodef = false)
		{
			_eqs = eqs ?? (nodef ? null : DataCenter.Instance.equipData.items.FindAll(e => e.type > 0 && e.type < 5));
			if (_eqs != null)
			{
				_eqs.Sort((a, b) =>
				{
					a.selected = b.selected = false;
					var c = m_view.m_eqTab.selectedIndex == 1 ? -a.upflag.CompareTo(b.upflag) : 0;
					if (c == 0)
					{
						c = a.realType.CompareTo(b.realType);
						if (c == 0)
						{
							c = -a.quality.CompareTo(b.quality);
							if (c == 0) c = a.cfgID.CompareTo(b.cfgID);
						}
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
			gObject.name = index.ToString();
			var info = _eqs[index];
			var eq = gObject as UI_BigEquip;
			gObject.SetEquipInfo(info, checkup: m_view.m_eqTab.selectedIndex == 1);
			eq.onClick.Clear();
			eq.onClick.Add(() => OnEqClick(gObject, info));
			eq.m_select.selectedIndex = info.selected ? 1 : 0;
		}

		void OnEqClick(GObject gObject, EquipItem data)
		{
			switch (m_view.m_eqTab.selectedIndex)
			{
				case 0: EquipOnOrOff(gObject, data); break;
				case 1: EquipSelectUp(gObject, data); break;
				case 2: EquipClickOnSuit(gObject, data); break;
			}

		}

		void EquipOnOrOff(GObject gObject, EquipItem data)
		{
			if (data != null && data.cfgID > 0)
			{
				if (data.type > 4)
					SGame.UIUtils.OpenUI("suitui", data);
				else
					SGame.UIUtils.OpenUI("eqtipsui", data);
				HideRed(gObject, data);
			}
		}

		void RefreshMergeState()
		{
			var index = DataCenter.Instance.equipData.canMerge ? 1 : 0;
			if (m_view.m_eqTab.selectedIndex == 1) index = 0;
			m_view.m_equipup.m___redpoint.selectedIndex = index;

			index =  DataCenter.Instance.equipData.canAutoMerge ? 2 : index;
			m_view.m_canmerge.selectedIndex = index;

		}

		void HideRed(GObject gObject, EquipItem data)
		{
			data.isnew = 0;
			if (gObject != null)
			{
				UIListener.SetControllerSelect(gObject, "__redpoint", data.CheckMats() ? 1 : 0, false);
			}
		}
		#endregion


	}
}
