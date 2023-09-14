using System.Collections;
using Cinemachine;
using Fibers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using System.Collections.Generic;

namespace SGame
{
    public class TravelAnimation : MonoBehaviour
    {
        public   AnimationData   flyData;
        public   AnimationData   landData;
        private  Fiber            m_fiber = new Fiber(FiberBucket.Manual) ;

        IEnumerator PlayAnim(AnimationData data)
        {
            data.Play();
            yield return new Fiber.OnExit(() =>
            {
                data.Stop();
            });

            if (data.triggers != null)
            {
                foreach (var t in data.triggers)
                {
                    yield return FiberHelper.Wait(t.time);
                    t.trigger?.Invoke();
                }
            }
            
            while (data.timeline.state == PlayState.Playing)
            {
                yield return null;
            }
        }

        /// <summary>
        /// 播放动画
        /// </summary>
        /// <param name="t"></param>
        public void Play(FlyType t)
        {
            AnimationData data = t == FlyType.FLY ? flyData : landData;
            m_fiber.Start(PlayAnim(data), true);
        }

        void Update()
        {
            if (!m_fiber.IsTerminated)
                m_fiber.Step();
        }

        public bool isPlaying
        {
            get { return !m_fiber.IsTerminated; }
        }

        /// <summary>
        /// 停止动画
        /// </summary>
        public void Stop()
        {
            m_fiber.Terminate();
        }


        /// <summary>
        /// 触发数据
        /// </summary>
        [System.Serializable]
        public struct TriggerData
        {
            public float                    time;
            public UnityEvent               trigger;
        }
        
        /// <summary>
        /// 动画数据
        /// </summary>
        [System.Serializable]
        public struct AnimationData
        {
            public GameObject               gameObject;
            public PlayableDirector         timeline;
            public List<TriggerData>        triggers;
            public UnityEvent               endTrigger;

            public void Play()
            {
                gameObject.SetActive(true);
                timeline.Play();
            }
            
            public void Stop()
            {
                gameObject.SetActive(false);
                timeline.Stop();
                endTrigger?.Invoke();
            }
        }
        
        /// <summary>
        /// 飞行类型
        /// </summary>
        public enum FlyType
        {
            FLY     = 0, // 起飞
            LAND    = 1, // 降落
        }
    }
}