
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Explore;
	
	public partial class UIExplore
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			m_view.m_exploreState.onChanged.Add(new EventCallback1(_OnExploreStateChanged));
			m_view.m_exploreAuto.onChanged.Add(new EventCallback1(_OnExploreAutoChanged));
			m_view.m_eqinfostate.onChanged.Add(new EventCallback1(_OnEqinfostateChanged));
			UIListener.ListenerClose(m_view.m_close, new EventCallback1(DoCloseUIClick));
			UIListener.ListenerIcon(m_view.m_progress, new EventCallback1(_OnProgressClick));
			UIListener.Listener(m_view.m_warn, new EventCallback1(_OnWarnClick));
			m_view.m_eq11.m_quality.onChanged.Add(new EventCallback1(_OnFightEquip_QualityChanged));
			m_view.m_eq11.m_strongstate.onChanged.Add(new EventCallback1(_OnFightEquip_StrongstateChanged));
			m_view.m_eq11.m_type.onChanged.Add(new EventCallback1(_OnFightEquip_TypeChanged));
			m_view.m_eq11.m_part.onChanged.Add(new EventCallback1(_OnFightEquip_PartChanged));
			UIListener.Listener(m_view.m_eq11, new EventCallback1(_OnEq11Click));
			m_view.m_eq12.m_quality.onChanged.Add(new EventCallback1(_OnFightEquip_Eq12_qualityChanged));
			m_view.m_eq12.m_strongstate.onChanged.Add(new EventCallback1(_OnFightEquip_Eq12_strongstateChanged));
			m_view.m_eq12.m_type.onChanged.Add(new EventCallback1(_OnFightEquip_Eq12_typeChanged));
			m_view.m_eq12.m_part.onChanged.Add(new EventCallback1(_OnFightEquip_Eq12_partChanged));
			UIListener.Listener(m_view.m_eq12, new EventCallback1(_OnEq12Click));
			m_view.m_eq13.m_quality.onChanged.Add(new EventCallback1(_OnFightEquip_Eq13_qualityChanged));
			m_view.m_eq13.m_strongstate.onChanged.Add(new EventCallback1(_OnFightEquip_Eq13_strongstateChanged));
			m_view.m_eq13.m_type.onChanged.Add(new EventCallback1(_OnFightEquip_Eq13_typeChanged));
			m_view.m_eq13.m_part.onChanged.Add(new EventCallback1(_OnFightEquip_Eq13_partChanged));
			UIListener.Listener(m_view.m_eq13, new EventCallback1(_OnEq13Click));
			m_view.m_eq20.m_quality.onChanged.Add(new EventCallback1(_OnFightEquip_Eq20_qualityChanged));
			m_view.m_eq20.m_strongstate.onChanged.Add(new EventCallback1(_OnFightEquip_Eq20_strongstateChanged));
			m_view.m_eq20.m_type.onChanged.Add(new EventCallback1(_OnFightEquip_Eq20_typeChanged));
			m_view.m_eq20.m_part.onChanged.Add(new EventCallback1(_OnFightEquip_Eq20_partChanged));
			UIListener.Listener(m_view.m_eq20, new EventCallback1(_OnEq20Click));
			UIListener.Listener(m_view.m_find, new EventCallback1(_OnFindClick));
			m_view.m_tool.m_type.onChanged.Add(new EventCallback1(_OnToolBtn_TypeChanged));
			m_view.m_tool.m_state.onChanged.Add(new EventCallback1(_OnToolBtn_StateChanged));
			UIListener.Listener(m_view.m_tool, new EventCallback1(_OnToolClick));
			m_view.m_auto.m_type.onChanged.Add(new EventCallback1(_OnToolBtn_Auto_typeChanged));
			m_view.m_auto.m_state.onChanged.Add(new EventCallback1(_OnToolBtn_Auto_stateChanged));
			UIListener.Listener(m_view.m_auto, new EventCallback1(_OnAutoClick));
			UIListener.Listener(m_view.m_help, new EventCallback1(_OnHelpClick));
			m_view.m_hp.m_size.onChanged.Add(new EventCallback1(_OnFightAttr_SizeChanged));
			m_view.m_hp.m_uptype.onChanged.Add(new EventCallback1(_OnFightAttr_UptypeChanged));
			m_view.m_hp.m_ftype.onChanged.Add(new EventCallback1(_OnFightAttr_FtypeChanged));
			UIListener.ListenerIcon(m_view.m_hp, new EventCallback1(_OnHpClick));
			m_view.m_atk.m_size.onChanged.Add(new EventCallback1(_OnFightAttr_Atk_sizeChanged));
			m_view.m_atk.m_uptype.onChanged.Add(new EventCallback1(_OnFightAttr_Atk_uptypeChanged));
			m_view.m_atk.m_ftype.onChanged.Add(new EventCallback1(_OnFightAttr_Atk_ftypeChanged));
			UIListener.ListenerIcon(m_view.m_atk, new EventCallback1(_OnAtkClick));
			m_view.m_eqinfo.m_type.onChanged.Add(new EventCallback1(_OnFightEquipInfo_TypeChanged));
			m_view.m_eqinfo.m_body.m_bgtype.onChanged.Add(new EventCallback1(_OnFightEquipInfoBody_Eqinfo_body_bgtypeChanged));
			m_view.m_eqinfo.m_body.m_quality.onChanged.Add(new EventCallback1(_OnFightEquipInfoBody_Eqinfo_body_qualityChanged));
			UIListener.ListenerClose(m_view.m_eqinfo.m_body, new EventCallback1(DoCloseUIClick));
			UIListener.ListenerIcon(m_view.m_eqinfo, new EventCallback1(_OnEqinfoClick));
			UIListener.Listener(m_view.m_fightBtn, new EventCallback1(_OnFightBtnClick));
			UIListener.ListenerIcon(m_view.m_fightHp1.m_effect, new EventCallback1(_OnFightHp_EffectClick));
			UIListener.ListenerIcon(m_view.m_fightHp1, new EventCallback1(_OnFightHp1Click));
			UIListener.ListenerIcon(m_view.m_fightHp2.m_effect, new EventCallback1(_OnFightHp_FightHp2ffectClick));
			UIListener.ListenerIcon(m_view.m_fightHp2, new EventCallback1(_OnFightHp2Click));

		}
		partial void UnInitUI(UIContext context){
			m_view.m_exploreState.onChanged.Remove(new EventCallback1(_OnExploreStateChanged));
			m_view.m_exploreAuto.onChanged.Remove(new EventCallback1(_OnExploreAutoChanged));
			m_view.m_eqinfostate.onChanged.Remove(new EventCallback1(_OnEqinfostateChanged));
			UIListener.ListenerClose(m_view.m_close, new EventCallback1(DoCloseUIClick),remove:true);
			UIListener.ListenerIcon(m_view.m_progress, new EventCallback1(_OnProgressClick),remove:true);
			UIListener.Listener(m_view.m_warn, new EventCallback1(_OnWarnClick),remove:true);
			m_view.m_eq11.m_quality.onChanged.Remove(new EventCallback1(_OnFightEquip_QualityChanged));
			m_view.m_eq11.m_strongstate.onChanged.Remove(new EventCallback1(_OnFightEquip_StrongstateChanged));
			m_view.m_eq11.m_type.onChanged.Remove(new EventCallback1(_OnFightEquip_TypeChanged));
			m_view.m_eq11.m_part.onChanged.Remove(new EventCallback1(_OnFightEquip_PartChanged));
			UIListener.Listener(m_view.m_eq11, new EventCallback1(_OnEq11Click),remove:true);
			m_view.m_eq12.m_quality.onChanged.Remove(new EventCallback1(_OnFightEquip_Eq12_qualityChanged));
			m_view.m_eq12.m_strongstate.onChanged.Remove(new EventCallback1(_OnFightEquip_Eq12_strongstateChanged));
			m_view.m_eq12.m_type.onChanged.Remove(new EventCallback1(_OnFightEquip_Eq12_typeChanged));
			m_view.m_eq12.m_part.onChanged.Remove(new EventCallback1(_OnFightEquip_Eq12_partChanged));
			UIListener.Listener(m_view.m_eq12, new EventCallback1(_OnEq12Click),remove:true);
			m_view.m_eq13.m_quality.onChanged.Remove(new EventCallback1(_OnFightEquip_Eq13_qualityChanged));
			m_view.m_eq13.m_strongstate.onChanged.Remove(new EventCallback1(_OnFightEquip_Eq13_strongstateChanged));
			m_view.m_eq13.m_type.onChanged.Remove(new EventCallback1(_OnFightEquip_Eq13_typeChanged));
			m_view.m_eq13.m_part.onChanged.Remove(new EventCallback1(_OnFightEquip_Eq13_partChanged));
			UIListener.Listener(m_view.m_eq13, new EventCallback1(_OnEq13Click),remove:true);
			m_view.m_eq20.m_quality.onChanged.Remove(new EventCallback1(_OnFightEquip_Eq20_qualityChanged));
			m_view.m_eq20.m_strongstate.onChanged.Remove(new EventCallback1(_OnFightEquip_Eq20_strongstateChanged));
			m_view.m_eq20.m_type.onChanged.Remove(new EventCallback1(_OnFightEquip_Eq20_typeChanged));
			m_view.m_eq20.m_part.onChanged.Remove(new EventCallback1(_OnFightEquip_Eq20_partChanged));
			UIListener.Listener(m_view.m_eq20, new EventCallback1(_OnEq20Click),remove:true);
			UIListener.Listener(m_view.m_find, new EventCallback1(_OnFindClick),remove:true);
			m_view.m_tool.m_type.onChanged.Remove(new EventCallback1(_OnToolBtn_TypeChanged));
			m_view.m_tool.m_state.onChanged.Remove(new EventCallback1(_OnToolBtn_StateChanged));
			UIListener.Listener(m_view.m_tool, new EventCallback1(_OnToolClick),remove:true);
			m_view.m_auto.m_type.onChanged.Remove(new EventCallback1(_OnToolBtn_Auto_typeChanged));
			m_view.m_auto.m_state.onChanged.Remove(new EventCallback1(_OnToolBtn_Auto_stateChanged));
			UIListener.Listener(m_view.m_auto, new EventCallback1(_OnAutoClick),remove:true);
			UIListener.Listener(m_view.m_help, new EventCallback1(_OnHelpClick),remove:true);
			m_view.m_hp.m_size.onChanged.Remove(new EventCallback1(_OnFightAttr_SizeChanged));
			m_view.m_hp.m_uptype.onChanged.Remove(new EventCallback1(_OnFightAttr_UptypeChanged));
			m_view.m_hp.m_ftype.onChanged.Remove(new EventCallback1(_OnFightAttr_FtypeChanged));
			UIListener.ListenerIcon(m_view.m_hp, new EventCallback1(_OnHpClick),remove:true);
			m_view.m_atk.m_size.onChanged.Remove(new EventCallback1(_OnFightAttr_Atk_sizeChanged));
			m_view.m_atk.m_uptype.onChanged.Remove(new EventCallback1(_OnFightAttr_Atk_uptypeChanged));
			m_view.m_atk.m_ftype.onChanged.Remove(new EventCallback1(_OnFightAttr_Atk_ftypeChanged));
			UIListener.ListenerIcon(m_view.m_atk, new EventCallback1(_OnAtkClick),remove:true);
			m_view.m_eqinfo.m_type.onChanged.Remove(new EventCallback1(_OnFightEquipInfo_TypeChanged));
			m_view.m_eqinfo.m_body.m_bgtype.onChanged.Remove(new EventCallback1(_OnFightEquipInfoBody_Eqinfo_body_bgtypeChanged));
			m_view.m_eqinfo.m_body.m_quality.onChanged.Remove(new EventCallback1(_OnFightEquipInfoBody_Eqinfo_body_qualityChanged));
			UIListener.ListenerClose(m_view.m_eqinfo.m_body, new EventCallback1(DoCloseUIClick),remove:true);
			UIListener.ListenerIcon(m_view.m_eqinfo, new EventCallback1(_OnEqinfoClick),remove:true);
			UIListener.Listener(m_view.m_fightBtn, new EventCallback1(_OnFightBtnClick),remove:true);
			UIListener.ListenerIcon(m_view.m_fightHp1.m_effect, new EventCallback1(_OnFightHp_EffectClick),remove:true);
			UIListener.ListenerIcon(m_view.m_fightHp1, new EventCallback1(_OnFightHp1Click),remove:true);
			UIListener.ListenerIcon(m_view.m_fightHp2.m_effect, new EventCallback1(_OnFightHp_FightHp2ffectClick),remove:true);
			UIListener.ListenerIcon(m_view.m_fightHp2, new EventCallback1(_OnFightHp2Click),remove:true);

		}
		void _OnExploreStateChanged(EventContext data){
			OnExploreStateChanged(data);
		}
		partial void OnExploreStateChanged(EventContext data);
		void SwitchExploreStatePage(int index)=>m_view.m_exploreState.selectedIndex=index;
		void _OnExploreAutoChanged(EventContext data){
			OnExploreAutoChanged(data);
		}
		partial void OnExploreAutoChanged(EventContext data);
		void SwitchExploreAutoPage(int index)=>m_view.m_exploreAuto.selectedIndex=index;
		void _OnEqinfostateChanged(EventContext data){
			OnEqinfostateChanged(data);
		}
		partial void OnEqinfostateChanged(EventContext data);
		void SwitchEqinfostatePage(int index)=>m_view.m_eqinfostate.selectedIndex=index;
		void SetTopbarText(string data)=>UIListener.SetText(m_view.m_topbar,data);
		string GetTopbarText()=>UIListener.GetText(m_view.m_topbar);
		void DoCloseUIClick(EventContext data){
			 bool __closestate = true;
			 OnUICloseClick(ref __closestate);
			 if(__closestate)SGame.UIUtils.CloseUIByID(__id);
			 
		}
		partial void OnUICloseClick(ref bool state);
		void SetCloseText(string data)=>UIListener.SetText(m_view.m_close,data);
		string GetCloseText()=>UIListener.GetText(m_view.m_close);
		void SetExploreProgress___textText(string data)=>UIListener.SetText(m_view.m_progress.m___text,data);
		string GetExploreProgress___textText()=>UIListener.GetText(m_view.m_progress.m___text);
		void SetExploreProgress_LimitText(string data)=>UIListener.SetText(m_view.m_progress.m_limit,data);
		string GetExploreProgress_LimitText()=>UIListener.GetText(m_view.m_progress.m_limit);
		void _OnProgressClick(EventContext data){
			OnProgressClick(data);
		}
		partial void OnProgressClick(EventContext data);
		void SetProgressText(string data)=>UIListener.SetText(m_view.m_progress,data);
		string GetProgressText()=>UIListener.GetText(m_view.m_progress);
		void _OnWarnClick(EventContext data){
			OnWarnClick(data);
		}
		partial void OnWarnClick(EventContext data);
		void SetWarnText(string data)=>UIListener.SetText(m_view.m_warn,data);
		string GetWarnText()=>UIListener.GetText(m_view.m_warn);
		void SetRoundText(string data)=>UIListener.SetText(m_view.m_round,data);
		string GetRoundText()=>UIListener.GetText(m_view.m_round);
		void _OnFightEquip_QualityChanged(EventContext data){
			OnFightEquip_QualityChanged(data);
		}
		partial void OnFightEquip_QualityChanged(EventContext data);
		void SwitchFightEquip_QualityPage(int index)=>m_view.m_eq11.m_quality.selectedIndex=index;
		void _OnFightEquip_StrongstateChanged(EventContext data){
			OnFightEquip_StrongstateChanged(data);
		}
		partial void OnFightEquip_StrongstateChanged(EventContext data);
		void SwitchFightEquip_StrongstatePage(int index)=>m_view.m_eq11.m_strongstate.selectedIndex=index;
		void _OnFightEquip_TypeChanged(EventContext data){
			OnFightEquip_TypeChanged(data);
		}
		partial void OnFightEquip_TypeChanged(EventContext data);
		void SwitchFightEquip_TypePage(int index)=>m_view.m_eq11.m_type.selectedIndex=index;
		void _OnFightEquip_PartChanged(EventContext data){
			OnFightEquip_PartChanged(data);
		}
		partial void OnFightEquip_PartChanged(EventContext data);
		void SwitchFightEquip_PartPage(int index)=>m_view.m_eq11.m_part.selectedIndex=index;
		void SetFightEquip_LevelText(string data)=>UIListener.SetText(m_view.m_eq11.m_level,data);
		string GetFightEquip_LevelText()=>UIListener.GetText(m_view.m_eq11.m_level);
		void _OnEq11Click(EventContext data){
			OnEq11Click(data);
		}
		partial void OnEq11Click(EventContext data);
		void SetEq11Text(string data)=>UIListener.SetText(m_view.m_eq11,data);
		string GetEq11Text()=>UIListener.GetText(m_view.m_eq11);
		void _OnFightEquip_Eq12_qualityChanged(EventContext data){
			OnFightEquip_Eq12_qualityChanged(data);
		}
		partial void OnFightEquip_Eq12_qualityChanged(EventContext data);
		void SwitchFightEquip_Eq12_qualityPage(int index)=>m_view.m_eq12.m_quality.selectedIndex=index;
		void _OnFightEquip_Eq12_strongstateChanged(EventContext data){
			OnFightEquip_Eq12_strongstateChanged(data);
		}
		partial void OnFightEquip_Eq12_strongstateChanged(EventContext data);
		void SwitchFightEquip_Eq12_strongstatePage(int index)=>m_view.m_eq12.m_strongstate.selectedIndex=index;
		void _OnFightEquip_Eq12_typeChanged(EventContext data){
			OnFightEquip_Eq12_typeChanged(data);
		}
		partial void OnFightEquip_Eq12_typeChanged(EventContext data);
		void SwitchFightEquip_Eq12_typePage(int index)=>m_view.m_eq12.m_type.selectedIndex=index;
		void _OnFightEquip_Eq12_partChanged(EventContext data){
			OnFightEquip_Eq12_partChanged(data);
		}
		partial void OnFightEquip_Eq12_partChanged(EventContext data);
		void SwitchFightEquip_Eq12_partPage(int index)=>m_view.m_eq12.m_part.selectedIndex=index;
		void SetFightEquip_Eq12_levelText(string data)=>UIListener.SetText(m_view.m_eq12.m_level,data);
		string GetFightEquip_Eq12_levelText()=>UIListener.GetText(m_view.m_eq12.m_level);
		void _OnEq12Click(EventContext data){
			OnEq12Click(data);
		}
		partial void OnEq12Click(EventContext data);
		void SetEq12Text(string data)=>UIListener.SetText(m_view.m_eq12,data);
		string GetEq12Text()=>UIListener.GetText(m_view.m_eq12);
		void _OnFightEquip_Eq13_qualityChanged(EventContext data){
			OnFightEquip_Eq13_qualityChanged(data);
		}
		partial void OnFightEquip_Eq13_qualityChanged(EventContext data);
		void SwitchFightEquip_Eq13_qualityPage(int index)=>m_view.m_eq13.m_quality.selectedIndex=index;
		void _OnFightEquip_Eq13_strongstateChanged(EventContext data){
			OnFightEquip_Eq13_strongstateChanged(data);
		}
		partial void OnFightEquip_Eq13_strongstateChanged(EventContext data);
		void SwitchFightEquip_Eq13_strongstatePage(int index)=>m_view.m_eq13.m_strongstate.selectedIndex=index;
		void _OnFightEquip_Eq13_typeChanged(EventContext data){
			OnFightEquip_Eq13_typeChanged(data);
		}
		partial void OnFightEquip_Eq13_typeChanged(EventContext data);
		void SwitchFightEquip_Eq13_typePage(int index)=>m_view.m_eq13.m_type.selectedIndex=index;
		void _OnFightEquip_Eq13_partChanged(EventContext data){
			OnFightEquip_Eq13_partChanged(data);
		}
		partial void OnFightEquip_Eq13_partChanged(EventContext data);
		void SwitchFightEquip_Eq13_partPage(int index)=>m_view.m_eq13.m_part.selectedIndex=index;
		void SetFightEquip_Eq13_levelText(string data)=>UIListener.SetText(m_view.m_eq13.m_level,data);
		string GetFightEquip_Eq13_levelText()=>UIListener.GetText(m_view.m_eq13.m_level);
		void _OnEq13Click(EventContext data){
			OnEq13Click(data);
		}
		partial void OnEq13Click(EventContext data);
		void SetEq13Text(string data)=>UIListener.SetText(m_view.m_eq13,data);
		string GetEq13Text()=>UIListener.GetText(m_view.m_eq13);
		void _OnFightEquip_Eq20_qualityChanged(EventContext data){
			OnFightEquip_Eq20_qualityChanged(data);
		}
		partial void OnFightEquip_Eq20_qualityChanged(EventContext data);
		void SwitchFightEquip_Eq20_qualityPage(int index)=>m_view.m_eq20.m_quality.selectedIndex=index;
		void _OnFightEquip_Eq20_strongstateChanged(EventContext data){
			OnFightEquip_Eq20_strongstateChanged(data);
		}
		partial void OnFightEquip_Eq20_strongstateChanged(EventContext data);
		void SwitchFightEquip_Eq20_strongstatePage(int index)=>m_view.m_eq20.m_strongstate.selectedIndex=index;
		void _OnFightEquip_Eq20_typeChanged(EventContext data){
			OnFightEquip_Eq20_typeChanged(data);
		}
		partial void OnFightEquip_Eq20_typeChanged(EventContext data);
		void SwitchFightEquip_Eq20_typePage(int index)=>m_view.m_eq20.m_type.selectedIndex=index;
		void _OnFightEquip_Eq20_partChanged(EventContext data){
			OnFightEquip_Eq20_partChanged(data);
		}
		partial void OnFightEquip_Eq20_partChanged(EventContext data);
		void SwitchFightEquip_Eq20_partPage(int index)=>m_view.m_eq20.m_part.selectedIndex=index;
		void SetFightEquip_Eq20_levelText(string data)=>UIListener.SetText(m_view.m_eq20.m_level,data);
		string GetFightEquip_Eq20_levelText()=>UIListener.GetText(m_view.m_eq20.m_level);
		void _OnEq20Click(EventContext data){
			OnEq20Click(data);
		}
		partial void OnEq20Click(EventContext data);
		void SetEq20Text(string data)=>UIListener.SetText(m_view.m_eq20,data);
		string GetEq20Text()=>UIListener.GetText(m_view.m_eq20);
		void _OnFindClick(EventContext data){
			OnFindClick(data);
		}
		partial void OnFindClick(EventContext data);
		void SetFindText(string data)=>UIListener.SetText(m_view.m_find,data);
		string GetFindText()=>UIListener.GetText(m_view.m_find);
		void _OnToolBtn_TypeChanged(EventContext data){
			OnToolBtn_TypeChanged(data);
		}
		partial void OnToolBtn_TypeChanged(EventContext data);
		void SwitchToolBtn_TypePage(int index)=>m_view.m_tool.m_type.selectedIndex=index;
		void _OnToolBtn_StateChanged(EventContext data){
			OnToolBtn_StateChanged(data);
		}
		partial void OnToolBtn_StateChanged(EventContext data);
		void SwitchToolBtn_StatePage(int index)=>m_view.m_tool.m_state.selectedIndex=index;
		void SetToolBtn_TimeText(string data)=>UIListener.SetText(m_view.m_tool.m_time,data);
		string GetToolBtn_TimeText()=>UIListener.GetText(m_view.m_tool.m_time);
		void _OnToolClick(EventContext data){
			OnToolClick(data);
		}
		partial void OnToolClick(EventContext data);
		void SetToolText(string data)=>UIListener.SetText(m_view.m_tool,data);
		string GetToolText()=>UIListener.GetText(m_view.m_tool);
		void _OnToolBtn_Auto_typeChanged(EventContext data){
			OnToolBtn_Auto_typeChanged(data);
		}
		partial void OnToolBtn_Auto_typeChanged(EventContext data);
		void SwitchToolBtn_Auto_typePage(int index)=>m_view.m_auto.m_type.selectedIndex=index;
		void _OnToolBtn_Auto_stateChanged(EventContext data){
			OnToolBtn_Auto_stateChanged(data);
		}
		partial void OnToolBtn_Auto_stateChanged(EventContext data);
		void SwitchToolBtn_Auto_statePage(int index)=>m_view.m_auto.m_state.selectedIndex=index;
		void SetToolBtn_Auto_timeText(string data)=>UIListener.SetText(m_view.m_auto.m_time,data);
		string GetToolBtn_Auto_timeText()=>UIListener.GetText(m_view.m_auto.m_time);
		void _OnAutoClick(EventContext data){
			OnAutoClick(data);
		}
		partial void OnAutoClick(EventContext data);
		void SetAutoText(string data)=>UIListener.SetText(m_view.m_auto,data);
		string GetAutoText()=>UIListener.GetText(m_view.m_auto);
		void _OnHelpClick(EventContext data){
			OnHelpClick(data);
		}
		partial void OnHelpClick(EventContext data);
		void SetHelpText(string data)=>UIListener.SetText(m_view.m_help,data);
		string GetHelpText()=>UIListener.GetText(m_view.m_help);
		void _OnFightAttr_SizeChanged(EventContext data){
			OnFightAttr_SizeChanged(data);
		}
		partial void OnFightAttr_SizeChanged(EventContext data);
		void SwitchFightAttr_SizePage(int index)=>m_view.m_hp.m_size.selectedIndex=index;
		void _OnFightAttr_UptypeChanged(EventContext data){
			OnFightAttr_UptypeChanged(data);
		}
		partial void OnFightAttr_UptypeChanged(EventContext data);
		void SwitchFightAttr_UptypePage(int index)=>m_view.m_hp.m_uptype.selectedIndex=index;
		void _OnFightAttr_FtypeChanged(EventContext data){
			OnFightAttr_FtypeChanged(data);
		}
		partial void OnFightAttr_FtypeChanged(EventContext data);
		void SwitchFightAttr_FtypePage(int index)=>m_view.m_hp.m_ftype.selectedIndex=index;
		void SetFightAttr_ValText(string data)=>UIListener.SetText(m_view.m_hp.m_val,data);
		string GetFightAttr_ValText()=>UIListener.GetText(m_view.m_hp.m_val);
		void SetFightAttr_Val2Text(string data)=>UIListener.SetText(m_view.m_hp.m_val2,data);
		string GetFightAttr_Val2Text()=>UIListener.GetText(m_view.m_hp.m_val2);
		void _OnHpClick(EventContext data){
			OnHpClick(data);
		}
		partial void OnHpClick(EventContext data);
		void SetHpText(string data)=>UIListener.SetText(m_view.m_hp,data);
		string GetHpText()=>UIListener.GetText(m_view.m_hp);
		void _OnFightAttr_Atk_sizeChanged(EventContext data){
			OnFightAttr_Atk_sizeChanged(data);
		}
		partial void OnFightAttr_Atk_sizeChanged(EventContext data);
		void SwitchFightAttr_Atk_sizePage(int index)=>m_view.m_atk.m_size.selectedIndex=index;
		void _OnFightAttr_Atk_uptypeChanged(EventContext data){
			OnFightAttr_Atk_uptypeChanged(data);
		}
		partial void OnFightAttr_Atk_uptypeChanged(EventContext data);
		void SwitchFightAttr_Atk_uptypePage(int index)=>m_view.m_atk.m_uptype.selectedIndex=index;
		void _OnFightAttr_Atk_ftypeChanged(EventContext data){
			OnFightAttr_Atk_ftypeChanged(data);
		}
		partial void OnFightAttr_Atk_ftypeChanged(EventContext data);
		void SwitchFightAttr_Atk_ftypePage(int index)=>m_view.m_atk.m_ftype.selectedIndex=index;
		void SetFightAttr_Atk_valText(string data)=>UIListener.SetText(m_view.m_atk.m_val,data);
		string GetFightAttr_Atk_valText()=>UIListener.GetText(m_view.m_atk.m_val);
		void SetFightAttr_Atk_val2Text(string data)=>UIListener.SetText(m_view.m_atk.m_val2,data);
		string GetFightAttr_Atk_val2Text()=>UIListener.GetText(m_view.m_atk.m_val2);
		void _OnAtkClick(EventContext data){
			OnAtkClick(data);
		}
		partial void OnAtkClick(EventContext data);
		void SetAtkText(string data)=>UIListener.SetText(m_view.m_atk,data);
		string GetAtkText()=>UIListener.GetText(m_view.m_atk);
		void SetPowerText(string data)=>UIListener.SetText(m_view.m_power,data);
		string GetPowerText()=>UIListener.GetText(m_view.m_power);
		void _OnFightEquipInfo_TypeChanged(EventContext data){
			OnFightEquipInfo_TypeChanged(data);
		}
		partial void OnFightEquipInfo_TypeChanged(EventContext data);
		void SwitchFightEquipInfo_TypePage(int index)=>m_view.m_eqinfo.m_type.selectedIndex=index;
		void _OnFightEquipInfoBody_Eqinfo_body_bgtypeChanged(EventContext data){
			OnFightEquipInfoBody_Eqinfo_body_bgtypeChanged(data);
		}
		partial void OnFightEquipInfoBody_Eqinfo_body_bgtypeChanged(EventContext data);
		void SwitchFightEquipInfoBody_Eqinfo_body_bgtypePage(int index)=>m_view.m_eqinfo.m_body.m_bgtype.selectedIndex=index;
		void _OnFightEquipInfoBody_Eqinfo_body_qualityChanged(EventContext data){
			OnFightEquipInfoBody_Eqinfo_body_qualityChanged(data);
		}
		partial void OnFightEquipInfoBody_Eqinfo_body_qualityChanged(EventContext data);
		void SwitchFightEquipInfoBody_Eqinfo_body_qualityPage(int index)=>m_view.m_eqinfo.m_body.m_quality.selectedIndex=index;
		void SetFightEquipInfoBody_Eqinfo_body_qnameText(string data)=>UIListener.SetText(m_view.m_eqinfo.m_body.m_qname,data);
		string GetFightEquipInfoBody_Eqinfo_body_qnameText()=>UIListener.GetText(m_view.m_eqinfo.m_body.m_qname);
		void SetFightEquipInfo_Eqinfo_bodyText(string data)=>UIListener.SetText(m_view.m_eqinfo.m_body,data);
		string GetFightEquipInfo_Eqinfo_bodyText()=>UIListener.GetText(m_view.m_eqinfo.m_body);
		void _OnEqinfoClick(EventContext data){
			OnEqinfoClick(data);
		}
		partial void OnEqinfoClick(EventContext data);
		void SetEqinfoText(string data)=>UIListener.SetText(m_view.m_eqinfo,data);
		string GetEqinfoText()=>UIListener.GetText(m_view.m_eqinfo);
		void SetExptipsText(string data)=>UIListener.SetText(m_view.m_exptips,data);
		string GetExptipsText()=>UIListener.GetText(m_view.m_exptips);
		void _OnFightBtnClick(EventContext data){
			OnFightBtnClick(data);
		}
		partial void OnFightBtnClick(EventContext data);
		void SetFightBtnText(string data)=>UIListener.SetText(m_view.m_fightBtn,data);
		string GetFightBtnText()=>UIListener.GetText(m_view.m_fightBtn);
		void SetFightHp_ValueText(string data)=>UIListener.SetText(m_view.m_fightHp1.m_value,data);
		string GetFightHp_ValueText()=>UIListener.GetText(m_view.m_fightHp1.m_value);
		void _OnFightHp_EffectClick(EventContext data){
			OnFightHp_EffectClick(data);
		}
		partial void OnFightHp_EffectClick(EventContext data);
		void SetFightHp_FightHp1ffectText(string data)=>UIListener.SetText(m_view.m_fightHp1.m_effect,data);
		string GetFightHp_FightHp1ffectText()=>UIListener.GetText(m_view.m_fightHp1.m_effect);
		void _OnFightHp1Click(EventContext data){
			OnFightHp1Click(data);
		}
		partial void OnFightHp1Click(EventContext data);
		void SetFightHp1Text(string data)=>UIListener.SetText(m_view.m_fightHp1,data);
		string GetFightHp1Text()=>UIListener.GetText(m_view.m_fightHp1);
		void SetFightHp_FightHp2_valueText(string data)=>UIListener.SetText(m_view.m_fightHp2.m_value,data);
		string GetFightHp_FightHp2_valueText()=>UIListener.GetText(m_view.m_fightHp2.m_value);
		void _OnFightHp_FightHp2ffectClick(EventContext data){
			OnFightHp_FightHp2ffectClick(data);
		}
		partial void OnFightHp_FightHp2ffectClick(EventContext data);
		void SetFightHp_FightHp2ffectText(string data)=>UIListener.SetText(m_view.m_fightHp2.m_effect,data);
		string GetFightHp_FightHp2ffectText()=>UIListener.GetText(m_view.m_fightHp2.m_effect);
		void _OnFightHp2Click(EventContext data){
			OnFightHp2Click(data);
		}
		partial void OnFightHp2Click(EventContext data);
		void SetFightHp2Text(string data)=>UIListener.SetText(m_view.m_fightHp2,data);
		string GetFightHp2Text()=>UIListener.GetText(m_view.m_fightHp2);

	}
}
