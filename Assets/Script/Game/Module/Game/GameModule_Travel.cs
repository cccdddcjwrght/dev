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

        void ResetUserSetting()
        {
            var setting = DataCenter.Instance.GetUserSetting();
            setting.autoUse = false;
            DataCenter.Instance.SetUserSetting(setting);
        }

        IEnumerator RunTavel()
        {
            switch (m_playerState)
            {
                case PlayState.TRAVEL_ENTER:
                    EventManager.Instance.Trigger((int)GameEvent.TRAVEL_START, true);
                    yield return TravelEnter();
                    ResetUserSetting();
                    EventManager.Instance.Trigger((int)GameEvent.TRAVEL_END, true);
                    break;
                
                case PlayState.TRAVEL_LEAVE:
                    EventManager.Instance.Trigger((int)GameEvent.TRAVEL_START, false);
                    yield return TravelLeave();
                    ResetUserSetting();
                    EventManager.Instance.Trigger((int)GameEvent.TRAVEL_END, false);
                    break;
            }
        }

        public TravelAnimation GetTravelEffect(MapType mapType)
        {
            TravelAnimation[] anims = GameObject.FindObjectsOfType<TravelAnimation>();
            string tag = mapType == MapType.NORMAL ? "normal" : "travel";
            foreach (var t in anims)
            {
                if (t.tag.Equals(tag))
                    return t;
            }

            return null;
        }
        
        /// <summary>
        /// 触发进入出行
        /// </summary>
        /// <returns></returns>
        IEnumerator TravelEnter()
        {
            var playerId = m_userData.GetNum((int)UserType.TRAVEL_PLAYERID);
            if (playerId == 0)
            {
                log.Error("TRAVEL PLAYER ID IS ZERO");
                UIUtils.ShowTips(EntityManager, "TRAVEL PLAYER ID IS ZERO", new float3(0, 0, 0), Color.red, 50, 2.0f, PositionType.POS2D_CENTER);
                yield break;
            }

            yield return null;
            m_tileEventModule.ClearAllEvents();
            ShowDice(false);

            // 清空出行金币   
            m_userData.SetNum((int)UserType.TRAVEL_GOLD, 0);
            m_userData.SetNum((int)UserType.TRAVEL, 1);
            m_userData.SetNum((int)UserType.TRAVEL_DICE_POWER, m_userData.GetNum((int)UserType.DICE_POWER));
            
            TravelAnimation travelAnimation = GetTravelEffect(MapType.NORMAL);
            
            // 播放飞行
            travelAnimation.Play(TravelAnimation.FlyType.FLY);

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

            // 等待动画播放出来
            yield return FiberHelper.Wait(10.0f);

            Time.timeScale = 0.1f;
            
            // 1. 显示更新界面
            Entity ui = UIRequest.Create(EntityManager, UIUtils.GetUI("travelenter"));
            
            // 玩家移动目标位置
            MovePlayerToPosition(pos);
            yield return FiberHelper.Wait(0.5f);

            // 关闭UI
            UIUtils.CloseUI(EntityManager, ui);
            m_playerState = PlayState.TRAVEL;
            Time.timeScale = 1.0f;
            
            // 播放鸟回来
            travelAnimation.Stop();
            TravelAnimation travelAnimation2 = GetTravelEffect(MapType.TRVAL);
            m_cameraModule.SwitchCamera(CameraType.PLAYER_TRAVEL);
            travelAnimation2.Play(TravelAnimation.FlyType.LAND);
            
            // 停止动画, 显示骰子
            yield return FiberHelper.Wait(6.0f);
            travelAnimation2.Stop();
            ShowDice(true);
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
            
            TravelAnimation travelAnimation = GetTravelEffect(MapType.TRVAL);
            travelAnimation.Play(TravelAnimation.FlyType.FLY);
            
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
            ShowDice(false);

            yield return FiberHelper.Wait(8.0f);

            // 1. 显示更新界面
            Entity ui = UIRequest.Create(EntityManager, UIUtils.GetUI("travelleave"));
            Time.timeScale = 0.1f;
            m_cameraModule.SwitchCamera(CameraType.PLAYER);

            // 设置出行状态
            m_playerState = PlayState.NORMAL;
            m_userData.SetNum((int)UserType.TRAVEL, 0);
            
            // 更新骰子位置
            ShowDice(true);
            
            // 将出行金币转换为玩家金币
            long travelGold = m_userData.GetNum((int)UserType.TRAVEL_GOLD);
            long dicePower = m_userData.GetNum((int)UserType.DICE_POWER);
            if (dicePower <= 0)
                log.Error("TraveDice Power Less Zero" + dicePower);
            
            // 等待1秒结束
            yield return FiberHelper.Wait(0.5f);
            UIUtils.CloseUI(EntityManager, ui);
            travelAnimation.Stop();
            Time.timeScale = 1.0f;
            
            // 添加从出行钟获得的金币
            travelGold *= dicePower;
            m_userData.AddNum((int)UserType.GOLD, travelGold);
            long newGold = m_userData.GetNum((int)UserType.GOLD);
            EventManager.Instance.Trigger((int)GameEvent.PROPERTY_GOLD, (int)travelGold, newGold, 0);
            
            // 播放鸟回来
            travelAnimation.Stop();
            TravelAnimation travelAnimation2 = GetTravelEffect(MapType.NORMAL);
            m_cameraModule.SwitchCamera(CameraType.PLAYER);
            travelAnimation2.Play(TravelAnimation.FlyType.LAND);
            
            // 停止动画, 显示骰子
            yield return FiberHelper.Wait(6.0f);
            travelAnimation2.Stop();
            ShowDice(true);
        }
             
    }
}