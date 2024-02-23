using System.Collections;
using Unity.Entities;
using FairyGUI;
using System.Collections.Generic;
using UnityEngine;
using libx;
using log4net;
using log4net.Core;

namespace SGame
{
	// 请求创建UI特效
	public class RequestSpawnUIEffect : IComponentData
	{
		// 创建类型
		public enum ReqType
		{
			REQ_FAIRYUI,                        // 请求FairyUI的UI
			REQ_3DPARENT,                       // 带父节点的3D对象
			REQ_3D,                             // 不带父节点的
		}

		public int effectId;       // 特效ID
		public Entity entity;         // 创建后通知entity

		public Vector3 position;       // 位置
		public Vector3 scale;          // 缩放
		public Quaternion rotation;       // 旋转

		public GGraph hoder;         // 特效占位的位置
		public GameObject parent;        // 挂载的父节点
		public ReqType reqType;       // 请求类型
		public float duration;      // 特效持续时间 0 表示一致使用

		public AssetRequest prefabRequest; // 加载的资源请求 
	}

	// 创建好的特效会添加
	public struct EffectSysData : ISystemStateComponentData
	{
	}

	// 特效对象标签
	public struct EffectData : IComponentData
	{
		public int effectId; // 特效ID
	}

	// 特效系统
	public partial class EffectSystem : SystemBase
	{
		// 配置表系统
		private ConfigSystem configSystem;

		// 实例化后的特效
		private Dictionary<Entity, GameObject> m_effectObjects;
		//private Dictionary<Entity, GoWrapper >   m_uiWrappers;
		//private Dictionary<Entity, GGraph >     m_uiHoders;

		// 请求创建UI特效
		private EntityArchetype m_actRequestUIEffect;

		// 请求创建世界特效
		private EntityArchetype m_actRequestWorldEffect;

		// 特效对象
		private EntityArchetype m_actEffect;

		//private EntityCommandBufferSystem       m_commandBuffer;

		static ILog log = LogManager.GetLogger("EffectSystem");

		protected override void OnCreate()
		{
			base.OnCreate();

			m_effectObjects = new Dictionary<Entity, GameObject>();
			configSystem = ConfigSystem.Instance;
			//m_commandBuffer             = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();

			m_actEffect = EntityManager.CreateArchetype(typeof(EffectData));
			m_actRequestUIEffect = EntityManager.CreateArchetype(typeof(RequestSpawnUIEffect));
		}

		public static EffectSystem Instance
		{
			get { return World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<EffectSystem>(); }
		}

		protected override void OnUpdate()
		{
			//EntityCommandBuffer cb = m_commandBuffer.CreateCommandBuffer();

			SpawnUIEffect();

			ProcessDestoryEffect();
		}

		// 实例化特效
		GameObject InstanceEffect(Entity effect, AssetRequest prefabRequest)
		{
			GameObject prefab = prefabRequest.asset as GameObject;
			if (prefab == null)
			{
				log.Error("Prefab Is Null!");
				return null;
			}

			GameObject obj = GameObject.Instantiate(prefab);
			EffectMono mono = obj.AddComponent<EffectMono>();
			mono.entity = effect;
			mono.prefabRequest = prefabRequest;
			m_effectObjects.Add(effect, obj);

			return obj;
		}

		void SetupGameObject(GameObject obj, Vector3 pos, Vector3 scale, Quaternion rotation)
		{
			var trans = obj.transform;
			trans.localPosition = pos;
			trans.localScale = scale;
			trans.localRotation = rotation;
		}

		// 创建UI特效
		void SpawnUIEffect()
		{
			Entities.WithAll<RequestSpawnUIEffect>().ForEach((Entity e, RequestSpawnUIEffect req) =>
			{
				switch (req.reqType)
				{
					case RequestSpawnUIEffect.ReqType.REQ_FAIRYUI:
						if (req.hoder == null || req.hoder.isDisposed)
						{
							// 挂点已经销毁, 不用创建了
							req.prefabRequest.Release();
							req.prefabRequest = null;
							EntityManager.DestroyEntity(e);
							return;
						}
						break;
					case RequestSpawnUIEffect.ReqType.REQ_3D:
						break;
					case RequestSpawnUIEffect.ReqType.REQ_3DPARENT:
						if (req.parent == null)
						{
							// 有父节点, 但是父节点已经被销毁!
							req.prefabRequest.Release();
							req.prefabRequest = null;
							EntityManager.DestroyEntity(e);
							return;
						}
						break;
				}


				// 等待资源加载完毕
				var prefabRequest = req.prefabRequest;
				if (prefabRequest.isDone)
				{
					// 加载资源
					if (string.IsNullOrEmpty(prefabRequest.error))
					{
						if (EntityManager.Exists(req.entity) == true)
						{
							// 实例化特效
							GameObject obj = InstanceEffect(req.entity, prefabRequest);

							// 添加到fairygui 上面去
							if (req.reqType == RequestSpawnUIEffect.ReqType.REQ_FAIRYUI)
							{
								//GoWrapper wrapper = new GoWrapper(obj);
								GoWrapper wrapper = req.hoder.displayObject as GoWrapper;
								if (wrapper != null)
								{
									if (wrapper.wrapTarget != null)
										GameObject.Destroy(wrapper.wrapTarget);
									wrapper.wrapTarget = obj;
								}
								else
								{
									wrapper = new GoWrapper(obj);
								}

								req.hoder.SetNativeObject(wrapper);
							}

							if (req.reqType == RequestSpawnUIEffect.ReqType.REQ_3DPARENT)
							{
								// 设置父节点
								obj.transform.parent = req.parent.transform;
							}

							// 设置特效属性
							SetupGameObject(obj, req.position, req.scale, req.rotation);

							// 加载成功
							EntityManager.AddComponent<EffectSysData>(req.entity);
						}
						else
						{
							req.prefabRequest.Release();
							req.prefabRequest = null;
							EntityManager.DestroyEntity(e);
						}
					}
					else
					{
						// 加载失败
						EntityManager.DestroyEntity(req.entity);
						req.prefabRequest.Release();
						log.Error("load effect fail!" + req.prefabRequest.error);
					}

					req.prefabRequest = null;
					EntityManager.DestroyEntity(e);
				}
			}).WithStructuralChanges().WithoutBurst().Run();
		}

