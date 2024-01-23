using System;
using System.Collections;
using System.Collections.Generic;
using GameTools.Maps;
using GameTools.Paths;
using UnityEngine;

namespace GameTools
{
	public class MapAgent : MonoBehaviour, IMap
	{
		private int[] holder;

		public bool enableHold;
		[SerializeField]
		private Maps.Grid _grid;

		public int version { get; set; } = 1;
		public Vector2Int mapSize { get; set; }
		public int cellSize { get; set; }

		public Maps.Grid grid
		{
			get
			{
				if (_grid == null)
				{
					_grid = transform.GetComponent<Maps.Grid>() ?? GameObject.FindObjectOfType<Maps.Grid>(true);
					Init();
				}
				return _grid;
			}
		}

		public Vector3 GetStartPos()
		{
			return GetPos(0, 0);
		}

		public bool GetWalkable(int index)
		{
			var c = grid.GetCell(index);
			if (c != null) return (!enableHold || holder[index] == 0) && grid.GetWalkCost(c) >= 0;
			return false;
		}

		public Vector3 GetPos(int x, int y)
		{
			return grid.GetCellPositionByIndex(x, y);
		}

		public bool IsInMap(Vector3 pos)
		{
			return grid.IsInGrid(pos);
		}

		public Vector2Int GetGridPos(Vector3 pos)
		{
			return grid.GridIndexPos(pos);
		}

		public MapInfo GetMapInfo()
		{
			return new MapInfo()
			{
				offset = grid.offset,
				dimension = new Unity.Mathematics.int2(grid.gridSize.x, grid.gridSize.y),
				size = (int)grid.size,
				minx = grid.corners[0].x,
				miny = grid.corners[0].y,
			};
		}

		public void Hold(int x, int y, int holder)
		{
			if (grid.IsInGridByIndex(x, y))
			{
				var id = x + grid.gridSize.x * y;
				this.holder[id] = holder;
				version++;
			}
			else if (x == -1 && y == -1 && holder == 0)
				Array.Fill(this.holder, 0);

		}

		private void Init()
		{
			if (_grid == null)
				return;
			_grid.walkables = null;
			_grid.Refresh();
			mapSize = _grid.gridSize;
			cellSize = (int)_grid.size;
			holder = new int[grid.cells.Count];
		}

		#region Mono

		private void Awake()
		{
			grid?.Refresh();
			Init();
			AStar.Init(this);
			agent = this;
		}

		private void Update()
		{
			if (Input.GetMouseButtonDown(0))
				AStar.UpdateHitMapPoint();
			else if (Input.GetMouseButtonDown(1))
			{
				AStar.UpdateHitMapPoint();
				var cell = grid.GetCell(grid.CellIndex(AStar.hit));
				if (cell != null)
				{
					version++;
					cell.Marking(MaskFlag.UnWalkable, !cell.IsWalkable());
				}
			}
		}
		#endregion

		#region Static

		static private MapAgent agent;

		/// <summary>
		/// 格子转坐标
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public static Vector3 CellToVector(int x, int y)
		{
			if (agent)
				return agent.grid.GetCellPosition(x, y);

			return default;
		}

		/// <summary>
		/// 坐标转格子
		/// </summary>
		/// <param name="pos"></param>
		/// <returns></returns>
		public static Vector2Int VectorToGrid(Vector3 pos)
		{
			if (agent)
				return agent.grid.GridPos(pos);
			return default;
		}

		/// <summary>
		/// 获取格子附件目标tag的位置
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="tag"></param>
		/// <param name="vector"></param>
		/// <returns></returns>
		public static bool GetNearTagPoint(int x, int y, string tag, out Vector2Int vector)
		{
			vector = default;
			if (agent != null)
				return agent.grid.GetNearTagPos(x, y, tag, out vector);
			return false;

		}

		public static Vector2Int IndexToGrid(int index)
		{
			if (agent != null)
			{
				var c = agent.grid.GetCell(index);
				if (c != null) return new Vector2Int(c.x, c.y);
			}
			return default;
		}

		/// <summary>
		/// 格子点转索引
		/// </summary>
		/// <param name="pos"></param>
		/// <returns></returns>
		public static Vector2Int GridToIndex(Vector2Int pos)
		{
			if(agent != null)
			{
				var c = agent.grid.corners[0];
				pos.x -= c.x;
				pos.y -= c.y;
				return pos;
			}
			return default;
		}


		#endregion

	}
}

