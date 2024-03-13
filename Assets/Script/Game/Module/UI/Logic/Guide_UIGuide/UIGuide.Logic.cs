﻿
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Guide;
	
	public partial class UIGuide
	{
		partial void InitLogic(UIContext context){
			var pos = context.gameWorld.GetEntityManager().GetComponentObject<UIPos>(context.entity).pos;
			var size = context.gameWorld.GetEntityManager().GetComponentObject<UISize>(context.entity).size;
			var alpha = context.gameWorld.GetEntityManager().GetComponentObject<UIAlpha>(context.entity).alpha;
			
			m_view.m_blank.xy = pos;
			m_view.m_blank.size = size;
			m_view.m_mask.alpha = alpha;
		}
		partial void UnInitLogic(UIContext context){

		}
	}
}
