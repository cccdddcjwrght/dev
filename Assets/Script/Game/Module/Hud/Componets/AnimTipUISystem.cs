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
public partial class AnimTipUISystem : SystemBase
{
    private EntityArchetype  m_tipType;
    private Entity           m_hud;
    private GComponent                                      m_hudContent;
    private EndInitializationEntityCommandBufferSystem      m_commandBuffer;
    private EntityArchetype                                 entityArchetype;
    private static ILog log = LogManager.GetLogger("xl.game.tip");

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
    }
    
    protected override void OnUpdate()
    {
        var commandBuffer = m_commandBuffer.CreateCommandBuffer();
        Entities.ForEach((Entity e, in AniImageDisplayData request) =>
        {
            // 生成图片UI
            entityArchetype = EntityManager.CreateArchetype(
                typeof(Translation),
                typeof(LocalToWorld),
                typeof(Rotation));
            Entity ui = commandBuffer.CreateEntity(entityArchetype);
            log.Info("create Entity=====HubAnim");
            commandBuffer.SetComponent(ui, new Rotation()    { Value = quaternion.identity });
            commandBuffer.SetComponent(ui, new Translation() { Value = request.position });
            // 删除创建请求
            commandBuffer.DestroyEntity(e);
          
        }).WithoutBurst().Run();
    }
}
