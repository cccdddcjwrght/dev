using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace SGame
{
    // 游戏格子进入事件
    public struct TitleEventTrigger : IBufferElementData
    {
        public enum State : uint
        {
            PASS =  0, // 路过
            STADY = 1, // 进入
        }

        public State state;
        public int   round;
    }
}