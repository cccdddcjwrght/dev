
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Worktable;
	
	public partial class UILockPanel
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			m_view.m_btnty.onChanged.Add(new EventCallback1(_OnBtntyChanged));
			m_view.m_type.onChanged.Add(new EventCallback1(_OnTypeChanged));
			UIListener.Listener(m_view.m_click, new EventCallback1(_OnClickClick));

		}
		partial void UnInitUI(UIContext context){
			m_view.m_btnty.onChanged.Remove(new EventCallback1(_OnBtntyChanged));
			m_view.m_type.onChanged.Remove(new EventCallback1(_OnTypeChanged));
			UIListener.Listener(m_view.m_click, new EventCallback1(_OnClickClick),remove:true);

		}
		void _OnBtntyChanged(EventContext data){
			OnBtntyChanged(data);
		}
		partial void OnBtntyChanged(EventContext data);
		void SwitchBtntyPage(int index)=>m_view.m_btnty.selectedIndex=index;
		void _OnTypeChanged(EventContext data){
			OnTypeChanged(data);
		}
		partial void OnTypeChanged(EventContext data);
		void SwitchTypePage(int index)=>m_view.m_type.selectedIndex=index;
		void SetUnlockText(string data)=>UIListener.SetText(m_view.m_unlock,data);
		string GetUnlockText()=>UIListener.GetText(m_view.m_unlock);
		void _OnClickClick(EventContext data){
			OnClickClick(data);
		}
		partial void OnClickClick(EventContext data);
		void SetClickText(string data)=>UIListener.SetText(m_view.m_click,data);
		string GetClickText()=>UIListener.GetText(m_view.m_click);
		void SetTipsText(string data)=>UIListener.SetText(m_view.m_tips,data);
		string GetTipsText()=>UIListener.GetText(m_view.m_tips);

	}
}
