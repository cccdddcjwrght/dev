
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Common;
	
	public partial class UIPopup
	{
		partial void InitLogic(UIContext context){
			int arg = (int)context.gameWorld.GetEntityManager().GetComponentObject<UIParam>(context.entity).Value;
			m_view.m_size.selectedIndex = arg;
		}
		partial void UnInitLogic(UIContext context){

		}
	}
}
