
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Buff;
	
	public partial class UILevelTech
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			m_view.m_completed.onChanged.Add(new EventCallback1(_OnCompletedChanged));
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick));
			UIListener.Listener(m_view.m_clickBtn, new EventCallback1(_OnClickBtnClick));

		}
		partial void UnInitUI(UIContext context){
			m_view.m_completed.onChanged.Remove(new EventCallback1(_OnCompletedChanged));
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick),remove:true);
			UIListener.Listener(m_view.m_clickBtn, new EventCallback1(_OnClickBtnClick),remove:true);

		}
		void _OnCompletedChanged(EventContext data){
			OnCompletedChanged(data);
		}
		partial void OnCompletedChanged(EventContext data);
		void SwitchCompletedPage(int index)=>m_view.m_completed.selectedIndex=index;
		void DoCloseUIClick(EventContext data){
			 bool __closestate = true;
			 OnUICloseClick(ref __closestate);
			 if(__closestate)SGame.UIUtils.CloseUIByID(__id);
			 
		}
		partial void OnUICloseClick(ref bool state);
		void SetBodyText(string data)=>UIListener.SetText(m_view.m_body,data);
		string GetBodyText()=>UIListener.GetText(m_view.m_body);
		void SetTitleText(string data)=>UIListener.SetText(m_view.m_title,data);
		string GetTitleText()=>UIListener.GetText(m_view.m_title);
		void _OnClickBtnClick(EventContext data){
			OnClickBtnClick(data);
		}
		partial void OnClickBtnClick(EventContext data);

	}
}
