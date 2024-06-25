using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using libx;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextureCombine
{
	static string G_SHADER = "Universal Render Pipeline/Lit";

	static int G_TEX_NAME_ID = Shader.PropertyToID("_BaseMap");
	static int G_TEX_NAME2_ID = Shader.PropertyToID("_MainTex");

	static string G_ROOT_PATH = "Assets/BuildAsset/Others/Scenes/";

	[MenuItem("[Tools]/资源/CombineModel")]
	static void CombineModel()
	{
		var selectObj = Selection.activeObject;
		if (!selectObj)
		{
			Debug.LogError("没有选中任何物体，请选择后再执行操作");
			return;
		}
		var meshRenders = Selection.activeTransform.GetComponentsInChildren<MeshRenderer>(true);
		var meshFs = Selection.activeTransform.GetComponentsInChildren<MeshFilter>(true);
		CombineInstance[] combine = new CombineInstance[meshFs.Length];

		Texture2D[] textures = new Texture2D[meshFs.Length];
		List<Vector2[]> uvList = new List<Vector2[]>();
		int uvCount = 0;
		for (int i = 0; i < meshFs.Length; i++)
		{
			var r = meshRenders[i];
			var f = meshFs[i];
			//meshFs[i].transform.position = f.transform.position;
			combine[i].mesh = f.sharedMesh;
			combine[i].transform = f.transform.localToWorldMatrix;
			textures[i] = r.sharedMaterial.GetTexture(G_TEX_NAME_ID) as Texture2D;
			uvList.Add(f.sharedMesh.uv);
			uvCount += f.sharedMesh.uv.Length;
		}

		Texture2D tex = new Texture2D(2048, 2048);
		Rect[] rects = tex.PackTextures(textures, 0);
		Vector2[] uvs = new Vector2[uvCount];
		int j = 0;
		for (int i = 0; i < meshFs.Length; i++)
		{
			foreach (var uv in uvList[i])
			{
				uvs[j].x = Mathf.Lerp(rects[i].xMin, rects[i].xMax, uv.x);
				uvs[j].y = Mathf.Lerp(rects[i].yMin, rects[i].yMax, uv.y);
				j++;
			}
		}
		var thisMeshRender = Selection.activeTransform.GetComponent<MeshRenderer>();
		Material newMat = new Material(meshRenders[0].sharedMaterial.shader);
		newMat.CopyPropertiesFromMaterial(meshRenders[0].sharedMaterial);
		newMat.SetTexture(G_TEX_NAME_ID, tex);
		newMat.name = "TestCombine";
		var newMesh = new Mesh();
		var thisMeshFilter = Selection.activeTransform.GetComponent<MeshFilter>();
		if (thisMeshFilter == null)
		{
			thisMeshFilter = Selection.activeTransform.gameObject.AddComponent<MeshFilter>();
			thisMeshRender = Selection.activeTransform.gameObject.AddComponent<MeshRenderer>();
		}
		newMesh.CombineMeshes(combine, true);
		newMesh.uv = uvs;
		thisMeshFilter.mesh = newMesh;
		thisMeshRender.material = newMat;
	}

	#region 贴图
	[MenuItem("[Tools]/资源/贴图合并")]
	static void CombineTex()
	{
		CombineDirTexture(GetDir());
	}

	static void CombineDirTexture(string dir)
	{

		if (string.IsNullOrEmpty(dir) || !Directory.Exists(dir + "/"))
		{
			Debug.Log("路径为空或路径不存在");
			return;
		}
		var texs = AssetDatabase.FindAssets("t:texture", new string[] { dir });
		if (texs?.Length > 1)
		{
			var saveName = Path.GetFileNameWithoutExtension(dir);
			CombineTextures(Path.Combine(dir, saveName), texs.Select(a => AssetDatabase.GUIDToAssetPath(a)).ToList());
		}
	}

	static void CombineTextures(string savename, List<string> assets)
	{
		if (assets?.Count > 1)
			CombineTextures(savename, LoadAllTexture(assets));
		else
			Debug.LogWarning("低于一张图不需要合并");
	}

	static Dictionary<int, Rect> CombineTextures(string savename, List<Texture2D> textures)
	{
		if (textures?.Count > 1)
		{
			textures.RemoveAll(t => t == null || string.IsNullOrEmpty(t.name));
			MarkReadable(textures, true);
			var newtex = new Texture2D(2048, 2048);
			var rects = newtex.PackTextures(textures.ToArray(), 0);
			var builder = new StringBuilder();
			var rets = new Dictionary<int, Rect>();
			for (int i = 0; i < rects.Length; i++)
			{
				var tex = textures[i];
				var name = AssetDatabase.GetAssetPath(tex);
				var rect = rects[i];
				builder.AppendLine($"{name}|{rect.x}|{rect.y}|{rect.width}|{rect.height}");
				rets[tex.GetInstanceID()] = rect;
			}
			newtex.Apply();
			var dir = Path.GetDirectoryName(savename);
			if (!Directory.Exists(dir))
				Directory.CreateDirectory(dir);
			var tpath = savename + ".png";
			File.WriteAllBytes(tpath, newtex.EncodeToPNG());
			File.WriteAllText(savename + ".txt", builder.ToString());
			MarkReadable(textures, false);
			return rets;
		}
		else
			Debug.LogWarning("低于一张图不需要合并");
		return default;
	}

	static private void MarkReadable(List<Texture2D> texs, bool state)
	{
		if (texs?.Count > 0)
		{
			var flag = false;
			foreach (var item in texs)
			{
				var import = TextureImporter.GetAtPath(AssetDatabase.GetAssetPath(item)) as TextureImporter;
				if (import != null)
				{
					flag = true;
					import.isReadable = state;
					import.mipmapEnabled = false;
					import.SaveAndReimport();
				}
			}
			if (flag)
			{
				AssetDatabase.SaveAssets();
				AssetDatabase.Refresh();
			}
		}
	}

	static private void MarkReadable(List<string> texs, bool state)
	{
		if (texs?.Count > 0)
		{
			var flag = false;
			foreach (var item in texs)
			{
				var import = TextureImporter.GetAtPath(item) as TextureImporter;
				if (import != null)
				{
					flag = true;
					import.isReadable = state;
					import.mipmapEnabled = false;
					import.SaveAndReimport();
				}
			}
			if (flag)
			{
				AssetDatabase.SaveAssets();
				AssetDatabase.Refresh();
			}
		}
	}

	static private List<Texture2D> LoadAllTexture(List<string> texs)
	{
		return texs.Select(a => AssetDatabase.LoadAssetAtPath<Texture2D>(a)).Where(t => t && t.width < 500 && t.height < 500).ToList();
	}

	static Material CreateMat(string name, string mpath, string tpath)
	{
		var mat = default(Material);
		if (File.Exists(tpath) && !File.Exists(mpath))
		{
			SetTextureSetting(tpath);
			var tex = AssetDatabase.LoadAssetAtPath<Texture>(tpath);
			if (tex != null)
			{
				mat = new Material(Shader.Find(G_SHADER));
				mat.SetTexture(G_TEX_NAME_ID, tex);
				AssetDatabase.CreateAsset(mat, mpath);
				AssetDatabase.Refresh();
			}
		}
		if (File.Exists(mpath))
			mat = AssetDatabase.LoadAssetAtPath<Material>(mpath);

		return mat;
	}

	static void SetTextureSetting(string texture)
	{
		var tp = AssetImporter.GetAtPath(texture) as TextureImporter;
		if (tp != null)
		{
			tp.mipmapEnabled = false;
			//tp.maxTextureSize = 512;
		}
	}

	#endregion

	#region Scene

	//[MenuItem("[Tools]/Scene/合并")]
	static void ExcuteSceneGameObject()
	{
		var selectObj = Selection.activeObject;
		if (!selectObj)
		{
			Debug.LogError("没有选中任何物体，请选择后再执行操作");
			return;
		}
		CombineLayer(Selection.activeGameObject, combineChild: true);
	}

	static public void CombineLayer(GameObject layer, string savepath = null, Dictionary<int, Rect> rectIndexs = null, bool combineChild = true)
	{
		if (layer == null)
		{
			Debug.Log("对象空");
			return;
		}

		if (string.IsNullOrEmpty(savepath))
		{
			var scene = SceneManager.GetActiveScene();
			savepath = $"{G_ROOT_PATH}{scene.name}/{scene.name}_{layer.name}";
		}

		var dirName = Path.GetDirectoryName(savepath);
		if (!Directory.Exists(Path.GetDirectoryName(savepath)))
			Directory.CreateDirectory(dirName);

		var meshRenders = layer.GetComponentsInChildren<MeshRenderer>(true);
		Texture2D[] textures = new Texture2D[meshRenders.Length];
		for (int i = 0; i < meshRenders.Length; i++)
		{
			var mat = meshRenders[i].sharedMaterial;
			if (mat.HasTexture(G_TEX_NAME_ID))
				textures[i] = mat.GetTexture(G_TEX_NAME_ID) as Texture2D;
			else if (mat.HasTexture(G_TEX_NAME2_ID))
				textures[i] = mat.GetTexture(G_TEX_NAME2_ID) as Texture2D;
		}

		var texpath = savepath;
		rectIndexs = rectIndexs ?? CombineTextures(texpath, textures.ToList());
		if (rectIndexs?.Count > 0)
		{
			var go = new List<Transform>();
			if (combineChild)
			{
				foreach (Transform item in layer.transform)
				{
					if (item != null && !item.name.StartsWith("@"))
						go.Add(item);
				}
			}
			else
				go.Add(layer.transform);

			go.ForEach(g =>
			{
				CombineGameObject(texpath + ".png", g, rectIndexs);
				GameObject.DestroyImmediate(g.gameObject);
			});
			go.Clear();
		}
	}

	static void CombineGameObject(string combineTexPath, Transform parent, Dictionary<int, Rect> rectIndexs)
	{
		if (parent != null)
		{
			var meshRenders = parent.GetComponentsInChildren<MeshRenderer>(true);
			var groups = meshRenders.GroupBy(m => m.sharedMaterial.shader);
			if (groups?.Count() > 0)
			{
				var i = 0;
				var go = new GameObject(parent.name);
				go.transform.parent = parent.parent;
				go.transform.position = parent.position;
				go.transform.rotation = parent.rotation;
				foreach (var item in groups)
					CombineRender(go.transform, i++, combineTexPath, item.ToList(), rectIndexs);

				//特效处理
				parent.GetComponentsInChildren<ParticleSystem>()
				.Where(p => p.transform.parent.GetComponentInParent<ParticleSystem>() == null)
				.ToList().ForEach((p) => p.transform.SetParent(go.transform, true));

				go.SetActive(parent.gameObject.activeSelf);
			}
		}
	}

	static void CombineRender(Transform parent, int index, string combineTexPath, List<MeshRenderer> meshRenderers, Dictionary<int, Rect> rectIndexs)
	{
		if (meshRenderers?.Count > 0)
		{
			var meshFs = meshRenderers.Select(m => m.GetComponent<MeshFilter>()).ToList();
			var combine = new List<CombineInstance>();
			var textures = new List<Texture2D>();
			var uvList = new List<Vector2[]>();
			var others = new List<MeshRenderer>();
			int uvCount = 0;
			var j = 0;
			for (int i = 0; i < meshFs.Count; i++)
			{
				var r = meshRenderers[i];
				var f = meshFs[i];
				var t = (r.sharedMaterial.GetTexture(G_TEX_NAME_ID) ?? r.sharedMaterial.GetTexture(G_TEX_NAME2_ID)) as Texture2D;
				if (t != null && rectIndexs.ContainsKey(t.GetInstanceID()))
				{
					var inst = new CombineInstance();
					inst.mesh = f.sharedMesh;
					inst.transform = f.transform.localToWorldMatrix;
					combine.Add(inst);
					textures.Add(t);
					uvList.Add(f.sharedMesh.uv);
					uvCount += f.sharedMesh.uv.Length;
					j++;
				}
				else
					others.Add(r);
			}

			var dir = Path.GetDirectoryName(combineTexPath);
			var tex = AssetDatabase.LoadAssetAtPath<Texture2D>(combineTexPath);
			var uvs = new Vector2[uvCount];
			j = 0;
			for (int i = 0; i < textures.Count; i++)
			{
				foreach (var uv in uvList[i])
				{
					if (textures[i] && rectIndexs.TryGetValue(textures[i].GetInstanceID(), out var rect))
					{
						uvs[j].x = Mathf.Lerp(rect.xMin, rect.xMax, uv.x);
						uvs[j].y = Mathf.Lerp(rect.yMin, rect.yMax, uv.y);
					}
					j++;
				}
			}

			if (others?.Count > 0)
			{
				foreach (var item in others)
					item.transform.SetParent(parent, true);
				others.Clear();
			}

			var go = new GameObject(parent.name + $"{index}_combine");
			go.transform.parent = parent;
			go.transform.position = parent.position;
			go.transform.rotation = parent.rotation;

			var thisMeshRender = go.AddComponent<MeshRenderer>();
			var thisMeshFilter = go.AddComponent<MeshFilter>();
			var mat = meshRenderers[0].sharedMaterial;

			Material newMat = new Material(mat.shader);
			var newMesh = new Mesh();
			newMat.CopyPropertiesFromMaterial(mat);
			if (newMat.HasTexture(G_TEX_NAME_ID))
				newMat.SetTexture(G_TEX_NAME_ID, tex);
			else if (newMat.HasTexture(G_TEX_NAME2_ID))
				newMat.SetTexture(G_TEX_NAME2_ID, tex);
			newMat.name = go.name + "_mat";
			newMesh.name = go.name + "_mesh";
			newMesh.CombineMeshes(combine.ToArray(), true);
			newMesh.uv = uvs;

			var mpath = Path.Combine(dir, newMat.name + ".mat");
			var meshpath = Path.Combine(dir, newMesh.name + ".mesh");

			AssetDatabase.CreateAsset(newMat, mpath);
			AssetDatabase.CreateAsset(newMesh, meshpath);
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();

			thisMeshFilter.mesh = AssetDatabase.LoadAssetAtPath<Mesh>(meshpath);
			thisMeshRender.material = AssetDatabase.LoadAssetAtPath<Material>(mpath);
		}
	}

	#endregion

	static private List<T> FilterAssets<T>(string filter, string path, out List<string> assetpaths) where T : UnityEngine.Object
	{
		assetpaths = null;

		var assets = AssetDatabase.FindAssets(filter, new string[] { path });
		if (assets?.Length > 0)
		{
			assetpaths = assets.Select(a => AssetDatabase.GUIDToAssetPath(a)).ToList();
			return assetpaths.Select(a => AssetDatabase.LoadAssetAtPath<T>(a)).ToList();
		}

		return default;
	}

	static private string GetDir()
	{
		var obj = Selection.activeObject;
		if (obj != null)
		{
			var path = AssetDatabase.GetAssetPath(obj);
			if (System.IO.Directory.Exists(path + "/"))
				return path;
		}
		return default;
	}


}
