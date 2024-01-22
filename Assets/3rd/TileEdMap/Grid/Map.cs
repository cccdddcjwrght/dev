using System.Collections;
using System.Collections.Generic;
using DataSets;
using UnityEngine;

namespace GameTools.Maps
{
	public enum NearDir
	{
		R = 0,
		RB,
		B,
		LB,
		L,
		LT,
		T,
		RT,
		__MAX__,
	}

	[System.Flags]
	public enum MaskFlag
	{
		UnWalkable = 1 << 0,
	}

	public partial class Map
	{
		public int id;
		public string name;
		public string desc;
		public Grid grid;
	}

	[System.Serializable]
	public partial class Grid
	{
		/// <summary>
		/// 顺时针四个角 左下-左上-右上-右下
		/// </summary>
		public Vector2Int[] corners { get; private set; }
		[SerializeField]
		private Vector2Int bl;
		[SerializeField]
		private Vector2Int tr;
		[SerializeField]
		private int stepX;
		[SerializeField]
		private int stepY;
		[SerializeField]
		private Bounds bounds;

		public float size;
		public Vector3 offset;
		public List<Cell> cells;

		public Vector2Int gridSize { get { return new Vector2Int(stepX, stepY); } }
		public Dictionary<string, List<int>> tags { get; set; }
		/// <summary>
		/// 建筑配置key=》场景建筑ID
		/// </summary>
		public Dictionary<string, List<int>> builds { get; private set; }
		/// <summary>
		/// 场景建筑ID=》格子index
		/// </summary>
		public Dictionary<int, int> buildIndexs { get; private set; }
		public List<int> walkables { get; set; }


		public Grid Create(Vector2Int min, Vector2Int max)
		{
			if (min == max)
				return this;
			bl = min;
			tr = max;

			corners = new Vector2Int[] {
				min,
				new Vector2Int(min.x,max.y),
				max,
				new Vector2Int(max.x,min.y),
			};

			bounds = new Bounds();
			bounds.Encapsulate(new Vector3(min.x, 0, min.y));
			bounds.Encapsulate(new Vector3(max.x, 0, max.y));

			stepX = max.x - min.x + 1;
			stepY = max.y - min.y + 1;

			cells = new List<Cell>();
			var index = 0;
			for (int i = min.y; i <= max.y; i++)
			{
				for (int j = min.x; j <= max.x; j++)
				{
					var cell = new Cell()
					{
						index = index++,
						x = j,
						y = i,
					};
					FillNear(cell);
					cells.Add(cell);
				}
			}

			return this;

		}

		public Grid Refresh()
		{
			corners = new Vector2Int[] {
				bl,
				new Vector2Int(bl.x,tr.y),
				tr,
				new Vector2Int(tr.x,bl.y),
			};

			if (cells != null && (tags == null || walkables == null))
			{
				walkables = new List<int>();
				tags = new Dictionary<string, List<int>>();
				builds = new Dictionary<string, List<int>>();
				buildIndexs = new Dictionary<int, int>();


				for (int i = 0; i < cells.Count; i++)
				{
					var cell = cells[i];
					if (cell.tags?.Count > 0)
					{
						for (int j = 0; j < cell.tags.Count; j++)
						{
							if (!tags.TryGetValue(cell.tags[j], out var ls)) tags[cell.tags[j]] = ls = new List<int>();
							ls.Add(cell.index);
						}
					}

					if (cell.builds?.Count > 0)
					{
						for (int j = 0; j < cell.builds.Count; j++)
						{
							var id = int.Parse(cell.builds[j]);
							buildIndexs[id] = cell.index;

							var d = cell.GetDataSetByBuildName(cell.builds[j]);
							var name = d.GetValByPath("itemid");
							if (string.IsNullOrEmpty(name)) continue;
							if (!builds.TryGetValue(name, out var ls)) builds[name] = ls = new List<int>();
							ls.Add(id);

						}
					}

					if (cell.walkcost >= 0 && cell.flag)
						walkables.Add(cell.index);

				}

			}
			return this;
		}

		public Grid Clear()
		{
			tags?.Clear();
			walkables?.Clear();
			cells?.Clear();
			tags = null;
			walkables = null;
			cells = null;
			return this;
		}

		#region Index

		public bool IsInGridByIndex(int x, int y)
		{
			return x >= 0 && x < stepX && y >= 0 && y < stepY;
		}

