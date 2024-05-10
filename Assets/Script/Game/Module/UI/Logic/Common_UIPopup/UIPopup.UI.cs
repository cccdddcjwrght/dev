
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
			m_view.m_type.onChanged.Add(new EventCallback1(_OnTypeChanged));
			m_view.m_hideclose.onChanged.Add(new EventCallback1(_OnHidecloseChanged));
			m_view.m_close.m_Type.onChanged.Add(new EventCallback1(_OnCloseBtn_TypeChanged));
			UIListener.ListenerClose(m_view.m_close, new EventCallback1(DoCloseUIClick));

		}
		partial void UnInitUI(UIContext context){
			m_view.m_size.onChanged.Remove(new EventCallback1(_OnSizeChanged));
			m_view.m_type.onChanged.Remove(new EventCallback1(_OnTypeChanged));
			m_view.m_hideclose.onChanged.Remove(new EventCallback1(_OnHidecloseChanged));
			m_view.m_close.m_Type.onChanged.Remove(new EventCallback1(_OnCloseBtn_TypeChanged));
			UIListener.ListenerClose(m_view.m_close, new EventCallback1(DoCloseUIClick),remove:true);

		}
		void _OnSizeChanged(EventContext data){
			OnSizeChanged(data);
		}
		partial void OnSizeChanged(EventContext data);
		void SwitchSizePage(int index)=>m_view.m_size.selectedIndex=index;
		void _OnTypeChanged(EventContext data){
			OnTypeChanged(data);
		}
		partial void OnTypeChanged(EventContext data);
		void SwitchTypePage(int index)=>m_view.m_type.selectedIndex=index;
		void _OnHidecloseChanged(EventContext data){
			OnHidecloseChanged(data);
		}
		partial void OnHidecloseChanged(EventContext data);
		void SwitchHideclosePage(int index)=>m_view.m_hideclose.selectedIndex=index;
		void _OnCloseBtn_TypeChanged(EventContext data){
			OnCloseBtn_TypeChanged(data);
		}
		partial void OnCloseBtn_TypeChanged(EventContext data);
		void SwitchCloseBtn_TypePage(int index)=>m_view.m_close.m_Type.selectedIndex=index;
		void DoCloseUIClick(EventContext data){
			 bool __closestate = true;
			 OnUICloseClick(ref __closestate);
			 if(__closestate)SGame.UIUtils.CloseUIByID(__id);
			 
		}
		partial void OnUICloseClick(ref bool state);
		void SetCloseText(string data)=>UIListener.SetText(m_view.m_close,data);
		string GetCloseText()=>UIListener.GetText(m_view.m_close);

	}
}
