using System.Collections;
using System.Collections.Generic;
using libx;
using log4net;
using SGame;
using UnityEngine;
using Unity.Entities;

/// <summary>
/// 批量转换GameObject 预制对象
/// </summary>
public class PrefabsToEntityConverter : MonoBehaviour, IDeclareReferencedPrefabs, IConvertGameObjectToEntity
{
    private List<GameObject> _gameObjects;
 
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        for (var i = 0; i < _gameObjects.Count; i++)
        {
            var prefabEntity = conversionSystem.GetPrimaryEntity(_gameObjects[i]);
            //We got prefab entity, store it in global map ID <-> Prefab Entity or where you want.
            ECSPrefabManager.Instance.AddEntity(prefabEntity);
        }
 
        dstManager.DestroyEntity(entity);
        GameObject.Destroy(gameObject);
    }
 
    public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
    {
        referencedPrefabs.AddRange(_gameObjects);
    }
 
    public void ProcessPrefabs(List<GameObject> prefabs)
    {
        _gameObjects = prefabs;
    }
}

/// <summary>
/// ECS里面的预制管理
/// </summary>
public class ECSPrefabManager : MonoSingleton<ECSPrefabManager>
{
    private static ILog log = LogManager.GetLogger("game.ecsprefab");
    private List<AssetRequest> m_requestList;
    private List<string>       m_requestString;

    public List<Entity>        m_prefabEntity;

    protected override void Awake()
    {
        base.Awake();
        m_requestList    = new List<AssetRequest>();
        m_requestString = new List<string>();
        m_prefabEntity  = new List<Entity>();
    }

    public void AddEntity(Entity e)
    {
        m_prefabEntity.Add(e);
    }

    // Update is called once per frame
    void Update()
    {
        if (Game.Instance.IsInitalize == false)
            return;

        if (m_requestString.Count != 0)
        {
            foreach (var req in m_requestString)
                m_requestList.Add(Assets.LoadAssetAsync(req, typeof(GameObject)));
            m_requestString.Clear();
        }
        
        if (m_requestList.Count == 0)
            return;
        
        // 等待所有加载请求都完毕
        foreach (var item in m_requestList)
        {
            if (item.isDone == false)
                return;
        }
        
        // 所有GameObject 都已经加载完毕
        List<GameObject> gameObjects = new List<GameObject>(m_requestList.Count);
        foreach (var item in m_requestList)
        {
            if (string.IsNullOrEmpty(item.error))
            {
                var prefab = item.asset as GameObject;
                if (prefab != null)
                {
                    gameObjects.Add(item.asset as GameObject);
                }
                else
                {
                    log.Error("prefab is null");
                }
            }
            else
            {
               log.Error("load prefab fail=" + item.error);
            }
        }
        
        // 转换GameObject
        if (gameObjects.Count > 0)
        {
            GameObject obj = new GameObject("coverting");
            var converter = obj.AddComponent<PrefabsToEntityConverter>();
            var cte = obj.AddComponent<ConvertToEntity>();
            cte.ConversionMode = ConvertToEntity.Mode.ConvertAndDestroy;
            converter.ProcessPrefabs(gameObjects);
        }
        m_requestList.Clear();
    }

    /// <summary>
    /// 加载预制, 并转换
    /// </summary>
    /// <param name="path"></param>
    public void AddPrefab(string path)
    {
        m_requestString.Add(path);
    }
}
