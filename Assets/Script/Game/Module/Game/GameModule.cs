using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Fibers;
using Unity.Entities;
using Unity.Mathematics;

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
            EntityManager mgr = m_gameWorld.GetEntityManager();
            EntityQuery query = mgr.CreateEntityQuery(typeof(CheckPointData));
            m_checkPoints = query.GetSingleton<CheckPointData>();
            
            // 1. 创建角色
            m_player = m_characterModule.CreateCharacter(101);

            // 2. 创建骰子
            // m_dice   = m_diceModule.Create();
            
            // 3. 获取场景路径
            yield return null;
        }

        // 等待下一轮
        IEnumerator WaitNextRond()
        {
            yield return FiberHelper.Wait(1.0f);
        }

        // 游戏循环
        IEnumerator Play()
        {
            while (true)
            {
                // 等待下一局
                yield return WaitNextRond();

                // 获得随机骰子
                int dice_value = m_randomSystem.NextInt(1, 7);
                
                // 显示骰子动画
                var dice = m_diceModule.Create();
                m_diceModule.Play(dice, 1.0f, dice_value);
                
                // 等待骰子动画结束
                yield return FiberHelper.Wait(1.2f);
                
                // 删除骰子
                m_gameWorld.RequestDespawn(dice);
                
                // 角色移动
                yield return PlayerMove(dice_value);
            }
        }

        IEnumerator PlayerMove(int move_num)
        {
            List<float3> paths = new List<float3>(move_num);
            
            for (int i = 0; i < move_num; i++)
            {
                int index = (m_curCheckPoint + i) % m_checkPoints.Value.Count;
                
                // 添加位置
                paths.Add(m_checkPoints.Value[index]);
            }

           // m_gameWorld.GetEntityManager().GetComponentData<CharacterMover>(m_player);
            
            yield return null;
        }
        
        private GameWorld           m_gameWorld      ;
        private ResourceManager     m_resourceManager;
        private CharacterModule     m_characterModule;
        private DiceModule          m_diceModule     ;
        private Fiber               m_fiber          ;
        
        // 角色
        private Entity              m_player         ;

        // 路径点
        private CheckPointData      m_checkPoints    ;
        private RandomSystem        m_randomSystem   ;
        
        // 当前移动点
        private int                 m_curCheckPoint  ;
    }
}