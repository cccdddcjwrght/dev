using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.IO;

public class SceneBake
{
	[MenuItem("[Tools]/Scene/烘培选中场景")]
	static public void Excute()
	{
		var select = Selection.activeObject;
		if (select != null)
		{
			BakeSelect(select);
		}

	}

	[MenuItem("[Tools]/Scene/烘培全部岛屿")]
	static public void BakeIslands()
	{
		BakeSelect("Assets/BuildAsset/Scenes/Islands/markeds/");
	}

	static public void BakeSelect(object select)
	{
		var oldscene = EditorSceneManager.GetActiveScene().path;
		var path = select is string ? (string)select : AssetDatabase.GetAssetPath((Object)select);
		Debug.Log(path);
		if (select is SceneAsset scene)
			Bake(path);
		else if (Directory.Exists(path))
		{
			var scenes = AssetDatabase.FindAssets("t:SceneAsset", new string[] { path });
			if (scenes.Length > 0)
			{
				foreach (var item in scenes)
					Bake(AssetDatabase.GUIDToAssetPath(item));
			}
		}
	}

	static void Bake(string scene)
	{
		if (string.IsNullOrEmpty(scene)) return;
		if (!File.Exists(scene)) return;
		Debug.Log($"Bake Scene:{scene} starting");
		EditorSceneManager.OpenScene(scene);
		Lightmapping.Bake();
		EditorSceneManager.MarkAllScenesDirty();
		EditorSceneManager.SaveOpenScenes();
		Debug.Log($"Bake Scene:{scene} completed");
	}
}
