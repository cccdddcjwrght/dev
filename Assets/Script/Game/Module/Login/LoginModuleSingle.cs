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
        private static     ILog log = LogManager.GetLogger("game.login");
        
        public LoginModuleSingle(GameWorld gameWorld)
        {
            m_gameWorld = gameWorld;
        }

        public void Enter()
        {
            m_fiber           = new Fiber(RunScriptLogin());
            m_userData        = PropertyManager.Instance.GetGroup(ItemType.USER);
        }

        IEnumerator RunScriptLogin()
        {
            var asset = Assets.LoadAssetAsync(script, typeof(GameObject));
            yield return asset;
            if (!string.IsNullOrEmpty(asset.error))
            {
                log.Error("script load fail=" + asset.error);
                yield break;
            }

            GameObject go = GameObject.Instantiate(asset.asset as GameObject);
            var     waitLogin = new WaitEvent(GameEvent.ENTER_GAME);
            yield return waitLogin;
            yield return null;
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

        private Fiber       m_fiber;

        private ItemGroup   m_userData;
    }
}