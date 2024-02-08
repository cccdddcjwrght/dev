using System.Collections;
using System.Collections.Generic;
using log4net;
using UnityEngine;
using Unity.VisualScripting;
using SGame.UI;
using Unity.Entities;

namespace SGame.VS
{
    // 记录餐桌上的食物数量
    [UnitTitle("RecordDishTable")] 
    [UnitCategory("Game/Table")]
    public class RecordDishTable : Unit
    {
        private static ILog log = LogManager.GetLogger("game.table");
        
        [DoNotSerialize]
        public ControlInput     inputTrigger;

        // 成功返回流程
        [DoNotSerialize]
        public ControlOutput    output;

        [DoNotSerialize]
        public ValueInput       _tableID;

        [DoNotSerialize]
        public ValueInput       _isAdd;

        // 端口定义
        protected override void Definition()
        {
            inputTrigger = ControlInput("Input", (flow) =>
            {
                // Making the resultValue equal to the input value from myValueA concatenating it with myValueB.
                var isAdd = flow.GetValue<bool>(_isAdd);
                var tableID = flow.GetValue<int>(_tableID);
                var table = TableManager.Instance.Get(tableID);
                log.Info("TABLE ID1=" + tableID + " isAdd=" + isAdd + " count=" + table.foodsCount);

                if (isAdd)
                {
                    table.foodsCount++;
                }
                else
                {
                    table.foodsCount--;
                    if (table.foodsCount < 0)
                    {
                        log.Error("food Count =" + table.foodsCount + " id = " + table.id);
                    }
                }
                log.Info("TABLE ID2=" + tableID + " isAdd=" + isAdd + " count=" + table.foodsCount);

                return output;
            });

            _tableID = ValueInput<int>("tableID");
            _isAdd = ValueInput<bool>("isAdd", true);
            output = ControlOutput("Output");
        }
    }
}