using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame
{
	public enum GameEvent : int
	{
		NET_GAME_CONNECT_SUCCESS = 1, // 网络连接事件
		NET_GAME_CONNECT_FAIL = 2,    // 网络连接失败           (int err, string msg)
		NET_GAME_DISCONNECTED = 3,    // 网络断开连接事件        int, string
		NET_GAME_EVENT = 4,           // 网络数据事件           (GamePackage)

		NET_BATTLE_CONNECT_SUCCESS = 10, // 网络连接事件
		NET_BATTLE_CONNECT_FAIL = 11,    // 网络连接失败           (int err, string msg)
		NET_BATTLE_DISCONNECTED = 12,    // 网络断开连接事件         int, string
		NET_BATTLE_EVENT = 13,           // 网络战斗包事件         (GamePackage)


		//tips 预留ID
		TIPS_ID_EVENT = 100000,

		//登录======================================
		HOTFIX_DONE       = 100, // 热更新结束
		ENTER_LOGIN       = 104, // 进入登录
		ENTER_GAME        = 105, // 进入游戏

		SYNC_COMPLETE = 110, //数据同步选择完成(int 1:本地，2：服务器)

		GAME_UPDATE_START = 150, //更新  (int 强更 or  热更)
		UI_CURRENCY_ANI_REFRESH = 151,

		PLAYER_ROTE_DICE = 2000, // 用户操作骰子
	}
}