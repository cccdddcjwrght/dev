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

	public enum EnumQuality
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

	/// <summary>
	/// 属性枚举
	/// </summary>
	public enum EnumAttribute
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
	}

}
