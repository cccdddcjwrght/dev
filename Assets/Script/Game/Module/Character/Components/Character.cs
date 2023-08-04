using Unity.Entities;
using UnityEngine;
using System;

namespace SGame
{
    /// <summary>
    /// 角色对象, 控制角色可以从该对象中得到
    /// </summary>
    [GenerateAuthoringComponent]
    public class Character : IComponentData
    {
        public string                       characterName;                    // 角色名字
        
        [NonSerialized] public Entity       render;                           // 显示对象, 初始化后创建的
        
        public int                          characterId;                      // 角色ID

        public int                          pathIndex;                        // 目标PATH
    }
}