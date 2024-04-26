using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core.Record;
using GameConfigs;

namespace SGame
{
	[Flags]
	public enum EvoStep
	{
		Evo1 = 1,
		Evo2 = 2,
		Evo3 = 4
	}

	partial class DataCenter
	{
		public int globalID = 1000;

		public static int GID { get { return Instance.globalID++; } }

		public PetData petData = new PetData();

		public static class PetUtil
		{


			static public float c_one_d_2_time;

			static private PetData _data { get { return Instance.petData; } }

			static private EventManager _eMgr = EventManager.Instance;

			static public void Init()
			{
				_data.pets?.ForEach(p => p.Refresh());
				_data.egg?.Refresh();
				_data.pet = _data.pets?.Find(p => p.id == _data.petID);
				if (_data.pet != null) _data.pet.isselected = true;
				c_one_d_2_time = GlobalDesginConfig.GetInt("hatch_diamond_time", 100);
				UpdatePetBuff();
				_eMgr.Reg(((int)GameEvent.BUFF_RESET), () => UpdatePetBuff());
			}

			static public void AddPets(params int[] pets)
			{
				if (pets?.Length > 0)
				{
					for (int i = 0; i < pets.Length; i++)
						AddPet(pets[i], false);
					_data.pets.Sort(PetSort);
					_eMgr.Trigger(((int)GameEvent.PET_LIST_REFRESH));
				}
			}

			static public PetItem AddPet(int id, bool triggerevent = true, bool iseggborn = false)
			{
				if (id > 0)
				{
					if (ConfigSystem.Instance.TryGet<PetsRowData>(id, out var cfg))
					{
						var p = new PetItem() { cfgID = id, cfg = cfg, isnew = 1 }.Refresh();
						_data.pets.Add(p);
						if (iseggborn) { _data.egg.Clear(); _data.egg = null; }
						_eMgr.Trigger(((int)GameEvent.PET_ADD), p);
						if (_data.pet == null || !_data.pet.cfg.IsValid())
							Follow(p);
						if (triggerevent)
						{
							Resort();
							_eMgr.Trigger(((int)GameEvent.PET_LIST_REFRESH));
						}
						return p;
					}
				}
				return default;
			}

			static public void RemovePets(params PetItem[] pets)
			{
				if (pets?.Length > 0)
				{
					for (int i = 0; i < pets.Length; i++)
						RemovePet(pets[i], false);
					_eMgr.Trigger(((int)GameEvent.PET_LIST_REFRESH));
				}
			}

			static public void RemovePet(PetItem pet, bool triggerevent = true)
			{

				if (pet != null && pet.type == 0)
				{
					if (_data.pets.Contains(pet))
					{
						_data.pets.Remove(pet);
						if (triggerevent)
							_eMgr.Trigger(((int)GameEvent.PET_LIST_REFRESH));
					}
				}
			}

			static public void Follow(PetItem pet , bool refresh = true)
			{
				if (_data.petID != 0)
				{
					_data.pet.isselected = false;
					_eMgr.Trigger(((int)GameEvent.PET_FOLLOW_CHANGE), _data.pet, false);
				}
				_data.petID = pet.id;
				_data.pet = pet.Follow();
				_eMgr.Trigger(((int)GameEvent.PET_FOLLOW_CHANGE), _data.pet, true);
				_eMgr.Trigger(((int)GameEvent.ROLE_EQUIP_CHANGE));
				Resort(); 
				UpdatePetBuff();
			}

			static public void Resort()
			{
				if (_data.pets.Count > 1) _data.pets.Sort(PetSort);
			}

			static public void ClearNewFlag()
			{
				_data.pets?.ForEach(p => p?.ResetNewFlag());
			}

			static public List<PetItem> GetPetsByCondition(Predicate<PetItem> condition = null, List<PetItem> rets = null)
			{
				rets = rets ?? new List<PetItem>();
				if (condition != null)
				{
					for (int i = 0; i < _data.pets.Count; i++)
					{
						var p = _data.pets[i];
						if (condition(p))
							rets.Add(p);
					}
				}
				else rets.AddRange(_data.pets);
				return rets;
			}

			static public ulong RandomEffectID(int quality, out List<int[]> effects)
			{
				ulong id = 0;
				effects = default;
				if (ConfigSystem.Instance.TryGet<EntryRowData>(quality, out var cfg))
				{
					effects = new List<int[]>();
					var buffs = Utils.GetArrayList(
						cfg.GetBuff1Array,
						cfg.GetBuff2Array,
						cfg.GetBuff3Array,
						cfg.GetBuff4Array,
						cfg.GetBuff5Array,
						cfg.GetBuff6Array,
						cfg.GetBuff7Array,
						cfg.GetBuff8Array,
						cfg.GetBuff9Array
					);

					var ws = Utils.GetArrayList(
						cfg.GetWeight1Array,
						cfg.GetWeight2Array,
						cfg.GetWeight3Array,
						cfg.GetWeight4Array,
						cfg.GetWeight5Array,
						cfg.GetWeight6Array,
						cfg.GetWeight7Array,
						cfg.GetWeight8Array,
						cfg.GetWeight9Array
					);

					return Utils.RandomEffectID(buffs, ws, effects);

				}
				return id;
			}

