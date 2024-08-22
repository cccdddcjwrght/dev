
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Explore;

	public partial class UIFightEquipTips
	{
		private FightEquip equip;

		partial void InitLogic(UIContext context)
		{

			DoOpen(context);

		}

		partial void DoOpen(UIContext context)
		{
			equip = context.GetParam().Value.To<object[]>().Val<FightEquip>(0);
			var type = equip.type;
			var current = DataCenter.Instance.exploreData.explorer.GetEquip(type - 10);
			m_view.m_type.selectedIndex = current != null ? 1 : 0;

			m_view.m_info.SetFightEquipInfo(equip, current);
			if (current != null) m_view.m_old.SetFightEquipInfo(current, equip);

		}

		partial void OnDropClick(EventContext data)
		{
			SGame.UIUtils.Confirm("ui_notice_title", "ui_notice_tips1", (index) =>
			{
				if (index == -1)
				{

				}
			}, new string[] { "ui_common_return" });
		}

		partial void DoHide(UIContext context)
		{
			equip = null;
		}

		partial void UnInitLogic(UIContext context)
		{

		}
	}
}
