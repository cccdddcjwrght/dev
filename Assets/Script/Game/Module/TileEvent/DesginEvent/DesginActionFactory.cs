using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

namespace SGame
{
    public class DesginActionFactory
    {
        /// <summary>
        /// 创建金币事件
        /// </summary>
        /// <param name="addGold"></param>
        /// <returns></returns>
        public IDesginAction CreateGold(int addGold)
        {
            return new DesginGoldAction(addGold);
        }
    }
}