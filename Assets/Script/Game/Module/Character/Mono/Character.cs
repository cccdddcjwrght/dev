using System.Collections;
using System.Collections.Generic;
using GameTools.Paths;
using UnityEngine;
using Unity.Entities;
using Unity.VisualScripting;
using SGame.VS;
using Unity.Mathematics;
using Unity.Transforms;

namespace SGame
{
    /// <summary>
    /// 角色数据处理
    /// </summary>
    public class Character : MonoBehaviour
    {
        /// <summary>
        /// 脚本数据
        /// </summary>
        public GameObject script;

        /// <summary>
        /// 模型数据
        /// </summary>
        public GameObject model;

        /// <summary>
        /// Entity对象
        /// </summary>
        public Entity entity;

        /// <summary>
        /// 角色实例化ID
        /// </summary>
        public int CharacterID = 0;
        
        public Transform pos
        {
            get { return transform; }
        }

        private EntityManager entityManager;
        
        /// <summary>
        /// 初始化角色
        /// </summary>
        /// <param name="entity">角色的ENTITY</param>
        /// <param name="mgr">Entity管理器</param>
        public void OnInitCharacter(Entity entity, EntityManager mgr)
        {
            this.entity = entity;
            this.entityManager = mgr;

            // 触发初始化角色事件
            EventBus.Trigger(CharacterInit.EventHook, script, this);
        }
        

        /// <summary>
        /// 显示等待HUD
        /// </summary>
        /// <param name="progressTime"></param>
        /// <returns></returns>
        public Entity ShowWaitUI(float progressTime)
        {
            Entity ui = UIUtils.ShowHUD("progress", transform, float3.zero);
            entityManager.AddComponent<LiveTime>(ui);
            entityManager.SetComponentData(ui, new LiveTime() {Value =  progressTime});
            return ui;
        }
        

        /// <summary>
        /// 角色移动到目标位置
        /// </summary>
        /// <param name="map_pos"></param>
        public void MoveTo(int2 map_pos)
        {
            // Debug.Log("you move to =" + map_pos.ToString());
            var searchPos = GameTools.MapAgent.GridToIndex(new Vector2Int(map_pos.x, map_pos.y));
            map_pos.x = searchPos.x;
            map_pos.y = searchPos.y;
            float3 pos = entityManager.GetComponentData<Translation>(entity).Value;
            int2 curPos = AStar.GetGridPos(pos);
            
            FindPathParams find = new FindPathParams() { start_pos = curPos, end_pos = map_pos };
            if (!entityManager.HasComponent<FindPathParams>(entity))
            {
                entityManager.AddComponent<FindPathParams>(entity);
            }
            
            entityManager.SetComponentData(entity, find);
        }

        public bool isMoving
        {
            get
            {
                if (entityManager.HasComponent<FindPathParams>(entity))
                    return true;
                
                if (entityManager.HasComponent<PathPositions>(entity) == false)
                    return false;

                if (!entityManager.HasComponent<Follow>(entity))
                    return false;

                var follow = entityManager.GetComponentData<Follow>(entity);
                return follow.Value > 0;
            }
        }
    }
}