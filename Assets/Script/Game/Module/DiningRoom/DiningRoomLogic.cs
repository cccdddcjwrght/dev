using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameConfigs;
using libx;
using UnityEngine;
using MapGrid = GameTools.Maps.Grid;

namespace SGame.Dining
{
	#region Class
	enum State
	{
		None,
		Loading,
		Loaded,
		Used,
		Close,
	}

	public interface IBuild : IRequest
	{
		public int cfgID { get; }
		public int objID { get; }
	}

	class Build : IBuild, IEquatable<Build>
	{
		protected IEnumerator _wait;

		public int cfgID { get; set; }

		public int objID { get; set; }

		public string error { get; set; }

		public virtual bool isDone { get; set; }

		public virtual void Close() { }

		public bool Equals(Build other)
		{
			return other != null && other.cfgID == cfgID && other.objID == objID;
		}

		public virtual IEnumerator Wait()
		{
			yield return _wait;
			_wait = null;
		}

	}

	class Seat : Build
	{
		public int index;
		public int near;
		public string tag;
	}

	class Part : Build
	{
		public int index;
		public Transform transform;
		public string asset;

		private State _state = State.None;
		private AssetRequest _req;


		public override bool isDone
		{
			get
			{
				Update();
				return _state == State.Used;
			}
			set => base.isDone = value;
		}


		public void Update()
		{
			switch (_state)
			{
				case State.None:
					_req = Load();
					break;
				case State.Loading:
					if (_req != null)
					{
						if (_req.isDone)
						{
							error = _req.error;
							_state = State.Loaded;
						}
					}
					else
					{
						_state = State.Loaded;
					}
					break;
				case State.Loaded:
					_state = State.Used;
					Process();
					break;
			}

		}

		public override void Close()
		{
			transform = null;
			asset = null;
			_state = State.None;
			_req?.Release();

		}

		private AssetRequest Load()
		{
			var req = default(AssetRequest);
			if (!transform.Find(objID.ToString()))
			{
				if (!string.IsNullOrEmpty(asset))
				{
					var path = asset;
					req = Assets.LoadAssetAsync(path, typeof(GameObject));
					if (req == null)
					{
						_state = State.Loaded;
						error = "Load Asset Fail" + path;
						return null;
					}
				}
			}
			_state = State.Loading;
			return req;
		}

		private void Process()
		{
			if (transform)
			{
				if (string.IsNullOrEmpty(error))
					GameObject.Instantiate(_req.asset as GameObject, transform);
				transform.gameObject.SetActive(true);
			}
		}

	}

	class Place : Build
	{
		public int index;
		public bool enable;
		public List<Part> parts;
		public List<Seat> seats;

		public bool waitActive;

		public Transform transform;

		public override bool isDone
		{
			get
			{
				if (!enable) return true;
				return parts == null || parts.All(p => p.isDone);
			}
			set => base.isDone = value;
		}

		public Place(int id) => cfgID = id;

		public void Enable(bool state)
		{
			waitActive = false;
			enable = state;
			EnableClick(state);
		}

		public void EnableClick(bool state)
		{
			if (transform)
				transform.GetComponent<BoxCollider>().enabled = state;
		}

		public override void Close()
		{
			parts?.Clear();
			seats?.Clear();
		}

		public override IEnumerator Wait()
		{
			while (!isDone) yield return null;

		}


	}

	class Region : Build
	{
		public EntityContainer gHandler = new SpawnContainer();

		public Worktable data;
		public List<Place> machines = new List<Place>();
		public Place next { get; private set; }
		public Place begin { get { return machines == null ? null : machines[0]; } }
		public bool enable { get { return begin?.enable == true; } set { if (begin != null) begin.enable = true; } }
		public override bool isDone { get => machines.All(m => m.isDone); set => base.isDone = value; }

		public Region(int id) => cfgID = id;

		public Place GetPlace(int id)
		{
			return machines.Find(m => m.cfgID == id);
		}

		public Place GetLockPlace()
		{
			return machines.Find(m => !m.enable);
		}

		public void SetNextUnlock(Place next)
		{
			this.next = next;
			next?.EnableClick(true);
		}

		public override void Close()
		{
			machines?.ForEach(m => m.Close());
			machines?.Clear();
			machines = null;
		}

	}

	class RegionHit : MonoBehaviour, ITouchOrHited
	{
		public int region;
		public int place;
		public Action<int, int> onClick;

		private void Awake()
		{
			var c = transform.gameObject.AddComponent<BoxCollider>();
			c.size = new Vector3(1, 1, 1);
		}

		public void OnClick()
		{
			onClick?.Invoke(region, place);
		}
	}


	#endregion

	class DiningRoomLogic : IBuild
	{
		#region Member

