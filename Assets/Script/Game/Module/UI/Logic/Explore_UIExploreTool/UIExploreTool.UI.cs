
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Explore;
	
	public partial class UIExploreTool
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			m_view.m_type.onChanged.Add(new EventCallback1(_OnTypeChanged));
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick));
			UIListener.Listener(m_view.m_click, new EventCallback1(_OnClickClick));
			UIListener.Listener(m_view.m_diamondbtn, new EventCallback1(_OnDiamondbtnClick));
			UIListener.Listener(m_view.m_adbtn, new EventCallback1(_OnAdbtnClick));

		}
		partial void UnInitUI(UIContext context){
			m_view.m_type.onChanged.Remove(new EventCallback1(_OnTypeChanged));
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick),remove:true);
			UIListener.Listener(m_view.m_click, new EventCallback1(_OnClickClick),remove:true);
			UIListener.Listener(m_view.m_diamondbtn, new EventCallback1(_OnDiamondbtnClick),remove:true);
			UIListener.Listener(m_view.m_adbtn, new EventCallback1(_OnAdbtnClick),remove:true);

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
		void SetLevelText(string data)=>UIListener.SetText(m_view.m_level,data);
		string GetLevelText()=>UIListener.GetText(m_view.m_level);
		void SetDescText(string data)=>UIListener.SetText(m_view.m_desc,data);
		string GetDescText()=>UIListener.GetText(m_view.m_desc);
		void SetConditionText(string data)=>UIListener.SetText(m_view.m_condition,data);
		string GetConditionText()=>UIListener.GetText(m_view.m_condition);
		void _OnClickClick(EventContext data){
			OnClickClick(data);
		}
		partial void OnClickClick(EventContext data);
		void SetClickText(string data)=>UIListener.SetText(m_view.m_click,data);
		string GetClickText()=>UIListener.GetText(m_view.m_click);
		void _OnDiamondbtnClick(EventContext data){
			OnDiamondbtnClick(data);
		}
		partial void OnDiamondbtnClick(EventContext data);
		void SetDiamondbtnText(string data)=>UIListener.SetText(m_view.m_diamondbtn,data);
		string GetDiamondbtnText()=>UIListener.GetText(m_view.m_diamondbtn);
		void _OnAdbtnClick(EventContext data){
			OnAdbtnClick(data);
		}
		partial void OnAdbtnClick(EventContext data);
		void SetAdbtnText(string data)=>UIListener.SetText(m_view.m_adbtn,data);
		string GetAdbtnText()=>UIListener.GetText(m_view.m_adbtn);
		void SetTimeText(string data)=>UIListener.SetText(m_view.m_time,data);
		string GetTimeText()=>UIListener.GetText(m_view.m_time);

	}
}
