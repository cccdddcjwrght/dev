
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Pet;
	using Unity.Entities;

	public partial class UIPetBorn
	{
		private Entity _effect;

		partial void InitLogic(UIContext context)
		{

			var pet = context.GetParam()?.Value.To<object[]>().Val<PetItem>(0);
			m_view.SetTextByKey(pet.name);
			m_view.m_model.SetPetInfo(pet);
			Utils.Timer(0.3f, () =>
			{
				_effect = EffectSystem.Instance.AddEffect(9, m_view);
			});
		}

		partial void UnInitLogic(UIContext context)
		{

			EffectSystem.Instance.ReleaseEffect(_effect);
			m_view.m_model.Release();
		}
	}
}
