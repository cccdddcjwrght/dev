
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Worktable;
	using Unity.Entities;
	using Unity.Mathematics;

	public class WorktableInfo : IComponentData
	{
		public int id;
		public int mid;
		public float3 target;
	}

	public partial class UIWorktable
	{
		partial void InitLogic(UIContext context)
		{
			var info = context.uiModule.GetEntityManager().GetComponentObject<WorktableInfo>(context.entity);
			m_view.m_panel.xy = SGame.UIUtils.GetUIPosition(m_view, info.target, PositionType.POS3D);
		}
		
		partial void UnInitLogic(UIContext context){

		}
	}
}
