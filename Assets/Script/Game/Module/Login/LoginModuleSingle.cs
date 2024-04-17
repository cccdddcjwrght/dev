using System.Collections;
using System.Collections.Generic;
using Fibers;
using Http;
using libx;
using log4net;
using SGame.Http;
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
			yield return UserSync();
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
		
		/// <summary>
		/// 用户数据同步功能, 首次登录, 若服务器有数据, 就使用服务器数据
		/// </summary>
		/// <returns></returns>
		IEnumerator UserSync(string userName)
		{
			if (!DataCenter.Instance.isFirst)
			{
				// 本地有数据, 不处理
				yield break;
			}

			// 唯一化用户ID
			var playerID = userName.GetHashCode();
			DataCenter.Instance.accountData.playerID = playerID;
			
			// 请求服务器
			HttpPackage pkg = new HttpPackage();
			pkg.data = playerID.ToString();
			var result = HttpSystem.Instance.Post("getData", pkg.ToJson());
			yield return result;
			if (!string.IsNullOrEmpty(result.error))
			{
				log.Error("get user sync data fail=" + result.error);
				yield break;
			}
			
			pkg = JsonUtility.FromJson<HttpPackage>(result.data);
			if (string.IsNullOrEmpty(pkg.data))
			{
				// 新用户, 不用管
				yield break;
			}
			
			// 老用户, 将数据同步回来, 并重新加载
			PlayerPrefs.SetString(DataCenterExtension.__DKey, pkg.data);
			DataCenter.Instance.Load();
			log.Info("Recovert Player Success=" + userName + " lasttime=" + DataCenter.Instance.accountData.lasttime);
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