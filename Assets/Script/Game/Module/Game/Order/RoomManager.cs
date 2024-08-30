using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame
{
    /// <summary>
    /// 分配订单的管理员
    /// </summary>
    public class RoomManager
    {
        private static int TAKE_ORDER_ROLE_MASK = 0;
        private static int COOK_ROLE_MASK = 0;
        private static int CHECK_ROLE_MASK = 0;
        private static int TAKE_FOOD_MAKE = 0;
        private List<Character> m_characterCache = null;
        private List<OrderTaskItem> m_taskCache = null;
        
        public RoomManager()
        {
            CHECK_ROLE_MASK         = BitOperator.GetMask((int)EnumRole.Cook, (int)EnumRole.Waiter, (int)EnumRole.Player);
            COOK_ROLE_MASK          = CHECK_ROLE_MASK;
            TAKE_ORDER_ROLE_MASK    = BitOperator.GetMask((int)EnumRole.Waiter, (int)EnumRole.Player);
            TAKE_FOOD_MAKE          = TAKE_ORDER_ROLE_MASK;//BitOperator.GetMask((int)EnumRole.Cook, (int)EnumRole.Player);
            m_characterCache        = new List<Character>();
            m_taskCache             = new List<OrderTaskItem>();
        }
        
        /// <summary>
        /// 运行餐厅经理脚本
        /// </summary>
        /// <returns></returns>
        public IEnumerator Run()
        {
            List<Character> characterCache = new List<Character>();
            while (true)
            {
                yield return null;

                // 当前没有任务
                if (OrderTaskManager.Instance.TaskCount <= 0)
                    continue;

                // 判断是否拥有空闲角色
                if (CharacterModule.Instance.FindFirst((character) => !character.IsDead
                    && character.isIdle
                    && BitOperator.Get(CHECK_ROLE_MASK, character.roleType)) == null)
                {
                    continue;
                }

                // 3. 分配任务
                DispatchTask();
            }
        }

        /// <summary>
        /// 配分服务员
        /// </summary>
        /// <returns></returns>
        void DispatchTaskOrker(int workArea)
        {
            int roleTypeMask = TAKE_ORDER_ROLE_MASK;
            int maxTakeOrderNum = 3;
            
            var tasks = OrderTaskManager.Instance.GetOrCreateTask(ORDER_PROGRESS.WAIT_ORDER);
            if (tasks.Count == 0)
                return;
            
            // 查找符合条件的角色
            CharacterModule.Instance.FindCharacters(m_characterCache, (character) =>
                !character.IsDead
                && BitOperator.Get(character.workerAread, workArea)      // 区域匹配
                //&& character.takeOrderNum <  maxTakeOrderNum                    // 连续接单数量
                && BitOperator.Get(roleTypeMask, character.roleType));

            // 处理订单任务
            for (int i = 0; i < tasks.Count; i++)
            {
                var t = tasks.Get(i);
                var orderPos = t.GetCustomerPos();
                foreach (var c in m_characterCache)
                {
                    var workTime = c.GetWorkTime(orderPos);
                    t.UpdateScore(c.CharacterID, workTime);
                }
            }
        }
        

        /// <summary>
        /// 任务分配给厨师
        /// </summary>
        /// <param name="workArea"></param>
        /// <returns></returns>
        void DispathTaskCook(int workArea)
        {
            int roleTypeMask = COOK_ROLE_MASK;
            
            // 查找符合条件的角色
            CharacterModule.Instance.FindCharacters(m_characterCache, (character) =>
                !character.IsDead
                && BitOperator.Get(character.workerAread, workArea) // 区域匹配
                && BitOperator.Get(roleTypeMask, character.roleType));

            if (m_characterCache.Count == 0)
                return;

            // 找到符合条件的任务
            FindChefTask(workArea, m_taskCache);
            if (m_taskCache.Count == 0)
                return;


            // 派发任务标记
            foreach (var t in m_taskCache)
            {
                foreach (var c in m_characterCache)
                {
                    var workTime = c.GetWorkTime(t.GetWorkerPos());
                    t.UpdateScore(c.CharacterID, workTime);
                }
            }
        }

        /// <summary>
        /// 查找空闲的任务, 并将标记其制作工作台
        /// </summary>
        /// <param name="workerArea"></param>
        /// <param name="ret"></param>
        /// <returns></returns>
        static bool FindChefTask(int workerArea, List<OrderTaskItem> ret)
        {
            ret.Clear();
            var tasks = OrderTaskManager.Instance.GetOrCreateTask(ORDER_PROGRESS.ORDED);
            var tableManager = TableManager.Instance;

            for (int i = 0; i < tasks.Count; i++)
            {
                var task = tasks.Get(i);
                TableData t = tableManager.GetFoodTable(task.m_order.foodType);
                if (t == null || t.workAreaID != workerArea)
                {
                    // 只匹配符合区域的订单
                    continue;
                }
                    
                // 已经获取过了
                if (task.isReadly) //.m_order.cookerID == -1)
                {
                    ret.Add(task);
                    continue;
                }
                    
                // 找到可用的桌子
                var workChair = tableManager.FindMachineChairFromFoodType(task.m_order.foodType);
                if (workChair.IsNull)
                    continue;

                // 锁定座位
                tableManager.SitChair(workChair, -1);
                    
                // 保存座位
                task.m_order.CookerTake(-1, workChair);

                // 添加
                ret.Add(task);
            }
            
            return true;
        }

        /// <summary>
        /// 分配订单去取食物
        /// </summary>
        void DispathTaskGetFood(int workArea)
        {
            int roleTypeMask = TAKE_FOOD_MAKE;
            
            var tasks = OrderTaskManager.Instance.GetOrCreateTask(ORDER_PROGRESS.FOOD_READLY);
            if (tasks.Count <= 0)
                return;
            
            // 查找符合条件的角色
            CharacterModule.Instance.FindCharacters(m_characterCache, (character) =>
                !character.IsDead
                && BitOperator.Get(character.workerAread, workArea) // 区域匹配
                && BitOperator.Get(roleTypeMask, character.roleType));

            if (m_characterCache.Count <= 0)
                return;

            for (int i = 0; i < tasks.Count; i++)
            {
                var t = tasks.Get(i);
                foreach (var c in m_characterCache)
                {
                    var workTime = c.GetWorkTime(t.GetPosition());
                    t.UpdateScore(c.CharacterID, workTime);
                }
            }
        }

        /// <summary>
        /// 派发任务
        /// </summary>
        void DispatchTask()
        {
            /// 对要派发的任务进行标记
            GameProfiler.BeginSample("DispatchTask");
            DispatchTaskOrker(0);

            for (int i = 0; i < 30; i++)
                DispathTaskCook(i);
         
            DispathTaskGetFood(0);
            GameProfiler.EndSample();
            
            /// 实际分配任务
            GameProfiler.BeginSample("DispatchTaskEvent");
            OrderTaskManager.Instance.DispatchEvent();
            GameProfiler.EndSample();
        }
    }
}
