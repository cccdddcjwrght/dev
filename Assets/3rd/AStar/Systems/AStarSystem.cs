using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Jobs;
using Unity.Burst;
using Unity.Burst.Intrinsics;

namespace GameTools.Paths
{
	// 使用AStar 算法讲结果计算出来并保存
	[UpdateInGroup(typeof(SimulationSystemGroup))]
	public partial class AStarSystem : SystemBase //: ComponentSystem
	{
		public const int CST_CROSS_VALUE = 15;    // 交叉行走的代价
		public const int CST_STRAIGHT_VALUE = 10; // 笔直行走的代价
		public const int CST_CROSSHOLD_OFFSET_VALUE = 10; // 笔直行走的代价


		// 节点
		#region Class
		[GenerateTestsForBurstCompatibility]
		public struct Node
		{
			public int2 pos;
			public int index; // gird index
			public int parentIndex;

			public bool isWalkable; // 是否可以移动
			public bool isClose;    // 是否已经在close中
			public bool inOpen;

			// 估值数据
			public int gValue; // 从开始节点到此节点值
			public int hValue; // 从该
			public int fValue;

			public int cost;

			public void CalcFValue()
			{
				fValue = gValue + hValue;
			}
		}

		[GenerateTestsForBurstCompatibility]
		public struct MapData
		{
			public NativeArray<Node> _datas;    // 地图查找数据
			public int2 _size;     // 地图逻辑长款
			public float _cellSize; // 地图size
			public float3 _startPos; // 地图起始
			public int _version;  // 地图版本

			public bool IsCreated { get { return _datas.IsCreated; } }

			public void Dispone()
			{
				if (IsCreated)
				{
					_datas.Dispose();
				}
			}

			public Node GetData(int2 pos)
			{
				var index = GetIndex(pos.x, pos.y);
				if (index >= 0)
				{
					return _datas[index];
				}

				return default(Node);
			}

			public Node GetData(int x, int y)
			{
				var index = GetIndex(x, y);
				if (index >= 0)
				{
					return _datas[index];
				}

				return default(Node);
			}

			public Node GetData(int index)
			{
				return _datas[index];
			}

			public int GetIndex(int x, int y)
			{
				if (x < 0 || y < 0 || x >= _size.x || y >= _size.y)
					return -1;

				return x + y * _size.x;
			}
		}
		#endregion

		#region Member

		private MapData _mapData;

		public float mapCellSize { get { return _mapData._cellSize; } }
		public float3 mapStartPos { get { return _mapData._startPos; } }
		public int2 mapSize { get { return _mapData._size; } }

		#endregion

		#region Static

		// 估算计算两个节点之间的代价
		public static int CalcDistanceCost(int2 startPosition, int2 endPosition)
		{
			int distanceX = math.abs(startPosition.x - endPosition.x);
			int distanceY = math.abs(startPosition.y - endPosition.y);
			int straightDistance = math.abs(distanceX - distanceY);
			int value = straightDistance * CST_STRAIGHT_VALUE + math.min(distanceX, distanceY) * CST_CROSS_VALUE;
			return value;
		}

		// 通过index 获得位置
		public static int2 GetPosFromIndex(int index, int width)
		{
			int2 ret;
			ret.x = index % width;
			ret.y = index / width;
			return ret;
		}

		// 通过位置获得index
		public static int GetIndexFromPos(int2 pos, int width)
		{
			return pos.x + pos.y * width;
		}

		public static int2 GetGridPos(float3 worldPos, float3 starPos, float cellSize)
		{
			worldPos -= starPos;
			float halfSize = cellSize / 2f;
			worldPos += new float3(halfSize, halfSize, 0);

			int2 ret = int2.zero;
			ret.x = (int)(worldPos.x / cellSize);
			ret.y = (int)(worldPos.y / cellSize);
			return ret;
		}

		public static void ResetMap(NativeArray<Node> nodes, int width)
		{
			int arraySize = nodes.Length;
			for (int i = 0; i < arraySize; i++)
			{
				Node val = new Node();
				val.index = i;
				val.isClose = false;
				val.isWalkable = true;
				val.fValue = 0;
				val.gValue = int.MaxValue;
				val.hValue = 0;
				val.parentIndex = -1;
				nodes[i] = val;
			}
		}

		public static NativeArray<Node> CreateMap(int2 mapSize, Allocator alloc = Allocator.Temp)
		{
			int arraySize = mapSize.x * mapSize.y;
			var ret = new NativeArray<Node>(arraySize, alloc);
			ResetMap(ret, mapSize.x);
			return ret;
		}

