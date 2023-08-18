using System.Collections;
using System.Collections.Generic;
using Cs;
using SGame.UI;
using UnityEngine;
using Unity.Entities;
using GameConfigs;
namespace SGame
{
    // 用于运行登录逻辑
    public partial class GameModule
    {
        private const string COIN_DEFAULT   = "coin_default";
        private const string DICE_DEFAULT   = "dice_default";
        private const string DICE_LIMIT     = "dice_limit";
        private const string DICE_ADD_TIME  = "dice_add_time";
        private const string DICE_ADD_NUM   = "dice_add_num";
        private const string SERVER_ADDRESS = "address";
        private const string SERVER_PORT    = "port";

        // 设置默认值
        private void SetupDefault()
        {
            m_userData.SetNum((int)UserType.GOLD,               GlobalDesginConfig.GetInt(COIN_DEFAULT));
            m_userData.SetNum((int)UserType.DICE_POWER,         GlobalDesginConfig.GetInt(DICE_DEFAULT));
            m_userData.SetNum((int)UserType.DICE_MAXPOWER,      GlobalDesginConfig.GetInt(DICE_LIMIT));
            
            // 创建恢复骰子对象
            var recover = EntityManager.CreateEntity(typeof(DiceRecover), typeof(TimeoutData));
            var dice_add_time = (float)GlobalDesginConfig.GetInt(DICE_ADD_TIME);
            EntityManager.SetComponentData(recover, new DiceRecover {
                duration   = dice_add_time,
                recoverNum = GlobalDesginConfig.GetInt(DICE_ADD_NUM),
            });
            EntityManager.SetComponentData(recover, new TimeoutData
            {
                Value = dice_add_time
            });
            
            UpdateDicePower(true);
        }
        
        public IEnumerator RunLogin()
        {
            const float HotfixTime  = 1.0f;  // 更新UI显示时间
            const float LoadingTime = 0.5f; // 加载UI更新时间
            
            // 1. 显示更新界面
            Entity hotfixUI = UIRequest.Create(EntityManager, UIUtils.GetUI("hotfix"));
            EntityManager.AddComponentData(hotfixUI, new UIParamFloat() {Value = HotfixTime});
            
            // 2. 显示登录界面
            yield return new WaitEvent(EntityManager, GameEvent.HOTFIX_DONE);
            Entity loginUI = UIRequest.Create(EntityManager, UIUtils.GetUI("login"));
            yield return new WaitUIOpen(EntityManager, loginUI);
            UIUtils.CloseUI(EntityManager, hotfixUI);

            // 3. 等待登录事件登录
            while (true)
            {
                var     waitLogin = new WaitEvent<string>(EntityManager, GameEvent.ENTER_LOGIN);
                yield return waitLogin;
                
                string  ip = GlobalConfig.GetStr(SERVER_ADDRESS);
                int     port = GlobalConfig.GetInt(SERVER_PORT);
                log.Info("Login UserName=" + waitLogin.m_Value + " server ip=" + ip + " port=" + port.ToString());
                var     connectHandle = NetworkManager.Instance.GetClient().Connect(ip, port, 1000);
                yield return connectHandle;
                
                if (!string.IsNullOrEmpty(connectHandle.error))
                {
                    log.Error("connect fail=" + connectHandle.error);
                    yield return null;
                    continue;
                }

                // 成功登录
                log.Info("connect success name=" + waitLogin.m_Value);
                
                // 登录
                // 设置游戏数据
                Login userinfo = new Login()
                {
                    Request = new Login.Types.Request()
                    {
                        Username =  waitLogin.m_Value
                    }
                };
                userinfo.Send((int)GameMsgID.CsLogin);

                var waitMessage = new WaitMessage<Login>((int)GameMsgID.CsLogin);
                yield return waitMessage;
                if (waitMessage.Value.Response.Err != ErrorCode.ErrorSuccess)
                {
                    log.Error("Login Fail=" + waitMessage.Value.Response.Err.ToString());
                    continue;
                }
                //log.Info("Login Success login id=" +  waitMessage.Value.Response.Playerid.ToString());
                break;
            }

            SetupDefault();

            // 登录成功后显示加载UI
            Entity loadingUI = UIRequest.Create(EntityManager, UIUtils.GetUI("loading"));
            EntityManager.AddComponentData(loadingUI, new UIParamFloat() {Value = LoadingTime});
            yield return new WaitUIOpen(EntityManager, loadingUI);
            UIUtils.CloseUI(EntityManager, loginUI);

            // 4. 完成后直接进入主界
            yield return new WaitEvent(EntityManager, GameEvent.ENTER_GAME);
            Entity mainUI = UIRequest.Create(EntityManager, UIUtils.GetUI("mainui"));
            UIUtils.CloseUI(EntityManager, loadingUI);
            

            yield return null;
        }
    }
}