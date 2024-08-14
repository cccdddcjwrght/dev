using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SDK
{
	public partial class SDKProxy
	{
		public static IEnumerator Init()
		{
			if (false)
				yield return null;
#if CHECK || !SVR_RELEASE
			Debug.Log("SDK Init start:" + Time.realtimeSinceStartup);
#endif
#if !TD_OFF || TD_ON
			//Всµг
			new SDK.TDSDK.ThinkDataSDK().StartRun(GameConfigs.GlobalConfig.GetStr);
#endif

#if USE_THIRD_SDK
			SDK.THSDK.THSdk.Instance.GetInstanceID();
#endif
#if CHECK || !SVR_RELEASE
			Debug.Log("SDK Init end:" + Time.realtimeSinceStartup);
#endif
		}
	}
}
