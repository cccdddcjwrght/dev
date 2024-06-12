
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
			UIListener.ListenerIcon(m_view.m_model, new EventCallback1(_OnModelClick));

		}
		partial void UnInitUI(UIContext context){
			m_view.m_quality.onChanged.Remove(new EventCallback1(_OnQualityChanged));
			UIListener.ListenerIcon(m_view.m_model, new EventCallback1(_OnModelClick),remove:true);

		}
		void _OnQualityChanged(EventContext data){
			OnQualityChanged(data);
		}
		partial void OnQualityChanged(EventContext data);
		void SwitchQualityPage(int index)=>m_view.m_quality.selectedIndex=index;
		void _OnModelClick(EventContext data){
			OnModelClick(data);
		}
		partial void OnModelClick(EventContext data);

	}
}
