using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Cs;
using Fibers;
using log4net;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEditorInternal;
using UnityEngine;

namespace SGame
{
    // 主要用于运行游戏逻辑
    public partial class GameModule
    {
        /// <summary>
        /// 游玩状态
        /// </summary>
        public enum PlayState
        {
            NORMAL          = 0, // 普通状态
            TRAVEL_ENTER    = 1, // 进入出行状态
            TRAVEL          = 2, // 出行状态
            TRAVEL_LEAVE    = 3, // 离开出行状态
        }

        private PlayState m_playerState = PlayState.NORMAL;


        private void OnTriggerTravel(bool isTravel)
        {
            if (isTravel)
                m_playerState = PlayState.TRAVEL_ENTER;
            else
                m_playerState = PlayState.TRAVEL_LEAVE;
        }

        public void InitTravel()
        {
            m_eventHandles += EventManager.Instance.Reg<bool>((int)GameEvent.TRAVEL_TRIGGER, OnTriggerTravel);
            m_playerState = PlayState.NORMAL;
        }

        IEnumerator RunTavel()
        {
            switch (m_playerState)
            {
                case PlayState.TRAVEL_ENTER:
                    yield return TravelEnter();
                    break;
                
                case PlayState.TRAVEL_LEAVE:
                    yield return TravelLeave();
                    break;
            }
        }
        
        /// <summary>
        /// 触发进入出行
        /// </summary>
        /// <returns></returns>
        IEnumerator TravelEnter()
        {
            yield return null;
            m_tileEventModule.ClearAllEvents();
            m_playerState = PlayState.TRAVEL;
        }

        /// <summary>
        /// 触发离开出行
        /// </summary>
        /// <returns></returns>
        IEnumerator TravelLeave()
        {
            yield return null;
            m_tileEventModule.ClearAllEvents();
            m_playerState = PlayState.NORMAL;
        }
             
    }
}