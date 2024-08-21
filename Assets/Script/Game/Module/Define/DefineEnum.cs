//枚举定义
using System;

namespace SGame
{
	/// <summary>
	/// 星星枚举
	/// </summary>
	public enum EnumStar
	{
		None = 0,
		Green = 1,
		Blue = 2,
		Purple = 3,
		Orange = 4,
	}

	public enum EnumQualityType
	{
		None = 0,
		White = 1,
		Green,
		Blue,
		Purple,
		Orange,
		Red,
		Max = 7
	}

	public enum EnumQuality
	{
		None = 0,
		White = 1,
		Green,
		Blue,
		Purple,
		Purple_1,
		Purple_2,
		Orange,
		Orange_1,
		Orange_2,
		Orange_3,
		Red,
		Max = 12
	}

	/// <summary>
	/// 属性枚举
	/// </summary>
	public enum EnumAttribute : int
	{
		//===================================
		//价格
		Price = 101,
		/// <summary>
		/// 速度
		/// </summary>
		Speed = 102,
		/// <summary>
		/// 下单速度
		/// </summary>
		OrderSpeed = 103,
		/// <summary>
		/// 工作速度
		/// </summary>
		WorkSpeed = 104,
		/// <summary>
		/// 小费
		/// </summary>
		Gratuity = 105,
		/// <summary>
		/// 钻石投资
		/// </summary>
		Diamond = 106,
		/// <summary>
		/// 金币投资
		/// </summary>
		Gold = 107,
		/// <summary>
		/// 关卡初始金币加成
		/// </summary>
		LevelGold = 108,
		/// <summary>
		/// 好评几率
		/// </summary>
		LikeRate = 109,
		/// <summary>
		/// 好评数量
		/// </summary>
		LikeNum = 110,

		//======================================
		/// <summary>
		/// 立即完成几率
		/// </summary>
		ImmediatelyCompleteRate = 201,
		/// <summary>
		/// 完美完成几率
		/// </summary>
		PerfectCompleteRate = 202,
		/// <summary>
		/// 小费几率
		/// </summary>
		GratuityRate = 203,
		/// <summary>
		/// 钻石投资概率
		/// </summary>
		DiamondRate = 204,
		//=======================================
		/// <summary>
		/// 离线时间
		/// </summary>
		OfflineTime = 301,
		/// <summary>
		/// 离线加成
		/// </summary>
		OfflineAddition = 302,
		/// <summary>
		/// 广告收益加成
		/// </summary>
		AdAddition = 303,
		/// <summary>
		/// 广告收益持续时间加成
		/// </summary>
		AdTime = 304,


		//=======================================
		//=================战斗属性===============
		#region 战斗属性-基础
		/// <summary>
		/// 血量-基础
		/// </summary>
		Hp = 10001,
		/// <summary>
		/// 攻击-基础
		/// </summary>
		Attack = 10002,
		/// <summary>
		/// 攻击速度-基础
		/// </summary>
		AtkSpeed = 10003,
		#endregion

		#region 战斗属性-二级属性
		/// <summary>
		/// 闪避
		/// </summary>
		Dodge = 10101,
		/// <summary>
		/// 连击
		/// </summary>
		Combo = 10102,
		/// <summary>
		/// 暴击
		/// </summary>
		Crit = 10103,
		/// <summary>
		/// 眩晕
		/// </summary>
		Stun = 10104,
		/// <summary>
		/// 吸血
		/// </summary>
		Steal = 10105,
		#endregion

		#region 战斗属性-二级属性-抗性
		AntiDodge = 10201,
		AntiCombo = 10202,
		AntiCrit = 10203,
		AntiStun = 10204,
		AntiSteal = 10205,
		#endregion

	}

	/// <summary>
	/// 目标枚举
	/// </summary>
	[Flags]
	public enum EnumTarget
	{
		None = 0,
		/// <summary>
		/// 主角
		/// </summary>
		Player = 1,
		/// <summary>
		/// 厨师
		/// </summary>
		Cook = 1 << 1,//2
		/// <summary>
		/// 服务生
		/// </summary>
		Waiter = 1 << 2,//4
		/// <summary>
		/// 工作台
		/// </summary>
		Machine = 1 << 3,//8
		/// <summary>
		/// 全部工作台
		/// </summary>
		AllMachine = 1 << 4,//16
		/// <summary>
		/// 装备
		/// </summary>
		Equip = 1 << 5,//32
		/// <summary>
		/// 全部装备
		/// </summary>
		AllEquip = 1 << 6,//64
		/// <summary>
		/// 客人
		/// </summary>
		Customer = 1 << 7,//128
		/// <summary>
		/// 投资人
		/// </summary>
		Investor = 1 << 8,//256
		/// <summary>
		/// 游戏全局
		/// </summary>
		Game = 1 << 9,//512
		/// <summary>
		/// 雇佣
		/// </summary>
		Employee = 1 << 10,//1024
		/// <summary>
		/// 探索者
		/// </summary>
		Explorer = 1 << 11,//2048
		/// <summary>
		/// 
		/// </summary>
		Monster = 1 << 12,//4096
		/// <summary>
		/// boss
		/// </summary>
		Boss = 1 << 13, //8192
	}

	/// <summary>
	/// 加成方式
	/// </summary>
	public enum EnumCaluType
	{
		/// <summary>
		/// 固定值
		/// </summary>
		Value = 0,
		/// <summary>
		/// 百分比
		/// </summary>
		Percentage = 1
	}

	/// <summary>
	/// 角色类型
	/// </summary>
	public enum EnumRole
	{
		None = 0,
		Cook = 1,
		Waiter = 2,
		Customer = 3,
		Car = 4,
		Player = 5,
		Employee = 1000,
	}


	public enum EnumShopArea : int
	{
		None = 0,
		Ad = 1,
		Gift = 2,
		Item = 3,
		Eq = 4,
		Diamond = 5,
	}

	/// <summary>
	/// 装备类型
	/// </summary>
	public enum EquipType
	{
		None = 0,
		Hair = 1,
		Clothes = 2,
		Weapon = 3,
		RightWeapon = 4,
		Shoes = 5
	}

	/// <summary>
	/// 来源
	/// </summary>
	public enum EnumFrom
	{
		None,
		/// <summary>
		/// 装备
		/// </summary>
		Equipment = 200001,
		/// <summary>
		/// 关卡科技
		/// </summary>
		LevelTech,
		/// <summary>
		/// 开局奖励buff
		/// </summary>
		Exclusive,
		/// <summary>
		/// 关卡好评buff
		/// </summary>
		RoomLike,
		/// <summary>
		/// 广告buff
		/// </summary>
		Ad,
		/// <summary>
		/// 宠物
		/// </summary>
		Pet,
		/// <summary>
		/// 俱乐部
		/// </summary>
		Club,
		/// <summary>
		/// 无广告
		/// </summary>
		NoAd,
		/// <summary>
		/// 工人收集
		/// </summary>
		WorkerCollect,
	}

	/// <summary>
	/// 物品道具类型
	/// </summary>
	public enum EnumItemType
	{
		None = 0,
		Normal,
		Buff,
		Chest,
		EqLvMat,
		EqUpMat,
		SuitBook,
		SuitMat,
		PetUpMat,
		ChestKey,
		PetEgg = 10,
		Trade,
		Act = 12,//活动
	}

	public enum EnumMachineType
	{
		CUSTOM = 1,     // 顾客桌子
		DISH = 2,       // 放餐机
		MACHINE = 3,    //机器桌子
		JOB = 4,        //招工
		EVOTABLE = 5,   //升级桌
	}
}
