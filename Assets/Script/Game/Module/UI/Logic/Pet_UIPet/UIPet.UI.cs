
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Pet;
	
	public partial class UIPet
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			m_view.m_tab.onChanged.Add(new EventCallback1(_OnTabChanged));
			m_view.m_type.onChanged.Add(new EventCallback1(_OnTypeChanged));
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick));
			m_view.m_pet.m_quality.onChanged.Add(new EventCallback1(_OnPetInfo_QualityChanged));
			m_view.m_pet.m_nullpet.onChanged.Add(new EventCallback1(_OnPetInfo_NullpetChanged));
			m_view.m_pet.m_model.m_quality.onChanged.Add(new EventCallback1(_OnPetModel_Pet_model_qualityChanged));
			m_view.m_pet.m_model.m_type.onChanged.Add(new EventCallback1(_OnPetModel_Pet_model_typeChanged));
			UIListener.ListenerIcon(m_view.m_pet.m_model.m_model, new EventCallback1(_OnPetModel_Pet_model_modelClick));
			UIListener.Listener(m_view.m_pet.m_model.m_free, new EventCallback1(_OnPetModel_Pet_model_freeClick));
			UIListener.ListenerIcon(m_view.m_pet.m_model, new EventCallback1(_OnPetInfo_ModelClick));
			UIListener.ListenerIcon(m_view.m_pet, new EventCallback1(_OnPetClick));
			UIListener.Listener(m_view.m_click, new EventCallback1(_OnClickClick));
			m_view.m_egg.m_quality.onChanged.Add(new EventCallback1(_OnPetEggBtn_QualityChanged));
			m_view.m_egg.m_state.onChanged.Add(new EventCallback1(_OnPetEggBtn_StateChanged));
			m_view.m_egg.m_select.onChanged.Add(new EventCallback1(_OnPetEggBtn_SelectChanged));
			UIListener.Listener(m_view.m_egg.m_add, new EventCallback1(_OnPetEggBtn_AddClick));
			UIListener.Listener(m_view.m_egg, new EventCallback1(_OnEggClick));
			UIListener.ListenerIcon(m_view.m_eggpanel, new EventCallback1(_OnEggpanelClick));
			UIListener.ListenerIcon(m_view.m_tips, new EventCallback1(_OnTipsClick));

		}
		partial void UnInitUI(UIContext context){
			m_view.m_tab.onChanged.Remove(new EventCallback1(_OnTabChanged));
			m_view.m_type.onChanged.Remove(new EventCallback1(_OnTypeChanged));
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick),remove:true);
			m_view.m_pet.m_quality.onChanged.Remove(new EventCallback1(_OnPetInfo_QualityChanged));
			m_view.m_pet.m_nullpet.onChanged.Remove(new EventCallback1(_OnPetInfo_NullpetChanged));
			m_view.m_pet.m_model.m_quality.onChanged.Remove(new EventCallback1(_OnPetModel_Pet_model_qualityChanged));
			m_view.m_pet.m_model.m_type.onChanged.Remove(new EventCallback1(_OnPetModel_Pet_model_typeChanged));
			UIListener.ListenerIcon(m_view.m_pet.m_model.m_model, new EventCallback1(_OnPetModel_Pet_model_modelClick),remove:true);
			UIListener.Listener(m_view.m_pet.m_model.m_free, new EventCallback1(_OnPetModel_Pet_model_freeClick),remove:true);
			UIListener.ListenerIcon(m_view.m_pet.m_model, new EventCallback1(_OnPetInfo_ModelClick),remove:true);
			UIListener.ListenerIcon(m_view.m_pet, new EventCallback1(_OnPetClick),remove:true);
			UIListener.Listener(m_view.m_click, new EventCallback1(_OnClickClick),remove:true);
			m_view.m_egg.m_quality.onChanged.Remove(new EventCallback1(_OnPetEggBtn_QualityChanged));
			m_view.m_egg.m_state.onChanged.Remove(new EventCallback1(_OnPetEggBtn_StateChanged));
			m_view.m_egg.m_select.onChanged.Remove(new EventCallback1(_OnPetEggBtn_SelectChanged));
			UIListener.Listener(m_view.m_egg.m_add, new EventCallback1(_OnPetEggBtn_AddClick),remove:true);
			UIListener.Listener(m_view.m_egg, new EventCallback1(_OnEggClick),remove:true);
			UIListener.ListenerIcon(m_view.m_eggpanel, new EventCallback1(_OnEggpanelClick),remove:true);
			UIListener.ListenerIcon(m_view.m_tips, new EventCallback1(_OnTipsClick),remove:true);

		}
		void _OnTabChanged(EventContext data){
			OnTabChanged(data);
		}
		partial void OnTabChanged(EventContext data);
		void SwitchTabPage(int index)=>m_view.m_tab.selectedIndex=index;
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
		void _OnPetInfo_QualityChanged(EventContext data){
			OnPetInfo_QualityChanged(data);
		}
		partial void OnPetInfo_QualityChanged(EventContext data);
		void SwitchPetInfo_QualityPage(int index)=>m_view.m_pet.m_quality.selectedIndex=index;
		void _OnPetInfo_NullpetChanged(EventContext data){
			OnPetInfo_NullpetChanged(data);
		}
		partial void OnPetInfo_NullpetChanged(EventContext data);
		void SwitchPetInfo_NullpetPage(int index)=>m_view.m_pet.m_nullpet.selectedIndex=index;
		void _OnPetModel_Pet_model_qualityChanged(EventContext data){
			OnPetModel_Pet_model_qualityChanged(data);
		}
		partial void OnPetModel_Pet_model_qualityChanged(EventContext data);
		void SwitchPetModel_Pet_model_qualityPage(int index)=>m_view.m_pet.m_model.m_quality.selectedIndex=index;
		void _OnPetModel_Pet_model_typeChanged(EventContext data){
			OnPetModel_Pet_model_typeChanged(data);
		}
		partial void OnPetModel_Pet_model_typeChanged(EventContext data);
		void SwitchPetModel_Pet_model_typePage(int index)=>m_view.m_pet.m_model.m_type.selectedIndex=index;
		void _OnPetModel_Pet_model_modelClick(EventContext data){
			OnPetModel_Pet_model_modelClick(data);
		}
		partial void OnPetModel_Pet_model_modelClick(EventContext data);
		void _OnPetModel_Pet_model_freeClick(EventContext data){
			OnPetModel_Pet_model_freeClick(data);
		}
		partial void OnPetModel_Pet_model_freeClick(EventContext data);
		void SetPetModel_Pet_model_freeText(string data)=>UIListener.SetText(m_view.m_pet.m_model.m_free,data);
		string GetPetModel_Pet_model_freeText()=>UIListener.GetText(m_view.m_pet.m_model.m_free);
		void _OnPetInfo_ModelClick(EventContext data){
			OnPetInfo_ModelClick(data);
		}
		partial void OnPetInfo_ModelClick(EventContext data);
		void SetPetInfo_Pet_modelText(string data)=>UIListener.SetText(m_view.m_pet.m_model,data);
		string GetPetInfo_Pet_modelText()=>UIListener.GetText(m_view.m_pet.m_model);
		void _OnPetClick(EventContext data){
			OnPetClick(data);
		}
		partial void OnPetClick(EventContext data);
		void _OnClickClick(EventContext data){
			OnClickClick(data);
		}
		partial void OnClickClick(EventContext data);
		void SetClickText(string data)=>UIListener.SetText(m_view.m_click,data);
		string GetClickText()=>UIListener.GetText(m_view.m_click);
		void _OnPetEggBtn_QualityChanged(EventContext data){
			OnPetEggBtn_QualityChanged(data);
		}
		partial void OnPetEggBtn_QualityChanged(EventContext data);
		void SwitchPetEggBtn_QualityPage(int index)=>m_view.m_egg.m_quality.selectedIndex=index;
		void _OnPetEggBtn_StateChanged(EventContext data){
			OnPetEggBtn_StateChanged(data);
		}
		partial void OnPetEggBtn_StateChanged(EventContext data);
		void SwitchPetEggBtn_StatePage(int index)=>m_view.m_egg.m_state.selectedIndex=index;
		void _OnPetEggBtn_SelectChanged(EventContext data){
			OnPetEggBtn_SelectChanged(data);
		}
		partial void OnPetEggBtn_SelectChanged(EventContext data);
		void SwitchPetEggBtn_SelectPage(int index)=>m_view.m_egg.m_select.selectedIndex=index;
		void _OnPetEggBtn_AddClick(EventContext data){
			OnPetEggBtn_AddClick(data);
		}
		partial void OnPetEggBtn_AddClick(EventContext data);
		void SetPetEggBtn_Egg_addText(string data)=>UIListener.SetText(m_view.m_egg.m_add,data);
		string GetPetEggBtn_Egg_addText()=>UIListener.GetText(m_view.m_egg.m_add);
		void SetPetEggBtn_ProgressText(string data)=>UIListener.SetText(m_view.m_egg.m_progress,data);
		string GetPetEggBtn_ProgressText()=>UIListener.GetText(m_view.m_egg.m_progress);
		void SetPetEggBtn_TimeText(string data)=>UIListener.SetText(m_view.m_egg.m_time,data);
		string GetPetEggBtn_TimeText()=>UIListener.GetText(m_view.m_egg.m_time);
		void SetPetEggBtn_GettipsText(string data)=>UIListener.SetText(m_view.m_egg.m_gettips,data);
		string GetPetEggBtn_GettipsText()=>UIListener.GetText(m_view.m_egg.m_gettips);
		void _OnEggClick(EventContext data){
			OnEggClick(data);
		}
		partial void OnEggClick(EventContext data);
		void SetEggText(string data)=>UIListener.SetText(m_view.m_egg,data);
		string GetEggText()=>UIListener.GetText(m_view.m_egg);
		void _OnEggpanelClick(EventContext data){
			OnEggpanelClick(data);
		}
		partial void OnEggpanelClick(EventContext data);
		void _OnTipsClick(EventContext data){
			OnTipsClick(data);
		}
		partial void OnTipsClick(EventContext data);
		void SetTipsText(string data)=>UIListener.SetText(m_view.m_tips,data);
		string GetTipsText()=>UIListener.GetText(m_view.m_tips);

	}
}
