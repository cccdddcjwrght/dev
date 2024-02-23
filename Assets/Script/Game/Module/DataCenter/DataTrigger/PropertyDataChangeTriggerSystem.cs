using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using static MessageBox;

namespace SGame
{
	[UpdateInGroup(typeof(GameLogicGroup))]
	public partial class PropertyDataChangeTriggerSystem : SystemBase
	{
		/// <summary>
		/// 属性事件触发器
		/// </summary>
		class PropertyChangeTrigger
		{
			private ItemGroup m_itemGroup;
			private int m_itemID;
			private double m_preValue = 0;
			private int m_eventID;

			public PropertyChangeTrigger(GameEvent eventID, ItemID itemID)
			{
				m_itemGroup = PropertyManager.Instance.GetGroup(PropertyGroup.ITEM); ;
				m_itemID = (int)itemID;
				m_preValue = 0;
				m_eventID = (int)eventID;
			}

			public void Update()
			{
				double value = m_itemGroup.GetNum((m_itemID));
				if (m_preValue != value)
				{
					// 更新事件
					var newValue = value;
					EventManager.Instance.Trigger(m_eventID, newValue, newValue - m_preValue);
					m_preValue = newValue;
				}
			}
		}
		private List<PropertyChangeTrigger> m_triggers;

		protected override void OnCreate()
		{

		}

		protected override void OnUpdate()
		{
			if (PropertyManager.Instance.IsInitalize == false)
				return;

			if (m_triggers == null)
				Init();

			foreach (var trigger in m_triggers)
			{
				trigger.Update();
			}
		}
	}
}