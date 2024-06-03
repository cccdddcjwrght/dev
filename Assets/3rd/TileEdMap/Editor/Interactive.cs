using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataSets;
using plyLib;
using plyLibEditor;
using TileEd;
using UnityEditor;
using UnityEngine;

namespace TileEdExt
{

	internal class Interactive : TileEd_Ed_Common<InteractiveAssets>
	{
		static private GUIStyle toggleStyle;
		static private GUIStyle selectStyle;
		static private Color normal;

		static private TileEdItem[] items;
		static private string[] names;
		static private int selectItem;


		[InitializeOnLoadMethod]
		static void RegTo()
		{
			TileEdGlobal.RegisterEditor(new Interactive());
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

		protected override void OnDrawPopupSetting(Rect rect, TileED_Common_SettingsPopup popup)
		{
			var areas = popup.tile.obstacles;

			if (toggleStyle == null)
			{
				toggleStyle = new GUIStyle(GUI.skin.button)
				{
					fixedWidth = 20,
					fixedHeight = 20,
					alignment = TextAnchor.MiddleCenter,
				};
			}

			if (items == null)
			{
				items = GetItems()?.ToArray();
				names = items.Select(t => t.name + "-" + t.desc).ToArray();
			}
			EditorGUIUtility.labelWidth = 75;

			EditorGUILayout.Space();
			popup.tile.name = EditorGUILayout.TextField("名：", popup.tile.name);
			selectItem = Mathf.Max(0, System.Array.FindIndex(items, (i) => (i.uniqid == 0 ? i.name.GetHashCode() : i.uniqid) == popup.tile.obsid));
			var change = EditorGUILayout.Popup("扩展区域:", selectItem, names);
			if (change != selectItem)
			{
				selectItem = change;
				var select = items[selectItem];
				if (select.uniqid == 0) select.uniqid = select.name.GetHashCode();
				popup.tile.obsid = select.uniqid;
			}

			//popup.tile.dataSet.SetVal(EditorGUILayout.Toggle("默认不显示:", popup.tile.dataSet.GetVal("isplatform")) , "isplatform");

			normal = GUI.color;
			EditorGUILayout.Space();
			EditorGUILayout.LabelField("区域覆盖:");
			EditorGUILayout.BeginVertical(GUI.skin.box);

			var size = TileEdGlobal.MaxExpendSize;
			var len = size * 2 + 1;
			var al = len * len;
			areas = areas.Count != al ? new List<int>(new int[al]) : areas;


			#region Obs
			//var lv = popup.tile.dataSet.GetVal("bindlv" + k);
			//if (GUILayout.Button(lv == null || lv.i_val < 0 ? "" : lv.i_val.ToString(), toggleStyle, GUILayout.Width(30), GUILayout.Height(30)))
			//{
			//	lv = lv ?? new DataSet() { i_val = -1 };
			//	popup.tile.dataSet.SetVal(lv, "bindlv" + k);
			//	lv.i_val += 1;
			//	if (lv.i_val > 10) lv.i_val = -1;
			//	v = lv.i_val >= 0 ? popup.tile.obsid : 0;
			//	areas[k] = v;
			//} 
			#endregion

			for (int i = size; i >= -size; i--)
			{
				EditorGUILayout.BeginHorizontal(GUILayout.Width(22));
				for (int j = -size; j <= size; j++)
				{
					var p = i * len + j;
					var k = p + (al - 1) / 2;
					var v = areas[k];
					if (p == 0) GUI.color = Color.green;
					else GUI.color = normal;
					if (GUILayout.Button(v == 0 ? "" : "*", toggleStyle, GUILayout.Width(20), GUILayout.Height(20)))
					{
						v = v == 0 ? popup.tile.obsid : 0;
						areas[k] = v;
					}
				}
				EditorGUILayout.EndHorizontal();
			}


			EditorGUILayout.EndVertical();
			popup.tile.obstacles = areas;
			GUI.color = normal;
		}

		protected override void OnPopupSetttingOK(TileED_Common_SettingsPopup popup)
		{
			TileEd.TileEd.grid.UpdatePreviewObject(null, 0, true);
		}

		protected override void OnPopupSettingClose(TileED_Common_SettingsPopup popup)
		{
			items = null;
		}

		protected override void OnNewTileToGroup(GameObject go, TileEdMapTile mapTile, TileEdItem tile, TileEdMapGroup group)
		{
			if (go)
			{

				if (tile.obsid != 0 && tile.obstacles?.Count > 0)
				{
					var grids = TileEd.TileEd.grid.GetExtendGrids()?.ToList();
					var items = GetItems();
					if (grids != null && grids.Count > 0 && items != null && items.Count > 0)
					{
						for (int i = grids.Count - 1; i >= 0; i--)
						{
							var item = grids[i];
							var oid = tile.obstacles[item.y];
							int lv = tile.dataSet.GetVal("bindlv" + item.y);

							item.y = 0;
							var ts = TileEdGlobal.GetTiles(item, "all");
							var obsTile = TileEdGlobal.GetTileItem(oid, out var tid, out var sid);
							if (ts != null && ts.Find(t => t.toolIdent == tid && t.data[_TileSetIdent] == sid && t.data[_TileIdent] == obsTile.ident) != null)
								continue;
							TileEdGlobal.RelexString = mapTile.gridPos.ToString() + mapTile.extraData[_eData_Ident].GetHashCode() + "x" + lv;
							TileEdGlobal.editors.ForEach(
								e => e.ed?.HandleTiles(obsTile, new IntVector3[] { item }, TileEdCursorMode.Normal, false, TileEd.TileEd.grid.mouseWorldPos)
							);

						}
						TileEdGlobal.RelexString = null;
					}
				}
			}
		}

		protected override void OnCreateTile(GameObject go, TileEdMapTile mapTile, TileEdItem tile, TileEdMapGroup group)
		{
			var pid = mapTile.data[_TileFabIdx];
			var data = go.GetComponent<DataBinder>().dataSet;

			if (mapTile.extraData.Length > 4)
				data.b_val = mapTile.extraData[4] == "true";

			data.SetVal(Mathf.Max(pid, 0), "level");
			if (pid >= 0)
			{
				var assets = data.GetValByPath("level");
				if (tile.prefab?.Length > 0)
				{
					for (int i = 0; i < tile.prefab.Length; i++)
					{
						var prefab = tile.prefab[i];
						var path = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(prefab));
						assets.SetVal(path, i.ToString());
					}
				}
			}
		}

		protected override void OnRemoveTile(TileEdMapTile tile)
		{
			if (tile != null)
			{

			}
		}

		protected override void AddItemToTileSet(TileEdItem item)
		{
			item.name = item.prefab?.Length > 0 ? item.prefab[0].name : null;
			base.AddItemToTileSet(item);
		}

		public override bool CanGroupCombine()
		{
			return false;
		}

		protected List<TileEdItem> GetItems()
		{
			return TileEdGlobal.editors.Select(e => e.ed.GetAssets<TileEdSetAssets>())
					.Where(d => d != null && d.tileSets?.Count > 0)
					.SelectMany(e => e.tileSets?.SelectMany(t => t.tiles))
					.Where(t => !string.IsNullOrEmpty(t.name))
					.ToList();
		}

	}
}
