using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;

namespace GameTools.Paths
{
	/// <summary>
	/// 重新寻路，当下一个点不可移动
	/// </summary>
	[UpdateAfter(typeof(MapSyncSystem))]
	public partial class RepathSystem : SystemBase
	{
		private AStarSystem m_astarsys;
		private EntityQuery m_query;
		private EndSimulationEntityCommandBufferSystem m_command;

		protected override void OnCreate()
		{
			m_command = World.GetExistingSystemManaged<EndSimulationEntityCommandBufferSystem>();
			m_astarsys = World.GetOrCreateSystemManaged<AStarSystem>();
			m_query = GetEntityQuery(new EntityQueryDesc()
			{
				All = new ComponentType[] { typeof(Follow), typeof(PathPositions) },
				None = new ComponentType[] { typeof(FindPathParams) }
			});
		}

		protected override void OnUpdate()
		{
			if (AStar.map == null) return;

			var command = m_command.CreateCommandBuffer().AsParallelWriter();
			var chunkTypeFollow = GetComponentTypeHandle<Follow>();
			var chunkTypeTranslation = GetComponentTypeHandle<LocalTransform>();
			var chunkTypePathPositions = GetBufferTypeHandle<PathPositions>(true);
			var entityType = GetEntityTypeHandle();

			var job = new RepathJob()
			{
				parallelWriter = command,
				map = m_astarsys.GetMap(),
				mapInfo = AStar.map.GetMapInfo(),
				chunkTypeFollow = chunkTypeFollow,
				chunkTypeTranslation = chunkTypeTranslation,
				chunkTypePathPositions = chunkTypePathPositions,
				entityType = entityType
			};
			Dependency = job.ScheduleParallel(m_query, Dependency);
			m_command.AddJobHandleForProducer(Dependency);
		}

		struct RepathJob : IJobChunk
		{
			public EntityCommandBuffer.ParallelWriter parallelWriter;
			[ReadOnly]
			public AStarSystem.MapData map;
			[ReadOnly]
			public MapInfo mapInfo;

			[ReadOnly]
			public ComponentTypeHandle<Follow> chunkTypeFollow;
			[ReadOnly]
			public ComponentTypeHandle<LocalTransform> chunkTypeTranslation;
			[ReadOnly]
			public BufferTypeHandle<PathPositions> chunkTypePathPositions;
			[ReadOnly]
			public EntityTypeHandle entityType;

			public void Execute(in ArchetypeChunk chunk, int unfilteredChunkIndex, bool useEnabledMask, in v128 chunkEnabledMask)
			{
				var chunkEntities = chunk.GetNativeArray(entityType);
				var chunkFollow = chunk.GetNativeArray(chunkTypeFollow);
				var chunkTranslation = chunk.GetNativeArray(chunkTypeTranslation);
				var chunkPathPositions = chunk.GetBufferAccessor(chunkTypePathPositions);

				for (int i = 0; i < chunk.Count; i++)
				{
					Follow follow = chunkFollow[i];
					if (follow.Value <= 0)
						continue;

					var e = chunkEntities[i];
					var points = chunkPathPositions[i];
					var translation = chunkTranslation[i];
					var fv = points[follow.Value - 1];
					var p = map.GetData(fv.Value);
					var flag = p.cost != fv.cost && follow.Value > 1;

					if (!p.isWalkable || flag) {

						var tpos = points[0].Value;
						var spos = AStar.GetGridPos(translation.Position, mapInfo);
						parallelWriter.AddComponent<FindPathParams>(unfilteredChunkIndex, e, new FindPathParams
						{
							start_pos = spos,
							end_pos = tpos
						});

					}

				}

			}
		}

	}

}