		static int GetNearestNodeIndex(NativeList<int> openList, NativeArray<Node> maps)
		{
			int index = 0;
			int fValue = maps[openList[0]].fValue;
			var len = openList.Length;
			for (int i = 1; i < len; i++)
			{
				var curFValue = maps[openList[i]].fValue;
				if (curFValue < fValue)
				{
					fValue = curFValue;
					index = i;
				}
			}

			return index;
		}


		// 打开附近节点
		static void OpenNearMap(int openIndex, int2 endPos, NativeList<int> openList, NativeArray<Node> maps, int2 mapSize)
		{
			Node node = maps[openIndex];
			int2 currentPos = GetPosFromIndex(node.index, mapSize.x);// node.pos;

			for (int y = -1; y <= 1; y++)
			{
				for (int x = -1; x <= 1; x++)
				{
					if (x == 0 && y == 0)
						continue;

					// 不在地图中，下一个
					int2 pos = new int2(x + currentPos.x, y + currentPos.y);
					if (!InMap(pos, mapSize))
						continue;

					// 获得index
					int nextNode = GetIndexFromPos(pos, mapSize.x);

					Node n = maps[nextNode];
					if (n.isClose == true || n.isWalkable == false) // 不可移动，或者是在close列表中就直接跳过
						continue;

					// 斜对角方向判定
					if (x != 0 && y != 0)
					{
						// 斜对角 y 轴不能移动
						if (!IsWalkAble(new int2(currentPos.x, pos.y), mapSize, maps))
							continue;

						// 斜对角 x 轴不能移动
						if (!IsWalkAble(new int2(pos.x, currentPos.y), mapSize, maps))
							continue;
					}

					int g = CalcDistanceCost(currentPos, pos) + node.gValue + n.cost;
					if (g < n.gValue) // 没有比另一个路径更哟（一开始这个值就是Max）
					{
						if (n.hValue == 0)
						{
							// 第一次open
							n.hValue = CalcDistanceCost(pos, endPos);
						}
						/*else
						{
							Debug.LogError("One Node Twice Open!!!");
						}
						*/
						var flag = n.inOpen;
						n.gValue = g;
						n.fValue = n.gValue + n.hValue;
						n.parentIndex = openIndex;
						n.inOpen = true;
						maps[nextNode] = n;
						if (!flag)
							openList.Add(nextNode);
					}

				}
			}
		}

		static bool InMap(int2 pos, int2 mapSize)
		{
			return pos.x >= 0 && pos.y >= 0 && pos.x < mapSize.x && pos.y < mapSize.y;
		}

		/// <summary>
		/// 判断某个点是否能移动
		/// </summary>
		/// <param name="pos"></param>
		/// <param name="mapSize"></param>
		/// <param name="maps"></param>
		/// <returns></returns>
		static bool IsWalkAble(int2 pos, int2 mapSize, NativeArray<Node> maps)
		{
			if (!InMap(pos, mapSize)) return false;
			var index = GetIndexFromPos(pos, mapSize.x);
			var node = maps[index];
			return node.isWalkable;
		}

		public static bool FindPath(int2 startPos, int2 endPos, NativeArray<Node> maps, int2 mapSize, DynamicBuffer<PathPositions> paths)//NativeList<int> paths)
		{

			int width = mapSize.x;

			// openlist
			NativeList<int> openList = new NativeList<int>(Allocator.Temp);
			paths.Clear();

			int currentIndex = GetIndexFromPos(startPos, width);
			int endIndex = GetIndexFromPos(endPos, width);

			// 添加首节点
			Node firstNode = maps[currentIndex];
			firstNode.gValue = 0;
			firstNode.hValue = CalcDistanceCost(startPos, endPos);
			firstNode.fValue = firstNode.gValue + firstNode.hValue;
			maps[currentIndex] = firstNode;
			openList.Add(currentIndex);

			// 搜索路径
			while (openList.Length > 0)
			{
				int openIndex = GetNearestNodeIndex(openList, maps);
				currentIndex = openList[openIndex];
				if (currentIndex == endIndex)
				{
					// find it!
					break;
				}

				Node currentNode = maps[currentIndex];
				currentNode.isClose = true;
				openList.RemoveAtSwapBack(openIndex);
				// 打开周围的节点
				OpenNearMap(currentIndex, endPos, openList, maps, mapSize);
			}
			openList.Dispose();
			if (maps[endIndex].parentIndex == -1)
			{
				// not found!
				return false;
			}

			// 找到了， 将其放到队列中去
			//for (int startIndex = endIndex; startIndex >= 0; startIndex = maps[startIndex].parentIndex)
			for (int startIndex = endIndex; startIndex >= 0 && maps[startIndex].parentIndex >= 0; startIndex = maps[startIndex].parentIndex)
			{
				int2 pos = GetPosFromIndex(startIndex, width);
				paths.Add(new PathPositions { Value = pos, cost = maps[startIndex].cost });
				//paths.Add(startIndex);
			}

			return true;
		}


