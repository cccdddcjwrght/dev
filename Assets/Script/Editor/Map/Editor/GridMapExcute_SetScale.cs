using System.Collections;
using System.Collections.Generic;
using OfficeOpenXml;
using UnityEngine;

public class GridMapExcute_SetScale : TileEdExt.IMapExcute
{
	public string title => "正交场景调整";

	public bool fade { get; set; }


	public void OnGUI(bool show)
	{

	}

	public void Excute(GameObject go)
	{
		if (go)
		{
			var ls = new List<GameObject>();
			go.transform.localScale = new Vector3(1, 5, 1);
			foreach (Transform item in go.transform)
			{
				if (item.childCount == 0) ls.Add(item.gameObject);
			}

			ls.ForEach(g=>GameObject.DestroyImmediate(g));

		}
	}
}

