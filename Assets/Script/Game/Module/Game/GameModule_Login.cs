using System.Collections;
using System.Collections.Generic;
using SGame.UI;
using UnityEngine;
using Unity.Entities;
namespace SGame
{
    // 用于运行登录逻辑
    public partial class GameModule
    {
        public IEnumerator RunLogin()
        {
            const float HotfixTime = 2.0f;  // 更新UI显示时间
            const float LoadingTime = 1.0f; // 加载UI更新时间
            
            // 1. 显示更新界面
            Entity hotfixUI = UIRequest.Create(EntityManager, UIUtils.GetUI("hotfix"));
            EntityManager.AddComponentData(hotfixUI, new UIParamFloat() {Value = HotfixTime});
            
            // 2. 显示登录界面
            yield return new WaitEvent(EntityManager, GameEvent.HOTFIX_DONE);
            Entity loginUI = UIRequest.Create(EntityManager, UIUtils.GetUI("login"));
            yield return new WaitUIOpen(EntityManager, loginUI);
            UIUtils.CloseUI(EntityManager, hotfixUI);

            
            // 3. 进入加载界面
            yield return new WaitEvent(EntityManager, GameEvent.ENTER_LOGIN);
            Entity loadingUI = UIRequest.Create(EntityManager, UIUtils.GetUI("loading"));
            EntityManager.AddComponentData(loadingUI, new UIParamFloat() {Value = LoadingTime});
            yield return new WaitUIOpen(EntityManager, loadingUI);
            UIUtils.CloseUI(EntityManager, loginUI);

            // 4. 完成后直接进入主界
            yield return new WaitEvent(EntityManager, GameEvent.ENTER_GAME);
            Entity mainUI = UIRequest.Create(EntityManager, UIUtils.GetUI("mainui"));
            UIUtils.CloseUI(EntityManager, loadingUI);

            yield return null;
        }
    }
}