using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame
{
    public enum FunctionID : int
    {
        TECH	   = 17, // 全局科技skill
        ROLE_EQUIP = 12, // 角色装备
        MAP		   = 15, // 地图
        LEVEL_TECH = 13, // 关卡科技
        SHOP	   = 10, // 商城
        FRIEND	   = 14, // 好友
        PET        = 24, // 宠物
        TREASURE   = 25, // 宝箱仓库
        TASK       = 32, // 任务
        RECIPE     = 33, // 食谱
        FRIEND_CUSTOMER = 35, // 好友客人功能定义 
        HOT_FOOD   =    37,   //热卖菜品
		EXPLORE	   = 41,//探索
        DAILY_TASK = 43,//日常任务
    }
}