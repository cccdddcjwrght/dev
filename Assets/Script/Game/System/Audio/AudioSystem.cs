using System;
using UnityEngine;
using Unity.Entities;
using UnityEngine.Audio;
using libx;
using System.Collections.Generic;
using GameConfigs;
using log4net;
using Unity.VisualScripting;

namespace SGame
{
	public enum SoundType : int
	{
		MASTER = 0, // 主体
		UI = 1, // UI 声音
		BACKGROUND = 2, // 背景音乐
		BACKGROUND0 = 3, // 背景1
		BACKGROUND1 = 4, // 背景2
	}

	// 声音数据
	public struct SoundData : IComponentData
	{
		// 声音类型
		public SoundType audio_type;

		// 持续时间
		public float duration;

		// 循环数量
		public int loop_num;

		// 音量大小
		public float volume;
	}

	// 声音持续时间
	public struct SoundTime : IComponentData
	{
		// 当前持续时间
		public float duration;

		// 循环次数
		public int loop;
	}

	// 声音资源
	public class SoundAsset : IComponentData
	{
		public PoolID audio;          // 声音资源ID
		public AssetRequest asset_ref;      // 声音资源索引
	}

	public struct SoundRef : IComponentData
	{
		public Entity Value;
	}

	// 声音系统
	public partial class AudioSystem : SystemBase
	{
		public const string AUDIO_RES_FORMAT = "Assets/BuildAsset/Audio/ogg/{0}.ogg";
		// 播放声音请求
		public class PlaySoundRequest : IComponentData
		{
			// 声音资源路径
			public string audio_path;

			// 声音类型
			public SoundType audio_type;

			// 持续时间
			public float duration;

			// 循环数量
			public int loop_num;

			// 音量大小
			public float volume;

			// 回调对象
			public Entity result;
		}

		// 删除标记
		public struct DespawnedTag : IComponentData
		{

		}

		private static ILog log = LogManager.GetLogger("AudioSystem");


		// 创建类型
		private EntityArchetype archCreater;
		private EntityArchetype archAudio;
		//private EndSimulationEntityCommandBufferSystem   m_commandBuffer     ;
		private AudioMixer m_audioMixer;
		private AudioMixerGroup[] m_audioMasterGroups;
		private ObjectPool<AudioSource> m_audioSourcePool; // 资源对象池      
		private List<SoundType> m_stopRequest;

		public static AudioSystem Instance
		{
			get
			{
				return World.DefaultGameObjectInjectionWorld.GetOrCreateSystemManaged<AudioSystem>();
			}
		}

		// 
		public void SetSoundVolume(string key, float v)
		{
			m_audioMixer.SetFloat(key, v);
		}

		// 
		public float GetSoundVolume(string key)
		{
			AudioMixerGroup g;

			if (m_audioMixer.GetFloat(key, out float out_value))
				return out_value;

			return 0;
		}

		protected override void OnCreate()
		{
			archCreater = EntityManager.CreateArchetype(typeof(PlaySoundRequest));
			archAudio = EntityManager.CreateArchetype(
				typeof(SoundData),
				typeof(SoundTime),
				typeof(SoundAsset));

			m_audioSourcePool = new ObjectPool<AudioSource>(NewAudioSource, SpwanAudioSource, DeSpawnAudioSource);
			//m_commandBuffer     = World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
			m_stopRequest = new List<SoundType>();
			base.OnCreate();
		}

		static AudioSource NewAudioSource()
		{
			GameObject obj = new GameObject("audio_source");
			GameObject.DontDestroyOnLoad(obj);
			return obj.AddComponent<AudioSource>();
		}

		static void SpwanAudioSource(AudioSource source)
		{
			source.gameObject.SetActive(true);
		}

		static void DeSpawnAudioSource(AudioSource source)
		{
			source.gameObject.SetActive(false);
			source.Stop();
			source.clip = null;
			source.outputAudioMixerGroup = null;
		}

