using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame
{
    /// <summary>
    /// 场景数据
    /// </summary>
   public class GameSceneVariable : MonoBehaviour
   {
       public static GameSceneVariable Instance;

       private void Awake()
       {
           Instance = this;
       }

       /// <summary>
       /// 场景是否初始化了
       /// </summary>
       public bool      IsInit = false;

       /// <summary>
       /// 最大顾客数量
       /// </summary>
       public int       MaxCustomer = 1;

       /// <summary>
       /// 当前顾客数量
       /// </summary>
       public int       CurrentCustomer = 0;

       /// <summary>
       /// 顾客创建时间间隔
       /// </summary>
       public float     customer_time = 0.5f;

       /// <summary>
       /// 房间开始时间
       /// </summary>
       public int       RoomTime = 0;

       /// <summary>
       /// 是否触发好友创建
       /// </summary>
       public bool      FriendCustomerTrigger = false;

       /// <summary>
       /// 获得在线时长
       /// </summary>
       public int OnlineTime => GameServerTime.Instance.serverTime - RoomTime;
   }
}