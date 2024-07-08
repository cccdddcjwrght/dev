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

		static public void SetEggToBorn(PetItem egg)
		{

			if (egg != null && egg.type == 1)
			{
				var data = DataCenter.Instance.petData;
				data.egg = egg;
				_eMgr.Trigger(((int)GameEvent.GAME_MAIN_REFRESH));
			}
		}

		static public PetItem AddPet(int petid, int quality = 1)
		{
			if (petid > 0)
			{
				var p = DataCenter.PetUtil.CreatePet(petid);
				var buffindex = -1;
				var addVal = 0;
				var ownerPet = DataCenter.PetUtil.GetPetsByCondition(p => p.cfgID == petid, null, true);
				if (ownerPet?.Count > 0)
				{
					p = ownerPet[0].Evo(p, out buffindex, out addVal);
					if (buffindex < 0)
						PropertyManager.Instance.Insert2Cache(new List<int[]>() { p.cfg.GetRecycleRewardArray() });
					else if (p.id == DataCenter.Instance.petData.petID)
						DataCenter.PetUtil.UpdatePetBuff();

				}
				else
					DataCenter.PetUtil.AddPet(p, true, true);

				DataCenter.Instance.petData.egg = default;
				p.tempQuality = quality;
				UIUtils.OpenUI("petborn", p, buffindex, addVal);
				_eMgr.Trigger(((int)GameEvent.GAME_MAIN_REFRESH));
				_eMgr.Trigger((int)GameEvent.RECORD_PROGRESS, (int)RecordDataEnum.PET_BORN, 1);

				return p;
			}
			return default;
		}

		static public PetItem PetBorn(PetItem egg)
		{

			if (egg != null && egg.type == 1)
			{
				var ws = egg.eCfg.GetWeightArray();
				var quality = egg.eCfg.Quality;
				var index = SGame.Randoms.Random._R.NextWeight(ws) + 1;
				var ps = ConfigSystem.Instance.Finds<PetsRowData>(p => p.Quality == index);
				var r = SGame.Randoms.Random._R.NextItem(ps);

				var p = AddPet(r.Id, quality);
				if (p != null)
				{
					PropertyManager.Instance.Update(1, egg.cfgID, 1, true);
					_eMgr.Trigger((int)GameEvent.PET_BORN, p.cfgID, egg.bornType, p.quality, p.level);
				}
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

	}

}
