
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Player;
	
	public partial class UIPlayer
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			m_view.m_eqTab.onChanged.Add(new EventCallback1(_OnEqTabChanged));
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick));
			UIListener.Listener(m_view.m_attrbtn, new EventCallback1(_OnAttrbtnClick));
			UIListener.Listener(m_view.m_eq1, new EventCallback1(_OnEq1Click));
			UIListener.Listener(m_view.m_eq3, new EventCallback1(_OnEq3Click));
			UIListener.Listener(m_view.m_eq2, new EventCallback1(_OnEq2Click));
			UIListener.Listener(m_view.m_eq5, new EventCallback1(_OnEq5Click));
			UIListener.Listener(m_view.m_eq4, new EventCallback1(_OnEq4Click));

		}
		partial void UnInitUI(UIContext context){
			m_view.m_eqTab.onChanged.Remove(new EventCallback1(_OnEqTabChanged));
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick),remove:true);
			UIListener.Listener(m_view.m_attrbtn, new EventCallback1(_OnAttrbtnClick),remove:true);
			UIListener.Listener(m_view.m_eq1, new EventCallback1(_OnEq1Click),remove:true);
			UIListener.Listener(m_view.m_eq3, new EventCallback1(_OnEq3Click),remove:true);
			UIListener.Listener(m_view.m_eq2, new EventCallback1(_OnEq2Click),remove:true);
			UIListener.Listener(m_view.m_eq5, new EventCallback1(_OnEq5Click),remove:true);
			UIListener.Listener(m_view.m_eq4, new EventCallback1(_OnEq4Click),remove:true);

		}
		void _OnEqTabChanged(EventContext data){
			OnEqTabChanged(data);
		}
		partial void OnEqTabChanged(EventContext data);
		void SwitchEqTabPage(int index)=>m_view.m_eqTab.selectedIndex=index;
		void DoCloseUIClick(EventContext data){
			 bool __closestate = true;
			 OnUICloseClick(ref __closestate);
			 if(__closestate)SGame.UIUtils.CloseUIByID(__id);
			 
		}
		partial void OnUICloseClick(ref bool state);
		void SetBodyText(string data)=>UIListener.SetText(m_view.m_body,data);
		string GetBodyText()=>UIListener.GetText(m_view.m_body);
		void _OnAttrbtnClick(EventContext data){
			OnAttrbtnClick(data);
		}
		partial void OnAttrbtnClick(EventContext data);
		void SetAttrbtnText(string data)=>UIListener.SetText(m_view.m_attrbtn,data);
		string GetAttrbtnText()=>UIListener.GetText(m_view.m_attrbtn);
		void SetAttrText(string data)=>UIListener.SetText(m_view.m_attr,data);
		string GetAttrText()=>UIListener.GetText(m_view.m_attr);
		void _OnEq1Click(EventContext data){
			OnEq1Click(data);
		}
		partial void OnEq1Click(EventContext data);
		void SetEq1Text(string data)=>UIListener.SetText(m_view.m_eq1,data);
		string GetEq1Text()=>UIListener.GetText(m_view.m_eq1);
		void _OnEq3Click(EventContext data){
			OnEq3Click(data);
		}
		partial void OnEq3Click(EventContext data);
		void SetEq3Text(string data)=>UIListener.SetText(m_view.m_eq3,data);
		string GetEq3Text()=>UIListener.GetText(m_view.m_eq3);
		void _OnEq2Click(EventContext data){
			OnEq2Click(data);
		}
		partial void OnEq2Click(EventContext data);
		void SetEq2Text(string data)=>UIListener.SetText(m_view.m_eq2,data);
		string GetEq2Text()=>UIListener.GetText(m_view.m_eq2);
		void _OnEq5Click(EventContext data){
			OnEq5Click(data);
		}
		partial void OnEq5Click(EventContext data);
		void SetEq5Text(string data)=>UIListener.SetText(m_view.m_eq5,data);
		string GetEq5Text()=>UIListener.GetText(m_view.m_eq5);
		void _OnEq4Click(EventContext data){
			OnEq4Click(data);
		}
		partial void OnEq4Click(EventContext data);
		void SetEq4Text(string data)=>UIListener.SetText(m_view.m_eq4,data);
		string GetEq4Text()=>UIListener.GetText(m_view.m_eq4);

	}
}
