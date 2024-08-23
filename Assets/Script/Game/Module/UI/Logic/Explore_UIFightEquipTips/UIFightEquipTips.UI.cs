
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Explore;
	
	public partial class UIFightEquipTips
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			m_view.m_type.onChanged.Add(new EventCallback1(_OnTypeChanged));
			m_view.m_size.onChanged.Add(new EventCallback1(_OnSizeChanged));
			m_view.m_old.m_new.onChanged.Add(new EventCallback1(_OnFightEquipTipsBody_NewChanged));
			m_view.m_old.m_upstate.onChanged.Add(new EventCallback1(_OnFightEquipTipsBody_UpstateChanged));
			m_view.m_old.m_bgtype.onChanged.Add(new EventCallback1(_OnFightEquipTipsBody_BgtypeChanged));
			m_view.m_old.m_eq.m_quality.onChanged.Add(new EventCallback1(_OnFightEquip_Oldq_qualityChanged));
			m_view.m_old.m_eq.m_strongstate.onChanged.Add(new EventCallback1(_OnFightEquip_Oldq_strongstateChanged));
			m_view.m_old.m_eq.m_type.onChanged.Add(new EventCallback1(_OnFightEquip_Oldq_typeChanged));
			m_view.m_old.m_eq.m_part.onChanged.Add(new EventCallback1(_OnFightEquip_Oldq_partChanged));
			UIListener.Listener(m_view.m_old.m_eq, new EventCallback1(_OnFightEquipTipsBody_EqClick));
			UIListener.ListenerIcon(m_view.m_old, new EventCallback1(_OnOldClick));
			m_view.m_info.m_new.onChanged.Add(new EventCallback1(_OnFightEquipTipsBody_Info_newChanged));
			m_view.m_info.m_upstate.onChanged.Add(new EventCallback1(_OnFightEquipTipsBody_Info_upstateChanged));
			m_view.m_info.m_bgtype.onChanged.Add(new EventCallback1(_OnFightEquipTipsBody_Info_bgtypeChanged));
			m_view.m_info.m_eq.m_quality.onChanged.Add(new EventCallback1(_OnFightEquip_Infoq_qualityChanged));
			m_view.m_info.m_eq.m_strongstate.onChanged.Add(new EventCallback1(_OnFightEquip_Infoq_strongstateChanged));
			m_view.m_info.m_eq.m_type.onChanged.Add(new EventCallback1(_OnFightEquip_Infoq_typeChanged));
			m_view.m_info.m_eq.m_part.onChanged.Add(new EventCallback1(_OnFightEquip_Infoq_partChanged));
			UIListener.Listener(m_view.m_info.m_eq, new EventCallback1(_OnFightEquipTipsBody_InfoqClick));
			UIListener.ListenerIcon(m_view.m_info, new EventCallback1(_OnInfoClick));
			UIListener.Listener(m_view.m_puton0, new EventCallback1(_OnPuton0Click));
			UIListener.Listener(m_view.m_drop, new EventCallback1(_OnDropClick));
			UIListener.Listener(m_view.m_puton, new EventCallback1(_OnPutonClick));

		}
		partial void UnInitUI(UIContext context){
			m_view.m_type.onChanged.Remove(new EventCallback1(_OnTypeChanged));
			m_view.m_size.onChanged.Remove(new EventCallback1(_OnSizeChanged));
			m_view.m_old.m_new.onChanged.Remove(new EventCallback1(_OnFightEquipTipsBody_NewChanged));
			m_view.m_old.m_upstate.onChanged.Remove(new EventCallback1(_OnFightEquipTipsBody_UpstateChanged));
			m_view.m_old.m_bgtype.onChanged.Remove(new EventCallback1(_OnFightEquipTipsBody_BgtypeChanged));
			m_view.m_old.m_eq.m_quality.onChanged.Remove(new EventCallback1(_OnFightEquip_Oldq_qualityChanged));
			m_view.m_old.m_eq.m_strongstate.onChanged.Remove(new EventCallback1(_OnFightEquip_Oldq_strongstateChanged));
			m_view.m_old.m_eq.m_type.onChanged.Remove(new EventCallback1(_OnFightEquip_Oldq_typeChanged));
			m_view.m_old.m_eq.m_part.onChanged.Remove(new EventCallback1(_OnFightEquip_Oldq_partChanged));
			UIListener.Listener(m_view.m_old.m_eq, new EventCallback1(_OnFightEquipTipsBody_EqClick),remove:true);
			UIListener.ListenerIcon(m_view.m_old, new EventCallback1(_OnOldClick),remove:true);
			m_view.m_info.m_new.onChanged.Remove(new EventCallback1(_OnFightEquipTipsBody_Info_newChanged));
			m_view.m_info.m_upstate.onChanged.Remove(new EventCallback1(_OnFightEquipTipsBody_Info_upstateChanged));
			m_view.m_info.m_bgtype.onChanged.Remove(new EventCallback1(_OnFightEquipTipsBody_Info_bgtypeChanged));
			m_view.m_info.m_eq.m_quality.onChanged.Remove(new EventCallback1(_OnFightEquip_Infoq_qualityChanged));
			m_view.m_info.m_eq.m_strongstate.onChanged.Remove(new EventCallback1(_OnFightEquip_Infoq_strongstateChanged));
			m_view.m_info.m_eq.m_type.onChanged.Remove(new EventCallback1(_OnFightEquip_Infoq_typeChanged));
			m_view.m_info.m_eq.m_part.onChanged.Remove(new EventCallback1(_OnFightEquip_Infoq_partChanged));
			UIListener.Listener(m_view.m_info.m_eq, new EventCallback1(_OnFightEquipTipsBody_InfoqClick),remove:true);
			UIListener.ListenerIcon(m_view.m_info, new EventCallback1(_OnInfoClick),remove:true);
			UIListener.Listener(m_view.m_puton0, new EventCallback1(_OnPuton0Click),remove:true);
			UIListener.Listener(m_view.m_drop, new EventCallback1(_OnDropClick),remove:true);
			UIListener.Listener(m_view.m_puton, new EventCallback1(_OnPutonClick),remove:true);

		}
		void _OnTypeChanged(EventContext data){
			OnTypeChanged(data);
		}
		partial void OnTypeChanged(EventContext data);
		void SwitchTypePage(int index)=>m_view.m_type.selectedIndex=index;
		void _OnSizeChanged(EventContext data){
			OnSizeChanged(data);
		}
		partial void OnSizeChanged(EventContext data);
		void SwitchSizePage(int index)=>m_view.m_size.selectedIndex=index;
		void _OnFightEquipTipsBody_NewChanged(EventContext data){
			OnFightEquipTipsBody_NewChanged(data);
		}
		partial void OnFightEquipTipsBody_NewChanged(EventContext data);
		void SwitchFightEquipTipsBody_NewPage(int index)=>m_view.m_old.m_new.selectedIndex=index;
		void _OnFightEquipTipsBody_UpstateChanged(EventContext data){
			OnFightEquipTipsBody_UpstateChanged(data);
		}
		partial void OnFightEquipTipsBody_UpstateChanged(EventContext data);
		void SwitchFightEquipTipsBody_UpstatePage(int index)=>m_view.m_old.m_upstate.selectedIndex=index;
		void _OnFightEquipTipsBody_BgtypeChanged(EventContext data){
			OnFightEquipTipsBody_BgtypeChanged(data);
		}
		partial void OnFightEquipTipsBody_BgtypeChanged(EventContext data);
		void SwitchFightEquipTipsBody_BgtypePage(int index)=>m_view.m_old.m_bgtype.selectedIndex=index;
		void _OnFightEquip_Oldq_qualityChanged(EventContext data){
			OnFightEquip_Oldq_qualityChanged(data);
		}
		partial void OnFightEquip_Oldq_qualityChanged(EventContext data);
		void SwitchFightEquip_Oldq_qualityPage(int index)=>m_view.m_old.m_eq.m_quality.selectedIndex=index;
		void _OnFightEquip_Oldq_strongstateChanged(EventContext data){
			OnFightEquip_Oldq_strongstateChanged(data);
		}
		partial void OnFightEquip_Oldq_strongstateChanged(EventContext data);
		void SwitchFightEquip_Oldq_strongstatePage(int index)=>m_view.m_old.m_eq.m_strongstate.selectedIndex=index;
		void _OnFightEquip_Oldq_typeChanged(EventContext data){
			OnFightEquip_Oldq_typeChanged(data);
		}
		partial void OnFightEquip_Oldq_typeChanged(EventContext data);
		void SwitchFightEquip_Oldq_typePage(int index)=>m_view.m_old.m_eq.m_type.selectedIndex=index;
		void _OnFightEquip_Oldq_partChanged(EventContext data){
			OnFightEquip_Oldq_partChanged(data);
		}
		partial void OnFightEquip_Oldq_partChanged(EventContext data);
		void SwitchFightEquip_Oldq_partPage(int index)=>m_view.m_old.m_eq.m_part.selectedIndex=index;
		void SetFightEquip_Oldq_levelText(string data)=>UIListener.SetText(m_view.m_old.m_eq.m_level,data);
		string GetFightEquip_Oldq_levelText()=>UIListener.GetText(m_view.m_old.m_eq.m_level);
		void _OnFightEquipTipsBody_EqClick(EventContext data){
			OnFightEquipTipsBody_EqClick(data);
		}
		partial void OnFightEquipTipsBody_EqClick(EventContext data);
		void SetFightEquipTipsBody_OldqText(string data)=>UIListener.SetText(m_view.m_old.m_eq,data);
		string GetFightEquipTipsBody_OldqText()=>UIListener.GetText(m_view.m_old.m_eq);
		void SetFightEquipTipsBody_NameText(string data)=>UIListener.SetText(m_view.m_old.m_name,data);
		string GetFightEquipTipsBody_NameText()=>UIListener.GetText(m_view.m_old.m_name);
		void SetFightEquipTipsBody_PowerText(string data)=>UIListener.SetText(m_view.m_old.m_power,data);
		string GetFightEquipTipsBody_PowerText()=>UIListener.GetText(m_view.m_old.m_power);
		void _OnOldClick(EventContext data){
			OnOldClick(data);
		}
		partial void OnOldClick(EventContext data);
		void SetOldText(string data)=>UIListener.SetText(m_view.m_old,data);
		string GetOldText()=>UIListener.GetText(m_view.m_old);
		void _OnFightEquipTipsBody_Info_newChanged(EventContext data){
			OnFightEquipTipsBody_Info_newChanged(data);
		}
		partial void OnFightEquipTipsBody_Info_newChanged(EventContext data);
		void SwitchFightEquipTipsBody_Info_newPage(int index)=>m_view.m_info.m_new.selectedIndex=index;
		void _OnFightEquipTipsBody_Info_upstateChanged(EventContext data){
			OnFightEquipTipsBody_Info_upstateChanged(data);
		}
		partial void OnFightEquipTipsBody_Info_upstateChanged(EventContext data);
		void SwitchFightEquipTipsBody_Info_upstatePage(int index)=>m_view.m_info.m_upstate.selectedIndex=index;
		void _OnFightEquipTipsBody_Info_bgtypeChanged(EventContext data){
			OnFightEquipTipsBody_Info_bgtypeChanged(data);
		}
		partial void OnFightEquipTipsBody_Info_bgtypeChanged(EventContext data);
		void SwitchFightEquipTipsBody_Info_bgtypePage(int index)=>m_view.m_info.m_bgtype.selectedIndex=index;
		void _OnFightEquip_Infoq_qualityChanged(EventContext data){
			OnFightEquip_Infoq_qualityChanged(data);
		}
		partial void OnFightEquip_Infoq_qualityChanged(EventContext data);
		void SwitchFightEquip_Infoq_qualityPage(int index)=>m_view.m_info.m_eq.m_quality.selectedIndex=index;
		void _OnFightEquip_Infoq_strongstateChanged(EventContext data){
			OnFightEquip_Infoq_strongstateChanged(data);
		}
		partial void OnFightEquip_Infoq_strongstateChanged(EventContext data);
		void SwitchFightEquip_Infoq_strongstatePage(int index)=>m_view.m_info.m_eq.m_strongstate.selectedIndex=index;
		void _OnFightEquip_Infoq_typeChanged(EventContext data){
			OnFightEquip_Infoq_typeChanged(data);
		}
		partial void OnFightEquip_Infoq_typeChanged(EventContext data);
		void SwitchFightEquip_Infoq_typePage(int index)=>m_view.m_info.m_eq.m_type.selectedIndex=index;
		void _OnFightEquip_Infoq_partChanged(EventContext data){
			OnFightEquip_Infoq_partChanged(data);
		}
		partial void OnFightEquip_Infoq_partChanged(EventContext data);
		void SwitchFightEquip_Infoq_partPage(int index)=>m_view.m_info.m_eq.m_part.selectedIndex=index;
		void SetFightEquip_Infoq_levelText(string data)=>UIListener.SetText(m_view.m_info.m_eq.m_level,data);
		string GetFightEquip_Infoq_levelText()=>UIListener.GetText(m_view.m_info.m_eq.m_level);
		void _OnFightEquipTipsBody_InfoqClick(EventContext data){
			OnFightEquipTipsBody_InfoqClick(data);
		}
		partial void OnFightEquipTipsBody_InfoqClick(EventContext data);
		void SetFightEquipTipsBody_InfoqText(string data)=>UIListener.SetText(m_view.m_info.m_eq,data);
		string GetFightEquipTipsBody_InfoqText()=>UIListener.GetText(m_view.m_info.m_eq);
		void SetFightEquipTipsBody_Info_nameText(string data)=>UIListener.SetText(m_view.m_info.m_name,data);
		string GetFightEquipTipsBody_Info_nameText()=>UIListener.GetText(m_view.m_info.m_name);
		void SetFightEquipTipsBody_Info_powerText(string data)=>UIListener.SetText(m_view.m_info.m_power,data);
		string GetFightEquipTipsBody_Info_powerText()=>UIListener.GetText(m_view.m_info.m_power);
		void _OnInfoClick(EventContext data){
			OnInfoClick(data);
		}
		partial void OnInfoClick(EventContext data);
		void SetInfoText(string data)=>UIListener.SetText(m_view.m_info,data);
		string GetInfoText()=>UIListener.GetText(m_view.m_info);
		void _OnPuton0Click(EventContext data){
			OnPuton0Click(data);
		}
		partial void OnPuton0Click(EventContext data);
		void SetPuton0Text(string data)=>UIListener.SetText(m_view.m_puton0,data);
		string GetPuton0Text()=>UIListener.GetText(m_view.m_puton0);
		void _OnDropClick(EventContext data){
			OnDropClick(data);
		}
		partial void OnDropClick(EventContext data);
		void SetDropText(string data)=>UIListener.SetText(m_view.m_drop,data);
		string GetDropText()=>UIListener.GetText(m_view.m_drop);
		void _OnPutonClick(EventContext data){
			OnPutonClick(data);
		}
		partial void OnPutonClick(EventContext data);
		void SetPutonText(string data)=>UIListener.SetText(m_view.m_puton,data);
		string GetPutonText()=>UIListener.GetText(m_view.m_puton);

	}
}
