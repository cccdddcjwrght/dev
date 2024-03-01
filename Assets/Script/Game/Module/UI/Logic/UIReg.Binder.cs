
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI;
	
	public partial class UIReg
	{
		partial void Register(UIContext context){
			context.uiModule.Reg("LoadingUI", "Loading", ()=>new UILoading());SGame.UI.Loading.LoadingBinder.BindAll();;
			context.uiModule.Reg("TechnologyUI", "Technology", ()=>new UITechnology());SGame.UI.Technology.TechnologyBinder.BindAll();;
			context.uiModule.Reg("LevelTechUI", "Buff", ()=>new UILevelTech());SGame.UI.Buff.BuffBinder.BindAll();;
			context.uiModule.Reg("WorktableUI", "Worktable", ()=>new UIWorktable());SGame.UI.Worktable.WorktableBinder.BindAll();;
			context.uiModule.Reg("WorktablePanelUI", "Worktable", ()=>new UIWorktablePanel());
			context.uiModule.Reg("MainUI", "Main", ()=>new UIMain());SGame.UI.Main.MainBinder.BindAll();;
			context.uiModule.Reg("PopupUI", "Common", ()=>new UIPopup());SGame.UI.Common.CommonBinder.BindAll();;
			context.uiModule.Reg("ShopUI", "Shop", ()=>new UIShop());SGame.UI.Shop.ShopBinder.BindAll();;
			context.uiModule.Reg("ChangeNameUI", "Setting", ()=>new UIChangeName());SGame.UI.Setting.SettingBinder.BindAll();;
			context.uiModule.Reg("SettingUI", "Setting", ()=>new UISetting());
			context.uiModule.Reg("LanguageUI", "Setting", ()=>new UILanguage());
			context.uiModule.Reg("ChangeHeadUI", "Setting", ()=>new UIChangeHead());
			context.uiModule.Reg("MaskUI", "Common", ()=>new UIMask());
			context.uiModule.Reg("FoodTipUI", "Hud", ()=>new UIFoodTip());
			context.uiModule.Reg("HudUI", "Hud", ()=>new UIHud());SGame.UI.Hud.HudBinder.BindAll();;
		}
	}
}
