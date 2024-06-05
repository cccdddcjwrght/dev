using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GridMapExcute_MeshCombine : TileEdExt.IMapExcute, TileEdExt.IAfterExcute
{
	public string title => "网格合并";

	public bool fade { get; set; }

	private bool _combine;

	public void OnGUI(bool show)
	{
		if (show)
		{
			_combine = GUIHelp.DrawToggle("开启合并:", _combine);
		}
	}

	public void Excute(GameObject go, plyLib.TileEdMap map)
	{
		var layer = GameObject.Find("RoomArea");
		if (layer != null)
		{
			SetAreaEffect(layer);
			if (_combine) TextureCombine.CombineLayer(layer, combineChild: true);
			else ResetStatic(layer.transform, ((int)StaticEditorFlags.BatchingStatic));
		}
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

	static void SetAreaEffect(GameObject layer)
	{
		if (layer != null)
		{
			foreach (Transform item in layer.transform)
			{
				var effect = item.Find("jiesuo");
				if (effect)
				{
					effect.gameObject.SetActive(false);
					effect.name = "__unlock";
				}
			}
		}
	}
}

