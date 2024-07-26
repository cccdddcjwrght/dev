
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Player;
	using System.Collections.Generic;
	using GameConfigs;
	using System.Linq;

	public partial class UIPropertyInfo
	{
		private SGame.RoleData roleData;

		private int _mainVal;
		private List<int[]> _effects;

		partial void InitLogic(UIContext context)
		{
			m_view.z = -500;
			roleData = (context.GetParam()?.Value as object[]).Val<SGame.RoleData>(0);

			_mainVal = DataCenter.EquipUtil.GetRoleEquipAddValue(roleData?.equips);
			_effects = DataCenter.EquipUtil.GetEquipEffects(roleData?.equips,pack:true);

			m_view.SetText($"+{_mainVal}%");
			m_view.m_list.RemoveChildrenToPool();
			m_view.m_list.itemRenderer = OnSetInfo;
			m_view.m_list.numItems = _effects == null ? 0 : _effects.Count;
		}

		void OnSetInfo(int index, GObject gObject)
		{
			var cfg = _effects[index];
			if (ConfigSystem.Instance.TryGet<BuffRowData>(cfg[0], out var buff))
			{
				UIListener.SetControllerSelect(gObject, "type", 2);
				gObject.SetTextByKey(buff.Describe, cfg[1]);
			}
		}

		partial void UnInitLogic(UIContext context)
		{

		}
	}
}
