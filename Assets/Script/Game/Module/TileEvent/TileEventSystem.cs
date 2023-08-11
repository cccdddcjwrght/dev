using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using Unity.Collections;

namespace SGame
{
    // 给场景中角色的移动产生场景触发事件
    [DisableAutoCreation]
    public partial class TileEventSystem : SystemBase
    {
        protected override void OnCreate()
        {
            base.OnCreate();
        }

        protected override void OnUpdate()
        {
            // 判断角色移动到某个场景中, 并触发对应的事件
            Entities.ForEach((Entity e,
                Character character, 
                CharacterMover mover, 
                ref TileEventRecord eventRecord, 
                ref DynamicBuffer<TileEventTrigger> trigger) =>
            {
                trigger.Clear();
                
                // 当前title id
                int titleId = character.titleId;
                if (titleId != eventRecord.titleId)
                {
                    // 离开格子
                    trigger.Add(new TileEventTrigger() { titleId = eventRecord.previousTitleId, state = TileEventTrigger.State.LEAVE });
                    trigger.Add(new TileEventTrigger() { titleId = titleId,                     state = TileEventTrigger.State.ENTER });
                    if (mover.isFinish) 
                    {
                        trigger.Add(new TileEventTrigger() { titleId = titleId, state = TileEventTrigger.State.FINISH });
                    }
                    eventRecord.previousTitleId = eventRecord.titleId;
                    eventRecord.titleId = titleId;
                }
            }).WithoutBurst().Run();
        }
    }
}