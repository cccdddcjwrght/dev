using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace SGame
{
    [UpdateInGroup(typeof(GameLogicAfterGroup))]
    public partial class GameObjectSyncSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            Entities.WithAll<GameObjectSyncTag>().ForEach((Entity e, Transform trans, in Translation t, in Rotation rot ) =>
            {
                trans.position = t.Value;
                trans.rotation = rot.Value;
            }).WithoutBurst().Run();
        }
    }
}