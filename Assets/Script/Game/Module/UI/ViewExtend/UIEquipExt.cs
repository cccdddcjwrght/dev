using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame.UI.Player
{
	partial class UI_Equip
	{
		public UI_Equip SetInfo(GameConfigs.EquipmentRowData cfg)
		{
			if (cfg.IsValid())
			{
				this.SetTextByKey(cfg.Name);
				this.SetIcon(cfg.Icon);
				this.m_quality.selectedIndex = cfg.Quality;
				this.m_level.SetText(cfg.Level.ToString(), false);
			}
			return this;
		}

		public UI_Equip SetInfo(EquipItem equip)
		{
			SetInfo(equip.cfg);
			this.m_level.SetText(equip.level.ToString(), false);
			return this;
		}

	}
}
