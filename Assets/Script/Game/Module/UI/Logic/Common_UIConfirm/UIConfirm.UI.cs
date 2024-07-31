
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Common;
	
	public partial class UIConfirm
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			m_view.m_type.onChanged.Add(new EventCallback1(_OnTypeChanged));
			m_view.m_clickstate.onChanged.Add(new EventCallback1(_OnClickstateChanged));
			m_view.m_textsize.onChanged.Add(new EventCallback1(_OnTextsizeChanged));
			m_view.m_body.m_type.onChanged.Add(new EventCallback1(_OnConfirmBody_TypeChanged));
			m_view.m_body.m_clickstate.onChanged.Add(new EventCallback1(_OnConfirmBody_ClickstateChanged));
			m_view.m_body.m_textsize.onChanged.Add(new EventCallback1(_OnConfirmBody_TextsizeChanged));
			m_view.m_body.m_body.m_size.onChanged.Add(new EventCallback1(_OnPopupBody_body_sizeChanged));
			m_view.m_body.m_body.m_type.onChanged.Add(new EventCallback1(_OnPopupBody_body_typeChanged));
			m_view.m_body.m_body.m_hideclose.onChanged.Add(new EventCallback1(_OnPopupBody_body_hidecloseChanged));
			m_view.m_body.m_body.m_close.m_Type.onChanged.Add(new EventCallback1(_OnCloseBtn_Body_body_close_TypeChanged));
			UIListener.ListenerClose(m_view.m_body.m_body.m_close, new EventCallback1(DoCloseUIClick));
			UIListener.ListenerClose(m_view.m_body.m_body, new EventCallback1(DoCloseUIClick));
			m_view.m_body.m_ok.m_bgSize.onChanged.Add(new EventCallback1(_OnClickBtn_Body_ok_bgSizeChanged));
			m_view.m_body.m_ok.m_txtSize.onChanged.Add(new EventCallback1(_OnClickBtn_Body_ok_txtSizeChanged));
			m_view.m_body.m_ok.m_bgColor.onChanged.Add(new EventCallback1(_OnClickBtn_Body_ok_bgColorChanged));
			m_view.m_body.m_ok.m_hasIcon.onChanged.Add(new EventCallback1(_OnClickBtn_Body_ok_hasIconChanged));
			m_view.m_body.m_ok.m_txtColor.onChanged.Add(new EventCallback1(_OnClickBtn_Body_ok_txtColorChanged));
			m_view.m_body.m_ok.m_iconImage.onChanged.Add(new EventCallback1(_OnClickBtn_Body_ok_iconImageChanged));
			m_view.m_body.m_ok.m_gray.onChanged.Add(new EventCallback1(_OnClickBtn_Body_ok_grayChanged));
			m_view.m_body.m_ok.m_limit.onChanged.Add(new EventCallback1(_OnClickBtn_Body_ok_limitChanged));
			m_view.m_body.m_ok.m_iconsize.onChanged.Add(new EventCallback1(_OnClickBtn_Body_ok_iconsizeChanged));
			UIListener.Listener(m_view.m_body.m_ok, new EventCallback1(_OnConfirmBody_OkClick));
			m_view.m_body.m_click1.m_bgSize.onChanged.Add(new EventCallback1(_OnClickBtn_Body_click1_bgSizeChanged));
			m_view.m_body.m_click1.m_txtSize.onChanged.Add(new EventCallback1(_OnClickBtn_Body_click1_txtSizeChanged));
			m_view.m_body.m_click1.m_bgColor.onChanged.Add(new EventCallback1(_OnClickBtn_Body_click1_bgColorChanged));
			m_view.m_body.m_click1.m_hasIcon.onChanged.Add(new EventCallback1(_OnClickBtn_Body_click1_hasIconChanged));
			m_view.m_body.m_click1.m_txtColor.onChanged.Add(new EventCallback1(_OnClickBtn_Body_click1_txtColorChanged));
			m_view.m_body.m_click1.m_iconImage.onChanged.Add(new EventCallback1(_OnClickBtn_Body_click1_iconImageChanged));
			m_view.m_body.m_click1.m_gray.onChanged.Add(new EventCallback1(_OnClickBtn_Body_click1_grayChanged));
			m_view.m_body.m_click1.m_limit.onChanged.Add(new EventCallback1(_OnClickBtn_Body_click1_limitChanged));
			m_view.m_body.m_click1.m_iconsize.onChanged.Add(new EventCallback1(_OnClickBtn_Body_click1_iconsizeChanged));
			UIListener.Listener(m_view.m_body.m_click1, new EventCallback1(_OnConfirmBody_Click1Click));
			m_view.m_body.m_click2.m_bgSize.onChanged.Add(new EventCallback1(_OnClickBtn_Body_click2_bgSizeChanged));
			m_view.m_body.m_click2.m_txtSize.onChanged.Add(new EventCallback1(_OnClickBtn_Body_click2_txtSizeChanged));
			m_view.m_body.m_click2.m_bgColor.onChanged.Add(new EventCallback1(_OnClickBtn_Body_click2_bgColorChanged));
			m_view.m_body.m_click2.m_hasIcon.onChanged.Add(new EventCallback1(_OnClickBtn_Body_click2_hasIconChanged));
			m_view.m_body.m_click2.m_txtColor.onChanged.Add(new EventCallback1(_OnClickBtn_Body_click2_txtColorChanged));
			m_view.m_body.m_click2.m_iconImage.onChanged.Add(new EventCallback1(_OnClickBtn_Body_click2_iconImageChanged));
			m_view.m_body.m_click2.m_gray.onChanged.Add(new EventCallback1(_OnClickBtn_Body_click2_grayChanged));
			m_view.m_body.m_click2.m_limit.onChanged.Add(new EventCallback1(_OnClickBtn_Body_click2_limitChanged));
			m_view.m_body.m_click2.m_iconsize.onChanged.Add(new EventCallback1(_OnClickBtn_Body_click2_iconsizeChanged));
			UIListener.Listener(m_view.m_body.m_click2, new EventCallback1(_OnConfirmBody_Click2Click));
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick));

		}
		partial void UnInitUI(UIContext context){
			m_view.m_type.onChanged.Remove(new EventCallback1(_OnTypeChanged));
			m_view.m_clickstate.onChanged.Remove(new EventCallback1(_OnClickstateChanged));
			m_view.m_textsize.onChanged.Remove(new EventCallback1(_OnTextsizeChanged));
			m_view.m_body.m_type.onChanged.Remove(new EventCallback1(_OnConfirmBody_TypeChanged));
			m_view.m_body.m_clickstate.onChanged.Remove(new EventCallback1(_OnConfirmBody_ClickstateChanged));
			m_view.m_body.m_textsize.onChanged.Remove(new EventCallback1(_OnConfirmBody_TextsizeChanged));
			m_view.m_body.m_body.m_size.onChanged.Remove(new EventCallback1(_OnPopupBody_body_sizeChanged));
			m_view.m_body.m_body.m_type.onChanged.Remove(new EventCallback1(_OnPopupBody_body_typeChanged));
			m_view.m_body.m_body.m_hideclose.onChanged.Remove(new EventCallback1(_OnPopupBody_body_hidecloseChanged));
			m_view.m_body.m_body.m_close.m_Type.onChanged.Remove(new EventCallback1(_OnCloseBtn_Body_body_close_TypeChanged));
			UIListener.ListenerClose(m_view.m_body.m_body.m_close, new EventCallback1(DoCloseUIClick),remove:true);
			UIListener.ListenerClose(m_view.m_body.m_body, new EventCallback1(DoCloseUIClick),remove:true);
			m_view.m_body.m_ok.m_bgSize.onChanged.Remove(new EventCallback1(_OnClickBtn_Body_ok_bgSizeChanged));
			m_view.m_body.m_ok.m_txtSize.onChanged.Remove(new EventCallback1(_OnClickBtn_Body_ok_txtSizeChanged));
			m_view.m_body.m_ok.m_bgColor.onChanged.Remove(new EventCallback1(_OnClickBtn_Body_ok_bgColorChanged));
			m_view.m_body.m_ok.m_hasIcon.onChanged.Remove(new EventCallback1(_OnClickBtn_Body_ok_hasIconChanged));
			m_view.m_body.m_ok.m_txtColor.onChanged.Remove(new EventCallback1(_OnClickBtn_Body_ok_txtColorChanged));
			m_view.m_body.m_ok.m_iconImage.onChanged.Remove(new EventCallback1(_OnClickBtn_Body_ok_iconImageChanged));
			m_view.m_body.m_ok.m_gray.onChanged.Remove(new EventCallback1(_OnClickBtn_Body_ok_grayChanged));
			m_view.m_body.m_ok.m_limit.onChanged.Remove(new EventCallback1(_OnClickBtn_Body_ok_limitChanged));
			m_view.m_body.m_ok.m_iconsize.onChanged.Remove(new EventCallback1(_OnClickBtn_Body_ok_iconsizeChanged));
			UIListener.Listener(m_view.m_body.m_ok, new EventCallback1(_OnConfirmBody_OkClick),remove:true);
			m_view.m_body.m_click1.m_bgSize.onChanged.Remove(new EventCallback1(_OnClickBtn_Body_click1_bgSizeChanged));
			m_view.m_body.m_click1.m_txtSize.onChanged.Remove(new EventCallback1(_OnClickBtn_Body_click1_txtSizeChanged));
			m_view.m_body.m_click1.m_bgColor.onChanged.Remove(new EventCallback1(_OnClickBtn_Body_click1_bgColorChanged));
			m_view.m_body.m_click1.m_hasIcon.onChanged.Remove(new EventCallback1(_OnClickBtn_Body_click1_hasIconChanged));
			m_view.m_body.m_click1.m_txtColor.onChanged.Remove(new EventCallback1(_OnClickBtn_Body_click1_txtColorChanged));
			m_view.m_body.m_click1.m_iconImage.onChanged.Remove(new EventCallback1(_OnClickBtn_Body_click1_iconImageChanged));
			m_view.m_body.m_click1.m_gray.onChanged.Remove(new EventCallback1(_OnClickBtn_Body_click1_grayChanged));
			m_view.m_body.m_click1.m_limit.onChanged.Remove(new EventCallback1(_OnClickBtn_Body_click1_limitChanged));
			m_view.m_body.m_click1.m_iconsize.onChanged.Remove(new EventCallback1(_OnClickBtn_Body_click1_iconsizeChanged));
			UIListener.Listener(m_view.m_body.m_click1, new EventCallback1(_OnConfirmBody_Click1Click),remove:true);
			m_view.m_body.m_click2.m_bgSize.onChanged.Remove(new EventCallback1(_OnClickBtn_Body_click2_bgSizeChanged));
			m_view.m_body.m_click2.m_txtSize.onChanged.Remove(new EventCallback1(_OnClickBtn_Body_click2_txtSizeChanged));
			m_view.m_body.m_click2.m_bgColor.onChanged.Remove(new EventCallback1(_OnClickBtn_Body_click2_bgColorChanged));
			m_view.m_body.m_click2.m_hasIcon.onChanged.Remove(new EventCallback1(_OnClickBtn_Body_click2_hasIconChanged));
			m_view.m_body.m_click2.m_txtColor.onChanged.Remove(new EventCallback1(_OnClickBtn_Body_click2_txtColorChanged));
			m_view.m_body.m_click2.m_iconImage.onChanged.Remove(new EventCallback1(_OnClickBtn_Body_click2_iconImageChanged));
			m_view.m_body.m_click2.m_gray.onChanged.Remove(new EventCallback1(_OnClickBtn_Body_click2_grayChanged));
			m_view.m_body.m_click2.m_limit.onChanged.Remove(new EventCallback1(_OnClickBtn_Body_click2_limitChanged));
			m_view.m_body.m_click2.m_iconsize.onChanged.Remove(new EventCallback1(_OnClickBtn_Body_click2_iconsizeChanged));
			UIListener.Listener(m_view.m_body.m_click2, new EventCallback1(_OnConfirmBody_Click2Click),remove:true);
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick),remove:true);

		}
		void _OnTypeChanged(EventContext data){
			OnTypeChanged(data);
		}
		partial void OnTypeChanged(EventContext data);
		void SwitchTypePage(int index)=>m_view.m_type.selectedIndex=index;
		void _OnClickstateChanged(EventContext data){
			OnClickstateChanged(data);
		}
		partial void OnClickstateChanged(EventContext data);
		void SwitchClickstatePage(int index)=>m_view.m_clickstate.selectedIndex=index;
		void _OnTextsizeChanged(EventContext data){
			OnTextsizeChanged(data);
		}
		partial void OnTextsizeChanged(EventContext data);
		void SwitchTextsizePage(int index)=>m_view.m_textsize.selectedIndex=index;
		void _OnConfirmBody_TypeChanged(EventContext data){
			OnConfirmBody_TypeChanged(data);
		}
		partial void OnConfirmBody_TypeChanged(EventContext data);
		void SwitchConfirmBody_TypePage(int index)=>m_view.m_body.m_type.selectedIndex=index;
		void _OnConfirmBody_ClickstateChanged(EventContext data){
			OnConfirmBody_ClickstateChanged(data);
		}
		partial void OnConfirmBody_ClickstateChanged(EventContext data);
		void SwitchConfirmBody_ClickstatePage(int index)=>m_view.m_body.m_clickstate.selectedIndex=index;
		void _OnConfirmBody_TextsizeChanged(EventContext data){
			OnConfirmBody_TextsizeChanged(data);
		}
		partial void OnConfirmBody_TextsizeChanged(EventContext data);
		void SwitchConfirmBody_TextsizePage(int index)=>m_view.m_body.m_textsize.selectedIndex=index;
		void _OnPopupBody_body_sizeChanged(EventContext data){
			OnPopupBody_body_sizeChanged(data);
		}
		partial void OnPopupBody_body_sizeChanged(EventContext data);
		void SwitchPopupBody_body_sizePage(int index)=>m_view.m_body.m_body.m_size.selectedIndex=index;
		void _OnPopupBody_body_typeChanged(EventContext data){
			OnPopupBody_body_typeChanged(data);
		}
		partial void OnPopupBody_body_typeChanged(EventContext data);
		void SwitchPopupBody_body_typePage(int index)=>m_view.m_body.m_body.m_type.selectedIndex=index;
		void _OnPopupBody_body_hidecloseChanged(EventContext data){
			OnPopupBody_body_hidecloseChanged(data);
		}
		partial void OnPopupBody_body_hidecloseChanged(EventContext data);
		void SwitchPopupBody_body_hideclosePage(int index)=>m_view.m_body.m_body.m_hideclose.selectedIndex=index;
		void SetPopupBody_body_textText(string data)=>UIListener.SetText(m_view.m_body.m_body.m_text,data);
		string GetPopupBody_body_textText()=>UIListener.GetText(m_view.m_body.m_body.m_text);
		void _OnCloseBtn_Body_body_close_TypeChanged(EventContext data){
			OnCloseBtn_Body_body_close_TypeChanged(data);
		}
		partial void OnCloseBtn_Body_body_close_TypeChanged(EventContext data);
		void SwitchCloseBtn_Body_body_close_TypePage(int index)=>m_view.m_body.m_body.m_close.m_Type.selectedIndex=index;
		void DoCloseUIClick(EventContext data){
			 bool __closestate = true;
			 OnUICloseClick(ref __closestate);
			 if(__closestate)SGame.UIUtils.CloseUIByID(__id);
			 
		}
		partial void OnUICloseClick(ref bool state);
		void SetPopupBody_body_closeText(string data)=>UIListener.SetText(m_view.m_body.m_body.m_close,data);
		string GetPopupBody_body_closeText()=>UIListener.GetText(m_view.m_body.m_body.m_close);
		void SetConfirmBody_Body_bodyText(string data)=>UIListener.SetText(m_view.m_body.m_body,data);
		string GetConfirmBody_Body_bodyText()=>UIListener.GetText(m_view.m_body.m_body);
		void _OnClickBtn_Body_ok_bgSizeChanged(EventContext data){
			OnClickBtn_Body_ok_bgSizeChanged(data);
		}
		partial void OnClickBtn_Body_ok_bgSizeChanged(EventContext data);
		void SwitchClickBtn_Body_ok_bgSizePage(int index)=>m_view.m_body.m_ok.m_bgSize.selectedIndex=index;
		void _OnClickBtn_Body_ok_txtSizeChanged(EventContext data){
			OnClickBtn_Body_ok_txtSizeChanged(data);
		}
		partial void OnClickBtn_Body_ok_txtSizeChanged(EventContext data);
		void SwitchClickBtn_Body_ok_txtSizePage(int index)=>m_view.m_body.m_ok.m_txtSize.selectedIndex=index;
		void _OnClickBtn_Body_ok_bgColorChanged(EventContext data){
			OnClickBtn_Body_ok_bgColorChanged(data);
		}
		partial void OnClickBtn_Body_ok_bgColorChanged(EventContext data);
		void SwitchClickBtn_Body_ok_bgColorPage(int index)=>m_view.m_body.m_ok.m_bgColor.selectedIndex=index;
		void _OnClickBtn_Body_ok_hasIconChanged(EventContext data){
			OnClickBtn_Body_ok_hasIconChanged(data);
		}
		partial void OnClickBtn_Body_ok_hasIconChanged(EventContext data);
		void SwitchClickBtn_Body_ok_hasIconPage(int index)=>m_view.m_body.m_ok.m_hasIcon.selectedIndex=index;
		void _OnClickBtn_Body_ok_txtColorChanged(EventContext data){
			OnClickBtn_Body_ok_txtColorChanged(data);
		}
		partial void OnClickBtn_Body_ok_txtColorChanged(EventContext data);
		void SwitchClickBtn_Body_ok_txtColorPage(int index)=>m_view.m_body.m_ok.m_txtColor.selectedIndex=index;
		void _OnClickBtn_Body_ok_iconImageChanged(EventContext data){
			OnClickBtn_Body_ok_iconImageChanged(data);
		}
		partial void OnClickBtn_Body_ok_iconImageChanged(EventContext data);
		void SwitchClickBtn_Body_ok_iconImagePage(int index)=>m_view.m_body.m_ok.m_iconImage.selectedIndex=index;
		void _OnClickBtn_Body_ok_grayChanged(EventContext data){
			OnClickBtn_Body_ok_grayChanged(data);
		}
		partial void OnClickBtn_Body_ok_grayChanged(EventContext data);
		void SwitchClickBtn_Body_ok_grayPage(int index)=>m_view.m_body.m_ok.m_gray.selectedIndex=index;
		void _OnClickBtn_Body_ok_limitChanged(EventContext data){
			OnClickBtn_Body_ok_limitChanged(data);
		}
		partial void OnClickBtn_Body_ok_limitChanged(EventContext data);
		void SwitchClickBtn_Body_ok_limitPage(int index)=>m_view.m_body.m_ok.m_limit.selectedIndex=index;
		void _OnClickBtn_Body_ok_iconsizeChanged(EventContext data){
			OnClickBtn_Body_ok_iconsizeChanged(data);
		}
		partial void OnClickBtn_Body_ok_iconsizeChanged(EventContext data);
		void SwitchClickBtn_Body_ok_iconsizePage(int index)=>m_view.m_body.m_ok.m_iconsize.selectedIndex=index;
		void SetClickBtn_Body_ok_iconTitleText(string data)=>UIListener.SetText(m_view.m_body.m_ok.m_iconTitle,data);
		string GetClickBtn_Body_ok_iconTitleText()=>UIListener.GetText(m_view.m_body.m_ok.m_iconTitle);
		void _OnConfirmBody_OkClick(EventContext data){
			OnConfirmBody_OkClick(data);
		}
		partial void OnConfirmBody_OkClick(EventContext data);
		void SetConfirmBody_Body_okText(string data)=>UIListener.SetText(m_view.m_body.m_ok,data);
		string GetConfirmBody_Body_okText()=>UIListener.GetText(m_view.m_body.m_ok);
		void _OnClickBtn_Body_click1_bgSizeChanged(EventContext data){
			OnClickBtn_Body_click1_bgSizeChanged(data);
		}
		partial void OnClickBtn_Body_click1_bgSizeChanged(EventContext data);
		void SwitchClickBtn_Body_click1_bgSizePage(int index)=>m_view.m_body.m_click1.m_bgSize.selectedIndex=index;
		void _OnClickBtn_Body_click1_txtSizeChanged(EventContext data){
			OnClickBtn_Body_click1_txtSizeChanged(data);
		}
		partial void OnClickBtn_Body_click1_txtSizeChanged(EventContext data);
		void SwitchClickBtn_Body_click1_txtSizePage(int index)=>m_view.m_body.m_click1.m_txtSize.selectedIndex=index;
		void _OnClickBtn_Body_click1_bgColorChanged(EventContext data){
			OnClickBtn_Body_click1_bgColorChanged(data);
		}
		partial void OnClickBtn_Body_click1_bgColorChanged(EventContext data);
		void SwitchClickBtn_Body_click1_bgColorPage(int index)=>m_view.m_body.m_click1.m_bgColor.selectedIndex=index;
		void _OnClickBtn_Body_click1_hasIconChanged(EventContext data){
			OnClickBtn_Body_click1_hasIconChanged(data);
		}
		partial void OnClickBtn_Body_click1_hasIconChanged(EventContext data);
		void SwitchClickBtn_Body_click1_hasIconPage(int index)=>m_view.m_body.m_click1.m_hasIcon.selectedIndex=index;
		void _OnClickBtn_Body_click1_txtColorChanged(EventContext data){
			OnClickBtn_Body_click1_txtColorChanged(data);
		}
		partial void OnClickBtn_Body_click1_txtColorChanged(EventContext data);
		void SwitchClickBtn_Body_click1_txtColorPage(int index)=>m_view.m_body.m_click1.m_txtColor.selectedIndex=index;
		void _OnClickBtn_Body_click1_iconImageChanged(EventContext data){
			OnClickBtn_Body_click1_iconImageChanged(data);
		}
		partial void OnClickBtn_Body_click1_iconImageChanged(EventContext data);
		void SwitchClickBtn_Body_click1_iconImagePage(int index)=>m_view.m_body.m_click1.m_iconImage.selectedIndex=index;
		void _OnClickBtn_Body_click1_grayChanged(EventContext data){
			OnClickBtn_Body_click1_grayChanged(data);
		}
		partial void OnClickBtn_Body_click1_grayChanged(EventContext data);
		void SwitchClickBtn_Body_click1_grayPage(int index)=>m_view.m_body.m_click1.m_gray.selectedIndex=index;
		void _OnClickBtn_Body_click1_limitChanged(EventContext data){
			OnClickBtn_Body_click1_limitChanged(data);
		}
		partial void OnClickBtn_Body_click1_limitChanged(EventContext data);
		void SwitchClickBtn_Body_click1_limitPage(int index)=>m_view.m_body.m_click1.m_limit.selectedIndex=index;
		void _OnClickBtn_Body_click1_iconsizeChanged(EventContext data){
			OnClickBtn_Body_click1_iconsizeChanged(data);
		}
		partial void OnClickBtn_Body_click1_iconsizeChanged(EventContext data);
		void SwitchClickBtn_Body_click1_iconsizePage(int index)=>m_view.m_body.m_click1.m_iconsize.selectedIndex=index;
		void SetClickBtn_Body_click1_iconTitleText(string data)=>UIListener.SetText(m_view.m_body.m_click1.m_iconTitle,data);
		string GetClickBtn_Body_click1_iconTitleText()=>UIListener.GetText(m_view.m_body.m_click1.m_iconTitle);
		void _OnConfirmBody_Click1Click(EventContext data){
			OnConfirmBody_Click1Click(data);
		}
		partial void OnConfirmBody_Click1Click(EventContext data);
		void SetConfirmBody_Body_click1Text(string data)=>UIListener.SetText(m_view.m_body.m_click1,data);
		string GetConfirmBody_Body_click1Text()=>UIListener.GetText(m_view.m_body.m_click1);
		void _OnClickBtn_Body_click2_bgSizeChanged(EventContext data){
			OnClickBtn_Body_click2_bgSizeChanged(data);
		}
		partial void OnClickBtn_Body_click2_bgSizeChanged(EventContext data);
		void SwitchClickBtn_Body_click2_bgSizePage(int index)=>m_view.m_body.m_click2.m_bgSize.selectedIndex=index;
		void _OnClickBtn_Body_click2_txtSizeChanged(EventContext data){
			OnClickBtn_Body_click2_txtSizeChanged(data);
		}
		partial void OnClickBtn_Body_click2_txtSizeChanged(EventContext data);
		void SwitchClickBtn_Body_click2_txtSizePage(int index)=>m_view.m_body.m_click2.m_txtSize.selectedIndex=index;
		void _OnClickBtn_Body_click2_bgColorChanged(EventContext data){
			OnClickBtn_Body_click2_bgColorChanged(data);
		}
		partial void OnClickBtn_Body_click2_bgColorChanged(EventContext data);
		void SwitchClickBtn_Body_click2_bgColorPage(int index)=>m_view.m_body.m_click2.m_bgColor.selectedIndex=index;
		void _OnClickBtn_Body_click2_hasIconChanged(EventContext data){
			OnClickBtn_Body_click2_hasIconChanged(data);
		}
		partial void OnClickBtn_Body_click2_hasIconChanged(EventContext data);
		void SwitchClickBtn_Body_click2_hasIconPage(int index)=>m_view.m_body.m_click2.m_hasIcon.selectedIndex=index;
		void _OnClickBtn_Body_click2_txtColorChanged(EventContext data){
			OnClickBtn_Body_click2_txtColorChanged(data);
		}
		partial void OnClickBtn_Body_click2_txtColorChanged(EventContext data);
		void SwitchClickBtn_Body_click2_txtColorPage(int index)=>m_view.m_body.m_click2.m_txtColor.selectedIndex=index;
		void _OnClickBtn_Body_click2_iconImageChanged(EventContext data){
			OnClickBtn_Body_click2_iconImageChanged(data);
		}
		partial void OnClickBtn_Body_click2_iconImageChanged(EventContext data);
		void SwitchClickBtn_Body_click2_iconImagePage(int index)=>m_view.m_body.m_click2.m_iconImage.selectedIndex=index;
		void _OnClickBtn_Body_click2_grayChanged(EventContext data){
			OnClickBtn_Body_click2_grayChanged(data);
		}
		partial void OnClickBtn_Body_click2_grayChanged(EventContext data);
		void SwitchClickBtn_Body_click2_grayPage(int index)=>m_view.m_body.m_click2.m_gray.selectedIndex=index;
		void _OnClickBtn_Body_click2_limitChanged(EventContext data){
			OnClickBtn_Body_click2_limitChanged(data);
		}
		partial void OnClickBtn_Body_click2_limitChanged(EventContext data);
		void SwitchClickBtn_Body_click2_limitPage(int index)=>m_view.m_body.m_click2.m_limit.selectedIndex=index;
		void _OnClickBtn_Body_click2_iconsizeChanged(EventContext data){
			OnClickBtn_Body_click2_iconsizeChanged(data);
		}
		partial void OnClickBtn_Body_click2_iconsizeChanged(EventContext data);
		void SwitchClickBtn_Body_click2_iconsizePage(int index)=>m_view.m_body.m_click2.m_iconsize.selectedIndex=index;
		void SetClickBtn_Body_click2_iconTitleText(string data)=>UIListener.SetText(m_view.m_body.m_click2.m_iconTitle,data);
		string GetClickBtn_Body_click2_iconTitleText()=>UIListener.GetText(m_view.m_body.m_click2.m_iconTitle);
		void _OnConfirmBody_Click2Click(EventContext data){
			OnConfirmBody_Click2Click(data);
		}
		partial void OnConfirmBody_Click2Click(EventContext data);
		void SetConfirmBody_Body_click2Text(string data)=>UIListener.SetText(m_view.m_body.m_click2,data);
		string GetConfirmBody_Body_click2Text()=>UIListener.GetText(m_view.m_body.m_click2);
		void SetConfirmBody_TipsText(string data)=>UIListener.SetText(m_view.m_body.m_tips,data);
		string GetConfirmBody_TipsText()=>UIListener.GetText(m_view.m_body.m_tips);
		void SetConfirmBody_TextText(string data)=>UIListener.SetText(m_view.m_body.m_text,data);
		string GetConfirmBody_TextText()=>UIListener.GetText(m_view.m_body.m_text);
		void SetBodyText(string data)=>UIListener.SetText(m_view.m_body,data);
		string GetBodyText()=>UIListener.GetText(m_view.m_body);

	}
}
