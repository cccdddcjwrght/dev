
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Worktable;

	public partial class UIRecruitment
	{
		private SGame.Worktable data;
		private float[] price;

		partial void InitLogic(UIContext context)
		{
			var args = context.GetParam()?.Value.To<object[]>();
			data = args.Val<SGame.Worktable>(0);

			EventManager.Instance.Reg<int, int, int, int>(((int)GameEvent.ITEM_CHANGE_BURYINGPOINT), OnChange);

			if (data == null) DoCloseUIClick(null);
			else SetInfo();
			context.window.AddEventListener("OnMaskClick", () => DoCloseUIClick(null));
		}

		partial void UnInitLogic(UIContext context)
		{
			data = null;
			EventManager.Instance.UnReg<int, int, int, int>(((int)GameEvent.ITEM_CHANGE_BURYINGPOINT), OnChange);
		}

		void OnChange(int a, int b, int c, int d)
		{
			var type = 3;
			if (!data.IsMaxLv())
			{
				if (!DataCenter.MachineUtil.IsAreaEnable(data.objLvCfg.Condition))
					type = 2;
				else
					type = Utils.CheckItemCount((int)price[1], price[2], false) ? 0 : 1;
			}
			m_view.m_type.selectedIndex = type;
		}

		void SetInfo()
		{
			price = data.GetUnlockPrice();
			m_view.m_currency.selectedIndex = (int)price[0];
			m_view.m_cost.SetText(Utils.ConvertNumberStr(price[2]), false);
			OnChange(0, 0, 0, 0);
		}

		public bool UpLevel()
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
					DataCenter.MachineUtil.UpdateLevel(data.id, 0);
					//EffectSystem.Instance.AddEffect(2, m_view.m_click);
					DoCloseUIClick(null);
					return true;
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
