
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Pet;
	using Unity.Entities;
	using System.Collections;
	using System;
	using UnityEngine.UI;

	public partial class UIPetBorn
	{
		private Entity _effect;
		private bool _isCompleted;

		private PetItem _pet;
		private int _index;
		private int _add;
		private bool _needAnim = false;
		private UIWindow _mask;
		private bool _isNew;


		partial void InitLogic(UIContext context)
		{
			var args = context.GetParam<object[]>();
			var pet = _pet = args.Val<PetItem>(0);
			_index = args.Val<int>(1);
			_add = args.Val<int>(2);

			m_view.z = -400;
			m_view.SetTextByKey(pet.name);
			m_view.m_model.SetPetInfo(pet);
			m_view.m_quality.selectedIndex = pet.tempQuality;
			_isCompleted = false;
			Logic().Start();

			context.window.AddEventListener("OnMaskClick", DoClose);

		}

		IEnumerator Logic()
		{
			_isNew = _add == 0;
			yield return new WaitForSeconds(2.5f);
			_effect = EffectSystem.Instance.AddEffect(9, m_view);
			if (_index >= 0 && _add > 0)
			{
				m_view.m_change.SetBuffItem(_pet.effects[_index], 0, false, 0, _add, true);
				m_view.m_change.m_level.SetTextByKey("ui_common_lv1", _pet.level);
			}
			m_view.m_type.selectedIndex = _isNew ? 0 : _index >= 0 ? 1 : 2;
			yield return new WaitForSeconds(0.8f);
			m_view.m_model.Play("show");
			_isCompleted = true;
		}

		partial void UnInitLogic(UIContext context)
		{

			EffectSystem.Instance.ReleaseEffect(_effect);
			m_view.m_model.Release();
			if (_mask != null)
				_mask.Value.z = 0;
		}

		void OnEffectCompleted()
		{
			if (_mask != null) _mask.Value.alpha = 1;
			m_view.m_type.selectedIndex = 3;
			m_view.visible = true;
			_isCompleted = true;
			_index = -1;
			_isNew = true;
		}

		void DoClose()
		{
			if (_isCompleted)
			{
				if (_index >= 0)
				{
					_isCompleted = false;
					_mask = SGame.UIUtils.GetUIView("mask");
					if (_mask != null)
					{
						_mask.Value.alpha = 0.01f;
						_mask.Value.z = -250;
					}
					m_view.visible = false;
					EventManager.Instance.Trigger(((int)GameEvent.PET_BORN_EVO), _pet, new Action(OnEffectCompleted));
				}
				else
				{
					if (!_isNew)
						SGame.Utils.ShowRewards(title: "@ui_pet_recycle_title").Append(_pet.cfg.GetRecycleRewardArray()).fly = true;

					SGame.UIUtils.CloseUIByID(__id);
				}
			}
		}
	}
}