		// 清理创建的GameObject对象
		void ProcessDestoryEffect()
		{
			Entities.WithAll<EffectSysData>().WithNone<EffectData>().ForEach((Entity e) =>
			{
				GameObject effectObject = null;
				if (m_effectObjects.Remove(e, out effectObject))
				{
					GameObject.Destroy(effectObject);
				}

				EntityManager.RemoveComponent<EffectSysData>(e);
			}).WithStructuralChanges().WithoutBurst().Run();
		}

		// 删除特效
		public void CloseEffect(Entity e)
		{
			// 添加销毁entity 的事件
			if (EntityManager.Exists(e) && !EntityManager.HasComponent<DespawningEntity>(e))
			{
				EntityManager.AddComponent<DespawningEntity>(e);
			}
		}

		// 判断特效是否加载完成
		public bool IsLoaded(Entity e)
		{
			return EntityManager.HasComponent<EffectData>(e) && EntityManager.HasComponent<EffectSysData>(e);
		}

		// 尝试获取特效GameObject对象
		public GameObject GetEffect(Entity e)
		{
			GameObject ret = null;
			if (m_effectObjects.TryGetValue(e, out ret))
				return ret;
			return null;
		}

		/// <summary>
		/// 解析配置数据
		/// </summary>
		/// <param name="config"></param>
		/// <param name="pos"></param>
		/// <param name="scale"></param>
		/// <param name="rotation"></param>
		/// <returns></returns>
		bool ParseConfigData(GameConfigs.effectsRowData config, out Vector3 pos, out Vector3 scale, out Quaternion rotation)
		{
			pos = Vector3.zero;
			scale = Vector3.one;
			rotation = Quaternion.identity;

			if (config.PositionLength != 0 && config.PositionLength != 3)
				return false;
			if (config.ScaleLength != 0 && config.ScaleLength != 3)
				return false;
			if (config.EulerAngleLength != 0 && config.EulerAngleLength != 3)
				return false;

			if (config.PositionLength == 3)
			{
				pos.x = config.Position(0);
				pos.y = config.Position(1);
				pos.z = config.Position(2);
			}

			if (config.ScaleLength == 3)
			{
				scale.x = config.Scale(0);
				scale.y = config.Scale(1);
				scale.z = config.Scale(2);
			}

			if (config.EulerAngleLength == 3)
			{
				float x = config.EulerAngle(0);
				float y = config.EulerAngle(1);
				float z = config.EulerAngle(2);
				rotation = Quaternion.Euler(x, y, z);
			}

			return true;
		}

		/// <summary>
		/// 获得特效路径
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		string GetEffectPath(string name)
		{
			if (name.EndsWith(".prefab")) return name;
			return "Assets/BuildAsset/Effects/prefabs/" + name + ".prefab";
		}

		/// <summary>
		/// 创建特效基础函数
		/// </summary>
		/// <param name="spawnData">请求创建对象</param>
		/// <returns>新的UI对象的索引</returns>
		public Entity SpawnBase(RequestSpawnUIEffect spawnData)
		{
			// 创建特效对象
			if (spawnData.entity == Entity.Null)
			{
				spawnData.entity = EntityManager.CreateEntity(m_actEffect);
				// 特效数据
				EntityManager.SetComponentData(spawnData.entity, new EffectData
				{
					effectId = spawnData.effectId
				});
			}

			// 创建特效请求
			Entity spawnRequest = EntityManager.CreateEntity(m_actRequestUIEffect);

			// 设置加载请求
			EntityManager.SetComponentData(spawnRequest, spawnData);

			if (spawnData.duration > 0 && EntityManager.HasComponent<LiveTime>(spawnData.entity) == false)
			{
				EntityManager.AddComponent<LiveTime>(spawnData.entity);
				EntityManager.SetComponentData(spawnData.entity, new LiveTime { Value = spawnData.duration });
			}

			var d = EntityManager.GetComponentData<RequestSpawnUIEffect>(spawnRequest);
			return spawnData.entity;
		}

