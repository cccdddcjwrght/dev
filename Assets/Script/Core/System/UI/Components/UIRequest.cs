using log4net;
using FairyGUI;
using System.Collections.Generic;
using libx;
using Unity.Entities;
using UnityEngine;
using System;

namespace SGame.UI
{
	public class UIRequestMgr
	{
		static private List<int> _reqs = new List<int>();

		static public void Add(int id)
		{
			if (id != 0 && !_reqs.Contains(id))
				_reqs.Add(id);
		}

		static public void Remove(int id)
		{
			if (id != 0 && _reqs.Contains(id)) _reqs.Remove(id);
		}

		static public bool Check(int id) => _reqs.Contains(id);

	}

	// UI元件请求加载
	public class UIRequest : IComponentData
	{
		// 原件名
		public string comName;

		// 包名
		public string pkgName;

		// 配置表名称
		public int configId;

		/// <summary>
		/// 父节点
		/// </summary>
		public GComponent parent;

		/// <summary>
		/// 创建的UI类型
		/// </summary>
		public UI_TYPE type = UI_TYPE.WINDOW;

		// 创建UI 对象
		public static Entity Create(EntityCommandBuffer commandBuffer, string name, string pkgName)
		{
			Entity e = commandBuffer.CreateEntity();
			commandBuffer.AddComponent<UIRequest>(e);
			var req = new UIRequest() { comName = name, pkgName = pkgName, configId = 0 };
			commandBuffer.SetComponent(e, req);
			return e;
		}

		public static Entity Create(EntityManager entityManager, string name, string pkgName)
		{
			Entity e = entityManager.CreateEntity(typeof(UIRequest));
			entityManager.SetComponentData(e, new UIRequest() { comName = name, pkgName = pkgName, configId = 0 });
			return e;
		}

		public static Entity Create(EntityCommandBuffer commandBuffer, int configId)
		{
			Entity e = commandBuffer.CreateEntity();
			commandBuffer.AddComponent<UIRequest>(e);
			var req = new UIRequest() { comName = null, pkgName = null, configId = configId };
			commandBuffer.SetComponent(e, req);
			return e;
		}

		public static Entity Create(EntityManager entityManager, int configId, UI_TYPE t = UI_TYPE.WINDOW, GComponent parent = null)
		{
			UIRequestMgr.Add(configId);
			Entity e = entityManager.CreateEntity(typeof(UIRequest));
			entityManager.SetComponentData(e, new UIRequest()
			{
				comName = null, 
				pkgName = null, 
				configId = configId,
				type = t,
				parent = parent
			});
			return e;
		}
	}
}