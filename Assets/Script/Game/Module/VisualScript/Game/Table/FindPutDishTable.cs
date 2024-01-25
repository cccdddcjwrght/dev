using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using SGame.UI;
using Unity.Entities;
using Unity.Serialization;

namespace SGame.VS
{
    // 查找空的放餐桌
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
        public ValueOutput      resultTable;
        
        [DoNotSerialize]
        public ValueOutput      resultID;

        [DoNotSerialize]
        private TableData       _valueTable;

        
        // 端口定义
        protected override void Definition()
        {
            inputTrigger = ControlInput("Input", (flow) =>
            {
                int tableID = TableManager.Instance.FindPutDishTable();
                if (tableID <= 0)
                    return outputFail;

                _valueTable = TableManager.Instance.Get(tableID);
                if (_valueTable == null)
                    return outputFail;
                
                return outputSuccess;
            });

            outputSuccess   = ControlOutput("Success");
            outputFail      = ControlOutput("Fail");
            resultTable     = ValueOutput<TableData>("table", (flow) => _valueTable);
            resultID        = ValueOutput<int>("tableID", (flow) => _valueTable != null ? _valueTable.id : 0);
        }
    }
}