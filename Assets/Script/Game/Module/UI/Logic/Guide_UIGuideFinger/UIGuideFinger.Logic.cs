
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Guide;
	
	public partial class UIGuideFinger
	{
		Vector2 pos;
		bool isCell = false;
		partial void InitLogic(UIContext context){
			context.onUpdate += onUpdate;

			pos = context.gameWorld.GetEntityManager().GetComponentObject<UIPos>(context.entity).pos;
			m_view.m_Finger.xy = pos;

			isCell = context.gameWorld.GetEntityManager().GetComponentObject<UICell>(context.entity).isCell;
		}

		private void onUpdate(UIContext context) 
		{
			if (isCell) 
			{
				Vector3 cellPos = GameTools.MapAgent.CellToVector((int)pos.x, (int)pos.y);
				var uiPos = SGame.UIUtils.WorldPosToUI(cellPos);
				m_view.m_Finger.xy = uiPos;
			}
		}
		partial void UnInitLogic(UIContext context){
			context.onUpdate -= onUpdate;
		}
	}
}
