using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMapExcute_AddAgent : TileEdExt.IMapExcute
{
	public string title => "��ӵ�ͼѰ·����";

	public bool fade { get; set; }

	public bool addTestModel;
	public GameObject testModel;

	public void OnGUI(bool show)
	{
		if (!show) return;
		addTestModel = GUIHelp.DrawObject(addTestModel, "��Ӳ���ģ��");
		if (addTestModel)
			testModel = GUIHelp.DrawObject(testModel, "ģ��Ԥ��");
	}

	public void Excute(GameObject go)
	{
		if (go)
			go.AddComponent<GameTools.MapAgent>();
		if (addTestModel && testModel)
			GameObject.Instantiate(testModel);
	}
}

