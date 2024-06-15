using log4net;
using SGame;
using Unity.VisualScripting;
using Unity.Mathematics;
using UnityEngine;
using System.Collections.Generic;
using SGame.Firend;
using Unity.Entities;

namespace SGame.VS
{
    /// <summary>
    /// 获得关卡路径点的点单位置
    /// </summary>
	
	[UnitTitle("GetLevelPathOrderChair")] 
    [UnitCategory("Game/Car")]
    public sealed class GetLevelPathOrderChair : Unit
    {
        private static ILog log = LogManager.GetLogger("game.character");
        
        [DoNotSerialize]
        public ControlInput     inputTrigger;

        // 成功返回流程
        [DoNotSerialize]
        public ControlOutput    successTrigger;
        
        // 生工返回流程
        [DoNotSerialize]
        public ControlOutput    failTrigger;

        /// <summary>
        /// 路径点名称
        /// </summary>
        [DoNotSerialize]
        public ValueInput pathTag { get; private set; }
        

        /// <summary>
        /// 创建的角色Entity
        /// </summary>
        public ValueOutput chairData { get; private set; }
        private ChairData  m_chairData;

        protected override void Definition()
        {
            inputTrigger = ControlInput("Input", (flow) =>
            {
                var tag         = flow.GetValue<string>(pathTag);
                CarQueue carQueue = CarQueueManager.Instance.GetOrCreate(tag);
                if (carQueue == null)
                {
                    log.Error("pathTag not found=" + tag);
                    return failTrigger;
                }

                if (carQueue.tableID <= 0)
                {
                    return failTrigger;
                }

                var t = TableManager.Instance.Get(carQueue.tableID);
                if (t == null)
                {
                    log.Error("table not found id=" + carQueue.tableID);
                    return failTrigger;
                }
                
                m_chairData = t.GetFirstChair(CHAIR_TYPE.CUSTOMER);
                if (m_chairData == ChairData.Null)
                {
                    log.Error("table not found customer chair pos=" + t.map_pos);
                    return failTrigger;
                }
                
                return successTrigger;
            });
            
            pathTag          = ValueInput<string>("pathTag", "");
            chairData        = ValueOutput<ChairData>("Result", (flow)=> m_chairData);
            successTrigger   = ControlOutput("Success");
            failTrigger      = ControlOutput("Fail");
        }
    }
}
