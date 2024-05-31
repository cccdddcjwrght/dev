using Unity.Entities;
using Unity.Collections;
using Unity.Mathematics;

namespace GameTools.Paths
{
	// 地图数据同步系统
	[UpdateBefore(typeof(AStarSystem))]
	public class MapSyncSystem : ComponentSystem
	{
		EntityManager _entityManager;
		AStarSystem _astarSystem;

		protected override void OnCreate()
		{
			_entityManager = this.EntityManager;
			_astarSystem = World.GetOrCreateSystem<AStarSystem>();
		}

		protected override void OnUpdate()
		{
			var map_data = AStar.map;
			if (map_data == null || map_data.mapSize.x == 0)
				return;

			int width = map_data.mapSize.x;
			int height = map_data.mapSize.y;
			int len = width * height;

			AStarSystem.MapData mapData = _astarSystem.GetMap();
			if (mapData.IsCreated == false || mapData._size.x != width || mapData._size.y != height)
			{
				// 地图大小变了
				if (mapData.IsCreated)// _CacheMapData.Length != 0)
				{
					// 清空之前的缓存
					mapData._datas.Dispose();
				}

				mapData._datas = AStarSystem.CreateMap(new int2 { x = width, y = height }, Allocator.Persistent);
				// 只是地图内容变了
				for (int i = 0; i < len; i++)
				{
					// 设置可行走位置
					AStarSystem.Node v = mapData._datas[i];
					v.isWalkable = map_data.GetWalkable(i);
					v.cost = map_data.HasHold(i) ? 20 : map_data.GetCost(i);
					mapData._datas[i] = v;
				}

				mapData._size = new int2(map_data.mapSize.x, map_data.mapSize.y);
				mapData._cellSize = map_data.cellSize;
				mapData._startPos = map_data.GetPos(0, 0);
				mapData._version = map_data.version;

				_astarSystem.UpdateMap(mapData);
			}
			else if (mapData._version != map_data.version)
			{
				// 只是地图内容变了
				for (int i = 0; i < len; i++)
				{
					// 设置可行走位置
					AStarSystem.Node v = mapData._datas[i];

					if (!v.isWalkable) continue;
					var c = map_data.HasHold(i) ? 20 : map_data.GetCost(i);
					v.isWalkable = map_data.GetWalkable(i);
					if (v.cost != c) v.cost = c;
					mapData._datas[i] = v;
				}
				mapData._version = map_data.version;
				//_astarSystem.UpdateMapVersion(map_data.version);
				_astarSystem.UpdateMap(mapData);
			}

		}

	}

}