
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Main;
	
	public partial class UIMain
	{
		partial void InitEvent(UIContext context){

		}
		partial void UnInitEvent(UIContext context){

		}

		partial void OnBattleBtn_PowerClick(EventContext datat)
		{
			UserSetting setting = DataCenter.Instance.GetUserSetting();
			setting.doubleBonus = (setting.doubleBonus) % 5 + 1;
			DataCenter.Instance.SetUserSetting(setting);
		}
	}
}
