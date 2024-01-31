namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Worktable;
	using Unity.Entities;
	using Unity.Mathematics;

	public struct WorktableInfo : IComponentData
	{
		public int id;
		public int mid;
		public float3 target;
		public int type;
	}

	public partial class UIWorktable
	{
		partial void InitLogic(UIContext context)
		{

		}
		
		partial void UnInitLogic(UIContext context){

		}
	}
}
