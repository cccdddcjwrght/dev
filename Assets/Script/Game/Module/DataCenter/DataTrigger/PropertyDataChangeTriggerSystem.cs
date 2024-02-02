using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace SGame
{
    [UpdateInGroup(typeof(GameLogicGroup))]
    public partial class PropertyDataChangeTriggerSystem : SystemBase
    {
        private double m_preGold;
        private ItemGroup m_itemGroup;
        
        protected override void OnCreate()
        {
            m_preGold = 0;
        }

        protected override void OnUpdate()
        {
            if (PropertyManager.Instance.IsInitalize == false)
                return;

            if (m_itemGroup == null)
            {
                m_itemGroup = PropertyManager.Instance.GetGroup(PropertyGroup.ITEM);
            }

            if (m_preGold != m_itemGroup.GetNum((int)ItemID.GOLD))
            {
                // 更新事件
                var newValue = m_itemGroup.GetNum((int)ItemID.GOLD);
                EventManager.Instance.Trigger((int)GameEvent.PROPERTY_GOLD_CHANGE, newValue, newValue - m_preGold);
                m_preGold = newValue;
            }
        }
    }
}