		public void Initalize(AudioMixer audioMixer)
		{
			m_audioMixer = audioMixer;
			m_audioMasterGroups = m_audioMixer.FindMatchingGroups("Master");
			OnInit();
		}

		public void Stop(SoundType t)
		{
			m_stopRequest.Add(t);
		}


		private PoolID PopAudioSource()
		{
			return m_audioSourcePool.Alloc();
		}


		AudioMixerGroup GetMixerGroup(SoundType sound_type)
		{
			return m_audioMasterGroups[(int)sound_type];
		}

		// 设置声音
		SoundData SetupAudio(SoundAsset asset, PlaySoundRequest req)
		{
			SoundData data = new SoundData();
			var clip = asset.asset_ref.asset as AudioClip;
			var source_id = asset.audio;
			if (!m_audioSourcePool.TryGet(source_id, out AudioSource audioSource))
			{
				log.ErrorFormat("Not Found AudioSource id={0}", source_id.Index);
				return default;
			}

			// 设置数据
			data.audio_type = req.audio_type;
			data.duration = req.duration;
			data.loop_num = req.loop_num;
			data.volume = req.volume;

			// 播放声音
			audioSource.clip = clip;
			audioSource.loop = data.loop_num < 0; // 循环播放
			audioSource.volume = data.volume;
			audioSource.outputAudioMixerGroup = GetMixerGroup(data.audio_type);
			audioSource.Play();
			return data;
		}

		void FreeSound(SoundAsset asset)
		{
			asset.asset_ref.Release();
			m_audioSourcePool.Free(asset.audio);
		}

		protected override void OnUpdate()
		{
			if (m_audioMixer == null)
				return;

			// 处理停止请求
			if (m_stopRequest.Count != 0)
			{
				Entities.WithNone<DespawnedTag>().ForEach((Entity e,
					ref SoundData sound_data) =>
				{
					if (m_stopRequest.Contains(sound_data.audio_type))
					{
						EntityManager.AddComponent<DespawnedTag>(e);
					}
				}).WithoutBurst().WithStructuralChanges().Run();
				m_stopRequest.Clear();
			}

			// 1. 加载资源
			Entities.WithNone<SoundAsset>().ForEach((Entity e,
				PlaySoundRequest request) =>
			{
				EntityManager.AddComponent<SoundAsset>(e);
				EntityManager.SetComponentData(e, new SoundAsset()
				{
					asset_ref = Assets.LoadAssetAsync(request.audio_path, typeof(AudioClip)),
					audio = PopAudioSource()
				});
			}).WithoutBurst().WithStructuralChanges().Run();;

			// 2. 产生新的clip
			Entities.ForEach((Entity e,
				SoundAsset asset,
				PlaySoundRequest request) =>
			{
				if (asset.asset_ref.isDone)
				{
					if (string.IsNullOrEmpty(asset.asset_ref.error))
					{

						Entity newEntity = EntityManager.CreateEntity(archAudio); //buffer.CreateEntity(archAudio);
						SoundData soundData = SetupAudio(asset, request);

						EntityManager.SetComponentData(newEntity, asset);
						EntityManager.SetComponentData(newEntity, soundData);
						EntityManager.SetComponentData(newEntity, new SoundTime() { duration = 0, loop = 0 });

						// 通知引用对象
						var result = request.result;
						if (result != Entity.Null && EntityManager.Exists(result))
						{
							EntityManager.AddComponent<SoundRef>(result);
							EntityManager.SetComponentData(result, new SoundRef { Value = newEntity });
						}
					}
					else
					{
						FreeSound(asset);
					}

					EntityManager.DestroyEntity(e);
				}
			}).WithoutBurst().WithStructuralChanges().Run();;

			float time = (float)GlobalTime.deltaTime;
			//3. 时间到销毁clip
			Entities.WithNone<DespawnedTag>().ForEach((Entity e,
				SoundAsset asset,
				ref SoundTime sound_time,
				ref SoundData sound_data) =>
			{
				if (sound_data.loop_num >= 0)
				{
					sound_time.duration += time;
					if (sound_time.duration >= sound_data.duration)
					{
						sound_time.duration = 0;
						sound_time.loop += 1;
						if (sound_time.loop <= sound_data.loop_num)
						{
							// 播放声音
							if (m_audioSourcePool.TryGet(asset.audio, out AudioSource audio_source))
							{
								audio_source.Play();
							}
						}
						else
						{
							EntityManager.AddComponent<DespawnedTag>(e);
						}
					}
				}
			}).WithoutBurst().WithStructuralChanges().Run();;

			// 销毁声音
			Entities.WithAll<DespawnedTag>().ForEach((Entity entity, SoundAsset asset) =>
			{
				FreeSound(asset);
				EntityManager.DestroyEntity(entity);
			}).WithoutBurst().WithStructuralChanges().Run();;
		}

