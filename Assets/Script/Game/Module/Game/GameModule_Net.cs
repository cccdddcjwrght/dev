using System.Collections;
using System.Collections.Generic;
using SGame.UI;
using UnityEngine;
using Unity.Entities;
using GameConfigs;
using Cs;
namespace SGame
{
    // 用于运行登录逻辑
    public partial class GameModule
    {
        void NetInit()
        {
            var client = NetworkManager.Instance.GetClient();
            m_eventHandles += client.RegMessage((int)GameMsgID.CsDiceCommitPos, OnMsg_DiceCommitPosResponse);
        }

        /// <summary>
        /// 同步事件
        /// </summary>
        /// <param name="eventId"></param>
        void SendCommitEvent(int eventId)
        {
            var commitPos = new Cs.DiceCommitPos()
            {
                Request = new DiceCommitPos.Types.Request()
                {
                    Id = eventId,
                    Travel = false,
                }
            }.Send((int)GameMsgID.CsDiceCommitPos);
        }

        /// <summary>
        /// 请求下一次的事件池
        /// </summary>
        void SendGetNextEventPool(int pos, int map_id)
        {
            var req = new DiceEventPool
            {
                Request = new DiceEventPool.Types.Request
                {
                    Pos = pos,
                    MapId = map_id,
                }
            };
            req.Send((int)GameMsgID.CsDiceEventPool);
        }

        void OnMsg_DiceCommitPosResponse(GamePackage message)
        {
            var d = message.data;
            var Value = Protocol.Deserialize<DiceCommitPos>(d.data, d.start_pos, d.len);
            if (Value.Response.Code == ErrorCode.ErrorSuccess)
            {
                log.Info("DiceCommitPos Commit Success");
            }
            else
            {
                log.Error("DiceCommitPos Recive Fail=" + Value.Response.Code.ToString());
            }
        }

        /// <summary>
        /// 接受更新事件
        /// </summary>
        /// <param name="message"></param>
        void OnMsg_DiceEventUpdate(GamePackage message)
        {
            var d = message.data;
            var response = Protocol.Deserialize<DiceEventPool>(d.data, d.start_pos, d.len).Response;
            if (response.Code != ErrorCode.ErrorSuccess)
            {
                log.Error("DiceCommitPos Recive Fail=" + response.Code.ToString());
                return;
            }
            
            // 添加新的事件
            foreach (var e in response.StepList)
            {
                m_tileEventModule.AddEventGroup(CovertNetEventToRound(e));
            }
        }
    }
}