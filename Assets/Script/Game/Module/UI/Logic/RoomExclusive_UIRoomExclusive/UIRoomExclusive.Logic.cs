
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.RoomExclusive;
    using System.Collections.Generic;
    using GameConfigs;
    using System.Linq;

    public partial class UIRoomExclusive
	{
		List<int> m_RoomExclusiveRowDatas;
		partial void InitLogic(UIContext context){

			m_RoomExclusiveRowDatas = DataCenter.Instance.exclusiveData.randomBuffs?.Count <= 0 ?
					DataCenter.ExclusiveUtils.GetRandomRewardList():
					DataCenter.Instance.exclusiveData.randomBuffs;
			DataCenter.Instance.exclusiveData.randomBuffs = m_RoomExclusiveRowDatas;

			m_view.m_list.itemRenderer = OnItemRenderer;
			m_view.m_list.onClickItem.Add(OnClick);
			m_view.m_list.numItems = m_RoomExclusiveRowDatas.Count;

			RefreshText();
		}

		void OnItemRenderer(int index, GObject gObject) 
		{
			var exclusiveId = m_RoomExclusiveRowDatas[index];
			if (ConfigSystem.Instance.TryGet<GameConfigs.RoomExclusiveRowData>(exclusiveId, out var data))
			{
				var obj = gObject as UI_exclusiveItem;

				obj.data = exclusiveId;
				obj.m_name.text = UIListener.Local(data.BuffName);
				obj.m_info.text = string.Format(UIListener.Local(data.BuffDesc), 
					data.BuffValue == 0 ? data.BuffDuration : data.BuffValue,
					data.BuffDuration);
				obj.m_markState.selectedIndex = data.BuffMark;
				obj.SetIcon(data.BuffIcon);
			}
		
		}

		void OnClick(EventContext context) 
		{
			var obj = context.data as GObject;
			var exclusiveId = (int)obj.data;

			DataCenter.ExclusiveUtils.SelectRewardBuff(exclusiveId);
			SGame.UIUtils.CloseUIByID(__id);
		}

		void RefreshText() 
		{
			m_view.title = UIListener.Local("ui_opening_title_1");
			m_view.m_select.SetText(UIListener.Local("tips_opening_1"));
		}


		partial void UnInitLogic(UIContext context){

		}
	}
}
