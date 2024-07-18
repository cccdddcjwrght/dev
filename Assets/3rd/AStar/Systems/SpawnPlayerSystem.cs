using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Unity.Jobs;
using Unity.Burst;
using Unity.Burst.Intrinsics;
using Unity.Collections;
using Unity.Core;

namespace GameTools.Paths.Test
{
	// 随机生成角色
	[DisableAutoCreation]
	public partial class SpawnPlayerSystem : SystemBase // JobComponentSystem//: ComponentSystem
	{
		int _incressID = 1;

		NativeArray<Unity.Mathematics.Random> _randoms;
		NativeQueue<int> _messageQueue;

		EntityQuery m_query;
		EndSimulationEntityCommandBufferSystem m_CommandBuffer;
		AStarSystem _astarSystem;

		protected override void OnCreate()
		{
			base.OnCreate();
			_randoms = new NativeArray<Unity.Mathematics.Random>(Unity.Jobs.LowLevel.Unsafe.JobsUtility.MaxJobThreadCount, Allocator.Persistent); //new Unity.Mathematics.Random(44);
			for (int i = 0; i < _randoms.Length; i++)
			{
				_randoms[i] = new Unity.Mathematics.Random((uint)(44 + i));
			}
			_messageQueue = new NativeQueue<int>(Allocator.Persistent);
			m_query = GetEntityQuery(new EntityQueryDesc()
			{
				All = new ComponentType[] { ComponentType.ReadOnly<Follow>(), ComponentType.ReadOnly<LocalTransform>() },
				None = new ComponentType[] { ComponentType.ReadOnly<FindPathParams>() }
			});
			m_CommandBuffer = this.World.GetOrCreateSystemManaged<EndSimulationEntityCommandBufferSystem>();
			_astarSystem = World.GetOrCreateSystemManaged<AStarSystem>();
		}

		protected override void OnDestroy()
		{
			_randoms.Dispose();
			_messageQueue.Dispose();
		}

		protected override void OnUpdate()
		{
			if (AStar.playerPrefab == null)
			{
				AStar.playerPrefab = Resources.Load<GameObject>("Player");
			}

			if (Input.GetKeyDown(KeyCode.Space))
			{
				for (int i = 0; i < 10; i++)
				{
					var go = GameObject.Instantiate(AStar.playerPrefab);
					go.name = "Player_" + (_incressID++).ToString();
					var initPos = Vector3.zero;
					go.transform.localPosition = initPos;
					go.transform.localScale = Vector3.one;
					var r = go.GetComponentInChildren<Renderer>();
					if (r)
						r.material.color = UnityEngine.Random.ColorHSV();
					go.hideFlags = HideFlags.None;
					go.SetActive(true);
				}
			}

			var commandBuffer = m_CommandBuffer.CreateCommandBuffer().AsParallelWriter();
			var entityType = GetEntityTypeHandle();
			var followType = GetComponentTypeHandle<Follow>(true);
			var translationType = GetComponentTypeHandle<LocalTransform>(true);
			var job = new SpawnJOB()
			{
				_commandBuffer = commandBuffer,
				_entityType = entityType,
				_followType = followType,
				_translationType = translationType,
				_myrandoms = _randoms,
				_mapData = AStar.map.GetMapInfo(),
				_map = _astarSystem.GetMap(),
				_speedType = GetComponentTypeHandle<Speed>(),
			};

			Dependency = job.ScheduleParallel(m_query, Dependency);
			m_CommandBuffer.AddJobHandleForProducer(Dependency);
		}


		[BurstCompile]
		struct SpawnJOB : IJobChunk
		{
			public EntityCommandBuffer.ParallelWriter _commandBuffer;

			[ReadOnly]
			public EntityTypeHandle _entityType;

			[ReadOnly]
			public ComponentTypeHandle<Follow> _followType;

			[ReadOnly]
			public ComponentTypeHandle<LocalTransform> _translationType;

			public ComponentTypeHandle<Speed> _speedType;

			[ReadOnly]
			public AStarSystem.MapData _map;

			[NativeDisableParallelForRestriction]
			public NativeArray<Unity.Mathematics.Random> _myrandoms;

			[ReadOnly]
			public MapInfo _mapData;

			int2 GetRandomValue(int2 maxValue, ref Unity.Mathematics.Random rnd)
			{
				var ret = new int2 { x = rnd.NextInt(maxValue.x), y = rnd.NextInt(maxValue.y) };
				return ret;
			}

			public void Execute(in ArchetypeChunk chunk, int chunkIndex, bool useEnabledMask, in v128 chunkEnabledMask)
			{
				var chunkEntities = chunk.GetNativeArray(_entityType);
				var chunkFollow = chunk.GetNativeArray(_followType);
				var chunkTranslation = chunk.GetNativeArray(_translationType);
				var chunkSpeed = chunk.GetNativeArray(_speedType);
				var rnd = _myrandoms[chunkIndex];

				for (int i = 0; i < chunk.Count; i++)
				{
					var fllow = chunkFollow[i];
					if (fllow.Value > 0)
					{
						continue;
					}

					var trans = chunkTranslation[i];
					var e = chunkEntities[i];
					var speed = chunkSpeed[i];

					// 添加寻路
					int2 startPos = AStar.GetGridPos(trans.Position, _mapData);// AStarSystem.GetGridPos(trans.Value, _startPos, _cellSize);
																			//int2 startPos = Global.map.GetGridPos(new Vector3(trans.Value.x, trans.Value.y, trans.Value.z));
					int2 endPos = GetRandomValue(_map._size, ref rnd); //new int2 { x = _random.NextInt(_map._size.x), y = _random.NextInt(_map._size.y) };
					int j = 0;
					for (;
							  (endPos.x == startPos.x && endPos.y == startPos.y) || _map.GetData(endPos).isWalkable == false;
							  endPos = GetRandomValue(_map._size, ref rnd))
					{
						if (j++ >= 999)
							throw new System.Exception("dead loop in the job!");
					}

					//speed.Value = rnd.NextFloat(2f, 10f);
					chunkSpeed[i] = speed;

					_commandBuffer.AddComponent<FindPathParams>(chunkIndex, e, new FindPathParams
					{
						start_pos = startPos,
						end_pos = endPos
					});
				}
				_myrandoms[chunkIndex] = rnd;
			}
		}

	}

}