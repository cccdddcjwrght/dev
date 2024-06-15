
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Guide;
	
	public partial class UIGuideDialogue
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			UIListener.ListenerClose(m_view.m_mask, new EventCallback1(DoCloseUIClick));

		}
		partial void UnInitUI(UIContext context){
			UIListener.ListenerClose(m_view.m_mask, new EventCallback1(DoCloseUIClick),remove:true);

		}
		void SetDialogueText(string data)=>UIListener.SetText(m_view.m_dialogue,data);
		string GetDialogueText()=>UIListener.GetText(m_view.m_dialogue);
		void DoCloseUIClick(EventContext data){
			 bool __closestate = true;
			 OnUICloseClick(ref __closestate);
			 if(__closestate)SGame.UIUtils.CloseUIByID(__id);
			 
		}
		partial void OnUICloseClick(ref bool state);

	}
}
