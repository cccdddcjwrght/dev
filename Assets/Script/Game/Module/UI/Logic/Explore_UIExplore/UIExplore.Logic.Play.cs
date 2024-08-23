
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
		private Coroutine _exlogic;
		private UIModel _model;

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
			_waitAtk = new WaitForSeconds(0.3f);
			_waitDead = new WaitForSeconds(0.5f);
			_waitRest = new WaitForSeconds(2f);

			((Action)CancelAuto).CallWhenPause();
		}

		void OnOpen_Play(UIContext context)
		{
			_exlogic?.Stop();
			_exlogic = Logic().Start();
			_model.RefreshModel("walk");
			ShowNewEquip();
		}

		void OnHide_Play(UIContext context)
		{
			_model.ClearModel();
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
		}

		partial void OnAutoClick(EventContext data)
		{
			if (!autoState)
			{
				if ("exploreauto".IsOpend(true, "ui_explore_auto_tips".Local()))
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
				SetExploreToolInfo(false);
		}

		partial void OnExploreStateChanged(EventContext data)
		{
		}

		#region ExploreLogic

		IEnumerator Logic()
		{
			MapLoop(false);
			while (m_view.visible)
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
			if (!autoState) Fast(true);
		}

		IEnumerator DoExplore()
		{
			MapLoop(true);
			_model.Play("attack");
			yield return _waitAtk;
			m_view.m_kill.Play();
			yield return _waitDead;
		}

		IEnumerator WaitEquipHandler()
		{
			ShowNewEquip();
			yield return new WaitUntil(() => exploreData.cacheEquip == null || exploreData.cacheEquip.cfgID == 0);
		}

		IEnumerator CheckComplete()
		{
			_model.Play("walk");
			MapLoop(false);
			var end = true;
			if (autoState)
			{
				if (CheckItem(true))
					yield return _waitRest;
				else
				{
					end = false;
					SwitchExploreAutoPage(0);
				}
			}
			else
			{
				end = false;
				yield return null;
			}
			if (end) Fast(true);
			fightState = end;
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
				}
				SGame.UIUtils.OpenUI("fightequiptips", exploreData.cacheEquip);
			}
		}

		void CancelAuto()
		{
			SwitchExploreAutoPage(0);
		}



		#endregion

		#region Role

		void InitRole()
		{
			m_view.z = 300;
			_model = new UIModel(m_view.m_holder)
				.SetData(exploreData.explorer)
				.SetTRS(Vector3.zero, new Vector3(0, 110, -45), 200)
				.SetLoad(LoadModel);
		}

		IEnumerator LoadModel(object data)
		{

			var path = "Assets/BuildAsset/Prefabs/Explore/" + exploreData.explorer.cfg.Model;
#if UNITY_EDITOR
			if (!File.Exists(path + ".prefab"))
			{
				Debug.LogError($"探索模型资源不存在:{path},将使用临时资源");
				path = "Assets/BuildAsset/Prefabs/Explore/001";
			}
#endif
			return SpawnSystem.Instance.SpawnAndWait(path);

		}

		#endregion
	}
}
