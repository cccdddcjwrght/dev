
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.EnterScene;
	
	public partial class UIWelcomeNewLevel
	{
		partial void InitEvent(UIContext context){

			m_view.AddEventListener("OnMaskClick", _OnClickClick);

		}

		partial void UnInitEvent(UIContext context){

		}



	}
}
