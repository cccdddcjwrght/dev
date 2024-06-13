
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Pet;
	using Unity.Entities;
	using System.Collections;

	public partial class UIPetBorn
	{
		private Entity _effect;
		private bool _isCompleted;

		partial void InitLogic(UIContext context)
		{

			var pet = context.GetParam()?.Value.To<object[]>().Val<PetItem>(0);
			m_view.z = 300;
			m_view.m_model.z = -400;
			m_view.SetTextByKey(pet.name);
			m_view.m_model.SetPetInfo(pet);
			m_view.m_quality.selectedIndex = pet.tempQuality;
			_isCompleted = false;
			Logic().Start();
		}

		IEnumerator Logic()
		{
			yield return new WaitForSeconds(2.5f);
			_effect = EffectSystem.Instance.AddEffect(9, m_view);
			yield return new WaitForSeconds(1f);
			_isCompleted = true;
		}

		partial void UnInitLogic(UIContext context)
		{

			EffectSystem.Instance.ReleaseEffect(_effect);
			m_view.m_model.Release();
		}
	}
}
