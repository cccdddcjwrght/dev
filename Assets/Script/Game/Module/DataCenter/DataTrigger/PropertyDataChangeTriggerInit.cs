using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace SGame
{
    public partial class PropertyDataChangeTriggerSystem 
    {
        void Init()
        {
            m_triggers = new List<PropertyChangeTrigger>();
            m_triggers.Add(new PropertyChangeTrigger(GameEvent.PROPERTY_GOLD_CHANGE, ItemID.GOLD));
            m_triggers.Add(new PropertyChangeTrigger(GameEvent.PROPERTY_DIAMOND_CHANGE, ItemID.DIAMOND));
        }
    }
}