using FairyGUI;
using Unity.Entities;
using SGame.UI.Hud;
using UnityEngine;
using log4net;
using Unity.Transforms;

namespace SGame
{

    [DisableAutoCreation]
    public partial class FloatTextSystem : SystemBase
    {
        private EntityArchetype          m_floatTextType;
        public ObjectPool<UI_FloatText>  m_floatComponents;
        private Entity                   m_hud;
        private bool                     m_isReadly;
        private GComponent               m_hudContent;
        private float                    FLOAT_SPEED = 100.0f;
        private static ILog              log = LogManager.GetLogger("xl.game.floatext");


        public void Initalize(Entity hud, ObjectPool<UI_FloatText> pool)
        {
            m_hud      = hud;
            m_isReadly = false;
            m_floatComponents = pool;
        }
        
        protected override void OnUpdate()
        {
            float moveMent = FLOAT_SPEED * Time.DeltaTime;
            Entities.WithNone<DespawningEntity>().ForEach((Entity e, in Translation translation, in FloatTextData data) =>
            {
                if (m_floatComponents.TryGet(data.Value, out UI_FloatText floatText))
                {
                    Vector2 pos = UIUtils.GetUIPosition (floatText.parent, translation.Value, data.posType);
                    floatText.xy = pos;
                }
            }).WithoutBurst().Run();
        }
    }
}