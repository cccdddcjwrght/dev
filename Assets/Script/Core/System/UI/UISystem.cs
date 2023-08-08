using System.Collections.Generic;
using Unity.Entities;
using log4net;
using FairyGUI;
using Unity.Collections;
using UnityEngine;
using System;

//*******************************************************************
//	创建日期:	2016-8-22
//	文件名称:	UISystem.cs
//  创 建 人:   Silekey
//	版权所有:	
//	说    明:	用于 绑定assetbundle 与 fairygui 包, 通过fairygui 包直接加载bundle并返回
//              UIPackage 没有卸载功能(应该给UIPackage 加一个引用计数, 然后当引用数为0, 可以对UIPackage 进行Dispose)
//*************************************************************************
// example: 
// var ui = UISystem.Instance.ShowwUI("name");
// var req = EntityManager.GetComponentObject<UIRequest>(ui);
// yield return req;
// var uiWindow = EntityManager.GetComponentObject<UIWindow>(ui);
// var component1 = uiWindow.Value.GetChild("child1")
// UISystem.Instance.CloseUI(ui); 

namespace SGame.UI
{
	[DisableAutoCreation]
	public partial class UISystem : SystemBase
	{
		// 创建UI的原型
		static ILog log = LogManager.GetLogger("xl.ui");

		// UI对象的查询条件
		private EntityQuery    m_groupUIWindow;

		// 有效的显示
		private EntityQuery    m_groupVisible;

		/// <summary>
		/// 更新
		/// </summary>
		protected override void OnUpdate()
		{
			float deltaTime = Time.DeltaTime;
			
			// 1. UI更新
			Entities.WithAll<UIInitalized>().ForEach((Entity e, UIWindow win) =>
			{
				if (win.Value.isReadly == false)
					return;

				win.Value.OnFrameUpdate(deltaTime);
			}).WithoutBurst().Run();
		}
	}
}