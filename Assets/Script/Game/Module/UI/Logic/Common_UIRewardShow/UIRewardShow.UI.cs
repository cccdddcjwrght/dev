
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Common;
	
	public partial class UIRewardShow
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			m_view.m_type.onChanged.Add(new EventCallback1(_OnTypeChanged));
			m_view.m_click.m_bgSize.onChanged.Add(new EventCallback1(_OnClickBtn_BgSizeChanged));
			m_view.m_click.m_txtSize.onChanged.Add(new EventCallback1(_OnClickBtn_TxtSizeChanged));
			m_view.m_click.m_bgColor.onChanged.Add(new EventCallback1(_OnClickBtn_BgColorChanged));
			m_view.m_click.m_hasIcon.onChanged.Add(new EventCallback1(_OnClickBtn_HasIconChanged));
			m_view.m_click.m_txtColor.onChanged.Add(new EventCallback1(_OnClickBtn_TxtColorChanged));
			m_view.m_click.m_iconImage.onChanged.Add(new EventCallback1(_OnClickBtn_IconImageChanged));
			m_view.m_click.m_gray.onChanged.Add(new EventCallback1(_OnClickBtn_GrayChanged));
			m_view.m_click.m_limit.onChanged.Add(new EventCallback1(_OnClickBtn_LimitChanged));
			m_view.m_click.m_iconsize.onChanged.Add(new EventCallback1(_OnClickBtn_IconsizeChanged));
			UIListener.Listener(m_view.m_click, new EventCallback1(_OnClickClick));

		}
		partial void UnInitUI(UIContext context){
			m_view.m_type.onChanged.Remove(new EventCallback1(_OnTypeChanged));
			m_view.m_click.m_bgSize.onChanged.Remove(new EventCallback1(_OnClickBtn_BgSizeChanged));
			m_view.m_click.m_txtSize.onChanged.Remove(new EventCallback1(_OnClickBtn_TxtSizeChanged));
			m_view.m_click.m_bgColor.onChanged.Remove(new EventCallback1(_OnClickBtn_BgColorChanged));
			m_view.m_click.m_hasIcon.onChanged.Remove(new EventCallback1(_OnClickBtn_HasIconChanged));
			m_view.m_click.m_txtColor.onChanged.Remove(new EventCallback1(_OnClickBtn_TxtColorChanged));
			m_view.m_click.m_iconImage.onChanged.Remove(new EventCallback1(_OnClickBtn_IconImageChanged));
			m_view.m_click.m_gray.onChanged.Remove(new EventCallback1(_OnClickBtn_GrayChanged));
			m_view.m_click.m_limit.onChanged.Remove(new EventCallback1(_OnClickBtn_LimitChanged));
			m_view.m_click.m_iconsize.onChanged.Remove(new EventCallback1(_OnClickBtn_IconsizeChanged));
			UIListener.Listener(m_view.m_click, new EventCallback1(_OnClickClick),remove:true);

		}
		void _OnTypeChanged(EventContext data){
			OnTypeChanged(data);
		}
		partial void OnTypeChanged(EventContext data);
		void SwitchTypePage(int index)=>m_view.m_type.selectedIndex=index;
		void SetTitleText(string data)=>UIListener.SetText(m_view.m_title,data);
		string GetTitleText()=>UIListener.GetText(m_view.m_title);
		void SetTipText(string data)=>UIListener.SetText(m_view.m_tip,data);
		string GetTipText()=>UIListener.GetText(m_view.m_tip);
		void _OnClickBtn_BgSizeChanged(EventContext data){
			OnClickBtn_BgSizeChanged(data);
		}
		partial void OnClickBtn_BgSizeChanged(EventContext data);
		void SwitchClickBtn_BgSizePage(int index)=>m_view.m_click.m_bgSize.selectedIndex=index;
		void _OnClickBtn_TxtSizeChanged(EventContext data){
			OnClickBtn_TxtSizeChanged(data);
		}
		partial void OnClickBtn_TxtSizeChanged(EventContext data);
		void SwitchClickBtn_TxtSizePage(int index)=>m_view.m_click.m_txtSize.selectedIndex=index;
		void _OnClickBtn_BgColorChanged(EventContext data){
			OnClickBtn_BgColorChanged(data);
		}
		partial void OnClickBtn_BgColorChanged(EventContext data);
		void SwitchClickBtn_BgColorPage(int index)=>m_view.m_click.m_bgColor.selectedIndex=index;
		void _OnClickBtn_HasIconChanged(EventContext data){
			OnClickBtn_HasIconChanged(data);
		}
		partial void OnClickBtn_HasIconChanged(EventContext data);
		void SwitchClickBtn_HasIconPage(int index)=>m_view.m_click.m_hasIcon.selectedIndex=index;
		void _OnClickBtn_TxtColorChanged(EventContext data){
			OnClickBtn_TxtColorChanged(data);
		}
		partial void OnClickBtn_TxtColorChanged(EventContext data);
		void SwitchClickBtn_TxtColorPage(int index)=>m_view.m_click.m_txtColor.selectedIndex=index;
		void _OnClickBtn_IconImageChanged(EventContext data){
			OnClickBtn_IconImageChanged(data);
		}
		partial void OnClickBtn_IconImageChanged(EventContext data);
		void SwitchClickBtn_IconImagePage(int index)=>m_view.m_click.m_iconImage.selectedIndex=index;
		void _OnClickBtn_GrayChanged(EventContext data){
			OnClickBtn_GrayChanged(data);
		}
		partial void OnClickBtn_GrayChanged(EventContext data);
		void SwitchClickBtn_GrayPage(int index)=>m_view.m_click.m_gray.selectedIndex=index;
		void _OnClickBtn_LimitChanged(EventContext data){
			OnClickBtn_LimitChanged(data);
		}
		partial void OnClickBtn_LimitChanged(EventContext data);
		void SwitchClickBtn_LimitPage(int index)=>m_view.m_click.m_limit.selectedIndex=index;
		void _OnClickBtn_IconsizeChanged(EventContext data){
			OnClickBtn_IconsizeChanged(data);
		}
		partial void OnClickBtn_IconsizeChanged(EventContext data);
		void SwitchClickBtn_IconsizePage(int index)=>m_view.m_click.m_iconsize.selectedIndex=index;
		void SetClickBtn_IconTitleText(string data)=>UIListener.SetText(m_view.m_click.m_iconTitle,data);
		string GetClickBtn_IconTitleText()=>UIListener.GetText(m_view.m_click.m_iconTitle);
		void _OnClickClick(EventContext data){
			OnClickClick(data);
		}
		partial void OnClickClick(EventContext data);
		void SetClickText(string data)=>UIListener.SetText(m_view.m_click,data);
		string GetClickText()=>UIListener.GetText(m_view.m_click);

	}
}
