
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.TomorrowGift;
	
	public partial class UINewbieGift
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick));
			UIListener.ListenerIcon(m_view.m_gift, new EventCallback1(_OnGiftClick));
			UIListener.Listener(m_view.m_btnOK, new EventCallback1(_OnBtnOKClick));

		}
		partial void UnInitUI(UIContext context){
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick),remove:true);
			UIListener.ListenerIcon(m_view.m_gift, new EventCallback1(_OnGiftClick),remove:true);
			UIListener.Listener(m_view.m_btnOK, new EventCallback1(_OnBtnOKClick),remove:true);

		}
		void DoCloseUIClick(EventContext data){
			 bool __closestate = true;
			 OnUICloseClick(ref __closestate);
			 if(__closestate)SGame.UIUtils.CloseUIByID(__id);
			 
		}
		partial void OnUICloseClick(ref bool state);
		void SetBodyText(string data)=>UIListener.SetText(m_view.m_body,data);
		string GetBodyText()=>UIListener.GetText(m_view.m_body);
		void _OnGiftClick(EventContext data){
			OnGiftClick(data);
		}
		partial void OnGiftClick(EventContext data);
		void _OnBtnOKClick(EventContext data){
			OnBtnOKClick(data);
		}
		partial void OnBtnOKClick(EventContext data);
		void SetBtnOKText(string data)=>UIListener.SetText(m_view.m_btnOK,data);
		string GetBtnOKText()=>UIListener.GetText(m_view.m_btnOK);
		void SetLblPrercentText(string data)=>UIListener.SetText(m_view.m_lblPrercent,data);
		string GetLblPrercentText()=>UIListener.GetText(m_view.m_lblPrercent);

	}
}
