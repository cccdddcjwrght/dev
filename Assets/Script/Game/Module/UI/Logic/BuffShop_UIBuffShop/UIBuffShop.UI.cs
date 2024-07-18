
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.BuffShop;
	
	public partial class UIBuffShop
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick));
			m_view.m_lotteryBtn.m_currency.onChanged.Add(new EventCallback1(_OnShopBuffBtn_CurrencyChanged));
			m_view.m_lotteryBtn.m_cd.onChanged.Add(new EventCallback1(_OnShopBuffBtn_CdChanged));
			m_view.m_lotteryBtn.m_saled.onChanged.Add(new EventCallback1(_OnShopBuffBtn_SaledChanged));
			m_view.m_lotteryBtn.m_click.m_currency.onChanged.Add(new EventCallback1(_Onshopclick_LotteryBtn_click_currencyChanged));
			UIListener.Listener(m_view.m_lotteryBtn.m_click, new EventCallback1(_OnShopBuffBtn_ClickClick));
			UIListener.Listener(m_view.m_lotteryBtn, new EventCallback1(_OnLotteryBtnClick));

		}
		partial void UnInitUI(UIContext context){
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick),remove:true);
			m_view.m_lotteryBtn.m_currency.onChanged.Remove(new EventCallback1(_OnShopBuffBtn_CurrencyChanged));
			m_view.m_lotteryBtn.m_cd.onChanged.Remove(new EventCallback1(_OnShopBuffBtn_CdChanged));
			m_view.m_lotteryBtn.m_saled.onChanged.Remove(new EventCallback1(_OnShopBuffBtn_SaledChanged));
			m_view.m_lotteryBtn.m_click.m_currency.onChanged.Remove(new EventCallback1(_Onshopclick_LotteryBtn_click_currencyChanged));
			UIListener.Listener(m_view.m_lotteryBtn.m_click, new EventCallback1(_OnShopBuffBtn_ClickClick),remove:true);
			UIListener.Listener(m_view.m_lotteryBtn, new EventCallback1(_OnLotteryBtnClick),remove:true);

		}
		void DoCloseUIClick(EventContext data){
			 bool __closestate = true;
			 OnUICloseClick(ref __closestate);
			 if(__closestate)SGame.UIUtils.CloseUIByID(__id);
			 
		}
		partial void OnUICloseClick(ref bool state);
		void SetBodyText(string data)=>UIListener.SetText(m_view.m_body,data);
		string GetBodyText()=>UIListener.GetText(m_view.m_body);
		void SetTotalText(string data)=>UIListener.SetText(m_view.m_total,data);
		string GetTotalText()=>UIListener.GetText(m_view.m_total);
		void SetTimeText(string data)=>UIListener.SetText(m_view.m_time,data);
		string GetTimeText()=>UIListener.GetText(m_view.m_time);
		void _OnShopBuffBtn_CurrencyChanged(EventContext data){
			OnShopBuffBtn_CurrencyChanged(data);
		}
		partial void OnShopBuffBtn_CurrencyChanged(EventContext data);
		void SwitchShopBuffBtn_CurrencyPage(int index)=>m_view.m_lotteryBtn.m_currency.selectedIndex=index;
		void _OnShopBuffBtn_CdChanged(EventContext data){
			OnShopBuffBtn_CdChanged(data);
		}
		partial void OnShopBuffBtn_CdChanged(EventContext data);
		void SwitchShopBuffBtn_CdPage(int index)=>m_view.m_lotteryBtn.m_cd.selectedIndex=index;
		void _OnShopBuffBtn_SaledChanged(EventContext data){
			OnShopBuffBtn_SaledChanged(data);
		}
		partial void OnShopBuffBtn_SaledChanged(EventContext data);
		void SwitchShopBuffBtn_SaledPage(int index)=>m_view.m_lotteryBtn.m_saled.selectedIndex=index;
		void _Onshopclick_LotteryBtn_click_currencyChanged(EventContext data){
			Onshopclick_LotteryBtn_click_currencyChanged(data);
		}
		partial void Onshopclick_LotteryBtn_click_currencyChanged(EventContext data);
		void Switchshopclick_LotteryBtn_click_currencyPage(int index)=>m_view.m_lotteryBtn.m_click.m_currency.selectedIndex=index;
		void _OnShopBuffBtn_ClickClick(EventContext data){
			OnShopBuffBtn_ClickClick(data);
		}
		partial void OnShopBuffBtn_ClickClick(EventContext data);
		void SetShopBuffBtn_LotteryBtn_clickText(string data)=>UIListener.SetText(m_view.m_lotteryBtn.m_click,data);
		string GetShopBuffBtn_LotteryBtn_clickText()=>UIListener.GetText(m_view.m_lotteryBtn.m_click);
		void SetShopBuffBtn_PriceText(string data)=>UIListener.SetText(m_view.m_lotteryBtn.m_price,data);
		string GetShopBuffBtn_PriceText()=>UIListener.GetText(m_view.m_lotteryBtn.m_price);
		void SetShopBuffBtn_TimeText(string data)=>UIListener.SetText(m_view.m_lotteryBtn.m_time,data);
		string GetShopBuffBtn_TimeText()=>UIListener.GetText(m_view.m_lotteryBtn.m_time);
		void _OnLotteryBtnClick(EventContext data){
			OnLotteryBtnClick(data);
		}
		partial void OnLotteryBtnClick(EventContext data);
		void SetLotteryBtnText(string data)=>UIListener.SetText(m_view.m_lotteryBtn,data);
		string GetLotteryBtnText()=>UIListener.GetText(m_view.m_lotteryBtn);

	}
}
