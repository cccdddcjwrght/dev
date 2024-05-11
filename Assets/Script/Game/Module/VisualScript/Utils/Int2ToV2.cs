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
    [UnitTitle("Int2ToV2")] // 
    [UnitCategory("Game/Utils")]
    public class Int2ToV2 : Unit
    {
        private static ILog log = LogManager.GetLogger("game.vs");
        
        [DoNotSerialize]
        public ValueInput m_int2;
        
        [DoNotSerialize]
        public ValueOutput m_v2;
        
        // 端口定义
        protected override void Definition()
        {
            m_int2 = ValueInput<int2>("int2");
            m_v2 = ValueOutput<Vector2Int>("Int", (flow) =>
            {
                var ret = flow.GetValue<int2>(m_int2);
                return new Vector2Int(ret.x, ret.y);
            });
        }
    }
}