		private MapGrid _sceneGrid;
		private List<Region> _regions;
		private Region _begin;
		private EventHandleContainer _eHandlers;
		private LevelRowData _cfg;

		public string name;
		public MapGrid grid { get { return _sceneGrid; } }


		public int cfgID { get; private set; }
		public int objID { get; private set; }
		public string error { get; set; }
		public bool isDone
		{
			get
			{
				return _regions == null || _regions.All(b => b.isDone);
			}
		}

		#endregion

		public DiningRoomLogic(int id) => cfgID = id;

		#region Method

		public bool Init()
		{
			if (_sceneGrid == null)
			{
				InitView();
				InitBuilds();
				InitEvents();
				if (_sceneGrid != null)
				{
					if(ConstDefine.SCENE_WORK_TAG?.Count > 0)
					{
						ConstDefine.SCENE_WORK_TAG.All((s) => {
							if (_sceneGrid.tags.TryGetValue(s, out var ls))
								WorkQueueSystem.Instance.AddWorkers(s, true, ls.ToArray());
							return true;
						});
					}
				}
			}
			return true;
		}

		public IEnumerator Wait(bool isnew = false)
		{
			double time = 1.0;
			Init();
			while (!isDone) yield return null;
			while ((time -= GlobalTime.deltaTime) > 0)
				yield return null;
		}

		public Region GetRegion(int id)
		{
			return _regions.Find(r => r.cfgID == id);
		}

		public bool UpLevel(Region region)
		{
			switch (DataCenter.MachineUtil.CheckCanUpLevel(region.data))
			{
				case Error_Code.LV_MAX:
					Debug.Log($"{region.cfgID} Lv Max!!!");
					break;
				case Error_Code.ITEM_NOT_ENOUGH:
					Debug.Log($"{region.cfgID} 升级道具不足!!!");
					break;
				case 0:
					DataCenter.MachineUtil.UpdateLevel(region.cfgID, cfgID);
					Debug.Log($"{region.cfgID} : Lv -> {region.data.level}");
					return true;
			}
			return false;
		}

		public bool Unlock(Region region)
		{
			if (!region.enable)
				region.SetNextUnlock(region.begin);

			if (region.next != null)
			{
				switch (DataCenter.MachineUtil.CheckCanActiveMachine(region.next.cfgID))
				{
					case Error_Code.MACHINE_DEPENDS_NOT_ENABLE:
						Debug.Log("前置条件不满足，无法解锁");
						break;
					case Error_Code.ITEM_NOT_ENOUGH:
						Debug.Log("消耗道具不足");
						break;
					case 0:
						var id = region.next.cfgID;
						DataCenter.MachineUtil.AddMachine(id);
						return true;
				}
			}
			return false;
		}

		public void CheckUnlock(Region region)
		{
			if (region.next == null)
			{
				//不能自动解锁
				if (!region.enable && DataCenter.MachineUtil.CheckDontAutoActive(region.begin.cfgID)) return;
				if (region.data.isTable || (region.data.level <= 0 && !region.enable))
				{
					if (0 != DataCenter.MachineUtil.CheckCanActiveMachine(region.begin.cfgID, true))
						return;
				}
				else if (!DataCenter.MachineUtil.CheckCanAddMachine(region.cfgID, cfgID)) return;
				DoPreview(region);
			}
		}

		public void MarkWalkFlag(Place machine, int index = -1)
		{
			if (machine != null)
			{
				var c = _sceneGrid.GetCell(index >= 0 ? index : machine.index);
				if (c != null)
					c.Marking(GameTools.Maps.MaskFlag.UnWalkable, !machine.enable);
				else
					Debug.Log(machine.index);
			}
		}

		public void Close()
		{
			cfgID = 0;
			_regions?.ForEach(b => b.Close());
			_regions?.Clear();
			_sceneGrid = null;
			_eHandlers?.Close();
			_eHandlers = null;
		}

		#endregion

		#region Private

		private void InitView()
		{
			ConfigSystem.Instance.TryGet(cfgID, out _cfg);
			_sceneGrid = GameObject.FindAnyObjectByType<MapGrid>(FindObjectsInactive.Include);
			_sceneGrid?.Refresh();
		}

