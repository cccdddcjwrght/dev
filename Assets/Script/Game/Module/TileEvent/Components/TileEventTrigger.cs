using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using System.Collections;

namespace SGame
{
    // 游戏格子进入事件
    [GenerateAuthoringComponent]
    public struct TileEventTrigger : IBufferElementData//, IComparable<TitleEventTrigger>
    {
        public enum State : uint
        {
            PASSOVER = 0,  // 路过
            FINISH   = 1, // 完成事件, 移动到目标点
        }

        //public Entity  entity;
        public int     titleId;
        public State   state;
    }
}