
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Common;
	
	public partial class UIPopup
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			m_view.m_size.onChanged.Add(new EventCallback1(_OnSizeChanged));
			UIListener.Listener(m_view.m_close, new EventCallback1(_OnCloseClick));

		}
		partial void UnInitUI(UIContext context){
			m_view.m_size.onChanged.Remove(new EventCallback1(_OnSizeChanged));
			UIListener.Listener(m_view.m_close, new EventCallback1(_OnCloseClick),remove:true);

		}
		void _OnSizeChanged(EventContext data){
			OnSizeChanged(data);
		}
		partial void OnSizeChanged(EventContext data);
		void SwitchSizePage(int index)=>m_view.m_size.selectedIndex=index;
		void SetTitleText(string data)=>UIListener.SetText(m_view.m_title,data);
		string GetTitleText()=>UIListener.GetText(m_view.m_title);
		void _OnCloseClick(EventContext data){
			OnCloseClick(data);
		}
		partial void OnCloseClick(EventContext data);
		void SetCloseText(string data)=>UIListener.SetText(m_view.m_close,data);
		string GetCloseText()=>UIListener.GetText(m_view.m_close);

	}
}
