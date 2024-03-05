
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Common;
	
	public partial class UIRedpoint
	{
		partial void InitLogic(UIContext context){
			m_view.SetPivot(0.5f, 1f, true);
		}
		partial void UnInitLogic(UIContext context){

		}
	}
}
