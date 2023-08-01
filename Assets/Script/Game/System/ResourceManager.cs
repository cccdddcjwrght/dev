using System.Collections.Generic;
using Unity.Entities;
using libx;
using log4net;
using UnityEngine;

namespace SGame
{
    public interface IResourceManager
    {
        
    }
    
    /// <summary>
    /// 该系统为游戏逻辑与底层xasset资源加载库引入一个 中间层, 
    /// 目的有3:
    /// 1. 让游戏逻辑与具体的资源加载插件解耦, 为后续单元测试提供基础
    /// 2. 防止直接使用xasset不当照成不必要的错误
    /// 3. 让一些常用的方法, 比如通过配置表ID加载对象, 可以直接写在这里
    /// </summary>
    public class ResourceManager : IResourceManager
    {
        private GameWorld m_world;
        private static ILog log = LogManager.GetLogger("xl.game.ResourceManager");

        private const string CHARACTER_TEST_PREFAB = "Assets/BuildAsset/Prefabs/Characters/Kiki_v2.prefab";
        private const string CHARACTER_PREFAB = "Assets/BuildAsset/Prefabs/Character.prefab";

        private Dictionary<GameObject, Entity> m_entityPrefab;

        public ResourceManager(GameWorld world)
        {
            m_world = world;
            m_entityPrefab = new Dictionary<GameObject, Entity>(32);
        }

        public GameObject LoadPrefab(string path)
        {
            AssetRequest        req     = Assets.LoadAsset(path, typeof(GameObject));
            if (!string.IsNullOrEmpty(req.error))
            { 
                log.Error("Load Prefab fail=" + req.error);
                return null;
            }
            
            GameObject          prefab  = req.asset as GameObject;
            if (prefab == null)
            {
                log.Error("Load Prefab Fail=" + path);
                return null;
            }

            return prefab;
        }

        public Entity Spawn(string path, Vector3 position, Quaternion rotation)
        {
            GameObject prefab = LoadPrefab(path);
            if (prefab == null)
                return Entity.Null;

            GameObject newObject = m_world.SpawnInternal(prefab, position, rotation, out Entity e);
            return e;
        }

        /// <summary>
        /// 獲得entity預製
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public Entity GetEntityPrefab(string path)
        {
            GameObject prefab = LoadPrefab(path);
            Entity ret = m_world.CoverToEntity(prefab);
            if (ret == Entity.Null)
            {
                log.Error("Cover Fail = " + path);
            }

            return ret;
        }

        /// <summary>
        /// 更根預製創建並轉換成entity
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public Entity SpawnAndCovert(string path)
        {
            // 1. 首先加载prefab
            // 2. 通过world实例化对象
            // 3. 返回对象
            AssetRequest        req     = Assets.LoadAsset(path, typeof(GameObject));
            if (!string.IsNullOrEmpty(req.error))
            { 
                log.Error("Load Prefab fail=" + req.error);
                return Entity.Null;
            }
            
            GameObject          prefab  = req.asset as GameObject;
            if (prefab == null)
            {
                log.Error("Load Prefab Fail=" + path);
                return Entity.Null;
            }
            
            GameObject obj = m_world.SpawnAndCovert(prefab, Vector3.zero, Quaternion.identity, out Entity ret);
            if (ret == Entity.Null)
            {
                log.Error("Cover Fail = " + path);
            }
            
            // 确保资源能够自动释放
            req.Require(obj);
            req.Release();
            return ret;
        }

        public Entity LoadCharacterRender(int charcterId)
        {
            string path = CHARACTER_TEST_PREFAB;
            // 1. 首先加载prefab
            // 2. 通过world实例化对象
            // 3. 返回对象
            
            AssetRequest        req     = Assets.LoadAsset(path, typeof(GameObject));
            if (!string.IsNullOrEmpty(req.error))
            { 
                log.Error("Load Prefab fail=" + req.error);
                return Entity.Null;
            }
            
            GameObject          prefab  = req.asset as GameObject;
            if (prefab == null)
            {
                log.Error("Load Prefab Fail=" + path);
                return Entity.Null;
            }
            
            GameObject obj = m_world.SpawnInternal(prefab, Vector3.zero, Quaternion.identity, out Entity ret);
            if (ret == Entity.Null)
            {
                log.Error("Cover Fail = " + path);
            }
            
            // 确保资源能够自动释放
            req.Require(obj);
            req.Release();
            return ret;
        }

        // 加载角色对象
        public Entity LoadCharacter(int charcterId)
        {
            return SpawnAndCovert(CHARACTER_PREFAB);
        }
    }
}