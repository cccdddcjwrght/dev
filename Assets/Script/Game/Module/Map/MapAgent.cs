using System.Collections;
using System.Collections.Generic;
using GameTools.Maps;
using GameTools.Paths;
using UnityEngine;

namespace GameTools
{
	public class MapAgent : MonoBehaviour, IMap
	{
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
					_grid.walkables = null;
					_grid.Refresh();
					mapSize = _grid.gridSize;
					cellSize = (int)_grid.size;
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
			if (c != null) return grid.GetWalkCost(c) >= 0;
			return false;
		}

		public Vector3 GetPos(int x, int y)
		{
			return grid.GetCellPositionByIndex(x, y);
		}


		private void Awake()
		{
			grid.walkables = null;
			grid.Refresh();
			mapSize = grid.gridSize;
			cellSize = (int)grid.size;
			grid?.Refresh();
			AStar.Init(this);
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
					cell.Marking(MaskFlag.UnWalkable , !cell.IsWalkable() );
				}
			}
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
	}
}

