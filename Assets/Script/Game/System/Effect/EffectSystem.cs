using System.Collections;
using Unity.Entities;
using FairyGUI;
using System.Collections.Generic;
using UnityEngine;
using libx;
using log4net;
using log4net.Core;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine.Animations;

namespace SGame
{
	// 请求创建UI特效
	public class RequestSpawnEffect : IComponentData
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
	public struct EffectSysData : ICleanupComponentData
	{
		public int		effectId; // 特效ID
		public PoolID	poolID;   // 对象池ID 
	}

	// 特效对象标签
	public struct EffectData : IComponentData
	{
		public int		effectId;	// 特效ID
	}

	// 特效系统
	public partial class EffectSystem : SystemBase
	{
		// 配置表系统
		private ConfigSystem configSystem;

		// 实例化后的特效
		//private Dictionary<Entity, GameObject> m_effectObjects;

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

			//m_effectObjects = new Dictionary<Entity, GameObject>();
			configSystem = ConfigSystem.Instance;
			//m_commandBuffer             = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();

			m_actEffect = EntityManager.CreateArchetype(typeof(EffectData), typeof(LocalTransform), typeof(LocalToWorld));
			m_actRequestUIEffect = EntityManager.CreateArchetype(typeof(RequestSpawnEffect));
		}

		public static EffectSystem Instance
		{
			get { return World.DefaultGameObjectInjectionWorld.GetOrCreateSystemManaged<EffectSystem>(); }
		}

		protected override void OnUpdate()
		{
			//EntityCommandBuffer cb = m_commandBuffer.CreateCommandBuffer();

			SpawnEffect();

			ProcessDestoryEffect();
		}

		// 实例化特效
		GameObject InstanceEffect(Entity effect, int effectID, AssetRequest prefabRequest, RequestSpawnEffect.ReqType reqType)
		{
			GameObject prefab = prefabRequest.asset as GameObject;
			if (prefab == null)
			{
				log.Error("Prefab Is Null!");
				return null;
			}

			var poolID = EffectFactory.Instance.Alloc(effectID, prefabRequest);
			if (poolID == PoolID.NULL)
			{
				log.Error("alloc effect fail=" + effectID);
				return null;
			}

			EntityManager.AddComponentData(effect, new EffectSysData() { effectId = effectID, poolID = poolID });
			GameObject obj = EffectFactory.Instance.Get(effectID, poolID); //GameObject.Instantiate(prefab);
			log.Debug("effect instance =" + effectID + " instanceID="  + obj.GetInstanceID());
			
			if (reqType != RequestSpawnEffect.ReqType.REQ_3D && reqType != RequestSpawnEffect.ReqType.REQ_3DPARENT)
			{

				EffectMono mono = obj.GetComponent<EffectMono>();
				if (mono == null)
				{
					mono = obj.AddComponent<EffectMono>();
				}

				mono.entity = effect;
				mono.effectID = effectID;
				return obj;
			}
			else
			{
				// 3D对象特殊处理, 使用Entity属性来完成
				GameObject parent = new GameObject("effect");
				obj.transform.parent = parent.transform;
				var localTransform = EntityManager.GetComponentData<LocalTransform>(effect);
				parent.transform.position = localTransform.Position;
				parent.transform.rotation = localTransform.Rotation;
				parent.transform.localScale = new Vector3(localTransform.Scale, localTransform.Scale, localTransform.Scale);
				EffectMono mono = parent.AddComponent<EffectMono>();
				EntityManager.AddComponentObject(effect, mono);
				EntityManager.AddComponentObject(effect, mono.transform);
				mono.entity = effect;
				mono.effectID = effectID;
				return parent;
			}
		}

		void SetupGameObject(GameObject obj, Vector3 pos, Vector3 scale, Quaternion rotation)
		{
			var trans = obj.transform;
			trans.localPosition = pos;
			trans.localScale = scale;
			trans.localRotation = rotation;

		}

