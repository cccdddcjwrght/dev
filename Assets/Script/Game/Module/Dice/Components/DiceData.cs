using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;

namespace SGame
{
    [GenerateAuthoringComponent]
    public struct DiceData : IComponentData
    {
        public int     Value;           // 骰子数字

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

        /// <summary>
        /// 给出筛子确切的方位
        /// </summary>
        /// <param name="value">筛子正面朝上的数字</param>
        /// <param name="face">第几个面, 骰子有4个面</param>
        /// <returns></returns>
        public static quaternion GetQuation(int value, int face)
        {
            quaternion baseRotation = GetQuation(value);
            
            // y轴旋转
            quaternion rot_face = quaternion.RotateY(face * (math.PI / 2));
            
            // 计算旋转
            return math.mul(rot_face, baseRotation);
        }
    }
}