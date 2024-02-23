
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Worktable;
	using GameConfigs;

	public partial class UIWorktablePanel
	{
		partial void InitEvent(UIContext context)
		{
			m_view.SetPivot(0.5f, 1f, true);
			if (info.id > 0 && info.type == 2)
			{
				new LongPressGesture(m_view.m_click)
				{
					once = false,
					interval = 0.1f,
					trigger = 1f
				}.onAction.Add(OnClickClick);
			}

			GRoot.inst.onClick.Add(OnOtherUIClick);

			EventManager.Instance.Reg<double, double>(((int)GameEvent.PROPERTY_GOLD_CHANGE), OnGoldChange);

		}

		partial void UnInitEvent(UIContext context)
		{
			GRoot.inst.onClick.Remove(OnOtherUIClick);

			EventManager.Instance.UnReg<double, double>(((int)GameEvent.PROPERTY_GOLD_CHANGE), OnGoldChange);
		}

		void OnGoldChange(double val, double change)
		{
			RefreshClick();
		}

		void OnOtherUIClick(EventContext data)
		{
			if (GRoot.inst.focus != null && !SGame.UIUtils.IsChild(m_view, GRoot.inst.focus))
			{

				SGame.UIUtils.CloseUIByID(__id);
			}
		}
	}
}
