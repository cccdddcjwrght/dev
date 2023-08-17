using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace SGame
{
    public struct UserSetting : IComponentData
    {
        public int doubleBonus; // 倍率数量, 默认以北
        public int maxBonus;    // 最大倍率

        public bool autoUse;    // 自动骰子

        public static UserSetting GetDefault()
        {
            UserSetting v = new UserSetting()
            {
                doubleBonus = 1,
                autoUse = false
            };
            return v;
        }
    }
}
