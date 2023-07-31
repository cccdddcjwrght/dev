using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameEvent : int
{
    NET_GAME_CONNECT_SUCCESS    = 1,        // 网络连接事件
    NET_GAME_CONNECT_FAIL       = 2,        // 网络连接失败           (int err, string msg)
    NET_GAME_DISCONNECTED       = 3,        // 网络断开连接事件        int, string
    NET_GAME_EVENT              = 4,        // 网络数据事件           (GamePackage)

    NET_BATTLE_CONNECT_SUCCESS  = 10,       // 网络连接事件
    NET_BATTLE_CONNECT_FAIL     = 11,       // 网络连接失败           (int err, string msg)
    NET_BATTLE_DISCONNECTED     = 12,       // 网络断开连接事件         int, string
    NET_BATTLE_EVENT            = 13,        // 网络战斗包事件         (GamePackage)


	//tips 预留ID
	TIPS_ID_EVENT				= 100000,

	//登录======================================
	LOGIN_SUCCESS				= 100,
	USER_DATA_REFRESH			= 101,
	LOAD_USER_INFO				= 102,		// 登陆后加载用户数据
	ANNOUNCE_DONE				= 103,		// 公告结束
	ENTER_LOGIN					= 104,		//进入登录
	RECONNECTING				= 105,		//重连 （bool start or end）
	
	SYNC_COMPLETE				= 110,		//数据同步选择完成(int 1:本地，2：服务器)

	GAME_UPDATE_START			= 150,      //更新  (int 强更 or  热更)
	UI_CURRENCY_ANI_REFRESH		= 151,

	//UI========================================
	UI_OPEN = 201,
	UI_CLOSE					= 202,
	UI_CREATE					= 203,
	LANGUAGE_CHANGE				= 204,     // 多语言改变
	//道具=======================================
	ITEM_NUM_CHANGE             = 1000,     // 物品数量更改         (int item_type, int oldValue, int newValue)
    
    CARD_REWARD					= 1001,		// 牌局奖励

	ITEM_USE					= 1002,		//道具使用
    ITEM_DATA_REFRESH			= 1010,		//数据刷新
	ITEM_USE_RECORDE			= 1011,     //使用记录（类型【FlagType】，ID ，数量 ，来源配置+界面）

	MASK_CLICK					= 1020,		// 蒙版被点击了

    
    //皮肤数据更改
    SKIN_CHANGE					= 1100,     
    //皮肤状态更改(解锁)
    SKINSTATE_CHANGE            =1050,
    
    
    PASSPORT_REWARD_LEVEL		= 1200,		// 触发赛季等级奖励
    PASSPORT_REWARD_CHEST		= 1201,		// 赛季循环宝箱奖励
    PASSPORT_REWARD_BASE	    = 1202,		// 赛季基础奖励
    
    PASSPORT_REWARD_DRESSUP	    = 1203,		// int 装扮ID， int 装扮数量
    PASSPORT_UPDATE				= 1204,		// 通行证数据更新
	PASSPORT_STAR		        = 1205,		// 使用星星道具
    
    // 排行榜
    RANK_LIST_UPDATE			= 1300,	    // 排行榜数据更新
    RANK_REWARD					= 1301,		// 排行榜奖励推送
    
    //通行证奖励事件
    //PASSPORT_REWARD_ITEM	    = 1200,	// int赛季ID, int nodeId, int itemId 道具， int num 道具数量
    //PASSPORT_ITEM_DRESSUP	    = 1201, // int赛季ID, int nodeId, int 装扮id     ， int num 道具数量
    //PASSPORT_ITEM_REWARD	    = 1200, // int赛季ID, int nodeId, int itemId 道具， int num 道具数量
    
    // 战斗管卡
    BATTLE_FINISH               = 2001,     // 战斗结束
    BATTLE_START				= 2002,		// 战斗开始 
    LEVEL_FINISH				= 2100,		// 关卡结算结束
    BATTLE_RESTORE_OPEN			= 2101,		// 关卡结算更新

	//==========================================

	SHIP_CHNAGE					= 6001,     //船只切换

    //卡包======================================
	CARD_LIST_UPDATE			= 7001,		//卡包更新
	CARD_ADD					= 7002,     //卡包新增
	CARD_DEL					= 7003,     //卡包回收
	CARD_COMPLETE				= 7004,		//卡组收集完成	

	//设置======================================
	SETTING_UPDATE_INT				= 8001,     //(int)设置更新
	SETTING_UPDATE_STR				= 8002,     //(str)设置更新

	//商城======================================
	SHOP_LIST_UPDATE				= 10001,	//商城商品列表
	SHOP_BUY_RESULT					= 10002,    //商城购买获得
	SHOP_PAY_PASS					= 10003,    //商城支付通过
	SHOP_RECHARGE_START				= 10004,    //支付开始
	SHOP_RECHARGE_COMPLETE			= 10005,    //支付完成
	SHOP_RECHARGE_FAIL				= 10006,    //支付失败
	SHOP_RECHARGE_GET				= 10007,    //支付失败
	SHOP_BUY_CONSUME				= 10008,    //商城购买消耗





	//海图======================================
	SEAMAP_CHIP_USE = 14001,	//海图碎片使用，随机岛屿列表
	SEAMAP_ISLAND_ADD				= 14002,	//岛屿激活
	SEAMAP_SWITCH_ISLAND			= 14003,    //切换岛屿(切换赛季)
	ISLAND_DECOR_UNLOCK		        = 14004,	//装扮点解锁（true:动画开始，false：动画结束）
	ISLAND_DECOR_UNLOCK_FINISH      = 14005,    

	//邮件======================================
	MAIL_READ						= 15001,	//标记邮件
	
	GM_EXCUTE						= 16001,    // GM指令执行中 cmd value1 value2

	//活动======================================
	ACTIVITY_UPDATE					= 17001,    //活动更新
	ACTIVITY_CLOSE					= 17002,    //活动关闭

	// 大厅事件
	LOBBY_FIRST_ENTER				= 18001,   // 大厅第一次进入
	LOBBY_ENTER_ISLAND				= 18002,   // 进入岛屿
	LOBBY_START_LOADING				= 18003,   //开始加载大厅

	//新手引导事件=============================
	CARD_MOVE                       = 19000,   //卡牌移动 uint, bool (移动的卡牌点数, 是否是撤销功能)
	GUIDE_UPDATE_TIP                = 19001,   //更新tip数据
	GUIDE_CLICK_TIP                 = 19002,   //点击tips
	STOP_SCROLL                     = 19003,   //通行证滑动   
	GUIDE_STEP						= 19007,   //每一步新手引导（int 引导id）
	GUIDE_END                       = 19008,   //强制引导流程完成
	ITEM_USE_FINISH					= 19009,   // 抽牌道具

		//流程====================================
	REWARD_ANIM_FINISH              = 19004,   //结算动画结束
	REWARD_ANIM                     = 19005,   //结算动画
	FINGER_ANIM                     = 19006,   //手指动画

	//广告====================================
	AD_STATE_CHANGE					= 20001,//广告状态变化

	//邮件====================================
	MAIL_GET_REWARD					= 21001,//邮件获取奖励
}
