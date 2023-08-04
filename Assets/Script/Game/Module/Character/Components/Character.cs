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
        public string                       m_characterName;                    // 角色名字
        
        [NonSerialized] public Entity       m_render;                           // 显示对象, 初始化后创建的

        public string                       m_playerAsset;                      // 资源地址

        public int                          m_characterId;                      // 角色ID

        public int                          m_pathIndex;                        // 目标PATH
    }
}