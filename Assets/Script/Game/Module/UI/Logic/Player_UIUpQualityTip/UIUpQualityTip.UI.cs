
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
			m_view.m_body.m_state.onChanged.Add(new EventCallback1(_OnUpQualityTipBody_StateChanged));
			m_view.m_body.m_equip.m_type.onChanged.Add(new EventCallback1(_OnEquip_Bodyquip_typeChanged));
			m_view.m_body.m_equip.m_quality.onChanged.Add(new EventCallback1(_OnEquip_Bodyquip_qualityChanged));
			UIListener.Listener(m_view.m_body.m_equip, new EventCallback1(_OnUpQualityTipBody_EquipClick));
			m_view.m_body.m_attr.m_quality.onChanged.Add(new EventCallback1(_Onattrlabel_Body_attr_qualityChanged));
			m_view.m_body.m_attr.m_lock.onChanged.Add(new EventCallback1(_Onattrlabel_Body_attr_lockChanged));
			m_view.m_body.m_attr.m_type.onChanged.Add(new EventCallback1(_Onattrlabel_Body_attr_typeChanged));
			UIListener.Listener(m_view.m_body.m_attr.m_info, new EventCallback1(_Onattrlabel_Body_attr_infoClick));
			UIListener.ListenerIcon(m_view.m_body.m_attr, new EventCallback1(_OnUpQualityTipBody_AttrClick));
			UIListener.ListenerClose(m_view.m_body.m_close, new EventCallback1(DoCloseUIClick));
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick));

		}
		partial void UnInitUI(UIContext context){
			m_view.m_body.m_state.onChanged.Remove(new EventCallback1(_OnUpQualityTipBody_StateChanged));
			m_view.m_body.m_equip.m_type.onChanged.Remove(new EventCallback1(_OnEquip_Bodyquip_typeChanged));
			m_view.m_body.m_equip.m_quality.onChanged.Remove(new EventCallback1(_OnEquip_Bodyquip_qualityChanged));
			UIListener.Listener(m_view.m_body.m_equip, new EventCallback1(_OnUpQualityTipBody_EquipClick),remove:true);
			m_view.m_body.m_attr.m_quality.onChanged.Remove(new EventCallback1(_Onattrlabel_Body_attr_qualityChanged));
			m_view.m_body.m_attr.m_lock.onChanged.Remove(new EventCallback1(_Onattrlabel_Body_attr_lockChanged));
			m_view.m_body.m_attr.m_type.onChanged.Remove(new EventCallback1(_Onattrlabel_Body_attr_typeChanged));
			UIListener.Listener(m_view.m_body.m_attr.m_info, new EventCallback1(_Onattrlabel_Body_attr_infoClick),remove:true);
			UIListener.ListenerIcon(m_view.m_body.m_attr, new EventCallback1(_OnUpQualityTipBody_AttrClick),remove:true);
			UIListener.ListenerClose(m_view.m_body.m_close, new EventCallback1(DoCloseUIClick),remove:true);
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick),remove:true);

		}
		void _OnUpQualityTipBody_StateChanged(EventContext data){
			OnUpQualityTipBody_StateChanged(data);
		}
		partial void OnUpQualityTipBody_StateChanged(EventContext data);
		void SwitchUpQualityTipBody_StatePage(int index)=>m_view.m_body.m_state.selectedIndex=index;
		void SetUpQualityTipBody_QnameText(string data)=>UIListener.SetText(m_view.m_body.m_qname,data);
		string GetUpQualityTipBody_QnameText()=>UIListener.GetText(m_view.m_body.m_qname);
		void SetUpQualityTipBody_AttributeText(string data)=>UIListener.SetText(m_view.m_body.m_attribute,data);
		string GetUpQualityTipBody_AttributeText()=>UIListener.GetText(m_view.m_body.m_attribute);
		void SetUpQualityTipBody_TipsText(string data)=>UIListener.SetText(m_view.m_body.m_tips,data);
		string GetUpQualityTipBody_TipsText()=>UIListener.GetText(m_view.m_body.m_tips);
		void _OnEquip_Bodyquip_typeChanged(EventContext data){
			OnEquip_Bodyquip_typeChanged(data);
		}
		partial void OnEquip_Bodyquip_typeChanged(EventContext data);
		void SwitchEquip_Bodyquip_typePage(int index)=>m_view.m_body.m_equip.m_type.selectedIndex=index;
		void _OnEquip_Bodyquip_qualityChanged(EventContext data){
			OnEquip_Bodyquip_qualityChanged(data);
		}
		partial void OnEquip_Bodyquip_qualityChanged(EventContext data);
		void SwitchEquip_Bodyquip_qualityPage(int index)=>m_view.m_body.m_equip.m_quality.selectedIndex=index;
		void _OnUpQualityTipBody_EquipClick(EventContext data){
			OnUpQualityTipBody_EquipClick(data);
		}
		partial void OnUpQualityTipBody_EquipClick(EventContext data);
		void SetUpQualityTipBody_BodyquipText(string data)=>UIListener.SetText(m_view.m_body.m_equip,data);
		string GetUpQualityTipBody_BodyquipText()=>UIListener.GetText(m_view.m_body.m_equip);
		void _Onattrlabel_Body_attr_qualityChanged(EventContext data){
			Onattrlabel_Body_attr_qualityChanged(data);
		}
		partial void Onattrlabel_Body_attr_qualityChanged(EventContext data);
		void Switchattrlabel_Body_attr_qualityPage(int index)=>m_view.m_body.m_attr.m_quality.selectedIndex=index;
		void _Onattrlabel_Body_attr_lockChanged(EventContext data){
			Onattrlabel_Body_attr_lockChanged(data);
		}
		partial void Onattrlabel_Body_attr_lockChanged(EventContext data);
		void Switchattrlabel_Body_attr_lockPage(int index)=>m_view.m_body.m_attr.m_lock.selectedIndex=index;
		void _Onattrlabel_Body_attr_typeChanged(EventContext data){
			Onattrlabel_Body_attr_typeChanged(data);
		}
		partial void Onattrlabel_Body_attr_typeChanged(EventContext data);
		void Switchattrlabel_Body_attr_typePage(int index)=>m_view.m_body.m_attr.m_type.selectedIndex=index;
		void Setattrlabel_Body_attr___titleText(string data)=>UIListener.SetText(m_view.m_body.m_attr.m___title,data);
		string Getattrlabel_Body_attr___titleText()=>UIListener.GetText(m_view.m_body.m_attr.m___title);
		void _Onattrlabel_Body_attr_infoClick(EventContext data){
			Onattrlabel_Body_attr_infoClick(data);
		}
		partial void Onattrlabel_Body_attr_infoClick(EventContext data);
		void Setattrlabel_Body_attr_infoText(string data)=>UIListener.SetText(m_view.m_body.m_attr.m_info,data);
		string Getattrlabel_Body_attr_infoText()=>UIListener.GetText(m_view.m_body.m_attr.m_info);
		void _OnUpQualityTipBody_AttrClick(EventContext data){
			OnUpQualityTipBody_AttrClick(data);
		}
		partial void OnUpQualityTipBody_AttrClick(EventContext data);
		void SetUpQualityTipBody_Body_attrText(string data)=>UIListener.SetText(m_view.m_body.m_attr,data);
		string GetUpQualityTipBody_Body_attrText()=>UIListener.GetText(m_view.m_body.m_attr);
		void SetUpQualityTipBody_RecycleText(string data)=>UIListener.SetText(m_view.m_body.m_recycle,data);
		string GetUpQualityTipBody_RecycleText()=>UIListener.GetText(m_view.m_body.m_recycle);
		void DoCloseUIClick(EventContext data){
			 bool __closestate = true;
			 OnUICloseClick(ref __closestate);
			 if(__closestate)SGame.UIUtils.CloseUIByID(__id);
			 
		}
		partial void OnUICloseClick(ref bool state);
		void SetUpQualityTipBody_Body_closeText(string data)=>UIListener.SetText(m_view.m_body.m_close,data);
		string GetUpQualityTipBody_Body_closeText()=>UIListener.GetText(m_view.m_body.m_close);
		void SetBodyText(string data)=>UIListener.SetText(m_view.m_body,data);
		string GetBodyText()=>UIListener.GetText(m_view.m_body);

	}
}
