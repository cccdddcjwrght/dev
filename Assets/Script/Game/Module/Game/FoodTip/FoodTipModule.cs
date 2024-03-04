using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

namespace SGame
{
    /// <summary>
    /// 食物小费管理
    /// </summary>
    public class FoodTipModule : Singleton<FoodTipModule>
    {
        private EntityManager   m_EntityManager;

        private static readonly float3 CST_HUD_OFFSET = new float3(0, 1.0f, 0);
        
        public FoodTipModule()
        {
            // 注册事件
            EventManager.Instance.Reg<Entity>((int)GameEvent.FOOD_TIP_CLICK, OnFoodClick);
            m_EntityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        }
        
        
        /// <summary>
        /// 创建小费对象
        /// </summary>
        /// <param name="tableID"></param>
        /// <param name="gold"></param>
        /// <returns></returns>
        public Entity CreateTips(double gold, float3 pos)
        {
            // 创建金币对象
            var goldEffect = EffectSystem.Instance.Spawn3d((int)EffectDefine.FODD_TIP_GOLD, null, pos);
            
            // hud 跟随
            var hud = UIUtils.ShowHUD("FoodTip", goldEffect, CST_HUD_OFFSET);
            m_EntityManager.AddComponentData(hud, new UIParam() {Value = goldEffect});
            m_EntityManager.AddComponentData(goldEffect, new FoodTips() {gold = gold, ui = hud});

            // 创建金币特效
            return goldEffect;
        }

        public void CloseTip(Entity e)
        {
            World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<DespawnEntitySystem>().DespawnEntity(e);
        }

        /// <summary>
        /// 食物小费点击
        /// </summary>
        /// <param name="foodTip"></param>
        public void OnFoodClick(Entity foodTip)
        {
            FoodTips food = m_EntityManager.GetComponentData<FoodTips>(foodTip);
            var property = PropertyManager.Instance.GetGroup(PropertyGroup.ITEM);
            
            // 添加金币
            property.AddNum((int)ItemID.GOLD,food.gold);
            
            // 删除对象
            //m_EntityManager.AddComponent<DespawningEntity>(foodTip);
            //m_EntityManager.AddComponent<DespawningEntity>(food.ui);

            // 显示点击特效
            var trans = m_EntityManager.GetComponentData<Translation>(foodTip);
            EffectSystem.Instance.Spawn3d((int)EffectDefine.FOOD_TIP_GOLD_EFFECT, null, trans.Value);

            CloseTip(foodTip);
        }
    }
}