		private void InitBuilds()
		{
			if (_sceneGrid != null)
			{
				_regions = new List<Region>();
				if (ConfigSystem.Instance.TryGets<RoomMachineRowData>(c => c.Scene == cfgID, out var list))
				{
					_regions = list.Where(c => c.Type >= 0).GroupBy(c => c.Machine).Select(c =>
					{
						var row = new Region(c.Key);
						row.data = DataCenter.MachineUtil.GetWorktable(c.Key, cfgID, true);
						row.machines = c.Select(
							m => ActiveBuild(machine: new Place(m.ID)
							{
								parts = CreateParts(m, out var i, out var t, out var s, row),
								index = i,
								seats = s,
								transform = t,
							},
							state: DataCenter.MachineUtil.IsActived(m.ID), region: c.Key)
						).ToList();
						return row;
					}).ToList();
					if (_cfg.IsValid())
						_begin = GetRegion(_cfg.FirstOrder);
					else
						_begin = _regions[0];
					OnWorkMachineEnable(0, 0);
				}
			}
		}

		private void InitEvents()
		{
			_eHandlers = new EventHandleContainer();
			_eHandlers += EventManager.Instance.Reg<int, int>(((int)GameEvent.WORK_TABLE_UPLEVEL), OnWorkTableUplevel);
			_eHandlers += EventManager.Instance.Reg<int, int>(((int)GameEvent.WORK_TABLE_MACHINE_ENABLE), OnWorkMachineEnable);
			_eHandlers += EventManager.Instance.Reg<int>(((int)GameEvent.TECH_ADD_TABLE), OnTechAddWorktable);
			_eHandlers += EventManager.Instance.Reg<int>(((int)GameEvent.ORDER), OnAddOrder);
		}

		private Place ActiveBuild(Place machine = default, int id = -1, bool state = true, int region = 0)
		{
			if (id > 0 || machine != null)
			{
				machine = machine ?? GetMachine(id);
				if (machine != null)
				{
					machine.Enable(state);
					MarkWalkFlag(machine);
					AddWorkers(region, machine);
					return machine;
				}
			}
			return default;
		}

		private Place GetMachine(int id)
		{
			if (id > 0)
			{
				for (int i = 0; i < _regions.Count; i++)
				{
					for (int j = 0; j < _regions[i].machines.Count; j++)
					{
						if (_regions[i].machines[j].cfgID == id)
							return _regions[i].machines[j];
					}
				}
			}
			return default;
		}

		private List<Part> CreateParts(RoomMachineRowData cfg, out int gindex, out Transform transform, out List<Seat> seats, Region region = null)
		{
			gindex = -1;
			transform = null;
			seats = new List<Seat>();
			var placeid = cfg.ID;
			var builds = new List<int>();
			var ls = default(List<Part>);
			var cs = new HashSet<int>();
			var ids = cfg.GetObjIdArray();
			var tags = cfg.GetArray(cfg.Tags, cfg.TagsLength);

			void AddBuilds(ref Transform trans, GameTools.Maps.Cell cell)
			{
				if (cell == null) return;
				cs.Add(cell.index);
				trans = trans ?? cell.cell?.transform;
				if (cell.builds?.Count > 0) builds.AddRange(cell.builds.Select(b => int.Parse(b)));
			}

			if (ids != null && ids.Length > 0)
			{
				if (ids[0] == 0)
				{
					for (int i = 1; i < ids.Length - 1; i += 2)
						AddBuilds(ref transform, grid.GetCell(ids[i], ids[i + 1]));
				}
				else builds.AddRange(ids);

			}

			if (tags != null && tags.Length > 0)
			{
				foreach (var item in tags)
				{
					if (grid.tags.TryGetValue(item, out var v) && v.Count > 0)
					{
						foreach (var idx in v)
							AddBuilds(ref transform, grid.GetCell(idx));
					}
				}
			}

			if (builds.Count > 0)
			{
				ls = builds.Select(o => new Part()
				{
					objID = o,
					index = _sceneGrid.buildIndexs[o],
					asset = _sceneGrid.GetBuildAsset(o),
					transform = _sceneGrid.GetBuildObject(o)?.transform,
				}).ToList();

			}

			if (cfg.LinkTagsLength > 0)
			{
				for (int i = 0; i < cfg.LinkTagsLength; i++)
				{
					foreach (var item in cs)
					{
						var t = cfg.LinkTags(i);
						var ps = grid.GetNearTagAllPos(item, t);
						if (ps?.Count > 0)
						{
							var ss = seats;
							ps.ForEach(p => ss.Add(new Seat()
							{
								index = grid.PosToIndex(p.x, p.y),
								tag = t,
								near = item
							}));
						}
					}
				}
			}

			if (cs.Count > 0)
			{
				gindex = cs.First();
				transform = transform ?? ls[0].transform?.parent;
				var h = transform.gameObject.AddComponent<RegionHit>();
				h.region = region.cfgID;
				h.place = placeid;
				h.onClick = OnRegionClick;

				foreach (var item in cs)
					AddPlaceHit(item, region.cfgID, cfg.ID);

			}

			return ls;

		}

