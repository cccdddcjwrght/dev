
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Worktable;

	public partial class UIWorktable
	{
		partial void InitEvent(UIContext context)
		{
			m_view.m_close.onClick.Add(() => SGame.UIUtils.CloseUIByID(__id));
		}

		partial void UnInitEvent(UIContext context)
		{

		}

	}
}
