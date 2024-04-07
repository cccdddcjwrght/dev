using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
namespace ZEditors.Cmds{
	public static partial class EditorMenus{
		[MenuItem("[Tools]/配置/不更新导出配置",false,0)]
		static void Do_Config_build(){
				BeforeDo_Config_build();
				Excute("Config","build",_Do_Config_build);
				AfterDo_Config_build();

		}

		static partial void BeforeDo_Config_build();
		static partial void AfterDo_Config_build();
		static void _Do_Config_build(){
				ConfigToolEditor.ExcuteNoUpdate();
		}

	}
}
