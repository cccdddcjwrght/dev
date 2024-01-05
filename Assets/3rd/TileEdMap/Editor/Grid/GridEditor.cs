using System.Collections;
using System.Collections.Generic;
using System.Linq;
using plyLib;
using UnityEditor;
using UnityEngine;

namespace GameTools.Maps
{
	[CustomEditor(typeof(GameTools.Maps.Grid))]
	public class GridEditor : Editor
	{
		private string[] tags;
		private int selectTags;

		private Plane plane;
		private Vector3 gridWorldPos;

		public GameTools.Maps.Grid grid { get { return target as Grid; } }

		public override void OnInspectorGUI()
		{
			if (tags == null)
			{

				var t = grid.alltags.ToList();
				t.Insert(0, "-grid");
				tags = t.ToArray();
			}
			var s = EditorGUILayout.Popup("œ‘ æTag£∫", selectTags, tags);
			if (s != selectTags)
			{
				grid.selectTag = tags[s];
				selectTags = s;
			}
			base.OnInspectorGUI();
		}

		private void OnSceneGUI()
		{
			plane = new Plane(Vector3.up, new Vector3(0f, 0, 0f));
			var ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);

			if (plane.Raycast(ray, out var d))
			{
				gridWorldPos = ray.GetPoint(d);
				var gp = RecalcCalcGridPos(ref gridWorldPos);
				Handles.DrawWireCube(gridWorldPos, Vector3.one * 0.5f);
				grid.selectIndex = grid.CellIndex(gridWorldPos);
			}
			SceneView.currentDrawingSceneView.Repaint();
		}


		public Vector3Int RecalcCalcGridPos(ref Vector3 pos)
		{
			pos.x = grid.size * Mathf.Round(pos.x / grid.size);
			pos.y = 0;
			pos.z = grid.size * Mathf.Round(pos.z / grid.size);
			return new Vector3Int()
			{
				x = Mathf.RoundToInt(pos.x / grid.size),
				y = 0,
				z = Mathf.RoundToInt(pos.z / grid.size)
			};
		}

	}

}