using System.Collections;
using System.Collections.Generic;
using GameConfigs;
using UnityEngine;

namespace SGame.UI.GameFriend
{
    using log4net;
    using SGame.Firend;
    using FairyGUI;
    using SGame.UI.Common;
    public partial class UI_FriendItem
    {
        private static ILog log = LogManager.GetLogger("game.ui");
        private FirendItemData m_friendData;

        public void SetData(FirendItemData data)
        {
            m_friendData = data;
            
            // 设置数据
            m_name.text = data.name;
            m_state.selectedIndex = data.state;

            m_equips.itemRenderer = RenderiEquips;
            m_equips.numItems = data.equips.Count;
            
            (m_head as UI_HeadBtn).SetHeadIcon(data.icon_id, data.frame_id);
        }

        void RenderiEquips(int index, GObject item)
        {
            var view = item as UI_equip;
            var data = m_friendData.equips[index];
            
            // 设置品质
            view.m_quality.selectedIndex = data.quality;
            
            // 设置类型
            if (!ConfigSystem.Instance.TryGet(data.id, out EquipmentRowData config))
            {
                log.Error("not found equipment id=" + data.id);
            }
            else
            {
                view.m_type.selectedIndex = config.Type;
            }
        }
    }
}
