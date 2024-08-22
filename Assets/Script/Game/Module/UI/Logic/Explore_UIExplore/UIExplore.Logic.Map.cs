
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Explore;
	using SGame.UI.Common;
	using SGame.UI.Pet;

	public partial class UIExplore
	{
		private GTweener mapAnim;

		void InitMap()
		{
			var w = m_view.m_map.width;
			m_view.m_map.width *= 2;
			mapAnim = m_view.m_map.TweenMoveX(-w, 10).SetEase(EaseType.Linear).SetRepeat(-1);
			MapLoop(true);
			onOpen += OnOpen_Map;
		}

		void OnOpen_Map(UIContext context) {

			MapLoop(false);

		}

		void MapLoop(bool stop = false)
		{
			if (!stop)
			{
				mapAnim.SetPaused(false);
			}
			else
			{
				mapAnim.SetPaused(true);
			}
		}


	}
}
