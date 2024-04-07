using SGame;
using System.Collections.Generic;
using System;
using GameConfigs;
using log4net;

// 道具系统
public class ItemGroup
{
	// 道具数据
	private ItemData m_itemData = new ItemData();
	private Dictionary<int, int> m_values = new Dictionary<int, int>();
	static ILog log = LogManager.GetLogger("Game.ItemSystem");

	public event Action<int, double, double> onValueUpdate;

	public void Initalize(ItemData data)
	{
		m_itemData = data;//DataCenter.Instance.itemData;
		m_values.Clear();
		for (int i = 0; i < m_itemData.Values.Count; i++)
		{
			if (!m_values.ContainsKey(m_itemData.Values[i].id))
			{
				m_values.Add(m_itemData.Values[i].id, i);
			}
			else
			{
				log.Error("ITEM ID REPEATE=" + m_itemData.Values[i].id.ToString());
			}
		}
	}

	// 清空所有数据
	public void Clear()
	{
		m_itemData.Values.Clear();
		m_values.Clear();
	}

	/// <summary>
	/// 获得道具数量
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	public double GetNum(int id)
	{
		if (m_values.TryGetValue(id, out int value))
		{
			return m_itemData.Values[value].num;
		}

		return 0;
	}


	/// <summary>
	/// 设置数量
	/// </summary>
	/// <param name="id"></param>
	/// <param name="value"></param>
	public bool SetNum(int id, double value)
	{
		if (value < 0)
			return false;

		var oldValue = 0d;

		if (m_values.TryGetValue(id, out int cur_index))
		{
			var item = m_itemData.Values[cur_index];
			if (item.num == value)
				return false;
			oldValue = item.num;
			item.num = value;
			m_itemData.Values[cur_index] = item;
			EventManager.Instance.Trigger(((int)GameEvent.PROPERTY_GOLD));
		}
		else
		{
			// 添加新数据
			m_itemData.Values.Add(new ItemData.Value() { id = id, num = value });
			m_values.Add(id, m_itemData.Values.Count - 1);
		}

		onValueUpdate?.Invoke(id, value, oldValue);

		return true;
	}

	public bool CanAddNum(int id, double add_value)
	{
		if (add_value == 0)
			return false;

		if (GetNum(id) + add_value < 0)
		{
			return false;
		}

		return true;
	}


	/// <summary>
	/// 添加道具数量
	/// </summary>
	/// <param name="item_value"></param>
	/// <param name="value"></param>
	/// <returns></returns>
	public bool AddNum(int id, double add_value)
	{
		if (!CanAddNum(id, add_value))
			return false;

		return SetNum(id, GetNum(id) + add_value);
	}

	public List<ItemData.Value> GetDatas(bool clear = false)
	{
		if (m_itemData.Values.Count > 0)
		{
			var ret = new List<ItemData.Value>(m_itemData.Values);
			if (clear)
				m_itemData.Values.Clear();
			return ret;
		}
		return default;
	}
}