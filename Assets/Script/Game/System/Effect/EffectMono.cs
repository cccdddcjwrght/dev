using System;
using log4net;
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
    private static ILog log = LogManager.GetLogger("game.effect");
    
    // 对应Entity索引
    public Entity               entity;

    // 粒子特效
    private ParticleSystem      m_particle;
    
    private float               m_particleTime;

    public int effectID = 0;

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
        int e = effectID;
        if (entity != Entity.Null)
        {
			// 删除Entity
			if (World.DefaultGameObjectInjectionWorld != null)
			{
				EntityManager mgr = World.DefaultGameObjectInjectionWorld.EntityManager;
				if (mgr.Exists(entity) && !mgr.HasComponent<DespawningEntity>(entity))
				{
					mgr.AddComponent<DespawningEntity>(entity);
				}
			}
            entity = Entity.Null;
        }
    }

    void OnDespawned()
    {
        Stop();
    }
}
