
using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;

namespace SGame
{
    // 参考对其位置
    public enum PositionType
    {
        POS3D = 0, // 3d 位置
        POS2D = 1, // 2d 位置
        POS2D_CENTER = 2, // 屏幕中心偏移量
    }
    
    /// <summary>
    /// 浮动文字创建请求
    /// </summary>
    public class FloatTextRequest : IComponentData
    {
        public PositionType posType;          // 位置
        public float3       position;         // 浮动文字位置
        public string       text1;            // 文本
        public int          fontSize;         // 字体大小
        public Color        color;            // 字体颜色
        public float        duration;         // 持续时间

        public static FloatTextRequest Create(
            string text, 
            float3 pos,
            Color color, 
            int fontSize, 
            float duration,
            PositionType posType)
        {
            FloatTextRequest req = new FloatTextRequest()
            {
                position    = pos,
                text1        = text,
                fontSize    = fontSize,
                color       = color,
                duration    =  duration,
                posType = posType
            };

            return req;
        }

        /// <summary>
        /// 创建浮动文字
        /// </summary>
        /// <param name="mgr"></param>
        /// <param name="text"></param>
        /// <param name="color"></param>
        /// <param name="fontSize"></param>
        /// <returns></returns>
        public static Entity CreateEntity(
            EntityManager mgr, 
            string text, 
            float3 pos,
            Color color, 
            int fontSize, 
            float duration,
            PositionType posType)
        {
            FloatTextRequest req = Create(text, pos, color, fontSize, duration, posType);
            Entity e = mgr.CreateEntity();
            mgr.AddComponentObject(e, req);
            return e;
        }
        
        /// <summary>
        /// 创建浮动文字
        /// </summary>
        /// <param name="mgr"></param>
        /// <param name="text"></param>
        /// <param name="color"></param>
        /// <param name="fontSize"></param>
        /// <returns></returns>
        public static Entity CreateEntity(EntityCommandBuffer commandBuffer, 
            string text, 
            float3 pos,
            Color color, 
            int fontSize, 
            float duration,
            PositionType posType)
        {
            FloatTextRequest req = Create(text, pos, color, fontSize, duration, posType);
            Entity e = commandBuffer.CreateEntity();
            commandBuffer.AddComponent(e, req);
            return e;
        }
    }
}