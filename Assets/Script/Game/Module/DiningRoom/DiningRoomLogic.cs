using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GameConfigs;
using GameTools.Maps;
using libx;
using log4net;
using plyLib;
using Unity.Entities.UniversalDelegates;
using Unity.Mathematics;
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

		public Transform holder;

		public int cfgID { get; set; }

		public int objID { get; set; }

		public string error { get; set; }

		public virtual bool isDone { get; set; }

		public virtual void Close() { }

		public bool Equals(Build other)
		{
			return other != null && other.cfgID == cfgID && other.objID == objID;
		}

		public virtual IEnumerator Wait(float waittime = 0)
		{
			yield return _wait;
			if (waittime > 0)
				yield return new WaitForSeconds(waittime);
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
		public bool forceLoad;


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
					if (transform != null && (forceLoad || transform.childCount == 0))
						_req = Load();
					else
						_state = State.Loaded;
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

		public bool IsNeedLoad()
		{
			return transform != null && (forceLoad || transform.childCount == 0 || !transform.GetChild(0).gameObject.activeSelf);
		}

		public Part SetLoad(string asset = null)
		{
			forceLoad = true;
			if (asset != null) this.asset = asset;
			if (transform != null && transform.childCount > 0) transform.DestroyImmediateAllChildren();
			return this;
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
				if (string.IsNullOrEmpty(error) && _req != null && _req.asset)
					GameObject.Instantiate(_req.asset as GameObject, transform);
				transform.gameObject.SetActive(true);
				if (transform.childCount > 0)
					transform.GetChild(0).gameObject?.SetActive(true);
			}
		}

	}

	class Place : Build
	{
		public int index;
		public bool enable;
		public bool isBegin;
		public List<Part> parts;
		public List<Seat> seats;

		public Queue<Seat> lockSeats;

		public bool waitActive;
		public string asset;

		public int area;
		public Transform transform;

		public override bool isDone
		{
			get
			{
				if (!enable || parts == null) return true;
				var val = parts.All(p => p.isDone);
				return val;
			}
			set => base.isDone = value;
		}

		public bool NeedLoadAsset()
		{
			if (parts?.Count > 0)
				return parts.Any(p => p.IsNeedLoad());
			return false;
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
			{
				var c = transform?.GetComponent<BoxCollider>();
				if (c)
					c.enabled = state;
			}
		}

		public override void Close()
		{
			parts?.Clear();
			seats?.Clear();
		}

		public override IEnumerator Wait(float waittime = 0)
		{
			while (!isDone) yield return null;
			if (waittime > 0) yield return new WaitForSeconds(waittime);
		}

		public Place SetFoodTex(object texture)
		{
			if (isBegin && parts?.Count > 0 && texture != null)
			{
				Texture2D tex = default;
				if (texture is Texture2D)
					tex = texture as Texture2D;
				else if (texture is string asset)
					tex = Assets.LoadAsset("Assets/BuildAsset/Textures/Foods/" + asset, typeof(Texture2D))?.asset as Texture2D;
				if (tex)
				{
					var renderer = parts[0].transform.GetComponentInChildren<Renderer>(true);
					if (renderer)
						renderer.material.mainTexture = tex;
				}
			}
			return this;
		}

		public Place SetBegin(Region region)
		{
			isBegin = true;
			if (transform != null)
				transform.tag = ConstDefine.C_WORKER_TABLE_GO_TAG;
			return this;
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

		public int area { get { return begin == null ? 0 : begin.area; } }

		public List<Transform> transforms;

		public Region(int id) => cfgID = id;

		public Place GetPlace(int id)
		{
			return machines.Find(m => m.cfgID == id);
		}

		public Place GetLockPlace()
		{
			return machines.Find(m => !m.enable);
		}

		public Region RefreshFood()
		{
			if (!data.isTable && begin != null && begin.transform)
				begin.SetFoodTex(data.GetFoodAsset());
			return this;
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

	class Area : Build
	{
		public RoomAreaRowData cfg;
		public bool enable;

		public GameObject lockGo;
		public GameObject unlockGo;

		public float duration = 4f;
		public float delay = 2.5f;

		private GameObject _lockBody;
		private GameObject _effect;

		public Area Refresh(Vector3 pos)
		{

			_lockBody = lockGo.transform.Find("GameObject")?.gameObject;
			_effect = lockGo.transform.Find("__unlock")?.gameObject;

			holder.tag = "area";
			holder.position = pos;

			var region = lockGo.AddComponent<RegionHit>();
			region.region = cfg.ID;
			region.place = -1;
			region.SetCollider(default);

			return this;
		}

		public void PlayUnlock(Action call = null)
		{
			if (_effect == null || _lockBody == null)
			{
				lockGo.SetActive(false);
				unlockGo.SetActive(true);
				call?.Invoke();
			}
			else
			{
				Anim(call).Start();
			}
		}

		private IEnumerator Anim(Action call = null)
		{
			UILockManager.Instance.Require("dining");
			SceneCameraSystem.Instance.disableControl = true;
			SceneCameraSystem.Instance.Focus(holder.gameObject, false, 11);
			_effect.SetActive(true);
			if (delay > 0) yield return new WaitForSeconds(delay);
			_lockBody.SetActive(false);
			unlockGo.SetActive(true);
			call?.Invoke();
			if (duration > 0)
			{
				yield return new WaitForSeconds(duration - delay);
				_effect.SetActive(false);
				SceneCameraSystem.Instance.disableControl = false;
				UILockManager.Instance.Release("dining");
			}

		}

	}

	class RegionHit : MonoBehaviour, ITouchOrHited
	{
		private List<string> C_IGNORE_UI = new List<string>() {
			"gmui","mask","guidefinger","guideui",
			"scenedecorui","hud","flight","SystemTip",
			"guideback","fingerui"
		};

		public int region;
		public int place;
		public Action<int, int> onClick;

		public BoxCollider collider;

		public Animation animation;

		static private Vector3 g_size;
		static private Vector3 g_center;

		private bool _hasGet;

		private void Awake()
		{
			if (g_size == Vector3.zero)
			{
				var ss = GlobalDesginConfig.GetStr("region_size")?.Split('|');
				if (ss != null && ss.Length >= 3)
					g_size = new Vector3(float.Parse(ss[0]), float.Parse(ss[1]), float.Parse(ss[2]));
				else
					g_size = new Vector3(0.9f, 0.5f, 0.9f);

				ss = GlobalDesginConfig.GetStr("region_center")?.Split('|');
				if (ss != null && ss.Length >= 3)
					g_center = new Vector3(float.Parse(ss[0]), float.Parse(ss[1]), float.Parse(ss[2]));
				else
					g_center = new Vector3(0, 0.22f, 0);
			}

			var o = transform.gameObject.GetComponentInChildren<BoxCollider>();
			var c = collider = transform.gameObject.AddComponent<BoxCollider>();
			c.size = o ? o.size : g_size;
			c.center = o ? o.center : g_center;

		}

		public void SetCollider(Bounds bounds)
		{

			if (collider)
			{
				collider.center = bounds.center;
				collider.size = bounds.size;
			}

		}

		public void OnClick()
		{
			if (UIUtils.CheckIsOnlyMainUI(C_IGNORE_UI))
				onClick?.Invoke(region, place);
		}


		private string _clipName;
		public void Play(string name)
		{

			if (!_hasGet && animation == null)
			{
				_hasGet = true;
				animation = GetComponentInChildren<Animation>(true);
			}

			if (animation)
			{
				if (_clipName == name && animation.isPlaying) return;
				_clipName = name;
				if (name == null) animation.Stop();
				else animation.Play(name);
			}
		}

	}


	#endregion

	class DiningRoomLogic : IBuild
	{
		#region Member

		private static ILog log = LogManager.GetLogger("zcf.scene.worktable");


		private const string c_def_asset = "assets/buildasset/prefabs/scenes/other/rewardbox.prefab";

		private MapGrid _sceneGrid;
		private List<Region> _regions;
		private List<Area> _areas;
		private Region _begin;
		private EventHandleContainer _eHandlers;
		private LevelRowData _cfg;
		private bool _isInited;

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
				_isInited = false;
				InitView();
				InitBuilds();
				InitArea();
				InitEvents();
				if (_sceneGrid != null)
				{
					if (ConstDefine.SCENE_WORK_TAG?.Count > 0)
					{
						ConstDefine.SCENE_WORK_TAG.All((s) =>
						{
							if (_sceneGrid.tags.TryGetValue(s, out var ls))
								WorkQueueSystem.Instance.AddWorkers(s, true, ls.ToArray());
							return true;
						});
					}
				}

				if (ConfigSystem.Instance.TryGet<GameConfigs.SceneRowData>(name, out var scene))
					SceneCameraSystem.Reload(scene.CameraCtr);
				_isInited = true;
			}
			return true;
		}

		public IEnumerator Wait(bool isnew = false)
		{
			double time = 0.1;
			Init();
			while (!isDone) yield return null;
			yield return SceneCameraSystem.WaitInited();
			while ((time -= GlobalTime.deltaTime) > 0) yield return null;
			_regions.ForEach(r => r.RefreshFood());
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
					case Error_Code.MACHINE_DEPENDS_LEVEL_ERROR:
						Debug.Log("依赖工作台等级不足");
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
			CheckUnlockAndAuto(region);
		}

		public void CheckUnlockAndAuto(Region region, bool checkauto = false)
		{
			if (region != null && region.next == null)
			{
				var mid = region.begin.cfgID;
				if (!DataCenter.MachineUtil.IsAreaEnableByMachine(mid)) return;
				ConfigSystem.Instance.TryGet<RoomMachineRowData>(mid, out var m);
				//不能自动解锁
				if (!region.enable && DataCenter.MachineUtil.CheckDontAutoActive(mid)) return;
				if (region.data.isTable || (region.data.level <= 1 && !region.enable))
				{
					if (0 != DataCenter.MachineUtil.CheckCanActiveMachine(mid, true))
						return;
				}
				else if (!DataCenter.MachineUtil.CheckCanAddMachine(region.cfgID, cfgID)) return;
				if (m.Enable == 1)
				{
					if (checkauto && !region.enable)
						DoUnlock(region, mid);
				}
				DoPreview(region, false);
			}
		}

		public void MarkWalkFlag(Place machine, int index = -1)
		{
			if (machine != null)
			{
				if (ConfigSystem.Instance.TryGet<RoomMachineRowData>(machine.cfgID, out var cfg))
				{

					var c = _sceneGrid.GetCell(index >= 0 ? index : machine.index);
					if (c != null)
					{
						if (cfg.Walkable == 0)
							c.Marking(GameTools.Maps.MaskFlag.UnWalkable, !machine.enable);
						else
							c.walkcost = !machine.enable ? 1 : cfg.Walkable;
					}
					else
						Debug.Log(machine.index);
				}
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
								asset = m.TipsAsset,
								area = m.RoomArea
							},
							state: DataCenter.MachineUtil.IsActived(m.ID) && DataCenter.MachineUtil.IsAreaEnable(m.RoomArea), region: row)
						).ToList();

						row.begin?.SetBegin(row);

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
			_eHandlers += EventManager.Instance.Reg<int2>(((int)GameEvent.WORK_COOK_START), OnWorktablekCook);
			_eHandlers += EventManager.Instance.Reg<int2>(((int)GameEvent.WORK_COOK_COMPLETE), OnWorktablekCookComplete);
			_eHandlers += EventManager.Instance.Reg<int, int>(((int)GameEvent.WORK_TABLE_UP_STAR), OnWorkTableUpStar);
			_eHandlers += EventManager.Instance.Reg<int, int>(((int)GameEvent.WORK_TABLE_CLICK_SIMULATE), OnRegionClick);
			_eHandlers += EventManager.Instance.Reg<int>(((int)GameEvent.WORK_AREA_UNLOCK), OnWorkAreaUnlock);



		}

		private void InitArea()
		{
			var room = DataCenter.Instance.roomData.current;
			StaticDefine.CUSTOMER_TAG_BORN.Clear();
			StaticDefine.CUSTOMER_TAG_BORN.Add(ConstDefine.TAG_BORN_CUSTOMER);
			if (room != null && room.roomAreas?.Count > 0)
			{
				RefreshAreaBuildState();

				var layer = GameObject.Find("RoomArea")?.transform;
				if (layer)
				{
					_areas = new List<Area>();
					var delay = GlobalDesginConfig.GetFloat("area_unlock_effect_time", 2f);
					foreach (var item in room.roomAreas)
					{
						var islock = !room.areas.Contains(item.Key);
						var lockgo = layer.Find(item.Value.LockRes)?.gameObject;
						var unlockgo = layer.Find(item.Value.UnlockRes)?.gameObject;

						if (!islock)
						{
							if (!string.IsNullOrEmpty(item.Value.CustomerBorn))
								StaticDefine.CUSTOMER_TAG_BORN.Add(item.Value.CustomerBorn);
						}

						if (unlockgo) unlockgo.SetActive(!islock);
						if (lockgo) lockgo.SetActive(islock);
						if (islock)
						{
							var reg = new Area()
							{
								cfg = item.Value,
								cfgID = item.Key,
								delay = delay,
								holder = new GameObject("area_" + item.Key).transform,
								lockGo = lockgo,
								unlockGo = unlockgo
							}.Refresh(_sceneGrid.GetCellPosition(item.Value.AreaPos(0), item.Value.AreaPos(1)));
							_areas.Add(reg);
						}
					}
				}
			}
		}

		private Place ActiveBuild(Place machine = default, int id = -1, bool state = true, Region region = default)
		{
			if (id > 0 || machine != null)
			{
				machine = machine ?? GetMachine(id);
				if (machine != null)
				{
					machine.Enable(state);
					MarkWalkFlag(machine);
					AddWorkers(region, machine);
					PlayClip(machine.index, "idle");
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
			var ts = new HashSet<Transform>();

			void AddBuilds(ref Transform trans, GameTools.Maps.Cell cell)
			{
				if (cell == null) return;
				cs.Add(cell.index);
				if (cell.cell)
				{
					ts.Add(cell.cell.transform);
					if (trans == null)
						trans = cell.cell.transform;
				}
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
			region.transforms = ts.ToList();
			if (cs.Count > 0)
			{
				gindex = cs.First();
				transform = transform ?? ls?.Count > 0 ? ls[0].transform?.parent : default;

				if (transform == null)
					log.Error($"点位->{cfg.ID} : 当前场景没有找到相关格子");
				if (transform)
				{
					var h = transform.gameObject.AddComponent<RegionHit>();
					h.region = region.cfgID;
					h.place = placeid;
					h.onClick = OnRegionClick;
				}
				if (cs.Count > 1)
				{
					foreach (var item in cs)
						AddPlaceHit(item, region.cfgID, cfg.ID);
				}
			}

			return ls;

		}

		private void AddWorkers(Region region, Place place)
		{
			if (place.enable)
			{
				var isobj = region.data.objCfg.IsValid();
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

					if (isobj)
					{
						if (place.lockSeats == null)
							place.lockSeats = new Queue<Seat>(place.seats.FindAll(s => s.tag == ConstDefine.TAG_SEAT));
						if (place.lockSeats.Count > 0)
						{
							var count = worktable.addMachine;
							if (!_isInited)
								count = worktable.objLvCfg.SetNum;
							if (count > 0)
							{
								if (count > place.lockSeats.Count)
									Debug.LogError($"桌子 {place.cfgID} 升星添加的座位数量{worktable.objLvCfg.CustomerNum}大于实际座位");

								for (int j = 0; j < count && place.lockSeats.Count > 0; j++)
								{
									var seat = place.lockSeats.Dequeue();
									var cell = grid.GetCell(seat.near);
									var sCell = grid.GetCell(seat.index);

									if (!grid.GetNearTagPos(cell.x, cell.y, ConstDefine.TAG_SERVE, out var order))
									{
										var servr = place.seats?.Find(seat => seat.tag == ConstDefine.TAG_SERVE);
										if (servr != null) order = grid.GetCell(servr.index).ToGrid();
									}
									TableFactory.CreateCustomer(cell.ToGrid(), order, place.area, new List<Vector2Int>() { sCell.ToGrid() });
								}
							}
						}
						return;
					}

					for (int i = 0; i < idxs.Count; i++)
					{
						var cell = grid.GetCell(idxs[i]);
						if (cell != null)
						{
							var type = (EnumMachineType)(m.cfg.Type == 0 ? 3 : m.cfg.Type);
							switch (type)
							{
								case EnumMachineType.CUSTOM:
									if (!grid.GetNearTagPos(cell.x, cell.y, ConstDefine.TAG_SERVE, out var order))
									{
										var servr = place.seats?.Find(seat => seat.tag == ConstDefine.TAG_SERVE);
										if (servr != null) order = grid.GetCell(servr.index).ToGrid();
									}
									var seats = grid.GetNearTagAllPos(cell.index, ConstDefine.TAG_SEAT);
									TableFactory.CreateCustomer(cell.ToGrid(), order, m.cfg.RoomArea, seats);
									break;
								case EnumMachineType.DISH:
									if (!grid.GetNearTagPos(cell.x, cell.y, ConstDefine.TAG_TAKE_SERVE, out order))
									{
										var servr = place.seats?.Find(seat => seat.tag == ConstDefine.TAG_TAKE_SERVE);
										if (servr != null) order = grid.GetCell(servr.index).ToGrid();
									}
									TableFactory.CreateDish(cell.ToGrid(), grid.GetNearTagPos(cell.x, cell.y, ConstDefine.TAG_TAKE), order);
									break;
								case EnumMachineType.MACHINE:
									if (m.cfg.Nowork != 1)
										TableFactory.CreateFood(cell.ToGrid(), worktable.id, worktable.item, m.cfg.RoomArea, grid.GetNearTagPos(cell.x, cell.y, ConstDefine.TAG_MACHINE_WORK));
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
			if (place > 0)
			{
				var r = GetRegion(region);
				if (r != null)
				{
					EventManager.Instance.Trigger((int)GameEvent.WORK_REGION_CLICK);
					var p = r.GetPlace(place);

					if ((!r.enable && r.begin.cfgID == place) || r.next?.cfgID == place)
					{
						if ((r.next ?? r.begin).waitActive == true)
						{
							if (r.begin.waitActive && r.data.type > 3)
							{
								switch (r.data.type)
								{
									case 5:
										UIUtils.OpenUI("unlocktable", r.data, r.begin.cfgID);
										break;
									default:
										EventManager.Instance.Trigger<Build, int>(((int)GameEvent.WORK_TABLE_CLICK), r, 1);
										break;
								}
							}
							else if (!r.data.isTable && r.begin.waitActive)
								EventManager.Instance.Trigger<Build, int>(((int)GameEvent.WORK_TABLE_CLICK), r, 1);
							else
							{
								ClickAddMachine(r, r.next.cfgID);
								return;
							}
						}
					}
					else if (r.next == null || r.next.cfgID != place)
					{
						if (p.enable)
						{
							if (!r.data.isTable)
								EventManager.Instance.Trigger<Build, int>(((int)GameEvent.WORK_TABLE_CLICK), r, 2);
							else if (r.data.objCfg.IsValid() && r.data.CanUpLv())
							{
								switch (r.data.type)
								{
									case 4:
										UIUtils.OpenUI("getworker", r.data);
										break;
									case 5:
										UIUtils.OpenUI("unlocktable", r.data, r.begin.cfgID);
										break;
									default:
										EventManager.Instance.Trigger<Build, int>(((int)GameEvent.WORK_TABLE_CLICK), r, 4);
										break;
								}

							}
						}
					}
					r.machines.ForEach(p => PlayClip(p.index, "click", false));

				}
			}
		}

		private Place DoPreview(Region region, bool autoactive = false)
		{
			if (region != null)
			{
				var place = region.GetLockPlace();
				if (place != null)
				{
					place.waitActive = true;
					region.SetNextUnlock(place);

					if (!autoactive)
						autoactive = ConfigSystem.Instance.TryGet(place.cfgID, out RoomMachineRowData cfg) && cfg.ActiveBox == 0;

					if (!autoactive)
						region.gHandler += SpawnSystem.Instance.Spawn(string.IsNullOrEmpty(place.asset) ? c_def_asset : place.asset, place.transform.gameObject, name: "scene_grid");
					else
						ClickAddMachine(region, place.cfgID);
					return place;
				}
			}
			return default;

		}

		private void DoUnlock(Region region, int place)
		{
			if (region != null)
			{
				var p = region.GetPlace(place);
				var needload = p.NeedLoadAsset();
				region.gHandler?.DestroyAllEntity();
				region.SetNextUnlock(null);
				ActiveBuild(p, region: region)?.Wait(0.15f)?.Wait(e =>
				{
					if (region.data.type == 0)
						p.SetFoodTex(region.data.GetFoodAsset());
					PlayClip(p.index, "appear", false);
				});
				if (needload)
				{
					3.ToAudioID().PlayAudio();
					if (ConfigSystem.Instance.TryGet<RoomMachineRowData>(p.cfgID, out var c) && c.ActiveEffect == 1)
						EffectSystem.Instance.AddEffect(1, p.transform.gameObject);
				}
			}
		}

		private void PlayClip(int pos, string name, bool loop = true, bool errorStop = false)
		{

			var cell = _sceneGrid.GetCell(pos);
			if (cell != null)
			{
				var a = cell.GetAnimation();
				if (a)
				{
					if (name != "idle" && a.IsPlaying("cook")) return;//cook动画不被打断
					var clip = a.GetClip(name);
					if (clip != null)
					{
						a.Stop();
						var state = a.PlayQueued(name);
						if (state != null)
							state.wrapMode = loop ? WrapMode.Loop : WrapMode.Once;
					}
					else
					{
						a.Play(a.clip.name);
						a.Stop();
					}
				}
			}
		}

		private void PlayClip(int2 pos, string name, bool loop = true)
		{
			PlayClip(_sceneGrid.PosToIndex(pos.x, pos.y), name, loop);
		}

		private void RefreshAreaBuildState(int area = -1)
		{
			foreach (var item in _regions)
			{
				if (area == -1 || area == item.area)
				{
					var f = DataCenter.MachineUtil.IsAreaEnable(item.area);
					item.transforms.ForEach(t => t.gameObject.SetActive(f));
				}
			}
		}

		private void ClickAddMachine(Region region, int place)
		{
			DataCenter.MachineUtil.AddMachine(place);
			EventManager.Instance.Trigger((int)GameEvent.RECORD_PROGRESS, (int)RecordDataEnum.BOX, 1);
		}

		public static Vector3 GetCenter(GameObject gameObject, out Bounds bounds)
		{
			bounds = default;
			if (gameObject)
			{
				var rs = gameObject.GetComponentsInChildren<Renderer>().Where(r => r is MeshRenderer || r is SkinnedMeshRenderer).ToArray();
				if (rs != null && rs.Length > 0)
				{
					Bounds bs = default;
					for (int i = 0; i < rs.Length; i++)
					{
						if (i == 0) bs = rs[i].bounds;
						else bs.Encapsulate(rs[i].bounds);
					}
					var c = bs.center;
					c.y = 2;
					bounds = bs;
					return c;
				}
			}
			return Vector3.zero;
		}

		#endregion

		#region Events


		private void OnWorkTableUplevel(int id, int level)
		{
			var r = GetRegion(id);
			if (r != null)
			{
				if (r.data.objCfg.IsValid() && r.data.addMachine > 0)
					DoUnlock(r, r.begin.cfgID);
				if (r.data.level > 1 && r.data.addProfit == 0)
					7.ToAudioID().PlayAudio();

				_regions.ForEach(CheckUnlock);

			}

		}

		private void OnWorkTableUpStar(int id, int star)
		{
			6.ToAudioID().PlayAudio();
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

		private void OnWorktablekCook(int2 pos)
		{
			PlayClip(pos, "cook");
		}

		private void OnWorktablekCookComplete(int2 pos)
		{
			PlayClip(pos, "idle");
		}

		private void OnWorkAreaUnlock(int area)
		{
			if (area > 0)
			{
				var a = _areas.Find(a => a.cfgID == area);
				if (a != null)
				{
					IEnumerator Run()
					{
						yield return null;
						_regions.ForEach(r => CheckUnlockAndAuto(r, true));
						EventManager.Instance.Trigger(((int)GameEvent.WORK_AREA_UNLOCK), -1);
					}

					a.PlayUnlock(() =>
					{
						RefreshAreaBuildState(area);
						Run().Start();
					});

				}
			}
		}


		#endregion

	}

}
