using System;
using log4net;
using Unity.VisualScripting;


namespace SGame.VS
{
    // 弹出正在等待的座椅
    [UnitTitle("PushOrderTask")] 
    [UnitCategory("Game/Table")]
    public class PushOrderTask : Unit
    {
        [DoNotSerialize]
        public ControlInput inputTrigger;

        // 成功返回流程
        [DoNotSerialize]
        public ControlOutput    outputTrigger;
        
        [DoNotSerialize]
        public ValueInput       Order;
        
        
        // 端口定义
        protected override void Definition()
        {
            inputTrigger = ControlInput("Input", (flow) =>
            {
                var order = flow.GetValue<OrderData>(Order);
                if (order == null)
                    throw new Exception("value is null");
                
                // Making the resultValue equal to the input value from myValueA concatenating it with myValueB.
                var orderType = order.progress;
                var tasks = OrderTaskManager.Instance.GetOrCreateTask(orderType);
                tasks.Add(order);
                return outputTrigger;
            });
            
            outputTrigger   = ControlOutput("Output");
            Order           = ValueInput<OrderData>("Order");
        }
    }
}
