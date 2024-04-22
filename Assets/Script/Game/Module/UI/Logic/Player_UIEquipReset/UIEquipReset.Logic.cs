
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Player;
	using System.Collections.Generic;

	public partial class UIEquipReset
	{
		private EquipItem equip;
		private ItemData.Value mat;

		private List<BaseEquip> items;

		private bool needRefreshPlayUI;
		partial void InitLogic(UIContext context)
		{
			var args = context.GetParam()?.Value.To<object[]>();
			equip = args.Val<EquipItem>(0);
			m_view.m_func.selectedIndex = args.Val<int>(1);
			m_view.m_equip.SetEquipInfo(equip, true);
			m_view.SetTextByKey(m_view.m_func.selectedIndex == 0 ? "ui_equip_remake_title" : "ui_equip_decompose_title");
			mat = new ItemData.Value() { id = ConstDefine.EQUIP_UPLV_MAT, type = PropertyGroup.ITEM };
			OnFuncChanged(null);
		}

		private void SetResetInfo()
		{
			m_view.SetTextByKey( "ui_equip_remake_title" );
			var next = equip.Clone().Refresh();
			var count = DataCenter.EquipUtil.RecycleEquip(equip, false, false);
			next.level = 1;
			items = new List<BaseEquip>() {
				next,
				new EquipItem().Convert(ConstDefine.EQUIP_UPLV_MAT , count , 1)
			};

		}

		private void SetDecomposeInfo()
		{
			m_view.SetTextByKey("ui_equip_decompose_title");

			var reward = equip.qcfg.GetBreakRewardArray();
			var count = DataCenter.EquipUtil.RecycleEquip(equip, false, false);
			items = new List<BaseEquip>() { new EquipItem().Convert(reward[1], reward[2], reward[0]) };
			if (count > 0)
				items.Add(new EquipItem().Convert(ConstDefine.EQUIP_UPLV_MAT, count, 1));
		}

		private void SetItemInfo(int index, object data, GObject gObject)
		{
			gObject.SetEquipInfo(data as BaseEquip);
		}

		partial void OnFuncChanged(EventContext data)
		{
			m_view.m_list.RemoveChildrenToPool();
			switch (m_view.m_func.selectedIndex)
			{
				case 0: SetResetInfo(); break;
				case 1: SetDecomposeInfo(); break;
			}
			SGame.UIUtils.AddListItems(m_view.m_list, items, SetItemInfo);

		}

		partial void OnClickClick(EventContext data)
		{
			needRefreshPlayUI = true;
			RequestExcuteSystem.EquipRemake(equip);
			DoCloseUIClick(null);
		}

		partial void OnClick2Click(EventContext data)
		{
			needRefreshPlayUI = true;
			RequestExcuteSystem.EquipDecompose(equip);
			DoCloseUIClick(null);
		}

		partial void UnInitLogic(UIContext context)
		{
			if (needRefreshPlayUI)
				EventManager.Instance.Trigger(((int)GameEvent.ROLE_PROPERTY_REFRESH));
		}
	}
}
