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
            yield return new WaitEvent(GameEvent.ENTER_GAME);
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

        IEnumerator LoginServer()
        {
            yield break;
        }

        void SetupDefault()
        {
            //m_currentPlayerPos = m_userInfo.Pos;
            /*
            
            m_userData.SetNum((int)UserType.GOLD,             m_userInfo.Coin);
            m_userData.SetNum((int)UserType.DICE_NUM,         m_userInfo.Dice);
            m_userData.SetNum((int)UserType.DICE_MAXNUM,      m_userInfo.DiceMax);
            m_userData.SetNum((int)UserType.POS,              m_userInfo.Pos);

            // 创建恢复骰子对象
            var recover = EntityManager.CreateEntity(typeof(DiceRecover), typeof(TimeoutData));
            var dice_add_time = (float)GlobalDesginConfig.GetInt(DICE_ADD_TIME);
            EntityManager.SetComponentData(recover, new DiceRecover {
                duration   = dice_add_time,
                recoverNum = GlobalDesginConfig.GetInt(DICE_ADD_NUM),
            });
            EntityManager.SetComponentData(recover, new TimeoutData { Value = dice_add_time });

            // 创建事件
            int i = 0;
            foreach (var e in m_userInfo.StepList)
            {
                m_tileEventModule.AddEventGroup(CovertNetEventToRound(e));
            }

            UpdateDicePower(true);
            
            // 加载地图
            TileModule.Instance.LoadMap(m_userInfo.MapId, MapType.NORMAL);
            */
        }
        
        

        public IEnumerator RunLogin()
        {
            const float HotfixTime = 1.0f;  // 更新UI显示时间
            const float LoadingTime = 0.5f; // 加载UI更新时间

            // 1. 显示更新界面
            Entity hotfixUI = UIRequest.Create(EntityManager, UIUtils.GetUI("hotfix"));
            EntityManager.AddComponentData(hotfixUI, new UIParam() { Value = HotfixTime });

            // 2. 显示登录界面
            yield return new WaitEvent(GameEvent.HOTFIX_DONE);
            Entity loginUI = UIRequest.Create(EntityManager, UIUtils.GetUI("login"));
            yield return new WaitUIOpen(EntityManager, loginUI);
            UIUtils.CloseUI(hotfixUI);

            
            /*
            // 3. 等待登录事件登录
            yield return LoginServer();
            SetupDefault();

            // 登录成功后显示加载UI
            Entity loadingUI = UIRequest.Create(EntityManager, UIUtils.GetUI("loading"));
            EntityManager.AddComponentData(loadingUI, new UIParamFloat() { Value = LoadingTime });
            yield return new WaitUIOpen(EntityManager, loadingUI);
            UIUtils.CloseUI(EntityManager, loginUI);

            // 4. 完成后直接进入主界
            yield return new WaitEvent(EntityManager, GameEvent.ENTER_GAME);
            Entity mainUI = UIRequest.Create(EntityManager, UIUtils.GetUI("mainui"));
            UIUtils.CloseUI(EntityManager, loadingUI);
*/
            yield return null;
        }
        
        ////////////////////// DATA ///////////////////
        private GameWorld m_gameWorld;
        private EntityManager EntityManager { get { return m_gameWorld.GetECSWorld().EntityManager; } }

        private Fiber       m_fiber;

        private ItemGroup   m_userData;
    }
}