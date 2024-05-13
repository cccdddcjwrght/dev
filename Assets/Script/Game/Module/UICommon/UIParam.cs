﻿
using Unity.Entities;
using Unity.VisualScripting;
using UnityEngine;

namespace SGame
{
    /// <summary>
    /// 带一个UI自定义参数
    /// </summary>
    public class UIParam : IComponentData
    {
        public object Value;
    }

    /// <summary>
    /// UI位置
    /// </summary>
    public class UIPos : IComponentData
    {
        public Vector2 pos;
    }

    //是否格子坐标转化UI坐标
    public class UICell : IComponentData 
    {
        public bool isCell;
    }

    public class UISize : IComponentData
    {
        public Vector2Int size;
    }
    
    public class UIAlpha : IComponentData
    {
        public float alpha;
    }
}
