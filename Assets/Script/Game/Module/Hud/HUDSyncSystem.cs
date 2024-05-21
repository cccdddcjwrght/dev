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
    public class HUDSyncSystem : ComponentSystem
    {
        private static ILog              log = LogManager.GetLogger("game.hud");
        
        protected override void OnUpdate()
        {
            Entities.WithAll<HUDSync, UIInitalized>().WithNone<DespawningEntity>().ForEach((Entity e, UIWindow data, ref Translation translation) =>
            {
                if (data.Value != null && !data.Value.isClosed)
                {
                    var ui = data.Value;
                    Vector2 pos = SGame.UIUtils.GetUIPosition(ui.parent, translation.Value, PositionType.POS3D);
                    ui.xy = pos;
                }
            });
        }
    }
}