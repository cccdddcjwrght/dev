using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Jobs;
using Unity.Burst;
using Unity.Burst.Intrinsics;

//using UnityEngine;
// 自动寻路的跟随系统 (多线程加速版)

namespace GameTools.Paths
{
	[UpdateAfter(typeof(MapSyncSystem))]
	public partial class FollowSystem : SystemBase
	{
		EntityQuery m_Query;
		AStarSystem m_AStarSys;
		EndSimulationEntityCommandBufferSystem m_CommandBuffer;

		[BurstCompile]
		struct FollowJob : IJobChunk
		{
			public EntityCommandBuffer.ParallelWriter _commandBuffer;


			public float DeltaTime;
			public MapInfo mapInfo;

			public ComponentTypeHandle<Follow> chunkTypeFollow;
			public ComponentTypeHandle<LocalTransform> chunkTypeTranslation;

			[ReadOnly]
			public ComponentTypeHandle<Speed> chunkTypeSpeed;

			[ReadOnly]
			public BufferTypeHandle<PathPositions> chunkTypePathPositions;

			public void Execute(in ArchetypeChunk chunk, int unfilteredChunkIndex, bool useEnabledMask, in v128 chunkEnabledMask)
			{
				var chunkFollow = chunk.GetNativeArray(chunkTypeFollow);
				var chunkTranslation = chunk.GetNativeArray(chunkTypeTranslation);
				var chunkSpeed = chunk.GetNativeArray(chunkTypeSpeed);
				var chunkPathPositions = chunk.GetBufferAccessor(chunkTypePathPositions);

				for (int i = 0; i < chunk.Count; i++)
				{
					Follow pathIndex = chunkFollow[i];
					if (pathIndex.Value <= 0 )
						continue;

					Speed comSpeed = chunkSpeed[i];
					DynamicBuffer<PathPositions> comPositions = chunkPathPositions[i];
					LocalTransform translation = chunkTranslation[i];

					float deltaMovement = DeltaTime * comSpeed.Value;
					int fllowIndex = pathIndex.Value - 1;
					var map_pos = comPositions[fllowIndex].Value;

					float3 target_pos = AStar.GetPos(map_pos.x, map_pos.y, mapInfo);
					var currentPos = translation.Position;//transform.position;

					var offset = (target_pos - currentPos);
					var offsetLen = math.length(offset);
					if (offsetLen <= deltaMovement || offsetLen <= 0.001f)
					{
						// Next Node
						translation.Position = target_pos;
						pathIndex.Value--;
						chunkFollow[i] = pathIndex;
					}
					else
					{
						// 直接移动
						translation.Position = currentPos + math.normalize(offset) * deltaMovement;
					}
					chunkTranslation[i] = translation;

				}
			}
		}

		protected override void OnCreate()
		{
			var query = new EntityQueryDesc()
			{
				All = new ComponentType[] {
					typeof(Follow),
					typeof(LocalTransform),
					ComponentType.ReadOnly<Speed>(),
					ComponentType.ReadOnly<PathPositions>()
				},
				None = new ComponentType[] {
					typeof(FindPathParams)
				}
			};
			m_Query			= GetEntityQuery(query);
			m_AStarSys		= World.GetOrCreateSystemManaged<AStarSystem>();
			m_CommandBuffer = World.GetOrCreateSystemManaged<EndSimulationEntityCommandBufferSystem>();
		}

		protected override void OnUpdate()
		{
			if (AStar.map == null)
				return;
			
			var chunkTypeFollow = GetComponentTypeHandle<Follow>();
			var chunkTypeTranslation = GetComponentTypeHandle<LocalTransform>();
			var chunkTypeSpeed = GetComponentTypeHandle<Speed>(true);
			var chunkTypePathPositions = GetBufferTypeHandle<PathPositions>(true);
			var commandBuffer = m_CommandBuffer.CreateCommandBuffer().AsParallelWriter();

			FollowJob job = new FollowJob()
			{
				DeltaTime = World.Time.DeltaTime, //SystemAPI.Time.DeltaTime,// Time.DeltaTime,
				mapInfo = AStar.map.GetMapInfo(),
				chunkTypeFollow = chunkTypeFollow,
				chunkTypeTranslation = chunkTypeTranslation,
				chunkTypeSpeed = chunkTypeSpeed,
				chunkTypePathPositions = chunkTypePathPositions,
				_commandBuffer = commandBuffer,
			};

			Dependency = job.ScheduleParallel(m_Query, Dependency);
			m_CommandBuffer.AddJobHandleForProducer(Dependency);

		}
	}
}