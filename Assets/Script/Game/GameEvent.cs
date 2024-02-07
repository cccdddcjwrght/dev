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
		LANGUAGE_CHANGE				= 106, // 语言修改
		GAME_START					= 107, // 游戏开始

		//场景=======================================
		BEFORE_ENTER_ROOM			= 110, //进入场景之前
		ENTER_ROOM					= 111, //进入场景
		AFTER_ENTER_ROOM			= 112, //进入场景之后

		ORDER					= 200, // 创建新的订单失败
		ORDER_FOODMAKED				= 201, // 
		
		/// 角色
		CHARACTER_CREATE			= 301, // 角色创建 (int 角色类型, int 角色实例ID)
		CHARACTER_REMOVE			= 302, // 角色销毁 (int 角色类型, int 角色实例ID)
		
		/// <summary>
		///  座位
		/// </summary>
		MACHINE_ADD					= 400, // 工作机器添加 (int 座位ID, int 食物类型)

		PROPERTY_GOLD				= 1000, // 金币属性添加   (int add_gold, long new_gold, int player_id)
		PROPERTY_GOLD_CHANGE		= 1001, // 金币更改 (double newValue, double addValue)
		PROPERTY_DIAMOND_CHANGE		= 1002, // 钻石更改 (double newValue, double addValue)

		
		//操作台=============================================
		WORK_TABLE_ENABLE			= 2001,//操作台解锁
		WORK_TABLE_UPLEVEL			= 2002,//操作台升级
		WORK_TABLE_MACHINE_ENABLE	= 2003,//操作台点位解锁
		WORK_TABLE_CLICK			= 2004,//工作台点击
		WORK_TABLE_UP_STAR			= 2005,//工作台升星


		//buff===============================================
		BUFF_TRIGGER = 3001,//触发一个buff （BuffData data)

		//Tech===============================================
		TECH_ADD_REWARD				= 4001,//科技添加奖励 （int tech)
		TECH_ADD_TABLE				= 4002,//科技添加桌子 （int roommachine)
		TECH_ADD_ROLE				= 4003,//科技添加角色 （int roletype,int count,int tableid)


	}
}