﻿
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Guide;
	
	public partial class UIGuideMask
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;

		}
		partial void UnInitUI(UIContext context){

		}

	}
}
