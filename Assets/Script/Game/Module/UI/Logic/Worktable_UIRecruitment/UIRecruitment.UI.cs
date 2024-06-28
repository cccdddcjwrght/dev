
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Worktable;
	
	public partial class UIRecruitment
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			m_view.m_currency.onChanged.Add(new EventCallback1(_OnCurrencyChanged));
			m_view.m_type.onChanged.Add(new EventCallback1(_OnTypeChanged));
			m_view.m_roletype.onChanged.Add(new EventCallback1(_OnRoletypeChanged));
			UIListener.Listener(m_view.m_click, new EventCallback1(_OnClickClick));
			UIListener.ListenerClose(m_view.m_close, new EventCallback1(DoCloseUIClick));

		}
		partial void UnInitUI(UIContext context){
			m_view.m_currency.onChanged.Remove(new EventCallback1(_OnCurrencyChanged));
			m_view.m_type.onChanged.Remove(new EventCallback1(_OnTypeChanged));
			m_view.m_roletype.onChanged.Remove(new EventCallback1(_OnRoletypeChanged));
			UIListener.Listener(m_view.m_click, new EventCallback1(_OnClickClick),remove:true);
			UIListener.ListenerClose(m_view.m_close, new EventCallback1(DoCloseUIClick),remove:true);

		}
		void _OnCurrencyChanged(EventContext data){
			OnCurrencyChanged(data);
		}
		partial void OnCurrencyChanged(EventContext data);
		void SwitchCurrencyPage(int index)=>m_view.m_currency.selectedIndex=index;
		void _OnTypeChanged(EventContext data){
			OnTypeChanged(data);
		}
		partial void OnTypeChanged(EventContext data);
		void SwitchTypePage(int index)=>m_view.m_type.selectedIndex=index;
		void _OnRoletypeChanged(EventContext data){
			OnRoletypeChanged(data);
		}
		partial void OnRoletypeChanged(EventContext data);
		void SwitchRoletypePage(int index)=>m_view.m_roletype.selectedIndex=index;
		void SetDescText(string data)=>UIListener.SetText(m_view.m_desc,data);
		string GetDescText()=>UIListener.GetText(m_view.m_desc);
		void SetAreatipsText(string data)=>UIListener.SetText(m_view.m_areatips,data);
		string GetAreatipsText()=>UIListener.GetText(m_view.m_areatips);
		void SetCostText(string data)=>UIListener.SetText(m_view.m_cost,data);
		string GetCostText()=>UIListener.GetText(m_view.m_cost);
		void _OnClickClick(EventContext data){
			OnClickClick(data);
		}
		partial void OnClickClick(EventContext data);
		void SetClickText(string data)=>UIListener.SetText(m_view.m_click,data);
		string GetClickText()=>UIListener.GetText(m_view.m_click);
		void DoCloseUIClick(EventContext data){
			 bool __closestate = true;
			 OnUICloseClick(ref __closestate);
			 if(__closestate)SGame.UIUtils.CloseUIByID(__id);
			 
		}
		partial void OnUICloseClick(ref bool state);
		void SetCloseText(string data)=>UIListener.SetText(m_view.m_close,data);
		string GetCloseText()=>UIListener.GetText(m_view.m_close);

	}
}
