
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Hud;
	
	public partial class UIHud
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			UIListener.ListenerIcon(m_view.m_nn, new EventCallback1(_OnNnClick));

		}
		partial void UnInitUI(UIContext context){
			UIListener.ListenerIcon(m_view.m_nn, new EventCallback1(_OnNnClick),remove:true);

		}
		void SetFloatText_TitleText(string data)=>UIListener.SetText(m_view.m_nn.m_title,data);
		string GetFloatText_TitleText()=>UIListener.GetText(m_view.m_nn.m_title);
		void _OnNnClick(EventContext data){
			OnNnClick(data);
		}
		partial void OnNnClick(EventContext data);

	}
}
