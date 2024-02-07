
using log4net;
using Unity.Entities;

namespace SGame
{
    /// <summary>
    /// 菜单进度
    /// </summary>
    public enum ORDER_PROGRESS : uint
    {
         NONE           = 0, // 未知状态
         ORDED          = 1, // 顾客下单
         FOOD_START     = 2, // 厨师接单
         FOOD_MAKING    = 3, // 制作中
         FOOD_MAKED     = 4, // 制作完成
         FOOD_READLY    = 5, // 食物已经放置到该有位置
         MOVETO_CUSTOM  = 6, // 移动到顾客身边
         FINISH         = 7, // 订单完成
    }
    
    // 订单数据 
    public class OrderData
    {
        private static ILog log = LogManager.GetLogger("game.order");
        
        public int          id {  get; private set; }          // 订单ID
        public float        startTime;                         // 下单时间
        public int          cookTime;                          // 制作时间
        public int          finishTime;                        // 订单完成时间

        public int          orderNum = 1;                      // 订单数量
        
        public int          customerID { get; private set; }   // 顾客
        public int          servicerID { get; private set; }   // 服务员ID （包含下单和清单)
        public int          cookerID { get; private set; }     // 厨师ID
        
        public bool         perfect { get; private set; }       // 是否完美菜品
        public int          dishPointID { get; private set; }   // 放餐点 talbe ID

        public Entity        foodID { get; private set; }        // 食物实例ID
        
        public int          foodType { get; private set; }      // 菜品类型
        
        public double       price;                              // 菜品价格
        
        public ORDER_PROGRESS progress { get; private set; } // 订单进度
        
        
        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="id">订单ID</param>
        /// <param name="customerID">顾客ID</param>
        /// <param name="ordermakerID">点单的服务员</param>
        /// <param name="foodID">菜品ID</param>
        /// <returns></returns>
        public static OrderData Create(int id, int customerID, int foodType)
        {
            OrderData v = new OrderData();
            v.Clear();
            v.id = id;
            v.customerID = customerID;
            v.foodType = foodType;
            v.progress = ORDER_PROGRESS.ORDED;
            EventManager.Instance.Trigger((int)GameEvent.ORDER, v.id);
            return v;
        }

        public void Clear()
        {
             this.id            = 0;                   // 订单ID
             this.startTime     = 0;                   // 下单时间
             this.cookTime      = 0;                   // 制作时间
             this.finishTime    = 0;                   // 订单完成时间
             this.customerID    = 0;                   // 顾客
             this.servicerID    = 0;                   // 服务员
             this.cookerID      = 0;                   // 厨师ID
             this.perfect       = false;               // 是否完美菜品
             this.dishPointID   = 0;                   // 放餐点
             this.foodID        = Entity.Null;         // 菜品ID
             this.price         = 0;                   // 菜品价格
             this.progress      = ORDER_PROGRESS.NONE; // 订单进度
        }

        /// <summary>
        /// 厨师接单
        /// </summary>
        /// <param name="cookerID"></param>
        /// <returns></returns>
        public bool CookerTake(int cookerID)
        {
            if (progress != ORDER_PROGRESS.ORDED)
            {
                log.Error("order progress not match!");
                return false;
            }

            this.cookerID = cookerID;
            progress = ORDER_PROGRESS.FOOD_START;
            return true;
        }

        /// <summary>
        /// 厨师开始制作菜品
        /// </summary>
        /// <param name="cookerID"></param>
        /// <returns></returns>
        public bool CookerCooking(int cookerID)
        {
            if (progress != ORDER_PROGRESS.FOOD_START)
            {
                log.Error("order progress not match!");
                return false;
            }

            progress = ORDER_PROGRESS.FOOD_MAKING;
            return true;
        }

        /// <summary>
        /// 厨师完成菜品制作, 记录食物ID, 记录食物价格
        /// </summary>
        /// <param name="cookerID"></param>
        /// <param name="foodID">食物对象</param>
        /// <param name="price">食物价格</param>
        /// <returns></returns>
        public bool CookFinish(int cookerID, Entity foodID, double price)
        {
            if (progress != ORDER_PROGRESS.FOOD_MAKING)
            {
                log.Error("order progress not match = " + progress);
                return false;
            }

            if (this.cookerID != cookerID)
            {
                // 厨师不匹配
                log.Error("cooker is not match!");
                return false;
            }

            this.foodID = foodID;
            this.price  = price;
            progress = ORDER_PROGRESS.FOOD_MAKED;
            return true;
        }

        /// <summary>
        /// 移动到放餐区
        /// </summary>
        /// <param name="cookerID">厨师ID</param>
        /// <param name="dishPointID">餐区ID</param>
        /// <returns></returns>
        public bool PutToDishPoint(int cookerID, int dishPointID)
        {
            if (progress != ORDER_PROGRESS.FOOD_MAKED)
            {
                log.Error("order progress not match!");
                return false;
            }

            if (this.cookerID != cookerID)
            {
                log.Error("cookerid not match!");
                return false;
            }

            this.dishPointID = dishPointID;
            this.progress = ORDER_PROGRESS.FOOD_READLY;
            return true;
        }

        /// <summary>
        /// 移动到顾客身边
        /// </summary>
        /// <param name="servicerID">服务员ID</param>
        /// <returns></returns>
        public bool MoveToCustom(int servicerID)
        {
            if (progress != ORDER_PROGRESS.FOOD_READLY && progress != ORDER_PROGRESS.FOOD_MAKED)
            {
                log.Error("order progress not match!");
                return false;
            }

            this.servicerID = servicerID;
            this.progress = ORDER_PROGRESS.MOVETO_CUSTOM;
            return true;
        }

        /// <summary>
        /// 完成订单
        /// </summary>
        /// <param name="servicerID"></param>
        /// <returns></returns>
        public bool Finish(int servicerID)
        {
            if (progress != ORDER_PROGRESS.MOVETO_CUSTOM)
            {
                log.Error("order progress not match!");
                return false;
            }

            if (this.servicerID != servicerID)
            {
                // 服务员不匹配， 完成订单的人必须是端盘子的人
                log.Error("ServicerID not match !"); 
                return false;
            }

            this.progress = ORDER_PROGRESS.FINISH;
            return true;
        }

        /// <summary>
        /// 判断订单是否结束
        /// </summary>
        public bool IsFinished
        {
            get { return progress == ORDER_PROGRESS.FINISH; }
        }
    }
}