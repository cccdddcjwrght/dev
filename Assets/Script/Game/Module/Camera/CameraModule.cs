using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using log4net;
using SGame;
using Unity.Entities.UniversalDelegates;
using UnityEngine;

namespace SGame
{
    public class CameraModule : IModule
    {
        //private EventHandleContainer m_handers;
        private static ILog         log = LogManager.GetLogger("xl.game.camera");

        private List<CinemachineVirtualCamera> m_vierutalCamers;
        
        public CameraModule()
        {
            //m_handers = new EventHandleContainer();
            //m_handers += EventManager.Instance.Reg<bool>((int)GameEvent.TRAVEL_TRIGGER, OnTarvelTrigger);
            InitCamera();
        }

        /// <summary>
        /// 获得指定摄像机
        /// </summary>
        /// <param name="cameraType"></param>
        /// <returns></returns>
        public CinemachineVirtualCamera GetCamera(CameraType cameraType)
        {
            return m_vierutalCamers[(int)cameraType];
        }

        void InitCamera()
        {
            m_vierutalCamers = new List<CinemachineVirtualCamera>((int)CameraType.MAX);
            for (int i = 0; i < (int)CameraType.MAX; i++)
                m_vierutalCamers.Add(null);
            
            CameraTag[] cameras = GameObject.FindObjectsOfType<CameraTag>();
            foreach (var c in cameras)
            {
                int index = (int)c.cameraType;
                if (m_vierutalCamers[index] != null)
                {
                    // 配置的摄像机重复了!
                    log.Error("virual camera repeate=" + c.cameraType);
                    continue;
                }
                
                m_vierutalCamers[index] = c.GetComponent<CinemachineVirtualCamera>();
                if (m_vierutalCamers[index] == null)
                {
                    log.Error("virual camera not found=" + c.cameraType);
                }
            }

            ChangeCamera(false);
        }

        /// <summary>
        /// 切换某个摄像机
        /// </summary>
        /// <param name="cameraType"></param>
        public void SwitchCamera(CameraType cameraType)
        {
            // 重置其他摄像机的优先级
            for (int i = 0; i < m_vierutalCamers.Count; i++)
            {
                if (m_vierutalCamers[i] != null)
                {
                    m_vierutalCamers[i].Priority = i;
                }
            }

            // 将激活的摄像机优先级调到最高的
            m_vierutalCamers[(int)cameraType].Priority = m_vierutalCamers.Count;
        }

        /// <summary>
        /// 老的的接口
        /// </summary>
        /// <param name="isTravel"></param>
        void ChangeCamera(bool isTravel)
        {
            SwitchCamera(isTravel ? CameraType.TRAVEL_MAP : CameraType.BASE_MAP);
        }

        /// <summary>
        /// 出行事件
        /// </summary>
        /// <param name="isTravel"></param>
        //void OnTarvelTrigger(bool isTravel)
        //{
        //    ChangeCamera(isTravel);
        //}
        
        public void Shutdown()
        {

        }

        public void Update()
        {
            
        }
        
    }
}