
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Rank;
	
	public partial class UIRankMain
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			m_view.m_state.onChanged.Add(new EventCallback1(_OnStateChanged));
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick));
			m_view.m_self.m_rankIndex.onChanged.Add(new EventCallback1(_OnRankItem_RankIndexChanged));
			UIListener.Listener(m_view.m_self.m_head, new EventCallback1(_OnRankItem_HaedClick));
			UIListener.ListenerIcon(m_view.m_self, new EventCallback1(_OnSelfClick));

		}
		partial void UnInitUI(UIContext context){
			m_view.m_state.onChanged.Remove(new EventCallback1(_OnStateChanged));
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick),remove:true);
			m_view.m_self.m_rankIndex.onChanged.Remove(new EventCallback1(_OnRankItem_RankIndexChanged));
			UIListener.Listener(m_view.m_self.m_head, new EventCallback1(_OnRankItem_HaedClick),remove:true);
			UIListener.ListenerIcon(m_view.m_self, new EventCallback1(_OnSelfClick),remove:true);

		}
		void _OnStateChanged(EventContext data){
			OnStateChanged(data);
		}
		partial void OnStateChanged(EventContext data);
		void SwitchStatePage(int index)=>m_view.m_state.selectedIndex=index;
		void DoCloseUIClick(EventContext data){
			 bool __closestate = true;
			 OnUICloseClick(ref __closestate);
			 if(__closestate)SGame.UIUtils.CloseUIByID(__id);
			 
		}
		partial void OnUICloseClick(ref bool state);
		void SetBodyText(string data)=>UIListener.SetText(m_view.m_body,data);
		string GetBodyText()=>UIListener.GetText(m_view.m_body);
		void SetTimeText(string data)=>UIListener.SetText(m_view.m_time,data);
		string GetTimeText()=>UIListener.GetText(m_view.m_time);
		void SetTipText(string data)=>UIListener.SetText(m_view.m_tip,data);
		string GetTipText()=>UIListener.GetText(m_view.m_tip);
		void _OnRankItem_RankIndexChanged(EventContext data){
			OnRankItem_RankIndexChanged(data);
		}
		partial void OnRankItem_RankIndexChanged(EventContext data);
		void SwitchRankItem_RankIndexPage(int index)=>m_view.m_self.m_rankIndex.selectedIndex=index;
		void SetRankItem_RankText(string data)=>UIListener.SetText(m_view.m_self.m_rank,data);
		string GetRankItem_RankText()=>UIListener.GetText(m_view.m_self.m_rank);
		void _OnRankItem_HaedClick(EventContext data){
			OnRankItem_HaedClick(data);
		}
		partial void OnRankItem_HaedClick(EventContext data);
		void SetRankItem_Self_haedText(string data)=>UIListener.SetText(m_view.m_self.m_head,data);
		string GetRankItem_Self_haedText()=>UIListener.GetText(m_view.m_self.m_head);
		void SetRankItem_NameText(string data)=>UIListener.SetText(m_view.m_self.m_name,data);
		string GetRankItem_NameText()=>UIListener.GetText(m_view.m_self.m_name);
		void SetRankItem_ValueText(string data)=>UIListener.SetText(m_view.m_self.m_value,data);
		string GetRankItem_ValueText()=>UIListener.GetText(m_view.m_self.m_value);
		void _OnSelfClick(EventContext data){
			OnSelfClick(data);
		}
		partial void OnSelfClick(EventContext data);
		void SetNoRankText(string data)=>UIListener.SetText(m_view.m_noRank,data);
		string GetNoRankText()=>UIListener.GetText(m_view.m_noRank);

	}
}
