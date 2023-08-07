using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

// UI销毁系统
namespace SGame.UI
{
    public partial class DespaenUISystem : SystemBase
    {
        protected override void OnUpdate()
        {
            //  throw new System.NotImplementedException();
            Entities.WithAll<DespawningEntity>().ForEach((Entity e, UIWindow window) =>
            {
                window.Dispose();
            });
        }
    }
}