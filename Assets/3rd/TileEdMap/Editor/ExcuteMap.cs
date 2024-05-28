using System.Collections;
using System.Collections.Generic;
using DataSets;
using plyLib;
using plyLibEditor;
using TileEd;
using UnityEditor;
using UnityEngine;
using System.Linq;
using UnityEditor.SceneManagement;
using System.IO;
using System.Text;
using GameTools.Maps;

namespace TileEdExt
{

	public interface IMapExcute
	{
		string title { get; }
		bool fade { get; set; }
		void OnGUI(bool show);
		void Excute(GameObject go, plyLib.TileEdMap map);
	}

	public interface IAfterExcute
	{

	}


	public class BuildConfig
	{
		public string newSceneName;
		public string newSceneOutput;
		public string bgScene;

		public bool debug;
		public bool dontRemoveInter;
		public string sceneouput;
		public bool enableCombineMesh;
		public string meshouput;
	}

	public class ExcuteMap : TileEd.TileEd_Ed_Base
	{
		private static List<IMapExcute> _excuteTypes;

		[InitializeOnLoadMethod]
		static void RegTo()
		{
			TileEdGlobal.RegisterEditor(new ExcuteMap());
			_excuteTypes = typeof(ExcuteMap)
				.Assembly
				.GetTypes()
				.Where(t => typeof(IMapExcute).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract)
				.Select(t => System.Activator.CreateInstance(t, true) as IMapExcute).ToList();
		}

		#region Show
		public override bool IsBrushTool()
		{
			return false;
		}

		public override bool CheckCanShow()
		{
			return false;
		}

		public override int PaletteOrder()
		{
			return 0;
		}

		public override string PaletteName()
		{
			return "Build";
		}

		public override void OnSettingsFocus(EditorWindow ed)
		{
			base.OnSettingsFocus(ed);
			Load();
		}


		#endregion

		#region Member

		const string c_cfg_file = "mapbuild.cfg";

		private bool _newScene;

		private BuildConfig config;
		private System.Action command;

		private bool newBool = false;
		private bool exportBool = false;
		private bool excuteBool = false;

		#endregion

		#region UI

		public override void OnSettingsGUI(EditorWindow ed)
		{
			if (config == null) Load();
			EditorGUIUtility.labelWidth = 150f;

			newBool = GUIHelp.DrawFadeOut(newBool, "新建场景", DrawNew);
			EditorGUILayout.Space();
			exportBool = GUIHelp.DrawFadeOut(exportBool, "导出场景", DrawExport);

		}

		private void DrawNew(bool state)
		{
			if (state)
			{
				EditorGUI.BeginChangeCheck();
				config.newSceneName = GUIHelp.DrawObject(config.newSceneName, "新场景名");
				GUIHelp.DrawFolderSelect("新场景存储文件夹", ref config.newSceneOutput);
				GUIHelp.GUI_DH(() =>
				{
					GUIHelp.DrawFileSelect("基础场景", ref config.bgScene);
					if (GUILayout.Button("X")) config.bgScene = default;
				});
				if (EditorGUI.EndChangeCheck()) Save();
				if (GUILayout.Button("新建场景"))
				{
					EditorApplication.delayCall -= DoCreateScene;
					EditorApplication.delayCall += DoCreateScene;
				}
			}
		}

		private void DrawExport(bool state)
		{
			if (state)
			{
				EditorGUI.BeginChangeCheck();
				config.debug = GUIHelp.DrawObject(config.debug, "debug");
				config.dontRemoveInter = GUIHelp.DrawObject(config.dontRemoveInter, "不隐藏交互层");

				GUIHelp.DrawFolderSelect("场景导出文件夹", ref config.sceneouput);
				config.enableCombineMesh = GUIHelp.DrawObject(config.enableCombineMesh, "允许合并网格");
				if (config.enableCombineMesh)
					GUIHelp.DrawFolderSelect("合并网格导出文件夹", ref config.meshouput);
				if (EditorGUI.EndChangeCheck()) Save();
				EditorGUILayout.Space();
				GUIHelp.GUI_DH(() =>
				{
					excuteBool = GUIHelp.DrawFadeOut(excuteBool, "其他附加处理", DrawMapExcute);
				}, GUI.skin.box);
				EditorGUILayout.Space();
				if (GUILayout.Button("导出"))
				{
					EditorApplication.delayCall -= DoBuild;
					EditorApplication.delayCall += DoBuild;
				}
			}
		}

		private void DrawMapExcute(bool s)
		{
			if (s && _excuteTypes != null)
			{
				EditorGUILayout.BeginVertical(GUI.skin.box);
				for (int i = 0; i < _excuteTypes.Count; i++)
				{
					var ex = _excuteTypes[i];
					if (ex != null)
						ex.fade = GUIHelp.DrawFadeOut(ex.fade, ex.title ?? "excute_" + i, ex.OnGUI);
					EditorGUILayout.Space();
				}
				EditorGUILayout.EndVertical();
			}
		}

