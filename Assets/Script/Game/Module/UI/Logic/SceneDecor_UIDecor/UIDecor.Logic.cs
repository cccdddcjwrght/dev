
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.SceneDecor;

	public partial class UIDecor
	{
		partial void InitLogic(UIContext context)
		{
			SetY(null);
			context.onUpdate += SetY;
		}

		partial void UnInitLogic(UIContext context)
		{
			context.onUpdate -= SetY;
		}

		void SetY(UIContext context)
		{
			var v = SceneCameraSystem.Instance.zMove.Rate();
			if (v > 0)
				m_view.m_wall.y = Screen.height * v;
		}
	}
}