		partial void OnInit();
		#region UserAPI

		/// <summary>
		/// 获得真实的资源路径
		/// </summary>
		/// <param name="name">配置表中的名字</param>
		/// <returns><return>
		static string GetResourcePath(string name)
		{
			return string.Format(AUDIO_RES_FORMAT, name);
		}

		/// <summary>
		/// 将配置表的声音类型改为系统的
		/// </summary>
		/// <param name="conf_type"></param>
		/// <returns></returns>
		static SoundType GetSoundType(int conf_type)
		{
			return (SoundType)(conf_type);
		}

		/// <summary>
		/// 通告配置表播放声音
		/// </summary>
		/// <param name="conf_id"></param>
		public PlaySoundRequest Play(int conf_id)
		{
			if (!ConfigSystem.Instance.TryGet(conf_id, out Sound_EffectRowData conf))
			{
				log.ErrorFormat("Load Sound Config Fail Id ={0}", conf_id);
				return null;
			}

			PlaySoundRequest req = new PlaySoundRequest()
			{
				audio_path = GetResourcePath(conf.Name),
				audio_type = GetSoundType(conf.Type),
				duration = conf.Time,
				loop_num = conf.Loop,
				volume = 1.0f,
				result = Entity.Null
			};
			//log.Info("Play Audio Res=" + req.audio_path);
			//背景音乐播放前先关闭之前的
			if(req.audio_type >= SoundType.BACKGROUND && req.audio_type <= SoundType.BACKGROUND1)
				Stop(req.audio_type);

			Play(req);

			return req;
		}

		/// <summary>
		/// 播放声音
		/// </summary>
		/// <param name="t">声音类型</param>
		/// <param name="duration">持续时间</param>
		/// <param name="loop">是否循环播放</param>
		/// <returns></returns>
		public PlaySoundRequest Play(SoundType t, string sound_res, float duration, int loop)
		{
			PlaySoundRequest req = new PlaySoundRequest
			{
				audio_path = sound_res,
				audio_type = t,
				duration = duration,
				result = Entity.Null,
				loop_num = loop,
				volume = 1.0f
			};

			//req.audio_path.Dispose();
			Play(req);
			return req;
		}

		/// <summary>
		/// 播放声音
		/// </summary>
		/// <param name="req"></param>
		/// <param name="buffer"></param>
		public void Play(PlaySoundRequest req, EntityCommandBuffer buffer)
		{
			var e = buffer.CreateEntity(archCreater);
			buffer.SetComponent(e, req);
		}

		/// <summary>
		/// 播放声音
		/// </summary>
		/// <param name="req">播放声音请求</param>
		public void Play(PlaySoundRequest req)
		{
			var e = EntityManager.CreateEntity(archCreater);
			EntityManager.SetComponentData(e, req);
		}

		#endregion
	}

	public static class AudioSystemExtend
	{
		public struct AudioID
		{
			public int id;

			static public implicit operator AudioID(int id)
			{
				return new AudioID() { id = id };

			}


		}
		public static AudioID ToAudioID(this int id) => id;

		static public void PlayAudio(this AudioID id)
		{
			AudioSystem.Instance.Play(id.id);
		}


	}
}