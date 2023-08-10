using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

/// 等待UI结束
namespace SGame
{
    public class WaitUIClose : IEnumerator
    {
        private Entity              m_eUI;
        private EntityManager       m_mgr;
        public WaitUIClose(EntityManager mgr, Entity e)
        {
            m_mgr = mgr;
            m_eUI = e;
        }

        public bool MoveNext()
        {
            return m_mgr.Exists(m_eUI);
        }

        public object Current
        {
            get
            {
                return null;
            }
        }

        public void Reset()
        {
            
        }
    }
}