
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Main;
	using Fibers;
	using System;
	using System.Collections;
	using Unity.Entities;
	using Unity.Mathematics;
	using Unity.Transforms;
	
	public partial class UIMain
	{
		private UserData         m_userData;
		private UserSetting      m_userSetting;
		private long             m_dicePower;
		private long             m_diceMaxPower;
		private long             m_glod;

		private GTextField       m_showText;
		private Fiber            m_numberEffect;
		private UIContext        m_context;
		private ItemGroup        m_userProperty;

		partial void InitLogic(UIContext context){
			m_context = context;
			m_numberEffect = new Fiber(FiberBucket.Manual);
			context.onUpdate += onUpdate;

			m_userProperty = PropertyManager.Instance.GetGroup(ItemType.USER);
			m_dicePower = -1;
			m_glod = m_userProperty.GetNum((int)UserType.GOLD);
			m_userData = DataCenter.Instance.GetUserData();
			m_userSetting = DataCenter.Instance.GetUserSetting();

			m_showText = m_view.m_goldFloating;
			SetGoldText(m_glod.ToString());
			UpdateBonusText();
		}
		
		void UpdateBonusText()
		{
			SetBattleBtn_PowerText("X" + m_userSetting.doubleBonus.ToString());
		}
		
		IEnumerator ShowNumberEffect(int num)
		{
			m_userData = DataCenter.Instance.GetUserData();
			EntityManager mgr = m_context.gameWorld.GetEntityManager();
			if (mgr.Exists(m_userData.player) == false)
				yield break;

			float3 pos = mgr.GetComponentData<Translation>(m_userData.player).Value;
			pos.y += 1.0f;
			Vector3 screenPoint = GameCamera.camera.WorldToScreenPoint(pos);
			screenPoint.y = Screen.height - screenPoint.y;

			Vector2 uiPos = m_context.content.GlobalToLocal(screenPoint);
			m_showText.alpha = 0;
			float wait = 1.0f;
			m_showText.text = num.ToString();
			for (float r = 0; r < wait;)
			{
				float per = r / wait;
				Vector2 runPos = uiPos;
				runPos.y -= per * 100;

				m_showText.xy = runPos;
				m_showText.alpha = math.clamp(per + 0.2f, 0, 1);
				r += Time.deltaTime;
				yield return null;
			}
			m_showText.alpha = 0;
			yield return null;
		}
		
		private void onUpdate(UIContext context)
		{
			UserSetting setting = DataCenter.Instance.GetUserSetting();
            
			if (m_glod != m_userProperty.GetNum((int)UserType.GOLD))
			{
				long cur = m_userProperty.GetNum((int)UserType.GOLD);
				long addvalue = cur - m_glod;
				m_glod = cur;
				SetGoldText(m_glod.ToString());
				m_numberEffect.Start(ShowNumberEffect((int)addvalue));
			}

			if (setting.doubleBonus != m_userSetting.doubleBonus)
			{
				m_userSetting.doubleBonus = setting.doubleBonus;
				UpdateBonusText();
			}

			if (m_dicePower    != m_userProperty.GetNum((int)UserType.DICE_POWER) ||
				m_diceMaxPower != m_userProperty.GetNum((int)UserType.DICE_MAXPOWER))
			{
				m_diceMaxPower = m_userProperty.GetNum((int)UserType.DICE_MAXPOWER);
				m_dicePower    = m_userProperty.GetNum((int)UserType.DICE_POWER);
				m_view.m_battle.m_countprogress.min = 0;
				m_view.m_battle.m_countprogress.max   = m_diceMaxPower;
				m_view.m_battle.m_countprogress.value = m_dicePower;
			}
			
			/// 显示道具恢复倒计时
			SetBattleBtn_TimeText(Utils.TimeFormat(DataCenter.Instance.GetDiceRecoverTime()));

			m_numberEffect.Step();
		}
		
		partial void UnInitLogic(UIContext context)
		{
			context.onUpdate -= onUpdate;
		}
	}
}
