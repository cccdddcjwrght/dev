
using GameConfigs;
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Main;
	using Unity.Mathematics;
	
	public partial class UIMain
	{
		// 长按表示两秒
		private float HOLD_LONG_TIME = 2.0f;
		private const string SHOW_TIPS_TIME = "show_power_tips_time";
		private const string HIDE_TIPS_TIME = "hide_power_tips_time";
		private const string HOLD_TIME		= "dice_button_hold_time";
		
		private float m_holdTime = 0;
		private bool  m_holdEvent = false;
		private bool m_inPlaying = false;

		private EventHandleContainer m_handles = new EventHandleContainer();
		
		partial void InitEvent(UIContext context)
		{
			HOLD_LONG_TIME = GlobalDesginConfig.GetFloat(HOLD_TIME);
			context.onUpdate += OnTouchLoginUpdate;
			var clickBtn = m_view.m_battle.m_main;
			clickBtn.onClick.Add(OnBattleIconClick);

			clickBtn.onTouchBegin.Add(OnTouchBattleBegin);
			clickBtn.onTouchEnd.Add(OnTouchBattleEnd);

			m_handles += EventManager.Instance.Reg<int,long,int>((int)GameEvent.PROPERTY_GOLD,			OnEventGoldChange);
			m_handles += EventManager.Instance.Reg<int, long, int>((int)GameEvent.PROPERTY_TRAVEL_GOLD, OnEventTravelGoldChange);
			m_handles += EventManager.Instance.Reg<int,long,int,int>((int)GameEvent.PROPERTY_BANK,		OnEventBankChange);
			m_handles += EventManager.Instance.Reg<bool>((int)GameEvent.TRAVEL_START,							OnTravelStart);
			m_handles += EventManager.Instance.Reg<bool>((int)GameEvent.TRAVEL_END,                           OnTravelEnd);
			m_handles += EventManager.Instance.Reg((int)GameEvent.GAME_WAIT_NEXTROUND,                  OnEventWaitNextGameRound);
			m_handles += EventManager.Instance.Reg((int)GameEvent.GAME_NEXTROUND,						OnEventNextGameRound);
		}

		private void OnTravelStart(bool isTravel)
		{
			m_view.visible = false;
			m_view.m_battle.m_travel.selectedIndex = isTravel ? 1 : 0;
		}
		private void OnTravelEnd(bool isTravel)
		{
			UpdateAutoUseUI(DataCenter.Instance.GetUserSetting());
			m_view.visible = true;
		}

		partial void UnInitEvent(UIContext context){

		}

		// 金币添加事件
		void OnEventGoldChange(int value, long newValue, int playerId)
		{
			log.Info("On Gold Update add =" + value + " newvalue=" + newValue + " plyaerid=" + playerId);
			SetGoldText(newValue.ToString());
			ShowNumberEffect("g+" + value, Color.red);
		}

		// 出行金币添加事件
		void OnEventTravelGoldChange(int value, long newValue, int playerId)
		{
			log.Info("On OnEventTravelGoldChange add =" + value + " newvalue=" + newValue + " plyaerid=" + playerId);
			//SetGoldText(newValue.ToString());
			ShowNumberEffect("g+" + value, Color.yellow);
		}
		
		void OnEventBankChange(int value, long newValue, int buildingId, int playerId)
		{
			log.Info("On Bank Update add =" + value + " newvalue=" + newValue + "buildingid="+ buildingId + " plyaerid=" + playerId);
			if (value > 0)
			{
				// 存钱
				ShowNumberEffect("b+" + value, Color.blue);
			}
			else
			{
				// 取钱
				ShowNumberEffect("b-" + value, Color.blue);

				UpdateGoldText();
			}
		}

		void UpdateGoldText()
		{
			SetGoldText(m_userProperty.GetNum((int)UserType.GOLD).ToString());
		}


		void OnTouchBattleBegin(EventContext e)
		{
			m_holdTime = HOLD_LONG_TIME;
			m_holdEvent = false;
			m_view.m_battle.m_main.m_state.selectedIndex = 1;
		}

		void OnTouchBattleEnd(EventContext e)
		{
			m_holdTime = 0;
			//UpdateDiceButtonState();
		}

		void OnTouchLoginUpdate(UIContext e)
		{
			if (m_holdTime > 0 && m_holdEvent == false)
			{
				m_holdTime = math.clamp(m_holdTime - Time.deltaTime, 0, float.MaxValue);
				if (m_holdTime <= 0)
					OnBattleLongClick();
			}
		}

		void OnBattleLongClick()
		{
			m_holdEvent = true;
			autoDice = true;
			//UpdateDiceButtonState();
		}
		
		// 倍率设置
		partial void OnBattleBtn_PowerClick(EventContext datat)
		{
			float showTime = GlobalDesginConfig.GetFloat(SHOW_TIPS_TIME);
			float hideTime = GlobalDesginConfig.GetFloat(HIDE_TIPS_TIME);
			EventManager.Instance.Trigger((int)GameEvent.PLAYER_POWER_DICE);

			// 显示ALL WIN 提示
			var v = DataCenter.Instance.GetUserSetting();
			string showTitle = string.Format("X{0}", v.power);
			if (v.power == v.maxPower)
				showTitle = "MAX";
			
			ShowTips("ALL WIN", showTitle, v.power == v.maxPower, 
				showTime, 
				hideTime);
			
			autoDice = false;
			UpdateDiceButtonState();
		}
		
		

		void OnBattleIconClick(EventContext context)
		{
			if (m_holdEvent == true)
				return;
			
			log.Info("on baccle icon click!");
			autoDice = false;
			if (Utils.PlayerIsMoving(EntityManager))
			{
				return;
			}
			
			m_userSetting = DataCenter.Instance.GetUserSetting();
			if (m_userSetting.power > (int)m_userProperty.GetNum((int)UserType.DICE_NUM))
			{
				UpdateDiceButtonState();
			}
			EventManager.Instance.Trigger((int)GameEvent.PLAYER_ROTE_DICE);
		}

		void OnEventNextGameRound()
		{
			m_inPlaying = true;
			UpdateDiceButtonState();
		}
		
		void OnEventWaitNextGameRound()
		{
			m_inPlaying = false;
			UpdateDiceButtonState();
		}

		void UnInitEvent()
		{
			m_handles.Close();
		}
	}
}
