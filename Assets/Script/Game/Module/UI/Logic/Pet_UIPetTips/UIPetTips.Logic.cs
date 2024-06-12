
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Pet;
	using System.Collections.Generic;

	public partial class UIPetTips
	{
		private PetItem _pet;
		private List<int[]> _effects;

		partial void InitLogic(UIContext context)
		{
			m_view.z = 300;
			_pet = context.GetParam()?.Value.To<object[]>().Val<PetItem>(0);
			if (_pet == null) DoCloseUIClick(null);
			else
			{
				m_view.SetPet(_pet, showbuff: true);
				ShowModel();
			}
		}

		void ShowModel()
		{
			m_view.m_model.SetPetInfo(_pet);
		}


		partial void OnClickClick(EventContext data)
		{
			if (RequestExcuteSystem.PetFollow(_pet))
				DoCloseUIClick(null);
		}

		partial void OnFreeClick(EventContext data)
		{
			RequestExcuteSystem.PetFree(_pet, true, () =>
			{
				DoCloseUIClick(null);
			});
		}

		partial void UnInitLogic(UIContext context)
		{
			m_view.m_model?.Release();
		}
	}
}
