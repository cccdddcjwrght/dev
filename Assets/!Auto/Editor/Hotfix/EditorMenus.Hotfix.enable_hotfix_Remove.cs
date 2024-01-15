using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
namespace ZEditors.Cmds{
	public static partial class EditorMenus{
		[MenuItem("[Tools]/热更/[Remove]enable_hotfix",false,222171936)]
		static void Do_Hotfix_enable_hotfix_Remove(){
				BeforeDo_Hotfix_enable_hotfix_Remove();
				Excute("Hotfix","enable_hotfix_Remove",_Do_Hotfix_enable_hotfix_Remove);
				AfterDo_Hotfix_enable_hotfix_Remove();

		}

		static partial void BeforeDo_Hotfix_enable_hotfix_Remove();
		static partial void AfterDo_Hotfix_enable_hotfix_Remove();
		static void _Do_Hotfix_enable_hotfix_Remove(){
				EditorTools.ChangeSymbol("ENABLE_HOTFIX" , true , true);
		}
		[MenuItem("[Tools]/热更/[Remove]enable_hotfix", true,222171936)]
		static bool Do_Hotfix_enable_hotfix_RemoveCondition(){
				return EditorTools.CheckHasSymbol("ENABLE_HOTFIX");
		}

	}
}