			static public List<int[]> GetBuffList(int quality)
			{
				if (ConfigSystem.Instance.TryGet<EntryRowData>(quality, out var cfg))
				{
					return Utils.GetArrayList(
						cfg.GetBuff1Array,
						cfg.GetBuff2Array,
						cfg.GetBuff3Array,
						cfg.GetBuff4Array,
						cfg.GetBuff5Array,
						cfg.GetBuff6Array,
						cfg.GetBuff7Array,
						cfg.GetBuff8Array,
						cfg.GetBuff9Array
					);
				}
				return default;
			}

			static public void UpdatePetBuff(bool remove = false)
			{
				EventManager.Instance.Trigger(((int)GameEvent.BUFF_TRIGGER), new BuffData() { id = 0, from = ((int)EnumFrom.Pet) });
				if (!remove)
				{
					var pet = _data.pet;
					if (pet != null && pet.cfg.IsValid())
						Utils.ToBuffDatas(pet.GetEffects(true, false, true), ((int)EnumFrom.Pet))
							?.ForEach(buff => EventManager.Instance.Trigger(((int)GameEvent.BUFF_TRIGGER), buff));
				}
			}

			static public bool EggCanBorn()
			{
				return _data.egg != null && _data.egg.type == 1 && _data.egg.GetRemainder() <= 0;
			}

			static public int GetBornTime()
			{
				if (_data.egg != null && _data.egg.type == 1)
					return _data.egg.GetRemainder();
				return 0;
			}

			static public int PetSort(PetItem a, PetItem b)
			{
				if (a.isselected) return -1;
				else if (b.isselected) return 1;
				var c = -a.quality.CompareTo(b.quality);
				if (c == 0)
				{
					c = b.evoMat.ItemId.CompareTo(a.evoMat.ItemId);
					if (c == 0)
					{
						c = -a.cfgID.CompareTo(b.cfgID);
						if (c == 0) c = -a.id.CompareTo(b.id);
					}
				}
				return c;
			}

		}

	}

	[System.Serializable]
	public class PetData
	{

		public List<PetItem> pets = new List<PetItem>();
		public int petID;
		public PetItem egg;

		[NonSerialized]
		public PetItem pet;

	}

	[System.Serializable]
	public class PetItem
	{
		const int MAX_EVO = 3;

		public int id;
		public int cfgID;
		public int type;

		public int step;
		public int[] evotimes;

		public int time;
		public ulong effectID;
		public int[] effectAdd = new int[9];
		public int isnew;
		public int uptime;

		public string name { get { return cfg.IsValid() ? cfg.Name : item.IsValid() ? item.Name : null; } }
		public string icon { get { return cfg.IsValid() ? cfg.Icon : item.IsValid() ? item.Icon : null; } }
		public int quality { get { return cfg.IsValid() ? cfg.Quality : eCfg.IsValid() ? eCfg.Quality : 1; } }

		public bool isselected { get; set; }

		public GameConfigs.PetsRowData cfg;
		public GameConfigs.ItemRowData item;
		public GameConfigs.EggRowData eCfg;
		public GameConfigs.EvolutionRowData evoCfg;

		public GameConfigs.ItemRowData evoMat;
		[NonSerialized]
		public double evoMatCount;

		[NonSerialized]
		public List<int[]> effects;
		[NonSerialized]
		public double count;
		[NonSerialized]
		public int evo;
		public PetItem()
		{
			id = DataCenter.GID;
		}

		public PetItem Refresh()
		{
			if (type == 0)
			{
				if (cfg.IsValid() || ConfigSystem.Instance.TryGet(cfgID, out cfg))
				{
					ConfigSystem.Instance.TryGet(cfg.Quality, out evoCfg);
					if (evotimes == null) evotimes = evoCfg.GetBasicEvolutionTimeArray();

					if (this.effectID == 0)
						this.effectID = DataCenter.PetUtil.RandomEffectID(quality, out effects);
					else
						this.effects = Utils.ConvertId2Effects(effectID, DataCenter.PetUtil.GetBuffList(quality));
					GetEvoMat(out evoMatCount, out evoMat);

				}
			}
			else if (type == 1)
			{
				if (ConfigSystem.Instance.TryGet(cfgID, out item))
				{
					ConfigSystem.Instance.TryGet(item.TypeId, out eCfg);
					count = PropertyManager.Instance.GetItem(cfgID).num;
				}
			}
			return this;
		}

		public PetItem SetEgg(int id, double count = 0)
		{
			type = 1;
			cfgID = id;
			Refresh();
			if (count != 0)
				this.count = count;
			return this;
		}

		public PetItem Follow()
		{
			if (type == 0)
			{
				isselected = true;
			}
			return this;
		}

		public PetItem Bron()
		{
			if (eCfg.IsValid())
			{
				time = GameServerTime.Instance.serverTime + eCfg.Time;
				isselected = true;
			}
			return this;
		}

