using System.Collections.Generic;
using Unity.Entities;

//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
/*
 * 1. 配置里面的mask 标签
 * 2. mask 事件处理
 * context.window.AddEventListener("OnMaskClick", () =>
			{
				log.Info("UITechnology OnMaskClick");
			});
 */
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Common;
	
	public partial class UIMask
	{
		private int __id;
		partial void InitUI(UIContext context){

		}
		
		partial void UnInitUI(UIContext context){
		}

	}
}