		private void DoCreateScene()
		{
			string bg = null;
			if (string.IsNullOrEmpty(config.newSceneName)) { Debug.LogError("没有指定场景名"); return; }
			if (string.IsNullOrEmpty(config.newSceneOutput)) { Debug.LogError("没有指定存储文件夹"); return; }
			var scenePath = config.newSceneOutput + "/" + config.newSceneName + ".unity";

			if (File.Exists(scenePath)) { Debug.LogError("该文件夹下存在同名场景"); return; }
			if (!string.IsNullOrEmpty(config.bgScene))
			{
				bg = config.bgScene;
				if (!File.Exists(bg))
				{
					Debug.Log("基础背景场景不存在，忽略该选项!!!");
					bg = null;
				}
			}

			var needNew = false;
			if (bg != null)
			{
				var scene = EditorSceneManager.OpenScene(bg, OpenSceneMode.Single);
				if (!scene.IsValid())
				{
					Debug.Log($"{bg}场景无效,新建空场景");
					needNew = true;
				}
				else if (!EditorSceneManager.SaveScene(scene, scenePath, true)) needNew = true;
				else
				{
					AssetDatabase.Refresh();
					EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);
					TileEdGlobal.CreateNewMap(null);
					EditorSceneManager.SaveScene(scene, scenePath);
				}
			}
			else needNew = true;

			if (needNew)
			{
				var scene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
				TileEdGlobal.CreateNewMap(null);
				EditorSceneManager.SaveScene(scene, scenePath);
				AssetDatabase.Refresh();
			}
		}

		private void DoBuild()
		{
			var map = TileEd.TileEd.Instance == null ? GameObject.FindAnyObjectByType<plyLib.TileEdMap>() : TileEd.TileEd.Instance.mapObj;
			if (map == null)
			{

				Debug.Log("当前场景没有刷格子，不需要导出");
				return;
			}
			var mapasset = plyEdUtil.LoadAsset<TileEdMapAsset>(TileEdGlobal.MAPS_PATH + map.ident + ".asset");
			if (mapasset == null)
			{
				Debug.Log("场景有问题，没有找到对应的Map资源：" + map.ident);
				return;
			}
			NewScene();

		}

		private void NewScene()
		{
			var scene = EditorSceneManager.GetActiveScene();
			if (scene.IsValid())
			{
				EditorSceneManager.SaveOpenScenes();
				var newName = scene.name.ToLower() + "_marked.unity";
				var dir = System.IO.Path.Combine(string.IsNullOrEmpty(config.sceneouput) ? System.IO.Path.GetDirectoryName(scene.path) : config.sceneouput, "markeds");
				if (!System.IO.Directory.Exists(dir))
					System.IO.Directory.CreateDirectory(dir);
				var path = System.IO.Path.Combine(dir, newName);
				EditorSceneManager.SaveScene(scene, path);
				AssetDatabase.Refresh();
				EditorSceneManager.OpenScene(path);
				EditorApplication.delayCall += ExcuteTiles;
			}
		}

		private void ExcuteTiles()
		{
			var maps = GameObject.FindObjectsByType<TileEdMap>(FindObjectsInactive.Exclude, FindObjectsSortMode.InstanceID);
			if (maps != null && maps.Length > 0)
			{
				for (int i = 0; i < maps.Length; i++)
				{
					var map = maps[i];
					var mapasset = plyEdUtil.LoadAsset<TileEdMapAsset>(TileEdGlobal.MAPS_PATH + map.ident + ".asset");
					if (mapasset != null)
					{
						Excute(map, mapasset);
						AfterExcute(map, mapasset);
					}
					else
						Debug.Log("场景有问题，没有找到对应的Map资源：" + map.ident);
				}
			}
			AfterExcute(maps);
		}

		private void AfterExcute(TileEdMap[] maps)
		{
			if (maps != null && maps.Length > 0) { foreach (TileEdMap map in maps) GameObject.DestroyImmediate(map.gameObject); }
			_excuteTypes?.Where(e => e is IAfterExcute)?.ToList()?.ForEach(e => e.Excute(null, null));

			GameObject.FindAnyObjectByType<Camera>()?.gameObject?.SetActive(false);
			var light = GameObject.FindAnyObjectByType<Light>();
			if (light) light.lightmapBakeType = LightmapBakeType.Mixed;

			Debug.Log("Begin baking!!!!!!");
			/*Lightmapping.bakeCompleted -= WaitBake;
			Lightmapping.bakeCompleted += WaitBake;*/
			Lightmapping.Bake();
			WaitBake();
		}

