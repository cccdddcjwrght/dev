using System.Collections;
using System.Collections.Generic;
using Fibers;
using SGame.UI;
using UnityEngine;

namespace SGame
{
   /// <summary>
   /// 房间逻辑流程控制
   /// </summary>
   public class GameScript : MonoBehaviour
   {
      private GameSceneVariable  m_sceneVariable;
      private Fiber              m_Logic;
      
      //public bool IsInit 
      void Start()
      {
         m_sceneVariable   = GetComponent<GameSceneVariable>();
         m_Logic           = new Fiber(GameLogic());
      }

      IEnumerator GameLogic()
      {
         // 游戏初始化
         yield return GameStart();
         
         // 运行自动创建顾客, 制动创建船
         yield return FiberHelper.RunParallel(
            AutoCreateCustomer(),
            AutoCreateCar());
      }
      
      // 房间开始流程
      IEnumerator GameStart()
      {
         /// 设置房间
         m_sceneVariable.RoomTime = GameServerTime.Instance.serverTime;
         m_sceneVariable.MaxCustomer = 0;
         
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
         yield return null;
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
   }
}