using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using SGame.UI;
using UnityEngine;
using Unity.Entities;
using GameConfigs;
using Unity.Mathematics;
using Unity.Transforms;

namespace SGame
{
    // 用于运行登录逻辑
    public partial class GameModule
    {
        void InitLogic()
        {
            
        }

        // 查找骰子配置
        static bool FindBetData(int diceNum, out BetRowData data)
        {
            return ConfigSystem.Instance.TryGet((BetRowData d) => diceNum >= d.DiceRange(0) && diceNum < d.DiceRange(1)
                , out data);
        }

        void UpdateDicePower(bool isForce = false)
        {
            if (isForce)
            {
                ChangeDicePower(false);
                return;
            }
            
            UserSetting setting = DataCenter.Instance.GetUserSetting();
            int diceNum = (int)m_userData.GetNum((int)UserType.DICE_NUM);
            if (setting.power < diceNum)
            {
                OnChangeDicePower();
            }
        }

        void ChangeDicePower(bool isNext)
        {
            UserSetting setting = DataCenter.Instance.GetUserSetting();
            int settingDiceNum = setting.power;
            int diceNum = (int)m_userData.GetNum((int)UserType.DICE_NUM);
            if  (FindBetData(diceNum, out BetRowData data) == false)
            {
                log.Error("Dice Num Config Not Found=" + diceNum.ToString());
                setting.power = 1;
                DataCenter.Instance.SetUserSetting(setting);
                return;
            }

            // 找到当前的索引
            int i = 0;
            for (i = 0; i < data.BetLength; i++)
            {
                if (data.Bet(i) == setting.power)
                    break;
            }
            if (i >= data.BetLength)
            {
                log.Error("Dice Config Error id=" + data.Id + " diceNum=" + diceNum.ToString());
                return;
            }

            // 是否取下一个, 否则就是强制刷新
            int index = 0;
            if (isNext)
                index = (i + 1) % data.BetLength;
            else
                index = i;

            int newNum          = data.Bet(index);
            setting.maxPower    = data.Bet(data.BetLength - 1);
            setting.power = newNum;
            DataCenter.Instance.SetUserSetting(setting);
        }

        // 更改骰子数逻辑
        void OnChangeDicePower()
        {
            ChangeDicePower(true);
        }

        /// <summary>
        /// 将PLAYER 移动到 目标点
        /// </summary>
        /// <param name="pos"></param>
        void MovePlayerToPosition(int pos)
        {
            m_currentPlayerPos = pos;
            float3 position3d  = m_tileModule.current.GetTileDataFromPos(m_currentPlayerPos).Position3d;
            CharacterMover mover = EntityManager.GetComponentObject<CharacterMover>(m_player);
            if (mover.m_controller != null)
            {
                mover.m_controller.transform.position = position3d;
            }
            else
            {
                EntityManager.SetComponentData(m_player, new Translation{Value = position3d});
                log.Error("controller not readly!");
            }

            Character character = EntityManager.GetComponentObject<Character>(m_player);
            character.titleId = m_currentPlayerPos;
        }
        
        /// <summary>
        /// 设置跟随角色的摄像机
        /// </summary>
        /// <returns></returns>
        IEnumerator PlayerCameraSetup()
        {
            // 等待 角色创建
            while (!m_characterModule.IsReadly(m_player))
                yield return null;
            
            var camera = m_cameraModule.GetCamera(CameraType.PLAYER);
            var mover = EntityManager.GetComponentObject<CharacterMover>(m_player);
            var playerGameObject = mover.m_controller.gameObject;
            
            // 绑定摄像机
            camera.Follow = playerGameObject.transform;
            //camera.LookAt = playerGameObject.transform;
            
            m_cameraModule.SwitchCamera(CameraType.PLAYER);
        }
    }
}