		private void WaitBake()
		{
			EditorSceneManager.MarkAllScenesDirty();
			EditorSceneManager.SaveOpenScenes();
			AssetDatabase.Refresh();
			Debug.Log("End baking!!!!!!");
		}

		private void Excute(plyLib.TileEdMap map, TileEdMapAsset mapasset)
		{
			var rect = mapasset.GetGroup(0).GetMinMaxGridPosition();
			var csize = mapasset.GetGroup(0).tileSize;
			var go = new GameObject("grid_" + map.name);
			var grid = go.AddComponent<GameTools.Maps.Grid>();
			grid.size = csize;
			grid.Create(new Vector2Int(rect[0].x, rect[0].z), new Vector2Int(rect[1].x, rect[1].z));
			var points = map.transform.GetComponentsInChildren<Transform>().Where(t => t.name.StartsWith("(")).ToList();
			foreach (var item in points)
			{
				if (item.name.StartsWith("__")) continue;
				var p = new IntVector3(item.name);
				var cell = grid.GetCell(p.x, p.z);
				if (cell == null) continue;

				var point = go.transform.Find(item.name);
				if (point == null)
				{
					point = new GameObject(item.name).transform;
					point.SetParent(go.transform);
					point.position = item.transform.position;
					if (cell != null)
						cell.cell = point.gameObject;
				}

				var data = item.GetComponent<DataBinder>();
				if (data != null)
				{
					bool isDummy = data.dataSet.GetValByPath("dummy");
					bool isplatform = data.dataSet;
					string tag = isDummy ? data.dataSet.GetValByPath("itemid") : null;
					string uname = data.dataSet.i_val.ToString();

					if (!isDummy || config.debug)
					{
						var parent = point;
						if (!isDummy)
						{
							parent = point.Find("build");
							if (!parent)
							{
								parent = new GameObject("build").transform;
								parent.SetParent(point, false);
							}
						}
						var child = GameObject.Instantiate(item.gameObject, parent);
						var flag = child.transform.Find("__flag");
						child.transform.position = item.transform.position;
						child.name = tag ?? uname ?? "body";
						var body = child.transform.Find("body");
						if (body)
						{
							if (isDummy && !config.debug)
								GameObject.DestroyImmediate(body.gameObject);
							if (!isDummy && !isplatform && (!config.debug && !config.dontRemoveInter))
								GameObject.DestroyImmediate(body.gameObject);
						}

						if (flag) GameObject.DestroyImmediate(flag.gameObject);
						GameObject.DestroyImmediate(child.GetComponent<plyLib.plyIdent>());
						GameObject.DestroyImmediate(child.GetComponent<DataBinder>());
					}

					if (cell != null)
					{
						cell.flag = true;

						if (isDummy)
						{
							bool walk = data.dataSet.GetValByPath("data.walkable");
							int cost = data.dataSet.GetValByPath("data.walkcost");
							if (cell.walkcost >= 0 && walk)
								cell.walkcost = Mathf.Max(cell.walkcost, cost);
							else if (!walk)
								cell.walkcost = -1;

							string relex = data.dataSet.GetVal("relex");
							if (!string.IsNullOrEmpty(relex))
							{
								var rs = relex.Split('x');
								if (rs.Length > 4)
									cell.relex = new int[] { int.Parse(rs[0]), int.Parse(rs[2]), int.Parse(rs[3]), int.Parse(rs[4]) };
							}

						}

						if (!string.IsNullOrEmpty(tag))
						{
							cell.cdatas.Add(GameTools.Maps.CellData.From(0, tag, data.dataSet));
						}
						else
						{
							cell.cdatas.Add(GameTools.Maps.CellData.From(1, uname, data.dataSet));
						}

					}
				}
				else
				{
					var child = GameObject.Instantiate(item.gameObject, point);
					child.transform.localPosition = Vector3.zero;
					GameObject.DestroyImmediate(child.GetComponent<plyLib.plyIdent>());
					child.name = "body";
				}
			}
			grid.Refresh();
			grid.cells.RemoveAll(c => !c.flag);
			try
			{
				foreach (var item in _excuteTypes)
				{
					if (!(item is IAfterExcute)) item.Excute(go, map);
				}
			}
			catch (System.Exception e)
			{
				Debug.LogException(e);
			}
		}

		private void AfterExcute(TileEdMap map, TileEdMapAsset mapasset)
		{

		}

		private void Save()
		{
			File.WriteAllText(c_cfg_file, JsonUtility.ToJson(config ?? new BuildConfig()));
		}

		private void Load()
		{
			if (!File.Exists(c_cfg_file))
				File.WriteAllText(c_cfg_file, "", Encoding.UTF8);
			config = JsonUtility.FromJson<BuildConfig>(File.ReadAllText(c_cfg_file, Encoding.UTF8)) ?? new BuildConfig();
		}

		#endregion

	}

}