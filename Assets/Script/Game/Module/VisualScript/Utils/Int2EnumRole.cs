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
    [UnitTitle("Int2EnumRole")] // 
    [UnitCategory("Game/Utils")]
    public class Int2EnumRole : Unit
    {
        private static ILog log = LogManager.GetLogger("game.vs");
        
        [DoNotSerialize]
        public ValueInput m_enum;
        
        [DoNotSerialize]
        public ValueOutput m_result;
        
        // 端口定义
        protected override void Definition()
        {
            m_enum = ValueInput<int>("RoleType", 0);
            m_result = ValueOutput<EnumRole>("Int", (flow) => (EnumRole)flow.GetValue<int>(m_enum));
        }
    }
}