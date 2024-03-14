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
    [UnitTitle("CreateEffect")] // 
    [UnitCategory("Game/Utils")]
    public class CreateEffect : Unit
    {
        private static ILog log = LogManager.GetLogger("game.vs");
        
        [DoNotSerialize]
        public ControlInput inputTrigger;

        [DoNotSerialize]
        public ControlOutput outputTrigger;
        
        [DoNotSerialize]
        public ValueInput m_effectID;
        
        /// <summary>
        /// 对象父节点
        /// </summary>
        [DoNotSerialize]
        public ValueInput m_parent;
        
        /// <summary>
        /// 位置信息
        /// </summary>
        [DoNotSerialize]
        public ValueInput m_position;

        [DoNotSerialize]
        public ValueOutput m_result;
        
        private Entity resultValue;
        
        // 端口定义
        protected override void Definition()
        {
            // 打开UI
            inputTrigger = ControlInput("Input", (flow) =>
            {
                var effectID = flow.GetValue<int>(m_effectID);
                var parent = flow.GetValue<GameObject>(m_parent);
                var pos = flow.GetValue<Vector3>(m_position);
                resultValue = EffectSystem.Instance.AddEffect(effectID, parent, pos);
                return outputTrigger;
            });
            
            m_effectID = ValueInput<int>("effectID", 1);
            m_parent = ValueInput<GameObject>("parent", null);
            m_position = ValueInput<Vector3>("position", Vector3.zero);
            outputTrigger = ControlOutput("Output");
            m_result = ValueOutput<Entity>("Result", (flow) => resultValue);
        }
    }
}