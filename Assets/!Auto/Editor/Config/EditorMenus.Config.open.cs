using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
namespace ZEditors.Cmds{
	public static partial class EditorMenus{
		[MenuItem("[Tools]/配置/打开文件夹",false,0)]
		static void Do_Config_open(){
				BeforeDo_Config_open();
				Excute("Config","open",_Do_Config_open);
				AfterDo_Config_open();

		}

		static partial void BeforeDo_Config_open();
		static partial void AfterDo_Config_open();
		static void _Do_Config_open(){
				ConfigToolEditor.OpenCfgDir();
		}

	}
}
