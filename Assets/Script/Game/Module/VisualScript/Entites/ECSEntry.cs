using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using SGame.UI;
using Unity.Entities;


namespace SGame.VS
{
    [UnitTitle("ECSEntry")] //The Custom Scripting Event node to receive the Event. Add "On" to the node title as an Event naming convention.
    [UnitCategory("Game/ECS")]
    public class ECSEntry : Unit
    {
        [DoNotSerialize]
        public ValueOutput world;
        
        [DoNotSerialize]
        public ValueOutput EntityManager;
        
    
        // 端口定义
        protected override void Definition()
        {
            world = ValueOutput<World>("Default World", (flow) =>  World.DefaultGameObjectInjectionWorld);
            EntityManager = ValueOutput<EntityManager>(nameof(EntityManager), (flow) =>  World.DefaultGameObjectInjectionWorld.EntityManager);
        }
    }
}