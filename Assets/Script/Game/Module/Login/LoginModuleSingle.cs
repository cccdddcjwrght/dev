using System.Collections;
using Fibers;
using libx;
using log4net;
using UnityEngine;
using Unity.Entities;


/// <summary>
/// 单机登录模块
/// </summary>
namespace SGame
{
	public class LoginModuleSingle : IModule
	{
		private const string script = "Assets/BuildAsset/VisualScript/Prefabs/Login.prefab";
		private static ILog log = LogManager.GetLogger("game.login");

		public LoginModuleSingle(GameWorld gameWorld)
		{
			m_gameWorld = gameWorld;
		}

		// 判断登录流程是否结束
		public bool IsFinished => m_fiber.IsTerminated;

		public void Enter()
		{
			m_fiber = new Fiber(RunScriptLogin());
		}

		IEnumerator RunScriptLogin()
		{
			GameObject go = null;
#if !SVR_RELEASE && !Auto_Login && !AUTO_LOGIN
			var asset = Assets.LoadAssetAsync(script, typeof(GameObject));
			yield return asset;
			if (!string.IsNullOrEmpty(asset.error))
			{
				log.Error("script load fail=" + asset.error);
				yield break;
			}

			go = GameObject.Instantiate(asset.asset as GameObject);
			var waitLogin = new WaitEvent<string>((int)GameEvent.ENTER_LOGIN);
			yield return waitLogin;
#if DATA_SYNC
			yield return DataSyncModule.GetDataFromServer(waitLogin.m_Value);
#endif			
#else
			EventManager.Instance.Trigger((int)GameEvent.ENTER_LOGIN, "aaa");
			yield return null;
#endif
			DataCenter.Instance.Initalize();
			while (!DataCenter.Instance.IsInitAll)
				yield return null;

			TomorrowGiftModule.Instance.Initalize();

			EventManager.Instance.Trigger((int)GameEvent.LOGIN_COMPLETE);
			if (go)
				GameObject.Destroy(go);
		}
		
		public void Shutdown()
		{

		}

		public void Update()
		{
			if (m_fiber != null)
			{
				m_fiber.Step();
			}
		}

		////////////////////// DATA ///////////////////
		private GameWorld m_gameWorld;
		private EntityManager EntityManager { get { return m_gameWorld.GetECSWorld().EntityManager; } }

		private Fiber m_fiber;
	}
}