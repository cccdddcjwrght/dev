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

namespace TileEdExt
{

	public interface IMapExcute
	{
		string title { get; }
		bool fade { get; set; }
		void OnGUI(bool show);
		void Excute(GameObject go);
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

			newBool = GUIHelp.DrawFadeOut(newBool, "�½�����", DrawNew);
			EditorGUILayout.Space();
			exportBool = GUIHelp.DrawFadeOut(exportBool, "��������", DrawExport);

		}

		private void DrawNew(bool state)
		{
			if (state)
			{
				EditorGUI.BeginChangeCheck();
				config.newSceneName = GUIHelp.DrawObject(config.newSceneName, "�³�����");
				GUIHelp.DrawFolderSelect("�³����洢�ļ���", ref config.newSceneOutput);
				GUIHelp.GUI_DH(() =>
				{
					GUIHelp.DrawFileSelect("��������", ref config.bgScene);
					if (GUILayout.Button("X")) config.bgScene = default;
				});
				if (EditorGUI.EndChangeCheck()) Save();
				if (GUILayout.Button("�½�����"))
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
				config.dontRemoveInter = GUIHelp.DrawObject(config.dontRemoveInter, "�����ؽ�����");

				GUIHelp.DrawFolderSelect("���������ļ���", ref config.sceneouput);
				config.enableCombineMesh = GUIHelp.DrawObject(config.enableCombineMesh, "����ϲ�����");
				if (config.enableCombineMesh)
					GUIHelp.DrawFolderSelect("�ϲ����񵼳��ļ���", ref config.meshouput);
				if (EditorGUI.EndChangeCheck()) Save();
				EditorGUILayout.Space();
				GUIHelp.GUI_DH(() =>
				{
					excuteBool = GUIHelp.DrawFadeOut(excuteBool, "�������Ӵ���", DrawMapExcute);
				}, GUI.skin.box);
				EditorGUILayout.Space();
				if (GUILayout.Button("����"))
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
				EditorGUILayout.BeginHorizontal(GUI.skin.box);
				for (int i = 0; i < _excuteTypes.Count; i++)
				{
					var ex = _excuteTypes[i];
					if (ex != null)
						ex.fade = GUIHelp.DrawFadeOut(ex.fade, ex.title ?? "excute_" + i, ex.OnGUI);
					EditorGUILayout.Space();
				}
				EditorGUILayout.EndHorizontal();
			}
		}

		private void DoCreateScene()
		{
			string bg = null;
			if (string.IsNullOrEmpty(config.newSceneName)) { Debug.LogError("û��ָ��������"); return; }
			if (string.IsNullOrEmpty(config.newSceneOutput)) { Debug.LogError("û��ָ���洢�ļ���"); return; }
			var scenePath = config.newSceneOutput + "/" + config.newSceneName + ".unity";

			if (File.Exists(scenePath)) { Debug.LogError("���ļ����´���ͬ������"); return; }
			if (!string.IsNullOrEmpty(config.bgScene))
			{
				bg = config.bgScene;
				if (!File.Exists(bg))
				{
					Debug.Log("�����������������ڣ����Ը�ѡ��!!!");
					bg = null;
				}
			}

			var needNew = false;
			if (bg != null)
			{
				var scene = EditorSceneManager.OpenScene(bg, OpenSceneMode.Single);
				if (!scene.IsValid())
				{
					Debug.Log($"{bg}������Ч,�½��ճ���");
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

				Debug.Log("��ǰ����û��ˢ���ӣ�����Ҫ����");
				return;
			}
			var mapasset = plyEdUtil.LoadAsset<TileEdMapAsset>(TileEdGlobal.MAPS_PATH + map.ident + ".asset");
			if (mapasset == null)
			{
				Debug.Log("���������⣬û���ҵ���Ӧ��Map��Դ��" + map.ident);
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
				var newName = scene.name + "_marked.unity";
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
						Debug.Log("���������⣬û���ҵ���Ӧ��Map��Դ��" + map.ident);
				}
			}
			AfterExcute(maps);
		}

		private void AfterExcute(TileEdMap[] maps)
		{
			if (maps != null && maps.Length > 0) { foreach (TileEdMap map in maps) GameObject.DestroyImmediate(map.gameObject); }

			GameObject.FindAnyObjectByType<Camera>()?.gameObject?.SetActive(false);
			Lightmapping.Bake();
			EditorSceneManager.MarkAllScenesDirty();
			EditorSceneManager.SaveOpenScenes();
			AssetDatabase.Refresh();
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
					{
						cell.cell = point.gameObject;
						cell.pos = point.position;
					}
					cell.name = item.name;
				}
				var data = item.GetComponent<DataBinder>();
				var tile = default(TileEdMapTile);
				if (data != null)
				{
					bool isDummy = data.dataSet.GetValByPath("dummy");
					bool isplatform = data.dataSet;
					string tag = isDummy ? data.dataSet.GetValByPath("itemid") : null;
					//string uname = (string)data.dataSet.GetValByPath("setid") + "_" + (int)data.dataSet.GetValByPath("itemid");
					string uname = data.dataSet.i_val.ToString();

					if (!isDummy || config.debug)
					{
						var child = GameObject.Instantiate(item.gameObject, point);
						var flag = child.transform.Find("__flag");

						child.transform.position = item.transform.position;
						if (flag) GameObject.DestroyImmediate(flag.gameObject);
						GameObject.DestroyImmediate(child.GetComponent<plyLib.plyIdent>());
						child.name = tag ?? uname ?? "body";
						var body = child.transform.Find("body");
						if (body)
						{
							if (isDummy && !config.debug)
								GameObject.DestroyImmediate(body.gameObject);
							if (!isDummy && !isplatform && (!config.debug && !config.dontRemoveInter))
								GameObject.DestroyImmediate(body.gameObject);
						}
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
						}

						if (!string.IsNullOrEmpty(tag))
						{
							cell.tags.Add(tag);
							cell.data.SetVal(data.dataSet, tag);

						}
						else
						{
							cell.builds.Add(uname);
							cell.data.SetVal(data.dataSet, uname);
						}

						if (isDummy)
						{
							string relex = data.dataSet.GetVal("relex");
							if (!string.IsNullOrEmpty(relex))
							{
								var rs = relex.Split('x');
								if (rs.Length > 4)
									cell.relex = new int[] { int.Parse(rs[0]), int.Parse(rs[2]), int.Parse(rs[3]), int.Parse(rs[4]) };
							}
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
			try
			{
				_excuteTypes?.ForEach(e => e.Excute(go));
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