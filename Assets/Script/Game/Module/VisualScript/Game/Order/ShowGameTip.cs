using log4net;
using Unity.VisualScripting;
using UnityEngine;

namespace SGame.VS
{
    [UnitTitle("ShowGameTip")] //The Custom Scripting Event node to receive the Event. Add "On" to the node title as an Event naming convention.
    [UnitCategory("Game/Order")]
    public class ShowGameTip : Unit
    {
        private static ILog log = LogManager.GetLogger("game.order");

        [DoNotSerialize]
        public ControlInput     inputTrigger;

        [DoNotSerialize]
        public ControlOutput    outputSuccess;
        
        /// <summary>
        /// 提示文字
        /// </summary>
        [DoNotSerialize]
        public ValueInput       m_tips;
        
        /// <summary>
        /// 显示位置
        /// </summary>
        [DoNotSerialize]
        public ValueInput       m_pos;
        
        /// <summary>
        /// 提示类型
        /// </summary>
        [DoNotSerialize]
        public ValueInput      m_type;

        protected override void Definition()
        {
            inputTrigger = ControlInput("Input", (flow) =>
            {
                string tip  = flow.GetValue<string>(m_tips);
                TipType  t  = flow.GetValue<TipType>(m_type);
                Vector3 pos = flow.GetValue<Vector3>(m_pos);
                if (tip.StartsWith("@"))
                {
                    // 使用多语言文本
                    HudModule.Instance.ShowGameTips(LanagueSystem.Instance.GetValue(tip.Substring(1, tip.Length - 1)), pos, t);
                }
                else
                {
                    // 直接显示
                    HudModule.Instance.ShowGameTips(tip, pos, t);
                }
                
                //log.Info("gametip show=" + tip);
                return outputSuccess;
            });
            
            m_tips  = ValueInput<string>("tips", "");
            m_pos   = ValueInput<Vector3>("pos", Vector3.zero);
            m_type  = ValueInput<TipType>("type", TipType.BLUE);
            outputSuccess = ControlOutput("Output");
        }
    }
}