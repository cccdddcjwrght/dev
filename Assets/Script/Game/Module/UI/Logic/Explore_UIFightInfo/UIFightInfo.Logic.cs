
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Explore;
	using System.Collections.Generic;

	public partial class UIFightInfo
	{
		private List<int[]> roleData;


		partial void DoOpen(UIContext context)
		{
			roleData = context.GetParam()?.Value.To<object[]>().Val<List<int[]>>(0);
			SetAttrList();
		}

		void SetAttrList()
		{
			m_view.m_attr.SetFightAttrList(roleData, addnull: 1);
		}

		partial void UnInitLogic(UIContext context)
		{

		}
	}
}
