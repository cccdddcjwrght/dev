
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Worktable;
	
	public partial class UIRecruitment
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			m_view.m_currency.onChanged.Add(new EventCallback1(_OnCurrencyChanged));
			m_view.m_type.onChanged.Add(new EventCallback1(_OnTypeChanged));
			m_view.m_roletype.onChanged.Add(new EventCallback1(_OnRoletypeChanged));
			m_view.m_recommand.onChanged.Add(new EventCallback1(_OnRecommandChanged));
			m_view.m_selectctr.onChanged.Add(new EventCallback1(_OnSelectctrChanged));
			UIListener.Listener(m_view.m_click, new EventCallback1(_OnClickClick));
			UIListener.ListenerClose(m_view.m_close, new EventCallback1(DoCloseUIClick));
			m_view.m_roles.m_roletype.onChanged.Add(new EventCallback1(_OnRecBody_RoletypeChanged));
			m_view.m_roles.m_recommand.onChanged.Add(new EventCallback1(_OnRecBody_RecommandChanged));
			m_view.m_roles.m_selectctr.onChanged.Add(new EventCallback1(_OnRecBody_SelectctrChanged));
			m_view.m_roles.m_select1.m_type.onChanged.Add(new EventCallback1(_OnRecWorkerItem_Roles_select1_typeChanged));
			m_view.m_roles.m_select1.m_recommand.onChanged.Add(new EventCallback1(_OnRecWorkerItem_Roles_select1_recommandChanged));
			UIListener.Listener(m_view.m_roles.m_select1, new EventCallback1(_OnRecBody_Select1Click));
			m_view.m_roles.m_select2.m_type.onChanged.Add(new EventCallback1(_OnRecWorkerItem_Roles_select2_typeChanged));
			m_view.m_roles.m_select2.m_recommand.onChanged.Add(new EventCallback1(_OnRecWorkerItem_Roles_select2_recommandChanged));
			UIListener.Listener(m_view.m_roles.m_select2, new EventCallback1(_OnRecBody_Select2Click));
			UIListener.ListenerIcon(m_view.m_roles, new EventCallback1(_OnRolesClick));

		}
		partial void UnInitUI(UIContext context){
			m_view.m_currency.onChanged.Remove(new EventCallback1(_OnCurrencyChanged));
			m_view.m_type.onChanged.Remove(new EventCallback1(_OnTypeChanged));
			m_view.m_roletype.onChanged.Remove(new EventCallback1(_OnRoletypeChanged));
			m_view.m_recommand.onChanged.Remove(new EventCallback1(_OnRecommandChanged));
			m_view.m_selectctr.onChanged.Remove(new EventCallback1(_OnSelectctrChanged));
			UIListener.Listener(m_view.m_click, new EventCallback1(_OnClickClick),remove:true);
			UIListener.ListenerClose(m_view.m_close, new EventCallback1(DoCloseUIClick),remove:true);
			m_view.m_roles.m_roletype.onChanged.Remove(new EventCallback1(_OnRecBody_RoletypeChanged));
			m_view.m_roles.m_recommand.onChanged.Remove(new EventCallback1(_OnRecBody_RecommandChanged));
			m_view.m_roles.m_selectctr.onChanged.Remove(new EventCallback1(_OnRecBody_SelectctrChanged));
			m_view.m_roles.m_select1.m_type.onChanged.Remove(new EventCallback1(_OnRecWorkerItem_Roles_select1_typeChanged));
			m_view.m_roles.m_select1.m_recommand.onChanged.Remove(new EventCallback1(_OnRecWorkerItem_Roles_select1_recommandChanged));
			UIListener.Listener(m_view.m_roles.m_select1, new EventCallback1(_OnRecBody_Select1Click),remove:true);
			m_view.m_roles.m_select2.m_type.onChanged.Remove(new EventCallback1(_OnRecWorkerItem_Roles_select2_typeChanged));
			m_view.m_roles.m_select2.m_recommand.onChanged.Remove(new EventCallback1(_OnRecWorkerItem_Roles_select2_recommandChanged));
			UIListener.Listener(m_view.m_roles.m_select2, new EventCallback1(_OnRecBody_Select2Click),remove:true);
			UIListener.ListenerIcon(m_view.m_roles, new EventCallback1(_OnRolesClick),remove:true);

		}
		void _OnCurrencyChanged(EventContext data){
			OnCurrencyChanged(data);
		}
		partial void OnCurrencyChanged(EventContext data);
		void SwitchCurrencyPage(int index)=>m_view.m_currency.selectedIndex=index;
		void _OnTypeChanged(EventContext data){
			OnTypeChanged(data);
		}
		partial void OnTypeChanged(EventContext data);
		void SwitchTypePage(int index)=>m_view.m_type.selectedIndex=index;
		void _OnRoletypeChanged(EventContext data){
			OnRoletypeChanged(data);
		}
		partial void OnRoletypeChanged(EventContext data);
		void SwitchRoletypePage(int index)=>m_view.m_roletype.selectedIndex=index;
		void _OnRecommandChanged(EventContext data){
			OnRecommandChanged(data);
		}
		partial void OnRecommandChanged(EventContext data);
		void SwitchRecommandPage(int index)=>m_view.m_recommand.selectedIndex=index;
		void _OnSelectctrChanged(EventContext data){
			OnSelectctrChanged(data);
		}
		partial void OnSelectctrChanged(EventContext data);
		void SwitchSelectctrPage(int index)=>m_view.m_selectctr.selectedIndex=index;
		void SetDescText(string data)=>UIListener.SetText(m_view.m_desc,data);
		string GetDescText()=>UIListener.GetText(m_view.m_desc);
		void SetAreatipsText(string data)=>UIListener.SetText(m_view.m_areatips,data);
		string GetAreatipsText()=>UIListener.GetText(m_view.m_areatips);
		void SetCostText(string data)=>UIListener.SetText(m_view.m_cost,data);
		string GetCostText()=>UIListener.GetText(m_view.m_cost);
		void _OnClickClick(EventContext data){
			OnClickClick(data);
		}
		partial void OnClickClick(EventContext data);
		void SetClickText(string data)=>UIListener.SetText(m_view.m_click,data);
		string GetClickText()=>UIListener.GetText(m_view.m_click);
		void DoCloseUIClick(EventContext data){
			 bool __closestate = true;
			 OnUICloseClick(ref __closestate);
			 if(__closestate)SGame.UIUtils.CloseUIByID(__id);
			 
		}
		partial void OnUICloseClick(ref bool state);
		void SetCloseText(string data)=>UIListener.SetText(m_view.m_close,data);
		string GetCloseText()=>UIListener.GetText(m_view.m_close);
		void _OnRecBody_RoletypeChanged(EventContext data){
			OnRecBody_RoletypeChanged(data);
		}
		partial void OnRecBody_RoletypeChanged(EventContext data);
		void SwitchRecBody_RoletypePage(int index)=>m_view.m_roles.m_roletype.selectedIndex=index;
		void _OnRecBody_RecommandChanged(EventContext data){
			OnRecBody_RecommandChanged(data);
		}
		partial void OnRecBody_RecommandChanged(EventContext data);
		void SwitchRecBody_RecommandPage(int index)=>m_view.m_roles.m_recommand.selectedIndex=index;
		void _OnRecBody_SelectctrChanged(EventContext data){
			OnRecBody_SelectctrChanged(data);
		}
		partial void OnRecBody_SelectctrChanged(EventContext data);
		void SwitchRecBody_SelectctrPage(int index)=>m_view.m_roles.m_selectctr.selectedIndex=index;
		void _OnRecWorkerItem_Roles_select1_typeChanged(EventContext data){
			OnRecWorkerItem_Roles_select1_typeChanged(data);
		}
		partial void OnRecWorkerItem_Roles_select1_typeChanged(EventContext data);
		void SwitchRecWorkerItem_Roles_select1_typePage(int index)=>m_view.m_roles.m_select1.m_type.selectedIndex=index;
		void _OnRecWorkerItem_Roles_select1_recommandChanged(EventContext data){
			OnRecWorkerItem_Roles_select1_recommandChanged(data);
		}
		partial void OnRecWorkerItem_Roles_select1_recommandChanged(EventContext data);
		void SwitchRecWorkerItem_Roles_select1_recommandPage(int index)=>m_view.m_roles.m_select1.m_recommand.selectedIndex=index;
		void SetRecWorkerItem_Roles_select1_countText(string data)=>UIListener.SetText(m_view.m_roles.m_select1.m_count,data);
		string GetRecWorkerItem_Roles_select1_countText()=>UIListener.GetText(m_view.m_roles.m_select1.m_count);
		void _OnRecBody_Select1Click(EventContext data){
			OnRecBody_Select1Click(data);
		}
		partial void OnRecBody_Select1Click(EventContext data);
		void SetRecBody_Roles_select1Text(string data)=>UIListener.SetText(m_view.m_roles.m_select1,data);
		string GetRecBody_Roles_select1Text()=>UIListener.GetText(m_view.m_roles.m_select1);
		void _OnRecWorkerItem_Roles_select2_typeChanged(EventContext data){
			OnRecWorkerItem_Roles_select2_typeChanged(data);
		}
		partial void OnRecWorkerItem_Roles_select2_typeChanged(EventContext data);
		void SwitchRecWorkerItem_Roles_select2_typePage(int index)=>m_view.m_roles.m_select2.m_type.selectedIndex=index;
		void _OnRecWorkerItem_Roles_select2_recommandChanged(EventContext data){
			OnRecWorkerItem_Roles_select2_recommandChanged(data);
		}
		partial void OnRecWorkerItem_Roles_select2_recommandChanged(EventContext data);
		void SwitchRecWorkerItem_Roles_select2_recommandPage(int index)=>m_view.m_roles.m_select2.m_recommand.selectedIndex=index;
		void SetRecWorkerItem_Roles_select2_countText(string data)=>UIListener.SetText(m_view.m_roles.m_select2.m_count,data);
		string GetRecWorkerItem_Roles_select2_countText()=>UIListener.GetText(m_view.m_roles.m_select2.m_count);
		void _OnRecBody_Select2Click(EventContext data){
			OnRecBody_Select2Click(data);
		}
		partial void OnRecBody_Select2Click(EventContext data);
		void SetRecBody_Roles_select2Text(string data)=>UIListener.SetText(m_view.m_roles.m_select2,data);
		string GetRecBody_Roles_select2Text()=>UIListener.GetText(m_view.m_roles.m_select2);
		void _OnRolesClick(EventContext data){
			OnRolesClick(data);
		}
		partial void OnRolesClick(EventContext data);

	}
}
