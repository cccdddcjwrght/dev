using System.Collections;
using System.Collections.Generic;
using Fibers;
using log4net;
using SGame.Firend;
using SGame.UI;
using UnityEngine;

namespace SGame
{
   /// <summary>
   /// 房间逻辑流程控制
   /// </summary>
   public partial class GameScript : MonoBehaviour
   {
      private static ILog log = LogManager.GetLogger("xl.game");
      
      private GameSceneVariable  m_sceneVariable;
      private Fiber              m_Logic;
      private RoomManager        m_roomManager;
      
      //public bool IsInit 
      void Start()
      {
         m_sceneVariable   = GetComponent<GameSceneVariable>();
         m_Logic           = new Fiber(GameLogic());
         m_roomManager     = new RoomManager();
      }

      IEnumerator GameLogic()
      {
         // 游戏初始化
         yield return GameStart();
         
         // 运行自动创建顾客, 制动创建船
         yield return FiberHelper.RunParallel(
            AutoCreateCustomer(),
            AutoCreateCar(),
            AutoTriggerFriendCustomer(),
            m_roomManager.Run());
      }
      
      // 房间开始流程
      IEnumerator GameStart()
      {
         /// 设置房间
         m_sceneVariable.RoomTime = GameServerTime.Instance.serverTime;
         m_sceneVariable.MaxCustomer = 1;
         
         /// 显示主界面并关闭登录界面
         var uiMain = UIModule.Instance.ShowSingleton(UIUtils.GetUI("mainui"));
         yield return new WaitUIOpen(UIModule.Instance.GetEntityManager(), uiMain);

         /// 关闭登录UI
         UIUtils.CloseUIByName("login");
         
         EventManager.Instance.AsyncTrigger((int)GameEvent.GAME_START);
         yield return WaitInitFinis();
      }

      IEnumerator WaitInitFinis()
      {
         while (m_sceneVariable.IsInit == false)
            yield return null;
      }

      // 制动创建顾客
      IEnumerator AutoCreateCustomer()
      {
         while (true)
         {
            yield return null;
            
            // 新手引导
            if (DataCenter.Instance.guideData.isStopCreate)
               continue;
            
            // 等待时间
            yield return FiberHelper.Wait(m_sceneVariable.customer_time);
            
            // 顾客最大上线判定
            if (m_sceneVariable.CurrentCustomer >= m_sceneVariable.MaxCustomer)
               continue;

            // 判断空闲桌子数量
            if (TableManager.Instance.GetEmptyChairCount(TABLE_TYPE.CUSTOM, CHAIR_TYPE.CUSTOMER) <= 0)
               continue;

            // 触发顾客创建事件
            EventManager.Instance.Trigger((int)GameEvent.ROOM_AUTOCREATE_CUSTOMER);
         }
      }

      void Update()
      {
         if (m_Logic != null)
         {
            if (!m_Logic.Step())
               m_Logic = null;
         }
      }
      
      /// <summary>
      /// 创建汽车 
      /// </summary>
      /// <returns></returns>
      IEnumerator AutoCreateCar()
      {
         CarQueueManager manager = CarQueueManager.Instance;
         while (true)
         {
            yield return FiberHelper.Wait(1.0f);
            manager.CreateNewCar();
         }
      }

      /// <summary>
      /// 自动触发好友创建标记
      /// </summary>
      /// <returns></returns>
      IEnumerator AutoTriggerFriendCustomer()
      {
         while (FriendModule.IsCustomerFriendOpened())
         {
            yield return FiberHelper.Wait(1.0f);
         }

         float waitTime = FriendModule.GetOnlineWaitTime();
         if (waitTime > 0)
            yield return FiberHelper.Wait(waitTime + 0.5f);
         
         while (true)//FriendModule.Instance.HasFriend())
         {
            yield return null;
            
            // 没有好友
            float triggerTime = FriendModule.GetFriendTriggerTime();
            if (triggerTime <= 0)
            {
               // 没有触发时长
               log.Error("GetFriendTriggerTime Not Found");
               yield break;
            }

            // 触发好友创建
            yield return FiberHelper.Wait(triggerTime);
            m_sceneVariable.FriendCustomerTrigger = true;
         }
      }
   }
}