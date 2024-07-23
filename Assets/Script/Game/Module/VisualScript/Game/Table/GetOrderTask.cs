using log4net;
using Unity.VisualScripting;


namespace SGame.VS
{
    // 弹出正在等待的座椅
    [UnitTitle("GetOrderTask")] 
    [UnitCategory("Game/Table")]
    public class GetOrderTask : Unit
    {
        [DoNotSerialize]
        public ControlInput inputTrigger;

        // 成功返回流程
        [DoNotSerialize]
        public ControlOutput    outputTrigger;

        [DoNotSerialize]
        public ValueInput       OrderType;

        [DoNotSerialize]
        public ValueOutput       _Tasks;
        
        private OrderTask        m_tasks;
        
        // 端口定义
        protected override void Definition()
        {
            inputTrigger = ControlInput("Input", (flow) =>
            {
                // Making the resultValue equal to the input value from myValueA concatenating it with myValueB.
                var orderType = flow.GetValue<ORDER_PROGRESS>(OrderType);
                m_tasks = OrderTaskManager.Instance.GetOrCreateTask(orderType);
                return outputTrigger;
            });
            
            outputTrigger   = ControlOutput("Output");
            _Tasks           = ValueOutput<OrderTask>("tasks", (flow) => m_tasks);
            OrderType       = ValueInput<ORDER_PROGRESS>("Type", ORDER_PROGRESS.WAIT_ORDER);
        }
    }
}
