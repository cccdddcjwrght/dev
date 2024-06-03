using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using plyLibEditor;
using System.IO;
using static UnityEngine.Mesh;
using System;
using System.Linq;

public class MeshCombine
{
	static string SAVE_PATH = "Assets/BuildAsset/Others/Mesh/";

	[MenuItem("地块/网格合并")]
	static void Combine()
	{
		var trans = Selection.activeGameObject;
		var go = new GameObject(trans.name+"_combine");
		go.transform.position = trans.transform.position;
		go.transform.rotation = trans.transform.rotation;
		foreach (Transform item in trans.transform)
		{
			Combine(item.gameObject,parent:go.transform);
		}

	}

	static public GameObject Combine(UnityEngine.GameObject go, string pixName = null, int layer = 0 , Transform parent = null)
	{
		if (go == null) return null;
		var oldRenderers = go.GetComponentsInChildren<MeshRenderer>(true);
		if (oldRenderers?.Length <= 1)
			return default;
		var select = go;
		if (!Directory.Exists(SAVE_PATH))
			Directory.CreateDirectory(SAVE_PATH);

		var p = new GameObject(select.name + "_combine");
		p.transform.parent = parent ?? select.transform.parent;
		p.transform.position = select.transform.position;
		p.transform.rotation = select.transform.rotation;

		var ls = plyLibEditor.plyEdUtil.MeshCombine(
			p.transform, new List<GameObject>() { select }, true,
			pixName + select.name, SAVE_PATH,
			false, default, "Combine", 1
		);

		if (oldRenderers.Length <= ls.Count)
		{
			GameObject.DestroyImmediate(p);
			return default;
		}

		if (layer > 0)
			AddCollider(ls, layer);

		select.gameObject.SetActive(false);

		EditorUtility.ClearProgressBar();

		return p;
	}

	static public void AddCollider(Transform transform)
	{
		if (transform == null) return;
		var gs = transform.GetComponentsInChildren<MeshFilter>()?.Select(m => m.gameObject).ToList();
		AddCollider(gs);
	}

	static public void AddCollider(List<GameObject> ls , int layer = 0)
	{
		if (ls != null && ls.Count > 0)
		{
			foreach (var item in ls)
			{
				var filter = item.GetComponent<MeshFilter>() ?? item.GetComponentInChildren<MeshFilter>();
				if (filter != null && filter.sharedMesh != null)
				{
					var mesh = filter.sharedMesh;
					var bounds = mesh.bounds;
					var size = bounds.size;
					if ( size.y >= 4 )
					{
						var box		= item.AddComponent<BoxCollider>();
						var center = bounds.center;
						center.y = center.y - 0.08f;
						box.size = size * 0.8f;
						box.center = bounds.center;
						item.layer = layer;
					}
				}
			}
		}
	}
}

