
using SGame.UI.Common;

namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Setting;
	
	public partial class UIChangeName
	{
		partial void InitEvent(UIContext context)
		{
			if (!string.IsNullOrEmpty(DataCenter.Instance.accountData.playerName))
			{
				m_view.m_input.promptText = DataCenter.Instance.accountData.playerName;
			}
			else
			{
				m_view.m_input.promptText = UIListener.Local("player_name");
			}
		}
		partial void UnInitEvent(UIContext context){

		}

		partial void OnBtnOKClick(EventContext data)
		{
			string input = m_view.m_input.text;
			SGame.UIUtils.CloseUIByID(__id);
			EventManager.Instance.Trigger(((int)GameEvent.SETTING_UPDATE_NAME), input);
		}
	}
}
