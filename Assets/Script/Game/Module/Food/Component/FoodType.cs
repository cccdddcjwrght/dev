using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace SGame
{
    [GenerateAuthoringComponent]
    public struct FoodType : IComponentData
    {
        // 食物类型
        public int Value;
        public int num;
    }
}