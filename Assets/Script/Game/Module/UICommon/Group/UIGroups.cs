
using Unity.Entities;
using Unity.VisualScripting;
using System.Collections.Generic;
using log4net;
using UnityEngine;

namespace SGame.UI
{
    /// <summary>
    /// UI 组, UI 内部根据
    /// </summary>
    public class UIGroups //: IUIGroup
    {
        public UISTATE_CHANGE onUIEvent { get; set; }

        private static ILog log = LogManager.GetLogger("ui.group");

        public enum GROUP_TYPE
        {
            NORMAL = 0,   // 普通UI组, 有就是显示, 没有就不显示
            STACK = 1,    // 堆栈UI组
            PRIORITY = 2, // 根据优先级展示
        }

        class UIState
        {
            public int       id;          // UI 的ID
            public bool      isShow;      // 当前UI是否显示
            public int       priority;    // 优先级
            public List<int> groups;      // 该UI 所属的UI组
        }

        /// <summary>
        /// UI数据
        /// </summary>
        private Dictionary<int, UIState> m_datas;

        /// <summary>
        /// UI组数据
        /// </summary>
        private Dictionary<int, IUIGroup> m_groups;

        /// <summary>
        /// 判断UI是否显示
        /// </summary>
        /// <param name="ui_id"></param>
        /// <returns></returns>
        public bool IsShow(int ui_id)
        {
            if (!m_datas.TryGetValue(ui_id, out UIState info))
            {
                return false;
            }

            return info.isShow;
        }

        /// <summary>
        /// 通过UI 状态获得最新UI
        /// </summary>
        /// <param name="ui_id"></param>
        /// <returns></returns>
        bool GetShowState(int ui_id)
        {
            if (!m_datas.TryGetValue(ui_id, out UIState info))
            {
                return false;
            }

            // 只要有一个显示false 就是false
            foreach (var group_id in info.groups)
            {
                if (GetGroup(group_id).IsShow(ui_id))
                    return false;
            }

            return true;
        }

        // 更新ui_id
        /// <summary>
        /// 更新UI显示装填
        /// </summary>
        /// <param name="ui_id"></param>
        /// <returns>是否成功更新状态, 状态不变不用更新</returns>
        bool UpdateUI()
        {
            var oldList = GetUITemp();
            List<UIState> changes = new List<UIState>();
            foreach (var v in m_datas.Keys)
            {
                var right = m_datas[v];
                if (oldList.ContainsKey(v))
                {
                    var left = oldList[v];
                    if (left.isShow != right.isShow)
                    {
                        changes.Add(right);
                    }
                }
                else
                {
                    changes.Add(right);
                }
            }

            // 没有修改状态的UI
            if (changes.Count == 0)
                return false;

            if (onUIEvent != null)
            {
                foreach (var v in changes)
                {
                    onUIEvent?.Invoke(v.id);
                }
            }

            return false;
        }

        IUIGroup GetGroup(int group_id)
        {
            return m_groups[group_id];
        }

        UIState CreateUIInfo(int ui_id)
        {
            if (!ConfigSystem.Instance.TryGet(ui_id, out GameConfigs.ui_resRowData info))
            {
                return null;
            }

            UIState data = new UIState()
            {
                id      = ui_id,
                isShow  = false,
                groups  = new List<int>(info.GroupLength)
            };
            for (int i = 0; i < info.GroupLength; i++)
            {
                data.groups.Add(info.Group(i));
            }
            return data;
        }

        Dictionary<int, UIState> GetUITemp()
        {
            return null;
        }

        /// <summary>
        /// 打开UI
        /// </summary>
        /// <param name="ui_id"></param>
        public bool OpenUI(int ui_id)
        {
            bool isChange = false;
            var oldList = GetUITemp();
            if (m_datas.TryGetValue(ui_id, out UIState info))
            {
                foreach (var g in info.groups)
                {
                    GetGroup(g).OpenUI(ui_id, info.priority);
                }
            }

            List<UIState> changes = new List<UIState>();
            foreach (var v in m_datas.Keys)
            {
                var right = m_datas[v];
                if (oldList.ContainsKey(v))
                {
                    var left = oldList[v];
                    if (left.isShow != right.isShow)
                    {
                        changes.Add(right);
                    }
                }
                else
                {
                    changes.Add(right);
                }
            }

            if (onUIEvent != null)
            {
                foreach (var v in changes)
                {
                    onUIEvent?.Invoke(v.id);
                }
            }
            return false;
        }
        
        
        /// <summary>
        /// 关闭UI
        /// </summary>
        /// <param name="ui_id"></param>
        public bool CloseUI(int ui_id)
        {
            return false;
        }

        IUIGroup GetOrCreateGroup(int groupId)
        {
            if (m_groups == null)
            {
                m_groups = new Dictionary<int, IUIGroup>();
            }

            if (m_groups.TryGetValue(groupId, out IUIGroup ret))
            {
                return ret;
            }

            if (!ConfigSystem.Instance.TryGet(groupId, out GameConfigs.ui_groupsRowData groupInfo))
            {
                log.Error("not found group id=" + groupId);
                return null;
            }

            ret = CreateCroup((GROUP_TYPE)groupInfo.Type);
            if (ret != null)
            {
                m_groups.Add(groupId, ret);
            }
            return ret;
        }

        /// <summary>
        /// UI 组创建工程
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        static IUIGroup CreateCroup(GROUP_TYPE t)
        {
            switch (t)
            {
                case GROUP_TYPE.STACK:
                    return new UIGroupStack();
                case GROUP_TYPE.NORMAL:
                    return new UIGroupNormal();
                case GROUP_TYPE.PRIORITY:
                    return new UIGroupPriority();
            }

            return null;
        }
    }
}
