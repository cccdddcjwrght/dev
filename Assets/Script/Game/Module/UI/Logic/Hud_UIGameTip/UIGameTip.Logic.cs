
using Unity.VisualScripting;
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Hud;
	using Unity.Mathematics;
	using Unity.Transforms;
	
	public partial class UIGameTip
	{
		private Vector3 m_startPos;
		partial void InitLogic(UIContext context)
		{
			var param = context.gameWorld.GetEntityManager().GetComponentObject<GameTipParam>(context.entity);
			m_view.pivot = new Vector2(0.5f, 0f);
			m_view.m_title.text = param.text;
			m_view.m_TipType.selectedIndex = (int)param.type;
			m_view.m_float.Play(() =>
			{
				context.window.Close();
			});
		}
		
		partial void UnInitLogic(UIContext context){

		}
	}
}
