using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame
{
    /// <summary>
    /// 物理更新
    /// </summary>
    public class PhysicsUpdate : Singleton<PhysicsUpdate>
    {
        private bool m_needUpdate = false;

        public PhysicsUpdate()
        {
            FiberCtrl.Pool.Run(Run());
        }
        
        public void Update()
        {
            m_needUpdate = true;
        }
        
        IEnumerator Run()
        {
            float timer = 0.1f;
            while (true)
            {
                timer = 0.1f;
                while (timer > 0)
                {
                    timer -= Time.deltaTime;
                    yield return null;
                }

                if (m_needUpdate)
                {
                    Physics.Simulate(0.1f);
                    m_needUpdate = false;
                }
            }
        }
        
        
    }
}