
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Explore;
	
	public partial class UIFightLevel
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick));
			UIListener.Listener(m_view.m_battleBtn, new EventCallback1(_OnBattleBtnClick));

		}
		partial void UnInitUI(UIContext context){
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick),remove:true);
			UIListener.Listener(m_view.m_battleBtn, new EventCallback1(_OnBattleBtnClick),remove:true);

		}
		void DoCloseUIClick(EventContext data){
			 bool __closestate = true;
			 OnUICloseClick(ref __closestate);
			 if(__closestate)SGame.UIUtils.CloseUIByID(__id);
			 
		}
		partial void OnUICloseClick(ref bool state);
		void SetBodyText(string data)=>UIListener.SetText(m_view.m_body,data);
		string GetBodyText()=>UIListener.GetText(m_view.m_body);
		void SetNameText(string data)=>UIListener.SetText(m_view.m_name,data);
		string GetNameText()=>UIListener.GetText(m_view.m_name);
		void SetFightText(string data)=>UIListener.SetText(m_view.m_fight,data);
		string GetFightText()=>UIListener.GetText(m_view.m_fight);
		void SetGradeText(string data)=>UIListener.SetText(m_view.m_grade,data);
		string GetGradeText()=>UIListener.GetText(m_view.m_grade);
		void _OnBattleBtnClick(EventContext data){
			OnBattleBtnClick(data);
		}
		partial void OnBattleBtnClick(EventContext data);
		void SetBattleBtnText(string data)=>UIListener.SetText(m_view.m_battleBtn,data);
		string GetBattleBtnText()=>UIListener.GetText(m_view.m_battleBtn);

	}
}
