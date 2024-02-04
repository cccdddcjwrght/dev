using System.Collections;
using System.Collections.Generic;
using libx;
using log4net;
using UnityEngine;

namespace SGame
{
    public enum SlotType
    {
        LEFT_HAND   = 1, // 左手武器
        RIGHT_HAND  = 2, // 右手武器
        
        FOOD        = 3, // 食物挂点
        HUD         = 4, // 头顶HUD信息挂点
    }
    
    public class Equipments : MonoBehaviour
    {
        private const string EquipPath = "Assets/BuildAsset/Prefabs/Equipments/";
        private static ILog log = LogManager.GetLogger("game.equipments");
        
        // 武器挂点
        public Transform slotWeapon;

        // 插槽对象
        public Dictionary<SlotType, GameObject> m_slots;
        private GameObject weapon;

        void Start()
        {
            m_slots = new Dictionary<SlotType, GameObject>();
            slotWeapon = transform.FindRecursive("weapon");
        }
        
        Transform GetSlot(SlotType equipType)
        {
            return slotWeapon;
        }
        
        // 设置装备 
        public void SetEquip(int id, SlotType slotId)
        {
            if (id == 0)
            {
                DespawnEquip(slotId);
                return;
            }
            
            if (!ConfigSystem.Instance.TryGet(id, out GameConfigs.EquipRowData config))
            {
                log.Error("equip config not found=" + id);
                return;
            }

            DespawnEquip(slotId);

            var req = Assets.LoadAsset(EquipPath + config.Prefab + ".prefab", typeof(GameObject));
            if (!string.IsNullOrEmpty(req.error))
            {
                log.Error("load prefab error=" + req.error);
                return;
            }

            Transform slot = GetSlot(slotId);
            Quaternion quaternion = Quaternion.identity;
            if (config.RotationLength == 3)
                quaternion = Quaternion.Euler(config.Rotation(0), config.Rotation(1), config.Rotation(2));

            Vector3 offset = Vector3.zero;
            if (config.OffsetLength == 3)
                offset = new Vector3(config.Offset(0), config.Offset(1), config.Offset(2));

            GameObject equip = GameObject.Instantiate(req.asset as GameObject, offset, quaternion, slot);
            equip.transform.localScale = new Vector3(config.Scale, config.Scale, config.Scale);
            equip.transform.localPosition = offset;
            equip.transform.localRotation = quaternion;
            m_slots.Add(slotId, equip);
        }

        /// <summary>
        /// 销毁插槽
        /// </summary>
        /// <param name="slotId"></param>
        void DespawnEquip(SlotType slotId)
        {
            if (m_slots.TryGetValue(slotId, out GameObject obj))
            {
                GameObject.Destroy(obj);
                m_slots.Remove(slotId);
            }
        }
    }
}