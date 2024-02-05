using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;
using UnityEngine;

public class FBSDK : MonoBehaviour
{
	private void Awake()
	{
		if (FB.IsInitialized)
		{
			FB.ActivateApp();
		}
		else
		{
			//Handle FB.Init
			FB.Init(() => {
				FB.ActivateApp();
			});
		}
	}


	void OnApplicationPause(bool pauseStatus)
	{
		if (!pauseStatus)
		{
			//app resume
			if (FB.IsInitialized)
			{
				FB.ActivateApp();
			}
			else
			{
				//Handle FB.Init
				FB.Init(() => {
					FB.ActivateApp();
				});
			}
		}
	}
}
