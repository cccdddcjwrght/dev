using System;
using System.Collections.Generic;

namespace ThirdSdk
{
    //第三方库事件类型
    public enum THIRD_EVENT_TYPE
    {
        TET_FIREBASE_INIT_COMPLETE,                     //firebase初始化完成事件
        TET_FIREBASE_INIT_FAILURE,                      //firebase初始化失败
        TET_FB_INIT_COMPLETE,                           //fb初始化完成事件
        TET_FB_DEEP_LINK_URL,                           //fb深度链接内容通知事件
        TET_THIRD_SDK_INIT_COMPLETE,                    //第三方初始化完成
    }
}
