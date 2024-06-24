using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core.Record;
using GameConfigs;

namespace SGame
{

	public enum EffectIndex : int
	{
		Key = 0,
		Val,
		Max,
		RandomMin,
		RandomMax,
		Weight
	}

	partial class DataCenter
	{
		public int globalID = 1000;

		public static int GID { get { return s_instance == null ? 0 : Instance.globalID++; } }

		public PetData petData = new PetData();

		public static class PetUtil
		{


			static public float c_one_d_2_time;

			static private List<int> _eggs;

			static private PetData _data { get { return Instance.petData; } }

			static private EventManager _eMgr = EventManager.Instance;

			static public void Init()
			{
				c_one_d_2_time = GlobalDesginConfig.GetInt("hatch_diamond_time", 100);
				_eggs = ConfigSystem.Instance.Finds<ItemRowData>((c) => c.Type == ((int)EnumItemType.PetEgg)).Select(c => c.ItemId).ToList();
				_data.pets?.ForEach(p => p.Refresh());
				_data.egg?.Refresh();
				_data.pet = _data.pets?.Find(p => p.id == _data.petID);
				if (_data.pet != null) _data.pet.isselected = true;
				//UpdatePetBuff();
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

			static public PetItem CreatePet(int id)
			{
				if (id > 0)
				{
					if (ConfigSystem.Instance.TryGet<PetsRowData>(id, out var cfg))
					{
						return new PetItem() { cfgID = id, cfg = cfg, isnew = 1 ,level = 1}.Refresh();
					}
				}
				return default;
			}
			static public PetItem AddPet(PetItem pet, bool triggerevent = true, bool iseggborn = false)
			{

				if (pet != null && pet.cfgID > 0)
				{
					_data.pets.Add(pet);
					if (iseggborn) { _data.egg.Clear(); _data.egg = null; }
					_eMgr.Trigger(((int)GameEvent.PET_ADD), pet);
					if (_data.pet == null || !_data.pet.cfg.IsValid()) Follow(pet);
					if (triggerevent)
					{
						Resort();
						_eMgr.Trigger(((int)GameEvent.PET_LIST_REFRESH));
					}
				}
				return pet;
			}

			static public PetItem AddPet(int id, bool triggerevent = true, bool iseggborn = false)
			{
				if (id > 0)
				{
					return AddPet(CreatePet(id), triggerevent, iseggborn);
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

			static public void Follow(PetItem pet, bool refresh = true)
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

			static public void ClearNewFlag(int eggid = 0)
			{
				_data.pets?.ForEach(p => p?.ResetNewFlag());
				if (eggid != 0)
				{
					if (eggid < 0)
						_data.newegg?.Clear();
					else if (_data.newegg.Contains(eggid))
						_data.newegg.Remove(eggid);
				}
			}

			static public List<PetItem> GetPetsByCondition(Predicate<PetItem> condition = null, List<PetItem> rets = null, bool onlyone = false)
			{
				rets = rets ?? new List<PetItem>();
				if (condition != null)
				{
					for (int i = 0; i < _data.pets.Count; i++)
					{
						var p = _data.pets[i];
						if (condition(p))
						{
							rets.Add(p);
							if (onlyone) break;
						}
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
					c = b.level.CompareTo(a.level);
					if (c == 0)
					{
						c = -a.cfgID.CompareTo(b.cfgID);
						if (c == 0) c = -a.id.CompareTo(b.id);
					}
				}
				return c;
			}

			static public bool IsEgg(int id)
			{
				if(_eggs != null)
					return _eggs.Contains(id);
				return false;
			}

		}

	}

	[System.Serializable]
	public class PetData
	{

		public List<PetItem> pets = new List<PetItem>();
		public List<int> newegg = new List<int>();

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
		public int level;
		public int type;

		public ulong effectID;
		public int[] effectAdd = new int[9];

		public int isnew;
		public int uptime;
		public int time;

		public string name { get { return cfg.IsValid() ? cfg.Name : item.IsValid() ? item.Name : null; } }
		public string icon { get { return cfg.IsValid() ? cfg.Icon : item.IsValid() ? item.Icon : null; } }
		public int quality { get { return cfg.IsValid() ? cfg.Quality : eCfg.IsValid() ? eCfg.Quality : 1; } }

		public bool isselected { get; set; }

		public GameConfigs.PetsRowData cfg;
		public GameConfigs.ItemRowData item;
		public GameConfigs.EggRowData eCfg;

		[NonSerialized]
		public List<int[]> effects;//0£ºid,1:val,2:Max,3:randomMin,4:randomMax
		[NonSerialized]
		public double count;
		[NonSerialized]
		public int evo;
		[NonSerialized]
		public int tempQuality;

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
					if (this.effects == null)
					{
						this.effects = new List<int[]>();
						if (cfg.BuffsLength > 0)
						{
							for (int i = 0; i < cfg.BuffsLength; i++)
							{
								if (ConfigSystem.Instance.TryGet(cfg.Buffs(i), out PetBuffConfigRowData buff))
									this.effects.Add(new int[] { buff.Buff, buff.Default, buff.Most, buff.Range(0), buff.Range(1), cfg.WeightsLength > i ? cfg.Weights(i) : 100 });
							}
						}
					}

				}
			}
			else if (type == 1)
			{
				if (ConfigSystem.Instance.TryGet(cfgID, out item))
				{
					ConfigSystem.Instance.TryGet(item.TypeId, out eCfg);
					count = PropertyManager.Instance.GetItem(cfgID).num;
					isnew = DataCenter.Instance.petData.newegg.Contains(cfgID) ? 1 : 0;
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

		public PetItem Born()
		{
			if (eCfg.IsValid())
			{
				time = GameServerTime.Instance.serverTime + eCfg.Time;
				isselected = true;
			}
			return this;
		}

		public PetItem Evo(PetItem mat, out int index, out int add)
		{
			index = add = -1;
			if (mat != null && mat.cfg.IsValid())
			{
				index = SGame.Randoms.Random._R.NextWeight(GetEffectRandomWeight());
				if (index >= 0)
				{
					level++;
					isnew = 1;
					var effect = effects[index];
					add = SGame.Randoms.Random._R.Next(effect[((int)EffectIndex.RandomMin)], effect[((int)EffectIndex.RandomMax)]);
					add = Math.Clamp(add, 0, effect[2] - effect[1] - effectAdd[index]);
					effectAdd[index] += add;
					evo |= 1 << index;
				}
			}
			return this;
		}

		public PetItem Clone()
		{
			return new PetItem() { type = type, cfgID = cfgID, effectID = effectID }.Refresh();
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
					for (int i = 0; i < effects.Count; i++)
					{
						int[] buff = null;
						if (!useAdd)
							buff = effects[i];
						else
							buff = new int[] { effects[i][0], (int)Utils.ToInt(effects[i][1] * (100 + effectAdd[i]) * 0.01f) };
						rets.Add(buff);
					}
					return rets;
				}
			}
			return effects;
		}

		public int GetBuffVal(int index, bool useAdd = true)
		{
			if (index >= 0 && index < effects.Count)
			{
				var effect = effects[index][((int)EffectIndex.Val)];
				if (useAdd && effectAdd.Length > index) return effect + effectAdd[index];
				return effect;
			}
			return 0;
		}

		public int[] GetEffectRandomWeight()
		{
			var ws = new int[effects.Count];
			for (int i = 0; i < ws.Length; i++)
			{
				var max = effects[i][((int)EffectIndex.Max)];
				ws[i] = GetBuffVal(i, true) >= max ? 0 : effects[i][((int)EffectIndex.Weight)];
			}
			return ws;
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
			effectID = 0;
			effects?.Clear();
			effects = default;
			item = default;
		}

	}


}