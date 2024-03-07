/*
 * 功能, 当多个模块调用UI 锁定的时候, 只需要管理自己的状态就行了
 * 当出现1个以上的锁定, 结果是锁定. 当所有锁定解除的时候 结果才是解锁状态, 类似一个简单的信号机
 * 使用例子:
 * UILockManager.Instance.Require("guild")
 * UILockManager.Instance.Release("guild")
 *
 * UILockManager.Instance.Require("rank")
 * UILockManager.Instance.Release("rank")
 */

using System.Text;
using log4net;
namespace SGame
{
    /// <summary>
    /// FAIRYGUI 锁定功能管理器
    /// </summary>
    public class UILockManager : SGame.Singleton<UILockManager>
    {
        private static ILog log = LogManager.GetLogger("Game.UILockManager");
        
        public UILockManager()
        {
            m_values = new MultiCounter<string>();
        }

        /// <summary>
        /// 判断UI是否已锁
        /// </summary>
        public bool isLocked
        {
            get { return !FairyGUI.GRoot.inst.touchable; }
        }


        /// <summary>
        /// 锁定UI输入, 不让玩家输入
        /// </summary>
        void LockUI()
        {
            if (FairyGUI.GRoot.inst.touchable == true)
            {
                FairyGUI.GRoot.inst.touchable = false; 
                EventManager.Instance.Trigger((int)GameEvent.UI_INPUT_LOCK);
            }
        }

        /// <summary>
        /// 解锁UI锁定状态
        /// </summary>
        void UnLockUI()
        {
            if (FairyGUI.GRoot.inst.touchable == false)
            {
                FairyGUI.GRoot.inst.touchable = true;
                EventManager.Instance.Trigger((int)GameEvent.UI_INPUT_UNLOCK);
            }
        }

        /// <summary>
        /// 获得对应模块锁定状态
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int GetLock(string name)
        {
            return m_values.Get(name);
        }

        /// <summary>
        /// 对应模块调用锁定
        /// </summary>
        /// <param name="name"></param>
        public void Require(string name)
        {
            m_values.Add(name, 1);
            UpdateState();
        }

        /// <summary>
        /// 对应模块调用解锁
        /// </summary>
        /// <param name="name"></param>
        public void Release(string name)
        {
            m_values.Add(name, -1);
            UpdateState();
        }

        /// <summary>
        /// 清除对应 ID 的锁定
        /// </summary>
        /// <param name="name"></param>
        void Clear(string name)
        {
            m_values.Remove(name);
            UpdateState();
        }

        /// <summary>
        /// 清除所有
        /// </summary>
        public void ClearAll()
        {
            m_values.Clear();
            UpdateState();
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        public void UpdateState()
        {
            bool oldValue = isLocked;
			if (m_values.HasValue())
            {
                LockUI();
            }
            else
            {
                UnLockUI();
            }

            ShowLockState(oldValue != isLocked);
        }

        /// <summary>
        /// 显示锁定状态
        /// </summary>
        /// <param name="isChange"></param>
        void ShowLockState(bool isChange)
        {
            /// 打印UI锁定状态
            if (isLocked)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("lock ");
                foreach (var v in m_values.m_counters)
                {
                    sb.Append("name=" + v.Key + ":" + v.Value + " , ");
                }
                log.Info(sb.ToString());
            }
            else
            {
                // 有修改才记录
                if (isChange)
                    log.Info("unlock");
            }
        }

        private MultiCounter<string> m_values;
    }
}
