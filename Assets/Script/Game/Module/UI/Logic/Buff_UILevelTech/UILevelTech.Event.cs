
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
			_datas.Sort((a, b) =>
			{

				var f = a.Cost(2).CompareTo(b.Cost(2));
				if (f == 0)
					f = a.Id.CompareTo(b.Id);
				return f;
			});
			m_view.m_BuffList.itemRenderer = OnSetItem;
			RefreshList();
			EventManager.Instance.Reg<double, double>(((int)GameEvent.PROPERTY_GOLD_CHANGE), OnGoldChange);
		}

		partial void UnInitEvent(UIContext context)
		{
			EventManager.Instance.UnReg<double, double>(((int)GameEvent.PROPERTY_GOLD_CHANGE), OnGoldChange);

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
			view.name = index.ToString();
			view.SetIcon(data.Icon);
			view.m_title.SetTextByKey(data.Name);
			view.m_desc.SetTextByKey(data.Des, Mathf.Max(1, data.Value));
			view.m_type.selectedIndex = data.Mark;

			var str = Utils.ConvertNumberStr(data.Cost(2));
			UIListener.SetIconIndex(view.m_click, data.Cost(1) - 1);
			UIListener.SetText(view.m_click, str);
			view.m_click.onClick.Clear();
			view.m_click.onClick.Add(() => OnItemClick(index, view, data));
			RefreshBtn(item);
		}

		private void RefreshBtn(GObject item)
		{
			var view = (UI_BuffItem)item;
			var data = _datas[int.Parse(item.name)];

			var flag = PropertyManager.Instance.CheckCountByArgs(data.GetCostArray());
			view.m_click.m_gray.selectedIndex = flag ? 0 : 1;
		}

		private void OnItemClick(int index, UI_BuffItem item, RoomTechRowData data)
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

		private void OnGoldChange(double val, double change)
		{
			m_view.m_BuffList.GetChildren().Foreach(c => RefreshBtn(c));

		}
	}
}
