using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delay : MonoBehaviour
{
	public float time;

	void Start()
	{
		StartCoroutine(DelayExcute());
	}

	IEnumerator DelayExcute()
	{

		yield return new WaitForSeconds(time);
		Destroy(gameObject);
	}

}
