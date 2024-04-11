using System.Collections;
using System.Collections.Generic;
using log4net;
using UnityEngine;
namespace SGame.UI.TomorrowGift
{
    using FairyGUI;
    using GameConfigs;
    using System;
    
    public partial class UI_CommomGift
    {
        private static ILog log = LogManager.GetLogger("ui.commgift");
        
        private int         m_goodID;
        private ShopRowData m_config;
        private Action      m_onInfoClick;
        
        public void Initalize(int goodID, Action systemInfoClick)
        {
            if (!ConfigSystem.Instance.TryGet(goodID, out ShopRowData config))
            {
                log.Error("good id not found=" + TomorrowGiftModule.GOOD_ITEM_ID);
                return;
            }
            
            m_goodID = goodID;
            m_config = config;
            m_onInfoClick = systemInfoClick;
            m_GiftItems.itemRenderer = RenderGiftItems;

            m_GiftItems.numItems = GetItemNum();
        }

        int GetItemNum()
        {
            if (m_config.Item4Length > 0)
                return 4;
            
            if (m_config.Item3Length > 0)
                return 3;
            
            if (m_config.Item2Length > 0)
                return 2;

            if (m_config.Item1Length > 0)
                return 1;

            return 0;
        }

        struct ItemConfigData
        {
            public int type; // 道具类型
            public int id;   // 道具ID
            public int num;  // 道具数量
            
            /// <summary>
            /// 判断是否是装备宝箱
            /// </summary>
            /// <returns></returns>
            public bool IsTreasureChest()
            {
                if (type != 1)
                    return false;

                // 道具类型
                if (!ConfigSystem.Instance.TryGet(id, out ItemRowData itemConfig))
                {
                    return false;
                }

                return itemConfig.Type == 3;
            }

        }

        ItemConfigData GetItemConfigDAta(int index)
        {
            switch (index)
            {
                case 0:
                    return new ItemConfigData() { type = m_config.Item1(0), id = m_config.Item1(1), num = m_config.Item1(2) };
                case 1:
                    return new ItemConfigData() { type = m_config.Item2(0), id = m_config.Item2(1), num = m_config.Item2(2) };
                case 2:
                    return new ItemConfigData() { type = m_config.Item3(0), id = m_config.Item3(1), num = m_config.Item3(2) };
                case 3:
                    return new ItemConfigData() { type = m_config.Item4(0), id = m_config.Item4(1), num = m_config.Item4(2) };
            }

            return new ItemConfigData{type = 0, id = 0, num = 0};
        }

        /// <summary>
        /// 按下概率详情
        /// </summary>
        void OnClickTreasureChest()
        {
            m_onInfoClick?.Invoke();
        }

        /// <summary>
        /// 显示每个ITEM
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        void RenderGiftItems(int index, GObject item)
        {
            UI_Item uiItem = item as UI_Item;
            ItemConfigData data = GetItemConfigDAta(index);
            var icon = Utils.GetItemIcon(data.type, data.id);//data[0], data[1]);
            uiItem.m_title.text = data.num.ToString();
            uiItem.SetIcon(icon);

            // 不同的类型不同的显示
            bool isTreasureChest = data.IsTreasureChest();
            if (isTreasureChest)
            {
                uiItem.m_sizesetting.selectedIndex = 1;
                uiItem.m_tipicon.selectedIndex = 1;
                uiItem.onClick.Set(OnClickTreasureChest);
            }
            else
            {
                uiItem.m_sizesetting.selectedIndex = 0;
                uiItem.m_tipicon.selectedIndex = 0;
                uiItem.onClick.Clear();
            }
        }
    }
}