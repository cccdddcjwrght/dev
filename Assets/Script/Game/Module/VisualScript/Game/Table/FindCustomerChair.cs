using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using SGame.UI;
using Unity.Entities;

namespace SGame.VS
{
    // 顾客所在位置
    [UnitTitle("FindCustomerChair")] 
    [UnitCategory("Game/Table")]
    public class FindCustomerChair : Unit
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
        public ValueInput _customerID; // 桌子类型

        [DoNotSerialize]
        public ValueOutput result;

        private ChairData  resultValue;
        
        // 端口定义
        protected override void Definition()
        {
            inputTrigger = ControlInput("Input", (flow) =>
            {
                var customerID = flow.GetValue<int>(_customerID);
                resultValue = TableManager.Instance.FindCustomerChair(customerID);

                if (resultValue == ChairData.Null)
                    return outputFail;
                
                return outputSuccess;
            });
            
            _customerID     = ValueInput<int>("customerID", 0);
            outputSuccess   = ControlOutput("Success");
            outputFail      = ControlOutput("Fail");
            result          = ValueOutput<ChairData>("Chair", (flow) => resultValue);
        }
    }
}