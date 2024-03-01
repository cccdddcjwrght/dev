using GameTools;
using log4net;
using Unity.VisualScripting;
using Unity.Entities;
using Unity.Mathematics;

/*
 * 创建小费对象
 */
namespace SGame.VS
{
    [UnitTitle("CreateFoodTips")] //The Custom Scripting Event node to receive the Event. Add "On" to the node title as an Event naming convention.
    [UnitCategory("Game/Order")]
    public class CreateTips : Unit
    {
        private static ILog log = LogManager.GetLogger("Game.Order");
        
        [DoNotSerialize]
        public ControlInput inputTrigger;

        [DoNotSerialize]
        public ControlOutput outputTrigger;
        
        [DoNotSerialize]
        public ControlOutput outputFail;
        
        [DoNotSerialize]
        public ValueInput _gold; // 金币数量
        
        [DoNotSerialize]
        public ValueInput _tableID; // 桌子ID

        [DoNotSerialize]
        public ValueOutput _result;

        private Entity   _resultEntity;

        
        protected override void Definition()
        {
            _resultEntity = Entity.Null;
            
            // 创建订单
            inputTrigger = ControlInput("Input", (flow) =>
            {
                double gold = flow.GetValue<int>(this._gold);
                int tableID = flow.GetValue<int>(this._tableID);
                _resultEntity = Entity.Null;

                if (tableID <= 0)
                {
                    log.Error("table id invalid=" + tableID);
                    return outputFail;
                }

                var table = TableManager.Instance.Get(tableID);
                if (table == null)
                {
                    log.Error("table not found=" + tableID);
                    return outputFail;
                }
                var pos = MapAgent.CellToVector(table.map_pos.x, table.map_pos.y);
                pos.y += ConstDefine.DISH_OFFSET_Y;
                _resultEntity = FoodTipModule.Instance.CreateTips(gold, pos);
                return outputTrigger;
            });
            
            _gold           = ValueInput<double>("gold");
            _tableID        = ValueInput<int>("tableID");
            _result         = ValueOutput<Entity>("entity", (flow) => _resultEntity);
            outputTrigger   = ControlOutput("Output");
            outputFail      = ControlOutput("Fail");
        }
    }
}