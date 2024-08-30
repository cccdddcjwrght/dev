
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Explore;
	
	public partial class UIFightGift
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			m_view.m_body.m_type.onChanged.Add(new EventCallback1(_OnGiftBody_TypeChanged));
			UIListener.Listener(m_view.m_body.m_click, new EventCallback1(_OnGiftBody_ClickClick));
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick));

		}
		partial void UnInitUI(UIContext context){
			m_view.m_body.m_type.onChanged.Remove(new EventCallback1(_OnGiftBody_TypeChanged));
			UIListener.Listener(m_view.m_body.m_click, new EventCallback1(_OnGiftBody_ClickClick),remove:true);
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick),remove:true);

		}
		void _OnGiftBody_TypeChanged(EventContext data){
			OnGiftBody_TypeChanged(data);
		}
		partial void OnGiftBody_TypeChanged(EventContext data);
		void SwitchGiftBody_TypePage(int index)=>m_view.m_body.m_type.selectedIndex=index;
		void _OnGiftBody_ClickClick(EventContext data){
			OnGiftBody_ClickClick(data);
		}
		partial void OnGiftBody_ClickClick(EventContext data);
		void SetGiftBody_Body_clickText(string data)=>UIListener.SetText(m_view.m_body.m_click,data);
		string GetGiftBody_Body_clickText()=>UIListener.GetText(m_view.m_body.m_click);
		void DoCloseUIClick(EventContext data){
			 bool __closestate = true;
			 OnUICloseClick(ref __closestate);
			 if(__closestate)SGame.UIUtils.CloseUIByID(__id);
			 
		}
		partial void OnUICloseClick(ref bool state);
		void SetBodyText(string data)=>UIListener.SetText(m_view.m_body,data);
		string GetBodyText()=>UIListener.GetText(m_view.m_body);

	}
}
