using System.Collections;
using System.Collections.Generic;
using SGame.UI;
using Unity.Entities;
using UnityEngine;
using SGame.UI;

/// 等待UI结束
namespace SGame
{
    public class WaitUIOpen : IEnumerator
    {
        private Entity              m_eUI;
        private EntityManager       m_mgr;
        
		
		public WaitUIOpen(EntityManager mgr, Entity e)
        {
            m_mgr = mgr;
            m_eUI = e;
        }

        public bool MoveNext()
        {
            if (m_mgr.Exists(m_eUI) == false)
            {
                return false;
            }

            if (m_mgr.HasComponent<UIInitalized>(m_eUI) && !m_mgr.HasComponent<DespawningEntity>(m_eUI))
                return false;

            return true;
        }

        public object Current
        {
            get
            {
                return m_eUI;
            }
        }

        public void Reset()
        {
            
        }
    }
}