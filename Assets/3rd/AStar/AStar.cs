using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Unity.Mathematics;
using Unity.Collections;

namespace GameTools.Paths
{

	public interface IMap
	{
		public int version { get; }
		public Vector2Int mapSize { get; }
		public int cellSize { get; }

		public bool GetWalkable(int index);
		public bool IsInMap(Vector3 pos);
		public Vector3 GetPos(int x, int y);
		public Vector2Int GetGridPos(Vector3 pos);
		/// <summary>
		/// 地图信息
		/// </summary>
		/// <returns></returns>
		public MapInfo GetMapInfo();
	}

	public struct MapInfo
	{
		/// <summary>
		/// 坐标偏移
		/// </summary>
		public Vector3 offset;
		/// <summary>
		/// 尺寸
		/// </summary>
		public int2 dimension;
		/// <summary>
		/// 格子大小
		/// </summary>
		public int size;
		//起始点格子x，y
		public int minx;
		public int miny;
	}

	public struct AStar
	{

		public static IMap map;
		public static bool isHited { get; private set; }
		public static Vector3 hit { get; private set; }

		public static GameObject playerPrefab;


		public static void Init(IMap data)
		{
			map = data;
		}

		static public bool IsInMap(Vector3 pos)
		{
			if (map != null) return map.IsInMap(pos);
			return false;
		}

		static public bool IsWalkable(int index)
		{
			if (map != null) return map.GetWalkable(index);
			return false;
		}

		static public Vector3 GetPos(int x, int y)
		{
			if (map != null) return map.GetPos(x, y);
			return default;
		}

		static public int2 GetGridPos(float3 pos)
		{
			if (map == null) return default;
			var p = map.GetGridPos(pos);
			return new int2(p.x, p.y);
		}

		static public Vector3 GetPos(int x, int y, MapInfo data)
		{
			x += data.minx;
			y += data.miny;
			return new Vector3(x, 0, y) * data.size + data.offset;
		}

		static public int2 GetGridPos(float3 p , MapInfo data)
		{
			Vector3 pos = p; 
			pos -= data.offset;
			pos.x = data.size * Mathf.Round(pos.x / data.size);
			pos.z = data.size * Mathf.Round(pos.z / data.size);
			return new int2()
			{
				x = Mathf.RoundToInt(pos.x / data.size) - data.minx,
				y = Mathf.RoundToInt(pos.z / data.size) - data.miny,
			};
		}

		static public void UpdateHitMapPoint()
		{
			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			var p = new Plane(Vector3.up, map.GetPos(0, 0));
			isHited = false;
			if (p.Raycast(ray, out var dis))
			{
				isHited = true;
				hit = ray.GetPoint(dis);
			}
			else hit = Vector3.one * 10000;
		}
	}

}