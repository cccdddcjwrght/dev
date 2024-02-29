﻿//枚举定义
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
		LevelGold=108,

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
		Player=5,
	}


	public enum EnumShopArea:int
	{
		None = 0,
		Ad = 1,
		Gift = 2,
		Item = 3,
		Eq = 4,
		Diamond = 5,
	}

}
