
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Offline;
	using System;

	public partial class UIOffline
	{

		const string c_ad_name = "offline_ad";

		private bool _isAd;
		private double _gold;

		partial void InitLogic(UIContext context)
		{
			var max = (int)AttributeSystem.Instance.GetValue(EnumTarget.Game, EnumAttribute.OfflineTime);
			var val = Mathf.Clamp(StaticDefine.G_Offline_Time, 0, max);
			m_view.m_progress.max = max;
			m_view.m_progress.value = val;
			m_view.m_progress.SetText(Utils.FormatTime((int)val, needsec: false) + "/" + Utils.FormatTime((int)max, needsec: false));

			var ws = DataCenter.MachineUtil.GetWorktables((w) => !w.isTable && w.level > 0);

			var gold = 0d;
			ws.ForEach(w => gold += w.GetPrice() / w.GetWorkTime());
			gold = gold.ToInt() * val;

			m_view.m_count.SetText(Utils.ConvertNumberStr(gold), false);

			_isAd = DataCenter.AdUtil.IsAdCanPlay(c_ad_name);
			m_view.m_state.selectedIndex = _isAd ? 1 : 0;
			_gold = gold;
		}

		partial void UnInitLogic(UIContext context)
		{

		}

		partial void OnClickClick(EventContext data)
		{
			if (_isAd)
			{
				Utils.PlayAd(c_ad_name, (s,t) => {
					if (s) {
						_isAd = false;
						OnClickClick(data);
					}
					else t.Tips();
				});
			}
			else
			{
				DoCloseUIClick(null);
			}
		}

		partial void OnUICloseClick(ref bool state)
		{
			RequestExcuteSystem.GetOfflineReward(_gold);
		}
	}
}
