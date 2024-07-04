
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.HotFood;
	
	public partial class UIHotFood
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			m_view.m_hoting.onChanged.Add(new EventCallback1(_OnHotingChanged));
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick));
			UIListener.Listener(m_view.m_startBtn, new EventCallback1(_OnStartBtnClick));
			UIListener.Listener(m_view.m_stopBtn, new EventCallback1(_OnStopBtnClick));

		}
		partial void UnInitUI(UIContext context){
			m_view.m_hoting.onChanged.Remove(new EventCallback1(_OnHotingChanged));
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick),remove:true);
			UIListener.Listener(m_view.m_startBtn, new EventCallback1(_OnStartBtnClick),remove:true);
			UIListener.Listener(m_view.m_stopBtn, new EventCallback1(_OnStopBtnClick),remove:true);

		}
		void _OnHotingChanged(EventContext data){
			OnHotingChanged(data);
		}
		partial void OnHotingChanged(EventContext data);
		void SwitchHotingPage(int index)=>m_view.m_hoting.selectedIndex=index;
		void DoCloseUIClick(EventContext data){
			 bool __closestate = true;
			 OnUICloseClick(ref __closestate);
			 if(__closestate)SGame.UIUtils.CloseUIByID(__id);
			 
		}
		partial void OnUICloseClick(ref bool state);
		void SetBodyText(string data)=>UIListener.SetText(m_view.m_body,data);
		string GetBodyText()=>UIListener.GetText(m_view.m_body);
		void SetCdtimeText(string data)=>UIListener.SetText(m_view.m_cdtime,data);
		string GetCdtimeText()=>UIListener.GetText(m_view.m_cdtime);
		void SetDurationText(string data)=>UIListener.SetText(m_view.m_duration,data);
		string GetDurationText()=>UIListener.GetText(m_view.m_duration);
		void SetFoodTipText(string data)=>UIListener.SetText(m_view.m_foodTip,data);
		string GetFoodTipText()=>UIListener.GetText(m_view.m_foodTip);
		void _OnStartBtnClick(EventContext data){
			OnStartBtnClick(data);
		}
		partial void OnStartBtnClick(EventContext data);
		void SetStartBtnText(string data)=>UIListener.SetText(m_view.m_startBtn,data);
		string GetStartBtnText()=>UIListener.GetText(m_view.m_startBtn);
		void _OnStopBtnClick(EventContext data){
			OnStopBtnClick(data);
		}
		partial void OnStopBtnClick(EventContext data);
		void SetStopBtnText(string data)=>UIListener.SetText(m_view.m_stopBtn,data);
		string GetStopBtnText()=>UIListener.GetText(m_view.m_stopBtn);

	}
}
