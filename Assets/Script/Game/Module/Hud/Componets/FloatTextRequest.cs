
using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;

namespace SGame
{
    /// <summary>
    /// 浮动文字创建请求
    /// </summary>
    public class FloatTextRequest : IComponentData
    {
        public float3 position;              // 浮动文字位置
        public string text;             // 文本
        public int    fontSize;         // 字体大小
        public Color  color;            // 字体颜色
        public float  duration;         // 持续时间

        public static FloatTextRequest Create(
            string text, 
            float3 pos,
            Color color, 
            int fontSize, 
            float duration)
        {
            FloatTextRequest req = new FloatTextRequest()
            {
                position    = pos,
                text        = text,
                fontSize    = fontSize,
                color       = color,
                duration    =  duration,
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
            float duration)
        {
            FloatTextRequest req = Create(text, pos, color, fontSize, duration);
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
            float duration)
        {
            FloatTextRequest req = Create(text, pos, color, fontSize, duration);
            Entity e = commandBuffer.CreateEntity();
            commandBuffer.AddComponent(e, req);
            return e;
        }
    }
}