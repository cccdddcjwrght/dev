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
    }
}