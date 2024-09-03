
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Explore;
	using GameConfigs;

	public partial class UIFightEquipTips
	{
		private FightEquip current;
		private FightEquip equip;
		private double ratio;

		private float _best;
		private float _worse;


		partial void InitLogic(UIContext context)
		{
			_best = GlobalDesginConfig.GetFloat("battle_equip_better");
			_worse = GlobalDesginConfig.GetFloat("battle_equip_worse");

			DoOpen(context);

		}

		partial void DoOpen(UIContext context)
		{
			equip = context.GetParam().Value.To<object[]>().Val<FightEquip>(0);
			var type = equip.type;
			current = DataCenter.Instance.exploreData.explorer.GetEquip(type - 10);
			m_view.m_type.selectedIndex = current != null ? 1 : 0;

			m_view.m_info.SetFightEquipInfo(equip, current , SetAttrInfo);
			if (current != null)
			{
				ratio = equip.power / current.power;
				m_view.m_old.SetFightEquipInfo(current, null, SetAttrInfo);
			}

		}

		void SetAttrInfo(GObject gObject , object data)
		{
			if(gObject == null) return;
			UIListener.SetControllerSelect(gObject, "size", 1);
			UIListener.SetControllerSelect(gObject, "ftype", 1);
		}

		partial void OnPuton0Click(EventContext data)
		{
			DropEquip(equip, current);
		}


		partial void OnPutonClick(EventContext data)
		{
			DropEquip(equip, current);
		}

		partial void OnDropClick(EventContext data)
		{
			DropEquip(current, equip);
		}

		void DropEquip(FightEquip equip, FightEquip drop)
		{
			if (equip == null && drop == null) return;

			var showtips = drop != null;

			if (showtips && equip != null)
			{
				if (equip.isnew == 1)
					showtips = ratio < _worse;
				else
					showtips = ratio > _best;
			}

			if (!showtips)
			{
				RequestExcuteSystem.ExplorePutOnEquip(equip, drop);
				SGame.UIUtils.CloseUIByID(__id);
			}
			else
			{
				SGame.UIUtils.Confirm("@ui_notice_title", drop.isnew == 1 ? "@ui_notice_tips1" : "@ui_notice_tips2", (index) =>
				{
					if (index == -1)
					{
						RequestExcuteSystem.ExplorePutOnEquip(equip, drop);
						SGame.UIUtils.CloseUIByID(__id);
					}
				}, new string[] { "@ui_common_return" });
			}
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
