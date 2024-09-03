
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.DailyTask;
	
	public partial class UIDailyTask
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick));
			m_view.m_giftItem1.m_state.onChanged.Add(new EventCallback1(_OnDailyGiftItem_StateChanged));
			UIListener.ListenerIcon(m_view.m_giftItem1, new EventCallback1(_OnGiftItem1Click));
			m_view.m_giftItem2.m_state.onChanged.Add(new EventCallback1(_OnDailyGiftItem_GiftItem2_stateChanged));
			UIListener.ListenerIcon(m_view.m_giftItem2, new EventCallback1(_OnGiftItem2Click));
			m_view.m_giftItem3.m_state.onChanged.Add(new EventCallback1(_OnDailyGiftItem_GiftItem3_stateChanged));
			UIListener.ListenerIcon(m_view.m_giftItem3, new EventCallback1(_OnGiftItem3Click));
			m_view.m_giftItem4.m_state.onChanged.Add(new EventCallback1(_OnDailyGiftItem_GiftItem4_stateChanged));
			UIListener.ListenerIcon(m_view.m_giftItem4, new EventCallback1(_OnGiftItem4Click));
			m_view.m_giftItem5.m_state.onChanged.Add(new EventCallback1(_OnDailyGiftItem_GiftItem5_stateChanged));
			UIListener.ListenerIcon(m_view.m_giftItem5, new EventCallback1(_OnGiftItem5Click));

		}
		partial void UnInitUI(UIContext context){
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick),remove:true);
			m_view.m_giftItem1.m_state.onChanged.Remove(new EventCallback1(_OnDailyGiftItem_StateChanged));
			UIListener.ListenerIcon(m_view.m_giftItem1, new EventCallback1(_OnGiftItem1Click),remove:true);
			m_view.m_giftItem2.m_state.onChanged.Remove(new EventCallback1(_OnDailyGiftItem_GiftItem2_stateChanged));
			UIListener.ListenerIcon(m_view.m_giftItem2, new EventCallback1(_OnGiftItem2Click),remove:true);
			m_view.m_giftItem3.m_state.onChanged.Remove(new EventCallback1(_OnDailyGiftItem_GiftItem3_stateChanged));
			UIListener.ListenerIcon(m_view.m_giftItem3, new EventCallback1(_OnGiftItem3Click),remove:true);
			m_view.m_giftItem4.m_state.onChanged.Remove(new EventCallback1(_OnDailyGiftItem_GiftItem4_stateChanged));
			UIListener.ListenerIcon(m_view.m_giftItem4, new EventCallback1(_OnGiftItem4Click),remove:true);
			m_view.m_giftItem5.m_state.onChanged.Remove(new EventCallback1(_OnDailyGiftItem_GiftItem5_stateChanged));
			UIListener.ListenerIcon(m_view.m_giftItem5, new EventCallback1(_OnGiftItem5Click),remove:true);

		}
		void DoCloseUIClick(EventContext data){
			 bool __closestate = true;
			 OnUICloseClick(ref __closestate);
			 if(__closestate)SGame.UIUtils.CloseUIByID(__id);
			 
		}
		partial void OnUICloseClick(ref bool state);
		void SetBodyText(string data)=>UIListener.SetText(m_view.m_body,data);
		string GetBodyText()=>UIListener.GetText(m_view.m_body);
		void SetDayValueText(string data)=>UIListener.SetText(m_view.m_dayValue,data);
		string GetDayValueText()=>UIListener.GetText(m_view.m_dayValue);
		void _OnDailyGiftItem_StateChanged(EventContext data){
			OnDailyGiftItem_StateChanged(data);
		}
		partial void OnDailyGiftItem_StateChanged(EventContext data);
		void SwitchDailyGiftItem_StatePage(int index)=>m_view.m_giftItem1.m_state.selectedIndex=index;
		void _OnGiftItem1Click(EventContext data){
			OnGiftItem1Click(data);
		}
		partial void OnGiftItem1Click(EventContext data);
		void SetGiftItem1Text(string data)=>UIListener.SetText(m_view.m_giftItem1,data);
		string GetGiftItem1Text()=>UIListener.GetText(m_view.m_giftItem1);
		void _OnDailyGiftItem_GiftItem2_stateChanged(EventContext data){
			OnDailyGiftItem_GiftItem2_stateChanged(data);
		}
		partial void OnDailyGiftItem_GiftItem2_stateChanged(EventContext data);
		void SwitchDailyGiftItem_GiftItem2_statePage(int index)=>m_view.m_giftItem2.m_state.selectedIndex=index;
		void _OnGiftItem2Click(EventContext data){
			OnGiftItem2Click(data);
		}
		partial void OnGiftItem2Click(EventContext data);
		void SetGiftItem2Text(string data)=>UIListener.SetText(m_view.m_giftItem2,data);
		string GetGiftItem2Text()=>UIListener.GetText(m_view.m_giftItem2);
		void _OnDailyGiftItem_GiftItem3_stateChanged(EventContext data){
			OnDailyGiftItem_GiftItem3_stateChanged(data);
		}
		partial void OnDailyGiftItem_GiftItem3_stateChanged(EventContext data);
		void SwitchDailyGiftItem_GiftItem3_statePage(int index)=>m_view.m_giftItem3.m_state.selectedIndex=index;
		void _OnGiftItem3Click(EventContext data){
			OnGiftItem3Click(data);
		}
		partial void OnGiftItem3Click(EventContext data);
		void SetGiftItem3Text(string data)=>UIListener.SetText(m_view.m_giftItem3,data);
		string GetGiftItem3Text()=>UIListener.GetText(m_view.m_giftItem3);
		void _OnDailyGiftItem_GiftItem4_stateChanged(EventContext data){
			OnDailyGiftItem_GiftItem4_stateChanged(data);
		}
		partial void OnDailyGiftItem_GiftItem4_stateChanged(EventContext data);
		void SwitchDailyGiftItem_GiftItem4_statePage(int index)=>m_view.m_giftItem4.m_state.selectedIndex=index;
		void _OnGiftItem4Click(EventContext data){
			OnGiftItem4Click(data);
		}
		partial void OnGiftItem4Click(EventContext data);
		void SetGiftItem4Text(string data)=>UIListener.SetText(m_view.m_giftItem4,data);
		string GetGiftItem4Text()=>UIListener.GetText(m_view.m_giftItem4);
		void _OnDailyGiftItem_GiftItem5_stateChanged(EventContext data){
			OnDailyGiftItem_GiftItem5_stateChanged(data);
		}
		partial void OnDailyGiftItem_GiftItem5_stateChanged(EventContext data);
		void SwitchDailyGiftItem_GiftItem5_statePage(int index)=>m_view.m_giftItem5.m_state.selectedIndex=index;
		void _OnGiftItem5Click(EventContext data){
			OnGiftItem5Click(data);
		}
		partial void OnGiftItem5Click(EventContext data);
		void SetGiftItem5Text(string data)=>UIListener.SetText(m_view.m_giftItem5,data);
		string GetGiftItem5Text()=>UIListener.GetText(m_view.m_giftItem5);
		void SetDayTimeText(string data)=>UIListener.SetText(m_view.m_dayTime,data);
		string GetDayTimeText()=>UIListener.GetText(m_view.m_dayTime);
		void SetWeekTimeText(string data)=>UIListener.SetText(m_view.m_weekTime,data);
		string GetWeekTimeText()=>UIListener.GetText(m_view.m_weekTime);
		void SetWeekValueText(string data)=>UIListener.SetText(m_view.m_weekValue,data);
		string GetWeekValueText()=>UIListener.GetText(m_view.m_weekValue);

	}
}
