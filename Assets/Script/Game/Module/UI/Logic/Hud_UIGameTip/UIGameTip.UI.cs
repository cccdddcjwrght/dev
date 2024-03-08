
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Hud;
	
	public partial class UIGameTip
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			m_view.m_TipType.onChanged.Add(new EventCallback1(_OnTipTypeChanged));

		}
		partial void UnInitUI(UIContext context){
			m_view.m_TipType.onChanged.Remove(new EventCallback1(_OnTipTypeChanged));

		}
		void _OnTipTypeChanged(EventContext data){
			OnTipTypeChanged(data);
		}
		partial void OnTipTypeChanged(EventContext data);
		void SwitchTipTypePage(int index)=>m_view.m_TipType.selectedIndex=index;
		void SetTitleText(string data)=>UIListener.SetText(m_view.m_title,data);
		string GetTitleText()=>UIListener.GetText(m_view.m_title);

	}
}
