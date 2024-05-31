using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using GameConfigs;
using GameTools.Maps;
using GameTools.Paths;
using UnityEngine;

namespace GameTools
{
	public class MapAgent : MonoBehaviour, IMap
	{
		private int[] holder;
		private int interval;

		public bool enableHold;
		public bool checkHoldCost = true;
		[SerializeField]
		private Maps.Grid _grid;
		private int _gflag = 0;

		public int version { get; set; } = 1;
		public Vector2Int mapSize { get; set; }
		public int cellSize { get; set; }

		public Maps.Grid grid
		{
			get
			{
				if (_gflag == 0)
				{
					_gflag = 1;
					_grid = transform.GetComponent<Maps.Grid>() ?? GameObject.FindObjectOfType<Maps.Grid>(true);
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
			if (_gflag == 0) return false;
			var c = _grid.GetCell(index);
			if (c != null) return (!enableHold || holder[index] == 0) && grid.GetWalkCost(c) >= 0;
			return false;
		}

		public Vector3 GetPos(int x, int y)
		{
			if (_gflag == 0) return default;
			return _grid.GetCellPositionByIndex(x, y);
		}

		public bool IsInMap(Vector3 pos)
		{
			if (_gflag == 0) return default;
			return _grid.IsInGrid(pos);
		}

		public Vector2Int GetGridPos(Vector3 pos)
		{
			if (_gflag == 0) return default;
			return _grid.GridIndexPos(pos);
		}

		public MapInfo GetMapInfo()
		{
			if (_gflag == 0) return default;
			return new MapInfo()
			{
				offset = _grid.offset,
				dimension = new Unity.Mathematics.int2(_grid.gridSize.x, _grid.gridSize.y),
				size = (int)_grid.size,
				minx = _grid.corners[0].x,
				miny = _grid.corners[0].y,
			};
		}

		public bool Hold(int x, int y, int holder)
		{
			if (_gflag == 0) return default;

			if (x == -1 && y == -1 && holder == 0)
			{
				Array.Fill(this.holder, 0);
				interval--;
			}
			else if (_grid.IsInGridByIndex(x, y))
			{
				var id = x + _grid.gridSize.x * y;
				if (holder == 0) return this.holder[id] != 0;
				this.holder[id] = holder;
				if (interval <= 0)
				{
					version++;
					interval = 30;
				}
			}
			return false;
		}

		public bool HasHold(int index)
		{

			if (!checkHoldCost) return false;
			if (index >= 0 && index < this.holder.Length)
				return this.holder[index] != 0;
			return false;
		}

		public int GetCost(int index)
		{
			if (_gflag == 0) return default;
			var cell = _grid.GetCell(index);
			if (cell != null) return cell.walkcost;
			else return 0;
		}


		private void Init()
		{
			if (_grid == null) return;
			_grid.Refresh();
			mapSize = _grid.gridSize;
			cellSize = (int)_grid.size;
			holder = new int[grid.cellCount];
		}

		#region Mono

		private void Awake()
		{
			_randomCell?.Clear();
			_gflag = 0;
			grid.GetInstanceID();
			Init();
			_gflag = _grid == null ? 0 : 1;
			AStar.Init(this);
			agent = this;
			checkHoldCost = GlobalConfig.GetInt("checkholdcost") == 1;
		}

#if UNITY_EDITOR
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
#endif
		#endregion

		#region Static

		static private Dictionary<string, List<Vector2Int>> _randomCell = new Dictionary<string, List<Vector2Int>>();

		static public MapAgent agent { get; protected set; }

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

		/// <summary>
		/// 获取格子附近所有又Tag标签的位置
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="tag"></param>
		/// <returns></returns>
		public static List<Vector2Int> GetAllNearTagPoints(int x, int y, string tag)
		{
			if (agent != null && !string.IsNullOrEmpty(tag))
			{
				return agent.grid.GetNearTagAllPos(x, y, tag);
			}
			return default;
		}

		public static bool GetTagGrid(string tag, out Vector2Int grid)
		{
			grid = default;
			if (agent != null && !string.IsNullOrEmpty(tag))
			{
				if (agent.grid.tags.TryGetValue(tag, out var ts))
				{
					grid = agent.grid.IndexToGrid(ts[0]);
					return true;
				}
			}
			return false;
		}

		public static List<Vector2Int> GetTagGrids(string tag)
		{
			var grids = default(List<Vector2Int>);
			if (agent != null && !string.IsNullOrEmpty(tag))
			{
				if (agent.grid.tags.TryGetValue(tag, out var ts))
					grids = ts.Select(t => agent.grid.IndexToGrid(t)).ToList();
			}
			return grids;
		}

		/// <summary>
		/// 随机获取一个一定范围内随机位置
		/// </summary>
		/// <param name="tag"></param>
		/// <returns></returns>
		public static Vector2Int RandomPop(string tag)
		{
			if (!string.IsNullOrEmpty(tag))
			{

				if (!_randomCell.TryGetValue(tag, out var q))
					_randomCell[tag] = q = new List<Vector2Int>();

				if (q.Count == 0)
				{
					var cells = GetTagGrids(tag);
					if (cells?.Count > 1)
						SGame.Randoms.Random._R.NextItem(cells, cells.Count, ref q, true);
					else
						q.AddRange(cells);
				}

				if (q.Count > 0)
				{
					var c = q[0];
					if (q.Count > 1)
					{
						q.RemoveAt(0);
						q.Add(c);
					}
					return c;
				}

			}
			return default;
		}

		public static Vector3 RandomPopVector(string tag)
		{
			var p = RandomPop(tag);
			return CellToVector(p.x, p.y);
		}

		public static Vector2Int IndexToGrid(int index)
		{
			if (agent != null)
			{
				return agent.grid.IndexToGrid(index);
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
			if (agent != null)
			{
				var c = agent.grid.corners[0];
				pos.x -= c.x;
				pos.y -= c.y;
				return pos;
			}
			return default;
		}

		/// <summary>
		/// 获得一维索引
		/// </summary>
		/// <param name="pos"></param>
		/// <returns></returns>
		public static int GirdToRealIndex(Vector2Int pos)
		{
			Vector2Int index = GridToIndex(pos);
			return index.x + index.y * agent.grid.gridSize.x;
		}

		/// <summary>
		/// 格子id，转索引
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public static int XYToCellIndex(int x, int y)
		{

			if (agent != null)
			{
				var cell = agent.grid.GetCell(x, y);
				if (cell != null) return cell.index;
			}
			return 0;
		}

		/// <summary>
		/// 获取路径
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public static List<Vector3> GetRoad(string name)
		{
			if (agent && !string.IsNullOrEmpty(name))
				return agent.grid.roads.Find(agent => agent.name == name)?.points;
			return default;
		}

		/// <summary>
		/// 获取格子标签列表
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		static public List<string> GetTagsByIndex(int index)
		{
			var cell = agent.grid.GetCell(index);
			if (cell != null) return cell.tags;
			return default;
		}

		/// <summary>
		/// 获取格子标签列表
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		static public List<string> GetTagsByPos(int x, int y)
		{
			var cell = agent.grid.GetCell(x, y);
			if (cell != null) return cell.tags;
			return default;
		}


		#endregion

	}
}

