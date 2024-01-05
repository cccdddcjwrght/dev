using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMapExcute_AddAgent : TileEdExt.IMapExcute
{
	public string title => "添加地图寻路代理";

	public bool fade { get; set; }

	public bool addTestModel;
	public GameObject testModel;

	public void OnGUI(bool show)
	{
		if (!show) return;
		addTestModel = GUIHelp.DrawObject(addTestModel, "添加测试模型");
		if (addTestModel)
			testModel = GUIHelp.DrawObject(testModel, "模型预制");
	}

	public void Excute(GameObject go)
	{
		if (go)
			go.AddComponent<GameTools.MapAgent>();
		if (addTestModel && testModel)
			GameObject.Instantiate(testModel);
	}
}

