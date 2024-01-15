using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
namespace ZEditors.Cmds{
	public static partial class EditorMenus{
		[MenuItem("[Tools]/AppLogo/Xianhua",false,0)]
		static void Do_AppLogo_Xianhua(){
				BeforeDo_AppLogo_Xianhua();
				Excute("AppLogo","Xianhua",_Do_AppLogo_Xianhua);
				AfterDo_AppLogo_Xianhua();
				AssetDatabase.Refresh();
		}

		static partial void BeforeDo_AppLogo_Xianhua();
		static partial void AfterDo_AppLogo_Xianhua();
		static void _Do_AppLogo_Xianhua(){
				AppLogoExcute.ReplaceApplogo("xianhua");
		}

	}
}
