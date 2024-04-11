using System.Collections;
using System.Collections.Generic;
using GameConfigs;
using libx;
using log4net;
using UnityEngine;
using Unity.Entities;

namespace SGame
{
    public enum SlotType
    {
        LWEAPON     = 0, // 左手武器
        RWEAPON     = 1, // 右手武器武器
        FOOD        = 2, // 食物挂点
        HUD         = 3, // 头顶HUD信息挂点
        FOOT        = 4, // 脚步挂点
        BODY        = 5, // main, 身体挂点
        GLASSES     = 6, // 眼镜挂点
    }
    
    public class Equipments : MonoBehaviour
    {
        private static ILog         log = LogManager.GetLogger("game.equipments");

        // 插槽对象
        public Dictionary<SlotType, Transform> m_slots = new Dictionary<SlotType, Transform>();

        // 左手装备
        //private GameObject m_weapon;

        // 武器容器
        public Dictionary<SlotType, GameObject> m_weapons = new Dictionary<SlotType, GameObject>();

        // 特效容器
        public Dictionary<SlotType, Entity>     m_effects = new Dictionary<SlotType, Entity>();

        void Start()
        {
            UpdateModel();
        }

        /// <summary>
        /// 清空容器内的对象
        /// </summary>
        public void Clear()
        {
            ClearEffects();
            ClearWeapons();
        }

        public void ClearWeapons()
        {
            // 清空武器
            foreach (var w in m_weapons)
            {
                GameObject.Destroy(w.Value);
            }
            m_weapons.Clear();
        }

        public void ClearEffects()
        {
            foreach (var eff in m_effects)
            {
                EffectSystem.Instance.CloseEffect(eff.Value);
            }
            m_effects.Clear();
        }

        /// <summary>
        /// 删除特定挂点上的武器
        /// </summary>
        /// <param name="slot"></param>
        public void ClearWeapon(SlotType slot)
        {
            if (m_weapons.TryGetValue(slot, out GameObject value))
            {
                GameObject.Destroy(value);
                m_weapons.Remove(slot);
            }
        }

        /// <summary>
        /// 删除特定挂点上的特效
        /// </summary>
        /// <param name="slot"></param>
        public void ClearEffect(SlotType slot)
        {
            if (m_effects.TryGetValue(slot, out Entity value))
            {
                EffectSystem.Instance.CloseEffect(value);
                m_effects.Remove(slot);
            }
        }
        
        public void UpdateModel()
        {
            m_slots.Clear();
            var weapon   = transform.FindRecursive("weapon-2");
            var weapon1  = transform.FindRecursive("weapon-1");
            var glasses  = transform.FindRecursive("glasses");
            var good     = transform.FindRecursive("goods");
            var foot     = transform.FindRecursive("foot");
            var main     = transform.FindRecursive("main");
            
            // 初始化插槽
            m_slots.Add(SlotType.RWEAPON, weapon);     // 右手武器
            m_slots.Add(SlotType.LWEAPON, weapon1);    // 左手武器
            m_slots.Add(SlotType.FOOD,    good);       // 食物挂点
            m_slots.Add(SlotType.HUD,     glasses);    // HUD挂点
            m_slots.Add(SlotType.FOOT,    foot);       //      = 4, // 脚步挂点
            m_slots.Add(SlotType.BODY,    main);       //      = 4, // 脚步挂点
            m_slots.Add(SlotType.GLASSES,    glasses); //      = 4, // 脚步挂点
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
            // 清空所有武器
            if (weaponID == 0)
            {
                ClearWeapons();
                return;
            }
            

            if (weaponID != 0)
            {
                if (!ConfigSystem.Instance.TryGet(weaponID, out EquipRowData config))
                {
                    log.Error("weapon id not found=" + weaponID.ToString());
                    return;
                }

                var slotType = (SlotType)config.Slot;
                var resPath    = ConstDefine.WEAPON_PATH + config.Prefab + ".prefab";
                var req  = Assets.LoadAsset(resPath, typeof(GameObject));
                var prefab          = req.asset as GameObject;
                GameObject obj      = GameObject.Instantiate(prefab);
                Transform slot      = GetSlot(slotType);
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

                ClearWeapon(slotType);
                m_weapons.Add(slotType, obj);
            }
        }

        /// <summary>
        /// 角色添加特效
        /// </summary>
        /// <param name="effectId"></param>
        public void SetEffect(int effectId)
        {
            if (effectId == 0)
            {
                ClearEffects();
                return;
            }
            
            if (!ConfigSystem.Instance.TryGet(effectId, out EquipRowData config))
            {
                log.Error("effect id not found=" + effectId.ToString());
                return;
            }
            
            var slotType        = (SlotType)config.Slot;
            Transform slot      = GetSlot(slotType);
            var eff = EffectSystem.Instance.Spawn3d(effectId, slot.gameObject);
            ClearEffect(slotType);
            m_effects.Add(slotType, eff);
        }

        /// <summary>
        /// 拆分部位
        /// </summary>
        /// <param name="part"></param>
        /// <param name="rolePart"></param>
        /// <param name="weapon"></param>
        /// <returns></returns>
        public static bool GetRolePart(string part, out string rolePart, out int weapon)
        {
            weapon = 0;
            rolePart = null;
            var index = part.LastIndexOf("|weapon|");

            if (index <= 0)
            {
                rolePart = part;
                return true;
            }

            // 找到武器
            rolePart = null;
            
            return true;
        }
    }
}