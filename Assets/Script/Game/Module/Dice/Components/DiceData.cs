using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
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
        
        // 筛子点数对应的旋转值
        public static quaternion GetQuation(int value)
        {
            switch (value)
            {
                case 1:
                    return quaternion.LookRotation(new float3(0, 1, 0), new float3(0, 0, -1));
                case 2:
                    return quaternion.LookRotation(new float3(0, 0, 1), new float3(-1, 0, 0));
                case 3:
                    return quaternion.LookRotation(new float3(0, 0, 1), new float3(0, -1, 0));
                case 5:
                    return quaternion.LookRotation(new float3(0, 0, 1), new float3(1, 0, 0));
                case 6:
                    return quaternion.LookRotation(new float3(0, -1, 0), new float3(1, 0, 0));
            }
            
            return quaternion.identity;
        }

        public static quaternion GetQuation(int value, int face)
        {
            quaternion baseRotation = GetQuation(value);
            
            // y轴旋转
            quaternion rot_face = quaternion.RotateY(face * (math.PI / 2));
            
            // 计算旋转
            return math.mul(baseRotation, rot_face);
        }
    }
}