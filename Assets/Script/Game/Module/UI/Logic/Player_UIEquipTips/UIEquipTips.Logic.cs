
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Player;
	using GameConfigs;
	using System.Collections.Generic;

	public partial class UIEquipTips
	{
		private EquipItem equip;
		private List<int[]> effects;

		partial void InitLogic(UIContext context)
		{
			m_view.z = -500;
			equip = (context.GetParam().Value as object[]).Val<EquipItem>(0);
			m_view.m_list.itemRenderer = OnSetEffect;
			SetInfo();
		}

		partial void DoShow(UIContext context)
		{
		}

		void SetInfo()
		{
			ConfigSystem.Instance.TryGet<BuffRowData>(equip.cfg.MainBuff(0), out var buff);

			m_view.m_lvmax.selectedIndex = 1;
			m_view.SetEquipInfo(equip);
			m_view.m_qualitytips.SetTextByKey("ui_quality_name_" + equip.cfg.Quality);
			m_view.m_attr.SetTextByKey(buff.Describe, equip.cfg.MainBuff(1));
			m_view.m_click.SetTextByKey(equip.pos == 0 ? "ui_equip_on" : "ui_equip_off");
			if (m_view.m_lvmax.selectedIndex == 0)
			{
				m_view.m_progress.value = 0;
				m_view.m_nextlvattr.SetTextByKey(buff.Describe, equip.cfg.MainBuff(1) + 5);
			}
			else
			{
				m_view.m_progress.value = 100;
				m_view.m_nextlvattr.SetTextByKey("ui_equip_lvmax");
			}

		}

		void SetEffectsInfo()
		{
			effects = DataCenter.EquipUtil.GetEquipEffects(equip.cfg);
			m_view.m_list.numItems = (int)effects?.Count;
		}

		void OnSetEffect(int index , GObject gObject) {

			var cfg = effects[index];
			if (ConfigSystem.Instance.TryGet<BuffRowData>(cfg[0] , out var buff))
				gObject.SetTextByKey(buff.Describe, cfg[1]);

		}

		partial void OnClickClick(EventContext data)
		{
			if (equip.pos == 0)
				DataCenter.EquipUtil.PutOn(equip);
			else
				DataCenter.EquipUtil.PutOff(equip);
			DoCloseUIClick(null);
		}

		partial void UnInitLogic(UIContext context)
		{

		}
	}
}
