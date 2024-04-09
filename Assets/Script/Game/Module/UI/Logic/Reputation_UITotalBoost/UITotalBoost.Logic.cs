
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Reputation;
    using System.Collections.Generic;

    public partial class UITotalBoost
	{
		private List<TotalItem> m_TotalItems;
		partial void InitLogic(UIContext context){
			m_TotalItems = ReputationModule.Instance.GetVailedBuffList();

			m_view.m_list.itemRenderer = OnItemRenderer;
			m_view.m_list.numItems = m_TotalItems.Count;

			m_view.m_totalNum.SetText(string.Format("X{0}", ReputationModule.Instance.GetTotalValue().ToString()));

		}

		void OnItemRenderer(int index, GObject gObject) 
		{
			var data = m_TotalItems[index];
			var item = gObject as UI_BoosItem;
			item.m_name.SetText(UIListener.Local(data.name));
			item.m_multiple.SetText(string.Format("X{0}",data.multiple));
			int startTime = GameServerTime.Instance.serverTime;
			Utils.Timer(data.time, () =>
			{
				int time = GameServerTime.Instance.serverTime - startTime;
				item.m_duration.SetText(Utils.FormatTime(data.time - time));
			}, item);
		}

		partial void UnInitLogic(UIContext context){

		}
	}
}
