using System.Collections;
using System.Collections.Generic;
using SGame.UI;
using UnityEngine;
using Unity.Entities;
using GameConfigs;
namespace SGame
{
    // 用于运行登录逻辑
    public partial class GameModule
    {
        void LogicInit()
        {
            
        }

        // 查找骰子配置
        static bool FindBetData(int diceNum, out BetRowData data)
        {
            return ConfigSystem.Instance.TryGet((BetRowData d) => diceNum >= d.DiceRange(0) && diceNum <= d.DiceRange(1)
                , out data);
        }

        void UpdateDicePower()
        {
            UserSetting setting = DataCenter.Instance.GetUserSetting();
            int diceNum = (int)m_userData.GetNum((int)UserType.DICE_POWER);
            if (setting.doubleBonus < diceNum)
            {
                OnChangeDicePower();
            }
        }

        // 更改骰子数逻辑
        void OnChangeDicePower()
        {
            UserSetting setting = DataCenter.Instance.GetUserSetting();
            int settingDiceNum = setting.doubleBonus;
            int diceNum = (int)m_userData.GetNum((int)UserType.DICE_POWER);
            if  (FindBetData(diceNum, out BetRowData data) == false)
            {
                log.Error("Dice Num Config Not Found=" + diceNum.ToString());
                setting.doubleBonus = 1;
                DataCenter.Instance.SetUserSetting(setting);
                return;
            }

            // 找到当前的索引
            int i = 0;
            for (i = 0; i < data.BetLength; i++)
            {
                if (data.Bet(i) == setting.doubleBonus)
                    break;
            }
            if (i >= data.BetLength)
            {
                log.Error("Dice Config Error id=" + data.Id + " diceNum=" + diceNum.ToString());
                return;
            }

            int index = (i + 1) % data.BetLength;
            int newNum = data.Bet(index);
            setting.maxBonus    = data.Bet(data.BetLength - 1);
            if (newNum <= diceNum)
            {
                setting.doubleBonus = newNum;
            }
            else
            {
                setting.doubleBonus = data.Bet(0);
            }
            DataCenter.Instance.SetUserSetting(setting);
        }
    }
}