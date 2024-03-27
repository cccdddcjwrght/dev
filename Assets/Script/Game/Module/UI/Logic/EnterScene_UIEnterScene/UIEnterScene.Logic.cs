﻿
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

	public partial class UIEnterScene
	{

		private int _nextScene = 0;
		private VideoPlayer _player;

		#region path
		string c_video_path =
#if UNITY_EDITOR
				"exts/apploge/splash.mp4";
#else
	            Application.streamingAssetsPath + "/splash.mp4";
#endif
		#endregion

		partial void InitLogic(UIContext context)
		{
			var path = GameConfigs.GlobalConfig.GetStr("splash_video");
			if (!string.IsNullOrEmpty(path)) c_video_path = path;

			m_view.m_loader.onClick.Add(OnClickClose);
			m_view.m_btnGO.onClick.Add(OnClick);
			m_view.m_btnClose.onClick.Add(OnClickClose);

			_nextScene = (context.GetParam()?.Value as object[]).Val<int>(0);
			
			if (_nextScene > 0)
			{
				UpdateLevelState(_nextScene - 1);
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

		void UpdateLevelState(int level)
		{
			int canUnlock = 0;
			if (DataCenter.MachineUtil.CheckAllWorktableIsMaxLv())
				canUnlock = level + 1;
			
			var levels = new UI_LevelItem[] { m_view.m_level1, m_view.m_level2, m_view.m_level3, m_view.m_level4 };
			for (int i = 0; i < levels.Length; i++)
			{
				var l = i + 1;
				if (canUnlock == l)
				{
					levels[i].m_state.selectedIndex = 3;
					continue;
				}

				if (l < level)
				{
					levels[i].m_state.selectedIndex = 2;
				}
				else if (l == level)
				{
					levels[i].m_state.selectedIndex = 1;
				}
				else
				{
					levels[i].m_state.selectedIndex = 0;
				}
			}
		}

		IEnumerator ShowVideo()
		{
			var flag = false;
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
				w = GlobalConfig.GetInt("splash_time");
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
			}
		}

		void OnClick(EventContext e)
		{
			if (!DataCenter.MachineUtil.CheckAllWorktableIsMaxLv())
			{
				"@ui_worktable_goto_next_fail".Tips();
				return;
			}
			
			//if (e.inputEvent.y < 100)
				//CompleteVideo();
			SGame.UIUtils.CloseUIByID(__id);
			Dining.DiningRoomSystem.Instance.LoadRoom(_nextScene);
		}

		void OnClickClose()
		{
			SGame.UIUtils.CloseUIByID(__id);
		}

		partial void UnInitLogic(UIContext context)
		{
		}
	}
}
