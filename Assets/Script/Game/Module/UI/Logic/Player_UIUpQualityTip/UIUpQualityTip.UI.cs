
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Player;
	
	public partial class UIUpQualityTip
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			m_view.m_state.onChanged.Add(new EventCallback1(_OnStateChanged));
			m_view.m_addeffect.m_quality.onChanged.Add(new EventCallback1(_Onattrlabel_QualityChanged));
			m_view.m_addeffect.m_lock.onChanged.Add(new EventCallback1(_Onattrlabel_LockChanged));
			UIListener.ListenerIcon(m_view.m_addeffect, new EventCallback1(_OnAddeffectClick));
			m_view.m_equip.m_type.onChanged.Add(new EventCallback1(_OnEquip_TypeChanged));
			m_view.m_equip.m_quality.onChanged.Add(new EventCallback1(_OnEquip_QualityChanged));
			UIListener.Listener(m_view.m_equip, new EventCallback1(_OnEquipClick));
			UIListener.ListenerClose(m_view.m_close, new EventCallback1(DoCloseUIClick));

		}
		partial void UnInitUI(UIContext context){
			m_view.m_state.onChanged.Remove(new EventCallback1(_OnStateChanged));
			m_view.m_addeffect.m_quality.onChanged.Remove(new EventCallback1(_Onattrlabel_QualityChanged));
			m_view.m_addeffect.m_lock.onChanged.Remove(new EventCallback1(_Onattrlabel_LockChanged));
			UIListener.ListenerIcon(m_view.m_addeffect, new EventCallback1(_OnAddeffectClick),remove:true);
			m_view.m_equip.m_type.onChanged.Remove(new EventCallback1(_OnEquip_TypeChanged));
			m_view.m_equip.m_quality.onChanged.Remove(new EventCallback1(_OnEquip_QualityChanged));
			UIListener.Listener(m_view.m_equip, new EventCallback1(_OnEquipClick),remove:true);
			UIListener.ListenerClose(m_view.m_close, new EventCallback1(DoCloseUIClick),remove:true);

		}
		void _OnStateChanged(EventContext data){
			OnStateChanged(data);
		}
		partial void OnStateChanged(EventContext data);
		void SwitchStatePage(int index)=>m_view.m_state.selectedIndex=index;
		void SetQnameText(string data)=>UIListener.SetText(m_view.m_qname,data);
		string GetQnameText()=>UIListener.GetText(m_view.m_qname);
		void SetAttributeText(string data)=>UIListener.SetText(m_view.m_attribute,data);
		string GetAttributeText()=>UIListener.GetText(m_view.m_attribute);
		void SetRecycleText(string data)=>UIListener.SetText(m_view.m_recycle,data);
		string GetRecycleText()=>UIListener.GetText(m_view.m_recycle);
		void _Onattrlabel_QualityChanged(EventContext data){
			Onattrlabel_QualityChanged(data);
		}
		partial void Onattrlabel_QualityChanged(EventContext data);
		void Switchattrlabel_QualityPage(int index)=>m_view.m_addeffect.m_quality.selectedIndex=index;
		void _Onattrlabel_LockChanged(EventContext data){
			Onattrlabel_LockChanged(data);
		}
		partial void Onattrlabel_LockChanged(EventContext data);
		void Switchattrlabel_LockPage(int index)=>m_view.m_addeffect.m_lock.selectedIndex=index;
		void _OnAddeffectClick(EventContext data){
			OnAddeffectClick(data);
		}
		partial void OnAddeffectClick(EventContext data);
		void SetAddeffectText(string data)=>UIListener.SetText(m_view.m_addeffect,data);
		string GetAddeffectText()=>UIListener.GetText(m_view.m_addeffect);
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
		void DoCloseUIClick(EventContext data){
			 bool __closestate = true;
			 OnUICloseClick(ref __closestate);
			 if(__closestate)SGame.UIUtils.CloseUIByID(__id);
			 
		}
		partial void OnUICloseClick(ref bool state);
		void SetCloseText(string data)=>UIListener.SetText(m_view.m_close,data);
		string GetCloseText()=>UIListener.GetText(m_view.m_close);

	}
}
