using Unity.Entities;
using libx;
using System.Collections.Generic;
using System.Collections;
using Fibers;
using SGame.UI;
using log4net;
using System;
using UnityEngine;

namespace SGame
{

	public class SceneSystemV2 : MonoSingleton<SceneSystemV2>
	{
		public class Param : IEnumerator
		{
			public bool MoveNext()
			{
				return false;
			}

			public void Reset()
			{
			}

			public object Current { get; set; }
		}

		// 当前场景加载引用
		private GSceneRequest m_current;

		// 加载中的场景
		private GSceneRequest m_loading;

		// 排队中的场景, 只保留一个
		private GSceneRequest m_waitLoading;

		// 叠加场景
		private List<GSceneRequest> m_addtived;

		// 逻辑协程
		private Fiber m_fiber;

		private Action<int> m_globalListerCall;

		private Func<string, IEnumerator> m_openUI;
		private Action<IEnumerator> m_closeUI;

		private const string EMPTY_SCENE = "";
		private static ILog log = LogManager.GetLogger("SceneSystem");

		public GSceneRequest Current
		{
			get
			{
				return m_current;
			}
		}


		public float lastLoadDurtion { get; protected set; }
		public string lastLoadError { get; protected set; }


		protected override void Awake()
		{
			base.Awake();
			m_waitLoading = null;
			m_fiber = new Fiber(FiberBucket.Manual);
			m_fiber.Start(RunLoadSceneLogic());
		}

		void PopupAllWaiting()
		{
			if (m_waitLoading != null)
			{
				m_waitLoading.Release();
				m_waitLoading = null;
			}
		}

		GSceneRequest GetLoadScene(string name)
		{
			// 加载中
			if (m_waitLoading != null && m_waitLoading.name == name)
			{
				m_waitLoading.Retain();
				return m_waitLoading;
			}

			// Loading 中
			if (m_loading != null && m_loading.name == name)
			{
				PopupAllWaiting();
				m_waitLoading.Retain();
				return m_loading;
			}

			// 再加载中
			if (m_current != null && m_current.name == name)
			{
				// 还有对象再loading中, 等下一次加载把
				if (m_loading != null)
					return null;

				PopupAllWaiting();
				m_current.Retain();
				return m_current;
			}

			return null;
		}

		public void SetUISys(Func<string, IEnumerator> open, Action<IEnumerator> close)
		{
			this.m_openUI = open;
			this.m_closeUI = close;
		}

		// 加载场景
		public GSceneRequest Load(string name)
		{
			if (!ConfigSystem.Instance.TryGet(name, out GameConfigs.SceneRowData config))
			{
				log.Error("Scene Config Not Found!");
				return null;
			}

			var ret = GetLoadScene(name);
			if (ret != null)
				return ret;


			PopupAllWaiting();
			m_waitLoading = SceneRequestFactory.Create(name);
			m_waitLoading.closeUI = m_closeUI;
			return m_waitLoading;
		}

		// 累加场景, 但不切换场景, 主要是处理资源
		public GSceneRequest LoadAddtive(string name)
		{
			var ret = SceneRequestFactory.Create(name);
			m_addtived.Add(ret);
			return ret;
		}

		public void AddListener(Action<int> call, bool removed = false, bool global = false)
		{

			if (global)
			{
				m_globalListerCall -= call;
				if (!removed)
					m_globalListerCall += call;
			}
			else
			{
				if (m_loading != null)
				{
					m_loading.onStateChange -= call;
					if (!removed)
						m_loading.onStateChange += call;
				}
				else if (m_waitLoading != null)
				{
					m_waitLoading.onStateChange -= call;
					if (!removed)
						m_waitLoading.onStateChange += call;
				}
			}
		}

