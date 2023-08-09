using log4net;
using FairyGUI;
using System.Collections.Generic;
using libx;
using Unity.Entities;
using UnityEngine;
using System;

namespace SGame.UI
{
    // UI元件请求加载
    public class UIRequest : IComponentData
    {
        // 原件名
        public   string                m_uiName;
        
        // 包名
        public   string                m_uiPackageName;
        
        // 配置表名称
        public   int                   m_configId;

        // 创建UI 对象
        public static Entity Create(EntityCommandBuffer commandBuffer, string name, string pkgName)
        {
            Entity e = commandBuffer.CreateEntity();
            commandBuffer.AddComponent<UIRequest>(e);
            var req = new UIRequest() { m_uiName = name, m_uiPackageName = pkgName };
            commandBuffer.SetComponent(e, req);
            return e;
        }
        
        public static Entity Create(EntityManager entityManager, string name, string pkgName)
        {
            Entity e = entityManager.CreateEntity(typeof(UIRequest));
            entityManager.SetComponentData(e, new UIRequest() { m_uiName = name, m_uiPackageName = pkgName });
            return e;
        }
    }
}