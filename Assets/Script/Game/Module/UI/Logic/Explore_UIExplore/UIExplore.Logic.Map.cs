
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Explore;
	using SGame.UI.Common;
	using SGame.UI.Pet;
	using System;
	using GameConfigs;

	public partial class UIExplore
	{
		private float c_tscale;

		private GTweener mapAnim;
		private Action<GTweener> onMapMove;

		private float _mapSpeed;
		private float _timeScale;

		void InitMap()
		{
			var w = m_view.m_map.width;
			m_view.m_map.width *= 2;

			_mapSpeed = GlobalDesginConfig.GetFloat("explore_map_speed", 100);
			c_tscale = GlobalDesginConfig.GetFloat("explore_map_time_scale", 3f);

			mapAnim = m_view.m_map
				.TweenMoveX(-w, w / _mapSpeed)
				.SetEase(EaseType.Linear)
				.SetRepeat(-1)
				.OnUpdate(v => onMapMove?.Invoke(v));
			MapLoop(true);
			onOpen += OnOpen_Map;
		}

		void OnOpen_Map(UIContext context)
		{


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

		void Fast(bool slow = false)
		{
			_timeScale = slow ? 1f : c_tscale;
			mapAnim.SetTimeScale(_timeScale);
		}

		float GetSpeed()
		{
			return _mapSpeed * _timeScale;
		}

		void ListenMapMove(Action<GTweener> call, bool remove = false)
		{
			onMapMove -= call;
			if (!remove)
				onMapMove += call;
		}

	}
}
