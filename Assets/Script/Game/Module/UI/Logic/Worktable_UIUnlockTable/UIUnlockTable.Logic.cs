
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Worktable;

	public partial class UIUnlockTable
	{
		private SGame.Worktable data;
		private int machineID;
		private float[] price;

		partial void InitLogic(UIContext context)
		{
			var args = context.GetParam()?.Value.To<object[]>();

			data = args.Val<SGame.Worktable>(0);
			machineID = args.Val<int>(1);

			EventManager.Instance.Reg<int, int, int, int>(((int)GameEvent.ITEM_CHANGE_BURYINGPOINT), OnChange);

			if (data == null) DoCloseUIClick(null);
			else SetInfo();
		}

		partial void UnInitLogic(UIContext context)
		{
			data = null;
			EventManager.Instance.UnReg<int, int, int, int>(((int)GameEvent.ITEM_CHANGE_BURYINGPOINT), OnChange);
		}

		partial void OnClickClick(EventContext data)
		{
			if (0 == DataCenter.MachineUtil.CheckCanActiveMachine(machineID))
			{
				DataCenter.MachineUtil.AddMachine(machineID);
				PropertyManager.Instance.UpdateByArgs(true, this.data.GetUnlockPrice());
				SGame.UIUtils.CloseUIByID(__id);
			}
		}

		void OnChange(int a, int b, int c, int d)
		{
			m_view.m_type.selectedIndex = Utils.CheckItemCount((int)price[1], price[2], false) ? 0 : 1;
		}

		void SetInfo()
		{
			var add = data.objNextLvCfg.CustomerNum - (data.objLvCfg.IsValid() ? data.objLvCfg.CustomerNum : 0);
			price = data.GetUnlockPrice();
			m_view.SetIcon(data.objCfg.Icon);
			m_view.m_tips.SetTextByKey(data.objCfg.Des);
			m_view.m_count.SetText("+" + add);
			m_view.m_cost.SetText(Utils.ConvertNumberStr(price[2]), false);
			OnChange(0, 0, 0, 0);
		}


	}
}
