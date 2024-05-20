using System.Collections;
using Unity.Entities;
using System.Collections.Generic;
using UnityEngine;
using libx;
using log4net;
using System;
using Unity.Mathematics;

namespace SGame
{


	public struct SpawnReq : IComponentData
	{
		public uint assetID;
		public uint parentID;
		public float life;
		public float3 pos;
		public float3 rot;
		public int scale ;
		public int layer;
		public uint nameID;
	}


	public class RequestSpawn : IComponentData
	{
		public string path;       // 路径

		public Vector3 position;       // 位置
		public Vector3 scale;          // 缩放
		public Quaternion rotation;       // 旋转

		public GameObject parent;         // 挂载的父节点
		public bool isParent;       // 是否有父节点

		public AssetRequest prefabRequest;  // 资源加载请求
		public Entity entity;         // 创建后通知entity
		public string gname;
	}

	public struct SpawnSysData : ISystemStateComponentData { }

	public struct SpawnData : IComponentData
	{
		public int id;
	}

	public struct SpawnLayer : IComponentData
	{
		public int layer;
	}

	public class MonoProxy : MonoBehaviour
	{
		public Entity entity;
		public AssetRequest request;

		private void OnDestroy()
		{
			if (request != null)
			{
				request.Release();
				request = null;
			}

			if (entity != Entity.Null)
			{
				if (World.DefaultGameObjectInjectionWorld != null)
				{
					EntityManager mgr = World.DefaultGameObjectInjectionWorld.EntityManager;
					if (mgr.Exists(entity))
						mgr.AddComponent<DespawningEntity>(entity);
				}
				entity = Entity.Null;
			}
		}
	}

	public partial class SpawnSystem : ComponentSystem
	{
		static ILog log = LogManager.GetLogger("SpawnSystem");

		private Dictionary<Entity, GameObject> m_spawnObjects;
		private EntityArchetype m_spreqType;
		private EntityArchetype m_requestType;
		private EntityArchetype m_spawnType;
		private EntityCommandBufferSystem m_commandBuffer;

		static public SpawnSystem Instance
		{
			get
			{
				return World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<SpawnSystem>();
			}
		}


		protected override void OnCreate()
		{
			base.OnCreate();

			m_spawnObjects = new Dictionary<Entity, GameObject>();
			m_commandBuffer = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();

			m_spawnType = EntityManager.CreateArchetype(typeof(SpawnData));
			m_requestType = EntityManager.CreateArchetype(typeof(RequestSpawn));
			m_spreqType = EntityManager.CreateArchetype(typeof(SpawnReq));

		}

		protected override void OnUpdate()
		{
			var cb = m_commandBuffer.CreateCommandBuffer();
			Spawn(cb);
			Destory(cb);
		}

		void Spawn(EntityCommandBuffer cb)
		{

			Entities.ForEach((Entity e, RequestSpawn req) =>
			{
				if (req.isParent && req.parent == null)
				{
					// 有父节点, 但是父节点已经被销毁!
					req.prefabRequest.Release();
					req.prefabRequest = null;
					cb.DestroyEntity(e);
					return;
				}

				// 等待资源加载完毕
				var prefabRequest = req.prefabRequest;
				if (prefabRequest.isDone)
				{
					// 加载资源
					if (string.IsNullOrEmpty(prefabRequest.error))
					{
						// 实例化特效
						GameObject obj = Create(req.entity, prefabRequest);
						if (!string.IsNullOrEmpty(req.gname))
							obj.name = req.gname;
						// 设置父节点
						if (req.isParent)
						{
							obj.transform.parent = req.parent.transform;
						}

						// 设置特效属性
						Setup(obj, req.position, req.scale, req.rotation);

						// 加载成功
						cb.AddComponent<SpawnSysData>(req.entity);
					}
					else
					{
						// 加载失败
						cb.DestroyEntity(req.entity);
						req.prefabRequest.Release();
						log.Error("load asset fail!" + req.prefabRequest.error);
					}

					req.prefabRequest = null;
					cb.DestroyEntity(e);
				}
			});

			Entities.ForEach((Entity e, ref SpawnReq req) =>
			{
				cb.RemoveComponent<SpawnReq>(e);
				Spawn(
					req.assetID.FromIndex<string>(),
					req.parentID.FromIndex<GameObject>(true),
					req.life,
					req.pos,
					req.rot,
					req.scale == 0 ? 1 : req.scale,
					req.nameID.FromIndex<string>(true),
					e
				);

			});
		}

		GameObject Create(Entity entity, AssetRequest prefabRequest)
		{
			GameObject prefab = prefabRequest.asset as GameObject;
			if (prefab == null)
			{
				log.Error("Prefab Is Null!");
				return null;
			}
			var obj = GameObject.Instantiate(prefab);
			var mono = obj.AddComponent<MonoProxy>();
			obj.name = System.IO.Path.GetFileNameWithoutExtension(prefabRequest.name);
			mono.entity = entity;
			mono.request = prefabRequest;
			m_spawnObjects.Add(entity, obj);
			if (EntityManager.HasComponent<SpawnLayer>(entity))
			{
				var layer = EntityManager.GetComponentData<SpawnLayer>(entity);
				if (layer.layer > 0)
					obj.SetLayer(LayerMask.LayerToName(layer.layer));
			}
			return obj;
		}

