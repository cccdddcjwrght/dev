using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using log4net;
using System;
using libx;
using Fibers;
using System.IO;
using SGame;
using Unity.VisualScripting;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

// 初始化完成状态
public struct GameInitFinish : IComponentData
{
}


[DefaultExecutionOrder(-1000)]
public class Game : SGame.MonoSingleton<Game>
{
	static public bool IsDebug = false;

	public static DebugOverlay debugOverlay => Game.Instance.m_DebugOverlay;
	public static Console console => Game.Instance.m_console;

	private DebugOverlay m_DebugOverlay;
	private Console m_console;
	private WaitForEndOfFrame wfeof;

	public static IEnumerator Main()
	{
		var asset = Assets.LoadAsset("Assets/BuildAsset/Prefabs/Game.prefab", typeof(GameObject));
		Instantiate(asset.asset);
		//asset.Release();

		log.Info("Game Start Running");
		yield return null;
	}

	// Start is called before the first frame update
	static ILog log = LogManager.GetLogger("GameSystem");

	private bool m_isInitalized = false;
	private Entity m_initalizeEntity = Entity.Null;
	List<GameLoopRequest> m_gameLoopRequest = new List<GameLoopRequest>();
	List<IGameLoop> m_gameLoops = new List<IGameLoop>();

	// 事件系统
	EventManager eventManager;

	public bool IsInitalize { get { return m_isInitalized; } }

	public string LogInitPath = "Assets/BuildAsset/Setting/log4net.xml";
	private string LogInitPath_ERROR = "Assets/BuildAsset/Log/log4net-error.xml.bytes";

	public string AudioMixerPath = "Assets/BuildAsset/Audio/mixer/Game.mixer";  // 声音Mixer资源
	public string ShaderCollectPath = "Assets/BuildAsset/Shaders/ShaderVariants.shadervariants";
	public bool enableGuide = true;

	public Action EVENT_INIT_FINISH;

	public Vector2Int DebugOverlaySize = new Vector2Int(1334, 750);

	Fiber m_initProcess;


#if UNITY_EDITOR

	[SerializeField]
	private DataCenter dataCenter;

#endif

	protected override void Awake()
	{
		base.Awake();

		wfeof = new WaitForEndOfFrame();
		m_DebugOverlay = new DebugOverlay();
		m_DebugOverlay.Init(DebugOverlaySize.x, DebugOverlaySize.y);

		m_console = new Console();
		m_console.Init();
		StartCoroutine(EndOfFrame());

		Application.targetFrameRate = 100;
		m_isInitalized = false;
		m_initProcess = new Fibers.Fiber(InitProcess());

		// 保证再编辑器模式下, 能首先初始化完毕
		m_initProcess.Step();

		//应用安卓退出键
		Input.backButtonLeavesApp = true;
		//不息屏
		Screen.sleepTimeout = SleepTimeout.NeverSleep;

#if !UNITY_EDITOR
#if GAME_GUIDE
				enableGuide = true;
#else
				enableGuide = false;
#endif
#endif

#if DEBUG
		IsDebug = true;
		Debug.Log("【注意】Debug model!!!");
#endif

	}

	IEnumerator InitSystem()
	{
		VSEventBridge.Instance.Init();

		// 版本设置
		if (GameData.gameVersion == null)
			GameData.gameVersion = IniUtils.LoadLocalVersion();

		// 资源加载初始化
		ManifestRequest assetRequest = libx.Assets.Initialize();
		yield return assetRequest;
		if (!string.IsNullOrEmpty(assetRequest.error))
		{
			GameDebug.LogError("Asset Bundle Load Fail=" + assetRequest.error);
			yield break;
		}

		// 日志系统初始化
#if CHECK || !SVR_RELEASE
		AssetRequest logReq = Assets.LoadAssetAsync(LogInitPath, typeof(TextAsset)); 
#else
		AssetRequest logReq = Assets.LoadAssetAsync(LogInitPath_ERROR, typeof(TextAsset));
#endif

		yield return logReq;
		if (!string.IsNullOrEmpty(logReq.error))
		{
			GameDebug.LogError("Asset Bundle Load Fail = " + logReq.error);
			yield break;
		}
		var logConfig = logReq.asset as TextAsset;
		using (MemoryStream ms = new MemoryStream(logConfig.bytes))
		{
			GameDebug.Log("logConfig bytes = " + logConfig.bytes.Length);
			log4net.Config.XmlConfigurator.Configure(ms);
		}

		//字体
		yield return FontManager.Instance.Initalize();

		//语言初始化
		yield return LanguageUtil.InitLanguage();
		LanagueSystem.Instance.Initalize(LanguageUtil.GetGameLanguage());
		// 声音初始化
		AssetRequest audioReq = Assets.LoadAssetAsync(AudioMixerPath, typeof(AudioMixer));
		yield return audioReq;
		if (!string.IsNullOrEmpty(audioReq.error))
		{
			GameDebug.LogError("Audio Mixer Load Fail = " + audioReq.error);
			yield break;
		}
		World.DefaultGameObjectInjectionWorld.GetOrCreateSystemManaged<AudioSystem>().Initalize(audioReq.asset as AudioMixer);

		yield return SDK.SDKProxy.Init();

		console.AddCommand("close", OnExitConsole, "close console");
		console.Write("Welcome Game Console...");
	}

