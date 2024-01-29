
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
		private int _count;

		partial void InitEvent(UIContext context)
		{
			var room = DataCenter.Instance.roomData.current;
			_datas = ConfigSystem.Instance.Finds<RoomTechRowData>((c) => c.Room == room.id && !room.techs.Contains(c.Id));
			_count = _datas.Count;
			_datas.Sort((a, b) => {

				var f = a.Cost(2).CompareTo(b.Cost(2));
				if (f == 0)
					f = a.Id.CompareTo(b.Id);
				return f;
			});
			m_view.m_BuffList.itemRenderer = OnSetItem;
			RefreshList();
		}

		partial void UnInitEvent(UIContext context)
		{

		}

		private void RefreshList()
		{
			var count = Mathf.Min(50, _datas.Count);
			m_view.m_completed.selectedIndex = _count > 0 ? 0 : 1;
			if (_count > 0)
			{
				m_view.m_BuffList.numItems = count;
				m_view.m_BuffList.scrollPane.touchEffect = _count > 7;
			}
		}

		private void OnSetItem(int index, GObject item)
		{
			var view = (UI_BuffItem)item;
			var data = _datas[index];
			view.m_title.text = data.Name;
			view.m_desc.text = data.Des;
			view.icon = data.Icon;
			view.m_type.selectedIndex = data.Mark - 1;

			var str = Utils.ConvertNumberStr(data.Cost(2));
			var flag = PropertyManager.Instance.CheckCountByArgs(data.GetCostArray());
			view.m_click.m_gray.selectedIndex = flag ? 0 : 1;
			UIListener.SetIconIndex(view.m_click, data.Cost(1) - 1);
			UIListener.SetText(view.m_click, str);
			view.m_click.onClick.Clear();
			view.m_click.onClick.Add(() => OnItemClick(index ,view, data));
		}

		private void OnItemClick(int index ,UI_BuffItem item, RoomTechRowData data)
		{
			if (PropertyManager.Instance.CheckCountByArgs(data.GetCostArray()))
			{
				DataCenter.RoomUtil.AddTech(data.Id);
				PropertyManager.Instance.UpdateByArgs(true, data.GetCostArray());
				item.m_state.selectedIndex = 1;
				_count--;
				//_datas.RemoveAt(index);
				RefreshList();
			}
			else
			{
				Error_Code.ITEM_NOT_ENOUGH.ToString().Tips();
			}
		}

	}
}
