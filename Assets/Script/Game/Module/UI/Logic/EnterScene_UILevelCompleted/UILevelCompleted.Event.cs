﻿
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.EnterScene;
	
	public partial class UILevelCompleted
	{
		partial void InitEvent(UIContext context){

			m_view.AddEventListener("OnMaskClick", _OnLevelCompletedBody_ClickClick);

		}
		partial void UnInitEvent(UIContext context){

		}
	}
}