		// 通告特效配置表创建3D
		public Entity Spawn3d(int effectId, GameObject parent = null , Vector3 point = default)
		{
			if (!configSystem.TryGet(effectId, out GameConfigs.effectsRowData config))
			{
				// 特效ID 找不到
				log.Error("Effect Id Not Found=" + effectId.ToString());
				return Entity.Null;
			}

			Vector3 pos = Vector3.zero;
			Vector3 scale = Vector3.one;
			Quaternion rotation = Quaternion.identity;
			if (!ParseConfigData(config, out pos, out scale, out rotation))
			{
				// 配置格式错误
				log.Error(string.Format("Parse Config Fail id = {0} position or scale or rotation format is not accept!", effectId));
				return Entity.Null;
			}

			// 加载prefab资源
			var assetRequest = libx.Assets.LoadAssetAsync(GetEffectPath(config.Prefab), typeof(GameObject));
			if (!string.IsNullOrEmpty(assetRequest.error))
			{
				assetRequest.Release();
				log.Error("Load Prefab Fail Error =" + assetRequest.error);
				return Entity.Null;
			}

			// 设置加载请求
			var req = new RequestSpawnUIEffect
			{
				effectId = effectId,
				entity = Entity.Null,
				hoder = null,
				position =  point + pos ,
				rotation = rotation,
				scale = scale,
				prefabRequest = assetRequest,
				reqType = parent != null ? RequestSpawnUIEffect.ReqType.REQ_3DPARENT : RequestSpawnUIEffect.ReqType.REQ_3D,
				parent = parent,
				duration = config.Duration,
			};

			return SpawnBase(req);
		}

		// 通过资源加载创建3D
		public Entity SpawnGameObject(string assetPath, GameObject parent, Vector3 pos, Vector3 scale, Quaternion rot)
		{
			// 加载prefab资源
			var assetRequest = libx.Assets.LoadAssetAsync(assetPath, typeof(GameObject));
			if (!string.IsNullOrEmpty(assetRequest.error))
			{
				assetRequest.Release();
				log.Error("Load Prefab Fail Error =" + assetRequest.error);
				return Entity.Null;
			}

			// 设置加载请求
			var req = new RequestSpawnUIEffect
			{
				effectId = 0,
				entity = Entity.Null,
				hoder = null,
				position = pos,
				rotation = rot,
				scale = scale,
				prefabRequest = assetRequest,
				reqType = parent != null ? RequestSpawnUIEffect.ReqType.REQ_3DPARENT : RequestSpawnUIEffect.ReqType.REQ_3D,
				parent = parent,
				duration = 0,
			};

			return SpawnBase(req);
		}

		// 在UI上添加特效
		public Entity SpawnUI(int effectId, GGraph hoder)
		{

			if (!configSystem.TryGet(effectId, out GameConfigs.effectsRowData config))
			{
				// 特效ID 找不到
				log.Error("Effect Id Not Found=" + effectId.ToString());
				return Entity.Null;
			}

			Vector3 pos = Vector3.zero;
			Vector3 scale = Vector3.one;
			Quaternion rotation = Quaternion.identity;
			if (!ParseConfigData(config, out pos, out scale, out rotation))
			{
				// 配置格式错误
				log.Error(string.Format("Parse Config Fail id = {0} position or scale or rotation format is not accept!", effectId));
				return Entity.Null;
			}

			// 加载prefab资源
			var assetRequest = libx.Assets.LoadAssetAsync(GetEffectPath(config.Prefab), typeof(GameObject));
			if (!string.IsNullOrEmpty(assetRequest.error))
			{
				assetRequest.Release();
				log.Error("Load Prefab Fail Error =" + assetRequest.error);
				return Entity.Null;
			}

			// 设置加载请求
			var req = new RequestSpawnUIEffect
			{
				effectId = effectId,
				entity = Entity.Null,
				hoder = hoder,
				position = pos,
				rotation = rotation,
				scale = scale,
				prefabRequest = assetRequest,
				reqType = RequestSpawnUIEffect.ReqType.REQ_FAIRYUI,
				duration = config.Duration,
			};

			return SpawnBase(req);
		}

		// 等待特效加载结束
		public IEnumerator WaitEffectLoaded(Entity e, System.Action callback = null)
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

		// 等待特效并产生回调
		public void WaitEffect(Entity e, System.Action onFinish)
		{
			FiberCtrl.Pool.Run(WaitEffectLoaded(e, onFinish));
		}

	}
}
