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
//	说    明:	用于调用UI的UPDATE
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
	[UpdateAfter(typeof(SpawnUISystem))]
	[UpdateInGroup(typeof(UIGroup))]
	public partial class UISystem : SystemBase
	{
		// 创建UI的原型
		static ILog log = LogManager.GetLogger("xl.ui");

		// UI对象的查询条件
		private EntityQuery    m_groupUIWindow;

		// 有效的显示
		private EntityQuery    m_groupVisible;

		private List<FairyWindow> m_windowCache;

		protected override void OnCreate()
		{
			base.OnCreate();
			m_windowCache = new List<FairyWindow>(32);
		}

		/// <summary>
		/// 更新
		/// </summary>
		protected override void OnUpdate()
		{
			float deltaTime = World.Time.DeltaTime;
			
			// 1. UI更新
			Entities.WithAll<UIInitalized>().WithNone<DespawningUI>().ForEach((Entity e, UIWindow win) =>
			{
				if (win.Value != null)
				{
					if (win.Value.isReadly == false)
						return;

					m_windowCache.Add(win.Value);
				}
			}).WithoutBurst().Run();

			foreach (var w in m_windowCache)
			{
				w.OnFrameUpdate(deltaTime);
			}
			m_windowCache.Clear();
		}
		
	}
}