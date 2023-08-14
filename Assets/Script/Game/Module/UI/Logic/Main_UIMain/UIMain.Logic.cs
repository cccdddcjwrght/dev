﻿
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
		private GTextField       m_showText;
		private Fiber            m_numberEffect;
		private UIContext        m_context;

		partial void InitLogic(UIContext context){
			m_context = context;
			m_numberEffect = new Fiber(FiberBucket.Manual);
			context.onUpdate += onUpdate;

            
			m_userData = DataCenter.Instance.GetUserData();
			m_userSetting = DataCenter.Instance.GetUserSetting();

			m_showText = m_view.m_goldFloating;
			SetGoldText(m_userData.gold.ToString());
			UpdateBonusText();
		}
		
		void UpdateBonusText()
		{
			SetBattleBtn_PowerText("X" + m_userSetting.doubleBonus.ToString());
		}
		
		IEnumerator ShowNumberEffect(int num)
		{
			EntityManager mgr = m_context.gameWorld.GetEntityManager();
			if (mgr.Exists(m_userData.player) == false)
				yield break;

			float3 pos = mgr.GetComponentData<Translation>(m_userData.player).Value;
			pos.y += 1.0f;
			Vector3 screenPoint = GameCamera.camera.WorldToScreenPoint(pos);
			screenPoint.y = Screen.height - screenPoint.y;

			Vector2 uiPos = m_context.content.GlobalToLocal(screenPoint);
			//GRoot.inst.LocalToGlobal(screenPoint)
			m_showText.alpha = 0;
			float wait = 1.0f;
			m_showText.text = num.ToString();
			//m_view.m_goldFloating.text = num.ToString();
			for (float r = 0; r < wait;)
			{
				// pos = mgr.GetComponentData<Translation>(userData.player).Value;
				//m_context.content.scree
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
			UserData cur = DataCenter.Instance.GetUserData();
			UserSetting setting = DataCenter.Instance.GetUserSetting();
            
			if (cur.gold != m_userData.gold)
			{
				int addvalue = cur.gold - m_userData.gold;
				m_userData = cur;
				//uiGold.Value = m_userData.gold;
				SetGoldText(m_userData.gold.ToString());
				m_numberEffect.Start(ShowNumberEffect(addvalue));
			}

			if (setting.doubleBonus != m_userSetting.doubleBonus)
			{
				m_userSetting.doubleBonus = setting.doubleBonus;
				UpdateBonusText();
			}
            
            

			m_numberEffect.Step();
		}

		
		partial void UnInitLogic(UIContext context)
		{
			context.onUpdate -= onUpdate;
		}
	}
}
