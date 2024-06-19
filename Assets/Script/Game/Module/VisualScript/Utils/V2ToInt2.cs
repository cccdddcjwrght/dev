using System.Collections;
using System.Collections.Generic;
using libx;
using log4net;
using UnityEngine;
using Unity.VisualScripting;
using SGame.UI;
using Unity.Entities;
using Unity.Mathematics;

/*
 * 通过资源路径创建对象
 */

namespace SGame.VS
{
    [UnitTitle("V2ToInt2")] // 
    [UnitCategory("Game/Utils")]
    public class V2ToInt2 : Unit
    {
        private static ILog log = LogManager.GetLogger("game.vs");
        
        [DoNotSerialize]
        public ValueOutput m_int2;
        
        [DoNotSerialize]
        public ValueInput m_v2;
        
        // 端口定义
        protected override void Definition()
        {
            m_v2 = ValueInput<ValueInput>("Vector2Int");
            m_int2 = ValueOutput<int2>("int2", (flow) =>
            {
                var ret = flow.GetValue<Vector2Int>(m_v2);
                return new int2(ret.x, ret.y);
            });
        }
    }
}