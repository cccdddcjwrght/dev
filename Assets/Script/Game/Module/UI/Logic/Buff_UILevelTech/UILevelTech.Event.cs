
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Buff;
	using GameConfigs;
	using System.Collections.Generic;

	public partial class UILevelTech
	{
		private List<RoomTechRowData> _datas;

		partial void InitEvent(UIContext context)
		{
			var room = DataCenter.Instance.roomData.current;
			_datas = ConfigSystem.Instance.Finds<RoomTechRowData>((c) => c.Room == room.id && !room.techs.Contains(room.id));

			m_view.m_BuffList.itemRenderer = OnSetItem;
			m_view.m_BuffList.SetVirtual();
			m_view.m_BuffList.numItems = 100; Mathf.Min(50, _datas.Count);
		}

		partial void UnInitEvent(UIContext context)
		{

		}

		private void OnSetItem(int index, GObject item)
		{
			var view = (UI_BuffItem)item;
			//var data = _datas[index];
		}

	}
}
