using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
namespace ZEditors.Cmds{
	public static partial class EditorMenus{
		[MenuItem("[Tools]/SDK/[Remove]激活三方sdk",false,571801134)]
		static void Do_SDK_use_sdk_Remove(){
				BeforeDo_SDK_use_sdk_Remove();
				Excute("SDK","use_sdk_Remove",_Do_SDK_use_sdk_Remove);
				AfterDo_SDK_use_sdk_Remove();

		}

		static partial void BeforeDo_SDK_use_sdk_Remove();
		static partial void AfterDo_SDK_use_sdk_Remove();
		static void _Do_SDK_use_sdk_Remove(){
				EditorTools.ChangeSymbol("USE_THIRD_SDK" , true , true);
		}
		[MenuItem("[Tools]/SDK/[Remove]激活三方sdk", true,571801134)]
		static bool Do_SDK_use_sdk_RemoveCondition(){
				return EditorTools.CheckHasSymbol("USE_THIRD_SDK");
		}

	}
}
