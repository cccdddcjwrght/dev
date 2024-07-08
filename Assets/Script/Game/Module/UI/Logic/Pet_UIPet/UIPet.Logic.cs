
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Pet;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	using System;
	using GameConfigs;

	public partial class UIPet
	{
		const string c_pet_ad = "petborn";

		private List<PetItem> _pets;
		private List<PetItem> _eggs;
		private PetData _data { get { return DataCenter.Instance.petData; } }

		private PetItem _current;
		private List<int[]> _effects;
		private Action<bool> _timer;

		private int eggState { get { return m_view.m_egg.m_state.selectedIndex; } set { m_view.m_egg.m_state.selectedIndex = value; } }

		private bool _isClickEgg;

		partial void InitLogic(UIContext context)
		{
			EventManager.Instance.Reg<PetItem, Action>(((int)GameEvent.PET_BORN_EVO), OnPetBornEvo);
			InitList();
			SwitchTabPage(0);
			OnTabChanged(null);
		}

		partial void OnTabChanged(EventContext data)
		{
			_current = null;
			_timer?.Invoke(false);
			m_view.m_list.ClearSelection();
			RefreshList(true);
			switch (m_view.m_tab.selectedIndex)
			{
				case 0: OnPetTab(); break;
				case 1: OnEggTab(); break;
			}
		}

		#region Pets

		void OnPetTab()
		{
			_current = _current ?? _data.pet;
			var pBody = m_view.m_pet;
			var flag = _current != null && _current.cfg.IsValid();
			pBody.m_nullpet.selectedIndex = flag ? 0 : 1;
			pBody.SetPet(_current, showbuff: true);
			if (flag)
			{
				pBody.m_model.m_level.SetTextByKey("ui_common_lv1", _current.level);
				pBody.m_model.m_model.SetPetInfo(_current);
				pBody.m_model.m_type.selectedIndex = _current != _data.pet ? 1 : 0;
			}

		}

		void OnPetClick(int index, PetItem data)
		{
			m_view.m_list.selectedIndex = index;
			if (_current != null) _current.ResetNewFlag();
			_current = data;
			OnPetTab();

		}

		partial void OnPetModel_Pet_model_clickClick(EventContext data)
		{
			if (_current.isselected) return;
			RequestExcuteSystem.PetFollow(_current);
			DoCloseUIClick(null);
		}

		void OnPetBornEvo(PetItem pet, Action complete)
		{
			IEnumerator Run()
			{
				SwitchTabPage(0);
				m_view.m___effect.xy = m_view.size * 0.5f;
				var e = EffectSystem.Instance.AddEffect(33, m_view);
				yield return EffectSystem.Instance.WaitEffectLoaded(e);
				var index = _pets.IndexOf(pet);
				m_view.m_list.ScrollToView(index);
				var target = m_view.m_list.GetChild(pet.cfgID.ToString());
				var tweener = m_view.m___effect.TweenMove(target.TransformPoint(target.size * 0.5f, m_view), 0.5f);
				yield return new WaitForSeconds(0.5f);

				complete?.Invoke();
			}
			Run().Start();
		}

		#endregion

		#region Eggs

		void OnEggTab()
		{
			eggState = -1;
			_isClickEgg = true;
			SetEggHatching();
			if (eggState == 0)
			{
				if (_pets?.Count > 0)
					OnEggItemClick(0, _pets[0], null);
			}
		}

		void SetEggHatching()
		{
			var eg = m_view.m_egg;
			var egg = _data.egg;
			eggState = 0;
			if (egg == null || egg.cfgID == 0) eg.m_select.selectedIndex = _current != null ? 1 : 0;
			else if (egg.GetRemainder() <= 0) eggState = 2;
			else eggState = 1;
			eg.m_get2.grayed = !DataCenter.AdUtil.IsAdCanPlay(c_pet_ad) || !NetworkUtils.IsNetworkReachability();
		}

		void OnEggItemClick(int index, PetItem data, GObject gObject)
		{
			DataCenter.PetUtil.ClearNewFlag(data.cfgID);
			switch (eggState)
			{
				case 0:
					_current = data;
					SetEggHatching();
					m_view.m_list.selectedIndex = index;
					m_view.m_egg.m_quality.selectedIndex = _current.eCfg.Quality;
					return;
				case 1: "@tips_egg_1".Tips(); break;
				case 2: "@tips_egg_2".Tips(); break;
			}
		}

		partial void OnPetEgg_StateChanged(EventContext data)
		{
			var selectIndex = eggState;
			var egg = _data.egg;
			switch (selectIndex)
			{
				case 0:
					m_view.m_egg.m_get3.SetTextByKey(_pets?.Count > 0 ? "ui_pet_select" : "ui_pet_buy");
					break;
				case 1:
					m_view.m_egg.m_progress.max = egg.eCfg.Time;
					m_view.m_egg.m_quality.selectedIndex = egg.eCfg.Quality;
					SetPrice();
					_timer = Utils.Timer(egg.GetRemainder(), SetPrice, m_view, completed: SetEggHatching);
					break;
				case 2:
					m_view.m_egg.m_quality.selectedIndex = egg.eCfg.Quality;
					m_view.m_egg.m_get3.SetTextByKey("ui_pet_get3");
					break;
			}
		}

		void SetPrice()
		{
			var egg = _data.egg;
			var time = egg.GetRemainder();
			m_view.m_egg.m_progress.value = m_view.m_egg.m_progress.max - time;
			m_view.m_egg.m_time.SetText(Utils.FormatTime(time), false);
			m_view.m_egg.m_price.SetText(Utils.ConvertNumberStr(egg.GetImmCost()), false);
		}

		partial void OnPetEgg_AddClick(EventContext data)
		{
			"shop".Goto();
		}

		partial void OnPetEgg_Get1Click(EventContext data)
		{
			var cost = _data.egg.GetImmCost();
			if (cost > 0)
			{
				if (Utils.CheckItemCount(((int)ItemID.DIAMOND), cost, go: true))
				{
					if (cost > 0) PropertyManager.Instance.Update(1, ((int)ItemID.DIAMOND), cost, true);
					_data.egg.time = 0;
					_data.egg.count = cost;
					_data.egg.bornType = 1;
					CloseTimer();
					OnPetEgg_Get3Click(null);
				}
			}
		}

		partial void OnPetEgg_Get2Click(EventContext data)
		{
			AdModule.PlayAd(c_pet_ad, (s) =>
			{
				if (s)
				{
					_data.egg.time = 0;
					_data.egg.bornType = 2;
					CloseTimer();
					OnPetEgg_Get3Click(null);
				}
			});
		}

		partial void OnPetEgg_Get3Click(EventContext data)
		{
			switch (eggState)
			{
				case 0:
					if (_pets.Count == 0)
					{
						OnPetEgg_AddClick(null);
						return;
					}
					if (_current != null)
					{
						RequestExcuteSystem.SetEggToBorn(_current.Clone().Born());
						SetEggHatching();
						RefreshList();
					}
					break;
				case 2:
					var p = RequestExcuteSystem.PetBorn(_data.egg);
					if (p != null) DelayExcuter.Instance.OnlyWaitUIClose("petborn", () =>
					{
						if (m_view == null) return;
						SwitchTabPage(0);
					}, true);
					break;
			}
		}

		List<PetItem> GetEggs()
		{
			if (_eggs == null)
			{
				_eggs = ConfigSystem.Instance
						.Finds<GameConfigs.ItemRowData>(c => c.Type == ((int)EnumItemType.PetEgg))
						.Select(c => new PetItem().SetEgg(c.ItemId))
						.ToList();
			}
			else
				_eggs.ForEach(e => e.Refresh());
			return _eggs.FindAll(e => e.count > 0);
		}

		#endregion

		#region Base

		void InitList()
		{
			m_view.m_list.SetVirtual();
			m_view.m_list.RemoveChildrenToPool();
			m_view.m_list.itemRenderer = OnSetPetInfo;
		}

		void RefreshList(bool refresh = false)
		{
			if (refresh)
			{
				_pets?.Clear();
				switch (m_view.m_tab.selectedIndex)
				{
					case 0:
						_pets = DataCenter.PetUtil.GetPetsByCondition();
						break;
					case 1:
						_pets = GetEggs();
						break;
				}
			}
			SetItemList(_pets);
		}

		void SetItemList(List<PetItem> pets = null, bool sort = true)
		{
			_pets = pets ?? _pets;
			if (sort && _pets?.Count > 1)
				_pets.Sort(DataCenter.PetUtil.PetSort);
			m_view.m_list.numItems = _pets.Count;
			m_view.m_list.selectedIndex = _pets.FindIndex(p => p.isselected);
		}


		void OnSetPetInfo(int index, GObject gObject)
		{
			var pet = _pets[index];
			gObject.name = pet.cfgID.ToString();
			gObject.SetPet(pet);
			gObject.onClick?.Clear();
			gObject.onClick.Add(() => OnPetClick(index, pet, gObject));
			if (pet.type == 0)
				UIListener.SetControllerSelect(gObject, "selected", pet.isselected ? 1 : 0);
			else
			{
				var s = pet.cfgID == _data.egg?.cfgID;
				if (s) m_view.m_list.selectedIndex = index;
				UIListener.SetControllerSelect(gObject, "selected", s ? 1 : 0);
			}
		}

		void OnPetClick(int index, PetItem data, GObject gObject)
		{
			if (data != null)
			{
				UIListener.SetControllerSelect(gObject, "__redpoint", 0, false);
				data.isnew = 0;
				switch (data.type)
				{
					case 0: OnPetClick(index, data); break;
					case 1: OnEggItemClick(index, data, gObject); break;
				}
			}
		}

		void CloseTimer()
		{
			_timer?.Invoke(true);
		}

		#endregion

		partial void UnInitLogic(UIContext context)
		{
			_timer?.Invoke(false);
			DataCenter.PetUtil.ClearNewFlag(_isClickEgg ? -1 : 0);
			m_view.m_pet.m_model.m_model.Release();
			EventManager.Instance.UnReg<PetItem, Action>(((int)GameEvent.PET_BORN_EVO), OnPetBornEvo);

		}
	}
}
