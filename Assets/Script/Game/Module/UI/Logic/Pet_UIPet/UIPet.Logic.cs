
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
	using System.Drawing;

	public partial class UIPet
	{
		const string c_pet_ad = "petborn";

		private List<PetItem> _pets;
		private List<PetItem> _eggs;
		private PetData _data { get { return DataCenter.Instance.petData; } }

		private PetItem _current;
		private List<int[]> _effects;
		private Action<bool> _timer;
		private UI_PetEggItem _selectEgg;
		private bool _dontRefresh = false;
		private bool _clickSkillFlag = false;

		private int eggState { get { return m_view.m_egg.m_state.selectedIndex; } set { m_view.m_egg.m_state.selectedIndex = value; } }

		private bool _isClickEgg;

		partial void InitLogic(UIContext context)
		{
			m_view.z = 400;
			m_view.m_eggpanel.z = -500;
			m_view.m_tips.z = -500;
			_dontRefresh = false;
			EventManager.Instance.Reg<PetItem, Action>(((int)GameEvent.PET_BORN_EVO), OnPetBornEvo);
			m_view.onClick.Add(OnViewClick);
			m_view.m_body.SetCurrency(2, "currency");
			InitList();
			SetInfo();
		}

		void SetInfo()
		{
			if (_dontRefresh) return;
			SetEggHatching();
			RefreshList(true, 1);
			OnPetTab();
			if (_isClickEgg)
				DataCenter.PetUtil.ClearNewFlag(-1);
		}

		partial void OnTabChanged(EventContext data)
		{
			_selectEgg = null;
			switch (m_view.m_tab.selectedIndex)
			{
				case 0:
					SetInfo();

					break;
				case 1:
					OnEggTab();
					break;
			}
		}

		#region Pets

		void OnPetTab()
		{
			if (_dontRefresh) return;
			_current = _current ?? _data.pet;
			var pBody = m_view.m_pet;
			var flag = _current != null && _current.cfg.IsValid();
			pBody.m_nullpet.selectedIndex = flag ? 0 : 1;
			pBody.SetPet(_current, showbuff: true, nameappend: flag ? "ui_common_lv1".Local(null, _current.level) + " " : null);
			if (flag)
			{
				m_view.m_type.selectedIndex = _current != _data.pet ? 1 : 0;
				pBody.m_model.m_model.SetPetInfo(_current);
			}

		}

		void OnPetClick(int index, PetItem data)
		{
			m_view.m_list.selectedIndex = index;
			if (_current != null) _current.ResetNewFlag();
			_current = data;
			OnPetTab();

		}

		partial void OnClickClick(EventContext data)
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

		void OnSetEggInfo(int index, GObject gObject)
		{
			var pet = _eggs[index];
			var view = gObject as UI_PetEggItem;
			view.name = pet.cfgID.ToString();
			view.m_hideline.selectedIndex = index == 0 ? 1 : 0;
			view.SetPet(pet);
			view.data = pet;
			view.m_get3.data = pet;
			view.onClick.Remove(OnEggListItemClick);
			view.onClick.Add(OnEggListItemClick);
			
			view.m_get1.onClick.Remove(OnPetEgg_Get1Click);
			view.m_get2.onClick.Remove(OnPetEgg_Get2Click);
			view.m_get3.onClick.Remove(OnPetEgg_Get3Click);
			view.m_get1.onClick.Add(OnPetEgg_Get1Click);
			view.m_get2.onClick.Add(OnPetEgg_Get2Click);
			view.m_get3.onClick.Add(OnPetEgg_Get3Click);

			if (eggState != 0)
			{
				if (index == 0)
				{
					view.m_state.selectedIndex = eggState;
					view.m_get3.SetTextByKey("ui_pet_get3");
					SetFirstEggInfo(view);
				}
				else
					view.m_state.selectedIndex = 3;
			}
			else
			{
				view.m_state.selectedIndex = 0;
				view.m_get3.SetTextByKey("ui_pet_select");
			}
		}

		void OnEggListItemClick(EventContext data) {

			var egg = data != null ? (data.sender as GObject)?.data as PetItem : null;
			if(egg!= null)
				OnEggItemClick(0, egg, data.sender as GObject);
		}

		void OnEggTab()
		{
			_isClickEgg = true;
			var list = m_view.m_eggpanel.m_egglist;
			RefreshList(type: 2);
			if (list.numItems > 0)
				_selectEgg = list.GetChildAt(0) as UI_PetEggItem;
		}

		void SetEggHatching()
		{
			var egg = _data.egg;
			if (egg == null || egg.cfgID == 0) eggState = 0;
			else if (egg.GetRemainder() <= 0) eggState = 2;
			else eggState = 1;
			SetFirstEggInfo();
		}

		void OnEggItemClick(int index, PetItem data, GObject gObject)
		{
			data.ResetNewFlag();
			DataCenter.PetUtil.ClearNewFlag(data.cfgID);
			UIListener.SetControllerSelect(gObject, "__redpoint", 0, false);
		}

		partial void OnPetEggBtn_StateChanged(EventContext data)
		{
			var selectIndex = eggState;
			var egg = _data.egg;
			switch (selectIndex)
			{
				case 1:
					m_view.m_egg.m_progress.max = egg.eCfg.Time;
					m_view.m_egg.m_quality.selectedIndex = egg.eCfg.Quality;
					SetPrice();
					_timer = Utils.Timer(egg.GetRemainder(), SetPrice, m_view, completed: SetEggHatching);
					break;
				case 2:
					m_view.m_egg.m_quality.selectedIndex = egg.eCfg.Quality;
					break;
			}
		}

		void SetPrice()
		{
			var egg = _data.egg;
			if (egg != null && egg.cfgID != 0)
			{
				var time = egg.GetRemainder();
				m_view.m_egg.m_progress.value = m_view.m_egg.m_progress.max - time;
				m_view.m_egg.m_time.SetText(Utils.FormatTime(time), false);
				if (_selectEgg != null)
				{
					_selectEgg.m_progress.max = m_view.m_egg.m_progress.max;
					_selectEgg.m_progress.value = m_view.m_egg.m_progress.max - time;
					_selectEgg.m_time.SetText(Utils.FormatTime(time), false);
					_selectEgg.m_get1.SetText(Utils.ConvertNumberStr(egg.GetImmCost()), false);
				}
			}
		}

		partial void OnEggClick(EventContext data)
		{
			switch (eggState)
			{
				case 0:
					OnPetEggBtn_AddClick(null);
					break;
				case 1:
					SwitchTabPage(1);
					break;
				case 2:
					OnPetEgg_Get3Click(null);
					break;
			}
		}

		partial void OnPetEggBtn_AddClick(EventContext data)
		{
			GetEggs(out var flag);
			if (!flag) "shop".Goto();
			else SwitchTabPage(1);
		}

		void OnPetEgg_Get1Click(EventContext data)
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

		void OnPetEgg_Get2Click(EventContext data)
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

		void OnPetEgg_Get3Click(EventContext data)
		{
			var egg = data != null ? (data.sender as GObject)?.data as PetItem : null;
			switch (eggState)
			{
				case 0:
					if (egg != null)
					{
						RequestExcuteSystem.SetEggToBorn(egg.Clone().Born());
						//SwitchTabPage(0);
						egg.ResetNewFlag();
						DataCenter.PetUtil.ClearNewFlag(egg.cfgID);
						SetEggHatching();
						RefreshList(true, 2);
					}
					break;
				case 2:
					_dontRefresh = true;
					var p = RequestExcuteSystem.PetBorn(_data.egg);
					if (p != null)
					{
						SwitchTabPage(0);
						DelayExcuter.Instance.OnlyWaitUIClose("petborn", () =>
						{
							if (m_view == null) return;
							_dontRefresh = false;
							OnTabChanged(null);
						}, true);
					}
					else _dontRefresh = false;

					break;
			}
		}

		void SetFirstEggInfo(UI_PetEggItem item = null)
		{
			var list = m_view.m_eggpanel.m_egglist;
			if (item != null || list.numItems > 0)
			{
				item = item ?? list.GetChildAt(0) as UI_PetEggItem;
				if (item != null)
				{
					item.m_state.selectedIndex = eggState;
					item.m_get2.grayed = !DataCenter.AdUtil.IsAdCanPlay(c_pet_ad) || !NetworkUtils.IsNetworkReachability();
				}
			}
		}

		List<PetItem> GetEggs(out bool has, bool refresh = true)
		{
			var v = false;
			if (_eggs == null)
			{
				_eggs = ConfigSystem.Instance
						.Finds<GameConfigs.ItemRowData>(c => c.Type == ((int)EnumItemType.PetEgg))
						.Select(c => new PetItem().SetEgg(c.ItemId))
						.ToList();
			}
			if (refresh)
				_eggs.ForEach(e => { e.Refresh(); v = v || e.count > 0; });
			has = v;
			return _eggs;
		}

		#endregion

		#region Base

		void InitList()
		{
			m_view.m_list.SetVirtual();
			m_view.m_list.RemoveChildrenToPool();
			m_view.m_list.itemRenderer = OnSetPetInfo;
			m_view.m_eggpanel.m_egglist.RemoveChildrenToPool();
			m_view.m_eggpanel.m_egglist.itemRenderer = OnSetEggInfo;
			m_view.m_pet.m_bufflist.onClickItem.Add(OnPetBuffClick);
		}

		void RefreshList(bool refresh = false, int type = 0)
		{
			var list = default(List<PetItem>);
			if (_dontRefresh) return;
			switch (type)
			{
				case 1: list = _pets = refresh || _pets == null ? DataCenter.PetUtil.GetPetsByCondition() : _pets; break;
				case 2: list = GetEggs(out _); break;
				default:
					RefreshList(refresh, 1);
					RefreshList(refresh, 2);
					return;
			}
			SetItemList(type, list);
		}

		void SetItemList(int type, List<PetItem> pets = null, bool sort = true)
		{
			if (sort && pets?.Count > 1) pets.Sort(DataCenter.PetUtil.PetSort);
			var list = type == 1 ? m_view.m_list : m_view.m_eggpanel.m_egglist;
			if (pets != null)
			{
				list.numItems = pets.Count;
				list.selectedIndex = Math.Max(0, pets.FindIndex(p => p == _current));
			}
			else
			{
				list.numItems = 0;
				list.selectedIndex = 0;
			}
		}

		void OnSetPetInfo(int index, GObject gObject)
		{
			var pet = _pets[index];
			gObject.name = pet.cfgID.ToString();
			gObject.SetPet(pet);
			gObject.onClick?.Clear();
			gObject.onClick.Add(() => OnPetClick(index, pet, gObject));
			if (pet.type == 0)
				UIListener.SetControllerSelect(gObject, "selected", pet.isselected ? 1 : 0, false);
			else
			{
				var s = pet.cfgID == _data.egg?.cfgID;
				if (s) m_view.m_list.selectedIndex = index;
				UIListener.SetControllerSelect(gObject, "selected", s ? 1 : 0, false);
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
						//case 1: OnEggItemClick(index, data, gObject); break;
				}
			}
		}

		void OnPetBuffClick(EventContext e)
		{
			var item = e.data as UI_PetBuffIcon;
			if (item != null)
			{
				item.m_type.selectedIndex = 0;
				var data = item.data as int[];
				if (data != null && data!= m_view.m_tips.data)
				{
					_clickSkillFlag = true;
					m_view.m_tips.alpha = 0;
					m_view.m_tips.data = data;
					m_view.m_tips.visible = true;
					m_view.m_tips.xy = item.TransformPoint(item.size * 0.5f, m_view) + new Vector2(0,-40);
					m_view.m_tips.SetBuffItem(data as int[], 0, appenddesc: "ui_common_lv1".Local(null, data[6] + 1) + "\n");
					m_view.m_tips.TweenFade(1, 0.3f);
				}
			}
		}

		void OnViewClick(EventContext data)
		{

			if (!_clickSkillFlag)
			{
				if (m_view != null && m_view.m_tips!= null && m_view.m_tips.visible)
				{
					m_view.m_tips.visible = false;
					m_view.m_tips.data = default;
				}
			}
			_clickSkillFlag = false;
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
			_selectEgg = default;
			EventManager.Instance.UnReg<PetItem, Action>(((int)GameEvent.PET_BORN_EVO), OnPetBornEvo);

		}
	}
}
