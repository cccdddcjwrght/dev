using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
namespace ZEditors.Cmds{
	public static partial class EditorMenus{
		[MenuItem("[Tools]/SDK/[Add]关闭数数",false,313023495)]
		static void Do_SDK_td_sdk_Add(){
				BeforeDo_SDK_td_sdk_Add();
				Excute("SDK","td_sdk_Add",_Do_SDK_td_sdk_Add);
				AfterDo_SDK_td_sdk_Add();

		}

		static partial void BeforeDo_SDK_td_sdk_Add();
		static partial void AfterDo_SDK_td_sdk_Add();
		static void _Do_SDK_td_sdk_Add(){
				EditorTools.ChangeSymbol("TD_OFF" , true);
		}
		[MenuItem("[Tools]/SDK/[Add]关闭数数", true,313023495)]
		static bool Do_SDK_td_sdk_AddCondition(){
				return !EditorTools.CheckHasSymbol("TD_OFF");
		}

	}
}
