using SGame;
using Unity.VisualScripting;
using Unity.Mathematics;
using System.Collections.Generic;
using GameConfigs;
using log4net;
using Sirenix.Utilities;
using UnityEngine;

namespace SGame.VS
{
    /// <summary>
    /// 获取当随机出生点地图TAG
    /// </summary>
	[UnitTitle("GetRandomBornTag")] 
    [UnitCategory("Game/Random")]
    public sealed class GetRandomBornTag : Unit
    {
        private static ILog log = LogManager.GetLogger("game.random");
        
        [DoNotSerialize]
        public ValueOutput m_OutValue;
        
        protected override void Definition()
        {
            m_OutValue       = ValueOutput<string>("value", (flow) =>
            {
                int bornCount = StaticDefine.CUSTOMER_TAG_BORN.Count;
                if (bornCount <= 0)
                {
                    log.Error("CUSTOMER_TAG_BORN empty!");
                    return null;
                }

                string ret = null;
                if (bornCount == 1)
                { 
                    ret = StaticDefine.CUSTOMER_TAG_BORN[0];
                    //log.Info("get random born=" + ret);
                    return ret;
                }
                
                    
                int index = RandomSystem.Instance.NextInt(0, bornCount);
                ret = StaticDefine.CUSTOMER_TAG_BORN[index];
                //log.Info("get random born=" + ret);
                return ret;
            });
        }
    }
}
