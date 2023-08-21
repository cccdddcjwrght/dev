using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Fibers;
using log4net;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEditorInternal;
using UnityEngine;

namespace SGame
{
    // 主要用于运行游戏逻辑
    public partial class GameModule
    {
        public GameModule(
            GameWorld       gameWorld,
            ResourceManager resourceManager,
            RandomSystem    randomSystem,
            UserInputsystem userInputSystem,
            CharacterModule characterModule, 
            DiceModule      diceModule,
            PropertyManager property,
            TileEventModule tileEventModule
            )
        {
            m_gameWorld       = gameWorld;
            m_resourceManager = resourceManager;
            m_randomSystem    = randomSystem;
            m_characterModule = characterModule;
            m_diceModule      = diceModule;
            m_resourceManager = resourceManager;
            m_userInputSystem = userInputSystem;
            m_tileEventModule = tileEventModule;
            m_fiber           = new Fiber(Logic());

            m_userData        = property.GetGroup(ItemType.USER);
            m_eventHandles.Add(EventManager.Instance.Reg((int)GameEvent.PLAYER_POWER_DICE, OnChangeDicePower));
            LogicInit();
        }
        
        public EntityManager EntityManager { get { return m_gameWorld.GetEntityManager(); } }

        private const float MOVE_INTERVAL_TIME = 0.0f;
        
        public void Update()
        {
            m_fiber.Step();
        }

        IEnumerator Logic()
        {
            // 执行登录逻辑
            yield return RunLogin();
            
            // 开始游戏
            yield return StartGame();
             
            // 进入循环逻辑
            while (true)
            {
                yield return WaitNextRond();
                yield return Play();
            }
        }

        // 开始游戏
        IEnumerator StartGame()
        {
            // 等待checkPoint 转换结束!
            yield return FiberHelper.Wait(1.0f);
            
            EntityManager mgr = m_gameWorld.GetEntityManager();
            EntityQuery query = mgr.CreateEntityQuery(typeof(CheckPointData));
            m_checkPoints = query.GetSingleton<CheckPointData>();

            if (m_checkPoints.Value.Count <= 2)
            {
                yield break;
            }
            
            // 1. 创建角色
            m_player = m_characterModule.CreateCharacter(101);
            mgr.SetComponentData(m_player, new Translation{Value = m_checkPoints.Value[0]});
            UserData userData = DataCenter.Instance.GetUserData();
            userData.player = m_player;
            DataCenter.Instance.SetUserData(userData);
            
            // 2. 创建骰子
            m_dice1   =   m_diceModule.Create();
            m_dice2   =   m_diceModule.Create();
            
            mgr.SetComponentData(m_dice1, new Translation() {Value = new float3(-4, 1, 0)});
            mgr.SetComponentData(m_dice1, new Rotation() {Value = quaternion.identity});
            
            mgr.SetComponentData(m_dice2, new Translation() {Value = new float3(-5, 1, 0)});
            mgr.SetComponentData(m_dice2, new Rotation() {Value = quaternion.identity});
            
            
            // 3. 获取场景路径
            yield return null;
        }

        /// <summary>
        /// 提示骰子不足
        /// </summary>
        void TipDiceNotEnough()
        {
            UIUtils.ShowTips(EntityManager, "NOT ENOUGH DICE", new float3(8.22000027f, -1.13f, 5.09000015f), Color.yellow, 50, 2.0f);
        }

        // 等待下一轮
        IEnumerator WaitNextRond()
        {
            var mgr = m_gameWorld.GetEntityManager();
            m_userSetting = DataCenter.Instance.GetUserSetting();
            
            
            while (true)
            {
                m_userSetting = DataCenter.Instance.GetUserSetting();
                int dicePower = (int)m_userData.GetNum((int)UserType.DICE_POWER);
                
                if (m_userSetting.autoUse == true)
                {
                    // 自动投掷就等待1秒
                    yield return FiberHelper.Wait(1.0f);
                    if (dicePower < m_userSetting.doubleBonus)
                    {
                        // 取消自动骰子
                        m_userSetting.autoUse = false;
                        DataCenter.Instance.SetUserSetting(m_userSetting);
                        TipDiceNotEnough();
                        yield return null;
                        continue;
                    }
                    break;
                }
                
                UserInput input = m_userInputSystem.GetInput();
                if (input.rollDice == true)
                {
                    if (dicePower < m_userSetting.doubleBonus)
                    {
                        TipDiceNotEnough();
                        yield return null;
                        continue;
                    }
                    break;
                }

                yield return null;
            }
        }

