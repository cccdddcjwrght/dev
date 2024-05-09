
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.GameFriend;
	
	public partial class UIFriendDetail
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			m_view.m_recomment.onChanged.Add(new EventCallback1(_OnRecommentChanged));
			m_view.m_comfirm.onChanged.Add(new EventCallback1(_OnComfirmChanged));
			UIListener.ListenerClose(m_view.m_comfirmDialog.m_body, new EventCallback1(DoCloseUIClick));
			UIListener.Listener(m_view.m_comfirmDialog.m_btnCancle, new EventCallback1(_OnComfirm_BtnCancleClick));
			UIListener.Listener(m_view.m_comfirmDialog.m_btnOK, new EventCallback1(_OnComfirm_BtnOKClick));
			UIListener.ListenerIcon(m_view.m_comfirmDialog, new EventCallback1(_OnComfirmDialogClick));
			UIListener.Listener(m_view.m_btnClose, new EventCallback1(_OnBtnCloseClick));
			UIListener.Listener(m_view.m_btnDelete, new EventCallback1(_OnBtnDeleteClick));
			UIListener.Listener(m_view.m_btnOK, new EventCallback1(_OnBtnOKClick));

		}
		partial void UnInitUI(UIContext context){
			m_view.m_recomment.onChanged.Remove(new EventCallback1(_OnRecommentChanged));
			m_view.m_comfirm.onChanged.Remove(new EventCallback1(_OnComfirmChanged));
			UIListener.ListenerClose(m_view.m_comfirmDialog.m_body, new EventCallback1(DoCloseUIClick),remove:true);
			UIListener.Listener(m_view.m_comfirmDialog.m_btnCancle, new EventCallback1(_OnComfirm_BtnCancleClick),remove:true);
			UIListener.Listener(m_view.m_comfirmDialog.m_btnOK, new EventCallback1(_OnComfirm_BtnOKClick),remove:true);
			UIListener.ListenerIcon(m_view.m_comfirmDialog, new EventCallback1(_OnComfirmDialogClick),remove:true);
			UIListener.Listener(m_view.m_btnClose, new EventCallback1(_OnBtnCloseClick),remove:true);
			UIListener.Listener(m_view.m_btnDelete, new EventCallback1(_OnBtnDeleteClick),remove:true);
			UIListener.Listener(m_view.m_btnOK, new EventCallback1(_OnBtnOKClick),remove:true);

		}
		void _OnRecommentChanged(EventContext data){
			OnRecommentChanged(data);
		}
		partial void OnRecommentChanged(EventContext data);
		void SwitchRecommentPage(int index)=>m_view.m_recomment.selectedIndex=index;
		void _OnComfirmChanged(EventContext data){
			OnComfirmChanged(data);
		}
		partial void OnComfirmChanged(EventContext data);
		void SwitchComfirmPage(int index)=>m_view.m_comfirm.selectedIndex=index;
		void DoCloseUIClick(EventContext data){
			 bool __closestate = true;
			 OnUICloseClick(ref __closestate);
			 if(__closestate)SGame.UIUtils.CloseUIByID(__id);
			 
		}
		partial void OnUICloseClick(ref bool state);
		void SetComfirm_TitleNameText(string data)=>UIListener.SetText(m_view.m_comfirmDialog.m_titleName,data);
		string GetComfirm_TitleNameText()=>UIListener.GetText(m_view.m_comfirmDialog.m_titleName);
		void _OnComfirm_BtnCancleClick(EventContext data){
			OnComfirm_BtnCancleClick(data);
		}
		partial void OnComfirm_BtnCancleClick(EventContext data);
		void SetComfirm_ComfirmDialog_btnCancleText(string data)=>UIListener.SetText(m_view.m_comfirmDialog.m_btnCancle,data);
		string GetComfirm_ComfirmDialog_btnCancleText()=>UIListener.GetText(m_view.m_comfirmDialog.m_btnCancle);
		void _OnComfirm_BtnOKClick(EventContext data){
			OnComfirm_BtnOKClick(data);
		}
		partial void OnComfirm_BtnOKClick(EventContext data);
		void SetComfirm_ComfirmDialog_btnOKText(string data)=>UIListener.SetText(m_view.m_comfirmDialog.m_btnOK,data);
		string GetComfirm_ComfirmDialog_btnOKText()=>UIListener.GetText(m_view.m_comfirmDialog.m_btnOK);
		void _OnComfirmDialogClick(EventContext data){
			OnComfirmDialogClick(data);
		}
		partial void OnComfirmDialogClick(EventContext data);
		void _OnBtnCloseClick(EventContext data){
			OnBtnCloseClick(data);
		}
		partial void OnBtnCloseClick(EventContext data);
		void SetBtnCloseText(string data)=>UIListener.SetText(m_view.m_btnClose,data);
		string GetBtnCloseText()=>UIListener.GetText(m_view.m_btnClose);
		void SetTitleText(string data)=>UIListener.SetText(m_view.m_title,data);
		string GetTitleText()=>UIListener.GetText(m_view.m_title);
		void _OnBtnDeleteClick(EventContext data){
			OnBtnDeleteClick(data);
		}
		partial void OnBtnDeleteClick(EventContext data);
		void SetBtnDeleteText(string data)=>UIListener.SetText(m_view.m_btnDelete,data);
		string GetBtnDeleteText()=>UIListener.GetText(m_view.m_btnDelete);
		void _OnBtnOKClick(EventContext data){
			OnBtnOKClick(data);
		}
		partial void OnBtnOKClick(EventContext data);
		void SetBtnOKText(string data)=>UIListener.SetText(m_view.m_btnOK,data);
		string GetBtnOKText()=>UIListener.GetText(m_view.m_btnOK);

	}
}
