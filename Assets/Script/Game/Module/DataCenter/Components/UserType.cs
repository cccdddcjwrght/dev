using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame
{
    public enum UserType : uint
    {
        DICE_NUM        = 1, // 骰子剩余数量
        DICE_MAXNUM     = 2, // 骰子最大数量 
        GOLD            = 3, // 金币数量
        POS             = 4, // 位置信息
        
        TRAVEL_PLAYERID = 5, // 记录的出行对象ID
    }
}
