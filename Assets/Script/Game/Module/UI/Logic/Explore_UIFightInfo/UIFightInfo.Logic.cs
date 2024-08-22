
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Explore;

	public partial class UIFightInfo
	{
		private ExploreRoleData roleData;


		partial void DoOpen(UIContext context)
		{
			roleData = context.GetParam()?.Value.To<object[]>().Val<ExploreRoleData>(0);
			SetAttrList();
		}

		void SetAttrList()
		{
			m_view.m_attr.SetFightAttrList(DataCenter.ExploreUtil.GetAttrList(true, roleData.equips));
		}

		partial void UnInitLogic(UIContext context)
		{

		}
	}
}
