using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delay : MonoBehaviour
{
	private float delay = 1;

	public float time;


	public bool checkui = true;

	void Start()
	{
		StartCoroutine(DelayExcute());
	}

	IEnumerator DelayExcute()
	{

		yield return new WaitForSeconds(time + delay);
		if (checkui)
		{
			while (FairyGUI.GRoot.inst.GetChildren()?.Length <= 0)
				yield return null;
		}
		Destroy(gameObject);
	}

}