	void OnExitConsole(string[] args)
	{
		console.SetOpen(false);
	}

	IEnumerator LoadScene()
	{
		/*
		Debug.Log("Start Load game scene!!!");
		log.Info("Start Load game scene!");
		var req = Assets.LoadSceneAsync("Assets/BuildAsset/Scenes/game.unity", true);
		yield return req;
		*/
		var loadHandle = SceneManager.LoadSceneAsync("game", LoadSceneMode.Additive);
		loadHandle.allowSceneActivation = true;
		yield return loadHandle;
		Debug.Log("game scene finish !!!");
		log.Info("game finish scene!");
	}

	// 初始化流程
	IEnumerator InitProcess()
	{
		GameDebug.Log("InitProcess");
		GlobalTime.Start();

		yield return null;
		// 事件系统
		eventManager = EventManager.Instance;

		yield return InitSystem();

		m_isInitalized = true;
		m_initalizeEntity = World.DefaultGameObjectInjectionWorld.EntityManager.CreateEntity(typeof(GameInitFinish));
		EVENT_INIT_FINISH?.Invoke();

		// 开启客户端代码
		RequestLoop(GameLoopType.Single, null);
		log.Info("Game System Startup...");
	}

	public void RequestLoop(GameLoopType t, string[] param)
	{
		m_gameLoopRequest.Add(new GameLoopRequest { gameType = t, param = param });
	}

	/// <summary>
	/// 时间计数基础模块
	/// </summary>
	void CreateGameLoop()
	{
		if (m_gameLoopRequest.Count > 0)
		{
			bool isSuccessed = false;
			foreach (var req in m_gameLoopRequest)
			{
				var loop = GameLoopFactory.Create(req.gameType);
				if (loop == null)
				{
					log.Error("Not Support GameType = " + req.gameType.ToString());
					break;
				}

				isSuccessed = loop.Init(req.param);
				if (isSuccessed == false)
				{
					log.Error("Create Game Loop Fail=" + req.gameType.ToString());
					break;
				}

				m_gameLoops.Add(loop);
				GameDebug.Assert(m_gameLoops.Count <= 1, "More than one gameloop!");
			}

			if (isSuccessed == false)
			{
				ShutdownGameLoops();
			}

			m_gameLoopRequest.Clear();
		}
	}

	// 游戏主循环
	void Update()
	{
		// 初始化流程
		if (m_initProcess != null && m_initProcess.Step() == false)
		{
			// 初始化结束
			m_initProcess = null;
		}

		if (m_isInitalized == false)
			return;

		// 创建gameloop
		CreateGameLoop();

		// 更新计数器时间
		GlobalTime.UpdateFrameTime();

		// 事件系统更新
		eventManager.Update();

		// 游戏逻辑更新
		foreach (var gameloop in m_gameLoops)
		{
			gameloop.Update();
		}

#if UNITY_EDITOR
		var d = DataCenter.Instance;
		if (d != null) dataCenter = d;

		if (Input.GetKeyUp(KeyCode.Space))
			ScreenCapture.CaptureScreenshot(System.DateTime.Now.Ticks + ".png");
#endif

		// 调试功能
		m_console.TickUpdate();
	}

	void LateUpdate()
	{
		m_console.TickLateUpdate();
		m_DebugOverlay.TickLateUpdate();
	}

	IEnumerator EndOfFrame()
	{
		while (true)
		{
			yield return wfeof;
			m_DebugOverlay.Render();
		}
	}

	void ShutdownGameLoops()
	{
		foreach (var gameLoop in m_gameLoops)
			gameLoop.Shutdown();
		m_gameLoops.Clear();

		m_DebugOverlay.Shutdown();
		m_console.Shutdown();
		m_DebugOverlay = null;
		m_console = null;
	}

	private void OnApplicationQuit()
	{
		ShutdownGameLoops();
		ConfigSystem.Cleanup();
	}

	private void OnApplicationPause(bool isPause)
	{
		EventManager.Instance.Trigger((int)GameEvent.APP_PAUSE, isPause);
	}

}
