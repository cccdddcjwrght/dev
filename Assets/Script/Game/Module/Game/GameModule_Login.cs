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
            
            
            // 2. 显示登录界面
            Entity loginUI = UIRequest.Create(EntityManager, 2);
            yield return new WaitUIClose(EntityManager, loginUI);
            
            // 3. 完成后直接进入主界
            Entity mainUI = UIRequest.Create(EntityManager, 3);
            yield return null;
        }
    }
}