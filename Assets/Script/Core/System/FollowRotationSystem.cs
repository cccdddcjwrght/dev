using GameTools.Paths;
using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;
using Unity.Transforms;

namespace SGame
{
    public struct LastRotation : IComponentData
    {
        public quaternion Value;
    }
    
    public struct RotationSpeed : IComponentData
    {
        public float Value;
    }
    
    //[UpdateInGroup(typeof(GameLogicGroup))]
    public partial class FollowRotationSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var mapInfo = AStar.map.GetMapInfo();
            float deltaTime = Time.DeltaTime;
            Entities.WithNone<LastRotation>().ForEach(
                (   Entity                       e, 
                ref Rotation                     rot, 
                in  DynamicBuffer<PathPositions> paths, 
                in Follow follow,
                in Translation translation,
                in RotationSpeed speed) =>
            {
                if (follow.Value <= 0)
                {
                    return;
                }

                int targetIndex     = follow.Value - 1;
                var map_pos     = paths[targetIndex].Value;
                float3 target_pos   = AStar.GetPos(map_pos.x, map_pos.y, mapInfo);
                float3 dir = target_pos - translation.Value;
                dir.y = 0;
                if (math.lengthsq(dir) <= 0.01) // 距离太短, 不足以判定
                    return;

                dir = math.normalize(dir);
                quaternion target_rot = quaternion.LookRotation(dir, new float3(0, 1, 0));

                float cos_value = math.dot(rot.Value, target_rot);
                if (cos_value >= 0.99998f)
                {
                    rot.Value = target_rot;
                }
                else
                {
                    float t = math.clamp(deltaTime * speed.Value, 0, 1.0f);
                    rot.Value = math.nlerp(rot.Value, target_rot, t);;
                }
            }).WithBurst().ScheduleParallel();
            
            Entities.ForEach(
                (   Entity                       e, 
                    ref Rotation                     rot, 
                    in  DynamicBuffer<PathPositions> paths, 
                    in Follow follow,
                    in Translation translation,
                    in RotationSpeed speed,
                    in LastRotation lookAt) =>
                {
                    quaternion target_rot = quaternion.identity;
                    if (follow.Value <= 0)
                    {
                        target_rot = lookAt.Value;
                    }
                    else
                    {
                        int targetIndex     = follow.Value - 1;
                        var map_pos     = paths[targetIndex].Value;
                        float3 target_pos   = AStar.GetPos(map_pos.x, map_pos.y, mapInfo);
                        float3 dir = target_pos - translation.Value;
                        dir.y = 0;
                        if (math.lengthsq(dir) <= 0.01) // 距离太短, 不足以判定
                            return;
                        
                        dir = math.normalize(dir);
                        target_rot = quaternion.LookRotation(dir, new float3(0, 1, 0));
                    }

                    //rot.Value = math.slerp(rot.Value, target_rot, deltaTime * speed.Value);
                    float cos_value = math.dot(rot.Value, target_rot);
                    if (cos_value >= 0.99998f)
                    {
                        rot.Value = target_rot;
                    }
                    else
                    {
                        float t = math.clamp(deltaTime * speed.Value, 0, 1.0f);
                        rot.Value = math.nlerp(rot.Value, target_rot, t);;
                    }
                }).WithBurst().ScheduleParallel();
        }
    }
}