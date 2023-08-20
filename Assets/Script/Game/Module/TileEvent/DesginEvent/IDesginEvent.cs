using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame
{
    /// <summary>
    /// 执行事件
    /// </summary>
    public abstract class IDesginEvent
    {
        // 执行的地块
        public  int tileId;
        
        // 执行
        public virtual void Do() { }
    }
    
}
