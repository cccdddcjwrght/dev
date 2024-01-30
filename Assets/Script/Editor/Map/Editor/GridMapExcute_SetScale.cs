using System.Collections;
using System.Collections.Generic;
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
			go.transform.localScale = new Vector3(1, 5, 1);
	}
}

