
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Main;
	
	public partial class UIMain
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			m_view.m_main.onChanged.Add(new EventCallback1(_OnMainChanged));
			m_view.m_ad.onChanged.Add(new EventCallback1(_OnAdChanged));
			m_view.m_getworker.onChanged.Add(new EventCallback1(_OnGetworkerChanged));
			UIListener.ListenerIcon(m_view.m_rightList, new EventCallback1(_OnRightListClick));
			m_view.m_leftList.m_treasureBtn.m_timeColor.onChanged.Add(new EventCallback1(_OnActBtn_LeftList_treasureBtn_timeColorChanged));
			m_view.m_leftList.m_treasureBtn.m_ctrlTime.onChanged.Add(new EventCallback1(_OnActBtn_LeftList_treasureBtn_ctrlTimeChanged));
			m_view.m_leftList.m_treasureBtn.m_side.onChanged.Add(new EventCallback1(_OnActBtn_LeftList_treasureBtn_sideChanged));
			UIListener.ListenerClose(m_view.m_leftList.m_treasureBtn.m_body, new EventCallback1(DoCloseUIClick));
			UIListener.Listener(m_view.m_leftList.m_treasureBtn.m_redpoint, new EventCallback1(_OnActBtn_LeftList_treasureBtn_redpointClick));
			UIListener.Listener(m_view.m_leftList.m_treasureBtn, new EventCallback1(_OnLeftBtnList_TreasureBtnClick));
			UIListener.ListenerIcon(m_view.m_leftList, new EventCallback1(_OnLeftListClick));
			UIListener.Listener(m_view.m_head, new EventCallback1(_OnHeadClick));
			UIListener.Listener(m_view.m_Gold, new EventCallback1(_OnGoldClick));
			UIListener.Listener(m_view.m_Diamond, new EventCallback1(_OnDiamondClick));
			m_view.m_btnShop.m_timeColor.onChanged.Add(new EventCallback1(_OnActBtn_TimeColorChanged));
			m_view.m_btnShop.m_ctrlTime.onChanged.Add(new EventCallback1(_OnActBtn_CtrlTimeChanged));
			m_view.m_btnShop.m_side.onChanged.Add(new EventCallback1(_OnActBtn_SideChanged));
			UIListener.ListenerClose(m_view.m_btnShop.m_body, new EventCallback1(DoCloseUIClick));
			UIListener.Listener(m_view.m_btnShop.m_redpoint, new EventCallback1(_OnActBtn_RedpointClick));
			UIListener.Listener(m_view.m_btnShop, new EventCallback1(_OnBtnShopClick));
			m_view.m_buff.m_markState.onChanged.Add(new EventCallback1(_OnBuffBtn_MarkStateChanged));
			m_view.m_buff.m_isTime.onChanged.Add(new EventCallback1(_OnBuffBtn_IsTimeChanged));
			m_view.m_buff.m_tipState.onChanged.Add(new EventCallback1(_OnBuffBtn_TipStateChanged));
			UIListener.Listener(m_view.m_buff, new EventCallback1(_OnBuffClick));
			UIListener.Listener(m_view.m_likeBtn, new EventCallback1(_OnLikeBtnClick));
			m_view.m_taskBtn.m_state.onChanged.Add(new EventCallback1(_OnTaskBtn_StateChanged));
			m_view.m_taskBtn.m_finish.onChanged.Add(new EventCallback1(_OnTaskBtn_FinishChanged));
			UIListener.Listener(m_view.m_taskBtn, new EventCallback1(_OnTaskBtnClick));
			UIListener.Listener(m_view.m_totalBtn, new EventCallback1(_OnTotalBtnClick));
			UIListener.Listener(m_view.m_techBtn, new EventCallback1(_OnTechBtnClick));
			UIListener.Listener(m_view.m_recipeBtn, new EventCallback1(_OnRecipeBtnClick));
			UIListener.Listener(m_view.m_AdBtn, new EventCallback1(_OnAdBtnClick));
			UIListener.Listener(m_view.m_equipBtn, new EventCallback1(_OnEquipBtnClick));
			UIListener.Listener(m_view.m_petBtn, new EventCallback1(_OnPetBtnClick));
			UIListener.Listener(m_view.m_InvestBtn, new EventCallback1(_OnInvestBtnClick));
			m_view.m_workflag.m_type.onChanged.Add(new EventCallback1(_OnGetWorkerFlag_TypeChanged));
			UIListener.Listener(m_view.m_workflag, new EventCallback1(_OnWorkflagClick));
			m_view.m_hotFoodBtn.m_hoting.onChanged.Add(new EventCallback1(_OnHotFoodBtn_HotingChanged));
			UIListener.Listener(m_view.m_hotFoodBtn, new EventCallback1(_OnHotFoodBtnClick));

		}
		partial void UnInitUI(UIContext context){
			m_view.m_main.onChanged.Remove(new EventCallback1(_OnMainChanged));
			m_view.m_ad.onChanged.Remove(new EventCallback1(_OnAdChanged));
			m_view.m_getworker.onChanged.Remove(new EventCallback1(_OnGetworkerChanged));
			UIListener.ListenerIcon(m_view.m_rightList, new EventCallback1(_OnRightListClick),remove:true);
			m_view.m_leftList.m_treasureBtn.m_timeColor.onChanged.Remove(new EventCallback1(_OnActBtn_LeftList_treasureBtn_timeColorChanged));
			m_view.m_leftList.m_treasureBtn.m_ctrlTime.onChanged.Remove(new EventCallback1(_OnActBtn_LeftList_treasureBtn_ctrlTimeChanged));
			m_view.m_leftList.m_treasureBtn.m_side.onChanged.Remove(new EventCallback1(_OnActBtn_LeftList_treasureBtn_sideChanged));
			UIListener.ListenerClose(m_view.m_leftList.m_treasureBtn.m_body, new EventCallback1(DoCloseUIClick),remove:true);
			UIListener.Listener(m_view.m_leftList.m_treasureBtn.m_redpoint, new EventCallback1(_OnActBtn_LeftList_treasureBtn_redpointClick),remove:true);
			UIListener.Listener(m_view.m_leftList.m_treasureBtn, new EventCallback1(_OnLeftBtnList_TreasureBtnClick),remove:true);
			UIListener.ListenerIcon(m_view.m_leftList, new EventCallback1(_OnLeftListClick),remove:true);
			UIListener.Listener(m_view.m_head, new EventCallback1(_OnHeadClick),remove:true);
			UIListener.Listener(m_view.m_Gold, new EventCallback1(_OnGoldClick),remove:true);
			UIListener.Listener(m_view.m_Diamond, new EventCallback1(_OnDiamondClick),remove:true);
			m_view.m_btnShop.m_timeColor.onChanged.Remove(new EventCallback1(_OnActBtn_TimeColorChanged));
			m_view.m_btnShop.m_ctrlTime.onChanged.Remove(new EventCallback1(_OnActBtn_CtrlTimeChanged));
			m_view.m_btnShop.m_side.onChanged.Remove(new EventCallback1(_OnActBtn_SideChanged));
			UIListener.ListenerClose(m_view.m_btnShop.m_body, new EventCallback1(DoCloseUIClick),remove:true);
			UIListener.Listener(m_view.m_btnShop.m_redpoint, new EventCallback1(_OnActBtn_RedpointClick),remove:true);
			UIListener.Listener(m_view.m_btnShop, new EventCallback1(_OnBtnShopClick),remove:true);
			m_view.m_buff.m_markState.onChanged.Remove(new EventCallback1(_OnBuffBtn_MarkStateChanged));
			m_view.m_buff.m_isTime.onChanged.Remove(new EventCallback1(_OnBuffBtn_IsTimeChanged));
			m_view.m_buff.m_tipState.onChanged.Remove(new EventCallback1(_OnBuffBtn_TipStateChanged));
			UIListener.Listener(m_view.m_buff, new EventCallback1(_OnBuffClick),remove:true);
			UIListener.Listener(m_view.m_likeBtn, new EventCallback1(_OnLikeBtnClick),remove:true);
			m_view.m_taskBtn.m_state.onChanged.Remove(new EventCallback1(_OnTaskBtn_StateChanged));
			m_view.m_taskBtn.m_finish.onChanged.Remove(new EventCallback1(_OnTaskBtn_FinishChanged));
			UIListener.Listener(m_view.m_taskBtn, new EventCallback1(_OnTaskBtnClick),remove:true);
			UIListener.Listener(m_view.m_totalBtn, new EventCallback1(_OnTotalBtnClick),remove:true);
			UIListener.Listener(m_view.m_techBtn, new EventCallback1(_OnTechBtnClick),remove:true);
			UIListener.Listener(m_view.m_recipeBtn, new EventCallback1(_OnRecipeBtnClick),remove:true);
			UIListener.Listener(m_view.m_AdBtn, new EventCallback1(_OnAdBtnClick),remove:true);
			UIListener.Listener(m_view.m_equipBtn, new EventCallback1(_OnEquipBtnClick),remove:true);
			UIListener.Listener(m_view.m_petBtn, new EventCallback1(_OnPetBtnClick),remove:true);
			UIListener.Listener(m_view.m_InvestBtn, new EventCallback1(_OnInvestBtnClick),remove:true);
			m_view.m_workflag.m_type.onChanged.Remove(new EventCallback1(_OnGetWorkerFlag_TypeChanged));
			UIListener.Listener(m_view.m_workflag, new EventCallback1(_OnWorkflagClick),remove:true);
			m_view.m_hotFoodBtn.m_hoting.onChanged.Remove(new EventCallback1(_OnHotFoodBtn_HotingChanged));
			UIListener.Listener(m_view.m_hotFoodBtn, new EventCallback1(_OnHotFoodBtnClick),remove:true);

		}
		void _OnMainChanged(EventContext data){
			OnMainChanged(data);
		}
		partial void OnMainChanged(EventContext data);
		void SwitchMainPage(int index)=>m_view.m_main.selectedIndex=index;
		void _OnAdChanged(EventContext data){
			OnAdChanged(data);
		}
		partial void OnAdChanged(EventContext data);
		void SwitchAdPage(int index)=>m_view.m_ad.selectedIndex=index;
		void _OnGetworkerChanged(EventContext data){
			OnGetworkerChanged(data);
		}
		partial void OnGetworkerChanged(EventContext data);
		void SwitchGetworkerPage(int index)=>m_view.m_getworker.selectedIndex=index;
		void _OnRightListClick(EventContext data){
			OnRightListClick(data);
		}
		partial void OnRightListClick(EventContext data);
		void _OnActBtn_LeftList_treasureBtn_timeColorChanged(EventContext data){
			OnActBtn_LeftList_treasureBtn_timeColorChanged(data);
		}
		partial void OnActBtn_LeftList_treasureBtn_timeColorChanged(EventContext data);
		void SwitchActBtn_LeftList_treasureBtn_timeColorPage(int index)=>m_view.m_leftList.m_treasureBtn.m_timeColor.selectedIndex=index;
		void _OnActBtn_LeftList_treasureBtn_ctrlTimeChanged(EventContext data){
			OnActBtn_LeftList_treasureBtn_ctrlTimeChanged(data);
		}
		partial void OnActBtn_LeftList_treasureBtn_ctrlTimeChanged(EventContext data);
		void SwitchActBtn_LeftList_treasureBtn_ctrlTimePage(int index)=>m_view.m_leftList.m_treasureBtn.m_ctrlTime.selectedIndex=index;
		void _OnActBtn_LeftList_treasureBtn_sideChanged(EventContext data){
			OnActBtn_LeftList_treasureBtn_sideChanged(data);
		}
		partial void OnActBtn_LeftList_treasureBtn_sideChanged(EventContext data);
		void SwitchActBtn_LeftList_treasureBtn_sidePage(int index)=>m_view.m_leftList.m_treasureBtn.m_side.selectedIndex=index;
		void DoCloseUIClick(EventContext data){
			 bool __closestate = true;
			 OnUICloseClick(ref __closestate);
			 if(__closestate)SGame.UIUtils.CloseUIByID(__id);
			 
		}
		partial void OnUICloseClick(ref bool state);
		void _OnActBtn_LeftList_treasureBtn_redpointClick(EventContext data){
			OnActBtn_LeftList_treasureBtn_redpointClick(data);
		}
		partial void OnActBtn_LeftList_treasureBtn_redpointClick(EventContext data);
		void SetActBtn_LeftList_treasureBtn_redpointText(string data)=>UIListener.SetText(m_view.m_leftList.m_treasureBtn.m_redpoint,data);
		string GetActBtn_LeftList_treasureBtn_redpointText()=>UIListener.GetText(m_view.m_leftList.m_treasureBtn.m_redpoint);
		void SetActBtn_LeftList_treasureBtn_contentText(string data)=>UIListener.SetText(m_view.m_leftList.m_treasureBtn.m_content,data);
		string GetActBtn_LeftList_treasureBtn_contentText()=>UIListener.GetText(m_view.m_leftList.m_treasureBtn.m_content);
		void _OnLeftBtnList_TreasureBtnClick(EventContext data){
			OnLeftBtnList_TreasureBtnClick(data);
		}
		partial void OnLeftBtnList_TreasureBtnClick(EventContext data);
		void SetLeftBtnList_LeftList_treasureBtnText(string data)=>UIListener.SetText(m_view.m_leftList.m_treasureBtn,data);
		string GetLeftBtnList_LeftList_treasureBtnText()=>UIListener.GetText(m_view.m_leftList.m_treasureBtn);
		void _OnLeftListClick(EventContext data){
			OnLeftListClick(data);
		}
		partial void OnLeftListClick(EventContext data);
		void _OnHeadClick(EventContext data){
			OnHeadClick(data);
		}
		partial void OnHeadClick(EventContext data);
		void SetHeadText(string data)=>UIListener.SetText(m_view.m_head,data);
		string GetHeadText()=>UIListener.GetText(m_view.m_head);
		void _OnGoldClick(EventContext data){
			OnGoldClick(data);
		}
		partial void OnGoldClick(EventContext data);
		void SetGoldText(string data)=>UIListener.SetText(m_view.m_Gold,data);
		string GetGoldText()=>UIListener.GetText(m_view.m_Gold);
		void _OnDiamondClick(EventContext data){
			OnDiamondClick(data);
		}
		partial void OnDiamondClick(EventContext data);
		void SetDiamondText(string data)=>UIListener.SetText(m_view.m_Diamond,data);
		string GetDiamondText()=>UIListener.GetText(m_view.m_Diamond);
		void _OnActBtn_TimeColorChanged(EventContext data){
			OnActBtn_TimeColorChanged(data);
		}
		partial void OnActBtn_TimeColorChanged(EventContext data);
		void SwitchActBtn_TimeColorPage(int index)=>m_view.m_btnShop.m_timeColor.selectedIndex=index;
		void _OnActBtn_CtrlTimeChanged(EventContext data){
			OnActBtn_CtrlTimeChanged(data);
		}
		partial void OnActBtn_CtrlTimeChanged(EventContext data);
		void SwitchActBtn_CtrlTimePage(int index)=>m_view.m_btnShop.m_ctrlTime.selectedIndex=index;
		void _OnActBtn_SideChanged(EventContext data){
			OnActBtn_SideChanged(data);
		}
		partial void OnActBtn_SideChanged(EventContext data);
		void SwitchActBtn_SidePage(int index)=>m_view.m_btnShop.m_side.selectedIndex=index;
		void _OnActBtn_RedpointClick(EventContext data){
			OnActBtn_RedpointClick(data);
		}
		partial void OnActBtn_RedpointClick(EventContext data);
		void SetActBtn_BtnShop_redpointText(string data)=>UIListener.SetText(m_view.m_btnShop.m_redpoint,data);
		string GetActBtn_BtnShop_redpointText()=>UIListener.GetText(m_view.m_btnShop.m_redpoint);
		void SetActBtn_ContentText(string data)=>UIListener.SetText(m_view.m_btnShop.m_content,data);
		string GetActBtn_ContentText()=>UIListener.GetText(m_view.m_btnShop.m_content);
		void _OnBtnShopClick(EventContext data){
			OnBtnShopClick(data);
		}
		partial void OnBtnShopClick(EventContext data);
		void SetBtnShopText(string data)=>UIListener.SetText(m_view.m_btnShop,data);
		string GetBtnShopText()=>UIListener.GetText(m_view.m_btnShop);
		void _OnBuffBtn_MarkStateChanged(EventContext data){
			OnBuffBtn_MarkStateChanged(data);
		}
		partial void OnBuffBtn_MarkStateChanged(EventContext data);
		void SwitchBuffBtn_MarkStatePage(int index)=>m_view.m_buff.m_markState.selectedIndex=index;
		void _OnBuffBtn_IsTimeChanged(EventContext data){
			OnBuffBtn_IsTimeChanged(data);
		}
		partial void OnBuffBtn_IsTimeChanged(EventContext data);
		void SwitchBuffBtn_IsTimePage(int index)=>m_view.m_buff.m_isTime.selectedIndex=index;
		void _OnBuffBtn_TipStateChanged(EventContext data){
			OnBuffBtn_TipStateChanged(data);
		}
		partial void OnBuffBtn_TipStateChanged(EventContext data);
		void SwitchBuffBtn_TipStatePage(int index)=>m_view.m_buff.m_tipState.selectedIndex=index;
		void SetBuffBtn_TimeText(string data)=>UIListener.SetText(m_view.m_buff.m_time,data);
		string GetBuffBtn_TimeText()=>UIListener.GetText(m_view.m_buff.m_time);
		void SetBuffBtn_InfoText(string data)=>UIListener.SetText(m_view.m_buff.m_info,data);
		string GetBuffBtn_InfoText()=>UIListener.GetText(m_view.m_buff.m_info);
		void _OnBuffClick(EventContext data){
			OnBuffClick(data);
		}
		partial void OnBuffClick(EventContext data);
		void SetBuffText(string data)=>UIListener.SetText(m_view.m_buff,data);
		string GetBuffText()=>UIListener.GetText(m_view.m_buff);
		void SetLikeBtn_NumText(string data)=>UIListener.SetText(m_view.m_likeBtn.m_num,data);
		string GetLikeBtn_NumText()=>UIListener.GetText(m_view.m_likeBtn.m_num);
		void SetLikeBtn_CountText(string data)=>UIListener.SetText(m_view.m_likeBtn.m_count,data);
		string GetLikeBtn_CountText()=>UIListener.GetText(m_view.m_likeBtn.m_count);
		void _OnLikeBtnClick(EventContext data){
			OnLikeBtnClick(data);
		}
		partial void OnLikeBtnClick(EventContext data);
		void SetLikeBtnText(string data)=>UIListener.SetText(m_view.m_likeBtn,data);
		string GetLikeBtnText()=>UIListener.GetText(m_view.m_likeBtn);
		void _OnTaskBtn_StateChanged(EventContext data){
			OnTaskBtn_StateChanged(data);
		}
		partial void OnTaskBtn_StateChanged(EventContext data);
		void SwitchTaskBtn_StatePage(int index)=>m_view.m_taskBtn.m_state.selectedIndex=index;
		void _OnTaskBtn_FinishChanged(EventContext data){
			OnTaskBtn_FinishChanged(data);
		}
		partial void OnTaskBtn_FinishChanged(EventContext data);
		void SwitchTaskBtn_FinishPage(int index)=>m_view.m_taskBtn.m_finish.selectedIndex=index;
		void _OnTaskBtnClick(EventContext data){
			OnTaskBtnClick(data);
		}
		partial void OnTaskBtnClick(EventContext data);
		void SetTaskBtnText(string data)=>UIListener.SetText(m_view.m_taskBtn,data);
		string GetTaskBtnText()=>UIListener.GetText(m_view.m_taskBtn);
		void SetTotalBtn_NumText(string data)=>UIListener.SetText(m_view.m_totalBtn.m_num,data);
		string GetTotalBtn_NumText()=>UIListener.GetText(m_view.m_totalBtn.m_num);
		void _OnTotalBtnClick(EventContext data){
			OnTotalBtnClick(data);
		}
		partial void OnTotalBtnClick(EventContext data);
		void _OnTechBtnClick(EventContext data){
			OnTechBtnClick(data);
		}
		partial void OnTechBtnClick(EventContext data);
		void SetTechBtnText(string data)=>UIListener.SetText(m_view.m_techBtn,data);
		string GetTechBtnText()=>UIListener.GetText(m_view.m_techBtn);
		void _OnRecipeBtnClick(EventContext data){
			OnRecipeBtnClick(data);
		}
		partial void OnRecipeBtnClick(EventContext data);
		void SetRecipeBtnText(string data)=>UIListener.SetText(m_view.m_recipeBtn,data);
		string GetRecipeBtnText()=>UIListener.GetText(m_view.m_recipeBtn);
		void _OnAdBtnClick(EventContext data){
			OnAdBtnClick(data);
		}
		partial void OnAdBtnClick(EventContext data);
		void SetAdBtnText(string data)=>UIListener.SetText(m_view.m_AdBtn,data);
		string GetAdBtnText()=>UIListener.GetText(m_view.m_AdBtn);
		void _OnEquipBtnClick(EventContext data){
			OnEquipBtnClick(data);
		}
		partial void OnEquipBtnClick(EventContext data);
		void SetEquipBtnText(string data)=>UIListener.SetText(m_view.m_equipBtn,data);
		string GetEquipBtnText()=>UIListener.GetText(m_view.m_equipBtn);
		void _OnPetBtnClick(EventContext data){
			OnPetBtnClick(data);
		}
		partial void OnPetBtnClick(EventContext data);
		void SetPetBtnText(string data)=>UIListener.SetText(m_view.m_petBtn,data);
		string GetPetBtnText()=>UIListener.GetText(m_view.m_petBtn);
		void _OnInvestBtnClick(EventContext data){
			OnInvestBtnClick(data);
		}
		partial void OnInvestBtnClick(EventContext data);
		void SetInvestBtnText(string data)=>UIListener.SetText(m_view.m_InvestBtn,data);
		string GetInvestBtnText()=>UIListener.GetText(m_view.m_InvestBtn);
		void _OnGetWorkerFlag_TypeChanged(EventContext data){
			OnGetWorkerFlag_TypeChanged(data);
		}
		partial void OnGetWorkerFlag_TypeChanged(EventContext data);
		void SwitchGetWorkerFlag_TypePage(int index)=>m_view.m_workflag.m_type.selectedIndex=index;
		void _OnWorkflagClick(EventContext data){
			OnWorkflagClick(data);
		}
		partial void OnWorkflagClick(EventContext data);
		void SetWorkflagText(string data)=>UIListener.SetText(m_view.m_workflag,data);
		string GetWorkflagText()=>UIListener.GetText(m_view.m_workflag);
		void _OnHotFoodBtn_HotingChanged(EventContext data){
			OnHotFoodBtn_HotingChanged(data);
		}
		partial void OnHotFoodBtn_HotingChanged(EventContext data);
		void SwitchHotFoodBtn_HotingPage(int index)=>m_view.m_hotFoodBtn.m_hoting.selectedIndex=index;
		void _OnHotFoodBtnClick(EventContext data){
			OnHotFoodBtnClick(data);
		}
		partial void OnHotFoodBtnClick(EventContext data);
		void SetHotFoodBtnText(string data)=>UIListener.SetText(m_view.m_hotFoodBtn,data);
		string GetHotFoodBtnText()=>UIListener.GetText(m_view.m_hotFoodBtn);

	}
}
