using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GameTools.Maps
{
	public partial class Grid : MonoBehaviour
	{
		public IEnumerable<string> alltags
		{
			get
			{
				if (tags == null) { Refresh(); }
				return tags.Keys;
			}
		}

#if UNITY_EDITOR
		[HideInInspector]
		[NonSerialized]
		public string selectTag;

		[HideInInspector]
		[NonSerialized]
		public int selectIndex;

		static Color walkcolor = new Color(0.2f, 1, 0.2f, 0.4f);

		static Vector3 c_pos = new Vector3(0, 0.3f, 0);

		private void OnDrawGizmosSelected()
		{
			if (cells != null)
			{
				var nor = Gizmos.color;
				var size = new Vector3(this.size, 0.01f, this.size);
				for (int i = 0; i < cells.Count; i++)
				{
					var c = cells[i];
					var p = new Vector3(c.x, 0, c.y) + transform.position;
					var pos = p * this.size;
					Gizmos.color = nor;


					if (selectTag != null && c.tags.Contains(selectTag))
					{
						Gizmos.color = selectTag.StartsWith("obs") ? Color.red : Color.yellow;
						Gizmos.DrawCube(pos + c_pos, size);
					}
					else if (c.flag && GetWalkCost(c) >= 0)
					{
						Gizmos.color = walkcolor * Mathf.Max((100 - c.walkcost), 0) * 0.01f;
						Gizmos.DrawCube(pos, size);
					}
					else
					{
						Gizmos.color = Color.red;
						Gizmos.DrawWireCube(pos, size);
					}
				}

				Gizmos.color = nor;
				for (int i = 0; i < cells.Count; i++)
				{
					var c = cells[i];
					if (c.builds?.Count > 0)
					{
						var p = new Vector3(c.x, 0, c.y) + transform.position;
						var pos = p * this.size;
						Gizmos.DrawCube(pos + c_pos, Vector3.one * 0.5f);
					}
				}

			}

			if (selectIndex >= 0)
			{
				Gizmos.DrawCube(GetCellPosition(selectIndex) + c_pos, Vector3.one * 0.5f);
			}

			if (roads?.Count > 0)
			{
				foreach (var item in roads)
				{
					for (int i = 0; i < item.points.Count - 1; i++)
						Gizmos.DrawLine(item.points[i], item.points[i + 1]);
				}
			}
		}
#endif

	}

}