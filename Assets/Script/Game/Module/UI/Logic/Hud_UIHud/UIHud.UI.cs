
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Hud;
	
	public partial class UIHud
	{
		partial void InitUI(UIContext context){
			UIListener.ListenerIcon(m_view.m_rrr, new EventCallback1(_OnRrrClick));

		}
		partial void UnInitUI(UIContext context){
			UIListener.ListenerIcon(m_view.m_rrr, new EventCallback1(_OnRrrClick),remove:true);

		}
		void SetFloatText_TitleText(string data)=>UIListener.SetText(m_view.m_rrr.m_title,data);
		string GetFloatText_TitleText()=>UIListener.GetText(m_view.m_rrr.m_title);
		void _OnRrrClick(EventContext data){
			OnRrrClick(data);
		}
		partial void OnRrrClick(EventContext data);

	}
}
