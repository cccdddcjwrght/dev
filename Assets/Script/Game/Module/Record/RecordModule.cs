using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SGame
{
	public enum RecordDataEnum
	{
		CHAPTER = 1,    //����½ڴ���
		LEVEL = 2,    //��ɹؿ�����
		BOX = 3,    //�򿪳�����������
		WORKER = 4,    //��Ӷ��������
		SELL = 5,    //������Ʒ����
		SERVE = 6,    //����ͻ�����
		TIP = 7,    //�ռ�����С�Ѵ���
		EQUIP_BOX = 8,    //��װ����������
		AD = 9,    //�ۿ�������
		EQUIP_LEVEL = 10,   //����װ������
		EQUIP_STAGE = 11,   //����װ������
		PET = 12,   //�����������
		FIRST_LOGIN = 13,   //�״ε�¼
		PET_BORN = 14,   //�����������
		TABEL_LEVEL = 15,   //�ӹ�̨����
		TECH_LEVEL = 16,   //�Ƽ���������
		PERFECT = 17,   //������������
		IMMEDIATE = 18,   //������ɴ���

		//��������
		MACHINE = 19,   //����̨id����X��
		TABLE = 20,   //����id����X��
		DECORATION = 21,   //�ڼ�id����X��
		AREA = 22,   //��������id
		COOK = 23,   //��Ʒid����X��
	}

	public enum RecordFunctionId
	{
		NONE = 0,    //Ĭ��
		RANK = 26,   //����
		EXCHANGE = 28,   //�һ��
		CLUB = 31,   //���ֲ�
		TASK = 32,   //��������
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

	//��¼����
	public class RecordModule : Singleton<RecordModule>
	{
		public EventHandleContainer m_EventHandle = new EventHandleContainer();

		public RecordTotalData m_RecordTotalData { get { return DataCenter.Instance.recordTotalData; } }
		public void Initalize()
		{
			m_EventHandle += EventManager.Instance.Reg<int, int>((int)GameEvent.RECORD_PROGRESS, AddValue);
			//������һ��֮ǰ ���㵱ǰС�ѵ��ۼƴ���
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

		//�����¼����
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

