
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Exchange;
	
	public partial class UIExchangeTask
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			m_view.m_type.onChanged.Add(new EventCallback1(_OnTypeChanged));
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick));
			UIListener.Listener(m_view.m_tab1, new EventCallback1(_OnTab1Click));
			UIListener.Listener(m_view.m_tab2, new EventCallback1(_OnTab2Click));
			UIListener.Listener(m_view.m_reddot1, new EventCallback1(_OnReddot1Click));
			UIListener.Listener(m_view.m_reddot2, new EventCallback1(_OnReddot2Click));

		}
		partial void UnInitUI(UIContext context){
			m_view.m_type.onChanged.Remove(new EventCallback1(_OnTypeChanged));
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick),remove:true);
			UIListener.Listener(m_view.m_tab1, new EventCallback1(_OnTab1Click),remove:true);
			UIListener.Listener(m_view.m_tab2, new EventCallback1(_OnTab2Click),remove:true);
			UIListener.Listener(m_view.m_reddot1, new EventCallback1(_OnReddot1Click),remove:true);
			UIListener.Listener(m_view.m_reddot2, new EventCallback1(_OnReddot2Click),remove:true);

		}
		void _OnTypeChanged(EventContext data){
			OnTypeChanged(data);
		}
		partial void OnTypeChanged(EventContext data);
		void SwitchTypePage(int index)=>m_view.m_type.selectedIndex=index;
		void DoCloseUIClick(EventContext data){
			 bool __closestate = true;
			 OnUICloseClick(ref __closestate);
			 if(__closestate)SGame.UIUtils.CloseUIByID(__id);
			 
		}
		partial void OnUICloseClick(ref bool state);
		void SetBodyText(string data)=>UIListener.SetText(m_view.m_body,data);
		string GetBodyText()=>UIListener.GetText(m_view.m_body);
		void SetValueText(string data)=>UIListener.SetText(m_view.m_value,data);
		string GetValueText()=>UIListener.GetText(m_view.m_value);
		void _OnTab1Click(EventContext data){
			OnTab1Click(data);
		}
		partial void OnTab1Click(EventContext data);
		void SetTab1Text(string data)=>UIListener.SetText(m_view.m_tab1,data);
		string GetTab1Text()=>UIListener.GetText(m_view.m_tab1);
		void _OnTab2Click(EventContext data){
			OnTab2Click(data);
		}
		partial void OnTab2Click(EventContext data);
		void SetTab2Text(string data)=>UIListener.SetText(m_view.m_tab2,data);
		string GetTab2Text()=>UIListener.GetText(m_view.m_tab2);
		void _OnReddot1Click(EventContext data){
			OnReddot1Click(data);
		}
		partial void OnReddot1Click(EventContext data);
		void SetReddot1Text(string data)=>UIListener.SetText(m_view.m_reddot1,data);
		string GetReddot1Text()=>UIListener.GetText(m_view.m_reddot1);
		void _OnReddot2Click(EventContext data){
			OnReddot2Click(data);
		}
		partial void OnReddot2Click(EventContext data);
		void SetReddot2Text(string data)=>UIListener.SetText(m_view.m_reddot2,data);
		string GetReddot2Text()=>UIListener.GetText(m_view.m_reddot2);
		void SetTimeText(string data)=>UIListener.SetText(m_view.m_time,data);
		string GetTimeText()=>UIListener.GetText(m_view.m_time);

	}
}
