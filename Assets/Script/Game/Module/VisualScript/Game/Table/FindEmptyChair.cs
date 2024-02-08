using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using SGame.UI;
using Unity.Entities;

namespace SGame.VS
{
    // 查找空的顾客位置
    [UnitTitle("FindEmptyChair")] 
    [UnitCategory("Game/Table")]
    public class FindEmptyChair : Unit
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
        public ValueInput tableType; // 桌子类型
        
        [DoNotSerialize]
        public ValueInput chairType; // 座椅类型

        [DoNotSerialize]
        public ValueOutput result;

        private ChairData  resultValue;
        
        // 端口定义
        protected override void Definition()
        {
            inputTrigger = ControlInput("Input", (flow) =>
            {
                var table_type = flow.GetValue<TABLE_TYPE>(tableType);
                var chair_type = flow.GetValue<CHAIR_TYPE>(chairType);

                resultValue = TableManager.Instance.FindEmptyChair(table_type, chair_type);

                if (resultValue == ChairData.Null)
                    return outputFail;
                
                return outputSuccess;
            });

            tableType       = ValueInput<TABLE_TYPE>("tableType", TABLE_TYPE.CUSTOM);
            chairType       = ValueInput<CHAIR_TYPE>("chairType", CHAIR_TYPE.CUSTOMER);
            outputSuccess   = ControlOutput("Success");
            outputFail      = ControlOutput("Fail");
            result          = ValueOutput<ChairData>("Chair", (flow) => resultValue);
        }
    }
}