
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Player;
	
	public partial class UIEquipPreview
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick));
			m_view.m_equip.m_type.onChanged.Add(new EventCallback1(_OnEquip_TypeChanged));
			m_view.m_equip.m_quality.onChanged.Add(new EventCallback1(_OnEquip_QualityChanged));
			UIListener.Listener(m_view.m_equip, new EventCallback1(_OnEquipClick));
			m_view.m_nextequip.m_type.onChanged.Add(new EventCallback1(_OnEquip_Nextequip_typeChanged));
			m_view.m_nextequip.m_quality.onChanged.Add(new EventCallback1(_OnEquip_Nextequip_qualityChanged));
			UIListener.Listener(m_view.m_nextequip, new EventCallback1(_OnNextequipClick));
			UIListener.ListenerIcon(m_view.m_level, new EventCallback1(_OnLevelClick));
			UIListener.ListenerIcon(m_view.m_main, new EventCallback1(_OnMainClick));
			m_view.m_attr.m_quality.onChanged.Add(new EventCallback1(_Onattrlabel_QualityChanged));
			m_view.m_attr.m_lock.onChanged.Add(new EventCallback1(_Onattrlabel_LockChanged));
			m_view.m_attr.m_type.onChanged.Add(new EventCallback1(_Onattrlabel_TypeChanged));
			UIListener.Listener(m_view.m_attr.m_info, new EventCallback1(_Onattrlabel_InfoClick));
			UIListener.ListenerIcon(m_view.m_attr, new EventCallback1(_OnAttrClick));

		}
		partial void UnInitUI(UIContext context){
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick),remove:true);
			m_view.m_equip.m_type.onChanged.Remove(new EventCallback1(_OnEquip_TypeChanged));
			m_view.m_equip.m_quality.onChanged.Remove(new EventCallback1(_OnEquip_QualityChanged));
			UIListener.Listener(m_view.m_equip, new EventCallback1(_OnEquipClick),remove:true);
			m_view.m_nextequip.m_type.onChanged.Remove(new EventCallback1(_OnEquip_Nextequip_typeChanged));
			m_view.m_nextequip.m_quality.onChanged.Remove(new EventCallback1(_OnEquip_Nextequip_qualityChanged));
			UIListener.Listener(m_view.m_nextequip, new EventCallback1(_OnNextequipClick),remove:true);
			UIListener.ListenerIcon(m_view.m_level, new EventCallback1(_OnLevelClick),remove:true);
			UIListener.ListenerIcon(m_view.m_main, new EventCallback1(_OnMainClick),remove:true);
			m_view.m_attr.m_quality.onChanged.Remove(new EventCallback1(_Onattrlabel_QualityChanged));
			m_view.m_attr.m_lock.onChanged.Remove(new EventCallback1(_Onattrlabel_LockChanged));
			m_view.m_attr.m_type.onChanged.Remove(new EventCallback1(_Onattrlabel_TypeChanged));
			UIListener.Listener(m_view.m_attr.m_info, new EventCallback1(_Onattrlabel_InfoClick),remove:true);
			UIListener.ListenerIcon(m_view.m_attr, new EventCallback1(_OnAttrClick),remove:true);

		}
		void DoCloseUIClick(EventContext data){
			 bool __closestate = true;
			 OnUICloseClick(ref __closestate);
			 if(__closestate)SGame.UIUtils.CloseUIByID(__id);
			 
		}
		partial void OnUICloseClick(ref bool state);
		void SetBodyText(string data)=>UIListener.SetText(m_view.m_body,data);
		string GetBodyText()=>UIListener.GetText(m_view.m_body);
		void _OnEquip_TypeChanged(EventContext data){
			OnEquip_TypeChanged(data);
		}
		partial void OnEquip_TypeChanged(EventContext data);
		void SwitchEquip_TypePage(int index)=>m_view.m_equip.m_type.selectedIndex=index;
		void _OnEquip_QualityChanged(EventContext data){
			OnEquip_QualityChanged(data);
		}
		partial void OnEquip_QualityChanged(EventContext data);
		void SwitchEquip_QualityPage(int index)=>m_view.m_equip.m_quality.selectedIndex=index;
		void _OnEquipClick(EventContext data){
			OnEquipClick(data);
		}
		partial void OnEquipClick(EventContext data);
		void SetEquipText(string data)=>UIListener.SetText(m_view.m_equip,data);
		string GetEquipText()=>UIListener.GetText(m_view.m_equip);
		void _OnEquip_Nextequip_typeChanged(EventContext data){
			OnEquip_Nextequip_typeChanged(data);
		}
		partial void OnEquip_Nextequip_typeChanged(EventContext data);
		void SwitchEquip_Nextequip_typePage(int index)=>m_view.m_nextequip.m_type.selectedIndex=index;
		void _OnEquip_Nextequip_qualityChanged(EventContext data){
			OnEquip_Nextequip_qualityChanged(data);
		}
		partial void OnEquip_Nextequip_qualityChanged(EventContext data);
		void SwitchEquip_Nextequip_qualityPage(int index)=>m_view.m_nextequip.m_quality.selectedIndex=index;
		void _OnNextequipClick(EventContext data){
			OnNextequipClick(data);
		}
		partial void OnNextequipClick(EventContext data);
		void SetNextequipText(string data)=>UIListener.SetText(m_view.m_nextequip,data);
		string GetNextequipText()=>UIListener.GetText(m_view.m_nextequip);
		void SetEquipUpLabel_ValText(string data)=>UIListener.SetText(m_view.m_level.m_val,data);
		string GetEquipUpLabel_ValText()=>UIListener.GetText(m_view.m_level.m_val);
		void SetEquipUpLabel_NextText(string data)=>UIListener.SetText(m_view.m_level.m_next,data);
		string GetEquipUpLabel_NextText()=>UIListener.GetText(m_view.m_level.m_next);
		void _OnLevelClick(EventContext data){
			OnLevelClick(data);
		}
		partial void OnLevelClick(EventContext data);
		void SetLevelText(string data)=>UIListener.SetText(m_view.m_level,data);
		string GetLevelText()=>UIListener.GetText(m_view.m_level);
		void SetEquipUpLabel_Main_valText(string data)=>UIListener.SetText(m_view.m_main.m_val,data);
		string GetEquipUpLabel_Main_valText()=>UIListener.GetText(m_view.m_main.m_val);
		void SetEquipUpLabel_Main_nextText(string data)=>UIListener.SetText(m_view.m_main.m_next,data);
		string GetEquipUpLabel_Main_nextText()=>UIListener.GetText(m_view.m_main.m_next);
		void _OnMainClick(EventContext data){
			OnMainClick(data);
		}
		partial void OnMainClick(EventContext data);
		void SetMainText(string data)=>UIListener.SetText(m_view.m_main,data);
		string GetMainText()=>UIListener.GetText(m_view.m_main);
		void _Onattrlabel_QualityChanged(EventContext data){
			Onattrlabel_QualityChanged(data);
		}
		partial void Onattrlabel_QualityChanged(EventContext data);
		void Switchattrlabel_QualityPage(int index)=>m_view.m_attr.m_quality.selectedIndex=index;
		void _Onattrlabel_LockChanged(EventContext data){
			Onattrlabel_LockChanged(data);
		}
		partial void Onattrlabel_LockChanged(EventContext data);
		void Switchattrlabel_LockPage(int index)=>m_view.m_attr.m_lock.selectedIndex=index;
		void _Onattrlabel_TypeChanged(EventContext data){
			Onattrlabel_TypeChanged(data);
		}
		partial void Onattrlabel_TypeChanged(EventContext data);
		void Switchattrlabel_TypePage(int index)=>m_view.m_attr.m_type.selectedIndex=index;
		void Setattrlabel___titleText(string data)=>UIListener.SetText(m_view.m_attr.m___title,data);
		string Getattrlabel___titleText()=>UIListener.GetText(m_view.m_attr.m___title);
		void _Onattrlabel_InfoClick(EventContext data){
			Onattrlabel_InfoClick(data);
		}
		partial void Onattrlabel_InfoClick(EventContext data);
		void Setattrlabel_Attr_infoText(string data)=>UIListener.SetText(m_view.m_attr.m_info,data);
		string Getattrlabel_Attr_infoText()=>UIListener.GetText(m_view.m_attr.m_info);
		void _OnAttrClick(EventContext data){
			OnAttrClick(data);
		}
		partial void OnAttrClick(EventContext data);
		void SetAttrText(string data)=>UIListener.SetText(m_view.m_attr,data);
		string GetAttrText()=>UIListener.GetText(m_view.m_attr);

	}
}
