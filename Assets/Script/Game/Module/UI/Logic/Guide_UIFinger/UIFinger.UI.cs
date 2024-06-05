
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Guide;
	
	public partial class UIFinger
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			UIListener.ListenerIcon(m_view.m_Finger, new EventCallback1(_OnFingerClick));

		}
		partial void UnInitUI(UIContext context){
			UIListener.ListenerIcon(m_view.m_Finger, new EventCallback1(_OnFingerClick),remove:true);

		}
		void _OnFingerClick(EventContext data){
			OnFingerClick(data);
		}
		partial void OnFingerClick(EventContext data);

	}
}
