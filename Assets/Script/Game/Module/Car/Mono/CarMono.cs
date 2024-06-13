using System;
using System.Collections;
using System.Collections.Generic;
using Fibers;
using UnityEngine;
using Unity.Entities;
using libx;
using log4net;

namespace SGame
{
    public class CarMono : MonoBehaviour
    {
        private Entity m_entity;                     // 车的ENTITY      
        private GameConfigs.CarDataRowData m_config; // 配置表
        private GameObject m_ai;
        private GameObject m_model; // 模型

        private Fiber m_logic; // 逻辑运行
        private static ILog log = LogManager.GetLogger("game.car");
        private EntityManager EntityManager;
        private const string ASSET_PATH = "Assets/BuildAsset/";
        
        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="e"></param>
        /// <param name="config"></param>
        public bool Initalize(EntityManager entityManager, Entity e,  int id)
        {
            if (!ConfigSystem.Instance.TryGet(id, out GameConfigs.CarDataRowData config))
            {
                log.Error("car config not found=" + id);
                return false;
            }

            EntityManager = entityManager;
            m_entity = e;
            m_config = config;
            EntityManager.SetComponentData(e, new Speed(){Value =  config.MoveSpeed});
            m_logic = FiberCtrl.Pool.Run(Logic());
            return true;
        }

        /// <summary>
        /// 加载AI脚本
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        AssetRequest LoadAI(string name)
        {
            string path = "Assets/BuildAsset/VisualScript/Prefabs/AI/" + name + ".prefab";
            return Assets.LoadAssetAsync(path, typeof(GameObject));
        }

        /// <summary>
        /// 加载资源
        /// </summary>
        /// <returns></returns>
        IEnumerator LoadResources()
        {
            var aiReq = LoadAI(m_config.AI);
            var modelRequest = Assets.LoadAssetAsync(ASSET_PATH + m_config.Model, typeof(GameObject));
            yield return aiReq;
            yield return modelRequest;
            aiReq.Release();
            modelRequest.Release();
            aiReq.Require(gameObject);
            modelRequest.Require(gameObject);

            if (!string.IsNullOrEmpty(aiReq.error) || !string.IsNullOrEmpty(modelRequest.error))
            {
                log.Error("load car resource fail!");
                yield break;
            }
            
            m_model = GameObject.Instantiate(modelRequest.asset as GameObject, transform);
            m_ai = GameObject.Instantiate(aiReq.asset as GameObject, transform);
        }

        /// <summary>
        /// 汽车线性逻辑代码
        /// </summary>
        /// <returns></returns>
        IEnumerator Logic()
        {
            yield return LoadResources();
            if (m_model == null || m_ai == null)
                yield break;

            // 设置位置线性
            m_ai.transform.localPosition = Vector3.zero;
            m_ai.transform.localRotation = Quaternion.identity;
            
            // 设置角色位置
            if (m_config.PositionLength != 3)
                m_model.transform.localPosition = Vector3.zero;
            else
                m_model.transform.localPosition = new Vector3(m_config.Position(0), m_config.Position(1), m_config.Position(2));

            if (m_config.RotationLength != 3)
                m_model.transform.localRotation = Quaternion.identity;
            else
                m_model.transform.localRotation = Quaternion.Euler(m_config.Rotation(0), m_config.Rotation(1), m_config.Rotation(2));
            m_model.transform.localScale = new Vector3(m_config.Scale, m_config.Scale, m_config.Scale);
            
        }

        private void OnDestroy()
        {
            if (m_logic != null)
            {
                m_logic.Terminate();
                m_logic = null;
            }
        }
    }
}
