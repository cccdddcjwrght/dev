using System.Collections;
using System.Collections.Generic;
using plyLib;
using UnityEngine;

public class GridMapExcute_ImportMap : TileEdExt.IMapExcute
{

	private TileEd.TileEdMapAsset tileEdMapAsset;

	public string title => "导入场景";

	public bool fade { get; set; }

	public void OnGUI(bool show)
	{
		if (show)
		{
			tileEdMapAsset = GUIHelp.DrawObject(tileEdMapAsset, "资源文件");
			if (GUIHelp.DrawButton("导入" , null)) {
			
				if(tileEdMapAsset != null)
				{
					TileEd.TileEdGlobal.CreateNewMap(tileEdMapAsset);
				}

			}
		}
	}

	public void Excute(GameObject go, TileEdMap map)
	{
	}
}