		IEnumerator RunLoadingLogic(GSceneRequest loading, GameConfigs.SceneRowData config)
		{
			// 1. 加载预加载场景
			if (!string.IsNullOrEmpty(config.LoadingUI))
			{
				loading.state = GSceneRequest.STATE.LOADING_UI;
				var uiEntity = m_openUI?.Invoke(config.LoadingUI);
				EntityManager mgr = World.DefaultGameObjectInjectionWorld.EntityManager;
				loading.loadingUI = uiEntity;

				// 等待UI显示后, 再去加载空白场景                
				yield return loading.loadingUI;

				//关闭其他界面保留加载界面
				m_closeUI?.Invoke(new Param() { Current = "-" + config.LoadingUI });
			}

			// 2. 加载切换场景, 主要用于释放上一个场景资源
			if (!string.IsNullOrEmpty(config.Loading))
			{
				if (ConfigSystem.Instance.TryGet(config.Loading, out GameConfigs.SceneRowData config2))
				{
					loading.state = GSceneRequest.STATE.LOADING_SCENE;
					loading.loadingScene = Assets.LoadSceneAsync(config2.FullPath, false);

					// 默认是自动激活
					yield return loading.loadingScene;
					if (m_current != null)
					{
						// 释放上一个场景资源
						m_current.Unload();
						m_current = null;
					}
				}
			}
		}

		IEnumerator RunPreloadLogic(GSceneRequest loading)
		{
			loading.state = GSceneRequest.STATE.PRELOAD_ASSET;
			loading.preloadAssets = SceneRequestFactory.CreatePreloadRequest(loading.name);
			if (loading.preloadAssets != null)
			{
				while (loading.Update())
					yield return null;
			}
		}

		// 场景加载逻辑
		IEnumerator RunLoadSceneLogic()
		{

			while (true)//m_loadingQueue != null)
			{
				if (m_waitLoading == null)
				{
					yield return null;
					continue;
				}

				void OnChange(int step) => m_globalListerCall?.Invoke(step);


				var starttime = Time.realtimeSinceStartup;
				m_loading = m_waitLoading;
				m_waitLoading = null;
				GSceneRequest loading = m_loading;
				string currentName = m_current != null ? m_current.name : null;
				loading.onStateChange += OnChange;

				// 加载场景
				// 请求网络
				if (ConfigSystem.Instance.TryGet(loading.name, out GameConfigs.SceneRowData config) == false)
				{
					log.Error("Load Config Fail=" + loading.name);
					break;
				}

				// 服务器同步逻辑
				loading.state = GSceneRequest.STATE.REQUEST_SERVER;
				loading.networkSync = SceneRequestFactory.CreateNetwork(loading.name);
				if (loading.networkSync != null)
				{
					loading.networkSync.SendLogin();
					while (loading.networkSync.WaitForLoginRequest() == false)
						yield return null;

					if (loading.networkSync.IsAcceptLogin() == false)
					{
						log.Error("Login Fail");
						break;
					}
				}

				// 1. 切换场景与UI
				yield return RunLoadingLogic(loading, config);

				// 2. 预加载资源
				yield return RunPreloadLogic(loading);

				// 3. 加载场景
				loading.state = GSceneRequest.STATE.LOAD_SCENE;
				loading.sceneRequest = libx.Assets.LoadSceneAsync(config.FullPath, false);
				yield return loading.sceneRequest;
				loading.state = GSceneRequest.STATE.LOAD_SCENE_COMPLETED;

				if (loading.logic != null)
					yield return loading.logic;

				// 5. 等待服务器同步
				if (loading.networkSync != null)
				{
					loading.state = GSceneRequest.STATE.APPLY_SERVER;
					loading.networkSync.SendSceneFinish();
					while (loading.networkSync.WaitForConfirm())
						yield return null;
				}

				//初始化完成
				loading.state = GSceneRequest.STATE.INIT_FINISH;

				// 6. 清除加载UI
				if (loading.loadingUI != default)
				{
					m_closeUI?.Invoke(loading.loadingUI);
					loading.loadingUI = default;
				}

				// 7. 清除加载资源
				if (loading.loadingScene != null)
				{
					loading.loadingScene.Release();
					loading.loadingScene = null;
				}
				lastLoadDurtion = loading.duration = Time.realtimeSinceStartup - starttime;
				lastLoadError = loading.error;
				loading.state = GSceneRequest.STATE.LOAD_FINISH;
				loading.Update();

				// 错误处理
				if (loading.isDone && string.IsNullOrEmpty(loading.error))
				{
					// 正常结束
					if (m_current != null)
					{
						m_current.Unload();
						m_current = null;
					}

					m_current = loading;
				}
				else
				{
					// 错误结束
					loading.Unload();
				}

				m_loading = null;
			}

			yield return null;
		}


		protected void Update()
		{
			// 运行逻辑
			m_fiber.Step();
		}
	}
}
