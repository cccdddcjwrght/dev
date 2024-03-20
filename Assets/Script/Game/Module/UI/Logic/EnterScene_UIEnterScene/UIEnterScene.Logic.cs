
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

			m_view.m_loader.onClick.Add(OnClick);
			_nextScene = (context.GetParam()?.Value as object[]).Val<int>(0);
			if (_nextScene > 0)
			{
				if (ConfigSystem.Instance.TryGet<RoomRowData>(_nextScene, out _))
				{
					PlayEffect().Start();
					return;
				}
			}
			else if (_nextScene < 0 && !string.IsNullOrEmpty(c_video_path))
			{
				ShowVideo().Start();
				return;
			}
			SGame.UIUtils.CloseUIByID(__id);
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
		}

		IEnumerator PlayEffect()
		{
			var wait = GlobalConfig.GetFloat("wait_scene_effect");
			if (wait <= 0) wait = 6.5f;
			var e = EffectSystem.Instance.AddEffect(7, m_view);
			yield return EffectSystem.Instance.WaitEffectLoaded(e);
			var go = EffectSystem.Instance.GetEffect(e);
			var com = go.GetComponent<EnterNextSceneComponent>().Set(_nextScene - 2).Play();
			yield return new WaitForSeconds(1f);
			yield return Dining.DiningRoomSystem.Instance.LoadRoom(_nextScene);
			com.continueFlag = true;
			while (com.director.time < wait)
				yield return null;
			EffectSystem.Instance.ReleaseEffect(e);
			SGame.UIUtils.CloseUIByID(__id);
		}

		void CompleteVideo()
		{
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
			if (e.inputEvent.y < 100)
				CompleteVideo();
		}

		partial void UnInitLogic(UIContext context)
		{
			EventManager.Instance.Trigger(((int)GameEvent.GAME_ENTER_SCENE_EFFECT_END));
			Debug.Log("<color=red>ad</color>asdasdasdasd");
		}
	}
}
