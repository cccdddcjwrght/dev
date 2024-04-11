
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.PiggyBank;
	
	public partial class UIUseHelp
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			UIListener.ListenerClose(m_view.m_mask, new EventCallback1(DoCloseUIClick));

		}
		partial void UnInitUI(UIContext context){
			UIListener.ListenerClose(m_view.m_mask, new EventCallback1(DoCloseUIClick),remove:true);

		}
		void DoCloseUIClick(EventContext data){
			 bool __closestate = true;
			 OnUICloseClick(ref __closestate);
			 if(__closestate)SGame.UIUtils.CloseUIByID(__id);
			 
		}
		partial void OnUICloseClick(ref bool state);
		void SetTitleText(string data)=>UIListener.SetText(m_view.m_title,data);
		string GetTitleText()=>UIListener.GetText(m_view.m_title);
		void SetInfo1Text(string data)=>UIListener.SetText(m_view.m_info1,data);
		string GetInfo1Text()=>UIListener.GetText(m_view.m_info1);
		void SetInfo2Text(string data)=>UIListener.SetText(m_view.m_info2,data);
		string GetInfo2Text()=>UIListener.GetText(m_view.m_info2);
		void SetInfo3Text(string data)=>UIListener.SetText(m_view.m_info3,data);
		string GetInfo3Text()=>UIListener.GetText(m_view.m_info3);
		void SetInfo4Text(string data)=>UIListener.SetText(m_view.m_info4,data);
		string GetInfo4Text()=>UIListener.GetText(m_view.m_info4);

	}
}
