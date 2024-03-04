
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Setting;
	
	public partial class UIChangeHead
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			m_view.m_c1.onChanged.Add(new EventCallback1(_OnC1Changed));
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick));
			UIListener.Listener(m_view.m_head, new EventCallback1(_OnHeadClick));
			UIListener.Listener(m_view.m_frame, new EventCallback1(_OnFrameClick));
			UIListener.Listener(m_view.m_icon, new EventCallback1(_OnIconClick));

		}
		partial void UnInitUI(UIContext context){
			m_view.m_c1.onChanged.Remove(new EventCallback1(_OnC1Changed));
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick),remove:true);
			UIListener.Listener(m_view.m_head, new EventCallback1(_OnHeadClick),remove:true);
			UIListener.Listener(m_view.m_frame, new EventCallback1(_OnFrameClick),remove:true);
			UIListener.Listener(m_view.m_icon, new EventCallback1(_OnIconClick),remove:true);

		}
		void _OnC1Changed(EventContext data){
			OnC1Changed(data);
		}
		partial void OnC1Changed(EventContext data);
		void SwitchC1Page(int index)=>m_view.m_c1.selectedIndex=index;
		void DoCloseUIClick(EventContext data){
			 bool __closestate = true;
			 OnUICloseClick(ref __closestate);
			 if(__closestate)SGame.UIUtils.CloseUIByID(__id);
			 
		}
		partial void OnUICloseClick(ref bool state);
		void SetBodyText(string data)=>UIListener.SetText(m_view.m_body,data);
		string GetBodyText()=>UIListener.GetText(m_view.m_body);
		void _OnHeadClick(EventContext data){
			OnHeadClick(data);
		}
		partial void OnHeadClick(EventContext data);
		void SetHeadText(string data)=>UIListener.SetText(m_view.m_head,data);
		string GetHeadText()=>UIListener.GetText(m_view.m_head);
		void _OnFrameClick(EventContext data){
			OnFrameClick(data);
		}
		partial void OnFrameClick(EventContext data);
		void SetFrameText(string data)=>UIListener.SetText(m_view.m_frame,data);
		string GetFrameText()=>UIListener.GetText(m_view.m_frame);
		void _OnIconClick(EventContext data){
			OnIconClick(data);
		}
		partial void OnIconClick(EventContext data);
		void SetIconText(string data)=>UIListener.SetText(m_view.m_icon,data);
		string GetIconText()=>UIListener.GetText(m_view.m_icon);

	}
}
