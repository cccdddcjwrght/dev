using System.Collections;
using System.Collections.Generic;
using System.Linq;
using libx;
using log4net;
using SGame;
using Unity.Entities;
using Unity.Transforms;
using Unity.VisualScripting;
using UnityEngine;

namespace SGame
{
    // 单个对象的对象池
    public class CharacterPool : IEnumerator
    {
        private static ILog log = LogManager.GetLogger("character.factory");
        
        public ObjectPool<GameObject>   m_pool;
        public CharacterGenerator       m_gen;
        private Transform               m_root;
        private string                  m_config;
        public string config => m_config;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="root"></param>
        /// <param name="part"></param>
        public CharacterPool(Transform root, string part)
        {
            m_root = root;
            m_config = part;
            m_gen = CharacterGenerator.CreateWithConfig(part);
            m_pool = new ObjectPool<GameObject>(Alloc, OnSpawn, OnDespawn, DesposeObject);
        }

        public int usedCount => m_pool.usedCount;

        private void DesposeObject(GameObject obj)
        {
            GameObject.Destroy(obj);
        }

        // 对象池 创建新的对象
        private GameObject Alloc()
        {
            GameObject newObject = m_gen.Generate();
            GameObject.DontDestroyOnLoad(newObject);
            newObject.name = "CharacterModel";
            return newObject;
        }

        // 创建对象
        private void OnSpawn(GameObject obj)
        {
            obj.transform.SetParent(null);
        }

        // 销毁对象
        private void OnDespawn(GameObject obj)
        {
            obj.SetActive(false);
            obj.transform.SetParent(m_root);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = Vector3.one;
            //obj.SendMessage("OnDespawn");
        }

        public bool isDone => m_gen.ConfigReady;
        

        // 分配
        public PoolID Spawn()
        {
            return m_pool.Alloc();
        }
        
        // 销毁
        public void Despawn(PoolID id)
        {
            if (!m_pool.Free(id))
            {
                log.Error(string.Format("character free fail config={0}, id={1}", m_gen.GetConfig(), id));
            }
        }

        // 获取对象 
        public GameObject GetObject(PoolID id)
        {
            if (m_pool.TryGet(id, out GameObject obj))
                return obj;

            log.Error(string.Format("character GetObject fail config={0}, id={1}", m_gen.GetConfig(), id));
            return null;
        }

        public void Dispose()
        {
            m_pool.Dispose();
        }
        
        // ******************************* IEnumerator 接口 **********************************************
        public bool MoveNext()
        {
            return !isDone;
        }
        
        public object Current => null;
        
        public void Reset() { }
    }
    
    /// <summary>
    /// 角色对象创建工厂
    /// </summary>
    public class CharacterFactory : Singleton<CharacterFactory>
    {
        public struct SPAWN_DATA
        {
            public CharacterPool pool;
            public PoolID        id;
        }
        private static ILog log = LogManager.GetLogger("character.factory");

        
        private Dictionary<string, CharacterPool> m_characterPools = new Dictionary<string, CharacterPool>();
        private Dictionary<int, SPAWN_DATA> m_object2Pool = new Dictionary<int, SPAWN_DATA>();

        private GameObject m_root;
        private GameObject root
        {
            get
            {
                if (m_root != null)
                    return m_root;

                m_root = new GameObject("CharacterPool");
                GameObject.DontDestroyOnLoad(m_root);
                m_root.transform.localPosition = Vector3.zero;
                m_root.transform.localRotation = Quaternion.identity;
                return m_root;
            }
        }
        
        /// <summary>
        /// 获取对象池
        /// </summary>
        /// <param name="genPart"></param>
        /// <returns></returns>
        public CharacterPool GetOrCreate(string genPart)
        {
            if (m_characterPools.TryGetValue(genPart, out CharacterPool pool))
            {
                return pool;
            }

            pool = new CharacterPool(root.transform, genPart);
            m_characterPools.Add(genPart, pool);
            return pool;
        }

        /// <summary>
        /// 创建对象
        /// </summary>
        /// <param name="pool"></param>
        /// <returns></returns>
        public GameObject Spawn(CharacterPool pool)
        {
            if (!pool.isDone)
            {
                log.Error("pool not readly！");
                return null;
            }
            
            var id = pool.Spawn();
            var obj = pool.GetObject(id);
            var data = new SPAWN_DATA() { pool = pool, id = id };
            var instanceID = obj.GetInstanceID();
            if (m_object2Pool.ContainsKey(instanceID))
            {
                log.Error("spawn same key=" + instanceID);
                return obj;
            }
            m_object2Pool.Add(instanceID, data);
            return obj;
        }

        /// <summary>
        /// 回收对象
        /// </summary>
        /// <param name="obj"></param>
        public bool Despawn(GameObject obj)
        {
            var instanceID = obj.GetInstanceID();
            if (!m_object2Pool.TryGetValue(instanceID, out SPAWN_DATA data))
            {
                log.Error("free object fail");
                return false;
            }

            var tempObj = data.pool.GetObject(data.id);
            if (tempObj != obj)
            {
                log.Error("despawn fail object not match=" + instanceID);
                return false;
            }
            data.pool.Despawn(data.id);
            m_object2Pool.Remove(instanceID);
            return true;
        }
        
        // 将空的对象池清除
        public void ClearEmpty()
        {
            foreach (var pool in m_characterPools.Values)
            {
                if (pool.usedCount == 0)
                {
                    pool.Dispose();
                }
            }
        }
    }
}