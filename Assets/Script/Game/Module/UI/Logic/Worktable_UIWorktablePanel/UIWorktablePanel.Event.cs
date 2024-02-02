
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Worktable;

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
		}

		partial void UnInitEvent(UIContext context)
		{

		}



	}
}
