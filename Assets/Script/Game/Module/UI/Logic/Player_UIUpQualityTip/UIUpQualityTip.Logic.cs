
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Player;
	using GameConfigs;
	using System.Collections;
	using Unity.Entities;

	public partial class UIUpQualityTip
	{
		private bool flag = false;
		private Entity effect;

		partial void InitLogic(UIContext context)
		{

			var equip = (context.GetParam()?.Value as object[])?.Val<BaseEquip>(0);
			var recycle = (context.GetParam()?.Value as object[])?.Val<double>(1);

			if (equip == null)
			{
				DoCloseUIClick(null);
				return;
			}
			flag = false;

			m_view.m_body.SetEquipInfo(equip, true);
			m_view.m_body.m_equip.SetEquipInfo(equip, true);
			m_view.m_body.m_state.selectedIndex = recycle > 0 ? 1 : 0;
			m_view.m_body.m_recycle.SetText("ui_equip_upquality_recycle".Local(null, recycle), false);
			UIListener.Listener(m_view, new EventCallback1(OnClick));
			Effect().Start();
		}

		IEnumerator Effect()
		{

			var anim = m_view.m_body.m_upqualitytipui;
			effect = EffectSystem.Instance.AddEffect(28, m_view.m_body);
			yield return EffectSystem.Instance.WaitEffectLoaded(effect);
			22.ToAudioID().PlayAudio();
			anim.Play();
			yield return null;
			flag = true;
		}

		partial void DoShow(UIContext context)
		{
		}

		partial void UnInitLogic(UIContext context)
		{
			UIListener.Listener(m_view, new EventCallback1(OnClick), remove: true);
		}

		void OnClick(EventContext context) {

			if (!flag) return;
			if (m_view.m_body.m_upqualitytipui.playing)
			{
				/*m_view.m_body.m_upqualitytipui.Stop(true , true);
				EffectSystem.Instance.ReleaseEffect(effect);*/
				return;
			}
			DoCloseUIClick(null);

		}
	}
}
