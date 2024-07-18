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

		UI_INPUT_LOCK				= 14,     // 锁定UI输入
		UI_INPUT_UNLOCK				= 15,     // UI输入解锁
		CROSS_DAY					= 16,	  // 跨天事件
		
		APP_PAUSE					= 17,     // 游戏中断(bool isPause)
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
		GAME_ENTER_VIEW_STATR		= 118,//进场视频开始
		GAME_ENTER_VIEW_END			= 119,//进场视频结束

		ORDER = 200,                             // 创建新订单 (int 订单ID)
		ORDER_FOODMAKED				= 201,       // 
		ORDER_FINSIH				= 202,       // 订单完成 (int 订单ID)
		ORDER_INSTANT				= 203,       // 订单立即完成 (int 订单ID)
		ORDER_PERFECT				= 204,       // 订单完美制作 (int 订单ID)
		ORDER_START					= 205,       // 顾客创建订单 (int 顾客实例ID)
		
		/// 角色
		CHARACTER_CREATE			= 301, // 角色创建 (int 角色实例ID, int roleID, int roleType)
		CHARACTER_REMOVE			= 302, // 角色销毁 (int 角色实例ID)
		
		/// <summary>
		///  座位
		/// </summary>
		MACHINE_ADD					= 400, // 工作机器添加 (int 座位ID, int 食物类型)
		FOOD_TIP_CLICK				= 401, // 食物小费点击 (Entity 小费对象)
		FOOD_TIP_CREATE				= 402, // 游戏小费创建(int playerID) 

		ITEM_CHANGE					= 999,  //道具变化（int id , long new , long old）
		PROPERTY_GOLD				= 1000, // 金币属性添加   (int add_gold, long new_gold, int player_id)
		PROPERTY_GOLD_CHANGE		= 1001, // 金币更改 (double newValue, double addValue)
		PROPERTY_DIAMOND_CHANGE		= 1002, // 钻石更改 (double newValue, double addValue)
		ITEM_CHANGE_BURYINGPOINT	= 1003, // 道具变化埋点


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
		WORK_AREA_UNLOCK			= 2013,//区域解锁(int areaid)
		WORK_REGION_CLICK			= 2014,//格子点击

		//=============================================================
		COOKBOOK_UP_LV				= 2201,//食谱升级（int cookid , int lv）


		//buff===============================================
		BUFF_TRIGGER = 3001,//触发一个buff （BuffData data)
		BUFF_RESET = 3002,//buff系统重置
		BUFF_ADD_ROLE = 3003,//添加角色(RoleData data)

		//Tech===============================================
		TECH_ADD_REWARD				= 4001,//科技添加奖励 （int tech)
		TECH_ADD_TABLE				= 4002,//科技添加桌子 （int roommachine)
		TECH_ADD_ROLE				= 4003,//科技添加角色 （int roletype,int count,int tableid)
		TECH_LEVEL					= 4004,//全局科技升级

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
		EQUIP_BURYINGPOINT			= 6006,//装备埋点
		EQUIP_NUM_UPDATE			= 6007,//装备数量变化（int id , int count）

		//设置======================================
		SETTING_UPDATE_INT = 8001,     //(int)设置更新
		SETTING_UPDATE_STR				= 8002,     //(str)设置更新
		SETTING_UPDATE_HEAD				= 8003,     //头像更新
		SETTING_UPDATE_NAME				= 8004,     //名字更新
		
		//新手引导===================================
		GUIDE_START                     = 9001,
		GUIDE_STEP                      = 9002,
		GUIDE_CREATE					= 9003,
		GUIDE_FIRST						= 9004,
		GUIDE_LOOP						= 9005,
		STEP_NEXT						= 9006,
		GUIDE_FINISH					= 9007,
		GUIDE_CLICK						= 9008,
		
		// 好友系统
		FRIEND_DATE_UPDATE				= 10000, // 好友数据更新
		FRIEND_HIRING					= 10001, // 好友雇佣 (long playerID, int roleID, RoleData equipData)
		FRIEND_HIRING_TIMEOUT			= 10002, // 好友雇佣倒计时结束
		FRIEND_HIRING_FINISH			= 10003, // 雇佣好友正式删除
		
		FRIEND_CUSTOMER_ENTER			= 10004, // 好友顾客入场(int playerID)

		//关卡内buff
		ROOM_START_BUFF					= 11000, //开局选择buff
		ROOM_LIKE_ADD					= 11001, //好评数量增加
		ROOM_BUFF_RESET					= 11002, //局内buff重置
		ROOM_BUFF_ADD					= 11003, //好评buff添加

		PIGGYBANK_UPDATE				= 12000, //存钱罐刷新



		//宠物==========================================
		PET_LIST_REFRESH				= 13000, //宠物列表刷新
		PET_REFRESH						= 13001, //宠物刷新(PetItem pet,int type)
		PET_ADD							= 13002, //宠物添加
		PET_FOLLOW_CHANGE				= 13003, //宠物跟随修改（PetItem pet,bool state）
		PET_BORN_EVO					= 13004, //宠物吞噬事件（PetItem pet,Action completed)
		PET_BORN						= 13005, //宠物孵化


		ACTIVITY_OPEN					= 14000, // 活动开启(int activeID)
		ACTIVITY_CLOSE					= 14001, // 活动结束(int activeID)
		
		GROW_GIFT_REFRESH				= 14002, // 成长礼包数据刷新

		FLIGHT_SINGLE_CREATE			= 15000, //飞行特效创建
		FLIGHT_LIST_CREATE				= 15001, 
		FLIGHT_LIST_TYPE				= 15002,
		FLIGHT_SINGLE_TYPE				= 15003,

		AD_CLICK						= 16000, //广告点击
		AD_FAILED						= 16001, //广告加载失败
		AD_SHOW							= 16002, //播放广告
		AD_CLOSE						= 16003, //关闭广告
		AD_REWARD						= 16004, //广告奖励

		INVEST_APPEAR					= 17000, //投资人出现
		INVEST_CLICK					= 17001, //投资人点击
		INVEST_DISAPPEAR				= 17002, //投资人消失

		RANK_UPDATE						= 18000, //排行刷新

		TASK_UPDATE						= 19000, //任务刷新
		TASK_BUY_GOOD					= 19001, //任务购买商品


		RECORD_PROGRESS					= 20000, //记录进度

		CLUB_HEAD_SELECT				= 21000, //俱乐部头像选择
		CLUB_MAIN_UPDATE				= 21001, //俱乐部信息刷新
		CLUB_LIST_UPDATE				= 21002, //俱乐部列表刷新
		CLUB_REWARD_UPDATE				= 21003, //俱乐部奖励刷新
		CLUB_MEMBER_REMOVE				= 21004, //俱乐部移除成员
		CLUB_CHANGE_HEAD				= 21005, //俱乐部改变头像

		MAIN_TASK_UPDATE				= 22000, //主线任务刷新

		LEVELPATH_QUEUE_UPDATE					= 23000, // 关卡排队更新 （string pathTag)

		RELOAD_ALL_UI					= 24000, //重新加载所有UI				
		

		LIKE_SPIN						= 25000, //好评奖励id
		
		CUSTOMER_BOOK_UPDATE			= 26000, // 角色图鉴数据刷新

		TOTAL_REFRESH					= 27000, //店铺刷新

		HOTFOOD_REFRESH					= 28000, //热卖商品刷新

		BUFFSHOP_REFRESH				= 29000, //buff商店刷新
	}
}