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

		UI_INPUT_LOCK				= 14,       // 锁定UI输入
		UI_INPUT_UNLOCK				= 15,     // UI输入解锁
		CROSS_DAY					= 16,	  // 跨天事件
								   //登录======================================
		HOTFIX_DONE = 100, // 热更新结束
		LOGIN_READLY				= 101, // 登录准备好了
		ENTER_LOGIN					= 102, // 进入登录
		ENTER_GAME					= 105, // 进入游戏
		LANGUAGE_CHANGE				= 106, // 语言修改
		GAME_START					= 107, // 游戏开始
		DATA_INIT_COMPLETE			= 108, // 数据初始化完成
		LOGIN_COMPLETE				= 109, // 登录完成

		//场景=======================================
		BEFORE_ENTER_ROOM			= 110, //进入场景之前
		ENTER_ROOM					= 111, //进入场景
		AFTER_ENTER_ROOM			= 112, //进入场景之后
		SCENE_REWARD				= 113,//场景宝箱（int cellIndex,  , Action call ,[string assetPath = null]）
		PREPARE_LEVEL_ROOM			= 114,//准备离开房间
		GAME_MAIN_REFRESH           = 115,//游戏刷新       
		GAME_ENTER_SCENE_EFFECT_END = 116,//进场表现结束
		ENTER_NEW_ROOM				= 117,//进入新场景

		ORDER = 200, // 创建新订单 (int 订单ID)
		ORDER_FOODMAKED				= 201, // 
		ORDER_FINSIH				= 202, // 订单完成 (int 订单ID)
		ORDER_INSTANT				= 203, // 订单立即完成 (int 订单ID)
		ORDER_PERFECT				= 204, // 订单完美制作 (int 订单ID)
		ORDER_START					= 205, // 顾客创建订单 (int 顾客实例ID)
		
		/// 角色
		CHARACTER_CREATE			= 301, // 角色创建 (int 角色实例ID)
		CHARACTER_REMOVE			= 302, // 角色销毁 (int 角色实例ID)
		
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
		WORK_TABLE_ALL_MAX_LV		= 2006,//所有工作台等级上限
		WORK_TABLE_CLICK_SIMULATE	= 2007,//模拟工作台点击（int machinetype , int id）

		WORK_COOK_START				= 2010,//工作台开始工作 （int2 cell）
		WORK_COOK_COMPLETE			= 2011,//工作台工作完成 （int2 cell）
		WORK_HUD_SHOW				= 2012,//hud显示完成

		//buff===============================================
		BUFF_TRIGGER = 3001,//触发一个buff （BuffData data)
		BUFF_RESET = 3002,//buff系统重置
		BUFF_ADD_ROLE = 3003,//添加角色(RoleData data)

		//Tech===============================================
		TECH_ADD_REWARD				= 4001,//科技添加奖励 （int tech)
		TECH_ADD_TABLE				= 4002,//科技添加桌子 （int roommachine)
		TECH_ADD_ROLE				= 4003,//科技添加角色 （int roletype,int count,int tableid)

		//Shop===============================================
		SHOP_REFRESH				= 5001,//商城刷新
		SHOP_GOODS_BUY_RESULT		= 5002,//商品购买
		SHOP_BOOST_BUY				= 5003,//buff购买(id,level)

		//===================================================
		EQUIP_REFRESH				= 6001,//装备刷新
		EQUIP_ADD					= 6002,//装备添加
		ROLE_EQUIP_CHANGE			= 6003,//装备穿戴变化
		ROLE_EQUIP_PUTON			= 6004,//装备穿戴（int id）
		ROLE_PROPERTY_REFRESH		= 6005,//角色属性变化


		//设置======================================
		SETTING_UPDATE_INT = 8001,     //(int)设置更新
		SETTING_UPDATE_STR				= 8002,     //(str)设置更新
		SETTING_UPDATE_HEAD				= 8003,     //头像更新
		SETTING_UPDATE_NAME				= 8004,     //名字更新
		
		//新手引导===================================
		GUIDE_START                     = 9001,
		GUIDE_STEP                      = 9002,

		
		// 好友系统
		FRIEND_DATE_UPDATE				= 10000, // 好友数据更新
		FRIEND_HIRING					= 10001, // 好友雇佣 (int playerID, int roleID, RoleData equipData)
		FIREND_HIRING_TIMEOUT			= 10002, // 好友雇佣倒计时结束
		FIREND_HIRING_FINISH			= 10003, // 雇佣好友正式删除

		//开局局内buff
		ROOM_START_BUFF					= 11000, //添加buff

	}
}