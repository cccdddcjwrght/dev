using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using SGame.UI;
using Unity.Entities;

namespace SGame.VS
{
    // 属性
    [UnitTitle("GetAttribute")] 
    [UnitCategory("Game/Attribute")]
    public class GetAttribute : Unit
    {
        [DoNotSerialize]
        public ValueInput m_targetType;    // 目标类型
        
        [DoNotSerialize]
        public ValueInput m_attributeType; // 属性类型
        
        [DoNotSerialize]
        public ValueInput m_targetID;      // 额外ID

        [DoNotSerialize]
        public ValueOutput result;

        
        // 端口定义
        protected override void Definition()
        {
            m_targetType    = ValueInput<EnumTarget>("target",       EnumTarget.Customer);
            m_attributeType = ValueInput<EnumAttribute>("attribute", EnumAttribute.Diamond);
            m_targetID      = ValueInput<int>("targetID", 0);
            result          = ValueOutput<double>("value", GetValue);
        }

        double GetValue(Flow flow)
        {
            var targetType = flow.GetValue<EnumTarget>(m_targetType);
            var attribute = flow.GetValue<EnumAttribute>(m_attributeType);
            var targetID = flow.GetValue<int>(m_targetID);
            return AttributeSystem.Instance.GetValue(targetType, attribute, targetID);
        }
    }
}