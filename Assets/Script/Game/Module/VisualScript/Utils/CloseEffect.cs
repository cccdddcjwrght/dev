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
    [UnitTitle("CloseEffect")] // 
    [UnitCategory("Game/Utils")]
    public class CloseEffect : Unit
    {
        private static ILog log = LogManager.GetLogger("game.vs");
        
        [DoNotSerialize]
        public ControlInput  inputTrigger;

        [DoNotSerialize]
        public ControlOutput outputTrigger;
        
        [DoNotSerialize]
        public ValueInput    m_effectEntity;
        
        // 端口定义
        protected override void Definition()
        {
            // 打开UI
            inputTrigger = ControlInput("Input", (flow) =>
            {
                //Making the resultValue equal to the input value from myValueA concatenating it with myValueB.
                var entity = flow.GetValue<Entity>(m_effectEntity);
                EffectSystem.Instance.CloseEffect(entity);
                return outputTrigger;
            });
            
            m_effectEntity  = ValueInput<Entity>("effectEntity");
            outputTrigger   = ControlOutput("Output");
        }
    }
}