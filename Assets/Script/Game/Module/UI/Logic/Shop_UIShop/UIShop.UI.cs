﻿
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Shop;
	
	public partial class UIShop
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			m_view.m_pages.onChanged.Add(new EventCallback1(_OnPagesChanged));
			m_view.m_rate.onChanged.Add(new EventCallback1(_OnRateChanged));
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick));
			m_view.m_content.m_pages.onChanged.Add(new EventCallback1(_OnShopBody_PagesChanged));
			m_view.m_content.m_adgood.m_type.onChanged.Add(new EventCallback1(_OnBigGoods_TypeChanged));
			m_view.m_content.m_adgood.m_saled.onChanged.Add(new EventCallback1(_OnBigGoods_SaledChanged));
			m_view.m_content.m_adgood.m_left_state.onChanged.Add(new EventCallback1(_OnBigGoods_Left_stateChanged));
			UIListener.Listener(m_view.m_content.m_adgood.m_click, new EventCallback1(_OnBigGoods_ClickClick));
			UIListener.ListenerIcon(m_view.m_content.m_adgood, new EventCallback1(_OnShopBody_AdgoodClick));
			UIListener.ListenerIcon(m_view.m_content, new EventCallback1(_OnContentClick));
			m_view.m_rate_2.m_show.onChanged.Add(new EventCallback1(_OnProbability_ShowChanged));
			UIListener.ListenerIcon(m_view.m_rate_2, new EventCallback1(_OnRate_2Click));

		}
		partial void UnInitUI(UIContext context){
			m_view.m_pages.onChanged.Remove(new EventCallback1(_OnPagesChanged));
			m_view.m_rate.onChanged.Remove(new EventCallback1(_OnRateChanged));
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick),remove:true);
			m_view.m_content.m_pages.onChanged.Remove(new EventCallback1(_OnShopBody_PagesChanged));
			m_view.m_content.m_adgood.m_type.onChanged.Remove(new EventCallback1(_OnBigGoods_TypeChanged));
			m_view.m_content.m_adgood.m_saled.onChanged.Remove(new EventCallback1(_OnBigGoods_SaledChanged));
			m_view.m_content.m_adgood.m_left_state.onChanged.Remove(new EventCallback1(_OnBigGoods_Left_stateChanged));
			UIListener.Listener(m_view.m_content.m_adgood.m_click, new EventCallback1(_OnBigGoods_ClickClick),remove:true);
			UIListener.ListenerIcon(m_view.m_content.m_adgood, new EventCallback1(_OnShopBody_AdgoodClick),remove:true);
			UIListener.ListenerIcon(m_view.m_content, new EventCallback1(_OnContentClick),remove:true);
			m_view.m_rate_2.m_show.onChanged.Remove(new EventCallback1(_OnProbability_ShowChanged));
			UIListener.ListenerIcon(m_view.m_rate_2, new EventCallback1(_OnRate_2Click),remove:true);

		}
		void _OnPagesChanged(EventContext data){
			OnPagesChanged(data);
		}
		partial void OnPagesChanged(EventContext data);
		void SwitchPagesPage(int index)=>m_view.m_pages.selectedIndex=index;
		void _OnRateChanged(EventContext data){
			OnRateChanged(data);
		}
		partial void OnRateChanged(EventContext data);
		void SwitchRatePage(int index)=>m_view.m_rate.selectedIndex=index;
		void DoCloseUIClick(EventContext data){
			 bool __closestate = true;
			 OnUICloseClick(ref __closestate);
			 if(__closestate)SGame.UIUtils.CloseUIByID(__id);
			 
		}
		partial void OnUICloseClick(ref bool state);
		void SetBodyText(string data)=>UIListener.SetText(m_view.m_body,data);
		string GetBodyText()=>UIListener.GetText(m_view.m_body);
		void _OnShopBody_PagesChanged(EventContext data){
			OnShopBody_PagesChanged(data);
		}
		partial void OnShopBody_PagesChanged(EventContext data);
		void SwitchShopBody_PagesPage(int index)=>m_view.m_content.m_pages.selectedIndex=index;
		void _OnBigGoods_TypeChanged(EventContext data){
			OnBigGoods_TypeChanged(data);
		}
		partial void OnBigGoods_TypeChanged(EventContext data);
		void SwitchBigGoods_TypePage(int index)=>m_view.m_content.m_adgood.m_type.selectedIndex=index;
		void _OnBigGoods_SaledChanged(EventContext data){
			OnBigGoods_SaledChanged(data);
		}
		partial void OnBigGoods_SaledChanged(EventContext data);
		void SwitchBigGoods_SaledPage(int index)=>m_view.m_content.m_adgood.m_saled.selectedIndex=index;
		void _OnBigGoods_Left_stateChanged(EventContext data){
			OnBigGoods_Left_stateChanged(data);
		}
		partial void OnBigGoods_Left_stateChanged(EventContext data);
		void SwitchBigGoods_Left_statePage(int index)=>m_view.m_content.m_adgood.m_left_state.selectedIndex=index;
		void _OnBigGoods_ClickClick(EventContext data){
			OnBigGoods_ClickClick(data);
		}
		partial void OnBigGoods_ClickClick(EventContext data);
		void SetBigGoods_ClickText(string data)=>UIListener.SetText(m_view.m_content.m_adgood.m_click,data);
		string GetBigGoods_ClickText()=>UIListener.GetText(m_view.m_content.m_adgood.m_click);
		void SetBigGoods_LeftText(string data)=>UIListener.SetText(m_view.m_content.m_adgood.m_left,data);
		string GetBigGoods_LeftText()=>UIListener.GetText(m_view.m_content.m_adgood.m_left);
		void SetBigGoods_DescText(string data)=>UIListener.SetText(m_view.m_content.m_adgood.m_desc,data);
		string GetBigGoods_DescText()=>UIListener.GetText(m_view.m_content.m_adgood.m_desc);
		void SetBigGoods_CountText(string data)=>UIListener.SetText(m_view.m_content.m_adgood.m_count,data);
		string GetBigGoods_CountText()=>UIListener.GetText(m_view.m_content.m_adgood.m_count);
		void _OnShopBody_AdgoodClick(EventContext data){
			OnShopBody_AdgoodClick(data);
		}
		partial void OnShopBody_AdgoodClick(EventContext data);
		void SetShopBody_AdgoodText(string data)=>UIListener.SetText(m_view.m_content.m_adgood,data);
		string GetShopBody_AdgoodText()=>UIListener.GetText(m_view.m_content.m_adgood);
		void _OnContentClick(EventContext data){
			OnContentClick(data);
		}
		partial void OnContentClick(EventContext data);
		void _OnProbability_ShowChanged(EventContext data){
			OnProbability_ShowChanged(data);
		}
		partial void OnProbability_ShowChanged(EventContext data);
		void SwitchProbability_ShowPage(int index)=>m_view.m_rate_2.m_show.selectedIndex=index;
		void SetProbability_BgText(string data)=>UIListener.SetText(m_view.m_rate_2.m_bg,data);
		string GetProbability_BgText()=>UIListener.GetText(m_view.m_rate_2.m_bg);
		void _OnRate_2Click(EventContext data){
			OnRate_2Click(data);
		}
		partial void OnRate_2Click(EventContext data);

	}
}
