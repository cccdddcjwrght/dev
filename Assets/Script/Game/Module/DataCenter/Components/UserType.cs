using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame
{
    public enum UserType : uint
    {
        DICE_POWER    = 1, // 骰子剩余数量
        DICE_MAXPOWER = 2, // 骰子最大数量 
        GOLD          = 3, // 金币数量
    }
}
