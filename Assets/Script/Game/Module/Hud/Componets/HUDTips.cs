
using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;

namespace SGame.UI
{
    /// <summary>
    /// TIPS 提示的参数
    /// </summary>
    public class HUDTips : IComponentData
    {
        public string   title;
        public Color    color;
        public int      fontSize;
        public float    speed;
        public float    offsetX;
        public float    offsetY;
    }
    
    public struct TweenTime : IComponentData
    {
        public float Value;
    }
}
