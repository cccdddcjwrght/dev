
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.GrowGift;
	
	public partial class UIGrowGift
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			m_view.m_buy.onChanged.Add(new EventCallback1(_OnBuyChanged));
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick));
			UIListener.Listener(m_view.m_btnCollect, new EventCallback1(_OnBtnCollectClick));
			UIListener.Listener(m_view.m_btnBuy, new EventCallback1(_OnBtnBuyClick));

		}
		partial void UnInitUI(UIContext context){
			m_view.m_buy.onChanged.Remove(new EventCallback1(_OnBuyChanged));
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick),remove:true);
			UIListener.Listener(m_view.m_btnCollect, new EventCallback1(_OnBtnCollectClick),remove:true);
			UIListener.Listener(m_view.m_btnBuy, new EventCallback1(_OnBtnBuyClick),remove:true);

		}
		void _OnBuyChanged(EventContext data){
			OnBuyChanged(data);
		}
		partial void OnBuyChanged(EventContext data);
		void SwitchBuyPage(int index)=>m_view.m_buy.selectedIndex=index;
		void DoCloseUIClick(EventContext data){
			 bool __closestate = true;
			 OnUICloseClick(ref __closestate);
			 if(__closestate)SGame.UIUtils.CloseUIByID(__id);
			 
		}
		partial void OnUICloseClick(ref bool state);
		void SetBodyText(string data)=>UIListener.SetText(m_view.m_body,data);
		string GetBodyText()=>UIListener.GetText(m_view.m_body);
		void _OnBtnCollectClick(EventContext data){
			OnBtnCollectClick(data);
		}
		partial void OnBtnCollectClick(EventContext data);
		void SetBtnCollectText(string data)=>UIListener.SetText(m_view.m_btnCollect,data);
		string GetBtnCollectText()=>UIListener.GetText(m_view.m_btnCollect);
		void _OnBtnBuyClick(EventContext data){
			OnBtnBuyClick(data);
		}
		partial void OnBtnBuyClick(EventContext data);
		void SetBtnBuyText(string data)=>UIListener.SetText(m_view.m_btnBuy,data);
		string GetBtnBuyText()=>UIListener.GetText(m_view.m_btnBuy);
		void SetLblTimeText(string data)=>UIListener.SetText(m_view.m_lblTime,data);
		string GetLblTimeText()=>UIListener.GetText(m_view.m_lblTime);

	}
}
