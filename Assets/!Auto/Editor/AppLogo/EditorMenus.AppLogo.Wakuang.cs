using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
namespace ZEditors.Cmds{
	public static partial class EditorMenus{
		[MenuItem("[Tools]/AppLogo/Wakuang",false,0)]
		static void Do_AppLogo_Wakuang(){
				BeforeDo_AppLogo_Wakuang();
				Excute("AppLogo","Wakuang",_Do_AppLogo_Wakuang);
				AfterDo_AppLogo_Wakuang();
				AssetDatabase.Refresh();
		}

		static partial void BeforeDo_AppLogo_Wakuang();
		static partial void AfterDo_AppLogo_Wakuang();
		static void _Do_AppLogo_Wakuang(){
				AppLogoExcute.ReplaceApplogo("wakuang");
		}

	}
}
