
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Flight;
	
	public partial class UIFlight
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			UIListener.Listener(m_view.m_Gold, new EventCallback1(_OnGoldClick));
			UIListener.Listener(m_view.m_Diamond, new EventCallback1(_OnDiamondClick));
			UIListener.Listener(m_view.m_rank, new EventCallback1(_OnRankClick));
			UIListener.ListenerIcon(m_view.m_rankTran, new EventCallback1(_OnRankTranClick));
			UIListener.Listener(m_view.m_Box, new EventCallback1(_OnBoxClick));
			UIListener.Listener(m_view.m_Pet, new EventCallback1(_OnPetClick));
			UIListener.Listener(m_view.m_totalBtn, new EventCallback1(_OnTotalBtnClick));

		}
		partial void UnInitUI(UIContext context){
			UIListener.Listener(m_view.m_Gold, new EventCallback1(_OnGoldClick),remove:true);
			UIListener.Listener(m_view.m_Diamond, new EventCallback1(_OnDiamondClick),remove:true);
			UIListener.Listener(m_view.m_rank, new EventCallback1(_OnRankClick),remove:true);
			UIListener.ListenerIcon(m_view.m_rankTran, new EventCallback1(_OnRankTranClick),remove:true);
			UIListener.Listener(m_view.m_Box, new EventCallback1(_OnBoxClick),remove:true);
			UIListener.Listener(m_view.m_Pet, new EventCallback1(_OnPetClick),remove:true);
			UIListener.Listener(m_view.m_totalBtn, new EventCallback1(_OnTotalBtnClick),remove:true);

		}
		void _OnGoldClick(EventContext data){
			OnGoldClick(data);
		}
		partial void OnGoldClick(EventContext data);
		void SetGoldText(string data)=>UIListener.SetText(m_view.m_Gold,data);
		string GetGoldText()=>UIListener.GetText(m_view.m_Gold);
		void _OnDiamondClick(EventContext data){
			OnDiamondClick(data);
		}
		partial void OnDiamondClick(EventContext data);
		void SetDiamondText(string data)=>UIListener.SetText(m_view.m_Diamond,data);
		string GetDiamondText()=>UIListener.GetText(m_view.m_Diamond);
		void _OnRankClick(EventContext data){
			OnRankClick(data);
		}
		partial void OnRankClick(EventContext data);
		void SetRankText(string data)=>UIListener.SetText(m_view.m_rank,data);
		string GetRankText()=>UIListener.GetText(m_view.m_rank);
		void SetRankTran___textText(string data)=>UIListener.SetText(m_view.m_rankTran.m___text,data);
		string GetRankTran___textText()=>UIListener.GetText(m_view.m_rankTran.m___text);
		void _OnRankTranClick(EventContext data){
			OnRankTranClick(data);
		}
		partial void OnRankTranClick(EventContext data);
		void _OnBoxClick(EventContext data){
			OnBoxClick(data);
		}
		partial void OnBoxClick(EventContext data);
		void SetBoxText(string data)=>UIListener.SetText(m_view.m_Box,data);
		string GetBoxText()=>UIListener.GetText(m_view.m_Box);
		void _OnPetClick(EventContext data){
			OnPetClick(data);
		}
		partial void OnPetClick(EventContext data);
		void SetPetText(string data)=>UIListener.SetText(m_view.m_Pet,data);
		string GetPetText()=>UIListener.GetText(m_view.m_Pet);
		void _OnTotalBtnClick(EventContext data){
			OnTotalBtnClick(data);
		}
		partial void OnTotalBtnClick(EventContext data);

	}
}
