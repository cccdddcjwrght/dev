    using System;
using System.Collections;
using Fibers;
using GameConfigs;
using GameTools;
using GameTools.Paths;
using log4net;
using UnityEngine;
using Unity.Entities;
using Unity.VisualScripting;
using SGame.VS;
using Unity.Mathematics;
using Unity.Transforms;
using System.Collections.Generic;
    using libx;


    namespace SGame
{
    /// <summary>
    /// 角色数据处理
    /// </summary>
    public partial class Character
    {
        /// <summary>
        /// 初始化角色
        /// </summary>
        /// <param name="entity">角色的ENTITY</param>
        /// <param name="mgr">Entity管理器</param>
        public void OnInitCharacter(Entity entity, EntityManager mgr, CharacterSpawnResult result)
        {
            this.entity         = entity;
            this.entityManager  = mgr;
            m_orderRecord.Initalize(this.CharacterID);

            if (!ConfigSystem.Instance.TryGet(roleID, out m_roleConfig))
            {
                log.Error("role id not found=" + roleID);
            }

            m_workAreaMask = 0;
            if (m_roleConfig.WorkerAreaLength > 0)
            {
                for (int i = 0; i < m_roleConfig.WorkerAreaLength; i++)
                {
                    m_workAreaMask = BitOperator.Set(m_workAreaMask, m_roleConfig.WorkerArea(i), true);
                }
            }
            else
            {
                m_workAreaMask = BitOperator.Set(0, 0, true);
            }

            // 触发初始化角色事件
            m_init.Start(RunInit(result));
        }

        /// <summary>
        /// 初始化形象
        /// </summary>
        /// <returns></returns>
        IEnumerator InitModel()
        {
            ConfigSystem.Instance.TryGet(m_roleConfig.Model, out GameConfigs.roleRowData config);
            var pool = CharacterFactory.Instance.GetOrCreate(config.Part);
            while (!pool.isDone)
                yield return null;
            
            // 创建对象
            GameObject ani = CharacterFactory.Instance.Spawn(pool); //loading.pool.GetObject(id);
            ani.transform.SetParent(transform, false);
            ani.transform.localRotation = Quaternion.identity;
            ani.transform.localPosition = Vector3.zero;
            ani.transform.localScale = Vector3.one;
            ani.name = "Model";
            ani.SetActive(true);
            if (config.RoleScaleLength == 3)
            {
                var scaleVector = new Vector3(config.RoleScale(0), config.RoleScale(1), config.RoleScale(2));
                ani.transform.localScale = scaleVector;
            }

            model = ani;
            SetLooking(pool.config);
            
            modelAnimator       = model.GetComponent<Animator>();
            m_slot              = gameObject.AddComponent<Equipments>();
        }
        
        
        /// <summary>
        /// 初始化AI
        /// </summary>
        /// <returns></returns>
        IEnumerator InitAI()
        {
            // 等待上一个AI结束
            var waitReq = AILoader.Instance.AddWait();
            yield return waitReq;
            
            /// 获得AI配置
            string configAI = "";
            ConfigSystem.Instance.TryGet(m_roleConfig.Model, out GameConfigs.roleRowData config);
            configAI = config.Ai;
            if (roleAI != 0)
            {
                if (!ConfigSystem.Instance.TryGet(roleAI, out GameConfigs.roleRowData aiConfig))
                {
                    log.Error("role ai config not found=" + roleAI);
                    yield break;
                }
                configAI = aiConfig.Ai;
            }
            else if (playerID != 0)// && !string.IsNullOrEmpty(config.FriendAI))
            {
                if (!string.IsNullOrEmpty(config.FriendAI))
                {
                    // 好友AI
                    configAI = config.FriendAI;
                }
            }

            var ai_path = Utils.GetAIPath(configAI);
            var req = Assets.LoadAssetAsync(ai_path, typeof(GameObject));
            yield return req;
            var prefab = req.asset as GameObject;
            var ai = GameObject.Instantiate(prefab);
            ai.transform.parent = transform;
            ai.transform.localRotation = Quaternion.identity;
            ai.transform.localPosition = Vector3.zero;
            ai.transform.localScale = Vector3.one;
            ai.name = "AI";
            
            script = ai;
            
            if (script != null)
                EventBus.Trigger(CharacterInit.EventHook, script, this);
        }

        IEnumerator RunInit(CharacterSpawnResult result)
        {
            yield return InitModel();
            yield return InitAI();

            if (result != null)
            {
                result.entity = entity;
                result.characterID = CharacterID;
            }
            
            entityManager.AddComponent<CharacterSpawnSystem.CharacterInitalized>(entity);
            EventManager.Instance.AsyncTrigger((int)GameEvent.CHARACTER_CREATE, 
                CharacterID, 
                roleID, 
                roleType);
        }
    }
}