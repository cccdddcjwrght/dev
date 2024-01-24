using System.Collections;
using UnityEngine;
using Unity.VisualScripting;

namespace SGame.VS
{
    /// <summary>
    /// 等待订单状态
    /// </summary>
    [UnitTitle("WaitOrder")]
    [UnitOrder(1)]
    public class WaitOrder : WaitUnit
    {
        /// <summary>
        /// The number of seconds to await.
        /// </summary>
        [DoNotSerialize]
        [PortLabel("Order")]
        public ValueInput _Order { get; private set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        [DoNotSerialize]
        [PortLabel("OrderState")]
        public ValueInput _Progress { get; private set; }

        protected override void Definition()
        {
            base.Definition();

            _Order      = ValueInput<OrderData>(nameof(_Order), null);
            _Progress   = ValueInput<ORDER_PROGRESS>(nameof(_Progress), ORDER_PROGRESS.WAIT);

            Requirement(_Order, enter);
            Requirement(_Progress, enter);
        }

        protected override IEnumerator Await(Flow flow)
        {
            var order = flow.GetValue<OrderData>(this._Order);
            var progress = flow.GetValue<ORDER_PROGRESS>(_Progress);

            while (order.progress < progress)
                yield return null;

            yield return exit;
        }
    }
}
