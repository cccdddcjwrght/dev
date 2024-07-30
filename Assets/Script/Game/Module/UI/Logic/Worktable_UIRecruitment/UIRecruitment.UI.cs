
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
			m_view.m_select1.m_type.onChanged.Add(new EventCallback1(_OnRecWorkerItem_TypeChanged));
			m_view.m_select1.m_recommand.onChanged.Add(new EventCallback1(_OnRecWorkerItem_RecommandChanged));
			UIListener.ListenerIcon(m_view.m_select1, new EventCallback1(_OnSelect1Click));
			m_view.m_select2.m_type.onChanged.Add(new EventCallback1(_OnRecWorkerItem_Select2_typeChanged));
			m_view.m_select2.m_recommand.onChanged.Add(new EventCallback1(_OnRecWorkerItem_Select2_recommandChanged));
			UIListener.ListenerIcon(m_view.m_select2, new EventCallback1(_OnSelect2Click));

		}
		partial void UnInitUI(UIContext context){
			m_view.m_currency.onChanged.Remove(new EventCallback1(_OnCurrencyChanged));
			m_view.m_type.onChanged.Remove(new EventCallback1(_OnTypeChanged));
			m_view.m_roletype.onChanged.Remove(new EventCallback1(_OnRoletypeChanged));
			m_view.m_recommand.onChanged.Remove(new EventCallback1(_OnRecommandChanged));
			m_view.m_selectctr.onChanged.Remove(new EventCallback1(_OnSelectctrChanged));
			UIListener.Listener(m_view.m_click, new EventCallback1(_OnClickClick),remove:true);
			UIListener.ListenerClose(m_view.m_close, new EventCallback1(DoCloseUIClick),remove:true);
			m_view.m_select1.m_type.onChanged.Remove(new EventCallback1(_OnRecWorkerItem_TypeChanged));
			m_view.m_select1.m_recommand.onChanged.Remove(new EventCallback1(_OnRecWorkerItem_RecommandChanged));
			UIListener.ListenerIcon(m_view.m_select1, new EventCallback1(_OnSelect1Click),remove:true);
			m_view.m_select2.m_type.onChanged.Remove(new EventCallback1(_OnRecWorkerItem_Select2_typeChanged));
			m_view.m_select2.m_recommand.onChanged.Remove(new EventCallback1(_OnRecWorkerItem_Select2_recommandChanged));
			UIListener.ListenerIcon(m_view.m_select2, new EventCallback1(_OnSelect2Click),remove:true);

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
		void _OnRecWorkerItem_TypeChanged(EventContext data){
			OnRecWorkerItem_TypeChanged(data);
		}
		partial void OnRecWorkerItem_TypeChanged(EventContext data);
		void SwitchRecWorkerItem_TypePage(int index)=>m_view.m_select1.m_type.selectedIndex=index;
		void _OnRecWorkerItem_RecommandChanged(EventContext data){
			OnRecWorkerItem_RecommandChanged(data);
		}
		partial void OnRecWorkerItem_RecommandChanged(EventContext data);
		void SwitchRecWorkerItem_RecommandPage(int index)=>m_view.m_select1.m_recommand.selectedIndex=index;
		void SetRecWorkerItem_CountText(string data)=>UIListener.SetText(m_view.m_select1.m_count,data);
		string GetRecWorkerItem_CountText()=>UIListener.GetText(m_view.m_select1.m_count);
		void _OnSelect1Click(EventContext data){
			OnSelect1Click(data);
		}
		partial void OnSelect1Click(EventContext data);
		void _OnRecWorkerItem_Select2_typeChanged(EventContext data){
			OnRecWorkerItem_Select2_typeChanged(data);
		}
		partial void OnRecWorkerItem_Select2_typeChanged(EventContext data);
		void SwitchRecWorkerItem_Select2_typePage(int index)=>m_view.m_select2.m_type.selectedIndex=index;
		void _OnRecWorkerItem_Select2_recommandChanged(EventContext data){
			OnRecWorkerItem_Select2_recommandChanged(data);
		}
		partial void OnRecWorkerItem_Select2_recommandChanged(EventContext data);
		void SwitchRecWorkerItem_Select2_recommandPage(int index)=>m_view.m_select2.m_recommand.selectedIndex=index;
		void SetRecWorkerItem_Select2_countText(string data)=>UIListener.SetText(m_view.m_select2.m_count,data);
		string GetRecWorkerItem_Select2_countText()=>UIListener.GetText(m_view.m_select2.m_count);
		void _OnSelect2Click(EventContext data){
			OnSelect2Click(data);
		}
		partial void OnSelect2Click(EventContext data);

	}
}
