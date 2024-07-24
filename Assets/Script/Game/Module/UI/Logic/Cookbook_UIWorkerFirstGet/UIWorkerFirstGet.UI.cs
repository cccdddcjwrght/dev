
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Cookbook;
	
	public partial class UIWorkerFirstGet
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			m_view.m_type.onChanged.Add(new EventCallback1(_OnTypeChanged));
			UIListener.ListenerClose(m_view.m_mask, new EventCallback1(DoCloseUIClick));
			m_view.m_customer.m_state.onChanged.Add(new EventCallback1(_OnCustomer_StateChanged));
			UIListener.ListenerClose(m_view.m_customer.m_body, new EventCallback1(DoCloseUIClick));
			UIListener.ListenerClose(m_view.m_customer.m_nobody, new EventCallback1(DoCloseUIClick));
			UIListener.ListenerIcon(m_view.m_customer, new EventCallback1(_OnCustomerClick));
			m_view.m_progress.m_lock.onChanged.Add(new EventCallback1(_OnWorkerProgress_LockChanged));
			UIListener.ListenerIcon(m_view.m_progress, new EventCallback1(_OnProgressClick));
			m_view.m_property.m_type.onChanged.Add(new EventCallback1(_OnWorkerAddProperty_TypeChanged));
			m_view.m_property.m_fullsize.onChanged.Add(new EventCallback1(_OnWorkerAddProperty_FullsizeChanged));
			m_view.m_property.m_color.onChanged.Add(new EventCallback1(_OnWorkerAddProperty_ColorChanged));
			UIListener.ListenerIcon(m_view.m_property, new EventCallback1(_OnPropertyClick));

		}
		partial void UnInitUI(UIContext context){
			m_view.m_type.onChanged.Remove(new EventCallback1(_OnTypeChanged));
			UIListener.ListenerClose(m_view.m_mask, new EventCallback1(DoCloseUIClick),remove:true);
			m_view.m_customer.m_state.onChanged.Remove(new EventCallback1(_OnCustomer_StateChanged));
			UIListener.ListenerClose(m_view.m_customer.m_body, new EventCallback1(DoCloseUIClick),remove:true);
			UIListener.ListenerClose(m_view.m_customer.m_nobody, new EventCallback1(DoCloseUIClick),remove:true);
			UIListener.ListenerIcon(m_view.m_customer, new EventCallback1(_OnCustomerClick),remove:true);
			m_view.m_progress.m_lock.onChanged.Remove(new EventCallback1(_OnWorkerProgress_LockChanged));
			UIListener.ListenerIcon(m_view.m_progress, new EventCallback1(_OnProgressClick),remove:true);
			m_view.m_property.m_type.onChanged.Remove(new EventCallback1(_OnWorkerAddProperty_TypeChanged));
			m_view.m_property.m_fullsize.onChanged.Remove(new EventCallback1(_OnWorkerAddProperty_FullsizeChanged));
			m_view.m_property.m_color.onChanged.Remove(new EventCallback1(_OnWorkerAddProperty_ColorChanged));
			UIListener.ListenerIcon(m_view.m_property, new EventCallback1(_OnPropertyClick),remove:true);

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
		void _OnCustomer_StateChanged(EventContext data){
			OnCustomer_StateChanged(data);
		}
		partial void OnCustomer_StateChanged(EventContext data);
		void SwitchCustomer_StatePage(int index)=>m_view.m_customer.m_state.selectedIndex=index;
		void _OnCustomerClick(EventContext data){
			OnCustomerClick(data);
		}
		partial void OnCustomerClick(EventContext data);
		void _OnWorkerProgress_LockChanged(EventContext data){
			OnWorkerProgress_LockChanged(data);
		}
		partial void OnWorkerProgress_LockChanged(EventContext data);
		void SwitchWorkerProgress_LockPage(int index)=>m_view.m_progress.m_lock.selectedIndex=index;
		void _OnProgressClick(EventContext data){
			OnProgressClick(data);
		}
		partial void OnProgressClick(EventContext data);
		void SetProgressText(string data)=>UIListener.SetText(m_view.m_progress,data);
		string GetProgressText()=>UIListener.GetText(m_view.m_progress);
		void _OnWorkerAddProperty_TypeChanged(EventContext data){
			OnWorkerAddProperty_TypeChanged(data);
		}
		partial void OnWorkerAddProperty_TypeChanged(EventContext data);
		void SwitchWorkerAddProperty_TypePage(int index)=>m_view.m_property.m_type.selectedIndex=index;
		void _OnWorkerAddProperty_FullsizeChanged(EventContext data){
			OnWorkerAddProperty_FullsizeChanged(data);
		}
		partial void OnWorkerAddProperty_FullsizeChanged(EventContext data);
		void SwitchWorkerAddProperty_FullsizePage(int index)=>m_view.m_property.m_fullsize.selectedIndex=index;
		void _OnWorkerAddProperty_ColorChanged(EventContext data){
			OnWorkerAddProperty_ColorChanged(data);
		}
		partial void OnWorkerAddProperty_ColorChanged(EventContext data);
		void SwitchWorkerAddProperty_ColorPage(int index)=>m_view.m_property.m_color.selectedIndex=index;
		void SetWorkerAddProperty_FullText(string data)=>UIListener.SetText(m_view.m_property.m_full,data);
		string GetWorkerAddProperty_FullText()=>UIListener.GetText(m_view.m_property.m_full);
		void SetWorkerAddProperty_NextText(string data)=>UIListener.SetText(m_view.m_property.m_next,data);
		string GetWorkerAddProperty_NextText()=>UIListener.GetText(m_view.m_property.m_next);
		void _OnPropertyClick(EventContext data){
			OnPropertyClick(data);
		}
		partial void OnPropertyClick(EventContext data);
		void SetPropertyText(string data)=>UIListener.SetText(m_view.m_property,data);
		string GetPropertyText()=>UIListener.GetText(m_view.m_property);

	}
}
