using System.Collections;
using System.Collections.Generic;
using FlatBuffers;
using GameTools;
using UnityEngine;
using Unity.VisualScripting;
using SGame.UI;
using Unity.Entities;
using Unity.Mathematics;
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
        public ValueInput       characterID;
        
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
                var cID = flow.GetValue<int>(characterID);
                var character = CharacterModule.Instance.FindCharacter(cID);
                if (character == null) {
                    return outputFail;
                }

                var character_pos = GameTools.MapAgent.VectorToGrid(character.transform.position);
                int tableID = TableManager.Instance.FindPutDishTable(new int2(character_pos.x, character_pos.y));
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

            characterID     = ValueInput<int>("CharacerID");
            resultChair     = ValueOutput<ChairData>("OrderChair", (flow) => _valueChair);
            resultTable     = ValueOutput<int>("tableID", (flow) => _valueChair.tableID);
        }
    }
}