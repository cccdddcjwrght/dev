
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

		private UIContext        m_context;
		private ItemGroup        m_userProperty;

		partial void InitLogic(UIContext context){
			m_context			= context;
			context.onUpdate	+= onUpdate;

			m_userProperty = PropertyManager.Instance.GetGroup(ItemType.USER);
			m_dicePower = -1;
			m_userData = DataCenter.Instance.GetUserData();
			m_userSetting = DataCenter.Instance.GetUserSetting();

			SetGoldText(m_userProperty.GetNum((int)UserType.GOLD).ToString());
			UpdateBonusText();
		}

		void UpdateAutoUseUI(bool autoUse)
		{
			m_view.m_battle.m_main.m_auto_dice.selectedIndex = autoUse ? 1 : 0;
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
				UpdateAutoUseUI(value);
				DataCenter.Instance.SetUserSetting(v);
			}
		}

		void UpdateBonusText()
		{
			SetBattleBtn_PowerText("X" + m_userSetting.power.ToString());
			if (m_userSetting.power != m_userSetting.maxPower)
			{
				m_view.m_battle.title = "X" + m_userSetting.maxPower.ToString();
			}
			else
			{
				m_view.m_battle.title = "";
			}
		}
		
		void ShowNumberEffect(string num, Color color)
		{
			m_userData = DataCenter.Instance.GetUserData();
			EntityManager mgr = m_context.gameWorld.GetEntityManager();
			if (mgr.Exists(m_userData.player) == false)
				return;

			float3 pos = mgr.GetComponentData<Translation>(m_userData.player).Value;
			FloatTextRequest.CreateEntity(mgr, num, pos, color, 40,1.0f);
		}
		
		private  void onUpdate(UIContext context)
		{
			UserSetting setting = DataCenter.Instance.GetUserSetting();
			if (m_userSetting.autoUse != setting.autoUse)
			{
				// 更新自动使用
				UpdateAutoUseUI(setting.autoUse);
			}
			
			// 设置最大控制状态
			m_view.m_battle.m_max.selectedIndex = setting.power != setting.maxPower ? 1 : 0;

			// 更新倍率设置
			if (setting.power != m_userSetting.power)
			{
				m_userSetting = setting;//.doubleBonus = setting.doubleBonus;
				UpdateBonusText();
			}

			// 更新骰子进度
			var diceMaxPower = m_userProperty.GetNum((int)UserType.DICE_MAXNUM);
			var dicePower    = m_userProperty.GetNum((int)UserType.DICE_NUM);
			
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
			m_userSetting = setting;
		}
		
		partial void UnInitLogic(UIContext context)
		{
			context.onUpdate -= onUpdate;
		}
	}
}