		// 创建UI特效
		void SpawnEffect()
		{
			Entities.WithAll<RequestSpawnEffect>().ForEach((Entity e, RequestSpawnEffect req) =>
			{
				switch (req.reqType)
				{
					case RequestSpawnEffect.ReqType.REQ_FAIRYUI:
						if (req.hoder == null || req.hoder.isDisposed) 
						{
							// 挂点已经销毁, 不用创建了
							req.prefabRequest.Release();
							req.prefabRequest = null;
							EntityManager.DestroyEntity(e);
							return;
						}
						break;
					case RequestSpawnEffect.ReqType.REQ_3D:
						break;
					case RequestSpawnEffect.ReqType.REQ_3DPARENT:
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
							// 对象先删除再生成, 防止对象是刚生成的那个
							if (req.reqType == RequestSpawnEffect.ReqType.REQ_FAIRYUI)
							{
								GoWrapper wrapper = req.hoder.displayObject as GoWrapper;
								if (wrapper != null && wrapper.wrapTarget != null)
								{
									CloseEffect(wrapper.wrapTarget);
									wrapper.wrapTarget = null;
								}
							}
							
							// 实例化特效
							GameObject obj = InstanceEffect(req.entity, req.effectId, prefabRequest, req.reqType);

							// 添加到fairygui 上面去
							switch (req.reqType)
							{
								case RequestSpawnEffect.ReqType.REQ_FAIRYUI:
									{
										GoWrapper wrapper = req.hoder.displayObject as GoWrapper;
										if (wrapper != null)
										{
											if (wrapper.wrapTarget != null)
												log.Error("wrapper is not null!!!");
											wrapper.wrapTarget = obj;
										}
										else
										{
											wrapper = new GoWrapper(obj);
										}

										req.hoder.SetNativeObject(wrapper);
									}

									// 设置缩放
									SetupGameObject(obj, req.position, req.scale, req.rotation);
									break;

								case RequestSpawnEffect.ReqType.REQ_3DPARENT:
									{
										// 自带父节点
										//obj.transform.parent = req.parent.transform;
										obj.transform.SetParent(req.parent.transform, false);
										//SetupGameObject(obj, req.position + (Vector3)GetComponent<Translation>(req.entity).Value, req.scale, req.rotation);
										SetupGameObject(obj.transform.GetChild(0).gameObject, req.position, req.scale, req.rotation);
									}
									break;

								case RequestSpawnEffect.ReqType.REQ_3D:
									{
										// 设置子节点位置信息
										SetupGameObject(obj.transform.GetChild(0).gameObject, req.position, req.scale, req.rotation);
									}
									break;
							}


							// 加载成功
							//EntityManager.AddComponent<EffectSysData>(req.entity);

							EffectMono mono = obj.GetComponent<EffectMono>();
							if (mono != null)
								mono.Play();
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
			}).WithoutBurst().WithStructuralChanges().Run();
		}

		// 清理创建的GameObject对象
		void ProcessDestoryEffect()
		{
			Entities.WithNone<EffectData>().ForEach((Entity e, in EffectSysData data) =>
			{
				EffectFactory.Instance.Free(data.effectId, data.poolID);
				EntityManager.RemoveComponent<EffectSysData>(e);
			}).WithoutBurst().WithStructuralChanges().Run();
		}

		public void CloseEffect(GameObject obj)
		{
			if (obj != null)
			{
				log.Debug("effect close name=" + obj.name + "instanceID=" + obj.GetInstanceID());

				var mono = obj.GetComponentInParent<EffectMono>(true);
				if (mono == null)
				{
					GameObject.Destroy(obj);
					return;
				}

				if (mono.entity != Entity.Null)
				{
					CloseEffect(mono.entity);
				}
			}
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
			if (!IsLoaded(e))
				return null;

			var sysData = EntityManager.GetComponentData<EffectSysData>(e);
			return EffectFactory.Instance.Get(sysData.effectId, sysData.poolID);
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
		public Entity SpawnBase(RequestSpawnEffect spawnData)
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

			var d = EntityManager.GetComponentData<RequestSpawnEffect>(spawnRequest);
			if (spawnData.reqType == RequestSpawnEffect.ReqType.REQ_3D)
			{
				// 添加上对象同步
				if (!EntityManager.HasComponent<EntitySyncGameObjectTag>(spawnData.entity))
					EntityManager.AddComponent<EntitySyncGameObjectTag>(spawnData.entity);
			}
			
			EntityManager.SetComponentData(spawnData.entity, LocalTransform.Identity);
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
			var req = new RequestSpawnEffect
			{
				effectId		= effectId,
				entity			= Entity.Null,
				hoder			= null,
				position		= pos ,
				rotation		= rotation,
				scale			= scale,
				prefabRequest	= assetRequest,
				reqType			= parent != null ? RequestSpawnEffect.ReqType.REQ_3DPARENT : RequestSpawnEffect.ReqType.REQ_3D,
				parent			= parent,
				duration		= config.Duration,
			};

			// 设置位置等信息
			var e = SpawnBase(req);
			LocalTransform trans = EntityManager.GetComponentData<LocalTransform>(e);
			trans.Position = point;
			EntityManager.SetComponentData(e, trans);
			return e;
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
			var req = new RequestSpawnEffect
			{
				effectId = 0,
				entity = Entity.Null,
				hoder = null,
				position = pos,
				rotation = rot,
				scale = scale,
				prefabRequest = assetRequest,
				reqType = parent != null ? RequestSpawnEffect.ReqType.REQ_3DPARENT : RequestSpawnEffect.ReqType.REQ_3D,
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
			var req = new RequestSpawnEffect
			{
				effectId = effectId,
				entity = Entity.Null,
				hoder = hoder,
				position = pos,
				rotation = rotation,
				scale = scale,
				prefabRequest = assetRequest,
				reqType = RequestSpawnEffect.ReqType.REQ_FAIRYUI,
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
