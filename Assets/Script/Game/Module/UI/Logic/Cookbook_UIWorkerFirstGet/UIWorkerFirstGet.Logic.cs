
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Cookbook;
	using System.Collections;

	public partial class UIWorkerFirstGet
	{
		private WorkerDataItem _data;
		private double time = 0;
		private int _flag = 0;
		private bool _isUnlock = false;

		partial void InitLogic(UIContext context)
		{
			_data = context.GetParam()?.Value.To<object[]>().Val<WorkerDataItem>(0);
			if (_data == null) { DoCloseUIClick(null); return; }
			_isUnlock = _data.lastLv == 0;
			m_view.m_type.selectedIndex = _isUnlock ? 0 : 1;
			m_view.SetWorkerInfo(_data, _isUnlock ? 1 : _data.lastLv , _isUnlock);
			time = Time.realtimeSinceStartup + m_view.m_t0.totalDuration;
		}

		partial void DoShow(UIContext context)
		{
			Logic().Start();
		}

		void ResetToLevelShow()
		{
			_isUnlock = false;
			_flag = 0;
			m_view.m_property.alpha = 0;
			m_view.m_type.selectedIndex = 1;
			Logic(true).Start();
		}

		IEnumerator Logic(bool type = false)
		{
			if (!type)
			{
				yield return new WaitForSeconds(0.1f);
				yield return new WaitUntil(() => !m_view.m_t0.playing);
			}
			if (m_view == null) yield break;

			if (!_isUnlock)
			{
				for (int i = _data.lastLv+1; i <= _data.level; i++)
				{
					if (m_view == null) yield break;
					var ss = DataCenter.MachineUtil.CalcuStarList(((int)EnumQualityType.Max) * 5, i);
					var g = default(UI_WorkStar);
					for (int j = 0; j < ss.Length; j++)
					{
						var c = (m_view.m_stars.GetChildAt(j) as UI_WorkStar);
						c.m_type.selectedIndex = ss[j];
						if (ss[j] > 0) g = c;
						else c.m_t0.Stop();
					}

					if (g != null)
						g.m_t0.Play();

					yield return new WaitForSeconds(0.15f);
				}
			}
			m_view.m_property.TweenFade(1, 0.5f).OnComplete(() =>
			{
				if (!_isUnlock)
					m_view.m_property.m_t0.Play(() => _flag = 1);
				else
					_flag = 1;
			});
		}

		partial void OnUICloseClick(ref bool state)
		{
			state = _flag > 0 && Time.realtimeSinceStartup > time;
			if (state)
			{
				if (_isUnlock && _data.level > 1)
				{
					state = false;
					ResetToLevelShow();
				}
			}
		}

		partial void UnInitLogic(UIContext context)
		{
		}
	}
}
