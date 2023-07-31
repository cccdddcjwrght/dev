using Unity.Entities;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

// 游戏逻辑之间的事件交互
namespace SGame
{
    public struct EventData : IComponentData
    {
        public int      eventId;
    }
    
    public partial class EventSystem : SystemBase
    {
        private Dictionary<int, Action<Entity>>         m_events;
        private EntityArchetype                         m_eventArchetype;
        private EndSimulationEntityCommandBufferSystem  m_commandBufferSystem;
        
        public static EventSystem Instance
        {
            get
            {
                EventSystem ret = Unity.Entities.World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<EventSystem>();
                return ret;
            }
        }

        protected override void OnCreate()
        {
            m_commandBufferSystem   = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
            m_events                = new Dictionary<int, Action<Entity>>();
            m_eventArchetype        = EntityManager.CreateArchetype(typeof(EventData));
        }

        protected override void OnUpdate()
        {
            var cmdBuffer = m_commandBufferSystem.CreateCommandBuffer();
            Entities.WithoutBurst().ForEach((Entity e, in EventData evtData) =>
            {
                if (m_events.TryGetValue(evtData.eventId, out Action<Entity> callback))
                {
                    callback(e);
                }
                cmdBuffer.DestroyEntity(e);
            }).Run();
        }

        // 注册事件
        public void Register(int eventId, Action<Entity> callback)
        {
            Action<Entity> ret;
            if (m_events.TryGetValue(eventId, out ret))
            {
                ret += callback;
            }
            else
            {
                m_events.Add(eventId, callback);
            }
        }

        // 取消事件
        public void Unregister(int eventId, Action<Entity> callback)
        {
            Action<Entity> ret;
            if (m_events.TryGetValue(eventId, out ret))
            {
                ret -= callback;
                if (ret == null)
                {
                    m_events.Remove(eventId);
                }
            }
        }

        // 创建通用事件
        // eventId, 事件Id
        public Entity FireEvent(EntityCommandBuffer cmdBuff, int eventId)
        {
            var e = cmdBuff.CreateEntity(m_eventArchetype);
            cmdBuff.SetComponent(e, new EventData{eventId =  eventId});
            return e;
        }

        // eventId, 事件Id
        // param， 事件参数
        public Entity FireEvent<T>(EntityCommandBuffer cmdBuff, int eventId, T param) where T : struct, IComponentData
        {
            var archeType = EntityManager.CreateArchetype(typeof(EventData), typeof(T));
            var e = cmdBuff.CreateEntity(archeType);
            cmdBuff.SetComponent(e, new EventData{eventId = eventId});
            cmdBuff.SetComponent(e, param);

            return e;
        }
    }
}