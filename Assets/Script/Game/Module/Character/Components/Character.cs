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
        
        public int                          characterId = 0;                  // 角色ID

        public int                          titleId = 0;                    // 目标PATH

        public int                          round     = 0;                    // 第几轮的骰子
    }
}