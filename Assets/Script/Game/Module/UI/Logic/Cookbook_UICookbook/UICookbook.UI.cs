
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Cookbook;
	
	public partial class UICookbook
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			m_view.m_tabs.onChanged.Add(new EventCallback1(_OnTabsChanged));
			m_view.m_body.m_type.onChanged.Add(new EventCallback1(_OnCollectBg_TypeChanged));
			UIListener.ListenerClose(m_view.m_body.m_close, new EventCallback1(DoCloseUIClick));
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick));
			UIListener.Listener(m_view.m_cooker, new EventCallback1(_OnCookerClick));
			UIListener.Listener(m_view.m_waiter, new EventCallback1(_OnWaiterClick));

		}
		partial void UnInitUI(UIContext context){
			m_view.m_tabs.onChanged.Remove(new EventCallback1(_OnTabsChanged));
			m_view.m_body.m_type.onChanged.Remove(new EventCallback1(_OnCollectBg_TypeChanged));
			UIListener.ListenerClose(m_view.m_body.m_close, new EventCallback1(DoCloseUIClick),remove:true);
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick),remove:true);
			UIListener.Listener(m_view.m_cooker, new EventCallback1(_OnCookerClick),remove:true);
			UIListener.Listener(m_view.m_waiter, new EventCallback1(_OnWaiterClick),remove:true);

		}
		void _OnTabsChanged(EventContext data){
			OnTabsChanged(data);
		}
		partial void OnTabsChanged(EventContext data);
		void SwitchTabsPage(int index)=>m_view.m_tabs.selectedIndex=index;
		void _OnCollectBg_TypeChanged(EventContext data){
			OnCollectBg_TypeChanged(data);
		}
		partial void OnCollectBg_TypeChanged(EventContext data);
		void SwitchCollectBg_TypePage(int index)=>m_view.m_body.m_type.selectedIndex=index;
		void DoCloseUIClick(EventContext data){
			 bool __closestate = true;
			 OnUICloseClick(ref __closestate);
			 if(__closestate)SGame.UIUtils.CloseUIByID(__id);
			 
		}
		partial void OnUICloseClick(ref bool state);
		void SetCollectBg_Body_closeText(string data)=>UIListener.SetText(m_view.m_body.m_close,data);
		string GetCollectBg_Body_closeText()=>UIListener.GetText(m_view.m_body.m_close);
		void SetBodyText(string data)=>UIListener.SetText(m_view.m_body,data);
		string GetBodyText()=>UIListener.GetText(m_view.m_body);
		void _OnCookerClick(EventContext data){
			OnCookerClick(data);
		}
		partial void OnCookerClick(EventContext data);
		void SetCookerText(string data)=>UIListener.SetText(m_view.m_cooker,data);
		string GetCookerText()=>UIListener.GetText(m_view.m_cooker);
		void _OnWaiterClick(EventContext data){
			OnWaiterClick(data);
		}
		partial void OnWaiterClick(EventContext data);
		void SetWaiterText(string data)=>UIListener.SetText(m_view.m_waiter,data);
		string GetWaiterText()=>UIListener.GetText(m_view.m_waiter);

	}
}
