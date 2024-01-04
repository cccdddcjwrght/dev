
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Main;
	using Fibers;
	using System;
	using System.Collections;
	using Unity.Entities;
	using Unity.Mathematics;
	using Unity.Transforms;
	
	public partial class UIMain
	{
		private UserData         m_userData;
		private UserSetting      m_userSetting;
		private long             m_dicePower;
		private long             m_diceMaxPower;

		private UIContext        m_context;
		private ItemGroup        m_userProperty;

		private Fiber			 m_fiberShowTips;

		partial void InitLogic(UIContext context){
			m_context			= context;
		}
	}
}
