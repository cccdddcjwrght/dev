
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Cookbook;
	
	public partial class UICustomerbookUp
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			m_view.m_take_reward.onChanged.Add(new EventCallback1(_OnTake_rewardChanged));
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick));
			m_view.m_customer.m_state.onChanged.Add(new EventCallback1(_OnCustomer_StateChanged));
			UIListener.ListenerClose(m_view.m_customer.m_body, new EventCallback1(DoCloseUIClick));
			UIListener.ListenerClose(m_view.m_customer.m_nobody, new EventCallback1(DoCloseUIClick));
			UIListener.ListenerIcon(m_view.m_customer, new EventCallback1(_OnCustomerClick));
			UIListener.ListenerIcon(m_view.m_click, new EventCallback1(_OnClickClick));

		}
		partial void UnInitUI(UIContext context){
			m_view.m_take_reward.onChanged.Remove(new EventCallback1(_OnTake_rewardChanged));
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick),remove:true);
			m_view.m_customer.m_state.onChanged.Remove(new EventCallback1(_OnCustomer_StateChanged));
			UIListener.ListenerClose(m_view.m_customer.m_body, new EventCallback1(DoCloseUIClick),remove:true);
			UIListener.ListenerClose(m_view.m_customer.m_nobody, new EventCallback1(DoCloseUIClick),remove:true);
			UIListener.ListenerIcon(m_view.m_customer, new EventCallback1(_OnCustomerClick),remove:true);
			UIListener.ListenerIcon(m_view.m_click, new EventCallback1(_OnClickClick),remove:true);

		}
		void _OnTake_rewardChanged(EventContext data){
			OnTake_rewardChanged(data);
		}
		partial void OnTake_rewardChanged(EventContext data);
		void SwitchTake_rewardPage(int index)=>m_view.m_take_reward.selectedIndex=index;
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
		void SetTxtDescText(string data)=>UIListener.SetText(m_view.m_txtDesc,data);
		string GetTxtDescText()=>UIListener.GetText(m_view.m_txtDesc);
		void SetTxtDesc2Text(string data)=>UIListener.SetText(m_view.m_txtDesc2,data);
		string GetTxtDesc2Text()=>UIListener.GetText(m_view.m_txtDesc2);
		void SetBtnIconItem_ItemText(string data)=>UIListener.SetText(m_view.m_click.m_item,data);
		string GetBtnIconItem_ItemText()=>UIListener.GetText(m_view.m_click.m_item);
		void _OnClickClick(EventContext data){
			OnClickClick(data);
		}
		partial void OnClickClick(EventContext data);

	}
}
