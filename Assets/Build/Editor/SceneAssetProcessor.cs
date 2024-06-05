using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class SceneAssetProcessor
{
	static string G_SHADER = "Universal Render Pipeline/Simple Lit";
	static int G_TEX_NAME_ID = Shader.PropertyToID("_BaseMap");

	static string G_SCENE_ASSET_SAVE_DIR = "Assets/BuildAsset/Art/scenes/";
	static string G_TILE_PREFAB_ROOT = "Assets/BuildAsset/Prefabs/Scenes/"; 
	static string G_ASSET_PREFAB_ROOT = "Assets/BuildAsset/Prefabs/Scenes/";

	static string[] G_PATTERNS = new string[] {
		"scene*.png",
		"scene_terrain*.fbx",
		"scene_floor*.fbx",
		"scene_wall*.fbx",
		"scene_object*.fbx",
		"scene_tool*.fbx",
		"scene_table*.fbx"
	};

	static private string _currentObjPath;

	[MenuItem("[Tools]/Scene/导入场景资源")]
	static void ImportSceneAsset()
	{
		var lastpath = EditorPrefs.GetString("__lastpath");
		var dir = EditorUtility.OpenFolderPanel("场景资源目录", lastpath, null);
		if (!string.IsNullOrEmpty(dir))
		{
			EditorPrefs.SetString("__lastpath", Path.GetDirectoryName(dir));
			var name = Path.GetFileName(dir);
			if (name.StartsWith("scene"))
			{
				var files = G_PATTERNS.AsParallel().SelectMany(p => Directory.EnumerateFiles(dir, p, SearchOption.AllDirectories)).ToList();

				if (files != null && files.Count > 0)
				{
					var outpath = G_SCENE_ASSET_SAVE_DIR + name + "/";
					if (!Directory.Exists(outpath))
						Directory.CreateDirectory(outpath);
					var assets = new List<string>();
					files.ForEach(f =>
					{
						var p = outpath + Path.GetFileName(f);
						File.Copy(f, p, true);
						assets.Add(p);
					});
					AssetDatabase.Refresh();
					ExcuteFbx(assets);
					ExcuteSceneAsset(outpath);
				}
			}
			else
			{
				Debug.LogError("当前文件夹没有以 scene 开头,不处理 ");
			}
		}
	}

	[MenuItem("Assets/地块/资源处理")]
	static void Excute()
	{
		var select = Selection.activeObject;
		Debug.Log(select.ToString());
		if (select != null)
			ExcuteSceneAsset(select is GameObject g ? g : AssetDatabase.GetAssetPath(select));
	}

	[MenuItem("Assets/地块/模型处理")]
	static void ExcuteFbx()
	{

		var select = Selection.activeObject;
		if (select != null)
			ExcuteFbx(AssetDatabase.GetAssetPath(select));

	}

	[MenuItem("Assets/地块/替换简单shader")]
	static void ExcuteShader() {

		var select = Selection.activeObject;
		if (select != null)
		{
			var dir = AssetDatabase.GetAssetPath(select);
			if (System.IO.Directory.Exists(dir))
			{
				var mats = AssetDatabase.FindAssets("t:Material", new string[] { dir })
					.Select(g=>AssetDatabase.LoadAssetAtPath<Material>(AssetDatabase.GUIDToAssetPath(g))).ToList();
			
				if(mats?.Count > 0)
				{
					var shader = Shader.Find(G_SHADER);
					var oldName = "Universal Render Pipeline/Lit";
					if (shader != null)
					{
						foreach (var item in mats)
						{
							if(item!=null && item.shader.name == oldName)
							{
								item.shader = shader;
								AssetDatabase.SaveAssetIfDirty(item);
							}
						}
					}
					AssetDatabase.Refresh();
				}
			}
		}

	}

	static void ExcuteSceneAsset(object select)
	{
		var objs = new List<GameObject>();
		if (select is string path && System.IO.Directory.Exists(path))
		{
			var models = AssetDatabase.FindAssets("t:model", new string[] { path });
			foreach (var item in models)
			{
				path = AssetDatabase.GUIDToAssetPath(item);
				var g = AssetDatabase.LoadAssetAtPath<GameObject>(path);
				if (g) objs.Add(g);
			}
		}
		else if (select is GameObject g)
		{
			objs.Add(g);
		}

		if (objs.Count > 0)
		{

			if (!System.IO.Directory.Exists(G_TILE_PREFAB_ROOT))
				System.IO.Directory.CreateDirectory(G_TILE_PREFAB_ROOT);

			if (!System.IO.Directory.Exists(G_ASSET_PREFAB_ROOT))
				System.IO.Directory.CreateDirectory(G_ASSET_PREFAB_ROOT);

			Dictionary<string, Material> mats = new Dictionary<string, Material>();

			foreach (var o in objs)
			{

				var opath = System.IO.Path.GetDirectoryName(AssetDatabase.GetAssetPath(o));
				var dir = System.IO.Path.GetFileNameWithoutExtension(opath);
				var matname = dir;

				_currentObjPath = opath;
				dir =  G_TILE_PREFAB_ROOT + dir;
				if (!System.IO.Directory.Exists(dir))
					System.IO.Directory.CreateDirectory(dir);

				if (!mats.TryGetValue(matname, out var mat))
				{
					var tpath = opath + "/" + matname + ".png";
					var mpath = opath + "/" + matname + ".mat";
					var ipath = opath + "/" + matname + ".txt";
					if (File.Exists(mpath))
						mat = AssetDatabase.LoadAssetAtPath<Material>(mpath);
					if (mat == null && !File.Exists(ipath))
						mat = CreateMat(matname, dir + "/" + matname + ".mat", tpath);
					if (mat == null)
					{
						var md = dir + "/mats/";
						var rect = FindUV(ipath, o.name);
						tpath = File.Exists(tpath) && rect.width != 0 ? tpath : opath + "/" + o.name + ".png";
						if (File.Exists(tpath))
						{
							mpath = md + o.name + ".mat";
							if (File.Exists(mpath))
								mat = AssetDatabase.LoadAssetAtPath<Material>(mpath);
							else if (!Directory.Exists(md))
								Directory.CreateDirectory(md);
							mats[o.name] = mat = CreateMat(matname, mpath, tpath, mat);
							if (rect.width > 0)
							{
								mat.SetTextureOffset(G_TEX_NAME_ID, rect.position);
								mat.SetTextureScale(G_TEX_NAME_ID, rect.size);
							}
						}
					}
					else
						mats.Add(matname, mat);
				}

				if (o.name.Contains("wall"))
				{
					CreateWall(o, mat, 0, dir, Vector3.zero);
					CreateWall(o, mat, 1, dir, Vector3.zero, new Vector3(1, -1, 1));
				}
				else
					CreateWall(o, mat, 0, dir);
			}
			AssetDatabase.Refresh();

		}
	}

	static Material CreateMat(string name, string mpath, string tpath, Material oldmat = null)
	{
		var mat = default(Material);
		if (!File.Exists(tpath))
			tpath = Directory.GetFiles(_currentObjPath, "scene*.png").FirstOrDefault();
		if (File.Exists(tpath))
		{

			SetTextureSetting(tpath);
			var tex = AssetDatabase.LoadAssetAtPath<Texture>(tpath);
			if (tex != null)
			{
				mat = new Material(Shader.Find(G_SHADER));
				if (oldmat) mat.CopyPropertiesFromMaterial(oldmat);
				mat.SetTexture(G_TEX_NAME_ID, tex);
				AssetDatabase.CreateAsset(mat, mpath);
				AssetDatabase.Refresh();
				mat = AssetDatabase.LoadAssetAtPath<Material>(mpath);
			}
		}

		return mat ?? oldmat;
	}

	static void SetTextureSetting(string texture)
	{

		var tp = AssetImporter.GetAtPath(texture) as TextureImporter;
		if (tp != null)
		{
			tp.mipmapEnabled = false;
		}

	}

	static void CreateWall(GameObject o, Material mat, int index, string savepath, Vector3 local = default, Vector3 scale = default)
	{

		var p = new GameObject();
		p.name = o.name + (index > 0 ? "_" + index.ToString() : "");
		var g = GameObject.Instantiate(o);
		if (o.name.Contains("_tool") && g.GetComponent<Animation>() != null)
			g.GetComponent<Animation>().playAutomatically = false;
		g.transform.SetParent(p.transform, false);
		g.name = "body";
		g.transform.localPosition = local;
		if (scale != default)
			g.transform.localScale = scale;
		else
			g.transform.localScale = Vector3.one;

		var rs = p.GetComponentsInChildren<Renderer>();
		if (rs != null && rs.Length > 0)
		{
			var defMat = AssetDatabase.LoadAssetAtPath<Material>(_currentObjPath + o.name + ".mat") ?? mat;

			if (defMat != null)
			{
				foreach (var item in rs)
					item.sharedMaterials = item.sharedMaterials.Select(m => defMat).ToArray();
			}
		}

		var path = System.IO.Path.Combine(savepath, p.name + ".prefab");
		PrefabUtility.SaveAsPrefabAsset(p, path, out var success);
		GameObject.DestroyImmediate(p);
	}

	static void ExcuteFbx(List<string> assets)
	{
		if (assets?.Count > 0)
			assets.ForEach(ExcuteFbx);
	}

	static void ExcuteFbx(string asset)
	{
		if (asset.Contains("_tool"))
		{
			var import = AssetImporter.GetAtPath(asset) as ModelImporter;
			if (import == null) return;
			var ans = import.clipAnimations;
			if (ans?.Length == 1 && ans[0].name.ToLower().StartsWith("take"))
			{
				ans[0].name = "cook";
				import.animationType = ModelImporterAnimationType.Legacy;
				import.clipAnimations = ans;
				import.animationWrapMode = WrapMode.Loop;
				import.SaveAndReimport();
			}
		}
	}

	#region MyRegion

	static private Dictionary<string, string[]> _uvs = new Dictionary<string, string[]>();

	static Rect FindUV(string uvpath, string path)
	{
		if (!string.IsNullOrEmpty(uvpath) && !string.IsNullOrEmpty(path))
		{
			if (!_uvs.TryGetValue(uvpath, out var val) && File.Exists(uvpath))
			{
				_uvs[uvpath] = val = File.ReadAllLines(uvpath);
			}
			if (val != null)
			{
				var v = val.FirstOrDefault(f => f.Contains(path))?.Split('|');
				if (v?.Length >= 5)
					return new Rect(float.Parse(v[1]), float.Parse(v[2]), float.Parse(v[3]), float.Parse(v[4]));
			}
		}
		return default;
	}

	#endregion

}
