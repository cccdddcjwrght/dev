
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
			m_view.z = 600;

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
			_current = _data.pet;
			var pBody = m_view.m_pet;
			var flag = _current != null && _current.cfg.IsValid();
			m_view.m_top.SetCurrency(flag ? _current.evoMat.ItemId : 3, "c1", null, "price");
			pBody.m_model.visible = flag;
			pBody.SetPet(_current, showbuff: true);
			if (flag)
			{
				pBody.m_model.m_step.selectedPage = _current.evoMat.ItemId.ToString();
				pBody.m_model.m_free.visible = _data.pets?.Count > 1;
				pBody.m_model.m_model.SetPetInfo(_current);
			}

		}

		partial void OnPetModel_Pet_model_clickClick(EventContext data)
		{
			RequestExcuteSystem.PetUp(_current);
		}

		partial void OnPetModel_Pet_model_freeClick(EventContext data)
		{
			RequestExcuteSystem.PetFree(_current);
		}

		#endregion

		#region Eggs

		void OnEggTab()
		{
			eggState = -1;
			_isClickEgg = true;
			SetEggHatching();
		}

		void SetEggHatching()
		{
			var eg = m_view.m_egg;
			var egg = _data.egg;
			eggState = 0;
			if (egg == null || egg.cfgID == 0) eg.m_select.selectedIndex = _current != null ? 1 : 0;
			else if (egg.GetRemainder() <= 0) eggState = 2;
			else eggState = 1;
			eg.m_get2.grayed = !DataCenter.AdUtil.IsAdCanPlay(c_pet_ad);
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
			DelayExcuter.Instance.OnlyWaitUIClose("shopui", () => OnTabChanged(null), true);
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
						RequestExcuteSystem.SetEggToBorn(_current.Clone().Bron());
						SetEggHatching();
						RefreshList();
					}
					break;
				case 2:
					var p = RequestExcuteSystem.PetBron(_data.egg);
					if (p != null) DelayExcuter.Instance.OnlyWaitUIClose("petborn", () => SwitchTabPage(0), true);
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
		}


		void OnSetPetInfo(int index, GObject gObject)
		{
			var pet = _pets[index];
			gObject.name = index.ToString();
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
					case 0:
						if (data.id != _data.petID)
							SGame.UIUtils.OpenUI("pettips", data);
						break;
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
		}
	}
}
