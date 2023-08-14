
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Main;
	
	public partial class UIMain
	{
		partial void InitUI(UIContext context){
			m_view.m_main.onChanged.Add(new EventCallback1(_OnMainChanged));
			UIListener.Listener(m_view.m_setting, new EventCallback1(_OnSettingClick));
			UIListener.Listener(m_view.m_battle.m_power, new EventCallback1(_OnBattleBtn_PowerClick));
			UIListener.Listener(m_view.m_battle, new EventCallback1(_OnBattleClick));

		}
		partial void UnInitUI(UIContext context){
			m_view.m_main.onChanged.Remove(new EventCallback1(_OnMainChanged));
			UIListener.Listener(m_view.m_setting, new EventCallback1(_OnSettingClick),remove:true);
			UIListener.Listener(m_view.m_battle.m_power, new EventCallback1(_OnBattleBtn_PowerClick),remove:true);
			UIListener.Listener(m_view.m_battle, new EventCallback1(_OnBattleClick),remove:true);

		}
		void _OnMainChanged(EventContext data){
			OnMainChanged(data);
		}
		partial void OnMainChanged(EventContext data);
		void SwitchMainPage(int index)=>m_view.m_main.selectedIndex=index;
		void SetLevelValue(float data)=>UIListener.SetValue(m_view.m_level,data);
		float GetLevelValue()=>UIListener.GetValue(m_view.m_level);
		void SetLevelText(string data)=>UIListener.SetText(m_view.m_level,data);
		string GetLevelText()=>UIListener.GetText(m_view.m_level);
		void SetGoldValue(float data)=>UIListener.SetValue(m_view.m_gold,data);
		float GetGoldValue()=>UIListener.GetValue(m_view.m_gold);
		void SetGoldText(string data)=>UIListener.SetText(m_view.m_gold,data);
		string GetGoldText()=>UIListener.GetText(m_view.m_gold);
		void SetDiamondValue(float data)=>UIListener.SetValue(m_view.m_diamond,data);
		float GetDiamondValue()=>UIListener.GetValue(m_view.m_diamond);
		void SetDiamondText(string data)=>UIListener.SetText(m_view.m_diamond,data);
		string GetDiamondText()=>UIListener.GetText(m_view.m_diamond);
		void _OnSettingClick(EventContext data){
			OnSettingClick(data);
		}
		partial void OnSettingClick(EventContext data);
		void SetSettingText(string data)=>UIListener.SetText(m_view.m_setting,data);
		string GetSettingText()=>UIListener.GetText(m_view.m_setting);
		void _OnBattleBtn_PowerClick(EventContext data){
			OnBattleBtn_PowerClick(data);
		}
		partial void OnBattleBtn_PowerClick(EventContext data);
		void SetBattleBtn_PowerText(string data)=>UIListener.SetText(m_view.m_battle.m_power,data);
		string GetBattleBtn_PowerText()=>UIListener.GetText(m_view.m_battle.m_power);
		void SetBattleBtn_CountprogressValue(float data)=>UIListener.SetValue(m_view.m_battle.m_countprogress,data);
		float GetBattleBtn_CountprogressValue()=>UIListener.GetValue(m_view.m_battle.m_countprogress);
		void SetBattleBtn_CountprogressText(string data)=>UIListener.SetText(m_view.m_battle.m_countprogress,data);
		string GetBattleBtn_CountprogressText()=>UIListener.GetText(m_view.m_battle.m_countprogress);
		void SetBattleBtn_TimeText(string data)=>UIListener.SetText(m_view.m_battle.m_time,data);
		string GetBattleBtn_TimeText()=>UIListener.GetText(m_view.m_battle.m_time);
		void _OnBattleClick(EventContext data){
			OnBattleClick(data);
		}
		partial void OnBattleClick(EventContext data);
		void SetBattleText(string data)=>UIListener.SetText(m_view.m_battle,data);
		string GetBattleText()=>UIListener.GetText(m_view.m_battle);
		void SetGoldFloatingText(string data)=>UIListener.SetText(m_view.m_goldFloating,data);
		string GetGoldFloatingText()=>UIListener.GetText(m_view.m_goldFloating);

	}
}
