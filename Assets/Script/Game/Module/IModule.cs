using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame
{
    /// <summary>
    /// 游戏中模块
    /// </summary>
    public interface IModule
    {
        void Shutdown();

        void Update();
    }
}