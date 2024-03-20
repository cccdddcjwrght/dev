
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Player;

	public partial class UIEquipTips
	{
		partial void InitEvent(UIContext context)
		{

			m_view.m_maskbg.onClick.Add(DoCloseUIClick);

		}
		partial void UnInitEvent(UIContext context)
		{
			m_view.m_maskbg.onClick.Clear();
		}
	}
}
