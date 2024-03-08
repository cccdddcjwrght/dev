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
    [UnitTitle("CreateGameObjectFromPrefab")] // 
    [UnitCategory("Game/Utils")]
    public class CreateGameObjectFromPrefab : Unit
    {
        private static ILog log = LogManager.GetLogger("game.vs");
        
        [DoNotSerialize]
        public ControlInput inputTrigger;

        [DoNotSerialize]
        public ControlOutput outputTrigger;
        
        [DoNotSerialize]
        public ValueInput m_assetPath;
        
        /// <summary>
        /// 对象父节点
        /// </summary>
        [DoNotSerialize]
        public ValueInput m_parent;

        [DoNotSerialize]
        public ValueOutput m_result;
        
        private GameObject resultValue;
        
        // 端口定义
        protected override void Definition()
        {
            // 打开UI
            inputTrigger = ControlInput("Input", (flow) =>
            {
                //Making the resultValue equal to the input value from myValueA concatenating it with myValueB.
                var assetPath = flow.GetValue<string>(m_assetPath);
                var parentObject = flow.GetValue<GameObject>(m_parent);
                if (string.IsNullOrEmpty(assetPath))
                {
                    log.Error("asset Path Is Null!");
                    return outputTrigger;
                }

                var prefab = ResourceManager.Instance.LoadPrefab(assetPath);
                resultValue = GameObject.Instantiate(prefab, parentObject ? parentObject.transform : null);
                return outputTrigger;
            });
            
            m_assetPath = ValueInput<string>("assetPath", "");
            m_parent = ValueInput<GameObject>("parent", null);
            outputTrigger = ControlOutput("Output");
            m_result = ValueOutput<GameObject>("GameObject", (flow) => resultValue);
        }
    }
}