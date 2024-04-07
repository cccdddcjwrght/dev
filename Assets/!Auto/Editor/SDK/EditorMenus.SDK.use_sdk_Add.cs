using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
namespace ZEditors.Cmds{
	public static partial class EditorMenus{
		[MenuItem("[Tools]/SDK/[Add]激活三方sdk",false,571801134)]
		static void Do_SDK_use_sdk_Add(){
				BeforeDo_SDK_use_sdk_Add();
				Excute("SDK","use_sdk_Add",_Do_SDK_use_sdk_Add);
				AfterDo_SDK_use_sdk_Add();

		}

		static partial void BeforeDo_SDK_use_sdk_Add();
		static partial void AfterDo_SDK_use_sdk_Add();
		static void _Do_SDK_use_sdk_Add(){
				EditorTools.ChangeSymbol("USE_THIRD_SDK" , true);
		}
		[MenuItem("[Tools]/SDK/[Add]激活三方sdk", true,571801134)]
		static bool Do_SDK_use_sdk_AddCondition(){
				return !EditorTools.CheckHasSymbol("USE_THIRD_SDK");
		}

	}
}
