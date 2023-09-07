using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame
{
	public enum GameEvent : int
	{
		NET_GAME_CONNECT_SUCCESS	= 1,	// 网络连接事件
		NET_GAME_CONNECT_FAIL		= 2,    // 网络连接失败           (int err, string msg)
		NET_GAME_DISCONNECTED		= 3,    // 网络断开连接事件        int, string
		NET_GAME_EVENT				= 4,    // 网络数据事件           (GamePackage)

		NET_BATTLE_CONNECT_SUCCESS	= 10,	// 网络连接事件
		NET_BATTLE_CONNECT_FAIL		= 11,   // 网络连接失败           (int err, string msg)
		NET_BATTLE_DISCONNECTED		= 12,   // 网络断开连接事件         int, string
		NET_BATTLE_EVENT			= 13,   // 网络战斗包事件         (GamePackage)

		//登录======================================
		HOTFIX_DONE					= 100, // 热更新结束
		ENTER_LOGIN					= 104, // 进入登录
		ENTER_GAME					= 105, // 进入游戏
		

		GAME_WAIT_NEXTROUND			= 106, // 等待下一局
		GAME_NEXTROUND				= 107, // 运行下一局
		
		PROPERTY_GOLD				= 1000, // 金币属性添加   (int add_gold, long new_gold, int player_id)
		PROPERTY_BANK				= 1001, // 银行存款或取款  (int add_gold, long new_value, int buildingId, int player_id)
		PROPERTY_TRAVEL_GOLD		= 1002, // 出行金币       (int add_gold, long new_gold, int player_id)
		
		PLAYER_ROTE_DICE			= 2000, // 用户操作骰子
		PLAYER_POWER_DICE			= 2001, // 用户更改倍率设置
		
		TRAVEL_TRIGGER				= 3000, // 触发出行 (bool isTravel)
		
		TRAVEL_START				= 3001, // (bool isTravel)触发出行开始 
		TRAVEL_END   				= 3003, // (bool isTravel)触发出行结束 

	}
}