		public Cell GetCellByIndex(int x, int y)
		{
			if (!IsInGridByIndex(x, y)) return default;
			return GetCell(y * stepX + x);
		}

		public int CellIndex(Vector3 pos)
		{
			var p = GridPos(pos);
			p -= bl;
			if (IsInGridByIndex(p.x, p.y))
				return p.y * stepX + p.x;
			return -1;
		}

		public Vector2Int GridIndexPos(Vector3 pos)
		{
			return GridPos(pos) - bl;
		}

		public Vector3 GetCellPositionByIndex(int x, int y)
		{
			if (!IsInGridByIndex(x, y)) return default;
			return GetCellPosition(y * stepX + x);
		}

		#endregion

		#region Position

		public bool IsInGrid(int x, int y)
		{
			return x >= bl.x && x <= tr.x && y >= bl.y && y <= tr.y;
		}

		public bool IsInGrid(Vector3 pos)
		{
			var index = CellIndex(pos);
			return index >= 0;
		}


		public Cell GetCell(int x, int y)
		{
			x -= bl.x;
			y -= bl.y;
			return GetCell(y * stepX + x);
		}

		public Vector2Int GridPos(Vector3 pos)
		{
			pos -= offset;
			pos.x = size * Mathf.Round(pos.x / size);
			pos.z = size * Mathf.Round(pos.z / size);
			return new Vector2Int()
			{
				x = Mathf.RoundToInt(pos.x / size),
				y = Mathf.RoundToInt(pos.z / size)
			};
		}

		public int PosToIndex(int x, int y)
		{
			x -= bl.x;
			y -= bl.y;
			return y * stepX + x;
		}

		#endregion

		public Cell GetCell(int index)
		{
			try
			{
				if (index >= 0 && cells.Count > index) return cells[index];
			}
			catch (System.Exception e)
			{
				Debug.Log(index);
				Debug.LogException(e);
			}
			return default;
		}

		public Vector3 GetCellPosition(int x, int y)
		{
			if (!IsInGrid(x, y)) return default;
			x -= bl.x;
			y -= bl.y;
			return GetCellPosition(y * stepX + x);
		}

		public Vector3 GetCellPosition(int index)
		{
			var c = GetCell(index);
			if (c != null)
				return new Vector3(c.x, 0, c.y) * size + offset;
			return offset;
		}

		public Cell GetCellNear(int index, NearDir dir)
		{
			var c = GetCell(index);
			if (c != null)
				return GetCell(c.GetNear(dir));
			return default;
		}

		public int GetWalkCost(Cell cell)
		{
			if (cell == null || !cell.flag) return -1;
			if (cell.relex != null && cell.relex.Length > 0)
			{
				var rc = GetCell(cell.relex[0], cell.relex[1]);
				if (rc != null && rc.builds?.Count > 0)
				{
					var b = rc.GetDataSetByBuildName(cell.relex[2].ToString());
					if (b != null) return b.GetVal("level") >= cell.relex[3] ? -1 : 0;
				}
			}
			return cell.IsWalkable() ? cell.walkcost : -1;
		}

		public DataSet GetBuild(int buildID)
		{

			if (buildIndexs.TryGetValue(buildID, out var index))
			{
				var cell = GetCell(index);
				if (cell != null)
				{
					return cell.GetDataSetByBuildName(buildID.ToString());
				}
			}
			return default;
		}

		public string GetBuildAsset(int buildID)
		{
			if (buildIndexs.TryGetValue(buildID, out var index))
			{
				var cell = GetCell(index);
				if (cell != null)
					return cell.GetAsset(buildID);
			}
			return default;
		}

		public GameObject GetBuildObject(int buildID)
		{
			if (buildIndexs.TryGetValue(buildID, out var index))
			{
				var cell = GetCell(index);
				if (cell != null)
					return cell.GetBuildObject(buildID);
			}
			return default;
		}

		public List<int> GetIDListByTag(string tag)
		{
			if (tags.TryGetValue(tag, out var cs) && cs.Count > 0)
			{
				var ls = new List<int>();
				for (int i = 0; i < cs.Count; i++)
				{
					var c = GetCell(cs[i]);
					if (c != null)
						ls.Add(c.GetDataSetByTag(tag).i_val);
				}
				return ls;
			}
			return default;
		}

