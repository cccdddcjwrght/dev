
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Worktable;
	
	public partial class UIWorktable
	{
		partial void InitLogic(UIContext context)
		{
			context.window.SetPivot(0.5f, 1f, true);
		}
		
		partial void UnInitLogic(UIContext context){

		}
	}
}
