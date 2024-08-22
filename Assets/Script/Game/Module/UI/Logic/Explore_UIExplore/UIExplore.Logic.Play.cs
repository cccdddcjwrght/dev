
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


		void InitPlay()
		{
			onOpen += OnOpen_Play;
			onHide += OnHide_Play;
			InitRole();

			_checkExplore = new WaitUntil(CheckCmd);
			_waitAtk = new WaitForSeconds(0.3f);
			_waitDead = new WaitForSeconds(0.5f);
			_waitRest = new WaitForSeconds(2f);

		}

		void OnOpen_Play(UIContext context)
		{
			_exlogic?.Stop();
			_exlogic = Logic().Start();
			_model.RefreshModel();
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

		#region ExploreLogic

		IEnumerator Logic()
		{
			while (m_view.visible)
			{
				yield return WaitReq();
				yield return MoveMonster();
				yield return DoExplore();
				yield return CheckComplete();
			}
		}

		IEnumerator WaitReq()
		{
			yield return _checkExplore;
			PropertyManager.Instance.Update(1, ConstDefine.EXPLORE_ITEM, 1, true);
			DataLogic();
		}

		void SpawnMonster()
		{
			m_view.m_monster.xy = m_view.m_mholder.xy;
			m_view.m_monster.alpha = 1;
			m_view.m_monster.scale = Vector2.one;
			m_view.m_monster.visible = true;
		}

		IEnumerator MoveMonster()
		{
			SpawnMonster();
			var target = m_view.m_holder.xy;
			var flag = false;
			m_view.m_monster.TweenMoveX(target.x + 130, 5f).OnComplete(() => flag = true).SetEase(EaseType.Linear);
			yield return new WaitUntil(() => flag);
		}

		IEnumerator DoExplore()
		{
			MapLoop(true);
			_model.Play("attack");
			yield return _waitAtk;
			m_view.m_kill.Play();
			yield return _waitDead;
			MapLoop(false);
		}

		IEnumerator CheckComplete()
		{
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

		void DataLogic() { }

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
