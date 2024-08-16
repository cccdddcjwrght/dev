using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SGame
{
	public enum RecordDataEnum
	{
		CHAPTER = 1,    //完成章节次数
		LEVEL = 2,    //完成关卡次数
		BOX = 3,    //打开场景箱子数量
		WORKER = 4,    //雇佣工人数量
		SELL = 5,    //出售商品数量
		SERVE = 6,    //服务客户人数
		TIP = 7,    //收集客人小费次数
		EQUIP_BOX = 8,    //打开装备宝箱数量
		AD = 9,    //观看广告次数
		EQUIP_LEVEL = 10,   //升级装备次数
		EQUIP_STAGE = 11,   //进阶装备次数
		PET = 12,   //宠物进化次数
		FIRST_LOGIN = 13,   //首次登录
		PET_BORN = 14,   //宠物孵化次数
		TABEL_LEVEL = 15,   //加工台升级
		TECH_LEVEL = 16,   //科技升级次数
		PERFECT = 17,   //完美制作次数
		IMMEDIATE = 18,   //立即完成次数

		//任务类型
		MACHINE = 19,   //操作台id升到X级
		TABLE = 20,   //桌子id升到X级
		DECORATION = 21,   //摆件id升到X级
		AREA = 22,   //解锁区域id
		COOK = 23,   //菜品id升到X级
	}

	public enum RecordFunctionId
	{
		NONE = 0,    //默认
		RANK = 26,   //排行
		EXCHANGE = 28,   //兑换活动
		CLUB = 31,   //俱乐部
		TASK = 32,   //主线任务
	}

	[Serializable]
	public class RecordTotalData
	{
		public List<RecordFuncitionIdData> data = new List<RecordFuncitionIdData>();
	}

	[Serializable]
	public class RecordFuncitionIdData
	{
		public int funcId;
		public List<RecordData> recordDatas = new List<RecordData>();
	}

	[Serializable]
	public class RecordData
	{
		public int type;
		public int value;
	}

	public partial class DataCenter
	{
		public RecordTotalData recordTotalData = new RecordTotalData();
	}

	//记录数据
	public class RecordModule : Singleton<RecordModule>
	{
		public EventHandleContainer m_EventHandle = new EventHandleContainer();

		public RecordTotalData m_RecordTotalData { get { return DataCenter.Instance.recordTotalData; } }
		public void Initalize()
		{
			m_EventHandle += EventManager.Instance.Reg<int, int>((int)GameEvent.RECORD_PROGRESS, AddValue);
			//进入下一关之前 计算当前小费的累计次数
			m_EventHandle += EventManager.Instance.Reg<int>((int)GameEvent.BEFORE_ENTER_ROOM, (s) =>
			{
				EventManager.Instance.Trigger((int)GameEvent.RECORD_PROGRESS, (int)RecordDataEnum.TIP, DataCenter.Instance.m_foodTipsCount);
				DataCenter.Instance.m_foodTipsCount = 0;
			});
			m_EventHandle += EventManager.Instance.Reg<int>((int)GameEvent.ENTER_ROOM, (s) => DataCenter.TaskMainUtil.UpdateRoomTaskReward());
			InitData();
		}

		public void InitData()
		{
			foreach (int funcId in Enum.GetValues(typeof(RecordFunctionId)))
			{
				var index = m_RecordTotalData.data.FindIndex((d) => d.funcId == funcId);
				if (index == -1)
				{
					RecordFuncitionIdData data = new RecordFuncitionIdData() { funcId = funcId };
					m_RecordTotalData.data.Add(data);
				}
			}
		}

		public void AddValue(int type, int value)
		{
			m_RecordTotalData.data.ForEach((d) =>
			{
				if (d.funcId.IsOpend() || d.funcId == (int)RecordFunctionId.NONE)
				{
					var index = d.recordDatas.FindIndex((r) => r.type == type);
					if (index == -1)
					{
						RecordData data = new RecordData() { type = type, value = value };
						d.recordDatas.Add(data);
					}
					else
					{
						d.recordDatas[index].value += value;
					}
				}
			});
		}

		public int GetValue(int type, int funcId = 0)
		{
			RecordFuncitionIdData data = default;
			for (int i = 0; i < m_RecordTotalData.data.Count; i++)
			{
				var d = m_RecordTotalData.data[i];
				if (d != null && d.funcId == funcId)
				{
					data = d;
					break;
				}
			}

			if (data != null)
			{
				for (int i = 0; i < data.recordDatas.Count; i++)
				{
					var recordData = data.recordDatas[i];
					if (recordData != null && recordData.type == type) return recordData.value;
				}
			}

			return 0;
		}

		//清除记录数据
		public void ClearValue(int type, int funcId = 0)
		{
			var index = m_RecordTotalData.data.FindIndex((d) => d.funcId == funcId);
			if (index >= 0)
			{
				var data = m_RecordTotalData.data[index];
				var recordData = data.recordDatas.Find((r) => r.type == type);
				if (recordData != null) recordData.value = 0;
			}
		}
	}

}

