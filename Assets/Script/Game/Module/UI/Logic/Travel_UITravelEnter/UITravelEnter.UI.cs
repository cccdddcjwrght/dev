
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Travel;
	
	public partial class UITravelEnter
	{
		partial void InitUI(UIContext context){
			m_view.m_currency.m_addhide.onChanged.Add(new EventCallback1(_OnCurrency_AddhideChanged));
			m_view.m_currency.m_size.onChanged.Add(new EventCallback1(_OnCurrency_SizeChanged));
			m_view.m_currency.m_mode.onChanged.Add(new EventCallback1(_OnCurrency_ModeChanged));
			m_view.m_currency.m_titlemode.onChanged.Add(new EventCallback1(_OnCurrency_TitlemodeChanged));
			UIListener.ListenerIcon(m_view.m_currency, new EventCallback1(_OnCurrencyClick));
			UIListener.ListenerIcon(m_view.m_head, new EventCallback1(_OnHeadClick));

		}
		partial void UnInitUI(UIContext context){
			m_view.m_currency.m_addhide.onChanged.Remove(new EventCallback1(_OnCurrency_AddhideChanged));
			m_view.m_currency.m_size.onChanged.Remove(new EventCallback1(_OnCurrency_SizeChanged));
			m_view.m_currency.m_mode.onChanged.Remove(new EventCallback1(_OnCurrency_ModeChanged));
			m_view.m_currency.m_titlemode.onChanged.Remove(new EventCallback1(_OnCurrency_TitlemodeChanged));
			UIListener.ListenerIcon(m_view.m_currency, new EventCallback1(_OnCurrencyClick),remove:true);
			UIListener.ListenerIcon(m_view.m_head, new EventCallback1(_OnHeadClick),remove:true);

		}
		void _OnCurrency_AddhideChanged(EventContext data){
			OnCurrency_AddhideChanged(data);
		}
		partial void OnCurrency_AddhideChanged(EventContext data);
		void SwitchCurrency_AddhidePage(int index)=>m_view.m_currency.m_addhide.selectedIndex=index;
		void _OnCurrency_SizeChanged(EventContext data){
			OnCurrency_SizeChanged(data);
		}
		partial void OnCurrency_SizeChanged(EventContext data);
		void SwitchCurrency_SizePage(int index)=>m_view.m_currency.m_size.selectedIndex=index;
		void _OnCurrency_ModeChanged(EventContext data){
			OnCurrency_ModeChanged(data);
		}
		partial void OnCurrency_ModeChanged(EventContext data);
		void SwitchCurrency_ModePage(int index)=>m_view.m_currency.m_mode.selectedIndex=index;
		void _OnCurrency_TitlemodeChanged(EventContext data){
			OnCurrency_TitlemodeChanged(data);
		}
		partial void OnCurrency_TitlemodeChanged(EventContext data);
		void SwitchCurrency_TitlemodePage(int index)=>m_view.m_currency.m_titlemode.selectedIndex=index;
		void SetCurrency_ShadowText(string data)=>UIListener.SetText(m_view.m_currency.m_shadow,data);
		string GetCurrency_ShadowText()=>UIListener.GetText(m_view.m_currency.m_shadow);
		void SetCurrency_LvText(string data)=>UIListener.SetText(m_view.m_currency.m_lv,data);
		string GetCurrency_LvText()=>UIListener.GetText(m_view.m_currency.m_lv);
		void _OnCurrencyClick(EventContext data){
			OnCurrencyClick(data);
		}
		partial void OnCurrencyClick(EventContext data);
		void SetNameText(string data)=>UIListener.SetText(m_view.m_name,data);
		string GetNameText()=>UIListener.GetText(m_view.m_name);
		void _OnHeadClick(EventContext data){
			OnHeadClick(data);
		}
		partial void OnHeadClick(EventContext data);

	}
}
