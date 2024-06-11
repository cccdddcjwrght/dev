using GameTools;
using SGame;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

namespace SGame.VS
{
    // 查找可用待机位置
    [UnitTitle("FindIdlePos")] 
    [UnitCategory("Game/Order")]
    public class FindIdlePos : Unit
    {
        [DoNotSerialize]
        public ControlInput inputTrigger;

        [DoNotSerialize]
        public ControlOutput outputTrigger;

        [DoNotSerialize]
        public ValueInput characterID;

        [DoNotSerialize]
        public ValueOutput pos;

        public int2 posValue;

        protected override void Definition()
        {
            inputTrigger = ControlInput("Input", (flow) =>
            {
                int id = flow.GetValue<int>(characterID);
                posValue = CharacterIdleModule.Instance.GetCharacterEmptyIdlePos(id);
                return outputTrigger;
            });

            outputTrigger = ControlOutput("output");
            characterID = ValueInput<int>("characterID");
            pos = ValueOutput<int2>("pos", (flow) => posValue);
        }
    }

}
