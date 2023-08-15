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
        private ItemGroup m_itemGroup;

        protected override void OnCreate()
        {
            m_itemGroup = PropertyManager.Instance.GetGroup(ItemType.USER);
        }
        
        protected override void OnUpdate()
        {
            UserSetting setting = DataCenter.Instance.GetUserSetting();
            Entities.ForEach((Entity e, in DynamicBuffer<TileEventTrigger> triggers) =>
            {
                for (int i = 0; i < triggers.Length; i++)
                {
                    if (triggers[i].state == TileEventTrigger.State.ENTER)
                    {
                        var data = DataCenter.Instance.GetUserData();
                        m_itemGroup.AddNum((int)UserType.GOLD, RandomSystem.Instance.NextInt(0, 10) * setting.doubleBonus);
                        //data.gold += (RandomSystem.Instance.NextInt(0, 10) * setting.doubleBonus);
                        DataCenter.Instance.SetUserData(data);
                    } 
                }
            }).WithoutBurst().Run();
        }
    }
}