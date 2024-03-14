using System.Collections;
using System.Collections.Generic;
using plyLib;
using UnityEngine;

public class GridMapExcute_ImportMap : TileEdExt.IMapExcute
{

	private TileEd.TileEdMapAsset tileEdMapAsset;

	public string title => "���볡��";

	public bool fade { get; set; }

	public void OnGUI(bool show)
	{
		if (show)
		{
			tileEdMapAsset = GUIHelp.DrawObject(tileEdMapAsset, "��Դ�ļ�");
			if (GUIHelp.DrawButton("����" , null)) {
			
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
