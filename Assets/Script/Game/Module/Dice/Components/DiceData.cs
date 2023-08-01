using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace SGame
{
    public struct DiceData : IComponentData
    {
        public int     Value;           // 骰子数字
        public Entity  m_instance;      // 骰子实例

        public static DiceData Create(int value)
        {
            return new DiceData() { Value = value, m_instance = Entity.Null };
        }

        public static Entity Create(EntityCommandBuffer command, int value)
        {
            var e = command.CreateEntity();
            command.AddComponent(e, Create(value));
            command.AddComponent<LocalToWorld>(e);
            command.AddComponent<Translation>(e);
            command.AddComponent<Rotation>(e);
            command.AddComponent<NonUniformScale>(e);
            var buff = command.AddBuffer<LinkedEntityGroup>(e);
            buff.Add(new LinkedEntityGroup() { Value = e });
            return e;
        }
    }
}