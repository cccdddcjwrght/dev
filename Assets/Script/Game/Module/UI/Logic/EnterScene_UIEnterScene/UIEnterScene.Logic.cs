
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

		readonly string c_video_path = Path.Combine(Application.streamingAssetsPath, "splash.mp4");

		partial void InitLogic(UIContext context)
		{
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
			else if (_nextScene < 0)
			{
				ShowVideo().Start();
				return;
			}
			SGame.UIUtils.CloseUIByID(__id);
		}

		IEnumerator ShowVideo()
		{
			var player = _player = new GameObject("_video").AddComponent<VideoPlayer>();
			var flag = false;
			player.waitForFirstFrame = true;
			player.aspectRatio = VideoAspectRatio.FitHorizontally;
			player.renderMode = VideoRenderMode.RenderTexture;
			player.targetTexture = RenderTexture.GetTemporary(
				   1024,
				   2048,
				   0,
				   RenderTextureFormat.RGB565,
				   RenderTextureReadWrite.Default
			);
			player.loopPointReached += (v) => CompleteVideo();
			m_view.m_loader.texture = new NTexture(player.targetTexture);
			player.url = c_video_path;
			yield return null;
		}

		IEnumerator PlayEffect()
		{
			var e = EffectSystem.Instance.AddEffect(7, m_view);
			yield return EffectSystem.Instance.WaitEffectLoaded(e);
			var go = EffectSystem.Instance.GetEffect(e);
			var com = go.GetComponent<EnterNextSceneComponent>().Set(_nextScene - 2).Play();
			yield return new WaitForSeconds(1f);
			yield return Dining.DiningRoomSystem.Instance.LoadRoom(_nextScene);
			com.continueFlag = true;
			while (com.director.time < com.director.duration * 0.9f)
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
				_player.targetTexture = default ;
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

		}
	}
}
