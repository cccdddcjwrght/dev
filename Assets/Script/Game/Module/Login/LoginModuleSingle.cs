using System.Collections;
using System.Collections.Generic;
using Fibers;
using libx;
using log4net;
using UnityEngine;
using Unity.Entities;
using SGame.UI;
using Unity.VisualScripting;

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
#if !SVR_RELEASE
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
#else
			EventManager.Instance.Trigger((int)GameEvent.ENTER_LOGIN, "aaa");
#endif
			DataCenter.Instance.Initalize();
			while (!DataCenter.Instance.IsInitAll)
				yield return null;
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