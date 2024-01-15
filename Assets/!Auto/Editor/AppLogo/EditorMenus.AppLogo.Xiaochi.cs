using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
namespace ZEditors.Cmds{
	public static partial class EditorMenus{
		[MenuItem("[Tools]/AppLogo/Xiaochi",false,0)]
		static void Do_AppLogo_Xiaochi(){
				BeforeDo_AppLogo_Xiaochi();
				Excute("AppLogo","Xiaochi",_Do_AppLogo_Xiaochi);
				AfterDo_AppLogo_Xiaochi();
				AssetDatabase.Refresh();
		}

		static partial void BeforeDo_AppLogo_Xiaochi();
		static partial void AfterDo_AppLogo_Xiaochi();
		static void _Do_AppLogo_Xiaochi(){
				AppLogoExcute.ReplaceApplogo("xiaochi");
		}

	}
}
