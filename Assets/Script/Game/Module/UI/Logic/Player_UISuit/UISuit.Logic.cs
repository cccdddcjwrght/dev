
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Player;
	using System;
	using System.Collections.Generic;


	public partial class UISuit
	{
		private EquipItem equip;
		private List<int[]> _mats;
		private bool _flag = true;

		partial void InitLogic(UIContext context)
		{

			var eq = (context.GetParam().Value as object[]).Val<BaseEquip>(0);
			var showBtn = (context.GetParam().Value as object[]).Val<bool>(1, true);
			if (eq is EquipItem) equip = eq as EquipItem;
			else equip = new EquipItem() { cfgID = eq.cfgID, level = eq.level, quality = eq.quality, progress = 0 }.Refresh() as EquipItem;
			var n = equip.cfg.Name.Local();
			m_view.m_funcType.selectedIndex = -1;
			m_view.SetEquipInfo(equip, true);
			m_view.m_tips.SetTextByKey(equip.cfg.Description);
			m_view.m_flag.selectedIndex = showBtn ? 0 : 1;
			m_view.m_funcType.selectedIndex = equip.level > 0 ? 1 : 0;
			m_view.m_body.SetTextByKey(equip.name);

		}

		partial void OnFuncTypeChanged(EventContext data)
		{
			switch (m_view.m_funcType.selectedIndex)
			{
				case 0:
					m_view.m_click.SetTextByKey("ui_suit_compose");
					SetMatItems();
					break;
				case 1:
					m_view.m_click.SetTextByKey(equip.pos == 0 ? "ui_equip_on" : "ui_equip_off");
					break;
			}
		}

		partial void UnInitLogic(UIContext context)
		{

		}

		partial void OnClickClick(EventContext data)
		{
			switch (m_view.m_funcType.selectedIndex)
			{
				case 0:
					if (!_flag) { "@ui_equip_tips2".Tips(); return; }
					if (!RequestExcuteSystem.SuitCompose(equip, _mats)) return;
					break;
				case 1:
					RequestExcuteSystem.PutOnOrOffSuit(equip);
					break;
			}
			DoCloseUIClick(null);
		}

		void SetMatItems()
		{
			_mats = Utils.GetArrayList(
						equip.cfg.GetPart1Array,
						equip.cfg.GetPart2Array,
						equip.cfg.GetPart3Array,
						equip.cfg.GetPart4Array,
						equip.cfg.GetPart5Array
					);

			if (_mats != null && _mats.Count > 0)
				SGame.UIUtils.AddListItems(m_view.m_list, _mats, SetItemInfo);

		}

		void SetItemInfo(int index, object data, GObject gObject)
		{
			var info = data as int[];
			var eq = new EquipItem().Convert(info[1], 0, info[0]).Refresh();
			_flag = gObject.SetEquipInfo(eq, true, needcount: info[2], showmask: true) && _flag;
		}

	}
}
