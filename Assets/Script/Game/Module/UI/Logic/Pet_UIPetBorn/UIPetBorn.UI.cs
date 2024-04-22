
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
			UIListener.ListenerIcon(m_view.m_model, new EventCallback1(_OnModelClick));

		}
		partial void UnInitUI(UIContext context){
			UIListener.ListenerIcon(m_view.m_model, new EventCallback1(_OnModelClick),remove:true);

		}
		void _OnModelClick(EventContext data){
			OnModelClick(data);
		}
		partial void OnModelClick(EventContext data);

	}
}
