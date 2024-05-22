using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace SGame
{
    [UpdateBefore(typeof(EffectSystem))]
    public class BulletHitSystem : ComponentSystem
    {
        struct Data
        {
            public Entity e;
            public BulletData bullet;
            public Translation trans;
        }
        private List<Data> m_Datas = new List<Data>();
        protected override void OnUpdate()
        {
            m_Datas.Clear();
            // 查询数据
            Entities.WithNone<MoveTarget>().ForEach((Entity e, ref BulletData bullet, ref Translation trans) =>
            {
                m_Datas.Add(new Data()
                {
                    e = e,
                    bullet = bullet,
                    trans = trans
                });
            });

            // 处理数据
            foreach (var item in m_Datas)
            {
                if (item.bullet.explorerEffectID != 0)
                {
                    // 爆炸特效
                    EffectSystem.Instance.Spawn3d(item.bullet.explorerEffectID, null, (Vector3)item.trans.Value);
                }
                
                // 关闭子弹特效
                EffectSystem.Instance.CloseEffect(item.e);
            }
        }
    }
}
