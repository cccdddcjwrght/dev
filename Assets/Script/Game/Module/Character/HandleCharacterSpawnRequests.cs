using log4net;
using Unity.Entities;
using Unity.Collections;

namespace SGame
{
    // 角色创建
    public struct CharacterSpawnRequest : IComponentData
    {
        public int          id;             // 角色ID
        public Entity       property;       // 角色属性信息

        // 通过COMMAND buffer 创建请求
        public static void Create(EntityCommandBuffer commandBuffer, int id)
        {
            var e = commandBuffer.CreateEntity();
            var data = new CharacterSpawnRequest { id = id, property = Entity.Null };
            commandBuffer.AddComponent(e, data);
        }
    }
    
    [DisableAutoCreation]
    public partial class HandleCharacterSpawnRequests : SystemBase
    {
        private static ILog log = LogManager.GetLogger("xl.Game.Character");
        private GameWorld           m_gameWorld;
        private ResourceManager     m_resourceManager;
        private EntityQuery         m_query;

        public void Initalize(GameWorld gameWorld, ResourceManager resourceManager)
        {
            log.Info("HandleCharacterSpawnRequests Initalize");
            m_gameWorld         = gameWorld;
            m_resourceManager   = resourceManager;
            
            m_query = GetEntityQuery(ComponentType.ReadOnly<CharacterSpawnRequest>());
        }
        
        protected override void OnCreate()
        {
            log.Info("HandleCharacterSpawnRequests On Create");
        }
 
        protected override void OnUpdate()
        {
            log.Info("HandleCharacterSpawnRequests On OnUpdate");
            NativeArray<CharacterSpawnRequest> datas = m_query.ToComponentDataArray<CharacterSpawnRequest>(Allocator.Temp);
            EntityCommandBuffer cmd = new EntityCommandBuffer(Allocator.Temp);
            for (int i = 0; i < datas.Length; i++)
            {
                m_resourceManager.LoadCharacter(datas[i].id);
            }
            
            Entities.WithAll<CharacterSpawnRequest>().ForEach((Entity e) =>
            {
                cmd.DestroyEntity(e);
            }).WithoutBurst().Run();
            
            cmd.Playback(EntityManager);
            cmd.Dispose();
        }
    }
}