		public PetItem Clone()
		{
			return new PetItem() { type = type, cfgID = cfgID, step = step, effectID = effectID }.Refresh();
		}

		public double GetImmCost()
		{
			if (type == 1 && time != 0)
				return (int)Math.Ceiling(GetRemainder() / DataCenter.PetUtil.c_one_d_2_time);
			return 0;
		}

		public int GetRemainder()
		{
			if (time > 0)
				return Math.Max(0, time - GameServerTime.Instance.serverTime);
			return 0;
		}

		public List<int[]> GetEffects(bool valid = true, bool replaceNull = false, bool useAdd = false)
		{
			if (valid)
			{
				if (type == 0)
				{
					var rets = new List<int[]>();
					var s = GetEvoStep();
					for (int i = 0; i < effects.Count; i++)
					{
						//前三条是进化属性
						if (i < MAX_EVO && !(1 << i).IsInState(s))
						{
							if (replaceNull) rets.Add(null);
						}
						else if (!useAdd)
							rets.Add(effects[i]);
						else
						{
							var v = (int)Utils.ToInt(effects[i][1] * (100 + effectAdd[i]) * 0.01f);
							rets.Add(new int[] { effects[i][0], v });
						}
					}
					return rets;
				}
			}
			return effects;
		}

		public int[] GetEffectRandomWeight(int weight = 100)
		{

			var ws = new int[effects.Count];
			var s = GetEvoStep();

			for (int i = 0; i < effects.Count; i++)
			{
				if (effects[i] == null || (i < MAX_EVO && !(1 << i).IsInState(s))) ws[i] = 0;
				else ws[i] = weight;

			}
			return ws;
		}

		public bool GetEvoMat(out double num, out GameConfigs.ItemRowData mat)
		{
			num = 0;
			mat = default;
			if (type == 0)
			{
				if (evoCfg.IsValid())
				{
					var id = 0;
					if (CanEvo(out var index, out _))
					{
						id = evoCfg.BadgeId(index);
						num = evoCfg.BadgeNum(index);
					}
					else
					{
						id = evoCfg.SpecialEvolutionNeed(1);
						num = evoCfg.SpecialEvolutionNeed(2);
					}
					if (id > 0)
						ConfigSystem.Instance.TryGet(id, out mat);
				}
			}
			return default;
		}

		public bool IsMaxEnhance()
		{
			return step >= evoCfg.SpecialEvolutionTime * 10;
		}

		public int[] Evo(out bool isevo)
		{
			isevo = false;
			uptime++;
			if (CanEvo(out var index, out var count))
			{
				isevo = true;
				if (SGame.Randoms.Random._R.Rate(evoCfg.BasicEvolutionRate(index)))
				{
					step += 1 << index;
					evotimes[index] = 0;
					Refresh();
					evo = (evo << 1) + (1 << 0);
					return GetEffects().FirstOrDefault();
				}
				else
				{
					if (count > 0)
					{
						evotimes[index] = count - 1;
						if (count == 1) GetEvoMat(out evoMatCount, out evoMat);
						else
						{
							"@ui_pet_evo_fail2".Tips();
							return null;
						}
					}
					"@ui_pet_evo_fail".Tips();
				}
			}
			else
			{
				step += 10;
				Refresh();
				var value = SGame.Randoms.Random._R.Next(evoCfg.SpecialEvolutionValue(0), evoCfg.SpecialEvolutionValue(1));
				index = SGame.Randoms.Random._R.NextWeight(GetEffectRandomWeight());
				effectAdd[index] = effectAdd[index] + value;
				evo = evo | (1 << index);
				return effects[index];
			}
			return null;
		}

		public int GetEvoStep()
		{
			return step % 10;
		}

		public int GetEvoSuccessCount()
		{
			var s = GetEvoStep();
			if (s > 0)
			{
				var c = 0;
				for (int i = 1; i <= MAX_EVO; i++)
				{
					if (i.IsInState(s)) c++;
				}
				return c;
			}
			return 0;
		}

		public int GetFreeRebackItemCount()
		{
			if (type == 0)
				return evoCfg.ReleaseReward + GetEvoSuccessCount() * evoCfg.EvolutionReward + Math.Clamp(step / 10, 0, evoCfg.SpecialEvolutionTime) * evoCfg.SpecialEvolutionReward;
			return 0;
		}

		public void ResetNewFlag()
		{
			isnew = 0;
			evo = 0;
		}

		public void Clear()
		{
			cfg = default;
			eCfg = default;
			evoCfg = default;
			effectID = 0;
			effects?.Clear();
			effects = default;
			item = default;
		}

		public bool CanEvo(out int index, out int count)
		{
			index = -1;
			count = 0;
			if (type == 0 && step < 10)
			{
				if (evotimes == null && evoCfg.IsValid())
					evotimes = evoCfg.GetBasicEvolutionTimeArray();
				for (index = 0; index < evotimes.Length; index++)
				{
					count = evotimes[index];
					if (count > 0 || count == -1) return true;
				}
			}
			return false;
		}
	}


}