using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMapExcute_SetScale : TileEdExt.IMapExcute
{
	public string title => "正交场景调整";

	public bool fade { get; set; }

	public Vector3 scale { get; set; } = Vector3.one;

	public void OnGUI(bool show)
	{
		if (show)
			scale = GUIHelp.DrawObject(scale, "场景缩放:");
	}

	public void Excute(GameObject go, plyLib.TileEdMap map)
	{
		if (go)
		{
			var ls = new List<GameObject>();
			go.transform.localScale = scale;
			foreach (Transform item in go.transform)
			{
				if (item.childCount == 0) ls.Add(item.gameObject);
			}

			ls.ForEach(g => GameObject.DestroyImmediate(g));

		}
	}
}

