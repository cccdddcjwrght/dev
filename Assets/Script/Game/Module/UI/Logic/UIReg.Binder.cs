
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
			context.uiModule.Reg("HudUI", "Hud", ()=>new UIHud());SGame.UI.Hud.HudBinder.BindAll();;
			context.uiModule.Reg("TravelLeaveUI", "Travel", ()=>new UITravelLeave());SGame.UI.Travel.TravelBinder.BindAll();;
			context.uiModule.Reg("TravelEnterUI", "Travel", ()=>new UITravelEnter());
		}
	}
}
