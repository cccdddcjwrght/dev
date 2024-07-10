
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
			flag = false;
			var equip = (context.GetParam()?.Value as object[])?.Val<BaseEquip>(0);
			var recycle = (context.GetParam()?.Value as object[])?.Val<double>(1);

			if (equip == null)
			{
				DoCloseUIClick(null);
				return;
			}

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
			yield return new WaitForSeconds(2f);
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

			DoCloseUIClick(null);
		}

		partial void OnUICloseClick(ref bool state)
		{
			if (!flag) state = false;
			if (m_view.m_body.m_upqualitytipui.playing) state = false;
		}
	}
}
