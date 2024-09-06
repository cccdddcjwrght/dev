
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Explore;
	
	public partial class UIExploreAutoSet
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			m_view.m_cdtype.onChanged.Add(new EventCallback1(_OnCdtypeChanged));
			m_view.m_quality.onChanged.Add(new EventCallback1(_OnQualityChanged));
			m_view.m_comparepower.onChanged.Add(new EventCallback1(_OnComparepowerChanged));
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick));
			m_view.m_costselect.m_type.onChanged.Add(new EventCallback1(_OnExploreSelectBtn_TypeChanged));
			m_view.m_costselect.m_quality.onChanged.Add(new EventCallback1(_OnExploreSelectBtn_QualityChanged));
			UIListener.Listener(m_view.m_costselect, new EventCallback1(_OnCostselectClick));
			m_view.m_qualityselect.m_type.onChanged.Add(new EventCallback1(_OnExploreSelectBtn_Qualityselect_typeChanged));
			m_view.m_qualityselect.m_quality.onChanged.Add(new EventCallback1(_OnExploreSelectBtn_Qualityselect_qualityChanged));
			UIListener.Listener(m_view.m_qualityselect, new EventCallback1(_OnQualityselectClick));
			m_view.m_cdtype_2.m_type.onChanged.Add(new EventCallback1(_OnConditionType_TypeChanged));
			UIListener.Listener(m_view.m_cdtype_2, new EventCallback1(_OnCdtype_2Click));
			m_view.m_toggle.m_type.onChanged.Add(new EventCallback1(_OnCheckBox_TypeChanged));
			m_view.m_toggle.m_hidebg.onChanged.Add(new EventCallback1(_OnCheckBox_HidebgChanged));
			UIListener.Listener(m_view.m_toggle, new EventCallback1(_OnToggleClick));
			UIListener.Listener(m_view.m_click, new EventCallback1(_OnClickClick));

		}
		partial void UnInitUI(UIContext context){
			m_view.m_cdtype.onChanged.Remove(new EventCallback1(_OnCdtypeChanged));
			m_view.m_quality.onChanged.Remove(new EventCallback1(_OnQualityChanged));
			m_view.m_comparepower.onChanged.Remove(new EventCallback1(_OnComparepowerChanged));
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick),remove:true);
			m_view.m_costselect.m_type.onChanged.Remove(new EventCallback1(_OnExploreSelectBtn_TypeChanged));
			m_view.m_costselect.m_quality.onChanged.Remove(new EventCallback1(_OnExploreSelectBtn_QualityChanged));
			UIListener.Listener(m_view.m_costselect, new EventCallback1(_OnCostselectClick),remove:true);
			m_view.m_qualityselect.m_type.onChanged.Remove(new EventCallback1(_OnExploreSelectBtn_Qualityselect_typeChanged));
			m_view.m_qualityselect.m_quality.onChanged.Remove(new EventCallback1(_OnExploreSelectBtn_Qualityselect_qualityChanged));
			UIListener.Listener(m_view.m_qualityselect, new EventCallback1(_OnQualityselectClick),remove:true);
			m_view.m_cdtype_2.m_type.onChanged.Remove(new EventCallback1(_OnConditionType_TypeChanged));
			UIListener.Listener(m_view.m_cdtype_2, new EventCallback1(_OnCdtype_2Click),remove:true);
			m_view.m_toggle.m_type.onChanged.Remove(new EventCallback1(_OnCheckBox_TypeChanged));
			m_view.m_toggle.m_hidebg.onChanged.Remove(new EventCallback1(_OnCheckBox_HidebgChanged));
			UIListener.Listener(m_view.m_toggle, new EventCallback1(_OnToggleClick),remove:true);
			UIListener.Listener(m_view.m_click, new EventCallback1(_OnClickClick),remove:true);

		}
		void _OnCdtypeChanged(EventContext data){
			OnCdtypeChanged(data);
		}
		partial void OnCdtypeChanged(EventContext data);
		void SwitchCdtypePage(int index)=>m_view.m_cdtype.selectedIndex=index;
		void _OnQualityChanged(EventContext data){
			OnQualityChanged(data);
		}
		partial void OnQualityChanged(EventContext data);
		void SwitchQualityPage(int index)=>m_view.m_quality.selectedIndex=index;
		void _OnComparepowerChanged(EventContext data){
			OnComparepowerChanged(data);
		}
		partial void OnComparepowerChanged(EventContext data);
		void SwitchComparepowerPage(int index)=>m_view.m_comparepower.selectedIndex=index;
		void DoCloseUIClick(EventContext data){
			 bool __closestate = true;
			 OnUICloseClick(ref __closestate);
			 if(__closestate)SGame.UIUtils.CloseUIByID(__id);
			 
		}
		partial void OnUICloseClick(ref bool state);
		void SetBodyText(string data)=>UIListener.SetText(m_view.m_body,data);
		string GetBodyText()=>UIListener.GetText(m_view.m_body);
		void _OnExploreSelectBtn_TypeChanged(EventContext data){
			OnExploreSelectBtn_TypeChanged(data);
		}
		partial void OnExploreSelectBtn_TypeChanged(EventContext data);
		void SwitchExploreSelectBtn_TypePage(int index)=>m_view.m_costselect.m_type.selectedIndex=index;
		void _OnExploreSelectBtn_QualityChanged(EventContext data){
			OnExploreSelectBtn_QualityChanged(data);
		}
		partial void OnExploreSelectBtn_QualityChanged(EventContext data);
		void SwitchExploreSelectBtn_QualityPage(int index)=>m_view.m_costselect.m_quality.selectedIndex=index;
		void _OnCostselectClick(EventContext data){
			OnCostselectClick(data);
		}
		partial void OnCostselectClick(EventContext data);
		void _OnExploreSelectBtn_Qualityselect_typeChanged(EventContext data){
			OnExploreSelectBtn_Qualityselect_typeChanged(data);
		}
		partial void OnExploreSelectBtn_Qualityselect_typeChanged(EventContext data);
		void SwitchExploreSelectBtn_Qualityselect_typePage(int index)=>m_view.m_qualityselect.m_type.selectedIndex=index;
		void _OnExploreSelectBtn_Qualityselect_qualityChanged(EventContext data){
			OnExploreSelectBtn_Qualityselect_qualityChanged(data);
		}
		partial void OnExploreSelectBtn_Qualityselect_qualityChanged(EventContext data);
		void SwitchExploreSelectBtn_Qualityselect_qualityPage(int index)=>m_view.m_qualityselect.m_quality.selectedIndex=index;
		void _OnQualityselectClick(EventContext data){
			OnQualityselectClick(data);
		}
		partial void OnQualityselectClick(EventContext data);
		void _OnConditionType_TypeChanged(EventContext data){
			OnConditionType_TypeChanged(data);
		}
		partial void OnConditionType_TypeChanged(EventContext data);
		void SwitchConditionType_TypePage(int index)=>m_view.m_cdtype_2.m_type.selectedIndex=index;
		void _OnCdtype_2Click(EventContext data){
			OnCdtype_2Click(data);
		}
		partial void OnCdtype_2Click(EventContext data);
		void SetCdtype_2Text(string data)=>UIListener.SetText(m_view.m_cdtype_2,data);
		string GetCdtype_2Text()=>UIListener.GetText(m_view.m_cdtype_2);
		void _OnCheckBox_TypeChanged(EventContext data){
			OnCheckBox_TypeChanged(data);
		}
		partial void OnCheckBox_TypeChanged(EventContext data);
		void SwitchCheckBox_TypePage(int index)=>m_view.m_toggle.m_type.selectedIndex=index;
		void _OnCheckBox_HidebgChanged(EventContext data){
			OnCheckBox_HidebgChanged(data);
		}
		partial void OnCheckBox_HidebgChanged(EventContext data);
		void SwitchCheckBox_HidebgPage(int index)=>m_view.m_toggle.m_hidebg.selectedIndex=index;
		void _OnToggleClick(EventContext data){
			OnToggleClick(data);
		}
		partial void OnToggleClick(EventContext data);
		void SetToggleText(string data)=>UIListener.SetText(m_view.m_toggle,data);
		string GetToggleText()=>UIListener.GetText(m_view.m_toggle);
		void _OnClickClick(EventContext data){
			OnClickClick(data);
		}
		partial void OnClickClick(EventContext data);
		void SetClickText(string data)=>UIListener.SetText(m_view.m_click,data);
		string GetClickText()=>UIListener.GetText(m_view.m_click);

	}
}
