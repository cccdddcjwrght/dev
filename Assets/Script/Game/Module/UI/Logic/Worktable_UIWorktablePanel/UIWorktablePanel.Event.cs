
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
		}

		partial void UnInitEvent(UIContext context)
		{

		}



	}
}
