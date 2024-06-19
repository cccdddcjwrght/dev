
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Reputation;
	
	public partial class UIFragment
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			UIListener.Listener(m_view.m_btn, new EventCallback1(_OnBtnClick));

		}
		partial void UnInitUI(UIContext context){
			UIListener.Listener(m_view.m_btn, new EventCallback1(_OnBtnClick),remove:true);

		}
		void _OnBtnClick(EventContext data){
			OnBtnClick(data);
		}
		partial void OnBtnClick(EventContext data);
		void SetBtnText(string data)=>UIListener.SetText(m_view.m_btn,data);
		string GetBtnText()=>UIListener.GetText(m_view.m_btn);

	}
}
