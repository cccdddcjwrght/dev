using System.Collections;
using System.Collections.Generic;
using SGame;
using UnityEngine;

namespace SGame
{
    public class CameraModule : IModule
    {
        private EventHandleContainer m_handers;
        private GameObject           m_vcNormal;
        private GameObject           m_vcTravel;
        public CameraModule()
        {
            m_handers = new EventHandleContainer();
            m_handers += EventManager.Instance.Reg<bool>((int)GameEvent.TRAVEL_TRIGGER, OnTarvelTrigger);
            InitCamera();
        }

        void InitCamera()
        {
            CameraTag[] cameras = GameObject.FindObjectsOfType<CameraTag>();
            foreach (var c in cameras)
            {
                if (c.mapType == MapType.NORMAL)
                {
                    m_vcNormal = c.gameObject;
                }
                else if (c.mapType == MapType.TRVAL)
                {
                    m_vcTravel = c.gameObject;
                }
            }

            ChangeCamera(false);
        }

        public void ChangeCamera(bool isTravel)
        {
            m_vcNormal?.SetActive(!isTravel);
            m_vcTravel?.SetActive(isTravel);
        }

        void OnTarvelTrigger(bool isTravel)
        {
            ChangeCamera(isTravel);
        }
        
        public void Shutdown()
        {

        }
    }
}