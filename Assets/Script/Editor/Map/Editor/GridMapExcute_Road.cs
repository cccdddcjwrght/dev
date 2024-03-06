using System.Collections;
using System.Collections.Generic;
using GameTools.Maps;
using Jacovone;
using OfficeOpenXml;
using UnityEngine;

public class GridMapExcute_Road : TileEdExt.IMapExcute
{
	public string title => "路径处理";

	public bool fade { get; set; }


	public void OnGUI(bool show)
	{

	}

	public void Excute(GameObject go, plyLib.TileEdMap map)
	{
		if (go)
		{
			var roads = map.gameObject.GetComponentsInChildren<PathMagic>();
			if (roads?.Length > 0)
			{
				var grid = go.GetComponent<GameTools.Maps.Grid>();
				grid.roads = new List<Road>();
				foreach (var item in roads)
				{
					if (item.waypoints.Length > 1)
					{
						var road = new Road() { name = item.name.ToLower(), points = new List<Vector3>() };
						grid.roads.Add(road);
						for (int i = 0; i < item.waypoints.Length; i++)
							road.points.Add(item.waypoints[i].position + item.transform.position);
					}
					GameObject.DestroyImmediate(item.gameObject);
				}
			}
			var panel = GameObject.Find("__panel");
			if (panel)
				GameObject.DestroyImmediate(panel.gameObject);
		}
	}
}

