
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

		}

		void OncloseBtnClick(EventContext context)
		{
			m_view.visible = false;
		}

		partial void UnInitEvent(UIContext context){

		}
	}
}
