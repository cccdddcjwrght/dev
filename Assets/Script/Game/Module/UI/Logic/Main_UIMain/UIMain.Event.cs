
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
		
		partial void InitEvent(UIContext context)
		{
			context.onUpdate += OnTouchLoginUpdate;
			var clickBtn = m_view.m_battle.m_main;
			clickBtn.onClick.Add(OnBattleIconClick);

			clickBtn.onTouchBegin.Add(OnTouchBattleBegin);
			clickBtn.onTouchEnd.Add(OnTouchBattleEnd);
		}
		
		partial void UnInitEvent(UIContext context){

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
		
		partial void OnBattleBtn_PowerClick(EventContext datat)
		{
			UserSetting setting = DataCenter.Instance.GetUserSetting();
			setting.doubleBonus = (setting.doubleBonus) % 5 + 1;
			DataCenter.Instance.SetUserSetting(setting);
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
	}
}
