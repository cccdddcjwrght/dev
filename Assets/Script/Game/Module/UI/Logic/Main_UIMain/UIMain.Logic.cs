
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

		private UIContext        m_context;
		private ItemGroup        m_userProperty;

		partial void InitLogic(UIContext context){
			m_context			= context;
			context.onUpdate	+= onUpdate;

			m_userProperty = PropertyManager.Instance.GetGroup(ItemType.USER);
			m_dicePower = -1;
			m_glod = m_userProperty.GetNum((int)UserType.GOLD);
			m_userData = DataCenter.Instance.GetUserData();
			m_userSetting = DataCenter.Instance.GetUserSetting();

			SetGoldText(m_glod.ToString());
			UpdateBonusText();
		}
		
		bool autoDice
		{
			get
			{
				var v = DataCenter.Instance.GetUserSetting();
				return v.autoUse;
			}

			set
			{
				var v = DataCenter.Instance.GetUserSetting();
				v.autoUse = value;
				m_view.m_battle.m_main.m_auto_dice.selectedIndex = value ? 1 : 0;
				DataCenter.Instance.SetUserSetting(v);
			}
		}

		void UpdateBonusText()
		{
			SetBattleBtn_PowerText("X" + m_userSetting.doubleBonus.ToString());
			if (m_userSetting.doubleBonus != m_userSetting.maxBonus)
			{
				m_view.m_battle.title = "X" + m_userSetting.maxBonus.ToString();
				m_view.m_battle.m_maxtext.visible = true;
			}
			else
			{
				m_view.m_battle.title = "";
				m_view.m_battle.m_maxtext.visible = false;
			}
		}
		
		void ShowNumberEffect(int num)
		{
			m_userData = DataCenter.Instance.GetUserData();
			EntityManager mgr = m_context.gameWorld.GetEntityManager();
			if (mgr.Exists(m_userData.player) == false)
				return;

			float3 pos = mgr.GetComponentData<Translation>(m_userData.player).Value;
			FloatTextRequest.CreateEntity(mgr, num.ToString(), pos, Color.red, 40,1.0f);
		}
		
		private  void onUpdate(UIContext context)
		{
			UserSetting setting = DataCenter.Instance.GetUserSetting();
            
			// 更新金币数量
			if (m_glod != m_userProperty.GetNum((int)UserType.GOLD))
			{
				long cur = m_userProperty.GetNum((int)UserType.GOLD);
				long addvalue = cur - m_glod;
				m_glod = cur;
				SetGoldText(m_glod.ToString());
				ShowNumberEffect((int)addvalue);
			}

			// 更新倍率设置
			if (setting.doubleBonus != m_userSetting.doubleBonus)
			{
				m_userSetting = setting;//.doubleBonus = setting.doubleBonus;
				UpdateBonusText();
			}

			// 更新骰子进度
			var diceMaxPower = m_userProperty.GetNum((int)UserType.DICE_MAXPOWER);
			var dicePower    = m_userProperty.GetNum((int)UserType.DICE_POWER);
			
			if (m_dicePower    != diceMaxPower ||
				m_diceMaxPower != dicePower)
			{
				m_diceMaxPower	= diceMaxPower;
				m_dicePower		= dicePower;
				m_view.m_battle.m_countprogress.min   = 0;
				m_view.m_battle.m_countprogress.max   = m_diceMaxPower;
				m_view.m_battle.m_countprogress.value = m_dicePower;
			}
			
			/// 显示道具恢复倒计时
			if (diceMaxPower != dicePower)
			{
				SetBattleBtn_TimeText("5 Rolls in " + Utils.TimeFormat(DataCenter.Instance.GetDiceRecoverTime()));
			}
			else
			{
				m_view.m_battle.m_time.text = "";
			}
		}
		
		partial void UnInitLogic(UIContext context)
		{
			context.onUpdate -= onUpdate;
		}
	}
}
