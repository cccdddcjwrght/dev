using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using SGame.UI;
using Unity.Entities;

//UI位置设置
namespace SGame.VS
{
    [UnitTitle("UISetSize")]
    [UnitCategory("Game/UI")]
    public class UISetSize : Unit
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
        public ValueInput uiSize;

        // 端口定义
        protected override void Definition()
        {
            // 打开UI
            inputTrigger = ControlInput("Input", (flow) =>
            {
                EntityManager mgr = World.DefaultGameObjectInjectionWorld.EntityManager;
                if (uiSize.hasValidConnection&&uiEntity.hasValidConnection)
                {
                    Vector2Int v2 = flow.GetValue<Vector2Int>(uiEntity);
                    Vector2Int p2 = flow.GetValue<Vector2Int>(uiSize);
                    Entity e = new Entity() {Index = v2.x, Version = v2.y};
                    mgr.AddComponentObject(e, new UISize() { size= p2 }); }
                return outputTrigger;
            });
            
            uiEntity = ValueInput<Vector2Int>("UI Entity");
            uiSize = ValueInput<Vector2Int>("UISize");
            outputTrigger = ControlOutput("Output");
        }
    }
}