using log4net;
using Unity.VisualScripting;

namespace SGame.VS
{
    [UnitTitle("GetOrder")] //The Custom Scripting Event node to receive the Event. Add "On" to the node title as an Event naming convention.
    [UnitCategory("Game/Order")]
    public class GetOrder : Unit
    {
        private static ILog log = LogManager.GetLogger("game.order");

        [DoNotSerialize]
        public ValueInput _orderID;
        
        [DoNotSerialize]
        public ValueOutput result;
        
        private OrderData resultValue;
        
        protected override void Definition()
        {
            _orderID = ValueInput<int>("OrderID");
            result = ValueOutput<OrderData>("Order", (flow) =>
            {
                var id = flow.GetValue<int>(_orderID);
                return OrderManager.Instance.Get(id);
            });
        }
    }
}