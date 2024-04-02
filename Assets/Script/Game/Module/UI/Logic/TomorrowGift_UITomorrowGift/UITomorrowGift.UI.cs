
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.TomorrowGift;
	
	public partial class UITomorrowGift
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick));
			UIListener.Listener(m_view.m_btnOK, new EventCallback1(_OnBtnOKClick));
			UIListener.ListenerIcon(m_view.m_item1, new EventCallback1(_OnItem1Click));
			UIListener.ListenerIcon(m_view.m_item2, new EventCallback1(_OnItem2Click));

		}
		partial void UnInitUI(UIContext context){
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick),remove:true);
			UIListener.Listener(m_view.m_btnOK, new EventCallback1(_OnBtnOKClick),remove:true);
			UIListener.ListenerIcon(m_view.m_item1, new EventCallback1(_OnItem1Click),remove:true);
			UIListener.ListenerIcon(m_view.m_item2, new EventCallback1(_OnItem2Click),remove:true);

		}
		void DoCloseUIClick(EventContext data){
			 bool __closestate = true;
			 OnUICloseClick(ref __closestate);
			 if(__closestate)SGame.UIUtils.CloseUIByID(__id);
			 
		}
		partial void OnUICloseClick(ref bool state);
		void SetBodyText(string data)=>UIListener.SetText(m_view.m_body,data);
		string GetBodyText()=>UIListener.GetText(m_view.m_body);
		void _OnBtnOKClick(EventContext data){
			OnBtnOKClick(data);
		}
		partial void OnBtnOKClick(EventContext data);
		void SetBtnOKText(string data)=>UIListener.SetText(m_view.m_btnOK,data);
		string GetBtnOKText()=>UIListener.GetText(m_view.m_btnOK);
		void _OnItem1Click(EventContext data){
			OnItem1Click(data);
		}
		partial void OnItem1Click(EventContext data);
		void _OnItem2Click(EventContext data){
			OnItem2Click(data);
		}
		partial void OnItem2Click(EventContext data);
		void SetTimelabelText(string data)=>UIListener.SetText(m_view.m_timelabel,data);
		string GetTimelabelText()=>UIListener.GetText(m_view.m_timelabel);
		void SetTimeText(string data)=>UIListener.SetText(m_view.m_time,data);
		string GetTimeText()=>UIListener.GetText(m_view.m_time);

	}
}
