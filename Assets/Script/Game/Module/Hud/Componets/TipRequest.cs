
using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;

namespace SGame
{
    // -0-订单
   // -1,-过程
   // -2-飘字 
    // public class TipType : IComponentData
    // {
    //
    //     public Type type;
    // }
    //
    // public enum Type
    // {
    //     IMAGE=1,
    //     PROGRESS,
    //     TEXT,
    //     ANIM
    // }

    /// <summary>
    /// 2DUI创建请求
    /// </summary>
    public class TipRequest : IComponentData
    {
        public static Entity CreateEntity(
            EntityManager mgr, 
            ImageDisplayData imgData
            )
        {
            ImageDisplayData imageData = new ImageDisplayData()
             {
                  position =imgData.position,
                  imgUrl = imgData.imgUrl,
                  posType = imgData.posType
             };
             Entity e = mgr.CreateEntity();
             mgr.AddComponentData(e, imageData);
             return e;
        }
        
      
        public static Entity CreateEntity(EntityCommandBuffer commandBuffer, 
            ImageDisplayData imgData)
        {
            ImageDisplayData imageData = new ImageDisplayData()
            {
                position =imgData.position,
                imgUrl = imgData.imgUrl,
                posType = imgData.posType
            };
            Entity e = commandBuffer.CreateEntity();
            commandBuffer.AddComponent(e, imageData);
            return e;
        }
        
        public static Entity CreateEntity(
            EntityManager mgr, 
            TextDisplayData imgData
        )
        {
            TextDisplayData imageData = new TextDisplayData()
            {
                position =imgData.position,
                textStr  = imgData.textStr,
                posType  = imgData.posType,
                speed    = imgData.speed,
                duration = imgData.duration
            };
            Entity e = mgr.CreateEntity();
            mgr.AddComponentData(e, imageData);
            return e;
        }
        
      
        public static Entity CreateEntity(EntityCommandBuffer commandBuffer, 
            TextDisplayData imgData)
        {
            TextDisplayData imageData = new TextDisplayData()
            {
                position =imgData.position,
                textStr  = imgData.textStr,
                posType  = imgData.posType,
                speed    = imgData.speed,
                duration = imgData.duration
            };
            Entity e = commandBuffer.CreateEntity();
            commandBuffer.AddComponent(e, imageData);
            return e;
        }
        
        public static Entity CreateEntity(
            EntityManager mgr, 
            AniImageDisplayData imgData
        )
        {
            AniImageDisplayData imageData = new AniImageDisplayData()
            {
                position =imgData.position,
                posType  = imgData.posType
            };
            Entity e = mgr.CreateEntity();
            mgr.AddComponentData(e, imageData);
            return e;
        }
        
      
        public static Entity CreateEntity(EntityCommandBuffer commandBuffer, 
            AniImageDisplayData imgData)
        {
            AniImageDisplayData imageData = new AniImageDisplayData()
            {
                position =imgData.position,
                posType  = imgData.posType,
            };
            Entity e = commandBuffer.CreateEntity();
            commandBuffer.AddComponent(e, imageData);
            return e;
        }
    }
    
    public class ImageDisplayData:IComponentData
    {
        public float3 position;
        public string imgUrl;
        public PositionType posType;
    }

    public class TextDisplayData:IComponentData
    {
        public float3 position;
        public string textStr;
        public PositionType posType;
        public float speed;
        public float duration;//飘字时间
    }

    public class AniImageDisplayData:IComponentData
    {
        public float3 position;
        public PositionType posType;
    }
}