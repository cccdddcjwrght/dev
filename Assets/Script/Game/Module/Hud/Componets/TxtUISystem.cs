using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using GameConfigs;
using log4net;
using SGame;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using SGame.UI.Hud;
using UnityEngine;


[UpdateInGroup(typeof(GameUIGroup))]
public partial class TxtUISystem : SystemBase
{
    private EntityArchetype  m_tipType;
    private Entity           m_hud;
    private bool                                            m_isReadly;
    private GComponent                                      m_hudContent;
    private EndInitializationEntityCommandBufferSystem      m_commandBuffer;
    private EntityArchetype                                 entityArchetype;
    private static ILog log = LogManager.GetLogger("xl.game.tip");
    private float FLOAT_SPEED   = 10.0f;
    private float FLOAT_SPEED2D = 50.0f; // 2d的飘字速度
    
    /// <summary>
    /// 提示生成初始化
    /// </summary>
    /// <param name="hud"></param>
    public void Initalize(Entity hud)
    {
        m_hud    = hud;
    }
    
    protected override void OnCreate()
    {
        base.OnCreate();
        m_commandBuffer = World.GetOrCreateSystem<EndInitializationEntityCommandBufferSystem>();
        FLOAT_SPEED = GlobalDesginConfig.GetFloat("float_text_speed");
        FLOAT_SPEED2D = GlobalDesginConfig.GetFloat("float_text2d_speed");
    }
    
    protected override void OnUpdate()
    {
        var commandBuffer = m_commandBuffer.CreateCommandBuffer();
        Entities.ForEach((Entity entity,in TextDisplayData request) =>
        {
            // 生成浮动文本UI
                    entityArchetype = EntityManager.CreateArchetype(
                        typeof(Translation),
                        typeof(LocalToWorld),
                        typeof(Rotation),
                        typeof(MoveDirection),
                        typeof(LiveTime));
                    Entity e = commandBuffer.CreateEntity(entityArchetype);
                    log.Info("create Entity=====HubFloat");
                    if (request.posType == PositionType.POS3D)
                    {
                        commandBuffer.SetComponent(e, new MoveDirection() { Value = new float3() { x = 0, y = 1, z = 0 } * FLOAT_SPEED, duration = request.duration - 0.2f });
                    }
                    else
                    {
                        // UI是反方向
                        commandBuffer.SetComponent(e,  new MoveDirection() { Value = new float3() { x = 0, y = -1, z = 0 } * FLOAT_SPEED2D, duration = request.duration - 0.2f});
                    }
                    commandBuffer.SetComponent(e, new Rotation()      { Value = quaternion.identity });
                    commandBuffer.SetComponent(e, new Translation()   { Value = request.position });
                    commandBuffer.SetComponent(e, new LiveTime()      { Value = request.duration});
                    // 删除创建请求
                    commandBuffer.DestroyEntity(entity);
        }).WithoutBurst().Run();
    }
}
