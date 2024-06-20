
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
			m_view.m_c1.onChanged.Add(new EventCallback1(_OnC1Changed));
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick));
			m_view.m_EquipPage.m_bg.onChanged.Add(new EventCallback1(_OnEquipPage_BgChanged));
			m_view.m_EquipPage.m_pos.onChanged.Add(new EventCallback1(_OnEquipPage_PosChanged));
			m_view.m_EquipPage.m_model.m_bg.onChanged.Add(new EventCallback1(_Onbgclick_EquipPage_model_bgChanged));
			UIListener.ListenerIcon(m_view.m_EquipPage.m_model, new EventCallback1(_OnEquipPage_ModelClick));
			UIListener.Listener(m_view.m_EquipPage.m_attrbtn, new EventCallback1(_OnEquipPage_AttrbtnClick));
			m_view.m_EquipPage.m_eq1.m_state.onChanged.Add(new EventCallback1(_OnEqPos_EquipPageq1_stateChanged));
			m_view.m_EquipPage.m_eq1.m_body.m_type.onChanged.Add(new EventCallback1(_OnEquip_EquipPageq1_body_typeChanged));
			m_view.m_EquipPage.m_eq1.m_body.m_quality.onChanged.Add(new EventCallback1(_OnEquip_EquipPageq1_body_qualityChanged));
			UIListener.ListenerClose(m_view.m_EquipPage.m_eq1.m_body, new EventCallback1(DoCloseUIClick));
			UIListener.Listener(m_view.m_EquipPage.m_eq1, new EventCallback1(_OnEquipPage_Eq1Click));
			m_view.m_EquipPage.m_eq2.m_state.onChanged.Add(new EventCallback1(_OnEqPos_EquipPageq2_stateChanged));
			m_view.m_EquipPage.m_eq2.m_body.m_type.onChanged.Add(new EventCallback1(_OnEquip_EquipPageq2_body_typeChanged));
			m_view.m_EquipPage.m_eq2.m_body.m_quality.onChanged.Add(new EventCallback1(_OnEquip_EquipPageq2_body_qualityChanged));
			UIListener.ListenerClose(m_view.m_EquipPage.m_eq2.m_body, new EventCallback1(DoCloseUIClick));
			UIListener.Listener(m_view.m_EquipPage.m_eq2, new EventCallback1(_OnEquipPage_Eq2Click));
			m_view.m_EquipPage.m_eq3.m_state.onChanged.Add(new EventCallback1(_OnEqPos_EquipPageq3_stateChanged));
			m_view.m_EquipPage.m_eq3.m_body.m_type.onChanged.Add(new EventCallback1(_OnEquip_EquipPageq3_body_typeChanged));
			m_view.m_EquipPage.m_eq3.m_body.m_quality.onChanged.Add(new EventCallback1(_OnEquip_EquipPageq3_body_qualityChanged));
			UIListener.ListenerClose(m_view.m_EquipPage.m_eq3.m_body, new EventCallback1(DoCloseUIClick));
			UIListener.Listener(m_view.m_EquipPage.m_eq3, new EventCallback1(_OnEquipPage_Eq3Click));
			m_view.m_EquipPage.m_eq5.m_type.onChanged.Add(new EventCallback1(_OnEquip_EquipPageq5_typeChanged));
			m_view.m_EquipPage.m_eq5.m_quality.onChanged.Add(new EventCallback1(_OnEquip_EquipPageq5_qualityChanged));
			UIListener.Listener(m_view.m_EquipPage.m_eq5, new EventCallback1(_OnEquipPage_Eq5Click));
			m_view.m_EquipPage.m_eq6.m_type.onChanged.Add(new EventCallback1(_OnEquip_EquipPageq6_typeChanged));
			m_view.m_EquipPage.m_eq6.m_quality.onChanged.Add(new EventCallback1(_OnEquip_EquipPageq6_qualityChanged));
			UIListener.Listener(m_view.m_EquipPage.m_eq6, new EventCallback1(_OnEquipPage_Eq6Click));
			m_view.m_EquipPage.m_eq4.m_type.onChanged.Add(new EventCallback1(_OnEquip_EquipPageq4_typeChanged));
			m_view.m_EquipPage.m_eq4.m_quality.onChanged.Add(new EventCallback1(_OnEquip_EquipPageq4_qualityChanged));
			UIListener.Listener(m_view.m_EquipPage.m_eq4, new EventCallback1(_OnEquipPage_Eq4Click));
			UIListener.ListenerIcon(m_view.m_EquipPage, new EventCallback1(_OnEquipPageClick));
			m_view.m_EquipQuality.m_state.onChanged.Add(new EventCallback1(_OnEquipUpQuality_StateChanged));
			m_view.m_EquipQuality.m_type.onChanged.Add(new EventCallback1(_OnEquipUpQuality_TypeChanged));
			m_view.m_EquipQuality.m_combine.onChanged.Add(new EventCallback1(_OnEquipUpQuality_CombineChanged));
			m_view.m_EquipQuality.m_nexteq.m_type.onChanged.Add(new EventCallback1(_OnEquip_EquipQuality_nexteq_typeChanged));
			m_view.m_EquipQuality.m_nexteq.m_quality.onChanged.Add(new EventCallback1(_OnEquip_EquipQuality_nexteq_qualityChanged));
			UIListener.Listener(m_view.m_EquipQuality.m_nexteq, new EventCallback1(_OnEquipUpQuality_NexteqClick));
			UIListener.Listener(m_view.m_EquipQuality.m_click, new EventCallback1(_OnEquipUpQuality_ClickClick));
			UIListener.Listener(m_view.m_EquipQuality.m_merge, new EventCallback1(_OnEquipUpQuality_MergeClick));
			UIListener.ListenerIcon(m_view.m_EquipQuality, new EventCallback1(_OnEquipQualityClick));
			UIListener.Listener(m_view.m_info, new EventCallback1(_OnInfoClick));
			UIListener.Listener(m_view.m_equipup, new EventCallback1(_OnEquipupClick));
			UIListener.Listener(m_view.m_clickBtn, new EventCallback1(_OnClickBtnClick));

		}
		partial void UnInitUI(UIContext context){
			m_view.m_eqTab.onChanged.Remove(new EventCallback1(_OnEqTabChanged));
			m_view.m_c1.onChanged.Remove(new EventCallback1(_OnC1Changed));
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick),remove:true);
			m_view.m_EquipPage.m_bg.onChanged.Remove(new EventCallback1(_OnEquipPage_BgChanged));
			m_view.m_EquipPage.m_pos.onChanged.Remove(new EventCallback1(_OnEquipPage_PosChanged));
			m_view.m_EquipPage.m_model.m_bg.onChanged.Remove(new EventCallback1(_Onbgclick_EquipPage_model_bgChanged));
			UIListener.ListenerIcon(m_view.m_EquipPage.m_model, new EventCallback1(_OnEquipPage_ModelClick),remove:true);
			UIListener.Listener(m_view.m_EquipPage.m_attrbtn, new EventCallback1(_OnEquipPage_AttrbtnClick),remove:true);
			m_view.m_EquipPage.m_eq1.m_state.onChanged.Remove(new EventCallback1(_OnEqPos_EquipPageq1_stateChanged));
			m_view.m_EquipPage.m_eq1.m_body.m_type.onChanged.Remove(new EventCallback1(_OnEquip_EquipPageq1_body_typeChanged));
			m_view.m_EquipPage.m_eq1.m_body.m_quality.onChanged.Remove(new EventCallback1(_OnEquip_EquipPageq1_body_qualityChanged));
			UIListener.ListenerClose(m_view.m_EquipPage.m_eq1.m_body, new EventCallback1(DoCloseUIClick),remove:true);
			UIListener.Listener(m_view.m_EquipPage.m_eq1, new EventCallback1(_OnEquipPage_Eq1Click),remove:true);
			m_view.m_EquipPage.m_eq2.m_state.onChanged.Remove(new EventCallback1(_OnEqPos_EquipPageq2_stateChanged));
			m_view.m_EquipPage.m_eq2.m_body.m_type.onChanged.Remove(new EventCallback1(_OnEquip_EquipPageq2_body_typeChanged));
			m_view.m_EquipPage.m_eq2.m_body.m_quality.onChanged.Remove(new EventCallback1(_OnEquip_EquipPageq2_body_qualityChanged));
			UIListener.ListenerClose(m_view.m_EquipPage.m_eq2.m_body, new EventCallback1(DoCloseUIClick),remove:true);
			UIListener.Listener(m_view.m_EquipPage.m_eq2, new EventCallback1(_OnEquipPage_Eq2Click),remove:true);
			m_view.m_EquipPage.m_eq3.m_state.onChanged.Remove(new EventCallback1(_OnEqPos_EquipPageq3_stateChanged));
			m_view.m_EquipPage.m_eq3.m_body.m_type.onChanged.Remove(new EventCallback1(_OnEquip_EquipPageq3_body_typeChanged));
			m_view.m_EquipPage.m_eq3.m_body.m_quality.onChanged.Remove(new EventCallback1(_OnEquip_EquipPageq3_body_qualityChanged));
			UIListener.ListenerClose(m_view.m_EquipPage.m_eq3.m_body, new EventCallback1(DoCloseUIClick),remove:true);
			UIListener.Listener(m_view.m_EquipPage.m_eq3, new EventCallback1(_OnEquipPage_Eq3Click),remove:true);
			m_view.m_EquipPage.m_eq5.m_type.onChanged.Remove(new EventCallback1(_OnEquip_EquipPageq5_typeChanged));
			m_view.m_EquipPage.m_eq5.m_quality.onChanged.Remove(new EventCallback1(_OnEquip_EquipPageq5_qualityChanged));
			UIListener.Listener(m_view.m_EquipPage.m_eq5, new EventCallback1(_OnEquipPage_Eq5Click),remove:true);
			m_view.m_EquipPage.m_eq6.m_type.onChanged.Remove(new EventCallback1(_OnEquip_EquipPageq6_typeChanged));
			m_view.m_EquipPage.m_eq6.m_quality.onChanged.Remove(new EventCallback1(_OnEquip_EquipPageq6_qualityChanged));
			UIListener.Listener(m_view.m_EquipPage.m_eq6, new EventCallback1(_OnEquipPage_Eq6Click),remove:true);
			m_view.m_EquipPage.m_eq4.m_type.onChanged.Remove(new EventCallback1(_OnEquip_EquipPageq4_typeChanged));
			m_view.m_EquipPage.m_eq4.m_quality.onChanged.Remove(new EventCallback1(_OnEquip_EquipPageq4_qualityChanged));
			UIListener.Listener(m_view.m_EquipPage.m_eq4, new EventCallback1(_OnEquipPage_Eq4Click),remove:true);
			UIListener.ListenerIcon(m_view.m_EquipPage, new EventCallback1(_OnEquipPageClick),remove:true);
			m_view.m_EquipQuality.m_state.onChanged.Remove(new EventCallback1(_OnEquipUpQuality_StateChanged));
			m_view.m_EquipQuality.m_type.onChanged.Remove(new EventCallback1(_OnEquipUpQuality_TypeChanged));
			m_view.m_EquipQuality.m_combine.onChanged.Remove(new EventCallback1(_OnEquipUpQuality_CombineChanged));
			m_view.m_EquipQuality.m_nexteq.m_type.onChanged.Remove(new EventCallback1(_OnEquip_EquipQuality_nexteq_typeChanged));
			m_view.m_EquipQuality.m_nexteq.m_quality.onChanged.Remove(new EventCallback1(_OnEquip_EquipQuality_nexteq_qualityChanged));
			UIListener.Listener(m_view.m_EquipQuality.m_nexteq, new EventCallback1(_OnEquipUpQuality_NexteqClick),remove:true);
			UIListener.Listener(m_view.m_EquipQuality.m_click, new EventCallback1(_OnEquipUpQuality_ClickClick),remove:true);
			UIListener.Listener(m_view.m_EquipQuality.m_merge, new EventCallback1(_OnEquipUpQuality_MergeClick),remove:true);
			UIListener.ListenerIcon(m_view.m_EquipQuality, new EventCallback1(_OnEquipQualityClick),remove:true);
			UIListener.Listener(m_view.m_info, new EventCallback1(_OnInfoClick),remove:true);
			UIListener.Listener(m_view.m_equipup, new EventCallback1(_OnEquipupClick),remove:true);
			UIListener.Listener(m_view.m_clickBtn, new EventCallback1(_OnClickBtnClick),remove:true);

		}
		void _OnEqTabChanged(EventContext data){
			OnEqTabChanged(data);
		}
		partial void OnEqTabChanged(EventContext data);
		void SwitchEqTabPage(int index)=>m_view.m_eqTab.selectedIndex=index;
		void _OnC1Changed(EventContext data){
			OnC1Changed(data);
		}
		partial void OnC1Changed(EventContext data);
		void SwitchC1Page(int index)=>m_view.m_c1.selectedIndex=index;
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
		void SetEquipPage_EquipPage_modelText(string data)=>UIListener.SetText(m_view.m_EquipPage.m_model,data);
		string GetEquipPage_EquipPage_modelText()=>UIListener.GetText(m_view.m_EquipPage.m_model);
		void _OnEquipPage_AttrbtnClick(EventContext data){
			OnEquipPage_AttrbtnClick(data);
		}
		partial void OnEquipPage_AttrbtnClick(EventContext data);
		void SetEquipPage_EquipPage_attrbtnText(string data)=>UIListener.SetText(m_view.m_EquipPage.m_attrbtn,data);
		string GetEquipPage_EquipPage_attrbtnText()=>UIListener.GetText(m_view.m_EquipPage.m_attrbtn);
		void SetEquipPage_AttrText(string data)=>UIListener.SetText(m_view.m_EquipPage.m_attr,data);
		string GetEquipPage_AttrText()=>UIListener.GetText(m_view.m_EquipPage.m_attr);
		void _OnEqPos_EquipPageq1_stateChanged(EventContext data){
			OnEqPos_EquipPageq1_stateChanged(data);
		}
		partial void OnEqPos_EquipPageq1_stateChanged(EventContext data);
		void SwitchEqPos_EquipPageq1_statePage(int index)=>m_view.m_EquipPage.m_eq1.m_state.selectedIndex=index;
		void _OnEquip_EquipPageq1_body_typeChanged(EventContext data){
			OnEquip_EquipPageq1_body_typeChanged(data);
		}
		partial void OnEquip_EquipPageq1_body_typeChanged(EventContext data);
		void SwitchEquip_EquipPageq1_body_typePage(int index)=>m_view.m_EquipPage.m_eq1.m_body.m_type.selectedIndex=index;
		void _OnEquip_EquipPageq1_body_qualityChanged(EventContext data){
			OnEquip_EquipPageq1_body_qualityChanged(data);
		}
		partial void OnEquip_EquipPageq1_body_qualityChanged(EventContext data);
		void SwitchEquip_EquipPageq1_body_qualityPage(int index)=>m_view.m_EquipPage.m_eq1.m_body.m_quality.selectedIndex=index;
		void SetEqPos_EquipPageq1_bodyText(string data)=>UIListener.SetText(m_view.m_EquipPage.m_eq1.m_body,data);
		string GetEqPos_EquipPageq1_bodyText()=>UIListener.GetText(m_view.m_EquipPage.m_eq1.m_body);
		void SetEqPos_EquipPageq1_attrText(string data)=>UIListener.SetText(m_view.m_EquipPage.m_eq1.m_attr,data);
		string GetEqPos_EquipPageq1_attrText()=>UIListener.GetText(m_view.m_EquipPage.m_eq1.m_attr);
		void _OnEquipPage_Eq1Click(EventContext data){
			OnEquipPage_Eq1Click(data);
		}
		partial void OnEquipPage_Eq1Click(EventContext data);
		void SetEquipPage_EquipPageq1Text(string data)=>UIListener.SetText(m_view.m_EquipPage.m_eq1,data);
		string GetEquipPage_EquipPageq1Text()=>UIListener.GetText(m_view.m_EquipPage.m_eq1);
		void _OnEqPos_EquipPageq2_stateChanged(EventContext data){
			OnEqPos_EquipPageq2_stateChanged(data);
		}
		partial void OnEqPos_EquipPageq2_stateChanged(EventContext data);
		void SwitchEqPos_EquipPageq2_statePage(int index)=>m_view.m_EquipPage.m_eq2.m_state.selectedIndex=index;
		void _OnEquip_EquipPageq2_body_typeChanged(EventContext data){
			OnEquip_EquipPageq2_body_typeChanged(data);
		}
		partial void OnEquip_EquipPageq2_body_typeChanged(EventContext data);
		void SwitchEquip_EquipPageq2_body_typePage(int index)=>m_view.m_EquipPage.m_eq2.m_body.m_type.selectedIndex=index;
		void _OnEquip_EquipPageq2_body_qualityChanged(EventContext data){
			OnEquip_EquipPageq2_body_qualityChanged(data);
		}
		partial void OnEquip_EquipPageq2_body_qualityChanged(EventContext data);
		void SwitchEquip_EquipPageq2_body_qualityPage(int index)=>m_view.m_EquipPage.m_eq2.m_body.m_quality.selectedIndex=index;
		void SetEqPos_EquipPageq2_bodyText(string data)=>UIListener.SetText(m_view.m_EquipPage.m_eq2.m_body,data);
		string GetEqPos_EquipPageq2_bodyText()=>UIListener.GetText(m_view.m_EquipPage.m_eq2.m_body);
		void SetEqPos_EquipPageq2_attrText(string data)=>UIListener.SetText(m_view.m_EquipPage.m_eq2.m_attr,data);
		string GetEqPos_EquipPageq2_attrText()=>UIListener.GetText(m_view.m_EquipPage.m_eq2.m_attr);
		void _OnEquipPage_Eq2Click(EventContext data){
			OnEquipPage_Eq2Click(data);
		}
		partial void OnEquipPage_Eq2Click(EventContext data);
		void SetEquipPage_EquipPageq2Text(string data)=>UIListener.SetText(m_view.m_EquipPage.m_eq2,data);
		string GetEquipPage_EquipPageq2Text()=>UIListener.GetText(m_view.m_EquipPage.m_eq2);
		void _OnEqPos_EquipPageq3_stateChanged(EventContext data){
			OnEqPos_EquipPageq3_stateChanged(data);
		}
		partial void OnEqPos_EquipPageq3_stateChanged(EventContext data);
		void SwitchEqPos_EquipPageq3_statePage(int index)=>m_view.m_EquipPage.m_eq3.m_state.selectedIndex=index;
		void _OnEquip_EquipPageq3_body_typeChanged(EventContext data){
			OnEquip_EquipPageq3_body_typeChanged(data);
		}
		partial void OnEquip_EquipPageq3_body_typeChanged(EventContext data);
		void SwitchEquip_EquipPageq3_body_typePage(int index)=>m_view.m_EquipPage.m_eq3.m_body.m_type.selectedIndex=index;
		void _OnEquip_EquipPageq3_body_qualityChanged(EventContext data){
			OnEquip_EquipPageq3_body_qualityChanged(data);
		}
		partial void OnEquip_EquipPageq3_body_qualityChanged(EventContext data);
		void SwitchEquip_EquipPageq3_body_qualityPage(int index)=>m_view.m_EquipPage.m_eq3.m_body.m_quality.selectedIndex=index;
		void SetEqPos_EquipPageq3_bodyText(string data)=>UIListener.SetText(m_view.m_EquipPage.m_eq3.m_body,data);
		string GetEqPos_EquipPageq3_bodyText()=>UIListener.GetText(m_view.m_EquipPage.m_eq3.m_body);
		void SetEqPos_EquipPageq3_attrText(string data)=>UIListener.SetText(m_view.m_EquipPage.m_eq3.m_attr,data);
		string GetEqPos_EquipPageq3_attrText()=>UIListener.GetText(m_view.m_EquipPage.m_eq3.m_attr);
		void _OnEquipPage_Eq3Click(EventContext data){
			OnEquipPage_Eq3Click(data);
		}
		partial void OnEquipPage_Eq3Click(EventContext data);
		void SetEquipPage_EquipPageq3Text(string data)=>UIListener.SetText(m_view.m_EquipPage.m_eq3,data);
		string GetEquipPage_EquipPageq3Text()=>UIListener.GetText(m_view.m_EquipPage.m_eq3);
		void _OnEquip_EquipPageq5_typeChanged(EventContext data){
			OnEquip_EquipPageq5_typeChanged(data);
		}
		partial void OnEquip_EquipPageq5_typeChanged(EventContext data);
		void SwitchEquip_EquipPageq5_typePage(int index)=>m_view.m_EquipPage.m_eq5.m_type.selectedIndex=index;
		void _OnEquip_EquipPageq5_qualityChanged(EventContext data){
			OnEquip_EquipPageq5_qualityChanged(data);
		}
		partial void OnEquip_EquipPageq5_qualityChanged(EventContext data);
		void SwitchEquip_EquipPageq5_qualityPage(int index)=>m_view.m_EquipPage.m_eq5.m_quality.selectedIndex=index;
		void _OnEquipPage_Eq5Click(EventContext data){
			OnEquipPage_Eq5Click(data);
		}
		partial void OnEquipPage_Eq5Click(EventContext data);
		void SetEquipPage_EquipPageq5Text(string data)=>UIListener.SetText(m_view.m_EquipPage.m_eq5,data);
		string GetEquipPage_EquipPageq5Text()=>UIListener.GetText(m_view.m_EquipPage.m_eq5);
		void _OnEquip_EquipPageq6_typeChanged(EventContext data){
			OnEquip_EquipPageq6_typeChanged(data);
		}
		partial void OnEquip_EquipPageq6_typeChanged(EventContext data);
		void SwitchEquip_EquipPageq6_typePage(int index)=>m_view.m_EquipPage.m_eq6.m_type.selectedIndex=index;
		void _OnEquip_EquipPageq6_qualityChanged(EventContext data){
			OnEquip_EquipPageq6_qualityChanged(data);
		}
		partial void OnEquip_EquipPageq6_qualityChanged(EventContext data);
		void SwitchEquip_EquipPageq6_qualityPage(int index)=>m_view.m_EquipPage.m_eq6.m_quality.selectedIndex=index;
		void _OnEquipPage_Eq6Click(EventContext data){
			OnEquipPage_Eq6Click(data);
		}
		partial void OnEquipPage_Eq6Click(EventContext data);
		void SetEquipPage_EquipPageq6Text(string data)=>UIListener.SetText(m_view.m_EquipPage.m_eq6,data);
		string GetEquipPage_EquipPageq6Text()=>UIListener.GetText(m_view.m_EquipPage.m_eq6);
		void _OnEquip_EquipPageq4_typeChanged(EventContext data){
			OnEquip_EquipPageq4_typeChanged(data);
		}
		partial void OnEquip_EquipPageq4_typeChanged(EventContext data);
		void SwitchEquip_EquipPageq4_typePage(int index)=>m_view.m_EquipPage.m_eq4.m_type.selectedIndex=index;
		void _OnEquip_EquipPageq4_qualityChanged(EventContext data){
			OnEquip_EquipPageq4_qualityChanged(data);
		}
		partial void OnEquip_EquipPageq4_qualityChanged(EventContext data);
		void SwitchEquip_EquipPageq4_qualityPage(int index)=>m_view.m_EquipPage.m_eq4.m_quality.selectedIndex=index;
		void _OnEquipPage_Eq4Click(EventContext data){
			OnEquipPage_Eq4Click(data);
		}
		partial void OnEquipPage_Eq4Click(EventContext data);
		void SetEquipPage_EquipPageq4Text(string data)=>UIListener.SetText(m_view.m_EquipPage.m_eq4,data);
		string GetEquipPage_EquipPageq4Text()=>UIListener.GetText(m_view.m_EquipPage.m_eq4);
		void _OnEquipPageClick(EventContext data){
			OnEquipPageClick(data);
		}
		partial void OnEquipPageClick(EventContext data);
		void _OnEquipUpQuality_StateChanged(EventContext data){
			OnEquipUpQuality_StateChanged(data);
		}
		partial void OnEquipUpQuality_StateChanged(EventContext data);
		void SwitchEquipUpQuality_StatePage(int index)=>m_view.m_EquipQuality.m_state.selectedIndex=index;
		void _OnEquipUpQuality_TypeChanged(EventContext data){
			OnEquipUpQuality_TypeChanged(data);
		}
		partial void OnEquipUpQuality_TypeChanged(EventContext data);
		void SwitchEquipUpQuality_TypePage(int index)=>m_view.m_EquipQuality.m_type.selectedIndex=index;
		void _OnEquipUpQuality_CombineChanged(EventContext data){
			OnEquipUpQuality_CombineChanged(data);
		}
		partial void OnEquipUpQuality_CombineChanged(EventContext data);
		void SwitchEquipUpQuality_CombinePage(int index)=>m_view.m_EquipQuality.m_combine.selectedIndex=index;
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
		void _OnEquipUpQuality_ClickClick(EventContext data){
			OnEquipUpQuality_ClickClick(data);
		}
		partial void OnEquipUpQuality_ClickClick(EventContext data);
		void SetEquipUpQuality_EquipQuality_clickText(string data)=>UIListener.SetText(m_view.m_EquipQuality.m_click,data);
		string GetEquipUpQuality_EquipQuality_clickText()=>UIListener.GetText(m_view.m_EquipQuality.m_click);
		void SetEquipUpQuality_NextattrText(string data)=>UIListener.SetText(m_view.m_EquipQuality.m_nextattr,data);
		string GetEquipUpQuality_NextattrText()=>UIListener.GetText(m_view.m_EquipQuality.m_nextattr);
		void SetEquipUpQuality_CurattrText(string data)=>UIListener.SetText(m_view.m_EquipQuality.m_curattr,data);
		string GetEquipUpQuality_CurattrText()=>UIListener.GetText(m_view.m_EquipQuality.m_curattr);
		void _OnEquipUpQuality_MergeClick(EventContext data){
			OnEquipUpQuality_MergeClick(data);
		}
		partial void OnEquipUpQuality_MergeClick(EventContext data);
		void SetEquipUpQuality_EquipQuality_mergeText(string data)=>UIListener.SetText(m_view.m_EquipQuality.m_merge,data);
		string GetEquipUpQuality_EquipQuality_mergeText()=>UIListener.GetText(m_view.m_EquipQuality.m_merge);
		void _OnEquipQualityClick(EventContext data){
			OnEquipQualityClick(data);
		}
		partial void OnEquipQualityClick(EventContext data);
		void _OnInfoClick(EventContext data){
			OnInfoClick(data);
		}
		partial void OnInfoClick(EventContext data);
		void SetInfoText(string data)=>UIListener.SetText(m_view.m_info,data);
		string GetInfoText()=>UIListener.GetText(m_view.m_info);
		void _OnEquipupClick(EventContext data){
			OnEquipupClick(data);
		}
		partial void OnEquipupClick(EventContext data);
		void SetEquipupText(string data)=>UIListener.SetText(m_view.m_equipup,data);
		string GetEquipupText()=>UIListener.GetText(m_view.m_equipup);
		void _OnClickBtnClick(EventContext data){
			OnClickBtnClick(data);
		}
		partial void OnClickBtnClick(EventContext data);

	}
}
