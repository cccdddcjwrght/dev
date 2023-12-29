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

        private LoaderUserInfo.Types.Response m_userInfo;

        // 设置默认值
        private void SetupDefault()
        {
            m_currentPlayerPos = m_userInfo.Pos;
            
            m_userData.SetNum((int)UserType.GOLD,               m_userInfo.Coin);
            m_userData.SetNum((int)UserType.DICE_NUM,         m_userInfo.Dice);
            m_userData.SetNum((int)UserType.DICE_MAXNUM,      m_userInfo.DiceMax);
            m_userData.SetNum((int)UserType.POS,                m_userInfo.Pos);

            // 创建恢复骰子对象
            var recover = EntityManager.CreateEntity(typeof(DiceRecover), typeof(TimeoutData));
            var dice_add_time = (float)GlobalDesginConfig.GetInt(DICE_ADD_TIME);
            EntityManager.SetComponentData(recover, new DiceRecover {
                duration   = dice_add_time,
                recoverNum = GlobalDesginConfig.GetInt(DICE_ADD_NUM),
            });
            EntityManager.SetComponentData(recover, new TimeoutData { Value = dice_add_time });

            // 创建事件
            int i = 0;
            foreach (var e in m_userInfo.StepList)
            {
                m_tileEventModule.AddEventGroup(CovertNetEventToRound(e));
            }

            UpdateDicePower(true);
            
            // 加载地图
            TileModule.Instance.LoadMap(m_userInfo.MapId, MapType.NORMAL);
        }

        static RoundData CovertNetEventToRound(DiceEvent diceEvent)
        {
            if (diceEvent.Data == null)
            {
                log.Error("round event is null " + diceEvent.Id);
            }
            if (diceEvent.Dice == 0 || diceEvent.Dice1 == 0 || diceEvent.Dice2 == 0)
            {
                log.Error("Dice Num Is Zero" + diceEvent.Id);
            }
            
            RoundData d = new RoundData()
            {
                eventId = diceEvent.EventId,
                dice1 = diceEvent.Dice1,
                dice2 = diceEvent.Dice2,
                showDice = diceEvent.ShowDiceValue,
                pos = diceEvent.Pos,
                serverEventId =  diceEvent.Id,
                roundEvent =  new RoundEvent()
                {
                    eventType   = (int)diceEvent.Data.Type,
                    playerId    = diceEvent.Data.PlayerId,
                    gold        =  diceEvent.Data.Gold,
                    card_id     =  diceEvent.Data.CardId,
                }
            };

            return d;
        }

        IEnumerator LoginServer()
        {
            while (true)
            {
                var     waitLogin = new WaitEvent<string>(GameEvent.ENTER_LOGIN);
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
                if (waitMessage.IsTimeOut)
                {
                    log.Error("Message Time Out");
                    break;
                }
                if (waitMessage.Value == null || waitMessage.Value.Response == null)
                {
                    log.Error("Message Null");
                    break;
                }
                if (waitMessage.Value.Response.Err != ErrorCode.ErrorSuccess)
                {
                    log.Error("Login Fail=" + waitMessage.Value.Response.Err.ToString());
                    break;
                }

                var loaderUserInfo = new LoaderUserInfo()
                {
                    Request = new LoaderUserInfo.Types.Request()
                };
                loaderUserInfo.Send((int)GameMsgID.CsLoaderUserInfo);
                var waitUserInfo = new WaitMessage<LoaderUserInfo>((int)GameMsgID.CsLoaderUserInfo);
                yield return waitUserInfo;
                if (waitUserInfo.Value == null)
                {
                    log.Error("loaderUserInfo  is null" );
                    break;
                }
                var userInfo = waitUserInfo.Value.Response;
                if (userInfo.Code != ErrorCode.ErrorSuccess)
                {
                    log.Error("Get User Fail=" + waitMessage.Value.Response.Err.ToString());
                    break;
                }
                //userInfo.Pos
                
                log.Info("dice =" + userInfo.Dice.ToString() + " dice max=" + userInfo.DiceMax.ToString());
                m_userInfo = userInfo;
                
                // 发送获取数据
                break;
            }
        }
        
        public IEnumerator RunLogin()
        {
            const float HotfixTime  = 1.0f;  // 更新UI显示时间
            const float LoadingTime = 0.5f; // 加载UI更新时间
            
            // 1. 显示更新界面
            Entity hotfixUI = UIRequest.Create(EntityManager, UIUtils.GetUI("hotfix"));
            EntityManager.AddComponentData(hotfixUI, new UIParam() {Value = HotfixTime});
            
            // 2. 显示登录界面
            yield return new WaitEvent(GameEvent.HOTFIX_DONE);
            Entity loginUI = UIRequest.Create(EntityManager, UIUtils.GetUI("login"));
            yield return new WaitUIOpen(EntityManager, loginUI);
            UIUtils.CloseUI(hotfixUI);

            // 3. 等待登录事件登录
            yield return LoginServer();
            SetupDefault();

            // 登录成功后显示加载UI
            Entity loadingUI = UIRequest.Create(EntityManager, UIUtils.GetUI("loading"));
            EntityManager.AddComponentData(loadingUI, new UIParam() {Value = LoadingTime});
            yield return new WaitUIOpen(EntityManager, loadingUI);
            UIUtils.CloseUI(loginUI);

            // 4. 完成后直接进入主界
            yield return new WaitEvent(GameEvent.ENTER_GAME);
            Entity mainUI = UIRequest.Create(EntityManager, UIUtils.GetUI("mainui"));
            UIUtils.CloseUI(loadingUI);
            
            yield return null;
        }
    }
}