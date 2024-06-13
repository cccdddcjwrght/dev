using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;


namespace SGame
{
    /// <summary>
    /// 模块自己处理销毁标记, 与DespawningEntity 不同, DespawningEntity最终会有默认系统处理. 这边只由对应的销毁系统去处理
    /// </summary>
    public struct DespawningTag : IComponentData
    {
    }
}