using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using libx;
using System;
using log4net;
using Unity.Mathematics;
using Unity.VisualScripting;
namespace SGame
{
    public class EffectFactory : Singleton<EffectFactory>
    {
        private GameObject m_effectRoot;

        private static ILog log = LogManager.GetLogger("game.effect");
        
        public class EffectPool
        {
            public int effectID;
            public SObjectPool<GameObject>  m_pool;
            public AssetRequest             m_prefabRequest;
            private GameObject              m_prefab;
            private Action<int, GameObject> m_onSpawn;

            public EffectPool(int effID,
                AssetRequest request,
                Action<int,GameObject> onSpawn,
                SObjectPool<GameObject>.DeSpawnDelegate onDespawn,
                SObjectPool<GameObject>.DisposeDelegate onDisponse)
            {
                effectID = effID;
                m_prefabRequest = request;
                m_prefab = m_prefabRequest.asset as GameObject;
                m_onSpawn = onSpawn;
                m_pool = new SObjectPool<GameObject>(onAlloc, OnSpawn, onDespawn, onDisponse);
                request.Retain();
            }

            public PoolID Alloc() => m_pool.Alloc();

            public bool Free(PoolID id)
            {
                GameObject obj = Get(id);
                if (obj != null)
                {
                    return m_pool.Free(id);
                }
                
                log.Warn("dispose id=" + effectID + " poolID=" + id.ToString());
                return m_pool.Dispose(id);
            }

            void OnSpawn(GameObject obj)
            {
                if (obj == null)
                {
                    log.Error("spawn data fail=" + effectID);
                    return;
                }
                
                m_onSpawn?.Invoke(effectID, obj);
            }

            public bool Dispose(PoolID id) => m_pool.Dispose(id);

            GameObject onAlloc()
            {
                GameObject obj = GameObject.Instantiate(m_prefab);
                obj.name = "EFFECT_ID_" + effectID.ToString() + "_" + m_prefab.name;
                return obj;
            }

            public GameObject Get(PoolID id)
            {
                if (m_pool.TryGet(id, out GameObject obj))
                    return obj;

                return null;
            }

            public void Dispose()
            {
                m_prefabRequest.Release();
                m_prefabRequest = null;
            }
        }

        public Dictionary<int, EffectPool> m_pools = new Dictionary<int, EffectPool>();

        void ClearParent(GameObject obj)
        {
            if (obj != null)
            {
                EffectMono mono = obj.GetComponent<EffectMono>();
                if (mono != null)
                    return;

                mono = obj.GetComponentInParent<EffectMono>();
                if (mono != null)
                {
                    obj.transform.SetParent(m_effectRoot.transform);
                    GameObject.Destroy(mono.gameObject);
                }
            }
        }
        

        public EffectFactory()
        {
            m_effectRoot = new GameObject("effectPool");
            GameObject.DontDestroyOnLoad(m_effectRoot);
        }

        void OnSpawnGameObject(int effectID, GameObject effect)
        {
            if (effect != null)
            {
                effect.transform.SetParent(null);
                effect.SetActive(true);
                effect.SendMessage("OnSpawned", SendMessageOptions.DontRequireReceiver);
            }
        }

        void OnDespawnGameObject(GameObject effect)
        {
            ClearParent(effect);
            effect.transform.SetParent(m_effectRoot.transform);
            effect.SendMessage("OnDespawned", SendMessageOptions.DontRequireReceiver);
            effect.SetActive(false);
        }

        void OnDisponseGameObject(GameObject effect)
        {
            if (effect != null)
            {
                ClearParent(effect);
                GameObject.Destroy(effect);
            }
        }

        public GameObject Get(int effectID, PoolID id)
        {
            if (!m_pools.TryGetValue(effectID, out EffectPool pool))
                return null;

            return pool.Get(id);
        }

        EffectPool GetOrCreate(int effectID, AssetRequest req)
        {
            if (m_pools.TryGetValue(effectID, out EffectPool pool))
            {
                return pool;
            }

            pool = new EffectPool(effectID, req, OnSpawnGameObject, OnDespawnGameObject, OnDisponseGameObject);
            m_pools.Add(effectID, pool);
            return pool;
        }

        /// <summary>
        /// 分配特效
        /// </summary>
        /// <param name="effectID"></param>
        /// <returns></returns>
        public PoolID Alloc(int effectID, AssetRequest req)
        {
            var pool = GetOrCreate(effectID, req);

            while (true)
            {
                var id = pool.Alloc();
                var obj = pool.Get(id);
                if (obj != null)
                    return id;

                log.Warn("alloc object is null=" + effectID);
                pool.Dispose(id);
            }
        }

        /// <summary>
        /// 回收特效
        /// </summary>
        /// <param name="effectID"></param>
        /// <param name="effID"></param>
        /// <returns></returns>
        public bool Free(int effectID, PoolID poolID)
        {
            if (m_pools.TryGetValue(effectID, out EffectPool pool))
            {
                return pool.Free(poolID);
            }
            
            return false;
        }

        /// <summary>
        /// 销毁对应的对象
        /// </summary>
        /// <param name="effectID"></param>
        /// <param name="poolID"></param>
        /// <returns></returns>
        public bool Dispose(int effectID, PoolID poolID)
        {
            if (m_pools.TryGetValue(effectID, out EffectPool pool))
            {
                return pool.Dispose(poolID);
            }
            
            return false;
        }
    }
}
