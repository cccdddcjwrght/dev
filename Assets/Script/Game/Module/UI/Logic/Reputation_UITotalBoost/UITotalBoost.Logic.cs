
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
			m_view.m_list.itemRenderer = OnItemRenderer;
			m_view.m_body.SetText(UIListener.Local("ui_like_title_3"));
			RefreshTotalList();
		}

		void RefreshTotalList() 
		{
			List<Character> list = new List<Character>();
			CharacterModule.Instance.FindCharacters(list, (c) => c.roleType == (int)EnumRole.Cook
			|| c.roleType == (int)EnumRole.Waiter);
			m_view.m_worker.SetTextByKey("total_worker_name", list.Count);
			m_view.m_customer.SetTextByKey("total_customer_name", Utils.GetAllMaxCustomer());

			ReputationModule.Instance.RefreshVailedBuffList();
			m_TotalItems = ReputationModule.Instance.GetVailedBuffList();
			m_view.m_list.numItems = m_TotalItems.Count;
			m_view.m_totalNum.SetText(string.Format("X{0}", ReputationModule.Instance.GetTotalValue().ToString()));
		}

		void OnItemRenderer(int index, GObject gObject) 
		{
			var data = m_TotalItems[index];
			var item = gObject as UI_BoosItem;
			item.m_name.SetTextByKey(data.name);
			item.m_multiple.SetText(string.Format("X{0}",data.multiple));
			int startTime = GameServerTime.Instance.serverTime;
			if (data.time > 0)
			{
				Utils.Timer(data.time, () =>
				{
					int time = GameServerTime.Instance.serverTime - startTime;
					int countDownTime = data.time - time;
					item.m_duration.SetText(Utils.FormatTime(Mathf.Max(countDownTime, 0)));
				}, item, completed: TimeDoFinish);
			}
			else 
			{
				item.m_duration.SetText("∞");
			}
		
		}

		void TimeDoFinish() 
		{
			RefreshTotalList();
			//if (m_TotalItems.Count <= 0) SGame.UIUtils.CloseUIByID(__id);
		}

		partial void UnInitLogic(UIContext context){

		}
	}
}
