
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Player;
	
	public partial class UISuit
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			m_view.m_funcType.onChanged.Add(new EventCallback1(_OnFuncTypeChanged));
			m_view.m_part.onChanged.Add(new EventCallback1(_OnPartChanged));
			m_view.m_quality.onChanged.Add(new EventCallback1(_OnQualityChanged));
			m_view.m_flag.onChanged.Add(new EventCallback1(_OnFlagChanged));
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick));
			UIListener.Listener(m_view.m_click, new EventCallback1(_OnClickClick));

		}
		partial void UnInitUI(UIContext context){
			m_view.m_funcType.onChanged.Remove(new EventCallback1(_OnFuncTypeChanged));
			m_view.m_part.onChanged.Remove(new EventCallback1(_OnPartChanged));
			m_view.m_quality.onChanged.Remove(new EventCallback1(_OnQualityChanged));
			m_view.m_flag.onChanged.Remove(new EventCallback1(_OnFlagChanged));
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick),remove:true);
			UIListener.Listener(m_view.m_click, new EventCallback1(_OnClickClick),remove:true);

		}
		void _OnFuncTypeChanged(EventContext data){
			OnFuncTypeChanged(data);
		}
		partial void OnFuncTypeChanged(EventContext data);
		void SwitchFuncTypePage(int index)=>m_view.m_funcType.selectedIndex=index;
		void _OnPartChanged(EventContext data){
			OnPartChanged(data);
		}
		partial void OnPartChanged(EventContext data);
		void SwitchPartPage(int index)=>m_view.m_part.selectedIndex=index;
		void _OnQualityChanged(EventContext data){
			OnQualityChanged(data);
		}
		partial void OnQualityChanged(EventContext data);
		void SwitchQualityPage(int index)=>m_view.m_quality.selectedIndex=index;
		void _OnFlagChanged(EventContext data){
			OnFlagChanged(data);
		}
		partial void OnFlagChanged(EventContext data);
		void SwitchFlagPage(int index)=>m_view.m_flag.selectedIndex=index;
		void DoCloseUIClick(EventContext data){
			 bool __closestate = true;
			 OnUICloseClick(ref __closestate);
			 if(__closestate)SGame.UIUtils.CloseUIByID(__id);
			 
		}
		partial void OnUICloseClick(ref bool state);
		void SetBodyText(string data)=>UIListener.SetText(m_view.m_body,data);
		string GetBodyText()=>UIListener.GetText(m_view.m_body);
		void _OnClickClick(EventContext data){
			OnClickClick(data);
		}
		partial void OnClickClick(EventContext data);
		void SetClickText(string data)=>UIListener.SetText(m_view.m_click,data);
		string GetClickText()=>UIListener.GetText(m_view.m_click);
		void Set__titleText(string data)=>UIListener.SetText(m_view.m___title,data);
		string Get__titleText()=>UIListener.GetText(m_view.m___title);
		void Set__textText(string data)=>UIListener.SetText(m_view.m___text,data);
		string Get__textText()=>UIListener.GetText(m_view.m___text);

	}
}
