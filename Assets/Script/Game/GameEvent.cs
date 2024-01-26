using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame
{
	public enum GameEvent : int
	{
		NONE					   = 0,
		TEST_EVENT1                = 1,
		TEST_EVENT2				   = 2,
		
		

		//登录======================================
		HOTFIX_DONE					= 100, // 热更新结束
		LOGIN_READLY				= 101, // 登录准备好了
		ENTER_LOGIN					= 102, // 进入登录
		ENTER_GAME					= 105, // 进入游戏

		//场景=======================================
		BEFORE_ENTER_ROOM			= 110, //进入场景之前
		ENTER_ROOM					= 111, //进入场景
		AFTER_ENTER_ROOM			= 112, //进入场景之后

		ORDER_NEW = 200, // 创建新的订单 (int orderId)
		ORDER_FOODMAKED				= 201, // 	
		
		PROPERTY_GOLD				= 1000, // 金币属性添加   (int add_gold, long new_gold, int player_id)

		
		//操作台=============================================
		WORK_TABLE_ENABLE			= 2001,//操作台解锁
		WORK_TABLE_UPLEVEL			= 2002,//操作台升级
		WORK_TABLE_MACHINE_ENABLE	= 2003,//操作台点位解锁
		
		//buff===============================================
		BUFF_TRIGGER				= 3001,//触发一个buff （BuffData data)

		//Tech===============================================
		TECH_ADD_REWARD				= 4001,//科技添加奖励 （int tech)


	}
}