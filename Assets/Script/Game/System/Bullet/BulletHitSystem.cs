using System.Collections;
using System.Collections.Generic;
using GameConfigs;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace SGame
{
    public partial class BulletHitSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            Entities.WithNone<MoveTarget>().ForEach((Entity e, in BulletData bullet, in Translation trans) =>
            {
                if (bullet.explorerEffectID != 0)
                {
                    // 爆炸特效
                    EffectSystem.Instance.Spawn3d(bullet.explorerEffectID, null, trans.Value);
                }
                
                // 关闭子弹特效
                EffectSystem.Instance.CloseEffect(e);
            }).WithoutBurst().Run();
        }
    }
}
