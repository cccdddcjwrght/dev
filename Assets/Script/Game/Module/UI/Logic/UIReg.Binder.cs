﻿
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
			context.uiModule.Reg("RedpointUI", "Common", ()=>new UIRedpoint());
			context.uiModule.Reg("HudUI", "Hud", ()=>new UIHud());SGame.UI.Hud.HudBinder.BindAll();;
			context.uiModule.Reg("OfflineUI", "Offline", ()=>new UIOffline());SGame.UI.Offline.OfflineBinder.BindAll();;
			context.uiModule.Reg("GuideUI", "Guide", ()=>new UIGuide());SGame.UI.Guide.GuideBinder.BindAll();;
			context.uiModule.Reg("GuideFingerUI", "Guide", ()=>new UIGuideFinger());
            context.uiModule.Reg("PlayerUI", "Player", ()=>new UIPlayer());SGame.UI.Player.PlayerBinder.BindAll();;
			context.uiModule.Reg("GameTipUI", "Hud", ()=>new UIGameTip());
			context.uiModule.Reg("SystemTipUI", "Hud", ()=>new UISystemTip());
            context.uiModule.Reg("DecorUI", "SceneDecor", ()=>new UIDecor());SGame.UI.SceneDecor.SceneDecorBinder.BindAll();;
			context.uiModule.Reg("GuideMaskUI", "Guide", ()=>new UIGuideMask());
			context.uiModule.Reg("EquipGiftUI", "Player", ()=>new UIEquipGift());
			context.uiModule.Reg("EquipTipsUI", "Player", ()=>new UIEquipTips());
			context.uiModule.Reg("EquipResetUI", "Player", () => new UIEquipReset());
			context.uiModule.Reg("UpQualityTipUI", "Player", () => new UIUpQualityTip());
			context.uiModule.Reg("EnterSceneUI", "EnterScene", ()=>new UIEnterScene());SGame.UI.EnterScene.EnterSceneBinder.BindAll();;
			context.uiModule.Reg("PropertyInfoUI", "Player", ()=>new UIPropertyInfo());
			context.uiModule.Reg("TomorrowGiftUI", "TomorrowGift", ()=>new UITomorrowGift());SGame.UI.TomorrowGift.TomorrowGiftBinder.BindAll();;
			context.uiModule.Reg("RoomExclusiveUI", "RoomExclusive", () => new UIRoomExclusive()); SGame.UI.RoomExclusive.RoomExclusiveBinder.BindAll();
			context.uiModule.Reg("WelcomeNewLevelUI", "EnterScene", ()=>new UIWelcomeNewLevel());
			context.uiModule.Reg("LevelCompletedUI", "EnterScene", ()=>new UILevelCompleted());
			context.uiModule.Reg("GoodReputationUI", "Reputation", () => new UIGoodReputation()); SGame.UI.Reputation.ReputationBinder.BindAll();
			context.uiModule.Reg("TotalBoostUI", "Reputation", () => new UITotalBoost());
			context.uiModule.Reg("FriendTipUI", "Hud", ()=>new UIFriendTip());
			context.uiModule.Reg("FriendDetailUI", "GameFriend", ()=>new UIFriendDetail());
			context.uiModule.Reg("FriendUI", "GameFriend", ()=>new UIFriend());SGame.UI.GameFriend.GameFriendBinder.BindAll();;
			context.uiModule.Reg("NewbieGiftUI", "TomorrowGift", () => new UINewbieGift());
			context.uiModule.Reg("SuitUI", "Player", () => new UISuit());
			context.uiModule.Reg("GMUI", "GM", () => new UIGM()); SGame.UI.GM.GMBinder.BindAll(); ;
			context.uiModule.Reg("PiggyBankUI", "PiggyBank", ()=>new UIPiggyBank());SGame.UI.PiggyBank.PiggyBankBinder.BindAll();;
			context.uiModule.Reg("UseHelpUI", "PiggyBank", ()=>new UIUseHelp());
			context.uiModule.Reg("ConfirmUI", "Common", ()=>new UIConfirm());
			context.uiModule.Reg("GrowGiftUI", "GrowGift", ()=>new UIGrowGift());SGame.UI.GrowGift.GrowGiftBinder.BindAll();;
			context.uiModule.Reg("PetUI", "Pet", ()=>new UIPet());SGame.UI.Pet.PetBinder.BindAll();;
			context.uiModule.Reg("PetBornUI", "Pet", ()=>new UIPetBorn());
			context.uiModule.Reg("PetTipsUI", "Pet", ()=>new UIPetTips());
			context.uiModule.Reg("RandomSelectUI", "Pet", ()=>new UIRandomSelect());
			context.uiModule.Reg("FlightUI", "Flight", ()=>new UIFlight());SGame.UI.Flight.FlightBinder.BindAll();;
			context.uiModule.Reg("RankMainUI", "Rank", ()=>new UIRankMain());SGame.UI.Rank.RankBinder.BindAll();;
			context.uiModule.Reg("RankResultUI", "Rank", ()=>new UIRankResult());
			context.uiModule.Reg("RankDetailUI", "Rank", ()=>new UIRankDetail());
			context.uiModule.Reg("ExchangeTaskUI", "Exchange", ()=>new UIExchangeTask());SGame.UI.Exchange.ExchangeBinder.BindAll();;
			context.uiModule.Reg("MonsterHunterUI", "MonsterHunter", ()=>new UIMonsterHunter());SGame.UI.MonsterHunter.MonsterHunterBinder.BindAll();;
			context.uiModule.Reg("TempShopUI", "MonsterHunter", ()=>new UITempShop());
			context.uiModule.Reg("LockRedUI", "Common", ()=>new UILockRed());
			context.uiModule.Reg("ClubMemberUI", "Club", ()=>new UIClubMember());SGame.UI.Club.ClubBinder.BindAll();;
			context.uiModule.Reg("ClubCreateUI", "Club", ()=>new UIClubCreate());
			context.uiModule.Reg("ClubFindUI", "Club", ()=>new UIClubFind());
			context.uiModule.Reg("ClubMainUI", "Club", ()=>new UIClubMain());
			context.uiModule.Reg("ClubSelectUI", "Club", ()=>new UIClubSelect());
			context.uiModule.Reg("ClubTaskUI", "Club", ()=>new UIClubTask());
			context.uiModule.Reg("ClubRewardUI", "Club", ()=>new UIClubReward());
			context.uiModule.Reg("ClubDetailUI", "Club", ()=>new UIClubDetail());
			context.uiModule.Reg("LockPanelUI", "Worktable", ()=>new UILockPanel());
			context.uiModule.Reg("TaskUI", "Task", ()=>new UITask());SGame.UI.Task.TaskBinder.BindAll();;
			context.uiModule.Reg("CookbookUI", "Cookbook", ()=>new UICookbook());SGame.UI.Cookbook.CookbookBinder.BindAll();;
			context.uiModule.Reg("CookbookUpUI", "Cookbook", ()=>new UICookbookUp());
			context.uiModule.Reg("WelcomeNewLevelAnimUI", "EnterScene", ()=>new UIWelcomeNewLevelAnim());
			context.uiModule.Reg("GuideBackUI", "Guide", ()=>new UIGuideBack());
			context.uiModule.Reg("FingerUI", "Guide", ()=>new UIFinger());
			context.uiModule.Reg("RewardShowUI", "Common", ()=>new UIRewardShow());
			context.uiModule.Reg("UnlockTableUI", "Worktable", ()=>new UIUnlockTable());
			context.uiModule.Reg("RecruitmentUI", "Worktable", ()=>new UIRecruitment());
			context.uiModule.Reg("EnterSceneTempUI", "EnterScene", ()=>new UIEnterSceneTemp());
			context.uiModule.Reg("CommonRewardUI", "Common", ()=>new UICommonReward());
			context.uiModule.Reg("GuideDialogueUI", "Guide", ()=>new UIGuideDialogue());
			context.uiModule.Reg("GetWorkerTipsUI", "Hud", ()=>new UIGetWorkerTips());
			context.uiModule.Reg("GuidePiercingUI", "Guide", ()=>new UIGuidePiercing());
			context.uiModule.Reg("LuckLikeUI", "Reputation", () => new UILuckLike());
			context.uiModule.Reg("FragmentUI", "Reputation", ()=>new UIFragment());
			context.uiModule.Reg("CustomerbookUpUI", "Cookbook", ()=>new UICustomerbookUp());
			context.uiModule.Reg("CustomerbookFirstUpUI", "Cookbook", ()=>new UICustomerbookFirstUp());
			context.uiModule.Reg("HotFoodUI", "HotFood", ()=>new UIHotFood());SGame.UI.HotFood.HotFoodBinder.BindAll();;
		}
	}
}
