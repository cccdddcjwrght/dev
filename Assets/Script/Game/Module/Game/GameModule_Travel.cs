using System.Collections;
using Cs;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using SGame.UI;

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
                    EventManager.Instance.Trigger((int)GameEvent.TRAVEL_START, true);
                    yield return TravelEnter();
                    EventManager.Instance.Trigger((int)GameEvent.TRAVEL_END, true);
                    break;
                
                case PlayState.TRAVEL_LEAVE:
                    EventManager.Instance.Trigger((int)GameEvent.TRAVEL_START, false);
                    yield return TravelLeave();
                    EventManager.Instance.Trigger((int)GameEvent.TRAVEL_END, false);
                    break;
            }
        }
        
        /// <summary>
        /// 触发进入出行
        /// </summary>
        /// <returns></returns>
        IEnumerator TravelEnter()
        {
            m_cameraModule.SwitchCamera(CameraType.TRAVEL_MAP);
            var playerId = m_userData.GetNum((int)UserType.TRAVEL_PLAYERID);
            if (playerId == 0)
            {
                log.Error("TRAVEL PLAYER ID IS ZERO");
                UIUtils.ShowTips(EntityManager, "TRAVEL PLAYER ID IS ZERO", new float3(8.22000027f, -1.13f, 5.09000015f), Color.red, 50, 2.0f);
                yield break;
            }

            yield return null;
            m_tileEventModule.ClearAllEvents();
            // 清空出行金币
            m_userData.SetNum((int)UserType.TRAVEL_GOLD, 0);
            m_userData.SetNum((int)UserType.TRAVEL, 1);
            m_userData.SetNum((int)UserType.TRAVEL_DICE_POWER, m_userData.GetNum((int)UserType.DICE_POWER));
            
            // 1. 显示更新界面
            Entity ui = UIRequest.Create(EntityManager, UIUtils.GetUI("travelenter"));
            
            // 2. 发送出行数据
            SendTravelData(playerId);
            var waitMessage = new WaitMessage<TravelPlayerInfo>((int)GameMsgID.CsTravelPlayerInfo);
            yield return waitMessage;
            if (waitMessage.IsTimeOut) {
                log.Error("TravelPlayerInfo Message Time Out");
            }
            if (waitMessage.Value == null || waitMessage.Value.Response == null)
                log.Error("TravelPlayerInfo recive is null");
            if (waitMessage.Value.Response.Code != ErrorCode.ErrorSuccess)
                log.Error("TravelPlayerInfo Errorcode = "+ waitMessage.Value.Response.Code);
            
            // 得到列表数据， 更新列表事件
            int mapId       = 1;
            int pos         = 0;
            if (waitMessage.Value != null && waitMessage.Value.Response != null)
            {
                int i = 0;
                var response = waitMessage.Value.Response;
                foreach (var e in response.StepList)
                {
                    m_tileEventModule.AddEventGroup(CovertNetEventToRound(e));
                }
                mapId   = response.MapId;
                pos     = response.Pos;
            }

            // 加载地图
            m_tileModule.LoadMap(mapId, MapType.TRVAL);

            // 玩家移动目标位置
            MovePlayerToPosition(pos);
            
            // 更新骰子位置
            UpdateDicePosition();
            
            // 等待1秒
            yield return FiberHelper.Wait(3.0f);

            m_cameraModule.SwitchCamera(CameraType.PLAYER);

            // 关闭UI
            UIUtils.CloseUI(EntityManager, ui);
            m_playerState = PlayState.TRAVEL;
        }

        /// <summary>
        /// 触发离开出行
        /// </summary>
        /// <returns></returns>
        IEnumerator TravelLeave()
        {
            m_cameraModule.SwitchCamera(CameraType.BASE_MAP);
            yield return null;
            m_tileEventModule.ClearAllEvents();

            // 1. 显示更新界面
            Entity ui = UIRequest.Create(EntityManager, UIUtils.GetUI("travelleave"));

            // 2. 发送出行数据
            var req = new DiceEventPool()
            {
                Request = new DiceEventPool.Types.Request()
                {
                    Pos = 0,
                    MapId = 0,
                }
            }.Send((int)GameMsgID.CsDiceEventPool);
            var waitMessage = new WaitMessage<DiceEventPool>((int)GameMsgID.CsDiceEventPool);
            yield return waitMessage;
            if (waitMessage.IsTimeOut) {
                log.Error("DiceEventPool Message Time Out");
            }
            if (waitMessage.Value == null || waitMessage.Value.Response == null)
                log.Error("DiceEventPool recive is null");
            if (waitMessage.Value.Response.Code != ErrorCode.ErrorSuccess)
                log.Error("DiceEventPool Errorcode = "+ waitMessage.Value.Response.Code);
            
            // 得到列表数据， 更新列表事件
            int mapId       = 1;
            int pos         = 0;
            if (waitMessage.Value != null && waitMessage.Value.Response != null)
            {
                int i = 0;
                var response = waitMessage.Value.Response;
                foreach (var e in response.StepList)
                {
                    m_tileEventModule.AddEventGroup(CovertNetEventToRound(e));
                }
                mapId   = response.MapId;
                pos     = response.Pos;
            }

            // 加载地图
            // m_tileModule.LoadMap(mapId, MapType.TRVAL);
            TileModule.Instance.SetMapType(MapType.NORMAL);

            // 玩家移动目标位置
            MovePlayerToPosition(pos);
            
            // 更新骰子位置
            UpdateDicePosition();
            
            // 等待1秒
            yield return FiberHelper.Wait(3.0f);
            m_cameraModule.SwitchCamera(CameraType.PLAYER);
            UIUtils.CloseUI(EntityManager, ui);

            // 设置出行状态
            m_playerState = PlayState.NORMAL;
            m_userData.SetNum((int)UserType.TRAVEL, 0);
            
            // 将出行金币转换为玩家金币
            long travelGold = m_userData.GetNum((int)UserType.TRAVEL_GOLD);
            long dicePower = m_userData.GetNum((int)UserType.DICE_POWER);
            if (dicePower <= 0)
                log.Error("TraveDice Power Less Zero" + dicePower);
            
            // 添加从出行钟获得的金币
            travelGold *= dicePower;
            m_userData.AddNum((int)UserType.GOLD, travelGold);
            long newGold = m_userData.GetNum((int)UserType.GOLD);
            EventManager.Instance.Trigger((int)GameEvent.PROPERTY_GOLD, (int)travelGold, newGold, 0);
        }
             
    }
}