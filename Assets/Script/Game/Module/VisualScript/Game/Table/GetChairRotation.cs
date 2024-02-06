using System;
using log4net;
using SGame;
using Unity.VisualScripting;
using UnityEngine;

namespace SGame.VS
{
    /// <summary>
    /// 获取座位旋转角度
    /// </summary>
	[UnitTitle("GetChairRotation")] 
    [UnitCategory("Game/Table")]
    public sealed class GetChairRotation : Unit
    {
        private ILog log = LogManager.GetLogger("game.table");
        
        /// <summary>
        /// The value of the variable.
        /// </summary>
        [DoNotSerialize]
        [PortLabelHidden]
        public ValueOutput value { get; private set; }
        
        [DoNotSerialize]
        public ValueInput chairData { get; private set; }

        protected override void Definition()
        {
            value         = ValueOutput<float>("degree ", Get);//.PredictableIf(IsDefined);
            chairData     = ValueInput<ChairData>("chair");
        }
        
        private float Get(Flow flow)
        {
            var chair = flow.GetValue<ChairData>(chairData);
            if (chair.tableID <= 0)
            {
                log.Error("not valid chair");
                return -1;
            }

            var table = TableManager.Instance.Get(chair.tableID);
            if (table == null)
            {
                log.Error("table not found");
                return -1;
            }

            // 不使用反
            var intDir = table.map_pos - chair.map_pos;
            return Utils.GetRotation(intDir.x, intDir.y);
        }
    }
}
