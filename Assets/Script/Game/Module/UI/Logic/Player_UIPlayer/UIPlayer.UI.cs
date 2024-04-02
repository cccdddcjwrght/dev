
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Player;
	
	public partial class UIPlayer
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			m_view.m_eqTab.onChanged.Add(new EventCallback1(_OnEqTabChanged));
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick));
			UIListener.Listener(m_view.m_attrbtn, new EventCallback1(_OnAttrbtnClick));
			m_view.m_eq5.m_quality.onChanged.Add(new EventCallback1(_OnEqPos_QualityChanged));
			m_view.m_eq5.m_eq.onChanged.Add(new EventCallback1(_OnEqPos_EqChanged));
			UIListener.Listener(m_view.m_eq5, new EventCallback1(_OnEq5Click));
			m_view.m_eq4.m_quality.onChanged.Add(new EventCallback1(_OnEqPos_Eq4_qualityChanged));
			m_view.m_eq4.m_eq.onChanged.Add(new EventCallback1(_OnEqPos_Eq4qChanged));
			UIListener.Listener(m_view.m_eq4, new EventCallback1(_OnEq4Click));
			m_view.m_eq1.m_quality.onChanged.Add(new EventCallback1(_OnEqPos_Eq1_qualityChanged));
			m_view.m_eq1.m_eq.onChanged.Add(new EventCallback1(_OnEqPos_Eq1qChanged));
			UIListener.Listener(m_view.m_eq1, new EventCallback1(_OnEq1Click));
			m_view.m_eq2.m_quality.onChanged.Add(new EventCallback1(_OnEqPos_Eq2_qualityChanged));
			m_view.m_eq2.m_eq.onChanged.Add(new EventCallback1(_OnEqPos_Eq2qChanged));
			UIListener.Listener(m_view.m_eq2, new EventCallback1(_OnEq2Click));
			m_view.m_eq3.m_quality.onChanged.Add(new EventCallback1(_OnEqPos_Eq3_qualityChanged));
			m_view.m_eq3.m_eq.onChanged.Add(new EventCallback1(_OnEqPos_Eq3qChanged));
			UIListener.Listener(m_view.m_eq3, new EventCallback1(_OnEq3Click));

		}
		partial void UnInitUI(UIContext context){
			m_view.m_eqTab.onChanged.Remove(new EventCallback1(_OnEqTabChanged));
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick),remove:true);
			UIListener.Listener(m_view.m_attrbtn, new EventCallback1(_OnAttrbtnClick),remove:true);
			m_view.m_eq5.m_quality.onChanged.Remove(new EventCallback1(_OnEqPos_QualityChanged));
			m_view.m_eq5.m_eq.onChanged.Remove(new EventCallback1(_OnEqPos_EqChanged));
			UIListener.Listener(m_view.m_eq5, new EventCallback1(_OnEq5Click),remove:true);
			m_view.m_eq4.m_quality.onChanged.Remove(new EventCallback1(_OnEqPos_Eq4_qualityChanged));
			m_view.m_eq4.m_eq.onChanged.Remove(new EventCallback1(_OnEqPos_Eq4qChanged));
			UIListener.Listener(m_view.m_eq4, new EventCallback1(_OnEq4Click),remove:true);
			m_view.m_eq1.m_quality.onChanged.Remove(new EventCallback1(_OnEqPos_Eq1_qualityChanged));
			m_view.m_eq1.m_eq.onChanged.Remove(new EventCallback1(_OnEqPos_Eq1qChanged));
			UIListener.Listener(m_view.m_eq1, new EventCallback1(_OnEq1Click),remove:true);
			m_view.m_eq2.m_quality.onChanged.Remove(new EventCallback1(_OnEqPos_Eq2_qualityChanged));
			m_view.m_eq2.m_eq.onChanged.Remove(new EventCallback1(_OnEqPos_Eq2qChanged));
			UIListener.Listener(m_view.m_eq2, new EventCallback1(_OnEq2Click),remove:true);
			m_view.m_eq3.m_quality.onChanged.Remove(new EventCallback1(_OnEqPos_Eq3_qualityChanged));
			m_view.m_eq3.m_eq.onChanged.Remove(new EventCallback1(_OnEqPos_Eq3qChanged));
			UIListener.Listener(m_view.m_eq3, new EventCallback1(_OnEq3Click),remove:true);

		}
		void _OnEqTabChanged(EventContext data){
			OnEqTabChanged(data);
		}
		partial void OnEqTabChanged(EventContext data);
		void SwitchEqTabPage(int index)=>m_view.m_eqTab.selectedIndex=index;
		void DoCloseUIClick(EventContext data){
			 bool __closestate = true;
			 OnUICloseClick(ref __closestate);
			 if(__closestate)SGame.UIUtils.CloseUIByID(__id);
			 
		}
		partial void OnUICloseClick(ref bool state);
		void SetBodyText(string data)=>UIListener.SetText(m_view.m_body,data);
		string GetBodyText()=>UIListener.GetText(m_view.m_body);
		void _OnAttrbtnClick(EventContext data){
			OnAttrbtnClick(data);
		}
		partial void OnAttrbtnClick(EventContext data);
		void SetAttrbtnText(string data)=>UIListener.SetText(m_view.m_attrbtn,data);
		string GetAttrbtnText()=>UIListener.GetText(m_view.m_attrbtn);
		void SetAttrText(string data)=>UIListener.SetText(m_view.m_attr,data);
		string GetAttrText()=>UIListener.GetText(m_view.m_attr);
		void _OnEqPos_QualityChanged(EventContext data){
			OnEqPos_QualityChanged(data);
		}
		partial void OnEqPos_QualityChanged(EventContext data);
		void SwitchEqPos_QualityPage(int index)=>m_view.m_eq5.m_quality.selectedIndex=index;
		void _OnEqPos_EqChanged(EventContext data){
			OnEqPos_EqChanged(data);
		}
		partial void OnEqPos_EqChanged(EventContext data);
		void SwitchEqPos_EqPage(int index)=>m_view.m_eq5.m_eq.selectedIndex=index;
		void SetEqPos_LevelText(string data)=>UIListener.SetText(m_view.m_eq5.m_level,data);
		string GetEqPos_LevelText()=>UIListener.GetText(m_view.m_eq5.m_level);
		void _OnEq5Click(EventContext data){
			OnEq5Click(data);
		}
		partial void OnEq5Click(EventContext data);
		void SetEq5Text(string data)=>UIListener.SetText(m_view.m_eq5,data);
		string GetEq5Text()=>UIListener.GetText(m_view.m_eq5);
		void _OnEqPos_Eq4_qualityChanged(EventContext data){
			OnEqPos_Eq4_qualityChanged(data);
		}
		partial void OnEqPos_Eq4_qualityChanged(EventContext data);
		void SwitchEqPos_Eq4_qualityPage(int index)=>m_view.m_eq4.m_quality.selectedIndex=index;
		void _OnEqPos_Eq4qChanged(EventContext data){
			OnEqPos_Eq4qChanged(data);
		}
		partial void OnEqPos_Eq4qChanged(EventContext data);
		void SwitchEqPos_Eq4qPage(int index)=>m_view.m_eq4.m_eq.selectedIndex=index;
		void SetEqPos_Eq4_levelText(string data)=>UIListener.SetText(m_view.m_eq4.m_level,data);
		string GetEqPos_Eq4_levelText()=>UIListener.GetText(m_view.m_eq4.m_level);
		void _OnEq4Click(EventContext data){
			OnEq4Click(data);
		}
		partial void OnEq4Click(EventContext data);
		void SetEq4Text(string data)=>UIListener.SetText(m_view.m_eq4,data);
		string GetEq4Text()=>UIListener.GetText(m_view.m_eq4);
		void _OnEqPos_Eq1_qualityChanged(EventContext data){
			OnEqPos_Eq1_qualityChanged(data);
		}
		partial void OnEqPos_Eq1_qualityChanged(EventContext data);
		void SwitchEqPos_Eq1_qualityPage(int index)=>m_view.m_eq1.m_quality.selectedIndex=index;
		void _OnEqPos_Eq1qChanged(EventContext data){
			OnEqPos_Eq1qChanged(data);
		}
		partial void OnEqPos_Eq1qChanged(EventContext data);
		void SwitchEqPos_Eq1qPage(int index)=>m_view.m_eq1.m_eq.selectedIndex=index;
		void SetEqPos_Eq1_levelText(string data)=>UIListener.SetText(m_view.m_eq1.m_level,data);
		string GetEqPos_Eq1_levelText()=>UIListener.GetText(m_view.m_eq1.m_level);
		void _OnEq1Click(EventContext data){
			OnEq1Click(data);
		}
		partial void OnEq1Click(EventContext data);
		void SetEq1Text(string data)=>UIListener.SetText(m_view.m_eq1,data);
		string GetEq1Text()=>UIListener.GetText(m_view.m_eq1);
		void _OnEqPos_Eq2_qualityChanged(EventContext data){
			OnEqPos_Eq2_qualityChanged(data);
		}
		partial void OnEqPos_Eq2_qualityChanged(EventContext data);
		void SwitchEqPos_Eq2_qualityPage(int index)=>m_view.m_eq2.m_quality.selectedIndex=index;
		void _OnEqPos_Eq2qChanged(EventContext data){
			OnEqPos_Eq2qChanged(data);
		}
		partial void OnEqPos_Eq2qChanged(EventContext data);
		void SwitchEqPos_Eq2qPage(int index)=>m_view.m_eq2.m_eq.selectedIndex=index;
		void SetEqPos_Eq2_levelText(string data)=>UIListener.SetText(m_view.m_eq2.m_level,data);
		string GetEqPos_Eq2_levelText()=>UIListener.GetText(m_view.m_eq2.m_level);
		void _OnEq2Click(EventContext data){
			OnEq2Click(data);
		}
		partial void OnEq2Click(EventContext data);
		void SetEq2Text(string data)=>UIListener.SetText(m_view.m_eq2,data);
		string GetEq2Text()=>UIListener.GetText(m_view.m_eq2);
		void _OnEqPos_Eq3_qualityChanged(EventContext data){
			OnEqPos_Eq3_qualityChanged(data);
		}
		partial void OnEqPos_Eq3_qualityChanged(EventContext data);
		void SwitchEqPos_Eq3_qualityPage(int index)=>m_view.m_eq3.m_quality.selectedIndex=index;
		void _OnEqPos_Eq3qChanged(EventContext data){
			OnEqPos_Eq3qChanged(data);
		}
		partial void OnEqPos_Eq3qChanged(EventContext data);
		void SwitchEqPos_Eq3qPage(int index)=>m_view.m_eq3.m_eq.selectedIndex=index;
		void SetEqPos_Eq3_levelText(string data)=>UIListener.SetText(m_view.m_eq3.m_level,data);
		string GetEqPos_Eq3_levelText()=>UIListener.GetText(m_view.m_eq3.m_level);
		void _OnEq3Click(EventContext data){
			OnEq3Click(data);
		}
		partial void OnEq3Click(EventContext data);
		void SetEq3Text(string data)=>UIListener.SetText(m_view.m_eq3,data);
		string GetEq3Text()=>UIListener.GetText(m_view.m_eq3);

	}
}
