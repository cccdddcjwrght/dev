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
        TRAVEL_GOLD     = 6, // 使用出行金币
        TRAVEL          = 7, // 出行状态 0 表示普通状态, 1表示出行状态
        
        DICE_POWER      = 100,      // 骰子设置数量
        TRAVEL_DICE_POWER = 101,    // 出行时保存的骰子数量
    }
}
