using GameTools.Paths;
using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;
using Unity.Transforms;

namespace SGame
{
    // 使用 PathPositions 的版本
    //[UpdateInGroup(typeof(GameLogicGroup))]
    public partial class FollowFloatRotationSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            if (AStar.map == null)
                return;
            
            var mapInfo = AStar.map.GetMapInfo();
            float deltaTime = World.Time.DeltaTime;
            Entities.WithNone<LastRotation>().ForEach(
                (   Entity                       e, 
                ref LocalTransform                translation, 
                in  DynamicBuffer<FPathPositions> paths, 
                in Follow follow,
                in RotationSpeed speed) =>
            {
                if (follow.Value <= 0)
                {
                    return;
                }

                int targetIndex     = follow.Value - 1;
                var map_pos     = paths[targetIndex].Value;
                float3 target_pos = map_pos;//AStar.GetPos(map_pos.x, map_pos.y, mapInfo);
                float3 dir = target_pos - translation.Position;
                dir.y = 0;
                if (math.lengthsq(dir) <= 0.01) // 距离太短, 不足以判定
                    return;

                dir = math.normalize(dir);
                quaternion target_rot = quaternion.LookRotation(dir, new float3(0, 1, 0));

                float cos_value = math.dot(translation.Rotation, target_rot);
                if (cos_value >= 0.99998f)
                {
                    translation.Rotation = target_rot;
                }
                else
                {
                    float t = math.clamp(deltaTime * speed.Value, 0, 1.0f);
                    translation.Rotation = math.nlerp(translation.Rotation, target_rot, t);;
                }
            }).WithBurst().ScheduleParallel();
            
            Entities.ForEach(
                (   Entity                       e, 
                    ref LocalTransform                translation, 
                    in  DynamicBuffer<FPathPositions> paths, 
                    in Follow follow,
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
                        var map_pos    = paths[targetIndex].Value;
                        float3 target_pos   = map_pos;
                        float3 dir = target_pos - translation.Position;
                        dir.y = 0;
                        if (math.lengthsq(dir) <= 0.01) // 距离太短, 不足以判定
                            return;
                        
                        dir = math.normalize(dir);
                        target_rot = quaternion.LookRotation(dir, new float3(0, 1, 0));
                    }

                    float cos_value = math.dot(translation.Rotation, target_rot);
                    if (cos_value >= 0.99998f)
                    {
                        translation.Rotation = target_rot;
                    }
                    else
                    {
                        float t = math.clamp(deltaTime * speed.Value, 0, 1.0f);
                        translation.Rotation = math.nlerp(translation.Rotation, target_rot, t);;
                    }
                }).WithBurst().ScheduleParallel();
        }
    }
}