
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Reputation;
	
	public partial class UILuckLike
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			m_view.m_reward.onChanged.Add(new EventCallback1(_OnRewardChanged));
			m_view.m_auto.onChanged.Add(new EventCallback1(_OnAutoChanged));
			UIListener.ListenerClose(m_view.m_close, new EventCallback1(DoCloseUIClick));
			UIListener.Listener(m_view.m_startBtn, new EventCallback1(_OnStartBtnClick));
			UIListener.Listener(m_view.m_stopBtn, new EventCallback1(_OnStopBtnClick));
			UIListener.ListenerClose(m_view.m_BigLuckShow.m_mask, new EventCallback1(DoCloseUIClick));
			UIListener.ListenerIcon(m_view.m_BigLuckShow, new EventCallback1(_OnBigLuckShowClick));

		}
		partial void UnInitUI(UIContext context){
			m_view.m_reward.onChanged.Remove(new EventCallback1(_OnRewardChanged));
			m_view.m_auto.onChanged.Remove(new EventCallback1(_OnAutoChanged));
			UIListener.ListenerClose(m_view.m_close, new EventCallback1(DoCloseUIClick),remove:true);
			UIListener.Listener(m_view.m_startBtn, new EventCallback1(_OnStartBtnClick),remove:true);
			UIListener.Listener(m_view.m_stopBtn, new EventCallback1(_OnStopBtnClick),remove:true);
			UIListener.ListenerClose(m_view.m_BigLuckShow.m_mask, new EventCallback1(DoCloseUIClick),remove:true);
			UIListener.ListenerIcon(m_view.m_BigLuckShow, new EventCallback1(_OnBigLuckShowClick),remove:true);

		}
		void _OnRewardChanged(EventContext data){
			OnRewardChanged(data);
		}
		partial void OnRewardChanged(EventContext data);
		void SwitchRewardPage(int index)=>m_view.m_reward.selectedIndex=index;
		void _OnAutoChanged(EventContext data){
			OnAutoChanged(data);
		}
		partial void OnAutoChanged(EventContext data);
		void SwitchAutoPage(int index)=>m_view.m_auto.selectedIndex=index;
		void DoCloseUIClick(EventContext data){
			 bool __closestate = true;
			 OnUICloseClick(ref __closestate);
			 if(__closestate)SGame.UIUtils.CloseUIByID(__id);
			 
		}
		partial void OnUICloseClick(ref bool state);
		void SetCloseText(string data)=>UIListener.SetText(m_view.m_close,data);
		string GetCloseText()=>UIListener.GetText(m_view.m_close);
		void SetNameText(string data)=>UIListener.SetText(m_view.m_name,data);
		string GetNameText()=>UIListener.GetText(m_view.m_name);
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
		void SetCountText(string data)=>UIListener.SetText(m_view.m_count,data);
		string GetCountText()=>UIListener.GetText(m_view.m_count);
		void _OnBigLuckShowClick(EventContext data){
			OnBigLuckShowClick(data);
		}
		partial void OnBigLuckShowClick(EventContext data);
		void SetBigLuckShowText(string data)=>UIListener.SetText(m_view.m_BigLuckShow,data);
		string GetBigLuckShowText()=>UIListener.GetText(m_view.m_BigLuckShow);

	}
}