        // 游戏循环
        IEnumerator Play()
        {
            // 消耗一个骰子数量
            m_userData.AddNum((int)UserType.DICE_POWER, -m_userSetting.doubleBonus);
            
            // 获得随机骰子
            // 获得下一轮数据
            var diceData = m_tileEventModule.NextRound(m_curCheckPoint);
            int dice_value1 = diceData.Value1; 
            int dice_value2 = diceData.Value2;

            // 同时运行两个骰子动画
            yield return FiberHelper.RunParallel(
                ShowDice(m_dice1, dice_value1, 0.5f), 
                ShowDice(m_dice2, dice_value2 , 0.5f));

            // 角色移动
            yield return PlayerMove(dice_value1 + dice_value2);
        }

        IEnumerator ShowDice(Entity dice, int num, float time)
        {
            // 播放骰子
            m_diceModule.Play(dice, time, num);
            
            // 等两等两帧开始播放动画
            yield return null;
            yield return null;
            
            // 等待骰子动画结束
            DiceAnimation diceAnimation = m_gameWorld.GetEntityManager().GetComponentData<DiceAnimation>(dice);
            while (diceAnimation.isPlaying)
            {
                yield return null;
                diceAnimation = m_gameWorld.GetEntityManager().GetComponentData<DiceAnimation>(dice);
            }
            
            // 删除骰子
            // m_gameWorld.RequestDespawn(dice);
        }

        IEnumerator PlayerMove(int move_num)
        {
            var mgr = m_gameWorld.GetEntityManager();
            
            // 统计移动点
            List<float3> paths = new List<float3>(move_num);
            for (int i = 0; i < move_num + 1; i++)
            {
                int index = (m_curCheckPoint + i) % m_checkPoints.Value.Count;
                
                // 添加位置 
                paths.Add(m_checkPoints.Value[index]);
            }
            int startIndex = m_curCheckPoint;
            m_curCheckPoint += move_num;
            m_curCheckPoint %= m_checkPoints.Value.Count;

            // 移动角色
            CharacterMover mover = mgr.GetComponentObject<CharacterMover>(m_player);
            mover.MoveTo(paths, startIndex, MOVE_INTERVAL_TIME);

            Character character = mgr.GetComponentObject<Character>(m_player);
            character.titleId = m_curCheckPoint;
            
            // 等待角色移动结束
            while (mover.isFinish == false)
                yield return null;
            
            // 显示游戏内事件
            log.Info(string.Format("Move Finish Start Index={0} End Index={1} DiceNum={2}", startIndex, m_curCheckPoint, move_num));
        }



        public void Shutdown()
        {
        }
        
        private GameWorld           m_gameWorld      ;
        private ResourceManager     m_resourceManager;
        private CharacterModule     m_characterModule;
        private DiceModule          m_diceModule     ;
        private Fiber               m_fiber          ;
        
        // 角色
        private Entity              m_player         ;

        // 骰子1
        private Entity              m_dice1           ;
        private Entity              m_dice2           ;

        // 路径点
        private CheckPointData      m_checkPoints    ;
        private RandomSystem        m_randomSystem   ;
        
        // 当前移动点
        private int                 m_curCheckPoint  ;

        private UserInputsystem    m_userInputSystem ;
        
        // 游戏主逻辑
        private static ILog log = LogManager.GetLogger("xl.Game.Main");
        
        private ItemGroup           m_userData;

        private UserSetting         m_userSetting;

        private TileEventModule    m_tileEventModule;
        
        private EventHandleContainer m_eventHandles = new EventHandleContainer();
    }
}