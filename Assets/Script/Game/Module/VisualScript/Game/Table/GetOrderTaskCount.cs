using log4net;
using Unity.VisualScripting;


namespace SGame.VS
{
    // 弹出正在等待的座椅
    [UnitTitle("GetOrderTaskCount")] 
    [UnitCategory("Game/Table")]
    public class GetOrderTaskCount : Unit
    {
        [DoNotSerialize]
        public ControlInput inputTrigger;

        // 成功返回流程
        [DoNotSerialize]
        public ControlOutput    outputTrigger;

        [DoNotSerialize]
        public ValueInput       OrderType;

        [DoNotSerialize]
        public ValueOutput       _Count;
        
        private int              m_count;
        
        // 端口定义
        protected override void Definition()
        {
            inputTrigger = ControlInput("Input", (flow) =>
            {
                // Making the resultValue equal to the input value from myValueA concatenating it with myValueB.
                var orderType = flow.GetValue<ORDER_PROGRESS>(OrderType);
                var tasks = OrderTaskManager.Instance.GetOrCreateTask(orderType);
                m_count = tasks.Count;
                return outputTrigger;
            });
            
            outputTrigger   = ControlOutput("Output");
            _Count           = ValueOutput<int>("num", (flow) => m_count);
            OrderType       = ValueInput<ORDER_PROGRESS>("Type", ORDER_PROGRESS.WAIT_ORDER);
        }
    }
}
