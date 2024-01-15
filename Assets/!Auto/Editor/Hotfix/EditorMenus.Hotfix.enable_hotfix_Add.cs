using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
namespace ZEditors.Cmds{
	public static partial class EditorMenus{
		[MenuItem("[Tools]/热更/[Add]enable_hotfix",false,222171936)]
		static void Do_Hotfix_enable_hotfix_Add(){
				BeforeDo_Hotfix_enable_hotfix_Add();
				Excute("Hotfix","enable_hotfix_Add",_Do_Hotfix_enable_hotfix_Add);
				AfterDo_Hotfix_enable_hotfix_Add();

		}

		static partial void BeforeDo_Hotfix_enable_hotfix_Add();
		static partial void AfterDo_Hotfix_enable_hotfix_Add();
		static void _Do_Hotfix_enable_hotfix_Add(){
				EditorTools.ChangeSymbol("ENABLE_HOTFIX" , true);
		}
		[MenuItem("[Tools]/热更/[Add]enable_hotfix", true,222171936)]
		static bool Do_Hotfix_enable_hotfix_AddCondition(){
				return !EditorTools.CheckHasSymbol("ENABLE_HOTFIX");
		}

	}
}
