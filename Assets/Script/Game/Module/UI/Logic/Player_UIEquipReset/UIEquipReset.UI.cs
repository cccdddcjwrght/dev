
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Player;
	
	public partial class UIEquipReset
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			m_view.m_func.onChanged.Add(new EventCallback1(_OnFuncChanged));
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick));
			m_view.m_equip.m_type.onChanged.Add(new EventCallback1(_OnEquip_TypeChanged));
			m_view.m_equip.m_quality.onChanged.Add(new EventCallback1(_OnEquip_QualityChanged));
			UIListener.Listener(m_view.m_equip, new EventCallback1(_OnEquipClick));
			UIListener.Listener(m_view.m_click, new EventCallback1(_OnClickClick));
			UIListener.Listener(m_view.m_click2, new EventCallback1(_OnClick2Click));

		}
		partial void UnInitUI(UIContext context){
			m_view.m_func.onChanged.Remove(new EventCallback1(_OnFuncChanged));
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick),remove:true);
			m_view.m_equip.m_type.onChanged.Remove(new EventCallback1(_OnEquip_TypeChanged));
			m_view.m_equip.m_quality.onChanged.Remove(new EventCallback1(_OnEquip_QualityChanged));
			UIListener.Listener(m_view.m_equip, new EventCallback1(_OnEquipClick),remove:true);
			UIListener.Listener(m_view.m_click, new EventCallback1(_OnClickClick),remove:true);
			UIListener.Listener(m_view.m_click2, new EventCallback1(_OnClick2Click),remove:true);

		}
		void _OnFuncChanged(EventContext data){
			OnFuncChanged(data);
		}
		partial void OnFuncChanged(EventContext data);
		void SwitchFuncPage(int index)=>m_view.m_func.selectedIndex=index;
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
		void _OnClickClick(EventContext data){
			OnClickClick(data);
		}
		partial void OnClickClick(EventContext data);
		void SetClickText(string data)=>UIListener.SetText(m_view.m_click,data);
		string GetClickText()=>UIListener.GetText(m_view.m_click);
		void _OnClick2Click(EventContext data){
			OnClick2Click(data);
		}
		partial void OnClick2Click(EventContext data);
		void SetClick2Text(string data)=>UIListener.SetText(m_view.m_click2,data);
		string GetClick2Text()=>UIListener.GetText(m_view.m_click2);

	}
}
