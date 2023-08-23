
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Main;
	using Unity.Mathematics;
	
	public partial class UIMain
	{
		// 长按表示两秒
		private const float HOLD_LONG_TIME = 2.0f;
		
		private float m_holdTime = 0;
		private bool  m_holdEvent = false;

		private EventHandleContainer m_handles = new EventHandleContainer();
		
		partial void InitEvent(UIContext context)
		{
			context.onUpdate += OnTouchLoginUpdate;
			var clickBtn = m_view.m_battle.m_main;
			clickBtn.onClick.Add(OnBattleIconClick);

			clickBtn.onTouchBegin.Add(OnTouchBattleBegin);
			clickBtn.onTouchEnd.Add(OnTouchBattleEnd);

			m_handles += EventManager.Instance.Reg<int,long,int>((int)GameEvent.PROPERTY_GOLD, OnEventGoldChange);
			m_handles += EventManager.Instance.Reg<int,long,int,int>((int)GameEvent.PROPERTY_BANK, OnEventBankChange);
		}
		
		partial void UnInitEvent(UIContext context){

		}

		void OnEventGoldChange(int value, long newValue, int playerId)
		{
			log.Info("On Gold Update add =" + value + " newvalue=" + newValue + " plyaerid=" + playerId);
			SetGoldText(newValue.ToString());
			ShowNumberEffect("g+" + value, Color.red);
		}
		
		void OnEventBankChange(int value, long newValue, int buildingId, int playerId)
		{
			log.Info("On Bank Update add =" + value + " newvalue=" + newValue + "buildingid="+ buildingId + " plyaerid=" + playerId);
			ShowNumberEffect("b+" + value, Color.blue);
		}


		void OnTouchBattleBegin(EventContext e)
		{
			m_holdTime = HOLD_LONG_TIME;
			m_holdEvent = false;
		}

		void OnTouchBattleEnd(EventContext e)
		{
			m_holdTime = 0;
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
		}
		
		// 倍率设置
		partial void OnBattleBtn_PowerClick(EventContext datat)
		{
			EventManager.Instance.Trigger((int)GameEvent.PLAYER_POWER_DICE);
			
			// 显示ALL WIN 提示
			var v = DataCenter.Instance.GetUserSetting();
			string showTitle = string.Format("all win  X{0}", v.power);
			Color color = Color.blue;
			// 138,43,226
			if (v.power == v.maxPower)
				color = new Color(138 / 255.0f, 43 / 255.0f, 226 /255.0f);
			FloatTextRequest.CreateEntity(
				m_context.gameWorld.GetEntityManager(), showTitle,
				new Vector3(8.22000027f,-1.13f,5.09000015f), color, 50, 2.0f);
		}

		void OnBattleIconClick(EventContext context)
		{
			if (m_holdEvent == true)
				return;
			
			log.Info("on baccle icon click!");
			{
				autoDice = false;
				EventManager.Instance.Trigger((int)GameEvent.PLAYER_ROTE_DICE);
			}
		}

		void UnInitEvent()
		{
			m_handles.Close();
		}
	}
}
