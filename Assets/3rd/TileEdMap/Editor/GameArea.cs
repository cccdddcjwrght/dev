using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataSets;
using plyLib;
using TileEd;
using UnityEditor;
using UnityEngine;

namespace TileEdExt
{

	public class AreaSetItemProxy : TileEd.TileItemProxy<AreaSetItemProxy>
	{
		public bool walkable { get { return _data.dataSet.GetVal(); } set { _data.dataSet.SetVal(value); } }
		public int walkcost { get { return _data.dataSet.GetVal(); } set { _data.dataSet.SetVal(value); } }
	}

	internal class GameArea : TileEd.TileEd_Ed_Common<GameAreaAssets>
	{
		[UnityEditor.InitializeOnLoadMethod]
		static void RegTo()
		{
			TileEdGlobal.RegisterEditor(new GameArea());
		}

		private static GUIStyle _label = null;

		public static GUIStyle G_Label
		{
			get
			{
				if (_label == null)
				{
					_label = new GUIStyle(GUI.skin.label)
					{
						fontSize = 15,
						fontStyle = FontStyle.Bold,
						stretchWidth = true,
						normal = new GUIStyleState()
						{
							textColor = Color.black,
						}
					};
				}
				return _label;
			}
		}

		private GUIContent _key_title = new GUIContent("区域ID", "区域ID");
		private GUIContent _name_title = new GUIContent("区域名", "区域命名");
		private GUIContent _walk_title = new GUIContent("是否可行走", "标记是否可以行走");
		private GUIContent _col_title = new GUIContent("颜色", "用来刷区域标识颜色");
		private GUIContent _layer_title = new GUIContent("层级", "区域显示层级");
		private GUIContent _mutex_title = new GUIContent("唯一", "和其他区域互斥");


		/// <summary>
		/// 画头
		/// </summary>
		/// <param name="ev"></param>
		protected override Rect DrawSetSettingHead(Event ev)
		{
			GUILayout.BeginVertical(TileEdGUI.Styles.DragDropBox, GUILayout.ExpandWidth(true));

			if (GUILayout.Button("添加"))
			{
				AddItemToTileSet(new TileEdItem());
			}
			GUILayout.EndVertical();
			return default;
		}

		protected override void DrawDummySettingUI(Event ev, int idx, GUIContent label, Texture2D icon, TileEdItem prop)
		{
			EditorGUIUtility.labelWidth = 80f;
			GUILayout.Space(18);
			var d = AreaSetItemProxy.Bind(prop);
			prop.name = EditorGUILayout.TextField(_key_title, prop.name);
			prop.desc = EditorGUILayout.TextField(_name_title, prop.desc);
			prop.color = EditorGUILayout.ColorField(_col_title, prop.color);
			prop.sortOrder = EditorGUILayout.IntSlider(_layer_title, prop.sortOrder, -10, 10);
			prop.mutex = EditorGUILayout.Toggle(_mutex_title, prop.mutex);

			d.walkable = EditorGUILayout.Toggle(_walk_title, d.walkable);
			if (d.walkable)
				d.walkcost = EditorGUILayout.IntField("移动消耗", d.walkcost);
		}

		public override void OnInspector(UnityEditor.Editor inspectorEd)
		{
			var group = TileEd.TileEd.Instance.group;
			EditorGUIUtility.labelWidth = 100f;
			group.enableColor = EditorGUILayout.ToggleLeft("使用层级颜色:", group.enableColor);
			if (group.enableColor)
				group.color = EditorGUILayout.ColorField(new GUIContent("标记颜色:"), group.color);
			GUILayout.Space(5);
			DrawRemoveAllBtn();
		}

		protected override void DrawDummyTileItemUI(Event ev, TileEdItem prop, Rect r, Rect starR)
		{
			EditorGUI.ColorField(r, prop.color);
			GUI.Label(r, prop.desc ?? prop.name, G_Label);
		}

		protected override void SetDummyMatProperty(TileEdItem item, Material material)
		{
			if (material)
			{
				var col = item.color;
				col.a = Mathf.Max(0.5f, col.a);
				material.SetColor("_Tint", col);
			}
		}

		public override bool IsEnablePlaceTile(IntVector3 pos, List<TileEdMapTile> tiles, int chosenFabIdx)
		{
			if (tiles != null && tiles.Count > 0)
			{
				var cts = asset.tileSets[tileSetIdx];
				var ctile = asset.tileSets[tileSetIdx].tiles[currTileIdx];
				for (int i = 0; i < tiles.Count; i++)
				{
					var tile = tiles[i];
					if (tile != null)
					{
						if (tile.data[_TileSetIdent] == cts.ident &&
							tile.data[_TileIdent] == ctile.ident &&
							tile.data[_TileFabIdx] == chosenFabIdx)
						{
							return false;
						}
						var t = asset.FindTileSet(tile.data[_TileSetIdent])?.FindTile(tile.data[_TileIdent]);

						if (t != null && t.mutex)
						{
							Debug.Log($"当前地块唯一:{t.name}，请删除后再刷！");
							return false;
						}
					}
				}
			}

			return true;
		}

		public override bool CanGroupCombine()
		{
			return false;
		}

		protected override void OnCreateTile(GameObject go, TileEdMapTile mapTile, TileEdItem tile, TileEdMapGroup group)
		{
			var data = go.GetComponent<DataBinder>()?.dataSet;
			if (data != null)
			{
				var relex = mapTile.extraData[_eData_Relate];
				if (!string.IsNullOrEmpty(relex) && !(bool)tile.dataSet.GetVal("walkable"))
				{
					var cs = relex.Substring(1).Replace(')','x');
					data.SetVal(cs, "relex");
				}
			}
		}

	}
}
