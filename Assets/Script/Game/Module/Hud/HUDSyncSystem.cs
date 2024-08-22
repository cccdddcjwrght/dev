using Unity.Entities;
using UnityEngine;
using log4net;
using Unity.Mathematics;
using Unity.Transforms;

namespace SGame.UI
{
    /// HUD 同步系统, 将3D坐标同步到UI坐标中去
    public struct HUDSync : IComponentData { }

    [UpdateAfter(typeof(SpawnUISystem))]
    [UpdateInGroup(typeof(GameUIGroup))]
    public partial class HUDSyncSystem : SystemBase
    {
        private static ILog              log = LogManager.GetLogger("game.hud");
        
        protected override void OnUpdate()
        {
            Entities.WithAll<HUDSync, UIInitalized>().WithNone<DespawningUI>().ForEach((Entity e, UIWindow data, in LocalTransform translation) =>
            {
                if (data.BaseValue != null && !data.BaseValue.isClosed && data.BaseValue.isShowing)
                {
                    var ui = data.rootUI;
                    Vector2 pos = SGame.UIUtils.GetUIPosition(ui.parent, translation.Position, PositionType.POS3D);
                    ui.xy = pos;
                }
            }).WithoutBurst().Run();
        }
    }
}