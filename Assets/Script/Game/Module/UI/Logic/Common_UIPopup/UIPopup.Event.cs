
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Common;
	
	public partial class UIPopup
	{

		partial void InitEvent(UIContext context){
			var closeBtn = m_view.m_close;
			closeBtn.onClick.Add(OncloseBtnClick);
			context.window.AddEventListener("OnMaskClick", OnMaskClick);
		}
	
		void OncloseBtnClick(EventContext context)
		{
			SGame.UIUtils.CloseUIByID(__id);
		}
		
		void OnMaskClick()
		{
			SGame.UIUtils.CloseUIByID(__id);
		}
	}
}
