
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
			m_view.m_battle.m_max.onChanged.Add(new EventCallback1(_OnBattleBtn_MaxChanged));
			m_view.m_battle.m_main.m_auto_dice.onChanged.Add(new EventCallback1(_OnDiceBtn_Auto_diceChanged));
			UIListener.Listener(m_view.m_battle.m_main, new EventCallback1(_OnBattleBtn_MainClick));
			UIListener.Listener(m_view.m_battle.m_power, new EventCallback1(_OnBattleBtn_PowerClick));
			UIListener.Listener(m_view.m_battle, new EventCallback1(_OnBattleClick));
			m_view.m_tip.m_state.onChanged.Add(new EventCallback1(_OnTip_StateChanged));
			UIListener.ListenerIcon(m_view.m_tip, new EventCallback1(_OnTipClick));

		}
		partial void UnInitUI(UIContext context){
			m_view.m_main.onChanged.Remove(new EventCallback1(_OnMainChanged));
			UIListener.Listener(m_view.m_setting, new EventCallback1(_OnSettingClick),remove:true);
			m_view.m_battle.m_max.onChanged.Remove(new EventCallback1(_OnBattleBtn_MaxChanged));
			m_view.m_battle.m_main.m_auto_dice.onChanged.Remove(new EventCallback1(_OnDiceBtn_Auto_diceChanged));
			UIListener.Listener(m_view.m_battle.m_main, new EventCallback1(_OnBattleBtn_MainClick),remove:true);
			UIListener.Listener(m_view.m_battle.m_power, new EventCallback1(_OnBattleBtn_PowerClick),remove:true);
			UIListener.Listener(m_view.m_battle, new EventCallback1(_OnBattleClick),remove:true);
			m_view.m_tip.m_state.onChanged.Remove(new EventCallback1(_OnTip_StateChanged));
			UIListener.ListenerIcon(m_view.m_tip, new EventCallback1(_OnTipClick),remove:true);

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
		void _OnBattleBtn_MaxChanged(EventContext data){
			OnBattleBtn_MaxChanged(data);
		}
		partial void OnBattleBtn_MaxChanged(EventContext data);
		void SwitchBattleBtn_MaxPage(int index)=>m_view.m_battle.m_max.selectedIndex=index;
		void _OnDiceBtn_Auto_diceChanged(EventContext data){
			OnDiceBtn_Auto_diceChanged(data);
		}
		partial void OnDiceBtn_Auto_diceChanged(EventContext data);
		void SwitchDiceBtn_Auto_dicePage(int index)=>m_view.m_battle.m_main.m_auto_dice.selectedIndex=index;
		void _OnBattleBtn_MainClick(EventContext data){
			OnBattleBtn_MainClick(data);
		}
		partial void OnBattleBtn_MainClick(EventContext data);
		void SetBattleBtn_MainText(string data)=>UIListener.SetText(m_view.m_battle.m_main,data);
		string GetBattleBtn_MainText()=>UIListener.GetText(m_view.m_battle.m_main);
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
		void SetBattleBtn_MaxtextText(string data)=>UIListener.SetText(m_view.m_battle.m_maxtext,data);
		string GetBattleBtn_MaxtextText()=>UIListener.GetText(m_view.m_battle.m_maxtext);
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
		void _OnTip_StateChanged(EventContext data){
			OnTip_StateChanged(data);
		}
		partial void OnTip_StateChanged(EventContext data);
		void SwitchTip_StatePage(int index)=>m_view.m_tip.m_state.selectedIndex=index;
		void SetTip_Tile1Text(string data)=>UIListener.SetText(m_view.m_tip.m_tile1,data);
		string GetTip_Tile1Text()=>UIListener.GetText(m_view.m_tip.m_tile1);
		void SetTip_Tile2Text(string data)=>UIListener.SetText(m_view.m_tip.m_tile2,data);
		string GetTip_Tile2Text()=>UIListener.GetText(m_view.m_tip.m_tile2);
		void _OnTipClick(EventContext data){
			OnTipClick(data);
		}
		partial void OnTipClick(EventContext data);

	}
}