		public List<int> GetIDListByBuild(string buildName)
		{
			if (builds.TryGetValue(buildName, out var cs) && cs.Count > 0)
			{
				var ls = new List<int>();
				for (int i = 0; i < cs.Count; i++)
					ls.Add(cs[i]);
				return ls;
			}
			return default;
		}

		public bool GetNearTagPos(int x, int y, string tag, out Vector2Int index)
		{
			index = default;
			var c = GetCell(x, y);
			if (c != null)
			{
				for (int i = 0; i < c.nears.Length; i += 2)
				{
					if (c.nears[i] >= 0)
					{
						var n = GetCell(c.nears[i]);
						if (n != null && c.tags?.Contains(tag) == true)
						{
							index = new Vector2Int(c.x, c.y);
							return true;
						}
					}
				}
			}
			return false;
		}

		public List<Vector2Int> GetNearTagAllPos(int index, string tag)
		{
			return GetNearTagAllPos(GetCell(index), tag);
		}

		public List<Vector2Int> GetNearTagAllPos(int x, int y, string tag)
		{
			return GetNearTagAllPos(GetCell(x, y), tag);
		}

		private List<Vector2Int> GetNearTagAllPos(Cell c, string tag)
		{
			if (c != null)
			{
				var ls = new List<Vector2Int>();
				for (int i = 0; i < c.nears.Length; i += 2)
				{
					if (c.nears[i] >= 0)
					{
						var n = GetCell(c.nears[i]);
						if (n != null && n.tags?.Contains(tag) == true)
							ls.Add(new Vector2Int(n.x, n.y));
					}
				}
				return ls;
			}
			return default;
		}

		private void FillNear(Cell cell)
		{

			var nr = cell.x < tr.x;
			var nb = cell.y > bl.y;
			var nl = cell.x > bl.x;
			var nt = cell.y < tr.y;

			cell.nears = new int[] {
				nr ? cell.index+1:-1,
				nr && nb ? cell.index+1-stepX:-1,
				nb ? cell.index - stepX:-1,
				nb && nl ? cell.index - stepX - 1:-1,
				nl ? cell.index-1:-1,
				nl && nt?cell.index+stepX-1:-1,
				nt ? cell.index+stepX:-1,
				nt && nr?cell.index+stepX+1:-1
			};

		}



	}

	[System.Serializable]
	public partial class Cell
	{
		public int index;
		public int x;
		public int y;
		public int[] nears = new int[8];
		public bool flag = false;


		public string name;
		public Vector3 pos;
		public int walkcost;
		public int[] relex = new int[0];

		public List<string> tags = new List<string>();
		public List<string> builds = new List<string>();
		public DataSet data = new DataSet();

		public GameObject cell;

		public int maskflag;

		public Cell Marking(MaskFlag flag, bool remove = false)
		{
			if (!remove)
				maskflag |= (int)flag;
			else
				maskflag &= ~(int)flag;
			return this;

		}

		public bool IsWalkable()
		{
			if (walkcost >= 0)
				return maskflag == 0 || (((MaskFlag)maskflag & MaskFlag.UnWalkable) != MaskFlag.UnWalkable);
			return false;
		}

		public int GetNear(NearDir dir)
		{
			return nears[(int)dir];
		}

		public DataSet GetDataSetByTag(string tag)
		{
			return data.GetValByPath(tag);
		}

		public DataSet GetDataSetByBuildName(string build)
		{
			return data.GetValByPath(build);
		}

		public GameObject GetBuildObject(int buildID)
		{
			if (cell != null)
			{
				var bk = buildID.ToString();
				if (builds.Contains(bk))
					return cell.transform.Find("build/" + bk)?.gameObject;
			}
			return default;
		}

		public Transform GetBuildLayer()
		{
			if (cell)
				return cell.transform.Find("build") ?? cell.transform;
			return default;
		}

		/// <summary>
		/// 获取建筑资源
		/// </summary>
		/// <param name="buildID">id</param>
		/// <param name="lv">等级（默认获取当前等级）</param>
		/// <returns></returns>
		public string GetAsset(int buildID, int lv = -1)
		{
			var bk = buildID.ToString();
			if (builds.Contains(bk))
			{
				var bd = GetDataSetByBuildName(bk);
				if (bd != null)
				{
					var levelData = bd.GetValByPath("level");
					if (levelData != null)
					{
						lv = lv >= 0 ? lv : levelData.i_val;
						return levelData.GetValByPath(lv.ToString());
					}
				}
			}
			return default;
		}

	}

}