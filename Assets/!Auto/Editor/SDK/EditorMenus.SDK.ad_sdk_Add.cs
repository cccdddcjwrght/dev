using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
namespace ZEditors.Cmds{
	public static partial class EditorMenus{
		[MenuItem("[Tools]/SDK/[Add]开启广告",false,313010204)]
		static void Do_SDK_ad_sdk_Add(){
				BeforeDo_SDK_ad_sdk_Add();
				Excute("SDK","ad_sdk_Add",_Do_SDK_ad_sdk_Add);
				AfterDo_SDK_ad_sdk_Add();

		}

		static partial void BeforeDo_SDK_ad_sdk_Add();
		static partial void AfterDo_SDK_ad_sdk_Add();
		static void _Do_SDK_ad_sdk_Add(){
				EditorTools.ChangeSymbol("USE_AD" , true);
		}
		[MenuItem("[Tools]/SDK/[Add]开启广告", true,313010204)]
		static bool Do_SDK_ad_sdk_AddCondition(){
				return !EditorTools.CheckHasSymbol("USE_AD");
		}

	}
}
