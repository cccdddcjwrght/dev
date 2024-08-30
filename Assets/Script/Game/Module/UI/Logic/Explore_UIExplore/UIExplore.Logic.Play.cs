
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Explore;
	using SGame.UI.Common;
	using SGame.UI.Pet;
	using System.Collections;
	using System.IO;
	using System;
	using GameConfigs;

	public enum MoveState
	{
		None,
		Moving,
		Completed,
	}

	class MoveInfo
	{
		public MoveState flag;
		public float target;

		public bool state;
		public float last;
	}

	public partial class UIExplore
	{
		const string c_walk_name = "walk";
		const string c_attack_name = "dig";

		float c_normal_walk_speed = 0.8f;
		float c_explore_walk_speed = 1.2f;



		private Coroutine _exlogic;
		private UIModel _model;
		private bool _autoPutonNewEquip;

		private IEnumerator _checkExplore;
		private WaitForSeconds _waitAtk;
		private WaitForSeconds _waitDead;
		private WaitForSeconds _waitRest;

		private bool fightState { get { return m_view.m_exploreState.selectedIndex == 1; } set { m_view.m_exploreState.selectedIndex = value ? 1 : 0; } }
		private bool autoState { get { return m_view.m_exploreAuto.selectedIndex == 1; } }
		private MoveInfo move { get; set; } = new MoveInfo();


		void InitPlay()
		{
			onOpen += OnOpen_Play;
			onHide += OnHide_Play;
			InitRole();

			_checkExplore = new WaitUntil(CheckCmd);
			_waitAtk = new WaitForSeconds(1f);
			_waitDead = new WaitForSeconds(0.5f);
			_waitRest = new WaitForSeconds(1f);

			c_normal_walk_speed = GlobalDesginConfig.GetFloat("explore_normal_walk_speed", c_normal_walk_speed);
			c_explore_walk_speed = GlobalDesginConfig.GetFloat("explore_fast_walk_speed", c_explore_walk_speed);


			((Action)CancelAuto).CallWhenPause();

#if UNITY_EDITOR
			eventHandle += EventManager.Instance.Reg(-((int)GameEvent.EXPLORE_CHNAGE_EQUIP), () => _autoPutonNewEquip = !_autoPutonNewEquip);
#endif
		}

		void OnOpen_Play(UIContext context)
		{
			_model.RefreshModel(c_walk_name);
			_exlogic?.Stop();
			_exlogic = Logic().Start();
			RefreshAutoState();
			ShowNewEquip();
		}

		void OnHide_Play(UIContext context)
		{
			_model.ClearModel();
			ResetExplore();
			SwitchExploreAutoPage(0);
			fightState = false;
			_exlogic?.Stop();
		}

		partial void OnFindClick(EventContext data)
		{
			if (fightState)
			{
				"@ui_explore_work_tips".Tips();
				return;
			}
			if (CheckItem())
				fightState = true;
			else
				"shop".Goto();
		}

		partial void OnAutoClick(EventContext data)
		{
			if (!autoState)
			{
				if (42.IsOpend(true, "ui_explore_auto_tips".Local()))
				{
					if (CheckItem(true))
						SwitchExploreAutoPage(1);
				}
			}
			else
			{
				SwitchExploreAutoPage(0);
			}
		}

		partial void OnExploreAutoChanged(EventContext data)
		{
			if (!autoState)
			{
				m_view.m_auto.SetTextByKey("ui_explore_auto");
				SetExploreToolInfo(false);
			}
			else
				m_view.m_auto.SetTextByKey("ui_explore_stop");
		}

		partial void OnExploreStateChanged(EventContext data)
		{
			if (!fightState)
				SetExploreToolInfo(false);
		}

		#region ExploreLogic

		IEnumerator Logic()
		{
			MapLoop(false);
			yield return new WaitUntil(() => _model.IsLoadCompleted());
			ResetExplore();
			while (m_view != null && m_view.visible)
			{
				yield return WaitReq();
				yield return MoveMonster();
				yield return DoExplore();
				yield return WaitEquipHandler();
				yield return CheckComplete();
			}
		}

		IEnumerator WaitReq()
		{
			yield return _checkExplore;
			_model.Play(c_walk_name,1.2f);
			DataLogic();
		}

		GObject SpawnMonster()
		{
			m_view.m_monster.xy = m_view.m_mholder.xy;
			m_view.m_monster.alpha = 1;
			m_view.m_monster.scale = Vector2.one;
			m_view.m_monster.visible = true;
			return m_view.m_monster;
		}

		IEnumerator MoveMonster()
		{
			Fast();
			yield return null;
			var m = SpawnMonster();
			var flag = false;
			var target = m_view.m_holder.x + 130;
			m.TweenMoveX(target, Mathf.Abs(target - m.x) / GetSpeed()).SetEase(EaseType.Linear).OnComplete(() => flag = true);
			yield return new WaitUntil(() => flag);
		}

		IEnumerator DoExplore()
		{
			MapLoop(true);
			_model.Play(c_attack_name);
			yield return _waitAtk;
			m_view.m_kill.Play();
			yield return _waitDead;
			if (!autoState) Fast(true);
		}

		IEnumerator WaitEquipHandler()
		{
			ShowNewEquip();
			yield return new WaitUntil(() => (exploreData.cacheEquip == null || exploreData.cacheEquip.cfgID == 0) && !exploreData.waitFlag);
		}

		IEnumerator CheckComplete()
		{
			_model.Play(c_walk_name);
			MapLoop(false);
			var end = true;
			if (autoState)
			{
				if (CheckItem(true))
				{
					end = false;
					yield return _waitRest;
				}
				else
					SwitchExploreAutoPage(0);
			}
			else
			{
				yield return null;
			}
			if (end) ResetExplore();
			fightState = !end;
		}

		bool CheckCmd()
		{

			return fightState || autoState;

		}

		bool CheckItem(bool tips = false)
		{
			var count = PropertyManager.Instance.GetItem(ConstDefine.EXPLORE_ITEM).num;
			var r = count > 0;
			if (!r && tips)
				"@ui_explore_tool_not_enough".Tips();
			return r;
		}

		void DataLogic()
		{
			PropertyManager.Instance.Update(1, ConstDefine.EXPLORE_ITEM, 1, true);
			RequestExcuteSystem.ExploreSuccess();
		}

		void ShowNewEquip()
		{
			if (exploreData.cacheEquip?.cfgID > 0)
			{
				var neq = exploreData.cacheEquip;
				if (autoState)
				{
					var eq = exploreData.explorer.GetEquip(exploreData.cacheEquip.type - 10);
					if (eq?.cfgID > 0 && neq.power <= eq.power)
					{
						RequestExcuteSystem.ExplorePutOnEquip(null, neq);
						return;
					}

#if !SVR_RELEASE && UNITY_EDITOR
					if (_autoPutonNewEquip)
					{
						RequestExcuteSystem.ExplorePutOnEquip(neq, eq);
						return;
					}
#endif

				}
				SGame.UIUtils.OpenUI("fightequiptips", exploreData.cacheEquip);
			}
		}

		void CancelAuto()
		{
			SwitchExploreAutoPage(0);
		}

		void RefreshAutoState()
		{
			m_view.m_auto.grayed = !42.IsOpend(false);
		}

		void ResetExplore()
		{
			m_view.m_monster.alpha = 0;//怪隐藏
			_model?.Play(c_walk_name, 0.8f);
			Fast(true);
		}

		#endregion

		#region Role

		void InitRole()
		{
			m_view.z = 400;
			m_view.m_holder.z = -200;
			_model = new UIModel(m_view.m_holder)
				.SetData(exploreData.explorer)
				.SetTRS(Vector3.zero, new Vector3(-5, 145, -15), 150)
				.SetLoad(LoadModel);
		}

		IEnumerator LoadModel(object data)
		{
			return Utils.GenCharacter(exploreData.explorer.GetRoleModelString());
		}

		#endregion
	}
}
