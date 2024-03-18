using System.Collections;
using System.Collections.Generic;
using GameConfigs;
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

        // 左手装备
        private GameObject m_weapon;

        void Awake()
        {
            int a = 99;
            a = 20;
        }
        

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

        /// <summary>
        /// 设置装备
        /// </summary>
        /// <param name="weaponID"></param>
        public void SetWeapon(int weaponID)
        {
            if (m_weapon != null)
            {
                // 删除原有装备
                GameObject.Destroy(m_weapon);
            }

            if (weaponID != 0)
            {
                if (!ConfigSystem.Instance.TryGet(weaponID, out EquipRowData config))
                {
                    log.Error("weapon id not found=" + weaponID.ToString());
                    return;
                }

                var resPath    = ConstDefine.WEAPON_PATH + config.Prefab + ".prefab";
                var req  = Assets.LoadAsset(resPath, typeof(GameObject));
                var prefab          = req.asset as GameObject;
                GameObject obj      = GameObject.Instantiate(prefab);
                Transform slot      = GetSlot(SlotType.WEAPON);
                req.Require(obj);

                obj.transform.parent        = slot;
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localRotation = Quaternion.identity;
                obj.transform.localScale    = new Vector3(config.Scale, config.Scale, config.Scale);

                if (config.OffsetLength == 3)
                {
                    obj.transform.localPosition = new Vector3(config.Offset(0), config.Offset(1), config.Offset(2));
                }

                if (config.RotationLength == 3)
                {
                    obj.transform.localRotation = Quaternion.Euler(config.Rotation(0), config.Rotation(1), config.Rotation(2));
                }

                m_weapon = obj;
            }
        }
    }
}