using System.Collections;
using System.Collections.Generic;
using log4net;
using Unity.VisualScripting;
using UnityEngine;
using Unity.Mathematics;

namespace SGame
{
    /// <summary>
    /// 桌子创建工厂, 根据3种不同的桌子类型创建桌子 分别有 客桌, 餐桌, 操作台
    /// </summary>
    public class TableFactory
    {
        private static ILog log = LogManager.GetLogger("game.table");

		static int __COUNT = 0;

        /// <summary>
        /// 创建客桌
        /// </summary>
        /// <param name="tablePos">桌子位置</param>
        /// <param name="orderPos">放餐|订单 位置</param>
        /// <param name="customerPos">顾客位置</param>
        /// <returns>桌子对象</returns>
        public static TableData CreateCustomer(
            Vector2Int       tablePos, 
            Vector2Int       orderPos,
            int              roomAreaID,
            List<Vector2Int> customerPos
            )
        {
            TableData value = new TableData() { type = TABLE_TYPE.CUSTOM, map_pos = new int2(tablePos.x, tablePos.y), roomAreaID = roomAreaID};
            TableManager.Instance.AddTable(value);
            
            value.AddChair(CHAIR_TYPE.ORDER, new int2(orderPos.x, orderPos.y));
			foreach (var pos in customerPos)
			{
				value.AddChair(CHAIR_TYPE.CUSTOMER, new int2(pos.x, pos.y));
				__COUNT++;
			}

#if !SVR_RELEASE
			Debug.Log("椅子：" + __COUNT); 
#endif
			//TableManager.Instance.UpdateTableInfo(value);

			return value;
        }
        
        /// <summary>
        /// 创建放餐取区的桌子
        /// </summary>
        /// <param name="tablePos">桌子位置</param>
        /// <param name="takerPos">取餐位置</param>
        /// <param name="puterPos">放餐位置</param>
        /// <returns>桌子对象</returns>
        public static TableData CreateDish(Vector2Int tablePos, Vector2Int takerPos, Vector2Int puterPos)
        {
            TableData value = new TableData() { type = TABLE_TYPE.DISH, map_pos = new int2(tablePos.x, tablePos.y)};

            TableManager.Instance.AddTable(value);
            value.AddChair(CHAIR_TYPE.CUSTOMER, new int2(takerPos.x, takerPos.y));
            value.AddChair(CHAIR_TYPE.ORDER, new int2(puterPos.x, puterPos.y));
            //TableManager.Instance.UpdateTableInfo(value);
            
             log.Debug(string.Format("Create Dish tablePos={0}, takerPos={1}, puterPos={2}", tablePos, takerPos, puterPos) );
             return value;
        }
        
        
        /// <summary>
        /// 创建食物生产桌子
        /// </summary>
        /// <param name="tablePos">桌子位置</param>
        /// <param name="foodType">食物类型</param>
        /// <param name="machineID">机器ID</param>
        /// <param name="roomAreaID">房间区域ID</param>
        /// <param name="operatorPos">机器操作区</param>
        /// <returns>桌子对象</returns>
        public static TableData CreateFood(Vector2Int tablePos, int machineID, int foodType, int roomAreaID, Vector2Int operatorPos)
        {
            Debug.Log("Create Food Type=" + foodType);
            TableData value         = new TableData() { type = TABLE_TYPE.MACHINE, map_pos = new int2(tablePos.x, tablePos.y)};
            value.machineID         = machineID;
            value.foodType          = foodType;
            value.roomAreaID        = roomAreaID;
            TableManager.Instance.AddTable(value);
            
            // 添加操作台
            value.AddChair(CHAIR_TYPE.OPERATOR, new int2(operatorPos.x, operatorPos.y));
            TableManager.Instance.UpdateTableInfo(value);

            log.Debug(string.Format("Create FooTable tablePos={0}, machineID={1}, foodType={2}, operatorPos={3}", tablePos, machineID, foodType, operatorPos) );
            return value;
        }
    }
}
