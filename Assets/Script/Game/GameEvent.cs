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
		
		ON_UI_MASK_SHOW				= 10, // 显示MASK 事件, (UIContext context)
		ON_UI_MASK_HIDE				= 11, // 隐藏MASK 事件, (UIContext context)

		UI_SHOW						= 12,//(UIContext context)
		UI_HIDE						= 13,//(UIContext context)


		//登录======================================
		HOTFIX_DONE = 100, // 热更新结束
		LOGIN_READLY				= 101, // 登录准备好了
		ENTER_LOGIN					= 102, // 进入登录
		ENTER_GAME					= 105, // 进入游戏
		LANGUAGE_CHANGE				= 106, // 语言修改
		GAME_START					= 107, // 游戏开始

		//场景=======================================
		BEFORE_ENTER_ROOM			= 110, //进入场景之前
		ENTER_ROOM					= 111, //进入场景
		AFTER_ENTER_ROOM			= 112, //进入场景之后
		SCENE_REWARD				= 113,//场景宝箱（int cellIndex,  , Action call ,[string assetPath = null]）

		ORDER					= 200, // 创建新的订单失败
		ORDER_FOODMAKED				= 201, // 
		
		/// 角色
		CHARACTER_CREATE			= 301, // 角色创建 (int 角色类型, int 角色实例ID)
		CHARACTER_REMOVE			= 302, // 角色销毁 (int 角色类型, int 角色实例ID)
		
		/// <summary>
		///  座位
		/// </summary>
		MACHINE_ADD					= 400, // 工作机器添加 (int 座位ID, int 食物类型)
		FOOD_TIP_CLICK				= 401, // 食物小费点击 (Entity 小费对象)

		ITEM_CHANGE					= 999,  //道具变化（int id , long new , long old）
		PROPERTY_GOLD				= 1000, // 金币属性添加   (int add_gold, long new_gold, int player_id)
		PROPERTY_GOLD_CHANGE		= 1001, // 金币更改 (double newValue, double addValue)
		PROPERTY_DIAMOND_CHANGE		= 1002, // 钻石更改 (double newValue, double addValue)

		
		//操作台=============================================
		WORK_TABLE_ENABLE			= 2001,//操作台解锁
		WORK_TABLE_UPLEVEL			= 2002,//操作台升级
		WORK_TABLE_MACHINE_ENABLE	= 2003,//操作台点位解锁
		WORK_TABLE_CLICK			= 2004,//工作台点击
		WORK_TABLE_UP_STAR			= 2005,//工作台升星

		WORK_COOK_START				= 2010,//工作台开始工作 （int2 cell）
		WORK_COOK_COMPLETE			= 2011,//工作台工作完成 （int2 cell）


		//buff===============================================
		BUFF_TRIGGER = 3001,//触发一个buff （BuffData data)

		//Tech===============================================
		TECH_ADD_REWARD				= 4001,//科技添加奖励 （int tech)
		TECH_ADD_TABLE				= 4002,//科技添加桌子 （int roommachine)
		TECH_ADD_ROLE				= 4003,//科技添加角色 （int roletype,int count,int tableid)

		//Shop===============================================
		SHOP_REFRESH				= 5002,//商城刷新
		SHOP_GOODS_BUY_RESULT		= 5002,//商品购买
		
		//设置======================================
		SETTING_UPDATE_INT				= 8001,     //(int)设置更新
		SETTING_UPDATE_STR				= 8002,     //(str)设置更新
		SETTING_UPDATE_HEAD				= 8003,     //头像更新
		SETTING_UPDATE_NAME				= 8004,     //名字更新
		
	}
}