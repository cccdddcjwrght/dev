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
    public class GameModule
    {
        public GameModule(
            GameWorld       gameWorld,
            ResourceManager resourceManager,
            RandomSystem    randomSystem,
            CharacterModule characterModule, 
            DiceModule      diceModule)
        {
            m_gameWorld       = gameWorld;
            m_resourceManager = resourceManager;
            m_randomSystem    = randomSystem;
            m_characterModule = characterModule;
            m_diceModule      = diceModule;
            m_resourceManager = resourceManager;
            m_fiber           = new Fiber(Logic());
        }

        public void Update()
        {
            m_fiber.Step();
        }

        IEnumerator Logic()
        {
            // 开始游戏
            yield return StartGame();
             
            // 进入循环逻辑
            yield return Play();
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
                ;
                yield break;
            }
            
            // 1. 创建角色
            m_player = m_characterModule.CreateCharacter(101);
            
            // 2. 创建骰子
            m_dice   =   m_diceModule.Create();
            
            mgr.SetComponentData(m_dice, new Translation() {Value = new float3(-4, 1, 0)});

            // 3. 获取场景路径
            yield return null;
        }

        // 等待下一轮
        IEnumerator WaitNextRond()
        {
            yield return FiberHelper.Wait(2.0f);
            log.Info("Next Round!");
        }

        // 游戏循环
        IEnumerator Play()
        {
            while (true)
            {
                // 获得随机骰子
                int dice_value = m_randomSystem.NextInt(1, 7);
                
                // 创建并显示骰子动画
                yield return ShowDice(dice_value, 0.5f);

                // 角色移动
                yield return PlayerMove(dice_value);
                
                // 等待下一局
                yield return WaitNextRond();
            }
        }

        IEnumerator ShowDice(int num, float time)
        {
            // 创建骰子
            m_diceModule.Play(m_dice, time, num);
            
            // 等两等两帧开始播放动画
            yield return null;
            yield return null;
            
            // 等待骰子动画结束
            DiceAnimation diceAnimation = m_gameWorld.GetEntityManager().GetComponentData<DiceAnimation>(m_dice);
            while (diceAnimation.isPlaying)
            {
                yield return null;
                diceAnimation = m_gameWorld.GetEntityManager().GetComponentData<DiceAnimation>(m_dice);
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
            m_curCheckPoint += move_num;
            m_curCheckPoint %= m_checkPoints.Value.Count;

            // 移动角色
            CharacterMover mover = mgr.GetComponentObject<CharacterMover>(m_player);
            mover.MoveTo(paths);
            
            // 等待角色移动结束
            while (mover.isFinish == false)
                yield return null;
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

        // 骰子
        private Entity              m_dice           ;

        // 路径点
        private CheckPointData      m_checkPoints    ;
        private RandomSystem        m_randomSystem   ;
        
        // 当前移动点
        private int                 m_curCheckPoint  ;

        // 游戏主逻辑
        private static ILog log = LogManager.GetLogger("xl.Game.Main");
    }
}