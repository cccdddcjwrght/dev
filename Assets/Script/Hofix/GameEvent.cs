using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame.Hotfix
{
    public enum GameEvent : int
    {
        HOTFIX_DONE          = 100, // 热更新结束
        LOGIN_READLY         = 101, // 登录准备好了
    }
}