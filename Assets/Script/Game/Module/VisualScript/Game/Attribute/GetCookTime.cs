using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using SGame.UI;
using Unity.Entities;

namespace SGame.VS
{
    // 角色煮饭时间
    [UnitTitle("GetCookTime")] 
    [UnitCategory("Game/Attribute")]
    public class GetCookTime : Unit
    {
        [DoNotSerialize]
        public ValueInput m_target;    // 角色
        
        [DoNotSerialize]
        public ValueOutput resultTime;     // 返回显示时间

        private INPUT_TYPE _inputType;

        [DoNotSerialize]
        [Inspectable, UnitHeaderInspectable("Type")]
        public INPUT_TYPE inputType
        {
            get => _inputType;
            set => _inputType = value;
        }
        
        // 端口定义
        protected override void Definition()
        {
            if (inputType == INPUT_TYPE.ID)
            {
                m_target = ValueInput<int>("CharacterID");
            }
            else
            {
                m_target = ValueInput<Character>("Character");
            }

            resultTime   = ValueOutput<float>("time", GetValue);
        }

        float GetValue(Flow flow)
        {
            return 1.0f;
        }
    }
}