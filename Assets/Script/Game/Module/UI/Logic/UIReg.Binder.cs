
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI;
	
	public partial class UIReg
	{
		partial void Register(UIContext context){
			context.uiModule.Reg("LoadingUI", "Loading", ()=>new UILoading());SGame.UI.Loading.LoadingBinder.BindAll();;
			context.uiModule.Reg("PopupUI", "Common", ()=>new UIPopup());SGame.UI.Common.CommonBinder.BindAll();;
			context.uiModule.Reg("MainUI", "Main", ()=>new UIMain());SGame.UI.Main.MainBinder.BindAll();;
			context.uiModule.Reg("HudUI", "Hud", ()=>new UIHud());SGame.UI.Hud.HudBinder.BindAll();;
			context.uiModule.Reg("TechnologyUI", "Technology", ()=>new UITechnology());SGame.UI.Technology.TechnologyBinder.BindAll();;
			context.uiModule.Reg("LevelTechUI", "Buff", ()=>new UILevelTech());SGame.UI.Buff.BuffBinder.BindAll();;
			context.uiModule.Reg("WorktableUI", "Worktable", ()=>new UIWorktable());SGame.UI.Worktable.WorktableBinder.BindAll();;
		}
	}
}
