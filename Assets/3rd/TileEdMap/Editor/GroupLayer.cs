using System;
using System.Collections.Generic;
using System.Linq;
using plyLibEditor;
using TileEd;
using UnityEditor;
using UnityEngine;

namespace TileEdExt
{

	internal class GroupLayer : TileEd.TileEd_Ed_Common<GroupLayerAssets>
	{
		[InitializeOnLoadMethod]
		static void RegTo()
		{
			TileEdGlobal.RegisterEditor(new GroupLayer());
		}

		protected override void OnAfterLoad()
		{
			var ls = TileEdGlobal.DefineGroupNames.ToList();
			TileEdGlobal.DefineGroupNames = ls.Union(asset.tileSets.Select(t => t.name)).ToArray();
		}

		public override bool CheckCanShow()
		{
			return false;
		}

		protected override void DrawSetSettings()
		{
			var sets = asset.tileSets[tileSetIdx];
			if (sets != null)
			{
				GUILayout.BeginVertical();
				EditorGUILayout.LabelField("ident：", sets.ident.ToString());
				EditorGUILayout.LabelField("名：", sets.name);
				GUILayout.EndHorizontal();
			}
		}

		protected override bool CheckAddSets(string name)
		{
			if (TileEdGlobal.DefineGroupNames.Contains(name))
			{
				Debug.Log("层级列表包含相同层");
				return false;
			}
			if (asset.tileSets.FindIndex(t => t.name == name) > -1)
			{
				Debug.Log("已经添加了相同名字的层");
				return false;
			}

			var ls = TileEdGlobal.DefineGroupNames.ToList();
			ls.Add(name);
			TileEdGlobal.DefineGroupNames = ls.ToArray();
			return true;
		}

		protected override void OnDeleteSets(TileEdSet set)
		{
			if (set != null)
			{
				var ls = TileEdGlobal.DefineGroupNames.ToList();
				ls.Remove(set.name);
				TileEdGlobal.DefineGroupNames = ls.ToArray();
			}
		}

		protected override void DoRenameSet(plyTextInputWiz wiz)
		{
			if (tileSetIdx >= 0 && wiz != null && !string.IsNullOrEmpty(wiz.text))
			{
				var oldName = asset.tileSets[tileSetIdx].name;
				var idx = Array.IndexOf(TileEdGlobal.DefineGroupNames, oldName);
				TileEdGlobal.DefineGroupNames[idx] = wiz.text;
			}
			base.DoRenameSet(wiz);

		}

		public override bool IsBrushTool()
		{
			return false;
		}

		public override void OnInspector(UnityEditor.Editor inspectorEd)
		{
		}

		public override int PaletteOrder()
		{
			return 0;
		}

	}
}
