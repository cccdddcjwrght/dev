using System.Collections;
using System.Collections.Generic;
using SGame;

namespace SGame
{
    public class TileCondition : IDesginCondition
    {
        private int                      m_tileId;  // 地块ID
        private TileEventTrigger.State   m_state;   // 触发事件
        private int                      m_playerId;
        
        public TileCondition(int playerId, int tileId, TileEventTrigger.State state)
        {
            m_playerId = playerId;
            m_tileId = tileId;
            m_state  = state;
        }
        
        public bool CheckTile(int playerId, int tileId, TileEventTrigger.State state)
        {
            return m_tileId == tileId && m_state == state;
        }

    }
}