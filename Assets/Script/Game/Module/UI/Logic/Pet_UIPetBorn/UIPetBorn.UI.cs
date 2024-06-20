
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Pet;
	
	public partial class UIPetBorn
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			m_view.m_quality.onChanged.Add(new EventCallback1(_OnQualityChanged));
			m_view.m_type.onChanged.Add(new EventCallback1(_OnTypeChanged));
			UIListener.ListenerIcon(m_view.m_model, new EventCallback1(_OnModelClick));
			m_view.m_change.m_type.onChanged.Add(new EventCallback1(_OnChangeProperty_TypeChanged));
			UIListener.ListenerIcon(m_view.m_change, new EventCallback1(_OnChangeClick));

		}
		partial void UnInitUI(UIContext context){
			m_view.m_quality.onChanged.Remove(new EventCallback1(_OnQualityChanged));
			m_view.m_type.onChanged.Remove(new EventCallback1(_OnTypeChanged));
			UIListener.ListenerIcon(m_view.m_model, new EventCallback1(_OnModelClick),remove:true);
			m_view.m_change.m_type.onChanged.Remove(new EventCallback1(_OnChangeProperty_TypeChanged));
			UIListener.ListenerIcon(m_view.m_change, new EventCallback1(_OnChangeClick),remove:true);

		}
		void _OnQualityChanged(EventContext data){
			OnQualityChanged(data);
		}
		partial void OnQualityChanged(EventContext data);
		void SwitchQualityPage(int index)=>m_view.m_quality.selectedIndex=index;
		void _OnTypeChanged(EventContext data){
			OnTypeChanged(data);
		}
		partial void OnTypeChanged(EventContext data);
		void SwitchTypePage(int index)=>m_view.m_type.selectedIndex=index;
		void _OnModelClick(EventContext data){
			OnModelClick(data);
		}
		partial void OnModelClick(EventContext data);
		void _OnChangeProperty_TypeChanged(EventContext data){
			OnChangeProperty_TypeChanged(data);
		}
		partial void OnChangeProperty_TypeChanged(EventContext data);
		void SwitchChangeProperty_TypePage(int index)=>m_view.m_change.m_type.selectedIndex=index;
		void SetChangeProperty_TipsText(string data)=>UIListener.SetText(m_view.m_change.m_tips,data);
		string GetChangeProperty_TipsText()=>UIListener.GetText(m_view.m_change.m_tips);
		void SetChangeProperty_LevelText(string data)=>UIListener.SetText(m_view.m_change.m_level,data);
		string GetChangeProperty_LevelText()=>UIListener.GetText(m_view.m_change.m_level);
		void _OnChangeClick(EventContext data){
			OnChangeClick(data);
		}
		partial void OnChangeClick(EventContext data);
		void SetChangeText(string data)=>UIListener.SetText(m_view.m_change,data);
		string GetChangeText()=>UIListener.GetText(m_view.m_change);

	}
}
