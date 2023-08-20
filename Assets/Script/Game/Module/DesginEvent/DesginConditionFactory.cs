using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame
{
    /// <summary>
    /// 触发条件工厂
    /// </summary>
    public class DesginConditionFactory
    {
        /// <summary>
        /// 创建网格
        /// </summary>
        /// <returns></returns>
        public IDesginCondition CreateTileCondition(int playerId, int tileId, TileEventTrigger.State state)
        {
            return new TileCondition(playerId, tileId, state);
        }
    }
}
