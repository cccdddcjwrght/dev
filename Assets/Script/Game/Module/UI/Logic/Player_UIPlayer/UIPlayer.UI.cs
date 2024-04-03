﻿
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Player;
	
	public partial class UIPlayer
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			m_view.m_eqTab.onChanged.Add(new EventCallback1(_OnEqTabChanged));
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick));
			m_view.m_EquipPage.m_bg.onChanged.Add(new EventCallback1(_OnEquipPage_BgChanged));
			m_view.m_EquipPage.m_pos.onChanged.Add(new EventCallback1(_OnEquipPage_PosChanged));
			m_view.m_EquipPage.m_model.m_bg.onChanged.Add(new EventCallback1(_Onbgclick_EquipPage_model_bgChanged));
			UIListener.ListenerIcon(m_view.m_EquipPage.m_model, new EventCallback1(_OnEquipPage_ModelClick));
			UIListener.Listener(m_view.m_EquipPage.m_attrbtn, new EventCallback1(_OnEquipPage_AttrbtnClick));
			m_view.m_EquipPage.m_eq5.m_quality.onChanged.Add(new EventCallback1(_OnEqPos_EquipPageq5_qualityChanged));
			m_view.m_EquipPage.m_eq5.m_eq.onChanged.Add(new EventCallback1(_OnEqPos_EquipPageq5qChanged));
			UIListener.Listener(m_view.m_EquipPage.m_eq5, new EventCallback1(_OnEquipPage_Eq5Click));
			m_view.m_EquipPage.m_eq4.m_quality.onChanged.Add(new EventCallback1(_OnEqPos_EquipPageq4_qualityChanged));
			m_view.m_EquipPage.m_eq4.m_eq.onChanged.Add(new EventCallback1(_OnEqPos_EquipPageq4qChanged));
			UIListener.Listener(m_view.m_EquipPage.m_eq4, new EventCallback1(_OnEquipPage_Eq4Click));
			m_view.m_EquipPage.m_eq1.m_quality.onChanged.Add(new EventCallback1(_OnEqPos_EquipPageq1_qualityChanged));
			m_view.m_EquipPage.m_eq1.m_eq.onChanged.Add(new EventCallback1(_OnEqPos_EquipPageq1qChanged));
			UIListener.Listener(m_view.m_EquipPage.m_eq1, new EventCallback1(_OnEquipPage_Eq1Click));
			m_view.m_EquipPage.m_eq2.m_quality.onChanged.Add(new EventCallback1(_OnEqPos_EquipPageq2_qualityChanged));
			m_view.m_EquipPage.m_eq2.m_eq.onChanged.Add(new EventCallback1(_OnEqPos_EquipPageq2qChanged));
			UIListener.Listener(m_view.m_EquipPage.m_eq2, new EventCallback1(_OnEquipPage_Eq2Click));
			m_view.m_EquipPage.m_eq3.m_quality.onChanged.Add(new EventCallback1(_OnEqPos_EquipPageq3_qualityChanged));
			m_view.m_EquipPage.m_eq3.m_eq.onChanged.Add(new EventCallback1(_OnEqPos_EquipPageq3qChanged));
			UIListener.Listener(m_view.m_EquipPage.m_eq3, new EventCallback1(_OnEquipPage_Eq3Click));
			UIListener.ListenerIcon(m_view.m_EquipPage, new EventCallback1(_OnEquipPageClick));
			m_view.m_EquipQuality.m_state.onChanged.Add(new EventCallback1(_OnEquipUpQuality_StateChanged));
			m_view.m_EquipQuality.m_selecteq.m_type.onChanged.Add(new EventCallback1(_OnEquip_EquipQuality_selecteq_typeChanged));
			m_view.m_EquipQuality.m_selecteq.m_quality.onChanged.Add(new EventCallback1(_OnEquip_EquipQuality_selecteq_qualityChanged));
			UIListener.Listener(m_view.m_EquipQuality.m_selecteq, new EventCallback1(_OnEquipUpQuality_SelecteqClick));
			m_view.m_EquipQuality.m_nexteq.m_type.onChanged.Add(new EventCallback1(_OnEquip_EquipQuality_nexteq_typeChanged));
			m_view.m_EquipQuality.m_nexteq.m_quality.onChanged.Add(new EventCallback1(_OnEquip_EquipQuality_nexteq_qualityChanged));
			UIListener.Listener(m_view.m_EquipQuality.m_nexteq, new EventCallback1(_OnEquipUpQuality_NexteqClick));
			UIListener.Listener(m_view.m_EquipQuality.m_cleareq, new EventCallback1(_OnEquipUpQuality_CleareqClick));
			UIListener.Listener(m_view.m_EquipQuality.m_click, new EventCallback1(_OnEquipUpQuality_ClickClick));
			UIListener.ListenerIcon(m_view.m_EquipQuality, new EventCallback1(_OnEquipQualityClick));
			UIListener.Listener(m_view.m_clickBtn, new EventCallback1(_OnClickBtnClick));

		}
		partial void UnInitUI(UIContext context){
			m_view.m_eqTab.onChanged.Remove(new EventCallback1(_OnEqTabChanged));
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick),remove:true);
			m_view.m_EquipPage.m_bg.onChanged.Remove(new EventCallback1(_OnEquipPage_BgChanged));
			m_view.m_EquipPage.m_pos.onChanged.Remove(new EventCallback1(_OnEquipPage_PosChanged));
			m_view.m_EquipPage.m_model.m_bg.onChanged.Remove(new EventCallback1(_Onbgclick_EquipPage_model_bgChanged));
			UIListener.ListenerIcon(m_view.m_EquipPage.m_model, new EventCallback1(_OnEquipPage_ModelClick),remove:true);
			UIListener.Listener(m_view.m_EquipPage.m_attrbtn, new EventCallback1(_OnEquipPage_AttrbtnClick),remove:true);
			m_view.m_EquipPage.m_eq5.m_quality.onChanged.Remove(new EventCallback1(_OnEqPos_EquipPageq5_qualityChanged));
			m_view.m_EquipPage.m_eq5.m_eq.onChanged.Remove(new EventCallback1(_OnEqPos_EquipPageq5qChanged));
			UIListener.Listener(m_view.m_EquipPage.m_eq5, new EventCallback1(_OnEquipPage_Eq5Click),remove:true);
			m_view.m_EquipPage.m_eq4.m_quality.onChanged.Remove(new EventCallback1(_OnEqPos_EquipPageq4_qualityChanged));
			m_view.m_EquipPage.m_eq4.m_eq.onChanged.Remove(new EventCallback1(_OnEqPos_EquipPageq4qChanged));
			UIListener.Listener(m_view.m_EquipPage.m_eq4, new EventCallback1(_OnEquipPage_Eq4Click),remove:true);
			m_view.m_EquipPage.m_eq1.m_quality.onChanged.Remove(new EventCallback1(_OnEqPos_EquipPageq1_qualityChanged));
			m_view.m_EquipPage.m_eq1.m_eq.onChanged.Remove(new EventCallback1(_OnEqPos_EquipPageq1qChanged));
			UIListener.Listener(m_view.m_EquipPage.m_eq1, new EventCallback1(_OnEquipPage_Eq1Click),remove:true);
			m_view.m_EquipPage.m_eq2.m_quality.onChanged.Remove(new EventCallback1(_OnEqPos_EquipPageq2_qualityChanged));
			m_view.m_EquipPage.m_eq2.m_eq.onChanged.Remove(new EventCallback1(_OnEqPos_EquipPageq2qChanged));
			UIListener.Listener(m_view.m_EquipPage.m_eq2, new EventCallback1(_OnEquipPage_Eq2Click),remove:true);
			m_view.m_EquipPage.m_eq3.m_quality.onChanged.Remove(new EventCallback1(_OnEqPos_EquipPageq3_qualityChanged));
			m_view.m_EquipPage.m_eq3.m_eq.onChanged.Remove(new EventCallback1(_OnEqPos_EquipPageq3qChanged));
			UIListener.Listener(m_view.m_EquipPage.m_eq3, new EventCallback1(_OnEquipPage_Eq3Click),remove:true);
			UIListener.ListenerIcon(m_view.m_EquipPage, new EventCallback1(_OnEquipPageClick),remove:true);
			m_view.m_EquipQuality.m_state.onChanged.Remove(new EventCallback1(_OnEquipUpQuality_StateChanged));
			m_view.m_EquipQuality.m_selecteq.m_type.onChanged.Remove(new EventCallback1(_OnEquip_EquipQuality_selecteq_typeChanged));
			m_view.m_EquipQuality.m_selecteq.m_quality.onChanged.Remove(new EventCallback1(_OnEquip_EquipQuality_selecteq_qualityChanged));
			UIListener.Listener(m_view.m_EquipQuality.m_selecteq, new EventCallback1(_OnEquipUpQuality_SelecteqClick),remove:true);
			m_view.m_EquipQuality.m_nexteq.m_type.onChanged.Remove(new EventCallback1(_OnEquip_EquipQuality_nexteq_typeChanged));
			m_view.m_EquipQuality.m_nexteq.m_quality.onChanged.Remove(new EventCallback1(_OnEquip_EquipQuality_nexteq_qualityChanged));
			UIListener.Listener(m_view.m_EquipQuality.m_nexteq, new EventCallback1(_OnEquipUpQuality_NexteqClick),remove:true);
			UIListener.Listener(m_view.m_EquipQuality.m_cleareq, new EventCallback1(_OnEquipUpQuality_CleareqClick),remove:true);
			UIListener.Listener(m_view.m_EquipQuality.m_click, new EventCallback1(_OnEquipUpQuality_ClickClick),remove:true);
			UIListener.ListenerIcon(m_view.m_EquipQuality, new EventCallback1(_OnEquipQualityClick),remove:true);
			UIListener.Listener(m_view.m_clickBtn, new EventCallback1(_OnClickBtnClick),remove:true);

		}
		void _OnEqTabChanged(EventContext data){
			OnEqTabChanged(data);
		}
		partial void OnEqTabChanged(EventContext data);
		void SwitchEqTabPage(int index)=>m_view.m_eqTab.selectedIndex=index;
		void DoCloseUIClick(EventContext data){
			 bool __closestate = true;
			 OnUICloseClick(ref __closestate);
			 if(__closestate)SGame.UIUtils.CloseUIByID(__id);
			 
		}
		partial void OnUICloseClick(ref bool state);
		void SetBodyText(string data)=>UIListener.SetText(m_view.m_body,data);
		string GetBodyText()=>UIListener.GetText(m_view.m_body);
		void _OnEquipPage_BgChanged(EventContext data){
			OnEquipPage_BgChanged(data);
		}
		partial void OnEquipPage_BgChanged(EventContext data);
		void SwitchEquipPage_BgPage(int index)=>m_view.m_EquipPage.m_bg.selectedIndex=index;
		void _OnEquipPage_PosChanged(EventContext data){
			OnEquipPage_PosChanged(data);
		}
		partial void OnEquipPage_PosChanged(EventContext data);
		void SwitchEquipPage_PosPage(int index)=>m_view.m_EquipPage.m_pos.selectedIndex=index;
		void _Onbgclick_EquipPage_model_bgChanged(EventContext data){
			Onbgclick_EquipPage_model_bgChanged(data);
		}
		partial void Onbgclick_EquipPage_model_bgChanged(EventContext data);
		void Switchbgclick_EquipPage_model_bgPage(int index)=>m_view.m_EquipPage.m_model.m_bg.selectedIndex=index;
		void _OnEquipPage_ModelClick(EventContext data){
			OnEquipPage_ModelClick(data);
		}
		partial void OnEquipPage_ModelClick(EventContext data);
		void _OnEquipPage_AttrbtnClick(EventContext data){
			OnEquipPage_AttrbtnClick(data);
		}
		partial void OnEquipPage_AttrbtnClick(EventContext data);
		void SetEquipPage_EquipPage_attrbtnText(string data)=>UIListener.SetText(m_view.m_EquipPage.m_attrbtn,data);
		string GetEquipPage_EquipPage_attrbtnText()=>UIListener.GetText(m_view.m_EquipPage.m_attrbtn);
		void SetEquipPage_AttrText(string data)=>UIListener.SetText(m_view.m_EquipPage.m_attr,data);
		string GetEquipPage_AttrText()=>UIListener.GetText(m_view.m_EquipPage.m_attr);
		void _OnEqPos_EquipPageq5_qualityChanged(EventContext data){
			OnEqPos_EquipPageq5_qualityChanged(data);
		}
		partial void OnEqPos_EquipPageq5_qualityChanged(EventContext data);
		void SwitchEqPos_EquipPageq5_qualityPage(int index)=>m_view.m_EquipPage.m_eq5.m_quality.selectedIndex=index;
		void _OnEqPos_EquipPageq5qChanged(EventContext data){
			OnEqPos_EquipPageq5qChanged(data);
		}
		partial void OnEqPos_EquipPageq5qChanged(EventContext data);
		void SwitchEqPos_EquipPageq5qPage(int index)=>m_view.m_EquipPage.m_eq5.m_eq.selectedIndex=index;
		void SetEqPos_EquipPageq5_levelText(string data)=>UIListener.SetText(m_view.m_EquipPage.m_eq5.m_level,data);
		string GetEqPos_EquipPageq5_levelText()=>UIListener.GetText(m_view.m_EquipPage.m_eq5.m_level);
		void _OnEquipPage_Eq5Click(EventContext data){
			OnEquipPage_Eq5Click(data);
		}
		partial void OnEquipPage_Eq5Click(EventContext data);
		void SetEquipPage_EquipPageq5Text(string data)=>UIListener.SetText(m_view.m_EquipPage.m_eq5,data);
		string GetEquipPage_EquipPageq5Text()=>UIListener.GetText(m_view.m_EquipPage.m_eq5);
		void _OnEqPos_EquipPageq4_qualityChanged(EventContext data){
			OnEqPos_EquipPageq4_qualityChanged(data);
		}
		partial void OnEqPos_EquipPageq4_qualityChanged(EventContext data);
		void SwitchEqPos_EquipPageq4_qualityPage(int index)=>m_view.m_EquipPage.m_eq4.m_quality.selectedIndex=index;
		void _OnEqPos_EquipPageq4qChanged(EventContext data){
			OnEqPos_EquipPageq4qChanged(data);
		}
		partial void OnEqPos_EquipPageq4qChanged(EventContext data);
		void SwitchEqPos_EquipPageq4qPage(int index)=>m_view.m_EquipPage.m_eq4.m_eq.selectedIndex=index;
		void SetEqPos_EquipPageq4_levelText(string data)=>UIListener.SetText(m_view.m_EquipPage.m_eq4.m_level,data);
		string GetEqPos_EquipPageq4_levelText()=>UIListener.GetText(m_view.m_EquipPage.m_eq4.m_level);
		void _OnEquipPage_Eq4Click(EventContext data){
			OnEquipPage_Eq4Click(data);
		}
		partial void OnEquipPage_Eq4Click(EventContext data);
		void SetEquipPage_EquipPageq4Text(string data)=>UIListener.SetText(m_view.m_EquipPage.m_eq4,data);
		string GetEquipPage_EquipPageq4Text()=>UIListener.GetText(m_view.m_EquipPage.m_eq4);
		void _OnEqPos_EquipPageq1_qualityChanged(EventContext data){
			OnEqPos_EquipPageq1_qualityChanged(data);
		}
		partial void OnEqPos_EquipPageq1_qualityChanged(EventContext data);
		void SwitchEqPos_EquipPageq1_qualityPage(int index)=>m_view.m_EquipPage.m_eq1.m_quality.selectedIndex=index;
		void _OnEqPos_EquipPageq1qChanged(EventContext data){
			OnEqPos_EquipPageq1qChanged(data);
		}
		partial void OnEqPos_EquipPageq1qChanged(EventContext data);
		void SwitchEqPos_EquipPageq1qPage(int index)=>m_view.m_EquipPage.m_eq1.m_eq.selectedIndex=index;
		void SetEqPos_EquipPageq1_levelText(string data)=>UIListener.SetText(m_view.m_EquipPage.m_eq1.m_level,data);
		string GetEqPos_EquipPageq1_levelText()=>UIListener.GetText(m_view.m_EquipPage.m_eq1.m_level);
		void _OnEquipPage_Eq1Click(EventContext data){
			OnEquipPage_Eq1Click(data);
		}
		partial void OnEquipPage_Eq1Click(EventContext data);
		void SetEquipPage_EquipPageq1Text(string data)=>UIListener.SetText(m_view.m_EquipPage.m_eq1,data);
		string GetEquipPage_EquipPageq1Text()=>UIListener.GetText(m_view.m_EquipPage.m_eq1);
		void _OnEqPos_EquipPageq2_qualityChanged(EventContext data){
			OnEqPos_EquipPageq2_qualityChanged(data);
		}
		partial void OnEqPos_EquipPageq2_qualityChanged(EventContext data);
		void SwitchEqPos_EquipPageq2_qualityPage(int index)=>m_view.m_EquipPage.m_eq2.m_quality.selectedIndex=index;
		void _OnEqPos_EquipPageq2qChanged(EventContext data){
			OnEqPos_EquipPageq2qChanged(data);
		}
		partial void OnEqPos_EquipPageq2qChanged(EventContext data);
		void SwitchEqPos_EquipPageq2qPage(int index)=>m_view.m_EquipPage.m_eq2.m_eq.selectedIndex=index;
		void SetEqPos_EquipPageq2_levelText(string data)=>UIListener.SetText(m_view.m_EquipPage.m_eq2.m_level,data);
		string GetEqPos_EquipPageq2_levelText()=>UIListener.GetText(m_view.m_EquipPage.m_eq2.m_level);
		void _OnEquipPage_Eq2Click(EventContext data){
			OnEquipPage_Eq2Click(data);
		}
		partial void OnEquipPage_Eq2Click(EventContext data);
		void SetEquipPage_EquipPageq2Text(string data)=>UIListener.SetText(m_view.m_EquipPage.m_eq2,data);
		string GetEquipPage_EquipPageq2Text()=>UIListener.GetText(m_view.m_EquipPage.m_eq2);
		void _OnEqPos_EquipPageq3_qualityChanged(EventContext data){
			OnEqPos_EquipPageq3_qualityChanged(data);
		}
		partial void OnEqPos_EquipPageq3_qualityChanged(EventContext data);
		void SwitchEqPos_EquipPageq3_qualityPage(int index)=>m_view.m_EquipPage.m_eq3.m_quality.selectedIndex=index;
		void _OnEqPos_EquipPageq3qChanged(EventContext data){
			OnEqPos_EquipPageq3qChanged(data);
		}
		partial void OnEqPos_EquipPageq3qChanged(EventContext data);
		void SwitchEqPos_EquipPageq3qPage(int index)=>m_view.m_EquipPage.m_eq3.m_eq.selectedIndex=index;
		void SetEqPos_EquipPageq3_levelText(string data)=>UIListener.SetText(m_view.m_EquipPage.m_eq3.m_level,data);
		string GetEqPos_EquipPageq3_levelText()=>UIListener.GetText(m_view.m_EquipPage.m_eq3.m_level);
		void _OnEquipPage_Eq3Click(EventContext data){
			OnEquipPage_Eq3Click(data);
		}
		partial void OnEquipPage_Eq3Click(EventContext data);
		void SetEquipPage_EquipPageq3Text(string data)=>UIListener.SetText(m_view.m_EquipPage.m_eq3,data);
		string GetEquipPage_EquipPageq3Text()=>UIListener.GetText(m_view.m_EquipPage.m_eq3);
		void _OnEquipPageClick(EventContext data){
			OnEquipPageClick(data);
		}
		partial void OnEquipPageClick(EventContext data);
		void _OnEquipUpQuality_StateChanged(EventContext data){
			OnEquipUpQuality_StateChanged(data);
		}
		partial void OnEquipUpQuality_StateChanged(EventContext data);
		void SwitchEquipUpQuality_StatePage(int index)=>m_view.m_EquipQuality.m_state.selectedIndex=index;
		void SetEquipUpQuality_ProgressValue(float data)=>UIListener.SetValue(m_view.m_EquipQuality.m_progress,data);
		float GetEquipUpQuality_ProgressValue()=>UIListener.GetValue(m_view.m_EquipQuality.m_progress);
		void SetEquipUpQuality_EquipQuality_progressText(string data)=>UIListener.SetText(m_view.m_EquipQuality.m_progress,data);
		string GetEquipUpQuality_EquipQuality_progressText()=>UIListener.GetText(m_view.m_EquipQuality.m_progress);
		void _OnEquip_EquipQuality_selecteq_typeChanged(EventContext data){
			OnEquip_EquipQuality_selecteq_typeChanged(data);
		}
		partial void OnEquip_EquipQuality_selecteq_typeChanged(EventContext data);
		void SwitchEquip_EquipQuality_selecteq_typePage(int index)=>m_view.m_EquipQuality.m_selecteq.m_type.selectedIndex=index;
		void _OnEquip_EquipQuality_selecteq_qualityChanged(EventContext data){
			OnEquip_EquipQuality_selecteq_qualityChanged(data);
		}
		partial void OnEquip_EquipQuality_selecteq_qualityChanged(EventContext data);
		void SwitchEquip_EquipQuality_selecteq_qualityPage(int index)=>m_view.m_EquipQuality.m_selecteq.m_quality.selectedIndex=index;
		void _OnEquipUpQuality_SelecteqClick(EventContext data){
			OnEquipUpQuality_SelecteqClick(data);
		}
		partial void OnEquipUpQuality_SelecteqClick(EventContext data);
		void SetEquipUpQuality_EquipQuality_selecteqText(string data)=>UIListener.SetText(m_view.m_EquipQuality.m_selecteq,data);
		string GetEquipUpQuality_EquipQuality_selecteqText()=>UIListener.GetText(m_view.m_EquipQuality.m_selecteq);
		void _OnEquip_EquipQuality_nexteq_typeChanged(EventContext data){
			OnEquip_EquipQuality_nexteq_typeChanged(data);
		}
		partial void OnEquip_EquipQuality_nexteq_typeChanged(EventContext data);
		void SwitchEquip_EquipQuality_nexteq_typePage(int index)=>m_view.m_EquipQuality.m_nexteq.m_type.selectedIndex=index;
		void _OnEquip_EquipQuality_nexteq_qualityChanged(EventContext data){
			OnEquip_EquipQuality_nexteq_qualityChanged(data);
		}
		partial void OnEquip_EquipQuality_nexteq_qualityChanged(EventContext data);
		void SwitchEquip_EquipQuality_nexteq_qualityPage(int index)=>m_view.m_EquipQuality.m_nexteq.m_quality.selectedIndex=index;
		void _OnEquipUpQuality_NexteqClick(EventContext data){
			OnEquipUpQuality_NexteqClick(data);
		}
		partial void OnEquipUpQuality_NexteqClick(EventContext data);
		void SetEquipUpQuality_EquipQuality_nexteqText(string data)=>UIListener.SetText(m_view.m_EquipQuality.m_nexteq,data);
		string GetEquipUpQuality_EquipQuality_nexteqText()=>UIListener.GetText(m_view.m_EquipQuality.m_nexteq);
		void _OnEquipUpQuality_CleareqClick(EventContext data){
			OnEquipUpQuality_CleareqClick(data);
		}
		partial void OnEquipUpQuality_CleareqClick(EventContext data);
		void SetEquipUpQuality_EquipQuality_cleareqText(string data)=>UIListener.SetText(m_view.m_EquipQuality.m_cleareq,data);
		string GetEquipUpQuality_EquipQuality_cleareqText()=>UIListener.GetText(m_view.m_EquipQuality.m_cleareq);
		void _OnEquipUpQuality_ClickClick(EventContext data){
			OnEquipUpQuality_ClickClick(data);
		}
		partial void OnEquipUpQuality_ClickClick(EventContext data);
		void SetEquipUpQuality_EquipQuality_clickText(string data)=>UIListener.SetText(m_view.m_EquipQuality.m_click,data);
		string GetEquipUpQuality_EquipQuality_clickText()=>UIListener.GetText(m_view.m_EquipQuality.m_click);
		void SetEquipUpQuality_NextattrText(string data)=>UIListener.SetText(m_view.m_EquipQuality.m_nextattr,data);
		string GetEquipUpQuality_NextattrText()=>UIListener.GetText(m_view.m_EquipQuality.m_nextattr);
		void _OnEquipQualityClick(EventContext data){
			OnEquipQualityClick(data);
		}
		partial void OnEquipQualityClick(EventContext data);
		void _OnClickBtnClick(EventContext data){
			OnClickBtnClick(data);
		}
		partial void OnClickBtnClick(EventContext data);

	}
}
