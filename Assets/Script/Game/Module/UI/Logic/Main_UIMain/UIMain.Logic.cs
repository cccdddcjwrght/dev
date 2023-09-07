
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

		private Fiber			 m_fiberShowTips;

		partial void InitLogic(UIContext context){
			m_context			= context;
			context.onUpdate	+= onUpdate;

			m_userProperty = PropertyManager.Instance.GetGroup(ItemType.USER);
			m_dicePower = -1;
			m_userData = DataCenter.Instance.GetUserData();
			m_userSetting = DataCenter.Instance.GetUserSetting();

			SetGoldText(m_userProperty.GetNum((int)UserType.GOLD).ToString());
			UpdateBonusText();

			m_fiberShowTips = FiberCtrl.Pool.AllocateFiber(FiberBucket.Manual);
		}

		EntityManager EntityManager
		{
			get
			{
				return m_context.gameWorld.GetEntityManager();
			}
		}

		/// <summary>
		/// 显示TIPS 特效
		/// </summary>
		/// <param name="tile1"></param>
		/// <param name="tile2"></param>
		/// <param name="isMax"></param>
		/// <param name="showTime"></param>
		/// <param name="hideTime"></param>
		/// <returns></returns>
		IEnumerator RunShowTips(string tile1, string tile2, bool isMax, float showTime, float hideTime)
		{
			var tipUI = m_view.m_tip;
			
			// 设置控制器
			tipUI.m_state.selectedIndex = isMax ? 1 : 0;

			tipUI.m_tile1.text = tile1;
			tipUI.m_tile2.text = tile2;
			
			tipUI.visible	= true;
			tipUI.alpha		= 1.0f;

			yield return FiberHelper.Wait(showTime);

			float passTime = 0.0f;
			while (passTime < hideTime)
			{
				passTime += Time.deltaTime;
				tipUI.alpha = math.clamp((hideTime - passTime) / hideTime, 0, 1.0f);
				yield return null;
			}

			tipUI.visible = false;
		}

		/// <summary>
		/// 显示tips
		/// </summary>
		/// <param name="tile1"></param>
		/// <param name="tile2"></param>
		/// <param name="isMax"></param>
		void ShowTips(string tile1, string tile2, bool isMax, float showTime = 2.0f, float hideTime = 0.2f)
		{
			m_fiberShowTips.Start(RunShowTips(tile1, tile2, isMax, showTime, hideTime));
		}

		void UpdateAutoUseUI(bool autoUse)
		{
			m_view.m_battle.m_main.m_auto_dice.selectedIndex = autoUse ? 1 : 0;
			UpdateDiceButtonState();
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
				DataCenter.Instance.SetUserSetting(v);
				UpdateAutoUseUI(value);
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
			m_fiberShowTips.Step();
			
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

		/// <summary>
		/// 更新骰子按钮状态
		/// </summary>
		void UpdateDiceButtonState()
		{
			// 更新按钮状态
			m_view.m_battle.m_main.m_state.selectedIndex = m_inPlaying ? 1 : 0;
		}
	}
}