		void Setup(GameObject obj, Vector3 pos, Vector3 scale, Quaternion rotation)
		{
			var trans = obj.transform;
			trans.SetLocalPositionAndRotation(pos, rotation);
			trans.localScale = scale;
		}

		void Destory(EntityCommandBuffer cb)
		{
			Entities.WithAll<SpawnSysData>().WithNone<SpawnData>().ForEach((Entity e) =>
			{
				GameObject obj = null;
				if (m_spawnObjects.Remove(e, out obj))
				{
					if (obj)
						GameObject.Destroy(obj);
					cb.RemoveComponent<SpawnSysData>(e);
				}
			});
		}

		public void Release(Entity e)
		{
			// 添加销毁entity 的事件
			if (e != Entity.Null && EntityManager.Exists(e))
				EntityManager.AddComponent<DespawningEntity>(e);
		}

		public bool IsLoaded(Entity e)
		{
			return EntityManager.HasComponent<SpawnData>(e) && EntityManager.HasComponent<SpawnSysData>(e);
		}

		public GameObject GetObject(Entity e)
		{
			GameObject ret = null;
			if (m_spawnObjects.TryGetValue(e, out ret))
				return ret;
			return null;
		}

		public IEnumerator SpawnAndWait(string path, GameObject parent = null, float life = 0,
			Vector3 pos = default, Vector3 rot = default, float scale = 1,
			Action<bool, Entity> complete = null
		)
		{
			var e = Spawn(path, parent, life, pos, rot, scale);
			if (e != Entity.Null)
			{
				yield return WaitLoaded(e);
				complete?.Invoke(true, e);
				yield return GetObject(e);
			}
			else
				complete?.Invoke(false, default);
		}

		public Entity Spawn(string path, GameObject parent = null, float life = 0, Vector3 pos = default, Vector3 rot = default, float scale = 1, string name = null, Entity holder = default)
		{
			if (string.IsNullOrEmpty(path)) return Entity.Null;


			if (!System.IO.Path.HasExtension(path))
				path += ".prefab";

			// 加载prefab资源
			var assetRequest = libx.Assets.LoadAssetAsync(path, typeof(GameObject));
			if (!string.IsNullOrEmpty(assetRequest.error))
			{
				assetRequest.Release();
				log.Error("Load Prefab Fail Error =" + assetRequest.error);
				return Entity.Null;
			}

			// 创建特效对象
			Entity entity = default;
			if (EntityManager.Exists(holder))
			{
				EntityManager.AddComponent<SpawnData>(holder);
				entity = holder;
			}
			else
				entity = EntityManager.CreateEntity(m_spawnType);

			// 创建特效请求
			Entity spawnRequest = EntityManager.CreateEntity(m_requestType);

			// 设置加载请求
			EntityManager.SetComponentData(spawnRequest, new RequestSpawn
			{
				path = path,
				entity = entity,
				position = pos,
				rotation = Quaternion.Euler(rot),
				scale = Vector3.one * scale,
				prefabRequest = assetRequest,
				parent = parent,
				isParent = parent != null,
				gname = name
			});

			// 特效数据
			EntityManager.SetComponentData(entity, new SpawnData() { id = path.GetHashCode() });

			if (life > 0)
			{
				EntityManager.AddComponent<LiveTime>(entity);
				EntityManager.SetComponentData(entity, new LiveTime { Value = life });
			}

			return entity;
		}

		public IEnumerator WaitLoaded(Entity e, System.Action callback = null)
		{
			while (true)
			{
				if (EntityManager.Exists(e) == false)
					break;

				if (IsLoaded(e))
					break;

				yield return null;
			}

			if (callback != null)
				callback();
		}

		public void Wait(Entity e, System.Action onFinish)
		{
			FiberCtrl.Pool.Run(WaitLoaded(e, onFinish));
		}

		public void SetLayer(Entity e, int layer)
		{
			if (e != Entity.Null)
				EntityManager.AddComponentData(e, new SpawnLayer() { layer = layer });
		}
	}


	public class SpawnContainer : EC<SpawnContainer>
	{
		private WeakReference<SpawnSystem> reference;

		public SpawnContainer()
		{
			reference = new WeakReference<SpawnSystem>(SpawnSystem.Instance);
		}

		protected override void DoDestroy(Entity e)
		{
			if (e != Entity.Null)
			{
				if (reference != null && reference.TryGetTarget(out var s))
				{
					var c = s.GetObject(e);
					if (c) c.SetActive(false);
					if (s.World.EntityManager.Exists(e))
						s.Release(e);
				}
			}
		}

		protected override void OnDispoed()
		{
			base.OnDispoed();
			reference?.SetTarget(null);
			reference = null;
		}
	}

}
