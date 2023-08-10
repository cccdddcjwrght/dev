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
            const float HotfixTime = 3.0f;  // 更新UI显示时间
            const float LoadingTime = 2.0f; // 加载UI更新时间
            
            // 1. 显示更新界面
            Entity hotfixUI = UIRequest.Create(EntityManager, UIUtils.GetUI("hotfix"));
            EntityManager.AddComponentData(hotfixUI, new UIParamFloat() {Value = HotfixTime});
            yield return FiberHelper.Wait(HotfixTime - 1); //new WaitUIClose(EntityManager, hotfixUI);
            
            // 2. 显示登录界面
            Entity loginUI = UIRequest.Create(EntityManager, UIUtils.GetUI("login"));
            yield return new WaitUIClose(EntityManager, loginUI);
            
            // 3. 进入加载界面
            Entity loadingUI = UIRequest.Create(EntityManager, UIUtils.GetUI("loading"));
            EntityManager.AddComponentData(loadingUI, new UIParamFloat() {Value = LoadingTime});
            yield return FiberHelper.Wait(LoadingTime - 1);

            // 4. 完成后直接进入主界
            Entity mainUI = UIRequest.Create(EntityManager, UIUtils.GetUI("mainui"));
            yield return null;
        }
    }
}