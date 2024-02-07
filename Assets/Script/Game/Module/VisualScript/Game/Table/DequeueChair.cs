using log4net;
using Unity.VisualScripting;


namespace SGame.VS
{
    // 弹出正在等待的座椅
    [UnitTitle("DequeueChair")] 
    [UnitCategory("Game/Table")]
    public class DequeueChair : Unit
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
        public ValueOutput       _out_chair;
        
        private ChairData       m_resultChair;
        
        // 端口定义
        protected override void Definition()
        {
            inputTrigger = ControlInput("Input", (flow) =>
            {
                // Making the resultValue equal to the input value from myValueA concatenating it with myValueB.
                if (!WaitForServiceChair.Instance.Dequeue(out m_resultChair))
                {
                    return outputFail;
                }

                return outputSuccess;
            });
            outputSuccess   = ControlOutput("Success");
            outputFail      = ControlOutput("Fail");
            _out_chair      = ValueOutput<ChairData>("chair", (flow) => m_resultChair);
        }
    }
}
