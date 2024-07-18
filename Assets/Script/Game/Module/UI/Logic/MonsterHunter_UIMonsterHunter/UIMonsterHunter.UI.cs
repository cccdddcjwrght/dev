
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.MonsterHunter;
	
	public partial class UIMonsterHunter
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			m_view.m_play.onChanged.Add(new EventCallback1(_OnPlayChanged));
			m_view.m_auto.onChanged.Add(new EventCallback1(_OnAutoChanged));
			m_view.m_type.onChanged.Add(new EventCallback1(_OnTypeChanged));
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick));
			UIListener.Listener(m_view.m_hp.m_item1, new EventCallback1(_OnMonsterHp_Item1Click));
			UIListener.Listener(m_view.m_hp.m_item2, new EventCallback1(_OnMonsterHp_Item2Click));
			UIListener.ListenerIcon(m_view.m_hp, new EventCallback1(_OnHpClick));
			UIListener.Listener(m_view.m_showreward, new EventCallback1(_OnShowrewardClick));
			m_view.m_panel.m_auto.onChanged.Add(new EventCallback1(_OnHunterWheel_AutoChanged));
			m_view.m_panel.m_panel.m_type.onChanged.Add(new EventCallback1(_OnSelectPanel_Panel_panel_typeChanged));
			m_view.m_panel.m_panel.m_play.onChanged.Add(new EventCallback1(_OnSelectPanel_Panel_panel_playChanged));
			m_view.m_panel.m_panel.m_panel.m_item0_x.m_type.onChanged.Add(new EventCallback1(_OnHunterBullet_Panel_panel_panel_item0_x_typeChanged));
			m_view.m_panel.m_panel.m_panel.m_item1_x.m_type.onChanged.Add(new EventCallback1(_OnHunterBullet_Panel_panel_panel_item1_x_typeChanged));
			m_view.m_panel.m_panel.m_panel.m_item2_x.m_type.onChanged.Add(new EventCallback1(_OnHunterBullet_Panel_panel_panel_item2_x_typeChanged));
			m_view.m_panel.m_panel.m_panel.m_item3_x.m_type.onChanged.Add(new EventCallback1(_OnHunterBullet_Panel_panel_panel_item3_x_typeChanged));
			m_view.m_panel.m_panel.m_panel.m_item4_x.m_type.onChanged.Add(new EventCallback1(_OnHunterBullet_Panel_panel_panel_item4_x_typeChanged));
			m_view.m_panel.m_panel.m_panel.m_item5_x.m_type.onChanged.Add(new EventCallback1(_OnHunterBullet_Panel_panel_panel_item5_x_typeChanged));
			m_view.m_panel.m_panel.m_panel.m_item6_x.m_type.onChanged.Add(new EventCallback1(_OnHunterBullet_Panel_panel_panel_item6_x_typeChanged));
			m_view.m_panel.m_panel.m_panel.m_item7_x.m_type.onChanged.Add(new EventCallback1(_OnHunterBullet_Panel_panel_panel_item7_x_typeChanged));
			m_view.m_panel.m_panel.m_panel.m_item8_x.m_type.onChanged.Add(new EventCallback1(_OnHunterBullet_Panel_panel_panel_item8_x_typeChanged));
			m_view.m_panel.m_panel.m_panel.m_item9_x.m_type.onChanged.Add(new EventCallback1(_OnHunterBullet_Panel_panel_panel_item9_x_typeChanged));
			m_view.m_panel.m_panel.m_panel.m_item10_x.m_type.onChanged.Add(new EventCallback1(_OnHunterBullet_Panel_panel_panel_item10_x_typeChanged));
			m_view.m_panel.m_panel.m_panel.m_item11_x.m_type.onChanged.Add(new EventCallback1(_OnHunterBullet_Panel_panel_panel_item11_x_typeChanged));
			UIListener.ListenerIcon(m_view.m_panel.m_panel.m_panel, new EventCallback1(_OnSelectPanel_Panel_panel_panelClick));
			UIListener.ListenerIcon(m_view.m_panel.m_panel, new EventCallback1(_OnHunterWheel_PanelClick));
			m_view.m_panel.m_playbtn.m_auto.onChanged.Add(new EventCallback1(_OnHunterBtn_Panel_playbtn_autoChanged));
			UIListener.Listener(m_view.m_panel.m_playbtn, new EventCallback1(_OnHunterWheel_PlaybtnClick));
			UIListener.Listener(m_view.m_panel.m_currency, new EventCallback1(_OnHunterWheel_CurrencyClick));
			UIListener.Listener(m_view.m_panel.m_power, new EventCallback1(_OnHunterWheel_PowerClick));
			UIListener.ListenerIcon(m_view.m_panel, new EventCallback1(_OnPanelClick));
			m_view.m_reward.m_open.onChanged.Add(new EventCallback1(_OnHunterRewardTips_OpenChanged));
			m_view.m_reward.m_completed.onChanged.Add(new EventCallback1(_OnHunterRewardTips_CompletedChanged));
			m_view.m_reward.m_type.onChanged.Add(new EventCallback1(_OnHunterRewardTips_TypeChanged));
			UIListener.ListenerIcon(m_view.m_reward, new EventCallback1(_OnRewardClick));
			m_view.m_rewardsPreview.m_size.onChanged.Add(new EventCallback1(_OnHunterRewardList_SizeChanged));
			UIListener.ListenerIcon(m_view.m_rewardsPreview, new EventCallback1(_OnRewardsPreviewClick));

		}
		partial void UnInitUI(UIContext context){
			m_view.m_play.onChanged.Remove(new EventCallback1(_OnPlayChanged));
			m_view.m_auto.onChanged.Remove(new EventCallback1(_OnAutoChanged));
			m_view.m_type.onChanged.Remove(new EventCallback1(_OnTypeChanged));
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick),remove:true);
			UIListener.Listener(m_view.m_hp.m_item1, new EventCallback1(_OnMonsterHp_Item1Click),remove:true);
			UIListener.Listener(m_view.m_hp.m_item2, new EventCallback1(_OnMonsterHp_Item2Click),remove:true);
			UIListener.ListenerIcon(m_view.m_hp, new EventCallback1(_OnHpClick),remove:true);
			UIListener.Listener(m_view.m_showreward, new EventCallback1(_OnShowrewardClick),remove:true);
			m_view.m_panel.m_auto.onChanged.Remove(new EventCallback1(_OnHunterWheel_AutoChanged));
			m_view.m_panel.m_panel.m_type.onChanged.Remove(new EventCallback1(_OnSelectPanel_Panel_panel_typeChanged));
			m_view.m_panel.m_panel.m_play.onChanged.Remove(new EventCallback1(_OnSelectPanel_Panel_panel_playChanged));
			m_view.m_panel.m_panel.m_panel.m_item0_x.m_type.onChanged.Remove(new EventCallback1(_OnHunterBullet_Panel_panel_panel_item0_x_typeChanged));
			m_view.m_panel.m_panel.m_panel.m_item1_x.m_type.onChanged.Remove(new EventCallback1(_OnHunterBullet_Panel_panel_panel_item1_x_typeChanged));
			m_view.m_panel.m_panel.m_panel.m_item2_x.m_type.onChanged.Remove(new EventCallback1(_OnHunterBullet_Panel_panel_panel_item2_x_typeChanged));
			m_view.m_panel.m_panel.m_panel.m_item3_x.m_type.onChanged.Remove(new EventCallback1(_OnHunterBullet_Panel_panel_panel_item3_x_typeChanged));
			m_view.m_panel.m_panel.m_panel.m_item4_x.m_type.onChanged.Remove(new EventCallback1(_OnHunterBullet_Panel_panel_panel_item4_x_typeChanged));
			m_view.m_panel.m_panel.m_panel.m_item5_x.m_type.onChanged.Remove(new EventCallback1(_OnHunterBullet_Panel_panel_panel_item5_x_typeChanged));
			m_view.m_panel.m_panel.m_panel.m_item6_x.m_type.onChanged.Remove(new EventCallback1(_OnHunterBullet_Panel_panel_panel_item6_x_typeChanged));
			m_view.m_panel.m_panel.m_panel.m_item7_x.m_type.onChanged.Remove(new EventCallback1(_OnHunterBullet_Panel_panel_panel_item7_x_typeChanged));
			m_view.m_panel.m_panel.m_panel.m_item8_x.m_type.onChanged.Remove(new EventCallback1(_OnHunterBullet_Panel_panel_panel_item8_x_typeChanged));
			m_view.m_panel.m_panel.m_panel.m_item9_x.m_type.onChanged.Remove(new EventCallback1(_OnHunterBullet_Panel_panel_panel_item9_x_typeChanged));
			m_view.m_panel.m_panel.m_panel.m_item10_x.m_type.onChanged.Remove(new EventCallback1(_OnHunterBullet_Panel_panel_panel_item10_x_typeChanged));
			m_view.m_panel.m_panel.m_panel.m_item11_x.m_type.onChanged.Remove(new EventCallback1(_OnHunterBullet_Panel_panel_panel_item11_x_typeChanged));
			UIListener.ListenerIcon(m_view.m_panel.m_panel.m_panel, new EventCallback1(_OnSelectPanel_Panel_panel_panelClick),remove:true);
			UIListener.ListenerIcon(m_view.m_panel.m_panel, new EventCallback1(_OnHunterWheel_PanelClick),remove:true);
			m_view.m_panel.m_playbtn.m_auto.onChanged.Remove(new EventCallback1(_OnHunterBtn_Panel_playbtn_autoChanged));
			UIListener.Listener(m_view.m_panel.m_playbtn, new EventCallback1(_OnHunterWheel_PlaybtnClick),remove:true);
			UIListener.Listener(m_view.m_panel.m_currency, new EventCallback1(_OnHunterWheel_CurrencyClick),remove:true);
			UIListener.Listener(m_view.m_panel.m_power, new EventCallback1(_OnHunterWheel_PowerClick),remove:true);
			UIListener.ListenerIcon(m_view.m_panel, new EventCallback1(_OnPanelClick),remove:true);
			m_view.m_reward.m_open.onChanged.Remove(new EventCallback1(_OnHunterRewardTips_OpenChanged));
			m_view.m_reward.m_completed.onChanged.Remove(new EventCallback1(_OnHunterRewardTips_CompletedChanged));
			m_view.m_reward.m_type.onChanged.Remove(new EventCallback1(_OnHunterRewardTips_TypeChanged));
			UIListener.ListenerIcon(m_view.m_reward, new EventCallback1(_OnRewardClick),remove:true);
			m_view.m_rewardsPreview.m_size.onChanged.Remove(new EventCallback1(_OnHunterRewardList_SizeChanged));
			UIListener.ListenerIcon(m_view.m_rewardsPreview, new EventCallback1(_OnRewardsPreviewClick),remove:true);

		}
		void _OnPlayChanged(EventContext data){
			OnPlayChanged(data);
		}
		partial void OnPlayChanged(EventContext data);
		void SwitchPlayPage(int index)=>m_view.m_play.selectedIndex=index;
		void _OnAutoChanged(EventContext data){
			OnAutoChanged(data);
		}
		partial void OnAutoChanged(EventContext data);
		void SwitchAutoPage(int index)=>m_view.m_auto.selectedIndex=index;
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
		void _OnMonsterHp_Item1Click(EventContext data){
			OnMonsterHp_Item1Click(data);
		}
		partial void OnMonsterHp_Item1Click(EventContext data);
		void SetMonsterHp_Hp_item1Text(string data)=>UIListener.SetText(m_view.m_hp.m_item1,data);
		string GetMonsterHp_Hp_item1Text()=>UIListener.GetText(m_view.m_hp.m_item1);
		void _OnMonsterHp_Item2Click(EventContext data){
			OnMonsterHp_Item2Click(data);
		}
		partial void OnMonsterHp_Item2Click(EventContext data);
		void SetMonsterHp_Hp_item2Text(string data)=>UIListener.SetText(m_view.m_hp.m_item2,data);
		string GetMonsterHp_Hp_item2Text()=>UIListener.GetText(m_view.m_hp.m_item2);
		void _OnHpClick(EventContext data){
			OnHpClick(data);
		}
		partial void OnHpClick(EventContext data);
		void SetHpText(string data)=>UIListener.SetText(m_view.m_hp,data);
		string GetHpText()=>UIListener.GetText(m_view.m_hp);
		void _OnShowrewardClick(EventContext data){
			OnShowrewardClick(data);
		}
		partial void OnShowrewardClick(EventContext data);
		void SetShowrewardText(string data)=>UIListener.SetText(m_view.m_showreward,data);
		string GetShowrewardText()=>UIListener.GetText(m_view.m_showreward);
		void _OnHunterWheel_AutoChanged(EventContext data){
			OnHunterWheel_AutoChanged(data);
		}
		partial void OnHunterWheel_AutoChanged(EventContext data);
		void SwitchHunterWheel_AutoPage(int index)=>m_view.m_panel.m_auto.selectedIndex=index;
		void _OnSelectPanel_Panel_panel_typeChanged(EventContext data){
			OnSelectPanel_Panel_panel_typeChanged(data);
		}
		partial void OnSelectPanel_Panel_panel_typeChanged(EventContext data);
		void SwitchSelectPanel_Panel_panel_typePage(int index)=>m_view.m_panel.m_panel.m_type.selectedIndex=index;
		void _OnSelectPanel_Panel_panel_playChanged(EventContext data){
			OnSelectPanel_Panel_panel_playChanged(data);
		}
		partial void OnSelectPanel_Panel_panel_playChanged(EventContext data);
		void SwitchSelectPanel_Panel_panel_playPage(int index)=>m_view.m_panel.m_panel.m_play.selectedIndex=index;
		void _OnHunterBullet_Panel_panel_panel_item0_x_typeChanged(EventContext data){
			OnHunterBullet_Panel_panel_panel_item0_x_typeChanged(data);
		}
		partial void OnHunterBullet_Panel_panel_panel_item0_x_typeChanged(EventContext data);
		void SwitchHunterBullet_Panel_panel_panel_item0_x_typePage(int index)=>m_view.m_panel.m_panel.m_panel.m_item0_x.m_type.selectedIndex=index;
		void SetPanelBody_Panel_panel_panel_item0_xText(string data)=>UIListener.SetText(m_view.m_panel.m_panel.m_panel.m_item0_x,data);
		string GetPanelBody_Panel_panel_panel_item0_xText()=>UIListener.GetText(m_view.m_panel.m_panel.m_panel.m_item0_x);
		void _OnHunterBullet_Panel_panel_panel_item1_x_typeChanged(EventContext data){
			OnHunterBullet_Panel_panel_panel_item1_x_typeChanged(data);
		}
		partial void OnHunterBullet_Panel_panel_panel_item1_x_typeChanged(EventContext data);
		void SwitchHunterBullet_Panel_panel_panel_item1_x_typePage(int index)=>m_view.m_panel.m_panel.m_panel.m_item1_x.m_type.selectedIndex=index;
		void SetPanelBody_Panel_panel_panel_item1_xText(string data)=>UIListener.SetText(m_view.m_panel.m_panel.m_panel.m_item1_x,data);
		string GetPanelBody_Panel_panel_panel_item1_xText()=>UIListener.GetText(m_view.m_panel.m_panel.m_panel.m_item1_x);
		void _OnHunterBullet_Panel_panel_panel_item2_x_typeChanged(EventContext data){
			OnHunterBullet_Panel_panel_panel_item2_x_typeChanged(data);
		}
		partial void OnHunterBullet_Panel_panel_panel_item2_x_typeChanged(EventContext data);
		void SwitchHunterBullet_Panel_panel_panel_item2_x_typePage(int index)=>m_view.m_panel.m_panel.m_panel.m_item2_x.m_type.selectedIndex=index;
		void SetPanelBody_Panel_panel_panel_item2_xText(string data)=>UIListener.SetText(m_view.m_panel.m_panel.m_panel.m_item2_x,data);
		string GetPanelBody_Panel_panel_panel_item2_xText()=>UIListener.GetText(m_view.m_panel.m_panel.m_panel.m_item2_x);
		void _OnHunterBullet_Panel_panel_panel_item3_x_typeChanged(EventContext data){
			OnHunterBullet_Panel_panel_panel_item3_x_typeChanged(data);
		}
		partial void OnHunterBullet_Panel_panel_panel_item3_x_typeChanged(EventContext data);
		void SwitchHunterBullet_Panel_panel_panel_item3_x_typePage(int index)=>m_view.m_panel.m_panel.m_panel.m_item3_x.m_type.selectedIndex=index;
		void SetPanelBody_Panel_panel_panel_item3_xText(string data)=>UIListener.SetText(m_view.m_panel.m_panel.m_panel.m_item3_x,data);
		string GetPanelBody_Panel_panel_panel_item3_xText()=>UIListener.GetText(m_view.m_panel.m_panel.m_panel.m_item3_x);
		void _OnHunterBullet_Panel_panel_panel_item4_x_typeChanged(EventContext data){
			OnHunterBullet_Panel_panel_panel_item4_x_typeChanged(data);
		}
		partial void OnHunterBullet_Panel_panel_panel_item4_x_typeChanged(EventContext data);
		void SwitchHunterBullet_Panel_panel_panel_item4_x_typePage(int index)=>m_view.m_panel.m_panel.m_panel.m_item4_x.m_type.selectedIndex=index;
		void SetPanelBody_Panel_panel_panel_item4_xText(string data)=>UIListener.SetText(m_view.m_panel.m_panel.m_panel.m_item4_x,data);
		string GetPanelBody_Panel_panel_panel_item4_xText()=>UIListener.GetText(m_view.m_panel.m_panel.m_panel.m_item4_x);
		void _OnHunterBullet_Panel_panel_panel_item5_x_typeChanged(EventContext data){
			OnHunterBullet_Panel_panel_panel_item5_x_typeChanged(data);
		}
		partial void OnHunterBullet_Panel_panel_panel_item5_x_typeChanged(EventContext data);
		void SwitchHunterBullet_Panel_panel_panel_item5_x_typePage(int index)=>m_view.m_panel.m_panel.m_panel.m_item5_x.m_type.selectedIndex=index;
		void SetPanelBody_Panel_panel_panel_item5_xText(string data)=>UIListener.SetText(m_view.m_panel.m_panel.m_panel.m_item5_x,data);
		string GetPanelBody_Panel_panel_panel_item5_xText()=>UIListener.GetText(m_view.m_panel.m_panel.m_panel.m_item5_x);
		void _OnHunterBullet_Panel_panel_panel_item6_x_typeChanged(EventContext data){
			OnHunterBullet_Panel_panel_panel_item6_x_typeChanged(data);
		}
		partial void OnHunterBullet_Panel_panel_panel_item6_x_typeChanged(EventContext data);
		void SwitchHunterBullet_Panel_panel_panel_item6_x_typePage(int index)=>m_view.m_panel.m_panel.m_panel.m_item6_x.m_type.selectedIndex=index;
		void SetPanelBody_Panel_panel_panel_item6_xText(string data)=>UIListener.SetText(m_view.m_panel.m_panel.m_panel.m_item6_x,data);
		string GetPanelBody_Panel_panel_panel_item6_xText()=>UIListener.GetText(m_view.m_panel.m_panel.m_panel.m_item6_x);
		void _OnHunterBullet_Panel_panel_panel_item7_x_typeChanged(EventContext data){
			OnHunterBullet_Panel_panel_panel_item7_x_typeChanged(data);
		}
		partial void OnHunterBullet_Panel_panel_panel_item7_x_typeChanged(EventContext data);
		void SwitchHunterBullet_Panel_panel_panel_item7_x_typePage(int index)=>m_view.m_panel.m_panel.m_panel.m_item7_x.m_type.selectedIndex=index;
		void SetPanelBody_Panel_panel_panel_item7_xText(string data)=>UIListener.SetText(m_view.m_panel.m_panel.m_panel.m_item7_x,data);
		string GetPanelBody_Panel_panel_panel_item7_xText()=>UIListener.GetText(m_view.m_panel.m_panel.m_panel.m_item7_x);
		void _OnHunterBullet_Panel_panel_panel_item8_x_typeChanged(EventContext data){
			OnHunterBullet_Panel_panel_panel_item8_x_typeChanged(data);
		}
		partial void OnHunterBullet_Panel_panel_panel_item8_x_typeChanged(EventContext data);
		void SwitchHunterBullet_Panel_panel_panel_item8_x_typePage(int index)=>m_view.m_panel.m_panel.m_panel.m_item8_x.m_type.selectedIndex=index;
		void SetPanelBody_Panel_panel_panel_item8_xText(string data)=>UIListener.SetText(m_view.m_panel.m_panel.m_panel.m_item8_x,data);
		string GetPanelBody_Panel_panel_panel_item8_xText()=>UIListener.GetText(m_view.m_panel.m_panel.m_panel.m_item8_x);
		void _OnHunterBullet_Panel_panel_panel_item9_x_typeChanged(EventContext data){
			OnHunterBullet_Panel_panel_panel_item9_x_typeChanged(data);
		}
		partial void OnHunterBullet_Panel_panel_panel_item9_x_typeChanged(EventContext data);
		void SwitchHunterBullet_Panel_panel_panel_item9_x_typePage(int index)=>m_view.m_panel.m_panel.m_panel.m_item9_x.m_type.selectedIndex=index;
		void SetPanelBody_Panel_panel_panel_item9_xText(string data)=>UIListener.SetText(m_view.m_panel.m_panel.m_panel.m_item9_x,data);
		string GetPanelBody_Panel_panel_panel_item9_xText()=>UIListener.GetText(m_view.m_panel.m_panel.m_panel.m_item9_x);
		void _OnHunterBullet_Panel_panel_panel_item10_x_typeChanged(EventContext data){
			OnHunterBullet_Panel_panel_panel_item10_x_typeChanged(data);
		}
		partial void OnHunterBullet_Panel_panel_panel_item10_x_typeChanged(EventContext data);
		void SwitchHunterBullet_Panel_panel_panel_item10_x_typePage(int index)=>m_view.m_panel.m_panel.m_panel.m_item10_x.m_type.selectedIndex=index;
		void SetPanelBody_Panel_panel_panel_item10_xText(string data)=>UIListener.SetText(m_view.m_panel.m_panel.m_panel.m_item10_x,data);
		string GetPanelBody_Panel_panel_panel_item10_xText()=>UIListener.GetText(m_view.m_panel.m_panel.m_panel.m_item10_x);
		void _OnHunterBullet_Panel_panel_panel_item11_x_typeChanged(EventContext data){
			OnHunterBullet_Panel_panel_panel_item11_x_typeChanged(data);
		}
		partial void OnHunterBullet_Panel_panel_panel_item11_x_typeChanged(EventContext data);
		void SwitchHunterBullet_Panel_panel_panel_item11_x_typePage(int index)=>m_view.m_panel.m_panel.m_panel.m_item11_x.m_type.selectedIndex=index;
		void SetPanelBody_Panel_panel_panel_item11_xText(string data)=>UIListener.SetText(m_view.m_panel.m_panel.m_panel.m_item11_x,data);
		string GetPanelBody_Panel_panel_panel_item11_xText()=>UIListener.GetText(m_view.m_panel.m_panel.m_panel.m_item11_x);
		void _OnSelectPanel_Panel_panel_panelClick(EventContext data){
			OnSelectPanel_Panel_panel_panelClick(data);
		}
		partial void OnSelectPanel_Panel_panel_panelClick(EventContext data);
		void _OnHunterWheel_PanelClick(EventContext data){
			OnHunterWheel_PanelClick(data);
		}
		partial void OnHunterWheel_PanelClick(EventContext data);
		void SetHunterWheel_Panel_panelText(string data)=>UIListener.SetText(m_view.m_panel.m_panel,data);
		string GetHunterWheel_Panel_panelText()=>UIListener.GetText(m_view.m_panel.m_panel);
		void _OnHunterBtn_Panel_playbtn_autoChanged(EventContext data){
			OnHunterBtn_Panel_playbtn_autoChanged(data);
		}
		partial void OnHunterBtn_Panel_playbtn_autoChanged(EventContext data);
		void SwitchHunterBtn_Panel_playbtn_autoPage(int index)=>m_view.m_panel.m_playbtn.m_auto.selectedIndex=index;
		void _OnHunterWheel_PlaybtnClick(EventContext data){
			OnHunterWheel_PlaybtnClick(data);
		}
		partial void OnHunterWheel_PlaybtnClick(EventContext data);
		void SetHunterWheel_Panel_playbtnText(string data)=>UIListener.SetText(m_view.m_panel.m_playbtn,data);
		string GetHunterWheel_Panel_playbtnText()=>UIListener.GetText(m_view.m_panel.m_playbtn);
		void _OnHunterWheel_CurrencyClick(EventContext data){
			OnHunterWheel_CurrencyClick(data);
		}
		partial void OnHunterWheel_CurrencyClick(EventContext data);
		void SetHunterWheel_Panel_currencyText(string data)=>UIListener.SetText(m_view.m_panel.m_currency,data);
		string GetHunterWheel_Panel_currencyText()=>UIListener.GetText(m_view.m_panel.m_currency);
		void _OnHunterWheel_PowerClick(EventContext data){
			OnHunterWheel_PowerClick(data);
		}
		partial void OnHunterWheel_PowerClick(EventContext data);
		void SetHunterWheel_Panel_powerText(string data)=>UIListener.SetText(m_view.m_panel.m_power,data);
		string GetHunterWheel_Panel_powerText()=>UIListener.GetText(m_view.m_panel.m_power);
		void _OnPanelClick(EventContext data){
			OnPanelClick(data);
		}
		partial void OnPanelClick(EventContext data);
		void _OnHunterRewardTips_OpenChanged(EventContext data){
			OnHunterRewardTips_OpenChanged(data);
		}
		partial void OnHunterRewardTips_OpenChanged(EventContext data);
		void SwitchHunterRewardTips_OpenPage(int index)=>m_view.m_reward.m_open.selectedIndex=index;
		void _OnHunterRewardTips_CompletedChanged(EventContext data){
			OnHunterRewardTips_CompletedChanged(data);
		}
		partial void OnHunterRewardTips_CompletedChanged(EventContext data);
		void SwitchHunterRewardTips_CompletedPage(int index)=>m_view.m_reward.m_completed.selectedIndex=index;
		void _OnHunterRewardTips_TypeChanged(EventContext data){
			OnHunterRewardTips_TypeChanged(data);
		}
		partial void OnHunterRewardTips_TypeChanged(EventContext data);
		void SwitchHunterRewardTips_TypePage(int index)=>m_view.m_reward.m_type.selectedIndex=index;
		void _OnRewardClick(EventContext data){
			OnRewardClick(data);
		}
		partial void OnRewardClick(EventContext data);
		void _OnHunterRewardList_SizeChanged(EventContext data){
			OnHunterRewardList_SizeChanged(data);
		}
		partial void OnHunterRewardList_SizeChanged(EventContext data);
		void SwitchHunterRewardList_SizePage(int index)=>m_view.m_rewardsPreview.m_size.selectedIndex=index;
		void _OnRewardsPreviewClick(EventContext data){
			OnRewardsPreviewClick(data);
		}
		partial void OnRewardsPreviewClick(EventContext data);

	}
}
