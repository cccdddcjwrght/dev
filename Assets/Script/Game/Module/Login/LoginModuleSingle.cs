using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using SGame.UI;

/// <summary>
/// 单机登录模块
/// </summary>
namespace SGame
{
    public class LoginModuleSingle : IModule
    {
        public LoginModuleSingle(GameWorld gameWorld)
        {

        }

        public void Enter()
        {

        }

        public void Shutdown()
        {

        }

        public void Update()
        {

        }

        IEnumerator LoginServer()
        {
            yield break;
        }

        void SetupDefault()
        {
            
        }

        public IEnumerator RunLogin()
        {
            const float HotfixTime = 1.0f;  // 更新UI显示时间
            const float LoadingTime = 0.5f; // 加载UI更新时间

            // 1. 显示更新界面
            Entity hotfixUI = UIRequest.Create(EntityManager, UIUtils.GetUI("hotfix"));
            EntityManager.AddComponentData(hotfixUI, new UIParamFloat() { Value = HotfixTime });

            // 2. 显示登录界面
            yield return new WaitEvent(EntityManager, GameEvent.HOTFIX_DONE);
            Entity loginUI = UIRequest.Create(EntityManager, UIUtils.GetUI("login"));
            yield return new WaitUIOpen(EntityManager, loginUI);
            UIUtils.CloseUI(EntityManager, hotfixUI);

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

            yield return null;
        }
        
        ////////////////////// DATA ///////////////////
        private GameWorld m_gameWorld;
        private EntityManager EntityManager { get { return m_gameWorld.GetECSWorld().EntityManager; } }
    }
}