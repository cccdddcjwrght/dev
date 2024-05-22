using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepLive : MonoBehaviour
{
	private void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}
}
