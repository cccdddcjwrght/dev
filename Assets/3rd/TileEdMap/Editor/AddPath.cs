using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Jacovone;
using System.Linq;

public class AddPath
{
	[MenuItem("GameObject/·��/����·��")]
	static public void CreatePath(MenuCommand command)
	{
		var select = Selection.activeObject as GameObject;
		if (select != null)
		{
			var pn = select.name + "_path";
			var p = select.transform.parent ?? select.transform;
			var path = p.Find(pn);
			var panel = GameObject.Find("__panel");
			var road = default(PathMagic);
			if (path != null)
			{
				if (EditorUtility.DisplayDialog("��ʾ", "����һ�����ڵ�ǰ�����·�����Ƿ�ɾ��", "ɾ��", "�޸�"))
				{
					GameObject.DestroyImmediate(path.gameObject);
					path = null;
				}
			}
			if (path == null)
			{
				PathMagicEditor.CreateNewPathMagic(command);
				path = (Selection.activeObject as GameObject).transform;
				road = path.GetComponent<PathMagic>();
				if (road != null)
				{
					road.autoStart = true;
					road.globalFollowPath = true;
					road.velocityBias = 0.3f;
					//road.loop = true;
					//road.presampledPath = true;
					//road.samplesNum = 50;
					//road.Target = select.transform;
				}
			}
			if (path != null)
			{
				path.parent = p;
				path.name = pn;
				road = road ?? path.GetComponent<PathMagic>();
				if (road == null)
				{
					Debug.LogError("·������ʧ��");
					return;
				}
				Waypoint wp1 = new Waypoint();
				wp1.Position = Vector3.zero;
				wp1.Rotation = select.transform.rotation.eulerAngles;
				road.waypoints = new Waypoint[] { wp1 };
			}

			if (panel == null)
			{
				panel = new GameObject("__panel");
				var box = panel.AddComponent<BoxCollider>();
				box.size = new Vector3(1000, 0.5f, 1000);
			}
			var bp = select.transform.position;
			bp.y -= 0.25f;
			panel.transform.position = bp;
		}

	}

	[MenuItem("GameObject/·��/���·������")]
	static public void MarkReturnPath(MenuCommand command)
	{

		var select = Selection.activeObject as GameObject;
		if (select != null && select.transform.parent != null)
		{

			var pn = select.name + "_path";
			var p = select.transform.parent;
			var path = p.Find(pn);
			if (path == null)
			{
				if (!EditorUtility.DisplayDialog("��ʾ", "��ǰ����û��·�����Ƿ��ȴ���һ��·��", "��", "��"))
					return;
				CreatePath(command);
			}

			path = p.Find(pn);
			if (path == null)
				return;

			var road = path.GetComponent<PathMagic>();
			if (road != null)
			{
				var ways = road.waypoints;
				if (ways != null && ways.Length > 0)
				{
					var rets = new List<Waypoint>(ways);
					rets.AddRange(ways.Reverse());
					road.Waypoints = rets.ToArray();
					AssetDatabase.SaveAssets();
				}
			}
		}
	}


}
