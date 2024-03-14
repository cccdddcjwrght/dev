using System;
using UnityEngine;
using Unity.Entities;

// Effect 的 Mono 对象
public interface IEffectOperator
{
    // 特效播放
    void Play();
    
    // 特效关闭
    void Stop();

    // 是否正在播放中
    bool isPlaying { get; }
    
    // 特效持续时间
    float duration { get; }
}

public class EffectMono : MonoBehaviour, IEffectOperator
{
    // 预制请求
    public libx.AssetRequest    prefabRequest;
    
    // 对应Entity索引
    public Entity               entity;

    // 粒子特效
    private ParticleSystem      m_particle;
    
    private float               m_particleTime;

    public float                duration
    {
        get
        {
            return GetParticleTime();
        }
    }

    private void Awake()
    {
        m_particle = GetComponentInChildren<ParticleSystem>();
    }

    public void Play()
    {
        if (m_particle)
            m_particle.Play();
    }

    public void Stop()
    {
        if (m_particle)
            m_particle.Stop();
    }

    public bool isPlaying { get { return m_particle.isPlaying; } }


    float GetParticleTime()
    {
        return m_particle.main.duration;
    }

    private void OnDestroy()
    {
        if (prefabRequest != null)
        {
            prefabRequest.Release();
            prefabRequest = null;
        }

        if (entity != Entity.Null)
        {
			// 删除Entity
			if (World.DefaultGameObjectInjectionWorld != null)
			{
				EntityManager mgr = World.DefaultGameObjectInjectionWorld.EntityManager;
				if (mgr.Exists(entity))
				{
					mgr.AddComponent<DespawningEntity>(entity);
				}
			}
            entity = Entity.Null;
        }
    }
}
