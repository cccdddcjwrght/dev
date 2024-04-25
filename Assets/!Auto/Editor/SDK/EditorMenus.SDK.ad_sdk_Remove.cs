using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
namespace ZEditors.Cmds{
	public static partial class EditorMenus{
		[MenuItem("[Tools]/SDK/[Remove]开启广告",false,313010204)]
		static void Do_SDK_ad_sdk_Remove(){
				BeforeDo_SDK_ad_sdk_Remove();
				Excute("SDK","ad_sdk_Remove",_Do_SDK_ad_sdk_Remove);
				AfterDo_SDK_ad_sdk_Remove();

		}

		static partial void BeforeDo_SDK_ad_sdk_Remove();
		static partial void AfterDo_SDK_ad_sdk_Remove();
		static void _Do_SDK_ad_sdk_Remove(){
				EditorTools.ChangeSymbol("USE_AD" , true , true);
		}
		[MenuItem("[Tools]/SDK/[Remove]开启广告", true,313010204)]
		static bool Do_SDK_ad_sdk_RemoveCondition(){
				return EditorTools.CheckHasSymbol("USE_AD");
		}

	}
}
