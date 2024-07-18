using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Jobs;
using Unity.Burst;
using Unity.Burst.Intrinsics;

//using UnityEngine;
// 自动寻路的跟随系统 (多线程加速版, 使用的是浮点位置)
namespace SGame
{
	public struct FPathPositions : IBufferElementData
	{
		public float3 Value;
	}
	
	public partial class FollowFloatSystem : SystemBase
	{
		EntityQuery m_Query;
		EndSimulationEntityCommandBufferSystem m_CommandBuffer;

		[BurstCompile]
		struct FollowJob : IJobChunk
		{
			public EntityCommandBuffer.ParallelWriter _commandBuffer;


			public float DeltaTime;

			public ComponentTypeHandle<Follow> chunkTypeFollow;
			public ComponentTypeHandle<LocalTransform> chunkTypeTranslation;

			[ReadOnly]
			public ComponentTypeHandle<Speed> chunkTypeSpeed;

			[ReadOnly]
			public BufferTypeHandle<FPathPositions> chunkTypePathPositions;

			public void Execute(in ArchetypeChunk chunk, int chunkIndex, bool useEnabledMask, in v128 chunkEnabledMask)
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
					DynamicBuffer<FPathPositions> comPositions = chunkPathPositions[i];
					LocalTransform translation = chunkTranslation[i];

					float deltaMovement = DeltaTime * comSpeed.Value;
					int fllowIndex = pathIndex.Value - 1;
					var map_pos = comPositions[fllowIndex].Value;

					float3 target_pos = map_pos;        
					var currentPos = translation.Position;

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
					ComponentType.ReadOnly<FPathPositions>()
				},
				None = new ComponentType[] {
					typeof(FindPathParams)
				}
			};
			m_Query = GetEntityQuery(query);
			m_CommandBuffer = this.World.GetOrCreateSystemManaged<EndSimulationEntityCommandBufferSystem>();
		}

		protected override void OnUpdate()
		{
			var chunkTypeFollow = GetComponentTypeHandle<Follow>();
			var chunkTypeTranslation = GetComponentTypeHandle<LocalTransform>();
			var chunkTypeSpeed = GetComponentTypeHandle<Speed>(true);
			var chunkTypePathPositions = GetBufferTypeHandle<FPathPositions>(true);
			var commandBuffer = m_CommandBuffer.CreateCommandBuffer().AsParallelWriter();

			FollowJob job = new FollowJob()
			{
				DeltaTime = World.Time.DeltaTime,
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