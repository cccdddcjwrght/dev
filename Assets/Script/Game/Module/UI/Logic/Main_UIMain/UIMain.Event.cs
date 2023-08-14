
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Main;
	
	public partial class UIMain
	{
		partial void InitEvent(UIContext context){

			var clickBtn = context.content.GetChildByPath("battle.icon").asButton;
			clickBtn.onClick.Add(OnBattleIconClick);
		}
		
		partial void UnInitEvent(UIContext context){

		}

		partial void OnBattleBtn_PowerClick(EventContext datat)
		{
			UserSetting setting = DataCenter.Instance.GetUserSetting();
			setting.doubleBonus = (setting.doubleBonus) % 5 + 1;
			DataCenter.Instance.SetUserSetting(setting);
		}

		void OnBattleIconClick(EventContext context)
		{
			if (context.inputEvent.isDoubleClick)
			{
				var v = DataCenter.Instance.GetUserSetting();
				v.autoUse = !v.autoUse;
				DataCenter.Instance.SetUserSetting(v);
			}
			else
			{
				EventManager.Instance.Trigger((int)GameEvent.PLAYER_ROTE_DICE);
			}
		}
	}
}
