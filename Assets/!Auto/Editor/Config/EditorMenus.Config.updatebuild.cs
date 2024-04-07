using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
namespace ZEditors.Cmds{
	public static partial class EditorMenus{
		[MenuItem("[Tools]/配置/更新导出配置",false,0)]
		static void Do_Config_updatebuild(){
				BeforeDo_Config_updatebuild();
				Excute("Config","updatebuild",_Do_Config_updatebuild);
				AfterDo_Config_updatebuild();

		}

		static partial void BeforeDo_Config_updatebuild();
		static partial void AfterDo_Config_updatebuild();
		static void _Do_Config_updatebuild(){
				ConfigToolEditor.Excute();
		}

	}
}
