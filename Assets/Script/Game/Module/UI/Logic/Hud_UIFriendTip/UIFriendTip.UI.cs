
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Hud;
	
	public partial class UIFriendTip
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			m_view.m_style.onChanged.Add(new EventCallback1(_OnStyleChanged));

		}
		partial void UnInitUI(UIContext context){
			m_view.m_style.onChanged.Remove(new EventCallback1(_OnStyleChanged));

		}
		void _OnStyleChanged(EventContext data){
			OnStyleChanged(data);
		}
		partial void OnStyleChanged(EventContext data);
		void SwitchStylePage(int index)=>m_view.m_style.selectedIndex=index;

	}
}
