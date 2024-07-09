
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Player;
	using GameConfigs;

	public partial class UIEqUpHelp
	{
		BaseEquip baseEquip;
		BaseEquip mat;

		partial void InitLogic(UIContext context)
		{
			var eqid = GlobalDesginConfig.GetInt("equip_help_id", 300101);
			baseEquip = new BaseEquip() { cfgID = eqid, quality = 1, level = 1 };
			baseEquip.Refresh();
			mat = new EquipItem().Convert(ConstDefine.EQUIP_UPQUALITY_MAT, 0, 1);

			m_view.m_list.RemoveChildrenToPool();
			m_view.m_list.itemRenderer = OnSetInfo;
			m_view.m_list.numItems = (int)EnumQuality.Max - 1;

		}

		void OnSetInfo(int index, GObject gObject)
		{
			if (ConfigSystem.Instance.TryGet<GameConfigs.EquipQualityRowData>(index + 1, out var cfg))
			{
				var ui = gObject as UI_EqHelpItem;
				if (ui != null)
				{
					var q = index + 1;
					baseEquip.UpQuality(q);
					SetMatInfo(baseEquip, ui.m_mats, cfg);
					ui.m_eq1.SetEquipInfo(baseEquip);
					ui.m_eq2.SetEquipInfo(baseEquip.UpQuality(q + 1));
				}
			}
		}

		void SetMatInfo(BaseEquip equip, GList list, GameConfigs.EquipQualityRowData cfg)
		{
			if (cfg.IsValid())
			{
				var count = cfg.AdvanceType == 3 ? 1 : cfg.AdvanceValue;
				for (int i = 0; i < count; i++)
				{
					SGame.UIUtils.AddListItem(list, (a, b, item) =>
					{
						if (equip.qcfg.AdvanceType != 3)
						{
							item.SetEquipInfo(equip, true);
							item.SetMerge(equip, true);
						}
						else
						{
							mat.count = cfg.AdvanceValue;
							item.SetEquipInfo(mat, true);
						}
						UIListener.SetControllerSelect(item, "mask", 0, throwex: false);
						item.asCom.GetChild("n100").visible = false;
					}, null);
				}

			}
		}

		partial void UnInitLogic(UIContext context)
		{

		}
	}
}
