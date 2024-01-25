using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using SGame.UI;
using Unity.Entities;

namespace SGame.VS
{
    // 查找食物工作台
    [UnitTitle("FindFoodMachine")] 
    [UnitCategory("Game/Table")]
    public class FindFoodMachine : Unit
    {
        [DoNotSerialize]
        public ControlInput inputTrigger;

        // 成功返回流程
        [DoNotSerialize]
        public ControlOutput outputSuccess;
        
        // 失败返回流程
        [DoNotSerialize]
        public ControlOutput outputFail;
        
        [DoNotSerialize]
        public ValueInput _foodType; // 食物类型
        
        [DoNotSerialize]
        public ValueOutput resultTable; // 返回桌子对象

        [DoNotSerialize]
        public ValueOutput resultChair; // 返回操作台工作位置

        private TableData  _tableValue;
        private ChairData  _chairValue;
        
        // 端口定义
        protected override void Definition()
        {
            inputTrigger = ControlInput("Input", (flow) =>
            {
                var foodType = flow.GetValue<int>(_foodType);

                if (TableManager.Instance.FindEmptyMatchine(foodType, out _tableValue, out _chairValue))
                    return outputSuccess;

                return outputFail;
            });

            _foodType       = ValueInput<int>("foodType", 0);
            outputSuccess   = ControlOutput("Success");
            outputFail      = ControlOutput("Fail");
            resultTable     = ValueOutput<TableData>("table", (flow) => _tableValue);
            resultChair     = ValueOutput<ChairData>("chair", (flow) => _chairValue);

        }
    }
}