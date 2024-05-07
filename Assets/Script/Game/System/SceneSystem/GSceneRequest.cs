
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using libx;
using SGame.UI;
using System;

namespace SGame
{
	// 网络加载
	public interface INetworkSceneLoader
	{
		// 开始登录
		public void SendLogin();

		// 等待登录请求
		public bool WaitForLoginRequest();

		// 等待场景完成
		public bool IsAcceptLogin();

		// 发送场景切换完成
		public void SendSceneFinish();

		// 等待服务器确认场景切换完成
		public bool WaitForConfirm();

		// 关闭
		public void Close();
	}


	public class GSceneRequest : GAssetRequest
	{

		public enum STATE
		{
			// 等待服务器同意切场景
			REQUEST_SERVER = 0,

			// 等待加载UI
			LOADING_UI = 1,

			// 等待加载场景
			LOADING_SCENE = 2,

			// 预加载资源
			PRELOAD_ASSET = 3,

			// 加载实际场景
			LOAD_SCENE = 4,

			// 加载实际场景完成
			LOAD_SCENE_COMPLETED = 5,

			// 告诉服务器加载完成
			APPLY_SERVER = 5,

			//逻辑初始化或同步完成
			INIT_FINISH = 6,

			// 加载完成
			LOAD_FINISH = 7,

			// 销毁
			UNLOAD = 8,
		}

		private STATE _state;

		// 加载状态
		public STATE state
		{
			get { return _state; }
			internal set
			{
				_state = value;
				onStateChange?.Invoke((int)_state);
			}
		}

		public Action<int> onStateChange;

		public Action<IEnumerator> closeUI;

		// 网络同步
		internal INetworkSceneLoader networkSync;

		// 下一个场景
		internal AssetRequest sceneRequest;

		// 预加载资源
		internal GAssetRequest preloadAssets;

		// 加载中的UI
		internal IEnumerator loadingUI;

		// 用于转场的空场景
		internal AssetRequest loadingScene;

		/// <summary>
		/// 场景逻辑
		/// </summary>
		internal IEnumerator logic;
		internal Action unloadLogic;

		public override void Unload()
		{
			if (networkSync != null)
			{
				networkSync.Close();
				networkSync = null;
			}

			if (sceneRequest != null)
			{
				sceneRequest.Release();
				sceneRequest = null;
			}

			if (preloadAssets != null)
			{
				preloadAssets.Unload();
				preloadAssets = null;
			}

			if (loadingUI != default)
			{
				closeUI?.Invoke(loadingUI);
				loadingUI = default;
			}

			if (loadingScene != null)
			{
				loadingScene.Release();
				loadingScene = null;
			}

			if (logic != null) logic = null;
			unloadLogic?.Invoke();
			unloadLogic = null;

			onStateChange = null;
			state = STATE.UNLOAD;
		}

		public float duration;

		public override bool isDone
		{
			get
			{
				return state == STATE.LOAD_FINISH || state == STATE.UNLOAD;
			}
		}


		// 加载进度
		public override float progress
		{
			get
			{
				if (state < STATE.PRELOAD_ASSET)
					return 0;

				switch (state)
				{
					case STATE.PRELOAD_ASSET:
						return preloadAssets != null ? preloadAssets.progress * 0.5f : 0.5f;
					case STATE.LOAD_SCENE:
						return 0.5f + sceneRequest.progress * 0.5f;
					default:
						return 1.0f;
				}

				return 1.0f;
			}
		}
	}
}
