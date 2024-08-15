
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Loading;

	public partial class UILoading
	{
		partial void InitEvent(UIContext context)
		{
			context.onHide += OnHide;
		}
		partial void UnInitEvent(UIContext context)
		{
			context.onHide -= OnHide;
		}

		void OnHide(UIContext context)
		{
			StaticDefine.G_IS_LOADING = false;
		}
	}
}
