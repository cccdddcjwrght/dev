using FairyGUI;
using SGame.UI;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace SGame
{
	public enum FlightType
	{
		GOLD = 1,
		DIAMOND = 2,
		SHOVEL = 3,
		BOX = 4,
		PET = 5,
	}

	public struct FlightItem
	{
		public int id;
		public Vector2 pos;
	}

	public class TransitionModule : Singleton<TransitionModule>
	{
		private EventHandleContainer m_EventHandle = new EventHandleContainer();
		private Dictionary<int, int> m_DependDict = new Dictionary<int, int>();

		public static float duration = 1.5f;
		public static bool isPlay = false;  //是否正在播放飞行特效表现

		public void Initalize()
		{
			m_EventHandle += EventManager.Instance.Reg<int>((int)GameEvent.ENTER_ROOM, CreateFlightUI);
		}

		public void CreateFlightUI(int scene)
		{
			CheckAndOpenUI();
		}


		List<int> m_TempIdList = new List<int>();
		List<Vector2> m_TempVecList = new List<Vector2>();
		List<FlightItem> m_FlightItemList = new List<FlightItem>();

		public void PlayFlight(GList list, List<int[]> reward, int type = 0)
		{
			m_TempIdList.Clear();
			m_TempVecList.Clear();
			m_FlightItemList.Clear();
			for (int i = 0; i < reward.Count; i++)
			{
				int[] r = reward[i];
				if (r.Length >= 2)
				{
					int t = r.Length == 2 ? 1 : r[0];
					int itemId = r.Length == 2 ? r[0] : r[1];
					if (t == 1 && CheckIsTranId(itemId))
					{
						Vector2 pos = ConvertGObjectGlobalPos(list.GetChildAt(i));
						m_FlightItemList.Add(new FlightItem()
						{
							id = itemId,
							pos = pos
						});
					}
				}
			}

			foreach (var i in m_FlightItemList)
				m_TempIdList.Add(i.id);

			m_TempIdList.Sort();
			foreach (var id in m_TempIdList)
			{
				var index = m_FlightItemList.FindIndex((f) => f.id == id);
				if (index >= 0)
				{
					m_TempVecList.Add(m_FlightItemList[index].pos);
					m_FlightItemList.RemoveAt(index);
				}
			}
			PlayFlight(m_TempIdList, m_TempVecList, type);
		}

		public void PlayFlight(List<int> ids, List<Vector2> startPos, int type = 0)
		{
			if (type > 0) EventManager.Instance.Trigger((int)GameEvent.FLIGHT_LIST_TYPE, ids, startPos, Vector2.zero, duration, type);
			else EventManager.Instance.Trigger((int)GameEvent.FLIGHT_LIST_CREATE, ids, startPos, Vector2.zero, duration);
		}

		public void PlayFlight(GObject gObject, int id, float offsetX = 0, float offsetY = 0, int type = 0)
		{
			if (gObject == null || gObject.isDisposed) return;
			var startPos = ConvertGObjectGlobalPos(gObject) + new Vector2(offsetX, offsetY);
			if (type > 0) EventManager.Instance.Trigger((int)GameEvent.FLIGHT_SINGLE_TYPE, id, startPos, Vector2.zero, duration, type);
			else EventManager.Instance.Trigger((int)GameEvent.FLIGHT_SINGLE_CREATE, id, startPos, Vector2.zero, duration);
		}

		void CheckAndOpenUI()
		{
			const string ui = "flight";
			if (!SGame.UIUtils.CheckUIIsOpen(ui))
				SGame.UIUtils.OpenUI(ui);
		}

		public void AddDepend(int id)
		{
			if (!m_DependDict.ContainsKey(id)) m_DependDict[id] = 0;
			m_DependDict[id]++;

			isPlay = true;
		}

		public void SubDepend(int id)
		{
			m_DependDict[id]--;

			foreach (var keyValue in m_DependDict)
				if (keyValue.Value > 0) return;

			isPlay = false;
		}

		public bool IsShow(int id)
		{
			if (m_DependDict.ContainsKey(id))
				return m_DependDict[id] > 0;
			return false;
		}

		//检测奖励id是否是需要播放飞行特效
		public bool CheckIsTranId(int id)
		{
			if (id == (int)FlightType.GOLD || id == (int)FlightType.DIAMOND || id == (int)FlightType.SHOVEL || CheckIsBox(id) || CheckIsPet(id))
				return true;
			return false;
		}

		//检测是否是宝箱 --对应物品表配置
		public bool CheckIsBox(int id)
		{
			if (ConfigSystem.Instance.TryGet<GameConfigs.ItemRowData>(id, out var data))
				if (data.Type == 3 && data.SubType == 1) return true;

			return false;
		}

		public bool CheckIsPet(int id)
		{
			if (ConfigSystem.Instance.TryGet<GameConfigs.ItemRowData>(id, out var data))
			{
				if (data.Type == 10) return true;
			}
			return false;
		}

		public Vector2 ConvertGObjectGlobalPos(GObject gObject)
		{
			Vector2 ret = gObject.LocalToGlobal(Vector2.zero);
			ret = GRoot.inst.GlobalToLocal(ret);

			if (gObject.pivot == Vector2.zero)
			{
				ret.x += gObject.width * 0.5f;
				ret.y += gObject.height * 0.5f;
			}
			return ret;
		}
	}

}

