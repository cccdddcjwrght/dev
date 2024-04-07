using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
namespace ZEditors.Cmds{
	public static partial class EditorMenus{
		[MenuItem("[Tools]/SDK/[Remove]关闭数数",false,313023495)]
		static void Do_SDK_td_sdk_Remove(){
				BeforeDo_SDK_td_sdk_Remove();
				Excute("SDK","td_sdk_Remove",_Do_SDK_td_sdk_Remove);
				AfterDo_SDK_td_sdk_Remove();

		}

		static partial void BeforeDo_SDK_td_sdk_Remove();
		static partial void AfterDo_SDK_td_sdk_Remove();
		static void _Do_SDK_td_sdk_Remove(){
				EditorTools.ChangeSymbol("TD_OFF" , true , true);
		}
		[MenuItem("[Tools]/SDK/[Remove]关闭数数", true,313023495)]
		static bool Do_SDK_td_sdk_RemoveCondition(){
				return EditorTools.CheckHasSymbol("TD_OFF");
		}

	}
}
