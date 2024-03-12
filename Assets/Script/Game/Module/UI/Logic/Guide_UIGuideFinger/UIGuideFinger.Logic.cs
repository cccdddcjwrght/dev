
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Guide;
	
	public partial class UIGuideFinger
	{
		partial void InitLogic(UIContext context){

			var pos = context.gameWorld.GetEntityManager().GetComponentObject<UIPos>(context.entity).pos;
			m_view.m_Finger.xy = pos;
		}
		partial void UnInitLogic(UIContext context){

		}
	}
}
