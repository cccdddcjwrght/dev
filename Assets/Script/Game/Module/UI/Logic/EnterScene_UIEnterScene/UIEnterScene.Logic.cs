
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.EnterScene;
	using GameConfigs;
	using System.IO;
	using UnityEngine.Video;
	using System.Collections;
	using System.Collections.Generic;

	public partial class UIEnterScene
	{

		private int _nextScene = 0;
		private int _maxLv;
		private int _curLv;

		private bool _isLastScene;
		private bool _canSwitch;
		private VideoPlayer _player;

		#region Scene

		private List<GameConfigs.RoomRowData> _sceneCfgs;

		#endregion

		#region path
		string c_video_path =
#if UNITY_EDITOR
				"exts/apploge/splash.mp4";
#else
	            Application.streamingAssetsPath + "/splash.mp4";
#endif
		#endregion

		#region Init

		partial void InitLogic(UIContext context)
		{
			var path = GameConfigs.GlobalConfig.GetStr("splash_video");
			if (!string.IsNullOrEmpty(path)) c_video_path = path;

			m_view.m_loader.onClick.Add(OnLoaderClick);
			m_view.m_btnGO.onClick.Add(OnClick);
			m_view.m_close.onClick.Add(OnClickClose);

			_nextScene = (context.GetParam()?.Value as object[]).Val<int>(0);
			m_view.m_show.selectedIndex = 0;

			if (_nextScene > 0)
			{
				m_view.m_show.selectedIndex = 1;
				(_maxLv, _curLv) = DataCenter.MachineUtil.GetRoomLvState();
				_canSwitch = DataCenter.MachineUtil.CheckAllWorktableIsMaxLv();
				_isLastScene = !ConfigSystem.Instance.TryGet<RoomRowData>(_nextScene, out _);

				SetLevelList();
				EventManager.Instance.Trigger(((int)GameEvent.GAME_ENTER_SCENE_EFFECT_END));
				return;
			}
			else if (_nextScene < 0 && !string.IsNullOrEmpty(c_video_path))
			{
				ShowVideo().Start();
				return;
			}
			SGame.UIUtils.CloseUIByID(__id);
		}

		partial void UnInitLogic(UIContext context)
		{

		}

		#endregion

		#region List

		void SetLevelList()
		{
			var c = _nextScene - 1;
			if (ConfigSystem.Instance.TryGet<RoomRowData>(c, out var cfg))
			{
				if (ConfigSystem.Instance.TryGet<RegionRowData>(cfg.RegionId, out var region))
				{
					_sceneCfgs = ConfigSystem.Instance.Finds<RoomRowData>((c) => c.RegionId == region.ID);
					_sceneCfgs.Reverse();

					//加个顶部空白区域
					var beginEmpty = UIPackage.CreateObject("EnterScene", "BeginEmpty");
					m_view.m_list.AddChild(beginEmpty);
					//----------------
					var headRegion = UIPackage.CreateObject("EnterScene", "HeadRegion");
					headRegion.asLabel.GetController("isMax").selectedIndex = _isLastScene ? 1 : 0;
					m_view.m_list.AddChild(headRegion);

					SGame.UIUtils.AddListItems(m_view.m_list, _sceneCfgs, OnSetRoomInfo);
					//加个底部空白区域
					var endEmpty = UIPackage.CreateObject("EnterScene", "EndEmpty");
					m_view.m_list.AddChild(endEmpty);
					//----------------
					m_view.m_list.ScrollToView(_sceneCfgs.FindIndex(v => v.ID == c) + 3);
					m_view.m_region.SetIcon(region.Icon);
					//m_view.m_title3.text = null;
					m_view.SetText(region.ID + "." + region.Name.Local());
					//if (string.IsNullOrEmpty(region.Icon) || _isLastScene)
					//	m_view.m_title3.SetTextByKey("ui_enterscene_2");

					m_view.m_btnGO.grayed = !_canSwitch || _isLastScene;
					//m_view.m_tips.visible = !_canSwitch;
					m_view.m_tips.SetTextByKey("ui_enterscene_tips_1", cfg.LevelMax); 
				}
			}

		}


		void OnSetRoomInfo(int index, object data, GObject gObject)
		{

			var cfg = (GameConfigs.RoomRowData)data;
			var view = gObject as UI_LevelItem;
			var room = DataCenter.RoomUtil.GetRoom(0);
			view.SetIcon(cfg.Icon);
			view.m_progress.SetText(cfg.SubId + "." + cfg.Name.Local(), false);
			view.m_left.selectedIndex = index % 2;
			view.m_progress.max = 1;
			view.m_progress.value = 0;

			view.m_chest.SetBaseItem(cfg.GetReward1Array());
			var cscene = _nextScene - 1;

			if (cfg.ID == cscene)
			{
				view.m_state.selectedIndex = _canSwitch ? 2 : 1;
				view.m_progress.max = _maxLv;
				view.m_progress.value = _canSwitch ? _maxLv : _curLv;
			}
			else if (cfg.ID < cscene)
			{
				view.m_state.selectedIndex = 2;
				view.m_progress.value = 1;
			}
			else if (cfg.ID == _nextScene && _canSwitch)
			{
				view.m_state.selectedIndex = 3;
				view.m_shake.Play(-1, 0, null);
			}
			else
			{
				view.m_state.selectedIndex = 0;
			}

		}

		#endregion

		#region Video
		IEnumerator ShowVideo()
		{
			var flag = false;
			EventManager.Instance.Trigger((int)GameEvent.GAME_ENTER_VIEW_STATR);
			var player = _player = new GameObject("_video").AddComponent<VideoPlayer>();
			player.waitForFirstFrame = true;
			player.aspectRatio = VideoAspectRatio.FitHorizontally;
			player.renderMode = VideoRenderMode.RenderTexture;
			player.targetTexture = RenderTexture.GetTemporary(
				   1024, 2048, 0, RenderTextureFormat.RGB565, RenderTextureReadWrite.Default
			);
			player.loopPointReached += (v) => CompleteVideo();
			player.prepareCompleted += (v) => flag = true;
			player.url = c_video_path;
			m_view.m_loader.texture = new NTexture(player.targetTexture);
			var w = 5f;
			yield return new WaitUntil(() => flag || (w -= Time.deltaTime) < 0);
			if (!flag) CompleteVideo();
			else
			{
				w = GlobalConfig.GetFloat("splash_time");
				if (w > 0)
				{
					yield return new WaitUntil(() => (w -= Time.deltaTime) < 0);
					StaticDefine.G_VIDEO_COMPLETE = true;
				}
			}

			CompleteVideo();
		}

		IEnumerator PlayEffect()
		{
			/*
			var wait = GlobalConfig.GetFloat("wait_scene_effect");
			if (wait <= 0) wait = 6f;
			var e = EffectSystem.Instance.AddEffect(7, m_view);
			yield return EffectSystem.Instance.WaitEffectLoaded(e);
			var go = EffectSystem.Instance.GetEffect(e);
			var com = go.GetComponent<EnterNextSceneComponent>().Set(_nextScene - 2).Play();
			yield return new WaitForSeconds(1f);
			yield return Dining.DiningRoomSystem.Instance.LoadRoom(_nextScene);
			com.continueFlag = true;
			if (wait > 0)
			{
				while (com.director.time < wait)
					yield return null;
				EventManager.Instance.Trigger(((int)GameEvent.GAME_ENTER_SCENE_EFFECT_END));
			}

			while (com.director.time < com.director.duration * 0.9f)
				yield return null;


			EffectSystem.Instance.ReleaseEffect(e);
			SGame.UIUtils.CloseUIByID(__id);
			*/
			while (true)
				yield return null;
		}

		void CompleteVideo()
		{
			EventManager.Instance.Trigger(((int)GameEvent.GAME_ENTER_SCENE_EFFECT_END));
			StaticDefine.G_VIDEO_COMPLETE = true;
			SGame.UIUtils.CloseUIByID(__id);
			if (_player != null)
			{
				RenderTexture.ReleaseTemporary(_player.targetTexture);
				_player.targetTexture = default;
				GameObject.Destroy(_player);
				EventManager.Instance.Trigger((int)GameEvent.GAME_ENTER_VIEW_END);
			}
		}

		void OnClick(EventContext e)
		{
			if (!DataCenter.MachineUtil.CheckAllWorktableIsMaxLv())
			{
				"@ui_worktable_goto_next_fail".Tips();
				return;
			}

			if (_isLastScene)
			{
				"@ui_enterscene_tips_2".Tips();
				return;
			}


			SGame.UIUtils.CloseUIByID(__id);
			Dining.DiningRoomSystem.Instance.LoadRoom(_nextScene);
		}
		#endregion

		#region Private
		void OnClickClose()
		{
			SGame.UIUtils.CloseUIByID(__id);
		}

		void OnLoaderClick(EventContext e)
		{
#if !SVR_RELEASE
			if (e.inputEvent.y < 100) CompleteVideo();
#endif
		}

		#endregion


	}
}
