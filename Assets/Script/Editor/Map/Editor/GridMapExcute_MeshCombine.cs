using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GridMapExcute_MeshCombine : TileEdExt.IMapExcute, TileEdExt.IAfterExcute
{
	public string title => "网格合并";

	public bool fade { get; set; }


	public void OnGUI(bool show)
	{
		if (show) { }
	}

	public void Excute(GameObject go, plyLib.TileEdMap map)
	{
		var layer = GameObject.Find("RoomArea");
		TextureCombine.CombineLayer(layer, combineChild: true);
	}

	static void ResetStatic(Transform transform, int flag = 0)
	{
		if (transform != null)
		{
			GameObjectUtility.SetStaticEditorFlags(transform.gameObject, (StaticEditorFlags)flag);
			foreach (Transform item in transform)
				ResetStatic(item, flag);
		}
	}
}

