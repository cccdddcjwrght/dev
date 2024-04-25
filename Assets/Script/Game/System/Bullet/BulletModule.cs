using System.Collections;
using System.Collections.Generic;
using GameConfigs;
using log4net;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;

namespace SGame
{
    public class BulletModule : Singleton<BulletModule>
    {
        private static ILog log = LogManager.GetLogger("game.bullet");
        
        /// <summary>
        /// 创建子弹
        /// </summary>
        /// <param name="characterID">发射子弹的角色</param>
        /// <param name="bulletID">子弹ID</param>
        /// <param name="targetPos">目标ID</param>
        /// <returns></returns>
        public Entity Create(int characterID, int bulletID, Vector3 targetPos)
        {
            // 数据校验
            var character = CharacterModule.Instance.FindCharacter(characterID);
            if (character == null)
            {
                log.Error("character not found=" + characterID);
                return Entity.Null;
            }
            
            if (!ConfigSystem.Instance.TryGet(bulletID, out BulletRowData bulletConfig))
            {
                log.Error("bullet not found=" + bulletID);
                return Entity.Null;
            }

            if (!ConfigSystem.Instance.TryGet(bulletConfig.BulletEffectId, out effectsRowData effectConfig))
            {
                log.Error("bullet effect config not found=" + bulletConfig.BulletEffectId);
                return Entity.Null;
            }

            var slot = character.GetSlot((SlotType)effectConfig.CharacterSlotType);
            if (slot == null)
            {
                log.Error("charcter slot not found=" + effectConfig.CharacterSlotType);
                return Entity.Null;
            }
            
            
            // 创建子弹特效
            var bulletEntity = EffectSystem.Instance.Spawn3d(bulletConfig.BulletEffectId, null, slot.position);
            
            // 设置初始旋转
            var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            entityManager.AddComponentData(bulletEntity, new MoveTarget()   { Value = targetPos });
            entityManager.AddComponentData(bulletEntity, new Speed()        { Value = bulletConfig.BulletSpeed });
            
            // 设置旋转
            var dir = targetPos - slot.position;
            Quaternion rot = Quaternion.LookRotation(dir, Vector3.up);
            entityManager.SetComponentData(bulletEntity, new Rotation(){Value = rot});
            entityManager.AddComponentData(bulletEntity, new BulletData() { bulletID = bulletID, explorerEffectID = bulletConfig.HitEffectId });
            return bulletEntity;
        }
    }
}