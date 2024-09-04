﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FairyGUI;
using SGame.UI;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace SGame
{
	public enum RedAlignType
	{
		Center = 0,
		TR,
		R,
		BR,
		B,
		BL,
		L,
		TL,
		T
	}

	public enum RedpointType
	{
		/// <summary>
		/// 节点红点
		/// </summary>
		Node = 0,
		/// <summary>
		/// 叶子红点
		/// </summary>
		Leaf = 1,
		/// <summary>
		/// 常驻
		/// </summary>
		Permanent = 2,
		/// <summary>
		/// 每日
		/// </summary>
		EveryDay = 3,
		/// <summary>
		/// 登录
		/// </summary>
		Login = 4,
		/// <summary>
		/// 集合
		/// </summary>
		Group = 5,
		/// <summary>
		/// 一次点击
		/// </summary>
		OnceClick = 6,
		/// <summary>
		/// 引导红点，游戏周期只引导一次
		/// </summary>
		Guide = 7,
		/// <summary>
		/// 事件
		/// </summary>
		Event,
		Contidion = 101,
	}

	public struct Redpoint : IComponentData
	{
		public int id;
		public int time;
		public byte status;
	}
	public struct RedCheck : IComponentData { }
	public struct RedPause : IComponentData { }
	public struct RedNode : IComponentData { }
	public struct RedEvent : IComponentData { public int flag; }
	public struct RedStatusChange : IComponentData { }
	public struct RedDestroy : IComponentData { }

	public class RPData
	{
		public int id;
		public RedpointType rtype;
		public string key;
		public Entity check;
		public Entity node;
		public GameConfigs.RedConfigRowData cfg;
		public string res;
		public string ui;
		public string path;
		public string ctr;
		public string filter;
		public string cname;
		public Vector3 offset;
		public string[] nodes;
		public EventHandleContainer ehandler;

		public EventCallback1 clickCall;

		public T GetData<T>(EntityManager mgr) where T : unmanaged, IComponentData
		{
			if (IsExists(mgr))
			{
				return mgr.GetComponentData<T>(node);
			}
			return default;
		}

		public bool IsExists(EntityManager mgr)
		{
			if (node != Entity.Null)
			{
				return mgr.Exists(node);
			}
			return false;
		}

		public void Release(EntityManager mgr)
		{
			id = 0;
			cfg = default;
			key = default;
			if (mgr.Exists(node) == true)
				mgr.DestroyEntity(node);
			ReleaseCheck(mgr);
			ehandler?.Close();
			node = default;
			ehandler = default;
		}

		public void ReleaseCheck(EntityManager mgr)
		{
			if (mgr.Exists(check) == true)
				mgr.DestroyEntity(check);
			check = default;
		}

	}

	public abstract partial class RedpointSystem : SystemBase
	{
		const string TAG = nameof(RedpointSystem);

		const int __red_check_time = 1000;
		const string __red_name = "__redpoint";
		const string __red_key = "redflag_";
		const string __red_layer = "redpoint";

		readonly object __flag = new object();

		private string[] C_DELAY_UI = new string[]{
			"confirmtips","rewardtips","loadingui","rewardflytips","enterseason"
		};

		private Dictionary<int, RPData> _datas = new Dictionary<int, RPData>();
		private Dictionary<string, List<GameConfigs.RedConfigRowData>> _enableCheckGroup = new Dictionary<string, List<GameConfigs.RedConfigRowData>>();
		private Dictionary<string, List<GameConfigs.RedConfigRowData>> _hideCheckGroup = new Dictionary<string, List<GameConfigs.RedConfigRowData>>();
		private EndSimulationEntityCommandBufferSystem _commandBuffSys;
		private Dictionary<int, string> _texts = new Dictionary<int, string>();


		protected bool _isInited = false;

		/// <summary>
		/// 只检测一次的红点
		/// </summary>
		private List<int> _onceCheckReds;
		private List<object> _stateCache = new List<object>();
		public Func<object, object, string, bool> OnCalculation;

		protected override void OnCreate()
		{
			RequireForUpdate<GameInitFinish>();
			_commandBuffSys = this.EntityManager.World.GetOrCreateSystemManaged<EndSimulationEntityCommandBufferSystem>();
		}

		protected override void OnUpdate()
		{
			if (!CheckUpdatae()) return;

			if (_commandBuffSys == null) return;
			var cmd = _commandBuffSys.CreateCommandBuffer();

			//清理
			Entities.WithAny<RedDestroy>().ForEach((Entity e, in Redpoint r) =>
			{
				if (_datas.TryGetValue(r.id, out var h)) h.ehandler?.Close();
				cmd.DestroyEntity(e);
			}).WithoutBurst().WithStructuralChanges().Run();

			//事件触发计算
			Entities.WithAll<RedCheck, RedEvent>().WithNone<RedPause>().ForEach((Entity e, ref Redpoint r, in RedEvent evt) =>
			{
				if (evt.flag > 0) r.status = 1;
				else Calculation(e, cmd, ref r);
			}).WithoutBurst().WithStructuralChanges().Run();

			//定时计算
			Entities.WithAll<RedCheck>().WithNone<RedPause>().ForEach((Entity e, ref Redpoint r) =>
			{
				if (World.Time.ElapsedTime * 1000 > r.time)
					Calculation(e, cmd, ref r);
			}).WithoutBurst().WithStructuralChanges().Run();

			//红点标记操作
			Entities.WithAll<RedNode, RedStatusChange>().ForEach((Entity e, ref Redpoint redpoint) =>
			{
				if (World.Time.ElapsedTime * 1000 > redpoint.time)
				{
					var old = redpoint.status;
					var d = GetRData(redpoint.id);
					var s = CheckRedStatus(redpoint.id, d);
					var t = (int)(World.Time.ElapsedTime * 1000);
					redpoint.status = (byte)(s ? 1 : 0);
					if (d.cfg.Type == 5 || old != redpoint.status)
						redpoint.time = OnRedpointStatusChange(redpoint.id, s, d) + t;
					else
						redpoint.time = (d.cfg.Interval > 0 ? d.cfg.Interval : __red_check_time) + t;
				}
			}).WithoutBurst().WithStructuralChanges().Run();
		}

		protected override void OnDestroy()
		{
			Reset();
			_commandBuffSys = null;
			OnCalculation = null;
		}

		public bool CheckRedStatus(int id, RPData e, bool checkCache = true)
		{
			if (e == null) _datas.TryGetValue(id, out e);
			if (checkCache && e?.IsExists(EntityManager) == true)//自身条件计算
				return e.GetData<Redpoint>(EntityManager).status == 1;
			//下面就是依赖或者直接计算了
			if (e != null)
			{
				if (e.cfg.DependFuncID > 0)
					if (!IsFuncOpened(e.cfg.DependFuncID)) return false;
				switch (e.rtype)
				{
					case RedpointType.Permanent:
					case RedpointType.Group: return true;
					case RedpointType.Leaf: return CheckNodeStatus(true, e);
					case RedpointType.Node: return CheckNodeStatus(false, e);
					case RedpointType.EveryDay:
					case RedpointType.Login:
					case RedpointType.OnceClick:
						return CheckRedPointNeedCheck(id, e?.key);
					case RedpointType.Guide:
						return CheckRedPointNeedCheck(id, e?.key) && CheckNodeStatus(false, e);

				}
			}
			return false;
		}

		public void MarkRedpointGroup(string group, bool enable)
		{
			if (!string.IsNullOrEmpty(group))
			{
				var gs = GetGroup(group, out _);
				_stateCache?.Clear();
				if (gs != null)
				{
					for (int i = 0; i < gs.Count; i++)
					{
						var c = gs[i];
						if (_datas.TryGetValue(c.Id, out var d) && d.IsExists(EntityManager))
						{
							try
							{
								if (enable)
									this.EntityManager.RemoveComponent<RedPause>(d.node);
								else
									this.EntityManager.AddComponent<RedPause>(d.node);
							}
							catch (Exception e)
							{
								Debug.LogError("红点错误：" + d.id);
							}
						}
						else
							_datas[c.Id] = d = CreateData(c);

						if (enable)
						{
							d.check = AddEntity(c);
							this.EntityManager.AddComponent<RedStatusChange>(d.check);
							this.EntityManager.AddComponent<RedNode>(d.check);
						}
						else
						{
							this.EntityManager.DestroyEntity(d.check);
						}
					}
				}
			}
		}

		public void ToggleRedpointGroup(string group, bool open = true, bool imm = false)
		{
			if (!string.IsNullOrEmpty(group))
			{
				var gs = GetGroup(group, out var isOpen);
				if (open && !isOpen)
				{
					_hideCheckGroup.Remove(group);
					_enableCheckGroup[group] = gs;
				}
				else if (!open && isOpen)
				{
					_hideCheckGroup[group] = gs;
					_enableCheckGroup.Remove(group);
				}

				if (imm || !open)
				{
					if (gs != null && gs.Count > 0)
					{
						foreach (var item in gs)
							ToggleRedpoint(item.Id, open);
					}
				}
			}
		}

		public void ToggleRedpoint(int id, bool status)
		{

			if (_datas.TryGetValue(id, out var data))
			{
				ToggleRedpoint(data, status);
			}
		}

		public void ToggleRedpoint(RPData rdata, bool status)
		{
			if (rdata != null)
			{
				var _ui = rdata.ui;
				var cpath = rdata.path;
				if (!string.IsNullOrEmpty(_ui) && !string.IsNullOrEmpty(cpath) && CheckUIState(_ui) == true)
				{
					var _f = rdata.filter;
					var _c = rdata.ctr;
					var e = GetUI(_ui);
					if (e != null && e.isShowing)
					{
						var p = e.contentPane;
						if (p != null)
						{
							var fstate = string.IsNullOrEmpty(_f);
							if (cpath[0] == '*')
							{
								if (!string.IsNullOrEmpty(_c))
								{
									var gos = GameObject.FindGameObjectsWithTag(_c);
									if (gos?.Length > 0)
									{
										for (int i = 0; i < gos.Length; i++)
										{
											if (gos[i].activeInHierarchy && (!fstate || gos[i].name.Contains(_f) || gos[i].transform.Find(_f)))
												SetRedOnGameObject(gos[i], OnCalculation?.Invoke(rdata, gos[i], null) == true, rdata);
										}
									}
								}
								else SetRedOnGameObject(GameObject.Find(cpath.Substring(1)), status, rdata);
							}
							else
							{
								var path = cpath;
								if (path.EndsWith(".*"))
								{
									//path = path.Substring(0, path.Length - 2);
									var list = FindUINode(p, rdata)?.asCom;//p.GetChildByPath(path)?.asCom;
									if (list != null && list.numChildren > 0)
									{
										for (int i = 0; i < list.numChildren; i++)
										{
											var item = list.GetChildAt(i);
											if (item is GComponent && (fstate || item.name.Contains(_f)))
											{
												status = _stateCache.Contains(rdata.id + item.name);
												SetRedPointState(item.asCom, !status && OnCalculation?.Invoke(rdata, item, item.name) == true, rdata);
											}
										}
										/*foreach (var item in list.GetChildren())
										{
											if (item is GComponent && (fstate || item.name.Contains(_f)))
											{
												status = _stateCache.Contains(rdata.id + item.name);
												SetRedPointState(item.asCom, !status && OnCalculation?.Invoke(rdata, item, item.name) == true, rdata);
											}
										}*/
									}
								}
								else
								{
									var child = p.GetChildByPath(path)?.asCom;
									if (child != null) SetRedPointState(child, status, rdata);
									else GameDebug.LogWarning($"没有在界面[{_ui}]找到路径[{cpath}]的对象");
								}
							}
						}
					}
				}
			}
		}

		public bool IsDontCheckConditionType(int type, int id = 0)
		{
			switch ((RedpointType)type)
			{
				case RedpointType.Permanent:
				case RedpointType.EveryDay:
				case RedpointType.Login:
				case RedpointType.Event:
				case RedpointType.Group:
				case RedpointType.OnceClick:
					return true;
				case RedpointType.Guide:
					if (id != 0)
						return !CheckRedPointNeedCheck(id);
					break;
			}
			return false;
		}

		protected virtual void SetRedOnGameObject(GameObject child, bool status, RPData data = null)
		{
			if (!child) return;

			var red = child.transform.Find("__red_auto");//系统添加的红点对象
			var sred = child.transform.Find("__red");//自带红点对象
			if (!status)
			{
				if (red) GameObject.Destroy(red.gameObject);
				if (sred) sred.gameObject.SetActive(false);
			}
			else if (sred) sred.gameObject.SetActive(true);
			else if (red) red.gameObject.SetActive(true);
			else
			{
				this.Delay(() =>
				{
					var e = SpawnSystem.Instance.Spawn(
						data.res, child, 0,
						data.offset + child.transform.position,
						name: "__red_auto");
					if (e != Entity.Null)
						SpawnSystem.Instance.SetLayer(e, LayerMask.NameToLayer(__red_layer));
				}, 1);
			}
		}

		private void SetRedPointState(GComponent child, bool status, RPData data = default)
		{
			if (child != null && data != null)
			{
				FairyGUI.Controller c = default;
				if (!string.IsNullOrEmpty(data.ctr))
					c = child.GetController(data.ctr);
				else
					c = child.GetController(__red_name);

				if (c == null || !string.IsNullOrEmpty(data.res))
				{
					var cname = string.IsNullOrEmpty(data.cname) ? __red_name : data.cname;
					var old = child.GetChildByPath(cname);
					if (status)
					{
						var item = old;
						if (item == null)
						{
							item = FairyGUI.UIPackage.CreateObjectFromURL(data.res)
								?? FairyGUI.UIPackage.CreateObjectFromURL("ui://Common/Redpoint");
							item.name = cname;
							item.data = __flag;//动态创建的红点，加个标识
						}
						item.visible = true;
						if (old == null) child.AddChild(item);
						SetPos(item, data.cfg.Postion, data.offset.x, data.offset.y);
						if (item != null && GetText(data.id, out var txt))
							item.SetText(txt);
					}
					else if (old != null)
					{
						if (old.data == __flag) old.Dispose();
						else old.visible = false;
					}
				}
				else if (c != null) c.selectedIndex = status ? 1 : 0;
				OnRedpointItemChange(child, data, status);
			}
		}

		private void SetPos(FairyGUI.GObject red, int type, float x = 0, float y = 0)
		{
			red?.Center();
			var size = red.parent.size;
			var pos = red.xy;
			switch ((RedAlignType)type)
			{
				case RedAlignType.T:
					pos.y -= size.y * 0.5f;
					break;
				case RedAlignType.BR:
					pos += size * 0.5f;
					break;
				case RedAlignType.R:
					pos.x += size.x * 0.5f;
					break;
				case RedAlignType.TR:
					pos.x += size.x * 0.5f;
					pos.y -= size.y * 0.5f;
					break;
				case RedAlignType.B:
					pos.y += size.y * 0.5f;
					break;
				case RedAlignType.TL:
					pos -= size * 0.5f;
					break;
				case RedAlignType.L:
					pos.x -= size.x * 0.5f;
					break;
				case RedAlignType.BL:
					pos.x -= size.x * 0.5f;
					pos.y += size.y * 0.5f;
					break;
			}
			pos += new Vector2(x, y);
			red?.SetXY(pos.x, pos.y);
		}

		private bool CheckRedList(bool and, params int[] ids)
		{
			if (ids != null && ids.Length > 0)
			{
				for (int i = 0; i < ids.Length; i++)
				{
					var s = CheckRedStatus(ids[i], default);
					if (and && !s)
						return false;
					else if (!and && s)
						return true;
				}
			}
			return and;
		}

		private bool CheckNodeStatus(bool and, RPData data)
		{
			var l = data.cfg.DependsLength;
			if (l > 0)
			{
				for (int i = 0; i < l; i++)
				{
					var s = CheckRedStatus(data.cfg.Depends(i), default);
					if (and && !s)
						return false;
					else if (!and && s)
						return true;
				}
				return and;
			}
			return false;
		}

		/// <summary>
		/// 检测相关key的红点是否没有记录过
		/// </summary>
		/// <param name="id"></param>
		/// <param name="key"></param>
		/// <returns></returns>
		private bool CheckRedPointNeedCheck(int id, string key = null)
		{
			if (_onceCheckReds == null)
				LoadOrSaveLocalRedStatus(0, out _onceCheckReds);
			return !_onceCheckReds.Contains(id) && RedCacheState(id, key) == 0;
		}

		private void Calculation(Entity e, EntityCommandBuffer cmd, ref Redpoint r)
		{
			var time = Calculation(ref r);
			if (time < 0)
				cmd.AddComponent<RedDestroy>(e);
			cmd.RemoveComponent<RedEvent>(e);
		}

		private int Calculation(ref Redpoint redpoint)
		{
			if (redpoint.id > 0)
			{
				var cfg = GetRData(redpoint.id);
				if (cfg != null)
				{
					var fid = cfg.cfg.DependFuncID;
					if (fid > 0 && !FunctionSystem.Instance.IsOpened(fid))
						return redpoint.status = 0;
					else
					{
						var old = redpoint.status;
						var status = redpoint.status == 1;
						if (OnCalculation != null)
							status = OnCalculation(cfg, null, null);
						redpoint.status = (byte)(status ? 1 : 0);
						redpoint.time = (cfg.cfg.Interval == 0 ? __red_check_time : cfg.cfg.Interval) + (int)math.floor(World.Time.ElapsedTime) * 1000;
						return old == redpoint.status ? 0 : 1;
					}
				}
			}
			return -1;
		}

		private int OnRedpointStatusChange(int id, bool status, RPData cfg = default)
		{
			if (cfg != null || (cfg = GetRData(id)) != null)
			{
				ToggleRedpoint(id, status);
				return cfg.cfg.Interval > 0 ? cfg.cfg.Interval : __red_check_time;
			}
			return 0;
		}

		private void OnRedpointItemChange(FairyGUI.GComponent item, RPData data, bool state)
		{
			if (item != null && data != null)
			{
				item.onClick.Remove(data.clickCall);
				var ty = data.rtype;
				if (state && (
					ty == RedpointType.EveryDay
					|| ty == RedpointType.Login
					|| ty == RedpointType.OnceClick
					|| ty == RedpointType.Guide
					|| ty == RedpointType.Group))
				{
					item.onClick.Add(data.clickCall);
				}
			}
		}

		private void OnItemClick(int id, FairyGUI.EventContext context)
		{
			var item = context.sender as FairyGUI.GComponent;
			if (item != null)
			{
				if (_datas.TryGetValue(id, out var e))
				{
					var cfg = e.cfg;
					item.onClick.Remove(e.clickCall);

					if (cfg.Type == ((int)RedpointType.Group))
					{
						if (cfg.SubType == 1) return;
						var o = id + item.name;
						if (_stateCache.Contains(o)) return;
						_stateCache.Add(o);
						SetRedPointState(item, false, e);
						return;
					}
					_onceCheckReds?.Add(id);//游戏期间记录

					SetRedPointState(item, status: false, e);
					if (cfg.Type == (int)RedpointType.EveryDay)
						LoadOrSaveLocalRedStatus(id, out _);//本地缓存
					else if (cfg.Type == (int)RedpointType.OnceClick || cfg.Type == ((int)RedpointType.Guide))
						RedCacheState(0, e.key, 1);
					e.ReleaseCheck(this.EntityManager);
					if (cfg.Closeui > 0 || !string.IsNullOrEmpty(cfg.Gotoui))
						ClickHandler(cfg);
				}
			}
		}

		private void ClickHandler(GameConfigs.RedConfigRowData cfg)
		{
			var flag = false;
			if (cfg.Closeui > 0)
			{
				flag = true;
				this.Call(() => { CloseUI(cfg.Ui); flag = false; }, Check);
			}
			if (!string.IsNullOrEmpty(cfg.Gotoui))
			{
				if (int.TryParse(cfg.Gotoui, out var fid))
					this.Call(() => Goto(fid), () => !flag && Check());
				else
					this.Call(() => OpenUI(cfg.Gotoui), () => !flag && Check());
			}
		}

		private bool Check()
		{
			if (C_DELAY_UI.Length > 0)
			{
				foreach (var item in C_DELAY_UI)
				{
					if (CheckUIState(item))
						return false;
				}
			}
			return true;
		}



		private List<GameConfigs.RedConfigRowData> GetGroup(string group, out bool isOpen)
		{
			isOpen = true;
			if (!_enableCheckGroup.TryGetValue(group, out var list))
			{
				isOpen = false;
				if (!_hideCheckGroup.TryGetValue(group, out list))
				{
					ConfigSystem.Instance.TryGets((c) => ((GameConfigs.RedConfigRowData)c).Ui == group, out list);
					_hideCheckGroup[group] = list;
				}
			}
			return list;
		}

		private GameConfigs.RedConfigRowData GetConfig(int id)
		{
			if (_datas.TryGetValue(id, out var cfg))
				return cfg.cfg;
			return default;
		}

		private RPData GetRData(int id)
		{

			if (_datas.TryGetValue(id, out var cfg))
				return cfg;
			return default;

		}

		protected void Init()
		{
			if (!_isInited)
			{
				_isInited = true;
				InitCalculation();
				InitConditions();
			}
		}

		private void Reset()
		{
			_onceCheckReds?.Clear();
			_onceCheckReds = null;
			foreach (var item in _enableCheckGroup)
				item.Value?.Clear();
			foreach (var item in _hideCheckGroup)
				item.Value?.Clear();
			foreach (var item in _datas)
				item.Value?.Release(EntityManager);
			_enableCheckGroup?.Clear();
		}

		private void InitConditions()
		{
			var all = ConfigSystem.Instance.LoadConfig<GameConfigs.RedConfig>();
			if (all.IsValid() && all.DatalistLength > 0)
			{
				for (int i = 0; i < all.DatalistLength; i++)
				{
					var c = all.Datalist(i).Value;
					if (c.ByteBuffer != null)
					{
						var flag = c.Type > 100 || c.DependsLength == 0;
						var data = _datas[c.Id] = CreateData(c, flag);
						if (!flag || IsDontCheckConditionType(c.Type, c.Id)) continue;
						var r = new Redpoint() { id = c.Id };
						var h = InitEventListen(c);
						var e = EntityManager.CreateEntity();
						data.ehandler = h;
						data.node = e;

						this.EntityManager.AddComponentData(e, r);
						this.EntityManager.AddComponent<RedCheck>(e);
						if (!string.IsNullOrEmpty(c.Ui))
							this.EntityManager.AddComponent<RedPause>(e);
					}
				}
			}
		}

		private EventHandleContainer InitEventListen(GameConfigs.RedConfigRowData cfg)
		{
			if (cfg.IsValid() && cfg.EventIDsLength > 0)
			{
				var handler = default(EventHandleContainer);
				var call = PackEventCall(cfg.Id);
				for (int i = 0; i < cfg.EventIDsLength; i++)
					handler += EventManager.Instance.Reg(cfg.EventIDs(i), call);
				return handler;
			}
			return default;
		}

		private Entity AddEntity(GameConfigs.RedConfigRowData data)
		{
			if (data.IsValid())
			{
				var e = this.EntityManager.CreateEntity();
				this.EntityManager.AddComponentData(e, new Redpoint() { id = data.Id });
				return e;
			}
			return default;
		}

		private Callback PackEventCall(int id)
		{
			return () =>
			{
				if (_datas.TryGetValue(id, out var e))
					this.EntityManager.SetComponentData<RedEvent>(e.node, default);
			};
		}

		private RPData CreateData(GameConfigs.RedConfigRowData cfg, bool needcall = true)
		{
			if (cfg.IsValid())
				return new RPData()
				{
					id = cfg.Id,
					rtype = (RedpointType)cfg.Type,
					cfg = cfg,
					key = needcall ? __red_key + cfg.Id : null,
					res = cfg.Res,
					ui = cfg.Ui,
					path = cfg.Path,
					ctr = cfg.Ctr,
					filter = cfg.Filter,
					cname = cfg.Childname,
					nodes = cfg.Path.Split(".", StringSplitOptions.RemoveEmptyEntries),
					offset = new Vector3(cfg.Offset(0), cfg.Offset(1), cfg.Offset(2)),
					clickCall = needcall ? (e) => OnItemClick(cfg.Id, e) : default
				};
			return default;
		}

		protected void SetText(int id, string txt)
		{
			_texts[id] = txt;
		}

		public bool GetText(int id, out string txt)
		{
			return _texts.TryGetValue(id, out txt);
		}


		private void LoadOrSaveLocalRedStatus(int id, out List<int> status)
		{
			status = null;
			var uid = GetUserID();
			var old = string.Format("{0}_{1}_{2}", uid, TAG, GameServerTime.Instance.serverDay - 1);
			var key = string.Format("{0}_{1}_{2}", uid, TAG, GameServerTime.Instance.serverDay);
			var next = string.Format("{0}_{1}_{2}", uid, TAG, GameServerTime.Instance.serverDay + 1);
			PlayerPrefs.DeleteKey(old);
			PlayerPrefs.DeleteKey(next);

			var str = PlayerPrefs.GetString(key);
			if (id > 0)
				PlayerPrefs.SetString(key, str + "|" + id);
			else
			{
				if (!string.IsNullOrEmpty(str))
					status = str.Split("|").Where(v => !string.IsNullOrEmpty(v)).Select(v => int.Parse(v)).ToList();
				else
					status = new List<int>();
			}
		}

		private GObject FindUINode(GComponent component, RPData data)
		{
			if (component != null && data != null && data.nodes!=null)
			{
				var arr = data.nodes;
				int cnt = arr.Length - 1;
				GComponent gcom = component;
				GObject obj = null;
				for (int i = 0; i < cnt; ++i)
				{
					obj = gcom.GetChild(arr[i]);
					if (obj == null) break;

					if (i != cnt - 1)
					{
						if (!(obj is GComponent))
						{
							obj = null;
							break;
						}
						else
							gcom = (GComponent)obj;
					}
				}
				return obj;

			}
			return default;
		}

		#region Handler

		protected virtual void InitCalculation()
		{
		}

		protected virtual int RedCacheState(int id, string key = default, int val = int.MaxValue)
		{
			if (val == int.MaxValue)
				return DataCenter.GetIntValue(key ?? __red_key + id);
			else
				DataCenter.SetIntValue(key ?? __red_key + id, val);
			return val;
		}

		protected virtual void OpenUI(string ui) => UIUtils.OpenUI(ui);

		protected virtual void CloseUI(string ui) => UIUtils.CloseUIByName(ui);

		protected virtual bool CheckUIState(string ui) => UIUtils.CheckUIIsOpen(ui);

		protected virtual FairyWindow GetUI(string ui) => default;

		protected virtual void Goto(int id) => id.Goto();

		protected virtual bool IsFuncOpened(int id) => id.IsOpend(false);

		protected virtual string GetUserID()
		{
			return SystemInfo.deviceUniqueIdentifier;
		}

		protected virtual bool CheckUpdatae() { return true; }

		#endregion

	}
}
