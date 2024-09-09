
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
		const string c_run_name = "run";
		const string c_attack_name = "dig";

		float c_normal_walk_speed = 1f;
		float c_explore_walk_speed = 1f;



		private Coroutine _exlogic;
		private UIModel _model;
		private bool _autoPutonNewEquip;

		private IEnumerator _checkExplore;
		private WaitForSeconds _waitAtk;
		private WaitForSeconds _waitDead;
		private WaitForSeconds _waitRest;

		private bool fightState { get { return m_view.m_exploreState.selectedIndex == 1; } set { m_view.m_exploreState.selectedIndex = value ? 1 : 0; } }
		private bool autoState { get { return m_view.m_exploreAuto.selectedIndex == 1; } }

		private bool isCombat { get { return BattleManager.Instance.isCombat; } }

		private FightEquip[] _tempEqs = new FightEquip[16];

		void InitPlay()
		{
			onOpen += OnOpen_Play;
			onHide += OnHide_Play;
			InitRole();

			_checkExplore = new WaitUntil(CheckCmd);
			_waitAtk = new WaitForSeconds(1f);
			_waitDead = new WaitForSeconds(0.8f);
			_waitRest = new WaitForSeconds(1f);

			c_normal_walk_speed = GlobalDesginConfig.GetFloat("explore_normal_walk_speed", c_normal_walk_speed);
			c_explore_walk_speed = GlobalDesginConfig.GetFloat("explore_fast_walk_speed", c_explore_walk_speed);


			((Action)CancelAuto).CallWhenPause();
			eventHandle += EventManager.Instance.Reg<bool>(((int)GameEvent.EXPLORE_AUTO_TOGGLE), OnAutoEvent);

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
			if (isCombat) return;

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
			if (isCombat) return;


			if (!autoState)
			{
				if (42.IsOpend(true, "ui_explore_auto_tips".Local()))
				{
					if (CheckItem(true))
					{
						SGame.UIUtils.OpenUI("exploreautoset");
					}
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

		void OnAutoEvent(bool state)
		{
			SwitchExploreAutoPage(state ? 1 : 0);
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
			var flag = true;
			while (flag)
			{
				yield return _checkExplore;
				if (DataLogic()) break;
				else StopExplore();

			}

			_model.Play(c_run_name, c_explore_walk_speed);
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
			51.ToAudioID().PlayAudio();
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
			52.ToAudioID().PlayAudio();
			MapLoop(true);
			_model.Play(c_attack_name);
			yield return _waitAtk;
			m_view.m_kill.Play();
			EffectSystem.Instance.AddEffect(67, m_view.m_monstereffect);
			yield return _waitDead;
			if (!autoState) Fast(true);
		}

		IEnumerator WaitEquipHandler()
		{
			ShowNewEquip();
			yield return new WaitUntil(() => (exploreData.cacheEqs == null || exploreData.cacheEqs.Count == 0) && !exploreData.waitFlag);
		}

		IEnumerator CheckComplete()
		{
			_model.Play(c_walk_name);
			MapLoop(false);
			var end = true;
			if (autoState)
			{
				if (CheckItem(true, exploreData.autoCfg.cost))
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

		bool CheckItem(bool tips = false, int need = 1)
		{
			var count = PropertyManager.Instance.GetItem(ConstDefine.EXPLORE_ITEM).num;
			var r = count >= need;
			if (!r && tips)
				"@ui_explore_tool_not_enough".Tips();
			return r;
		}

		bool DataLogic()
		{
			var cost = autoState ? exploreData.autoCfg.cost : 1;
			if (CheckItem(true, cost))
			{
				PropertyManager.Instance.Update(1, ConstDefine.EXPLORE_ITEM, cost, true);
				RequestExcuteSystem.ExploreSuccess(cost);
				return true;
			}
			return false;
		}

		void ShowNewEquip()
		{
			if (exploreData.cacheEqs?.Count > 0)
			{
				if (autoState)
				{
					AutoFilter();
					if (exploreData.cacheEqs?.Count == 0) return;
				}
				SGame.UIUtils.OpenUI("fightequiptips", exploreData.cacheEqs);
			}
		}

		void CancelAuto()
		{
			SwitchExploreAutoPage(0);
		}

		void StopExplore()
		{
			SwitchExploreAutoPage(0);
			fightState = false;
			ResetExplore();
		}

		void RefreshAutoState()
		{
			m_view.m_auto.grayed = !42.IsOpend(false) || isCombat;
		}

		void ResetExplore()
		{
			m_view.m_monster.alpha = 0;//怪隐藏
			_model?.Play(c_walk_name, c_normal_walk_speed);
			Fast(true);
		}

		void AutoFilter()
		{
			if (exploreData.cacheEqs?.Count > 0)
			{
				var eqs = exploreData.cacheEqs;
				var index = 0;
				for (int i = eqs.Count - 1; i >= 0; i--)
				{
					var item = eqs[i];
					if (!exploreData.Filter(item))
						_tempEqs[index++] = item;
				}
				if (index > 0) RequestExcuteSystem.ExplorePutOnEquip(null, _tempEqs);
				Array.Clear(_tempEqs, 0, _tempEqs.Length);
			}
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
