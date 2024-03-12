using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using SGame.UI;
using Unity.Entities;

//UI位置设置
namespace SGame.VS
{
    [UnitTitle("UISetPos")]
    [UnitCategory("Game/UI")]
    public class UISetPos : Unit
    {
        [DoNotSerialize]
        public ControlInput inputTrigger;

        [DoNotSerialize]
        public ControlOutput outputTrigger;

        //ui
        [DoNotSerialize]
        public ValueInput uiEntity;
        
        //UI位置
        [DoNotSerialize]
        public ValueInput uiPos;

        // 端口定义
        protected override void Definition()
        {
            // 打开UI
            inputTrigger = ControlInput("Input", (flow) =>
            {
                EntityManager mgr = World.DefaultGameObjectInjectionWorld.EntityManager;
                if (uiPos.hasValidConnection&&uiEntity.hasValidConnection)
                {
                    Vector2Int v2 = flow.GetValue<Vector2Int>(uiEntity);
                    Vector2Int p2 = flow.GetValue<Vector2Int>(uiPos);
                    Entity e = new Entity() {Index = v2.x, Version = v2.y};
                    mgr.AddComponentObject(e, new UIPos() { pos= p2 });
                }
                return outputTrigger;
            });
            
            uiEntity = ValueInput<Vector2Int>("UI Entity");
            uiPos = ValueInput<Vector2Int>("UIPos");
            outputTrigger = ControlOutput("Output");
        }
    }
}