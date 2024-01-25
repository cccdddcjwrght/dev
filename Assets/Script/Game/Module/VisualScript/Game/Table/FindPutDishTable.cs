using System.Collections;
using System.Collections.Generic;
using FlatBuffers;
using UnityEngine;
using Unity.VisualScripting;
using SGame.UI;
using Unity.Entities;
using Unity.Serialization;

namespace SGame.VS
{
    // 查找空的放餐桌 位置
    [UnitTitle("FindPutDishTable")] 
    [UnitCategory("Game/Table")]
    public class FindPutDishTable : Unit
    {
        [DoNotSerialize]
        public ControlInput     inputTrigger;

        // 成功返回流程
        [DoNotSerialize]
        public ControlOutput    outputSuccess;
        
        // 失败返回流程
        [DoNotSerialize]
        public ControlOutput    outputFail;
        
        
        [DoNotSerialize]
        public ValueOutput      resultChair;

        [DoNotSerialize]
        public ValueOutput      resultTable;
        
        [DoNotSerialize]
        private ChairData       _valueChair;

        protected override void Definition()
        {
            inputTrigger = ControlInput("Input", (flow) =>
            {
                int tableID = TableManager.Instance.FindPutDishTable();
                if (tableID <= 0)
                    return outputFail;

                var table = TableManager.Instance.Get(tableID);
                if (table == null)
                    return outputFail;

                int chairIndex = table.FindFirstChair(CHAIR_TYPE.ORDER);
                if (chairIndex < 0)
                    return outputFail;

                _valueChair = table.GetChair(chairIndex);
                return outputSuccess;
            });

            outputSuccess   = ControlOutput("Success");
            outputFail      = ControlOutput("Fail");
            
            resultChair     = ValueOutput<ChairData>("OrderChair", (flow) => _valueChair);
            resultTable     = ValueOutput<int>("tableID", (flow) => _valueChair.tableID);
        }
    }
}