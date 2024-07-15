using log4net;
using Unity.VisualScripting;


namespace SGame.VS
{
    // 弹出正在等待的座椅
    [UnitTitle("PopOrderTask")] 
    [UnitCategory("Game/Table")]
    public class PopOrderTask : Unit
    {
        [DoNotSerialize]
        public ControlInput inputTrigger;

        // 成功返回流程
        [DoNotSerialize]
        public ControlOutput    outputSuccess;
        
        // 成功返回流程
        [DoNotSerialize]
        public ControlOutput    outputFail;
        
        [DoNotSerialize]
        public ValueInput       OrderType;

        [DoNotSerialize]
        public ValueOutput       Order;
        
        private OrderData        m_resultOrder;
        
        // 端口定义
        protected override void Definition()
        {
            inputTrigger = ControlInput("Input", (flow) =>
            {
                // Making the resultValue equal to the input value from myValueA concatenating it with myValueB.
                var orderType = flow.GetValue<ORDER_PROGRESS>(OrderType);
                var tasks = OrderTaskManager.Instance.GetOrCreateTask(orderType);
                if (tasks.Count == 0)
                    return outputFail;

                m_resultOrder = tasks.Pop();
                return outputSuccess;
            });
            
            outputSuccess   = ControlOutput("Success");
            outputFail      = ControlOutput("Fail");
            Order           = ValueOutput<OrderData>("Order", (flow) => m_resultOrder);
            OrderType       = ValueInput<ORDER_PROGRESS>("Type", ORDER_PROGRESS.WAIT_ORDER);
        }
    }
}
