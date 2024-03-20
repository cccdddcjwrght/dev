using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace SGame
{
    [GenerateAuthoringComponent]
    public struct FoodItem : IComponentData
    {
        // 食物类型
        public int itemID; // 道具ID
        public int num;    // 道具数量
    }
}