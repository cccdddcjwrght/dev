using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace SGame
{
    // 给场景中角色的移动产生场景触发事件
    [DisableAutoCreation]
    public partial class TileEventSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            // 判断角色移动到某个场景中, 并触发对应的事件
            Entities.ForEach((Entity e, Character character, CharacterMover mover, ref TitleEvent eventState, ref DynamicBuffer<TitleEventTrigger> trigger) =>
            {
                // 当前title id
                int titleId = character.titleId;
                if (titleId != eventState.titleId)
                {
                    if (mover.isFinish)
                    {
                        trigger.Add(new TitleEventTrigger() { state = TitleEventTrigger.State.STADY });
                    }
                }
            }).WithoutBurst().Run();
        }
    }
}