
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Pet;

	public partial class UIPetBorn
	{
		partial void InitEvent(UIContext context)
		{

			context.window.AddEventListener("OnMaskClick", () =>
			{
				if (_isCompleted)
					SGame.UIUtils.CloseUIByID(__id);

			});

		}
		partial void UnInitEvent(UIContext context)
		{

		}
	}
}
