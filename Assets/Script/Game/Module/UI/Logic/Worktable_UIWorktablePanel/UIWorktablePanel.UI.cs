
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Worktable;
	
	public partial class UIWorktablePanel
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			m_view.m_type.onChanged.Add(new EventCallback1(_OnTypeChanged));
			m_view.m_pos.onChanged.Add(new EventCallback1(_OnPosChanged));
			m_view.m_isAd.onChanged.Add(new EventCallback1(_OnIsAdChanged));
			m_view.m_btnty.onChanged.Add(new EventCallback1(_OnBtntyChanged));
			m_view.m_maxlv.onChanged.Add(new EventCallback1(_OnMaxlvChanged));
			m_view.m_roleType.onChanged.Add(new EventCallback1(_OnRoleTypeChanged));
			UIListener.Listener(m_view.m_clickBtn, new EventCallback1(_OnClickBtnClick));
			UIListener.Listener(m_view.m_click, new EventCallback1(_OnClickClick));
			UIListener.Listener(m_view.m_adBtn, new EventCallback1(_OnAdBtnClick));

		}
		partial void UnInitUI(UIContext context){
			m_view.m_type.onChanged.Remove(new EventCallback1(_OnTypeChanged));
			m_view.m_pos.onChanged.Remove(new EventCallback1(_OnPosChanged));
			m_view.m_isAd.onChanged.Remove(new EventCallback1(_OnIsAdChanged));
			m_view.m_btnty.onChanged.Remove(new EventCallback1(_OnBtntyChanged));
			m_view.m_maxlv.onChanged.Remove(new EventCallback1(_OnMaxlvChanged));
			m_view.m_roleType.onChanged.Remove(new EventCallback1(_OnRoleTypeChanged));
			UIListener.Listener(m_view.m_clickBtn, new EventCallback1(_OnClickBtnClick),remove:true);
			UIListener.Listener(m_view.m_click, new EventCallback1(_OnClickClick),remove:true);
			UIListener.Listener(m_view.m_adBtn, new EventCallback1(_OnAdBtnClick),remove:true);

		}
		void _OnTypeChanged(EventContext data){
			OnTypeChanged(data);
		}
		partial void OnTypeChanged(EventContext data);
		void SwitchTypePage(int index)=>m_view.m_type.selectedIndex=index;
		void _OnPosChanged(EventContext data){
			OnPosChanged(data);
		}
		partial void OnPosChanged(EventContext data);
		void SwitchPosPage(int index)=>m_view.m_pos.selectedIndex=index;
		void _OnIsAdChanged(EventContext data){
			OnIsAdChanged(data);
		}
		partial void OnIsAdChanged(EventContext data);
		void SwitchIsAdPage(int index)=>m_view.m_isAd.selectedIndex=index;
		void _OnBtntyChanged(EventContext data){
			OnBtntyChanged(data);
		}
		partial void OnBtntyChanged(EventContext data);
		void SwitchBtntyPage(int index)=>m_view.m_btnty.selectedIndex=index;
		void _OnMaxlvChanged(EventContext data){
			OnMaxlvChanged(data);
		}
		partial void OnMaxlvChanged(EventContext data);
		void SwitchMaxlvPage(int index)=>m_view.m_maxlv.selectedIndex=index;
		void _OnRoleTypeChanged(EventContext data){
			OnRoleTypeChanged(data);
		}
		partial void OnRoleTypeChanged(EventContext data);
		void SwitchRoleTypePage(int index)=>m_view.m_roleType.selectedIndex=index;
		void SetLevelText(string data)=>UIListener.SetText(m_view.m_level,data);
		string GetLevelText()=>UIListener.GetText(m_view.m_level);
		void SetProgressText(string data)=>UIListener.SetText(m_view.m_progress,data);
		string GetProgressText()=>UIListener.GetText(m_view.m_progress);
		void SetNextText(string data)=>UIListener.SetText(m_view.m_next,data);
		string GetNextText()=>UIListener.GetText(m_view.m_next);
		void SetNowText(string data)=>UIListener.SetText(m_view.m_now,data);
		string GetNowText()=>UIListener.GetText(m_view.m_now);
		void SetNow1Text(string data)=>UIListener.SetText(m_view.m_now1,data);
		string GetNow1Text()=>UIListener.GetText(m_view.m_now1);
		void SetTimeText(string data)=>UIListener.SetText(m_view.m_time,data);
		string GetTimeText()=>UIListener.GetText(m_view.m_time);
		void SetPriceText(string data)=>UIListener.SetText(m_view.m_price,data);
		string GetPriceText()=>UIListener.GetText(m_view.m_price);
		void SetUnlockText(string data)=>UIListener.SetText(m_view.m_unlock,data);
		string GetUnlockText()=>UIListener.GetText(m_view.m_unlock);
		void _OnClickBtnClick(EventContext data){
			OnClickBtnClick(data);
		}
		partial void OnClickBtnClick(EventContext data);
		void _OnClickClick(EventContext data){
			OnClickClick(data);
		}
		partial void OnClickClick(EventContext data);
		void SetClickText(string data)=>UIListener.SetText(m_view.m_click,data);
		string GetClickText()=>UIListener.GetText(m_view.m_click);
		void _OnAdBtnClick(EventContext data){
			OnAdBtnClick(data);
		}
		partial void OnAdBtnClick(EventContext data);
		void SetAdBtnText(string data)=>UIListener.SetText(m_view.m_adBtn,data);
		string GetAdBtnText()=>UIListener.GetText(m_view.m_adBtn);

	}
}
