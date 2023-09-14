using System.Collections;
using System.Collections.Generic;
using Fibers;
using log4net;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
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
            TileModule      tileModule,
            TileEventModule tileEventModule,
            CameraModule     cameraModule
            )
        {
            m_gameWorld       = gameWorld;
            m_resourceManager = resourceManager;
            m_randomSystem    = randomSystem;
            m_characterModule = characterModule;
            m_diceModule      = diceModule;
            m_resourceManager = resourceManager;
            m_userInputSystem = userInputSystem;
            m_tileModule      = tileModule;
            m_tileEventModule = tileEventModule;
            m_cameraModule    = cameraModule;
            m_fiber           = new Fiber(Logic());

            m_userData        = property.GetGroup(ItemType.USER);
            m_eventHandles.Add(EventManager.Instance.Reg((int)GameEvent.PLAYER_POWER_DICE, OnChangeDicePower));
            InitLogic();
            InitTravel();
            
            // 网络初始化
            InitNet();
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
             
            // 游戏逻辑
            while (true)
            {
                // 运行出行逻辑
                yield return RunTavel();
                
                // 等待下一轮 自动摇骰子还是手动
                EventManager.Instance.Trigger((int)GameEvent.GAME_WAIT_NEXTROUND);
                yield return WaitNextRond();
                
                // 播放下一轮
                EventManager.Instance.Trigger((int)GameEvent.GAME_NEXTROUND);
                yield return PlayNextRound();

                // 防止进入死循环
                yield return null;
            }
        }

        // 开始游戏
        IEnumerator StartGame()
        {
            // 等待checkPoint 转换结束!
            yield return FiberHelper.Wait(1.0f);
            
            EntityManager mgr = m_gameWorld.GetEntityManager();

            if (m_tileModule.tileCount <= 2)
            {
                yield break;
            }
            
            // 1. 创建角色
            m_player            = m_characterModule.CreateCharacter(101);
            float3 pos          = m_tileModule.current.GetTileDataFromPos(m_currentPlayerPos).Position3d;
            mgr.SetComponentData(m_player, new Translation{Value = pos});
            UserData userData   = DataCenter.Instance.GetUserData();
            userData.player     = m_player;
            DataCenter.Instance.SetUserData(userData);
            
            // 2. 创建骰子
            m_dice1   =   m_diceModule.Create();
            m_dice2   =   m_diceModule.Create();
            var spawnCheckPoints = EntityManager.CreateEntityQuery(typeof(SpawnPoint), typeof(Translation));
            var checkPoints         = spawnCheckPoints.ToEntityArray(Allocator.Temp);
            var checkPointTypes = spawnCheckPoints.ToComponentDataArray<SpawnPoint>(Allocator.Temp);
            for (int i = 0; i < checkPointTypes.Length; i++)
            {
                switch (checkPointTypes[i].Value)
                {
                    case SpawnPoint.PointType.NORMAL_DICE:
                        m_normalDiceCheckPoint = checkPoints[i];
                        break;
                    case SpawnPoint.PointType.TRAVEL_DICE:
                        m_travelDiceCheckPoint = checkPoints[i];
                        break;
                }
            }
            spawnCheckPoints.Dispose();
            checkPoints.Dispose();
            checkPointTypes.Dispose();
            // 设置骰子位置
            if (EntityManager.Exists(m_normalDiceCheckPoint) == false)
            {
                log.Error("normal dice check point not found");
            }
            if (EntityManager.Exists(m_travelDiceCheckPoint) == false)
            {
                log.Error("travel dice check point not found");
            }

            UpdateDicePosition();

            yield return PlayerCameraSetup();

            // 3. 获取场景路径
            yield return null;
        }

        private void UpdateDicePosition()
        {
            float3 pos = float3.zero;
            if (m_characterModule.IsReadly(m_player))
            {
                pos = EntityManager.GetComponentData<Translation>(m_player).Value;
                pos += new float3(0, 2, 0);
            }
            else if (m_playerState == PlayState.NORMAL)
            {
                pos = EntityManager.GetComponentData<Translation>(m_normalDiceCheckPoint).Value;
            }
            else 
            {
                pos = EntityManager.GetComponentData<Translation>(m_travelDiceCheckPoint).Value;
            }
            
            EntityManager.SetComponentData(m_dice1, new Translation() {Value = pos});
            EntityManager.SetComponentData(m_dice1, new Rotation()    {Value = quaternion.identity});

            EntityManager.SetComponentData(m_dice2, new Translation() {Value = pos + new float3(1,0,0)});
            EntityManager.SetComponentData(m_dice2, new Rotation()    {Value = quaternion.identity});
        }


        void ShowDice(bool show)
        {
            Utils.EnableEntity(EntityManager, m_dice1, show);
            Utils.EnableEntity(EntityManager, m_dice2, show);
        }

        /// <summary>
        /// 提示骰子不足
        /// </summary>
        void TipDiceNotEnough()
        {
            UIUtils.ShowTips(EntityManager, "NOT ENOUGH DICE", float3.zero, Color.yellow, 50, 2.0f, PositionType.POS2D_CENTER);
        }

        // 等待下一局
        IEnumerator WaitNextRond()
        {
            var mgr = m_gameWorld.GetEntityManager();
            m_userSetting = DataCenter.Instance.GetUserSetting();

            while (true)
            {
                m_userSetting = DataCenter.Instance.GetUserSetting();
                int dicePower = (int)m_userData.GetNum((int)UserType.DICE_NUM);
                
                if (m_userSetting.autoUse == true)
                {
                    // 自动投掷就等待1秒
                    yield return FiberHelper.Wait(1.0f);
                    
                    // 只有普通关卡才计算骰子
                    if (m_playerState ==  PlayState.NORMAL &&  dicePower < m_userSetting.power)
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
                
                // 等待玩家输入
                UserInput input = m_userInputSystem.GetInput();
                if (input.rollDice == true)
                {
                    if (m_playerState == PlayState.NORMAL && dicePower < m_userSetting.power)
                    {
                        TipDiceNotEnough();
                        yield return null;
                        continue;
                    }
                    break;
                }

                yield return null;
            }

            if (m_playerState == PlayState.NORMAL)
                m_userData.SetNum((int)UserType.DICE_POWER, m_userSetting.power);
            else
                m_userData.SetNum((int)UserType.DICE_POWER, 1);
        }

        // 执行下一局
        IEnumerator PlayNextRound()
        {
            // 获得随机骰子
            // 获得下一轮数据
            if (m_tileEventModule.NextRound(m_currentPlayerPos, out DiceRollData diceData) == false)
            {
                log.Error("Game Event List Is Empty!");
                yield break;
            }
            
            // 消耗一个骰子数量
            var power = m_userData.GetNum((int)UserType.DICE_POWER); //m_userSetting.power;
            if (m_playerState == PlayState.NORMAL)
            {
                // 正常运行需要扣骰子数量
                m_userData.AddNum((int)UserType.DICE_NUM, -power);
            }

            int dice_value1 = diceData.Value1; 
            int dice_value2 = diceData.Value2;
            if (dice_value1 == 0)
            {
                log.Error("dice is zero! evenit id=" + diceData.eventId);
                yield break;
            }

            Utils.EnableEntity(EntityManager, m_dice2, dice_value2 > 0);
            
            // 同时运行两个骰子动画
            UpdateDicePosition();
            if (diceData.ShowValue == 0)
            {
                yield return FiberHelper.RunParallel(
                    ShowDice(m_dice1, dice_value1, 0.5f),
                    ShowDice(m_dice2, dice_value2, 0.5f));
            }
            else
            {
                yield return FiberHelper.RunParallel(
                    ShowDice(m_dice1, diceData.ShowValue, 0.5f),
                    ShowDice(m_dice2, 0, 0.5f));
            }

            // 角色移动
            var waitMove = PlayerMove(dice_value1 + dice_value2);
            if (m_tileEventModule.IsEmpty())
            {
                SendGetNextEventPool(m_currentPlayerPos,  m_tileModule.mapId);
            }
            yield return waitMove;

            // 移动结束了, 同步服务器
            SendCommitEvent(diceData.eventId, (int)power);
        }

        IEnumerator ShowDice(Entity dice, int num, float time)
        {
            if (num == 0)
                yield break;
            
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
            var currentMap = m_tileModule.current;
            for (int i = 0; i < move_num + 1; i++)
            {
                int index = (m_currentPlayerPos + i) % currentMap.count;
                
                // 添加位置 
                paths.Add(currentMap.GetTileDataFromPos(index).Position3d);// m_checkPoints.Value[index]);
            }
            int startIndex = m_currentPlayerPos;
            m_currentPlayerPos += move_num;
            m_currentPlayerPos %= currentMap.count;

            // 移动角色
            CharacterMover mover = mgr.GetComponentObject<CharacterMover>(m_player);
            mover.MoveTo(paths, startIndex, MOVE_INTERVAL_TIME);

            Character character = mgr.GetComponentObject<Character>(m_player);
            character.titleId = m_currentPlayerPos;
            
            // 等待角色移动结束
            while (mover.isFinish == false)
                yield return null;

            // 显示游戏内事件
            log.Info(string.Format("Move Finish Start Index={0} End Index={1} DiceNum={2}", startIndex, m_currentPlayerPos, move_num));
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
        private TileModule          m_tileModule     ;
        private RandomSystem        m_randomSystem   ;
        
        // 当前移动点
        private int                 m_currentPlayerPos  ;

        private UserInputsystem     m_userInputSystem ;
        
        // 游戏主逻辑
        private static ILog log = LogManager.GetLogger("xl.Game.Main");
        
        private ItemGroup           m_userData;

        private UserSetting         m_userSetting;

        private TileEventModule     m_tileEventModule;
        
        private EventHandleContainer m_eventHandles = new EventHandleContainer();

        /// <summary>
        /// 摄像机模块引用 
        /// </summary>
        private CameraModule         m_cameraModule;         

        /// <summary>
        /// 关联点
        /// </summary>
        private Entity               m_normalDiceCheckPoint;
        private Entity               m_travelDiceCheckPoint;
    }
}