using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace SGame
{
    // 游戏格子进入事件
    public struct TitleEventState : IBufferElementData
    {
        public enum State : uint
        {
            ENTER = 0, // 进入
            STADY = 1, // 持续
            LEAVE = 2, // 离开
        }

        public State state;
    }
}