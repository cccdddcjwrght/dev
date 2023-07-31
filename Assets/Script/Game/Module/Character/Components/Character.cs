using Unity.Entities;
using UnityEngine;
using System;

namespace SGame
{
    /// <summary>
    /// 角色对象, 控制角色可以从该对象中得到
    /// </summary>
    public class Character : MonoBehaviour, IConvertGameObjectToEntity
    {
        public string                       m_characterName;                    // 角色名字
        
        [NonSerialized] public Entity       m_render;                           // 显示对象, 初始化后创建的

        public Entity                       m_selfEntity;                       // Entity 自身

        public string                       m_playerAsset;                      // 资源地址

        public int                          m_characterId;

        public void Convert(
            Entity entity,
            EntityManager dstManager,
            GameObjectConversionSystem conversionSystem)
        {
            m_selfEntity = entity;
            dstManager.AddComponentObject(entity, this);
        }
    }
}