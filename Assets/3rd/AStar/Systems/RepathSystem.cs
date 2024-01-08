using System.Collections;
using System.Collections.Generic;
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
			m_command = World.GetExistingSystem<EndSimulationEntityCommandBufferSystem>();
			m_astarsys = World.GetOrCreateSystem<AStarSystem>();
			m_query = GetEntityQuery(new EntityQueryDesc()
			{
				All = new ComponentType[] { typeof(Follow) },
				None = new ComponentType[] { typeof(FindPathParams) }
			});
		}

		protected override void OnUpdate()
		{
			if (AStar.map == null) return;

			var command = m_command.CreateCommandBuffer().AsParallelWriter();
			var chunkTypeFollow = GetComponentTypeHandle<Follow>();
			var chunkTypeTranslation = GetComponentTypeHandle<Translation>();
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
			public ComponentTypeHandle<Translation> chunkTypeTranslation;
			[ReadOnly]
			public BufferTypeHandle<PathPositions> chunkTypePathPositions;
			[ReadOnly]
			public EntityTypeHandle entityType;

			public void Execute(ArchetypeChunk chunk, int chunkIndex, int firstEntityIndex)
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

					var map_pos = points[follow.Value - 1].Value;
					if (!map.GetData(map_pos).isWalkable) {

						var tpos = points[0].Value;
						var spos = AStar.GetGridPos(translation.Value, mapInfo);
						parallelWriter.AddComponent<FindPathParams>(chunkIndex, e, new FindPathParams
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