
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.PiggyBank;
	
	public partial class UIUseHelp
	{
		partial void InitLogic(UIContext context){
			m_view.m_mask.onClick.Add(DoCloseUIClick);
			m_view.m_title.SetText(UIListener.Local("ui_piggybank_help_title"));
			m_view.m_info1.SetText(UIListener.Local("ui_piggybank_help_tips1"));
			m_view.m_info2.SetText(UIListener.Local("ui_piggybank_help_tips2"));
			m_view.m_info3.SetText(UIListener.Local("ui_piggybank_help_tips3"));
			m_view.m_info4.SetText(UIListener.Local("ui_piggybank_help_tips4"));
		}
		partial void UnInitLogic(UIContext context){

		}
	}
}
