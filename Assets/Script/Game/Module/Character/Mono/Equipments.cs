using System.Collections;
using System.Collections.Generic;
using libx;
using log4net;
using UnityEngine;

namespace SGame
{
    public enum SlotType
    {
        WEAPON      = 1, // 武器
        FOOD        = 2, // 食物挂点
        HUD         = 3, // 头顶HUD信息挂点
    }
    
    public class Equipments : MonoBehaviour
    {
        //private const string EquipPath  = "Assets/BuildAsset/Prefabs/Equipments/";
        private static ILog         log = LogManager.GetLogger("game.equipments");

        // 插槽对象
        public Dictionary<SlotType, Transform> m_slots;

        void Start()
        {
            m_slots = new Dictionary<SlotType, Transform>();
            var weapon      = transform.FindRecursive("weapon");
            var glasses     = transform.FindRecursive("glasses");
            var good        = transform.FindRecursive("good");
            
            // 初始化插槽
            m_slots.Add(SlotType.WEAPON, weapon);
            m_slots.Add(SlotType.FOOD,   good);
            m_slots.Add(SlotType.HUD,    glasses);

        }
        
        /// <summary>
        /// 获得插槽
        /// </summary>
        /// <param name="equipType"></param>
        /// <returns></returns>
        public Transform GetSlot(SlotType equipType)
        {
            if (m_slots.TryGetValue(equipType, out Transform trans))
            {
                return trans;
            }

            return this.transform;
        }
    }
}