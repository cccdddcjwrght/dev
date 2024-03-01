using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame
{
    /// <summary>
    /// 点击小费
    /// </summary>
    public class FoodTipClick : MonoBehaviour, ITouchOrHited
    {
        /// <summary>
        /// 鼠标点击
        /// </summary>
        public void OnClick()
        {
            var effectMono = GetComponentInParent<EffectMono>();

            // 只触发一次, 防止多次点击造成 重复添加数值
            EventManager.Instance.Trigger((int)GameEvent.FOOD_TIP_CLICK, effectMono.entity);
            Destroy(this);

        }
    }
}