		#endregion

		#region Update Method
		public MapData GetMap()
		{
			return _mapData;
		}

		public void UpdateMap(MapData data)
		{
			_mapData = data;
		}

		public bool IsMapCreated()
		{
			return _mapData.IsCreated;
		}

		#endregion


		#region ecs override

		EntityQuery m_Group;
		EndSimulationEntityCommandBufferSystem m_EntityCommandBufferSystem;

		protected override void OnCreate()
		{
			// Cached access to a set of ComponentData based on a specific query
			m_Group = GetEntityQuery(typeof(FindPathParams));//typeof(Rotation), ComponentType.ReadOnly<RotationSpeed_IJobChunk>());
			m_EntityCommandBufferSystem = World.GetOrCreateSystemManaged<EndSimulationEntityCommandBufferSystem>();
		}

		protected override void OnDestroy()
		{
			_mapData.Dispone();
		}

		protected override void OnUpdate()
		{
			if (_mapData.IsCreated == false)
				return;

			ComponentTypeHandle<FindPathParams> FindParamType = GetComponentTypeHandle<FindPathParams>(); // 查找类型
			ComponentTypeHandle<Follow> FollowType = GetComponentTypeHandle<Follow>();                    // 跟随类型
			BufferTypeHandle<PathPositions> PositionType = GetBufferTypeHandle<PathPositions>();          // 位置类型
			EntityTypeHandle EntityType = GetEntityTypeHandle();

			var commandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer().AsParallelWriter();


			var findJob = new FindJob()
			{
				DeltaTime = World.Time.DeltaTime,
				map_size = _mapData._size,
				FindParamType = FindParamType,
				FollowType = FollowType,
				PositionType = PositionType,
				commandBuffer = commandBuffer,
				EntityType = EntityType,
				MapDataSource = _mapData._datas,
			};

			Dependency = findJob.ScheduleParallel(m_Group, Dependency);
			m_EntityCommandBufferSystem.AddJobHandleForProducer(Dependency);
		}

		#endregion

		[BurstCompile]
		struct FindJob : IJobChunk
		{
			public float DeltaTime;
			public int2 map_size;
			public EntityCommandBuffer.ParallelWriter commandBuffer;                         // buffer命令队列, 用于把添加组件这种操作批处理
			[ReadOnly] public EntityTypeHandle EntityType;
			public ComponentTypeHandle<FindPathParams> FindParamType; // 查找类型
			public ComponentTypeHandle<Follow> FollowType;    // 跟随类型
			public BufferTypeHandle<PathPositions> PositionType;  // 位置类型

			[ReadOnly]
			public NativeArray<Node> MapDataSource;   // 地图原始数据

			void ResetMap(NativeArray<Node> mapData)
			{
				MapDataSource.CopyTo(mapData);
			}

			void FindPath(FindPathParams param, DynamicBuffer<PathPositions> paths, NativeArray<Node> mapData)
			{
				AStarSystem.FindPath(param.start_pos, param.end_pos, mapData, map_size, paths);
			}

			public void Execute(in ArchetypeChunk chunk, int chunkIndex, bool useEnabledMask, in v128 chunkEnabledMask)
			{
				var chunkFindParams = chunk.GetNativeArray(FindParamType);
				//chunk.Ge
				var chunkEntity = chunk.GetNativeArray(EntityType);
				var chunkFollow = chunk.GetNativeArray(FollowType);
				bool hasFollow = chunk.Has(FollowType);

				NativeArray<Node> mapData = new NativeArray<Node>(MapDataSource.Length, Allocator.Temp);
				if (chunk.Has(PositionType)) // 有position的情况
				{
					var chunkPosition = chunk.GetBufferAccessor(PositionType);
					for (int i = 0; i < chunk.Count; i++)
					{
						var param = chunkFindParams[i];
						var paths = chunkPosition[i];
						ResetMap(mapData);
						FindPath(param, paths, mapData);

						commandBuffer.RemoveComponent<FindPathParams>(chunkIndex, chunkEntity[i]);

						if (hasFollow)
						{
							chunkFollow[i] = new Follow { Value = paths.Length };
						}
					}
				}
				else
				{
					// 没有position的情况
					for (int i = 0; i < chunk.Count; i++)
					{
						var param = chunkFindParams[i];
						var paths = commandBuffer.AddBuffer<PathPositions>(chunkIndex, chunkEntity[i]);
						ResetMap(mapData);
						FindPath(param, paths, mapData);

						commandBuffer.RemoveComponent<FindPathParams>(chunkIndex, chunkEntity[i]);

						if (hasFollow)
						{
							chunkFollow[i] = new Follow { Value = paths.Length };
						}
					}
				}

				mapData.Dispose();
			}
		}

	}

}