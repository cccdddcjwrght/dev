#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// 变体收集器
public class ShaderVarCollect : MonoBehaviour
{
	const int TaskNum = 20;

	public List<Object> target;
	public float interval = 2f;

	void Start()
	{
		StartCoroutine(UpdateTask());
	}

	IEnumerator UpdateTask()
	{

		if (target != null && target.Count > 0)
		{

			while (target.Count > 0)
			{
				var t = target[0];
				target.RemoveAt(0);
				(Instantiate(t) as GameObject)?.SetActive(true);
				yield return new WaitForSeconds(interval);
			}

			EditorApplication.isPlaying = false;

			yield break;
		}



		string[] prefabAssets = System.IO.Directory.GetFiles("Assets/", "*.prefab", System.IO.SearchOption.AllDirectories);

		yield return null;

		List<Object> objs = new List<Object>();
		int sec = Mathf.CeilToInt(prefabAssets.Length / TaskNum);
		for (int secIndex = 0; secIndex < sec; ++secIndex)
		{
			foreach (var obj in objs)
			{
				Destroy(obj);
			}
			objs.Clear();

			var baseIndex = secIndex * TaskNum;
			for (int i = 0; i < TaskNum; ++i)
			{
				var currIndex = baseIndex + i;
				if (currIndex < prefabAssets.Length)
				{
					var tobj = AssetDatabase.LoadAssetAtPath(prefabAssets[currIndex], typeof(UnityEngine.Object));
					if (tobj != null)
					{
						objs.Add(Instantiate(tobj));
					}
				}
			}

			yield return null;
			yield return null;
		}

		foreach (var obj in objs)
		{
			Destroy(obj);
		}
		objs.Clear();


		string[] matList = System.IO.Directory.GetFiles("Assets/", "*.mat", System.IO.SearchOption.AllDirectories);

		yield return null;

		List<MeshRenderer> spheres = new List<MeshRenderer>();

		for (int i = 0; i < TaskNum; ++i)
		{
			spheres.Add(GameObject.CreatePrimitive(PrimitiveType.Sphere).GetComponent<MeshRenderer>());
		}

		sec = Mathf.CeilToInt(matList.Length / TaskNum);
		for (int secIndex = 0; secIndex < sec; ++secIndex)
		{
			var baseIndex = secIndex * TaskNum;
			for (int i = 0; i < TaskNum; ++i)
			{
				var currIndex = baseIndex + i;
				if (currIndex < matList.Length)
				{
					var tobj = AssetDatabase.LoadAssetAtPath(matList[currIndex], typeof(UnityEngine.Material)) as UnityEngine.Material;
					if (tobj != null)
					{
						spheres[i].sharedMaterial = tobj;
					}
				}
			}

			yield return null;
			yield return null;
		}
	}
}

#endif