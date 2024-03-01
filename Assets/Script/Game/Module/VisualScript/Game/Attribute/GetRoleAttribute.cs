using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using SGame.UI;
using Unity.Entities;

namespace SGame.VS
{
    // 属性
    [UnitTitle("GetRoleAttribute")] 
    [UnitCategory("Game/Attribute")]
    public class GetRoleAttribute : BaseRoleAttribute
    {
        [DoNotSerialize]
        public ValueInput m_attributeType; // 属性类型

        [DoNotSerialize]
        public ValueOutput result;

        [DoNotSerialize]
        public ValueOutput resultRoleID;
        
        // 端口定义
        protected override void Definition()
        {
            base.Definition();
            m_attributeType = ValueInput<EnumAttribute>("attribute", EnumAttribute.Diamond);
            result          = ValueOutput<double>("value", GetValue);
            resultRoleID    = ValueOutput<int>("RoleID", (flow) => GetRoleID(flow));
        }

        double GetValue(Flow flow)
        {
            var roleID = GetRoleID(flow);
            var attribute = flow.GetValue<EnumAttribute>(m_attributeType);
            return AttributeSystem.Instance.GetValueByRoleID(roleID, attribute);
        }
    }
}