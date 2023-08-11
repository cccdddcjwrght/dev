using System.Collections;
using System.Collections.Generic;
using log4net;
using Unity.Entities;
using UnityEngine;
namespace SGame
{
    [DisableAutoCreation]
    public partial class TileEventProcessSystem : SystemBase
    {
        private static ILog log = LogManager.GetLogger("xl.game");

        protected Entity m_userData;

        protected override void OnCreate()
        {
            base.OnCreate();
            m_userData = DataCenter.Instance.m_userData;
        }

        protected override void OnUpdate()
        {
            Entities.ForEach((Entity e, in DynamicBuffer<TileEventTrigger> triggers) =>
            {
                for (int i = 0; i < triggers.Length; i++)
                {
                    if (triggers[i].state == TileEventTrigger.State.ENTER)
                    {
                        var data = DataCenter.Instance.GetUserData();
                        data.gold += RandomSystem.Instance.NextInt(0, 10);
                        DataCenter.Instance.SetUserData(data);
                    } 
                }
            }).WithoutBurst().Run();
        }
    }
}