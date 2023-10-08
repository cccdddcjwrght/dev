using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace SGame.BT
{
    [TaskCategory("Game")]
    public class BConditon : Conditional
    {
        public override TaskStatus OnUpdate()
        {
            //Unity.Entities.Entity
            return TaskStatus.Running;
        }
    }
}