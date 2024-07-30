
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Worktable;
	using System;

	public partial class UIRecruitment
	{
		private SGame.Worktable data;
		private float[] price;

		partial void InitLogic(UIContext context)
		{
			var args = context.GetParam()?.Value.To<object[]>();
			data = args.Val<SGame.Worktable>(0);

			EventManager.Instance.Reg<double, double>(((int)GameEvent.PROPERTY_GOLD_CHANGE), OnChange);

			if (data == null) DoCloseUIClick(null);
			else SetInfo();
			context.window.AddEventListener("OnMaskClick", () => DoCloseUIClick(null));
			//m_view.m_roletype.selectedIndex = StaticDefine.G_GET_WORKER_TYPE;
		}

		partial void UnInitLogic(UIContext context)
		{
			EventManager.Instance.UnReg<double, double>(((int)GameEvent.PROPERTY_GOLD_CHANGE), OnChange);
			data = null;
		}

		void OnChange(double a, double b)
		{
			if (m_view == null || data == null)
			{
				DoCloseUIClick(null);
				return;
			}
			var gray = false;
			var type = 3;
			if (!data.IsMaxLv())
			{
				if (!DataCenter.MachineUtil.IsAreaEnable(data.objLvCfg.Condition))
				{
					type = 2;
					gray = Utils.CheckItemCount((int)price[1], price[2], false);
				}
				else
					type = Utils.CheckItemCount((int)price[1], price[2], false) ? 0 : 1;
			}
			try
			{
				m_view.m_type.selectedIndex = type;
				if (gray) UIListener.SetControllerSelect(m_view.m_click, "gray", 2);
			}
			catch (System.Exception e)
			{
				DoCloseUIClick(null);
			}
		}

		void SetInfo()
		{
			price = data.GetUnlockPrice();
			m_view.m_currency.selectedIndex = (int)price[0];
			m_view.m_click.SetText(Utils.ConvertNumberStr(price[2]), false);
			m_view.m_selectctr.selectedIndex = 0;
			m_view.m_roletype.selectedIndex = data.objLvCfg.ShowType == 0 ? 2 : data.objLvCfg.ShowType - 1;
			m_view.m_select1.m_count.SetTextByKey("ui_recruitment_count", DataCenter.Instance.roomData.current.waiter);
			m_view.m_select2.m_count.SetTextByKey("ui_recruitment_count", DataCenter.Instance.roomData.current.cooker);
			SetRecommand();
			OnChange(0, 0);
		}

		void SetRecommand()
		{

			var select = 0;
			if (data.objLvCfg.ShowType == 0)
			{
				var room = DataCenter.Instance.roomData.current;
				if (room != null)
				{
					var cooker = Math.Max(1f, DataCenter.Instance.roomData.current.cooker);
					var waiter = Math.Max(1f, DataCenter.Instance.roomData.current.waiter);
					var v = waiter / cooker;
					select = v > room.roomCfg.RecommendValue(0) ? 1 : v < room.roomCfg.RecommendValue(1) ? 2 : 0;
				}
			}
			m_view.m_recommand.selectedIndex = select;
		}

		public bool UpLevel()
		{
			if (data.objLvCfg.ShowType != 0 || m_view.m_selectctr.selectedIndex > 0)
			{
				switch (DataCenter.MachineUtil.CheckCanUpLevel(data))
				{
					case Error_Code.LV_MAX:
						Debug.Log($"{data.id} Lv Max!!!");
						break;
					case Error_Code.ITEM_NOT_ENOUGH:
						Debug.Log($"{data.id} 升级道具不足!!!");
						break;
					case 0:
						20.ToAudioID().PlayAudio();
						DataCenter.MachineUtil.UpdateLevel(data.id, 0, select: m_view.m_selectctr.selectedIndex);
						//EffectSystem.Instance.AddEffect(2, m_view.m_click);
						DoCloseUIClick(null);
						return true;
				}
			}
			else
			{
				"@ui_recruitment_tips2".Tips();
			}
			return false;
		}

		partial void OnClickClick(EventContext data)
		{
			if (m_view.m_type.selectedIndex == 0)
			{
				UpLevel();
			}
		}
	}
}