		private void AddWorkers(int region, Place place)
		{
			if (place.enable)
			{
				var idxs = new List<int>();
				if (place.parts?.Count > 0)
				{
					for (int i = 0; i < place.parts.Count; i++)
					{
						var part = place.parts[i];
						if (idxs.Contains(part.index)) continue;
						idxs.Add(part.index);
						MarkWalkFlag(place, part.index);
					}
				}

				if (idxs.Count > 0)
				{
					var m = DataCenter.MachineUtil.GetMachine(place.cfgID, out var worktable);

					for (int i = 0; i < idxs.Count; i++)
					{
						var cell = grid.GetCell(idxs[i]);
						if (cell != null)
						{
							switch ((TABLE_TYPE)m.cfg.Type)
							{
								case TABLE_TYPE.CUSTOM:
									if (!grid.GetNearTagPos(cell.x, cell.y, ConstDefine.TAG_SERVE, out var order))
									{
										var servr = place.seats?.Find(seat => seat.tag == ConstDefine.TAG_SERVE);
										if (servr != null) order = grid.GetCell(servr.index).ToGrid();
									}
									TableFactory.CreateCustomer(cell.ToGrid(), order, grid.GetNearTagAllPos(cell.index, ConstDefine.TAG_SEAT));
									break;
								case TABLE_TYPE.DISH:
									if (!grid.GetNearTagPos(cell.x, cell.y, ConstDefine.TAG_TAKE_SERVE, out order))
									{
										var servr = place.seats?.Find(seat => seat.tag == ConstDefine.TAG_TAKE_SERVE);
										if (servr != null) order = grid.GetCell(servr.index).ToGrid();
									}
									TableFactory.CreateDish(cell.ToGrid(), grid.GetNearTagPos(cell.x, cell.y, ConstDefine.TAG_TAKE), order);
									break;
								default:
									if (m.cfg.Nowork != 1)
										TableFactory.CreateFood(cell.ToGrid(), worktable.id, worktable.item, grid.GetNearTagPos(cell.x, cell.y, ConstDefine.TAG_MACHINE_WORK));
									break;
							}
						}
					}
				}

			}
		}

		private void AddPlaceHit(int cell, int region, int placeid)
		{
			var c = grid.GetCell(cell);
			if (c != null)
			{
				var g = c.GetBuildLayer();
				if (!g) return;
				var h = g.gameObject.AddComponent<RegionHit>();
				h.region = region;
				h.place = placeid;
				h.onClick = OnRegionClick;
			}
		}

		private void OnRegionClick(int region, int place)
		{
			var r = GetRegion(region);
			if (r != null)
			{
				EventManager.Instance.Trigger(((int)GameEvent.WORK_TABLE_CLICK), r);
				if (!r.enable || r.next?.cfgID == place)
				{
					if ((r.next ?? r.begin).waitActive == true)
					{
						if (!r.data.isTable && r.begin.waitActive)
							EventManager.Instance.Trigger(((int)GameEvent.WORK_TABLE_CLICK), r, 1);
						else
							DataCenter.MachineUtil.AddMachine(r.next.cfgID);
					}
				}
				else if (r.next == null || r.next.cfgID != place)
				{
					//UpLevel(r);
					if (!r.data.isTable)
						EventManager.Instance.Trigger(((int)GameEvent.WORK_TABLE_CLICK), r, 2);
				}
			}
		}

		private Place DoPreview(Region region)
		{
			if (region != null)
			{
				var place = region.GetLockPlace();
				if (place != null)
				{
					place.waitActive = true;
					region.SetNextUnlock(place);
					region.gHandler += SpawnSystem.Instance.Spawn("Assets/BuildAsset/Prefabs/Scenes/other/rewardbox.prefab", place.transform.gameObject);
					return place;
				}
			}
			return default;

		}

		private void DoUnlock(Region region, int place)
		{
			if (region != null)
			{
				region.gHandler?.DestroyAllEntity();
				region.SetNextUnlock(null);
				ActiveBuild(region.GetPlace(place), region: region.cfgID)?.Wait()?.Start();
			}
		}

		#endregion

		#region Events


		private void OnWorkTableUplevel(int id, int level)
		{
			var r = GetRegion(id);
			if (r != null)
				CheckUnlock(r);
		}

		private void OnWorkMachineEnable(int region, int id)
		{
			if (id > 0)
				DoUnlock(GetRegion(region), id);
			_regions.ForEach(CheckUnlock);
		}

		private void OnTechAddWorktable(int id)
		{
			if (id < 0)
				Unlock(GetRegion(-id));
			else
				DoPreview(GetRegion(id));
		}

		private void OnAddOrder(int id)
		{
			if (!_begin.enable)
				DoPreview(_begin);
		}

		#endregion

	}

}
