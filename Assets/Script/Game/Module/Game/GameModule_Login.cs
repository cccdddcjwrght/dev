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
            // 1. 显示更新界面
            Entity hotfixUI = UIRequest.Create(EntityManager, UIUtils.GetUI("hotfix"));
            yield return new WaitUIClose(EntityManager, hotfixUI);
            
            // 2. 显示登录界面
            Entity loginUI = UIRequest.Create(EntityManager, UIUtils.GetUI("login"));
            yield return new WaitUIClose(EntityManager, loginUI);
            
            // 3. 进入加载界面
            Entity loadingUI = UIRequest.Create(EntityManager, UIUtils.GetUI("loading"));
            yield return new WaitUIClose(EntityManager, loadingUI);

            // 4. 完成后直接进入主界
            Entity mainUI = UIRequest.Create(EntityManager, UIUtils.GetUI("mainui"));
            yield return null;
        }
    }
}