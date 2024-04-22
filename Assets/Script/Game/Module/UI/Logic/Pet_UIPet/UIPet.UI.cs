
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
			m_view.m_pet.m_step.onChanged.Add(new EventCallback1(_OnPetInfo_StepChanged));
			m_view.m_pet.m_lock.onChanged.Add(new EventCallback1(_OnPetInfo_LockChanged));
			m_view.m_pet.m_model.m_quality.onChanged.Add(new EventCallback1(_OnPetModel_Pet_model_qualityChanged));
			m_view.m_pet.m_model.m_step.onChanged.Add(new EventCallback1(_OnPetModel_Pet_model_stepChanged));
			UIListener.ListenerIcon(m_view.m_pet.m_model.m_model, new EventCallback1(_OnPetModel_Pet_model_modelClick));
			UIListener.Listener(m_view.m_pet.m_model.m_click, new EventCallback1(_OnPetModel_Pet_model_clickClick));
			UIListener.Listener(m_view.m_pet.m_model.m_free, new EventCallback1(_OnPetModel_Pet_model_freeClick));
			UIListener.ListenerIcon(m_view.m_pet.m_model, new EventCallback1(_OnPetInfo_ModelClick));
			UIListener.ListenerIcon(m_view.m_pet, new EventCallback1(_OnPetClick));
			m_view.m_egg.m_state.onChanged.Add(new EventCallback1(_OnPetEgg_StateChanged));
			m_view.m_egg.m_select.onChanged.Add(new EventCallback1(_OnPetEgg_SelectChanged));
			UIListener.Listener(m_view.m_egg.m_add, new EventCallback1(_OnPetEgg_AddClick));
			UIListener.Listener(m_view.m_egg.m_get1, new EventCallback1(_OnPetEgg_Get1Click));
			UIListener.Listener(m_view.m_egg.m_get2, new EventCallback1(_OnPetEgg_Get2Click));
			UIListener.Listener(m_view.m_egg.m_get3, new EventCallback1(_OnPetEgg_Get3Click));
			UIListener.ListenerIcon(m_view.m_egg, new EventCallback1(_OnEggClick));
			m_view.m_top.m_type.onChanged.Add(new EventCallback1(_OnPetMedalList_TypeChanged));
			m_view.m_top.m_price.onChanged.Add(new EventCallback1(_OnPetMedalList_PriceChanged));
			m_view.m_top.m_price2.onChanged.Add(new EventCallback1(_OnPetMedalList_Price2Changed));
			UIListener.Listener(m_view.m_top.m_c1, new EventCallback1(_OnPetMedalList_C1Click));
			UIListener.Listener(m_view.m_top.m_c2, new EventCallback1(_OnPetMedalList_C2Click));
			UIListener.Listener(m_view.m_top.m_c3, new EventCallback1(_OnPetMedalList_C3Click));
			UIListener.ListenerIcon(m_view.m_top, new EventCallback1(_OnTopClick));

		}
		partial void UnInitUI(UIContext context){
			m_view.m_tab.onChanged.Remove(new EventCallback1(_OnTabChanged));
			m_view.m_type.onChanged.Remove(new EventCallback1(_OnTypeChanged));
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick),remove:true);
			m_view.m_pet.m_quality.onChanged.Remove(new EventCallback1(_OnPetInfo_QualityChanged));
			m_view.m_pet.m_step.onChanged.Remove(new EventCallback1(_OnPetInfo_StepChanged));
			m_view.m_pet.m_lock.onChanged.Remove(new EventCallback1(_OnPetInfo_LockChanged));
			m_view.m_pet.m_model.m_quality.onChanged.Remove(new EventCallback1(_OnPetModel_Pet_model_qualityChanged));
			m_view.m_pet.m_model.m_step.onChanged.Remove(new EventCallback1(_OnPetModel_Pet_model_stepChanged));
			UIListener.ListenerIcon(m_view.m_pet.m_model.m_model, new EventCallback1(_OnPetModel_Pet_model_modelClick),remove:true);
			UIListener.Listener(m_view.m_pet.m_model.m_click, new EventCallback1(_OnPetModel_Pet_model_clickClick),remove:true);
			UIListener.Listener(m_view.m_pet.m_model.m_free, new EventCallback1(_OnPetModel_Pet_model_freeClick),remove:true);
			UIListener.ListenerIcon(m_view.m_pet.m_model, new EventCallback1(_OnPetInfo_ModelClick),remove:true);
			UIListener.ListenerIcon(m_view.m_pet, new EventCallback1(_OnPetClick),remove:true);
			m_view.m_egg.m_state.onChanged.Remove(new EventCallback1(_OnPetEgg_StateChanged));
			m_view.m_egg.m_select.onChanged.Remove(new EventCallback1(_OnPetEgg_SelectChanged));
			UIListener.Listener(m_view.m_egg.m_add, new EventCallback1(_OnPetEgg_AddClick),remove:true);
			UIListener.Listener(m_view.m_egg.m_get1, new EventCallback1(_OnPetEgg_Get1Click),remove:true);
			UIListener.Listener(m_view.m_egg.m_get2, new EventCallback1(_OnPetEgg_Get2Click),remove:true);
			UIListener.Listener(m_view.m_egg.m_get3, new EventCallback1(_OnPetEgg_Get3Click),remove:true);
			UIListener.ListenerIcon(m_view.m_egg, new EventCallback1(_OnEggClick),remove:true);
			m_view.m_top.m_type.onChanged.Remove(new EventCallback1(_OnPetMedalList_TypeChanged));
			m_view.m_top.m_price.onChanged.Remove(new EventCallback1(_OnPetMedalList_PriceChanged));
			m_view.m_top.m_price2.onChanged.Remove(new EventCallback1(_OnPetMedalList_Price2Changed));
			UIListener.Listener(m_view.m_top.m_c1, new EventCallback1(_OnPetMedalList_C1Click),remove:true);
			UIListener.Listener(m_view.m_top.m_c2, new EventCallback1(_OnPetMedalList_C2Click),remove:true);
			UIListener.Listener(m_view.m_top.m_c3, new EventCallback1(_OnPetMedalList_C3Click),remove:true);
			UIListener.ListenerIcon(m_view.m_top, new EventCallback1(_OnTopClick),remove:true);

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
		void _OnPetInfo_StepChanged(EventContext data){
			OnPetInfo_StepChanged(data);
		}
		partial void OnPetInfo_StepChanged(EventContext data);
		void SwitchPetInfo_StepPage(int index)=>m_view.m_pet.m_step.selectedIndex=index;
		void _OnPetInfo_LockChanged(EventContext data){
			OnPetInfo_LockChanged(data);
		}
		partial void OnPetInfo_LockChanged(EventContext data);
		void SwitchPetInfo_LockPage(int index)=>m_view.m_pet.m_lock.selectedIndex=index;
		void _OnPetModel_Pet_model_qualityChanged(EventContext data){
			OnPetModel_Pet_model_qualityChanged(data);
		}
		partial void OnPetModel_Pet_model_qualityChanged(EventContext data);
		void SwitchPetModel_Pet_model_qualityPage(int index)=>m_view.m_pet.m_model.m_quality.selectedIndex=index;
		void _OnPetModel_Pet_model_stepChanged(EventContext data){
			OnPetModel_Pet_model_stepChanged(data);
		}
		partial void OnPetModel_Pet_model_stepChanged(EventContext data);
		void SwitchPetModel_Pet_model_stepPage(int index)=>m_view.m_pet.m_model.m_step.selectedIndex=index;
		void _OnPetModel_Pet_model_modelClick(EventContext data){
			OnPetModel_Pet_model_modelClick(data);
		}
		partial void OnPetModel_Pet_model_modelClick(EventContext data);
		void _OnPetModel_Pet_model_clickClick(EventContext data){
			OnPetModel_Pet_model_clickClick(data);
		}
		partial void OnPetModel_Pet_model_clickClick(EventContext data);
		void SetPetModel_Pet_model_clickText(string data)=>UIListener.SetText(m_view.m_pet.m_model.m_click,data);
		string GetPetModel_Pet_model_clickText()=>UIListener.GetText(m_view.m_pet.m_model.m_click);
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
		void _OnPetEgg_StateChanged(EventContext data){
			OnPetEgg_StateChanged(data);
		}
		partial void OnPetEgg_StateChanged(EventContext data);
		void SwitchPetEgg_StatePage(int index)=>m_view.m_egg.m_state.selectedIndex=index;
		void _OnPetEgg_SelectChanged(EventContext data){
			OnPetEgg_SelectChanged(data);
		}
		partial void OnPetEgg_SelectChanged(EventContext data);
		void SwitchPetEgg_SelectPage(int index)=>m_view.m_egg.m_select.selectedIndex=index;
		void _OnPetEgg_AddClick(EventContext data){
			OnPetEgg_AddClick(data);
		}
		partial void OnPetEgg_AddClick(EventContext data);
		void SetPetEgg_Egg_addText(string data)=>UIListener.SetText(m_view.m_egg.m_add,data);
		string GetPetEgg_Egg_addText()=>UIListener.GetText(m_view.m_egg.m_add);
		void SetPetEgg_ProgressValue(float data)=>UIListener.SetValue(m_view.m_egg.m_progress,data);
		float GetPetEgg_ProgressValue()=>UIListener.GetValue(m_view.m_egg.m_progress);
		void SetPetEgg_Egg_progressText(string data)=>UIListener.SetText(m_view.m_egg.m_progress,data);
		string GetPetEgg_Egg_progressText()=>UIListener.GetText(m_view.m_egg.m_progress);
		void SetPetEgg_TimeText(string data)=>UIListener.SetText(m_view.m_egg.m_time,data);
		string GetPetEgg_TimeText()=>UIListener.GetText(m_view.m_egg.m_time);
		void SetPetEgg_PriceText(string data)=>UIListener.SetText(m_view.m_egg.m_price,data);
		string GetPetEgg_PriceText()=>UIListener.GetText(m_view.m_egg.m_price);
		void _OnPetEgg_Get1Click(EventContext data){
			OnPetEgg_Get1Click(data);
		}
		partial void OnPetEgg_Get1Click(EventContext data);
		void SetPetEgg_Egg_get1Text(string data)=>UIListener.SetText(m_view.m_egg.m_get1,data);
		string GetPetEgg_Egg_get1Text()=>UIListener.GetText(m_view.m_egg.m_get1);
		void _OnPetEgg_Get2Click(EventContext data){
			OnPetEgg_Get2Click(data);
		}
		partial void OnPetEgg_Get2Click(EventContext data);
		void SetPetEgg_Egg_get2Text(string data)=>UIListener.SetText(m_view.m_egg.m_get2,data);
		string GetPetEgg_Egg_get2Text()=>UIListener.GetText(m_view.m_egg.m_get2);
		void _OnPetEgg_Get3Click(EventContext data){
			OnPetEgg_Get3Click(data);
		}
		partial void OnPetEgg_Get3Click(EventContext data);
		void SetPetEgg_Egg_get3Text(string data)=>UIListener.SetText(m_view.m_egg.m_get3,data);
		string GetPetEgg_Egg_get3Text()=>UIListener.GetText(m_view.m_egg.m_get3);
		void _OnEggClick(EventContext data){
			OnEggClick(data);
		}
		partial void OnEggClick(EventContext data);
		void _OnPetMedalList_TypeChanged(EventContext data){
			OnPetMedalList_TypeChanged(data);
		}
		partial void OnPetMedalList_TypeChanged(EventContext data);
		void SwitchPetMedalList_TypePage(int index)=>m_view.m_top.m_type.selectedIndex=index;
		void _OnPetMedalList_PriceChanged(EventContext data){
			OnPetMedalList_PriceChanged(data);
		}
		partial void OnPetMedalList_PriceChanged(EventContext data);
		void SwitchPetMedalList_PricePage(int index)=>m_view.m_top.m_price.selectedIndex=index;
		void _OnPetMedalList_Price2Changed(EventContext data){
			OnPetMedalList_Price2Changed(data);
		}
		partial void OnPetMedalList_Price2Changed(EventContext data);
		void SwitchPetMedalList_Price2Page(int index)=>m_view.m_top.m_price2.selectedIndex=index;
		void _OnPetMedalList_C1Click(EventContext data){
			OnPetMedalList_C1Click(data);
		}
		partial void OnPetMedalList_C1Click(EventContext data);
		void SetPetMedalList_Top_c1Text(string data)=>UIListener.SetText(m_view.m_top.m_c1,data);
		string GetPetMedalList_Top_c1Text()=>UIListener.GetText(m_view.m_top.m_c1);
		void _OnPetMedalList_C2Click(EventContext data){
			OnPetMedalList_C2Click(data);
		}
		partial void OnPetMedalList_C2Click(EventContext data);
		void SetPetMedalList_Top_c2Text(string data)=>UIListener.SetText(m_view.m_top.m_c2,data);
		string GetPetMedalList_Top_c2Text()=>UIListener.GetText(m_view.m_top.m_c2);
		void _OnPetMedalList_C3Click(EventContext data){
			OnPetMedalList_C3Click(data);
		}
		partial void OnPetMedalList_C3Click(EventContext data);
		void SetPetMedalList_Top_c3Text(string data)=>UIListener.SetText(m_view.m_top.m_c3,data);
		string GetPetMedalList_Top_c3Text()=>UIListener.GetText(m_view.m_top.m_c3);
		void _OnTopClick(EventContext data){
			OnTopClick(data);
		}
		partial void OnTopClick(EventContext data);

	}
}
