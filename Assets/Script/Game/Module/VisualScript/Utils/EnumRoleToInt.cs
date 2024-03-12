using System.Collections;
using System.Collections.Generic;
using libx;
using log4net;
using UnityEngine;
using Unity.VisualScripting;
using SGame.UI;
using Unity.Entities;

/*
 * 通过资源路径创建对象
 */

namespace SGame.VS
{
    [UnitTitle("EnumRoleToInt")] // 
    [UnitCategory("Game/Utils")]
    public class EnumRoleToInt : Unit
    {
        private static ILog log = LogManager.GetLogger("game.vs");
        
        [DoNotSerialize]
        public ValueInput m_enum;
        
        [DoNotSerialize]
        public ValueOutput m_result;
        
        // 端口定义
        protected override void Definition()
        {
            m_enum = ValueInput<EnumRole>("RoleType", EnumRole.Customer);
            m_result = ValueOutput<int>("Int", (flow) => (int)flow.GetValue<EnumRole>(m_enum));
        }
    }
}