
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Cookbook;
	
	public partial class UIWorkerUp
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			m_view.m_state.onChanged.Add(new EventCallback1(_OnStateChanged));
			m_view.m_selected.onChanged.Add(new EventCallback1(_OnSelectedChanged));
			m_view.m_maxlv.onChanged.Add(new EventCallback1(_OnMaxlvChanged));
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick));
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
			UIListener.Listener(m_view.m_reward, new EventCallback1(_OnRewardClick));
			UIListener.Listener(m_view.m_click, new EventCallback1(_OnClickClick));

		}
		partial void UnInitUI(UIContext context){
			m_view.m_state.onChanged.Remove(new EventCallback1(_OnStateChanged));
			m_view.m_selected.onChanged.Remove(new EventCallback1(_OnSelectedChanged));
			m_view.m_maxlv.onChanged.Remove(new EventCallback1(_OnMaxlvChanged));
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick),remove:true);
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
			UIListener.Listener(m_view.m_reward, new EventCallback1(_OnRewardClick),remove:true);
			UIListener.Listener(m_view.m_click, new EventCallback1(_OnClickClick),remove:true);

		}
		void _OnStateChanged(EventContext data){
			OnStateChanged(data);
		}
		partial void OnStateChanged(EventContext data);
		void SwitchStatePage(int index)=>m_view.m_state.selectedIndex=index;
		void _OnSelectedChanged(EventContext data){
			OnSelectedChanged(data);
		}
		partial void OnSelectedChanged(EventContext data);
		void SwitchSelectedPage(int index)=>m_view.m_selected.selectedIndex=index;
		void _OnMaxlvChanged(EventContext data){
			OnMaxlvChanged(data);
		}
		partial void OnMaxlvChanged(EventContext data);
		void SwitchMaxlvPage(int index)=>m_view.m_maxlv.selectedIndex=index;
		void DoCloseUIClick(EventContext data){
			 bool __closestate = true;
			 OnUICloseClick(ref __closestate);
			 if(__closestate)SGame.UIUtils.CloseUIByID(__id);
			 
		}
		partial void OnUICloseClick(ref bool state);
		void SetBodyText(string data)=>UIListener.SetText(m_view.m_body,data);
		string GetBodyText()=>UIListener.GetText(m_view.m_body);
		void _OnCustomer_StateChanged(EventContext data){
			OnCustomer_StateChanged(data);
		}
		partial void OnCustomer_StateChanged(EventContext data);
		void SwitchCustomer_StatePage(int index)=>m_view.m_customer.m_state.selectedIndex=index;
		void _OnCustomerClick(EventContext data){
			OnCustomerClick(data);
		}
		partial void OnCustomerClick(EventContext data);
		void SetDescText(string data)=>UIListener.SetText(m_view.m_desc,data);
		string GetDescText()=>UIListener.GetText(m_view.m_desc);
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
		void _OnRewardClick(EventContext data){
			OnRewardClick(data);
		}
		partial void OnRewardClick(EventContext data);
		void SetRewardText(string data)=>UIListener.SetText(m_view.m_reward,data);
		string GetRewardText()=>UIListener.GetText(m_view.m_reward);
		void _OnClickClick(EventContext data){
			OnClickClick(data);
		}
		partial void OnClickClick(EventContext data);
		void SetClickText(string data)=>UIListener.SetText(m_view.m_click,data);
		string GetClickText()=>UIListener.GetText(m_view.m_click);

	}
}
