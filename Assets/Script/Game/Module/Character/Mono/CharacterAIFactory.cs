using System.Collections;
using System.Collections.Generic;
using libx;
using Unity.Collections;
using UnityEngine;

namespace SGame
{
    // chu
    public class CharacterAIFactory : Singleton<CharacterAIFactory>
    {
        private GameObject m_poolObject;
        struct PoolData
        {
            public PoolID poolID;
            public GameObjectPool pool;
        }
        // AI 对象池
        private Dictionary<string, GameObjectPool> m_pools;

        private Dictionary<GameObject, PoolData> m_instanceData;

        public CharacterAIFactory()
        {
            m_pools = new Dictionary<string, GameObjectPool>();
            m_instanceData = new Dictionary<GameObject, PoolData>();
            m_poolObject = new GameObject("AIPool");
            GameObject.DontDestroyOnLoad(m_poolObject);
        }

        void OnSpawned(GameObject ai)
        {
            ai.SetActive(true);
        }

        void OnDespawned(GameObject ai)
        {
            if (ai != null)
            {
                ai.SetActive(false);
                ai.transform.SetParent(m_poolObject.transform);
            }
        }

        GameObjectPool GetOrCreate(string aiName)
        {
            aiName = aiName.ToLower();
            if (m_pools.TryGetValue(aiName, out GameObjectPool pool))
            {
                return pool;
            }

            AssetRequest req = libx.Assets.LoadAsset(Utils.GetAIPath(aiName), typeof(GameObject));
            pool = new GameObjectPool(req, OnSpawned, OnDespawned);
            m_pools.Add(aiName, pool);
            return pool;
        }

        // 分配对象
        public GameObject Create(string aiName)
        {
            var pool = GetOrCreate(aiName);
            var id = pool.Alloc();
            GameObject obj = pool.Get(id);
            m_instanceData.Add(obj, new PoolData() { pool = pool, poolID = id });
            return obj;
        }

        /// <summary>
        /// 释放对象
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Free(GameObject obj)
        {
            if (!m_instanceData.TryGetValue(obj, out PoolData data))
            {
                return false;
            }

            var ret = data.pool.Free(data.poolID);
            m_instanceData.Remove(obj);
            return ret;
        }
    }
}
