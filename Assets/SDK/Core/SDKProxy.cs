using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SDK
{
	public partial class SDKProxy
	{
		public static IEnumerator Init()
		{

#if !TD_OFF || TD_ON
			//Всµг
			new SDK.TDSDK.ThinkDataSDK().StartRun(GameConfigs.GlobalConfig.GetStr);
#endif

#if USE_THIRD_SDK 
			yield return SDK.THSDK.THSdk.Instance.WaitInitCompleted();
#endif
			yield return null;

		}
	}
}
