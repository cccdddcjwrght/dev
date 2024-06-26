
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Hud;

	public partial class UIGetWorkerTips
	{
		partial void InitLogic(UIContext context)
		{

			context.onUpdate += OnUpdate;

		}

		partial void UnInitLogic(UIContext context)
		{
			context.onUpdate -= OnUpdate;
			StaticDefine.G_IN_VIEW_GET_WORKER = false;
		}

		void OnUpdate(UIContext context)
		{
		  	var pos = m_view.TransformPoint(Vector2.zero,GRoot.inst);
			var state = false;
			if (pos.x > 0 && pos.x < GRoot.inst.width && pos.y > 150 && pos.y < GRoot.inst.height)
				state = true;
			StaticDefine.G_IN_VIEW_GET_WORKER = state;
		}

	}
}
