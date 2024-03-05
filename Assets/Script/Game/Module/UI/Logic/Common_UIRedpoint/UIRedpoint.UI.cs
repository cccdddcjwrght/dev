
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Common;
	
	public partial class UIRedpoint
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			UIListener.Listener(m_view.m_icon, new EventCallback1(_OnIconClick));

		}
		partial void UnInitUI(UIContext context){
			UIListener.Listener(m_view.m_icon, new EventCallback1(_OnIconClick),remove:true);

		}
		void _OnIconClick(EventContext data){
			OnIconClick(data);
		}
		partial void OnIconClick(EventContext data);
		void SetIconText(string data)=>UIListener.SetText(m_view.m_icon,data);
		string GetIconText()=>UIListener.GetText(m_view.m_icon);

	}
}
