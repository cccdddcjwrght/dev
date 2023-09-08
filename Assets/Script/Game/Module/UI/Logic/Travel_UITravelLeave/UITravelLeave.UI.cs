
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Travel;
	
	public partial class UITravelLeave
	{
		partial void InitUI(UIContext context){
			UIListener.ListenerIcon(m_view.m_head, new EventCallback1(_OnHeadClick));
			m_view.m_gold.m_addhide.onChanged.Add(new EventCallback1(_OnGold_AddhideChanged));
			m_view.m_gold.m_size.onChanged.Add(new EventCallback1(_OnGold_SizeChanged));
			m_view.m_gold.m_mode.onChanged.Add(new EventCallback1(_OnGold_ModeChanged));
			m_view.m_gold.m_titlemode.onChanged.Add(new EventCallback1(_OnGold_TitlemodeChanged));
			UIListener.ListenerIcon(m_view.m_gold, new EventCallback1(_OnGoldClick));

		}
		partial void UnInitUI(UIContext context){
			UIListener.ListenerIcon(m_view.m_head, new EventCallback1(_OnHeadClick),remove:true);
			m_view.m_gold.m_addhide.onChanged.Remove(new EventCallback1(_OnGold_AddhideChanged));
			m_view.m_gold.m_size.onChanged.Remove(new EventCallback1(_OnGold_SizeChanged));
			m_view.m_gold.m_mode.onChanged.Remove(new EventCallback1(_OnGold_ModeChanged));
			m_view.m_gold.m_titlemode.onChanged.Remove(new EventCallback1(_OnGold_TitlemodeChanged));
			UIListener.ListenerIcon(m_view.m_gold, new EventCallback1(_OnGoldClick),remove:true);

		}
		void _OnHeadClick(EventContext data){
			OnHeadClick(data);
		}
		partial void OnHeadClick(EventContext data);
		void _OnGold_AddhideChanged(EventContext data){
			OnGold_AddhideChanged(data);
		}
		partial void OnGold_AddhideChanged(EventContext data);
		void SwitchGold_AddhidePage(int index)=>m_view.m_gold.m_addhide.selectedIndex=index;
		void _OnGold_SizeChanged(EventContext data){
			OnGold_SizeChanged(data);
		}
		partial void OnGold_SizeChanged(EventContext data);
		void SwitchGold_SizePage(int index)=>m_view.m_gold.m_size.selectedIndex=index;
		void _OnGold_ModeChanged(EventContext data){
			OnGold_ModeChanged(data);
		}
		partial void OnGold_ModeChanged(EventContext data);
		void SwitchGold_ModePage(int index)=>m_view.m_gold.m_mode.selectedIndex=index;
		void _OnGold_TitlemodeChanged(EventContext data){
			OnGold_TitlemodeChanged(data);
		}
		partial void OnGold_TitlemodeChanged(EventContext data);
		void SwitchGold_TitlemodePage(int index)=>m_view.m_gold.m_titlemode.selectedIndex=index;
		void SetGold_ShadowText(string data)=>UIListener.SetText(m_view.m_gold.m_shadow,data);
		string GetGold_ShadowText()=>UIListener.GetText(m_view.m_gold.m_shadow);
		void _OnGoldClick(EventContext data){
			OnGoldClick(data);
		}
		partial void OnGoldClick(EventContext data);
		void SetGoldValue(float data)=>UIListener.SetValue(m_view.m_gold,data);
		float GetGoldValue()=>UIListener.GetValue(m_view.m_gold);
		void SetGoldText(string data)=>UIListener.SetText(m_view.m_gold,data);
		string GetGoldText()=>UIListener.GetText(m_view.m_gold);

	}
}
