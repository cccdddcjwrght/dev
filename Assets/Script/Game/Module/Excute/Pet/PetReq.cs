using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FairyGUI;
using GameConfigs;
using SGame.UI.Pet;

namespace SGame
{
	public enum PetHandler
	{
		None = 0,
		Evo = 1,//进化
		Enhance, //强化
	}

	partial class RequestExcuteSystem
	{

		[InitCall]
		static void InitPet()
		{
			DataCenter.PetUtil.Init();

		}

		static public void SetEggToBorn(PetItem egg) {

			if (egg != null && egg.type == 1)
			{
				var data = DataCenter.Instance.petData;
				data.egg = egg;
				_eMgr.Trigger(((int)GameEvent.GAME_MAIN_REFRESH));
			}
		}

		static public PetItem PetBron(PetItem egg)
		{

			if (egg != null && egg.type == 1)
			{
				var ws = egg.eCfg.GetWeightArray();
				var index = SGame.Randoms.Random._R.NextWeight(ws) + 1;
				var ps = ConfigSystem.Instance.Finds<PetsRowData>(p => p.Quality == index);
				var r = SGame.Randoms.Random._R.NextItem(ps);
				var p = DataCenter.PetUtil.AddPet(r.Id, true, true);

				UIUtils.OpenUI("petborn", p);
				PropertyManager.Instance.Update(1, egg.cfgID, 1, true);
				_eMgr.Trigger(((int)GameEvent.GAME_MAIN_REFRESH));
				return p;
			}
			return default;

		}

		static public bool PetFollow(PetItem pet)
		{
			if (pet != null && pet.cfg.IsValid())
			{
				DataCenter.PetUtil.Follow(pet);
				return true;
			}
			return false;
		}

		static public void PetFree(PetItem pet, bool needConfirm = true, Action queryCall = null)
		{
			const string rich_text = "<img src='ui://Pet/ui_pet_medal_01' width='50%' height='50%' />x{0}";

			var data = DataCenter.Instance.petData;
			if (pet == null || !pet.cfg.IsValid()) return;
			if (data.pets.Count > 1)
			{
				void Free(int index)
				{
					if (index == 0)
					{
						queryCall?.Invoke();
						if (pet.isselected) PetFollow(data.pets[1]);
						DataCenter.PetUtil.RemovePet(pet);
						var count = pet.GetFreeRebackItemCount();
						if (count > 0) PropertyManager.Instance.Update(1, ((int)ItemID.MEDAL_1), count);
					}
				}


				if (needConfirm)
				{
					void InitBody(FairyGUI.GObject gObject)
					{

						if (gObject is SGame.UI.Common.UI_ConfirmUI ui)
						{
							var view = ui.m_body;
							if (view != null)
							{
								view.m_top.url = "ui://Pet/PetMedalList";
								view.m_context.url = "ui://Pet/SimplePetModel";
								view.m_context.y -= 50;

								var model = view.m_context.component as UI_SimplePetModel;
								if (model != null) model.SetPetInfo(pet);
								UIListener.SetControllerSelect(view.m_top.component, "type", 3, false);
								view.m_top.component.SetCurrency(((int)ItemID.MEDAL_1), "c1")
									.SetCurrency(((int)ItemID.MEDAL_1) + 1, "c2")
									.SetCurrency(((int)ItemID.MEDAL_1) + 2, "c3");

							}
						}
					}

					UIUtils.Confirm(
						 "@ui_pet_free_title",
						 null, Free,
						 new string[] { "@ui_pet_free_ok", "@ui_pet_free_cancel" },
						 new object[] {
							 "desc","ui_pet_free_tips".Local(null , string.Format(rich_text , pet.GetFreeRebackItemCount())),
							 "init",new Action<FairyGUI.GObject>(InitBody)
						 }
					);
				}
				else
					Free(0);
			}
			else "@ui_pet_free_error".Tips();

		}

		static public void PetUp(PetItem pet)
		{
			if (pet != null && pet.cfg.IsValid())
			{
				var costID = pet.evoMat.ItemId;
				var cost = pet.evoMatCount;
				object go = true;
				Action<int> call = null;
				ExchangeRowData eCfg;

				if (ConfigSystem.Instance.TryGet<ExchangeRowData>(costID, out eCfg))
					call = new Action<int>((i) => MedalExchange(i == 0 ? eCfg : default));

				if (Utils.CheckItemCount(costID, cost, go: go, call: call))
				{
					var es = pet.Evo(out var isevo);
					PropertyManager.Instance.Update(1, costID, cost, true);

					if (es != null)
					{
						if (isevo)
						{
							var bs = DataCenter.PetUtil.GetBuffList(7);
							bs.Add(es);

							var rets = bs.Select(b =>
							{
								if (ConfigSystem.Instance.TryGet(b[0], out BuffRowData buff))
									return new string[] { buff.Icon, buff.Describe.Local(null, b[1]) };
								return null;

							}).ToList();
							UIUtils.OpenUI("randomselect", rets, new Action(() =>
							{
								_eMgr.Trigger(((int)GameEvent.PET_REFRESH), pet, ((int)PetHandler.Evo));
							}));
						}
						else
							_eMgr.Trigger(((int)GameEvent.PET_REFRESH), pet, ((int)PetHandler.Enhance));
						DataCenter.PetUtil.UpdatePetBuff();
					}
					else
						_eMgr.Trigger(((int)GameEvent.PET_REFRESH), pet, ((int)PetHandler.None));
				}
			}
		}

		static public void MedalExchange(ExchangeRowData cfg)
		{
			if (!cfg.IsValid()) return;

			void InitBody(FairyGUI.GObject gObject)
			{
				if (gObject is SGame.UI.Common.UI_ConfirmUI ui)
				{
					var view = ui.m_body;
					if (view != null)
					{
						view.m_context.url = "ui://Pet/Exchange";
						view.m_top.url = "ui://Pet/PetMedalList";
						UIListener.SetControllerSelect(view.m_top.component, "type", 4, false);

						view.m_top.component
							.SetCurrency(cfg.Ingredient, "c1", iconCtr: "price")
							.SetCurrency(cfg.ExchangeId, "c2", iconCtr: "price2");

						view.m_context.component
							.SetCurrency(cfg.Ingredient, "item1", "x" + Utils.ConvertNumberStr(cfg.Num), "price1", false)
							.SetCurrency(cfg.ExchangeId, "item2", "x" + 1, "price2", false);

						EventCallback0 call = new EventCallback0(() => view.m_ok.grayed = !Utils.CheckItemCount(cfg.Ingredient, cfg.Num, ""));
						view.m_ok.onClick.Add(call);
						call();

					}
				}
			}

			void Exchange(int index)
			{
				if (index == 0)
				{
					if (Utils.CheckItemCount(cfg.Ingredient, cfg.Num))
					{
						PropertyManager.Instance.Update(1, cfg.Ingredient, cfg.Num, true);
						PropertyManager.Instance.Update(1, cfg.ExchangeId, 1);
					}
				}
			}
			GTween.To(0, 1, 0.1f).OnComplete(() =>
			{

				UIUtils.Confirm(
					"@ui_pet_exchange_title", null, Exchange,
					new string[] { "@ui_pet_exchange_btn" },
					new object[] {
						"desc","@ui_pet_exchange_tips",
						"clickclose" , false,
						"init",new Action<FairyGUI.GObject>(InitBody)
					}
				);

			});
		}

	}

}
