﻿
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI;
	
	public partial class UIReg
	{
		partial void Register(UIContext context){
			context.uiModule.Reg("MainUI", "Main", ()=>new UIMain());SGame.UI.Main.MainBinder.BindAll();;
			context.uiModule.Reg("LoadingUI", "Loading", ()=>new UILoading());SGame.UI.Loading.LoadingBinder.BindAll();;
		}
	}
}
