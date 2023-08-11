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
            ENTER = 0,  // 进入
            STAY  = 1,  // 停留            
            LEAVE = 2,  // 离开事件
            FINISH = 3, // 完成事件, 移动到目标点
        }

        //public Entity  entity;
        public int     titleId;
        public State   state;
    }
}