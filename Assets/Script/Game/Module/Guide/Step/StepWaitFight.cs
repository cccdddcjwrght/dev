using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame 
{
    /// <summary>
    /// �ȴ�ս������
    /// </summary>
    public class StepWaitFight : Step
    {
        public override IEnumerator Excute()
        {
            yield return new WaitUntil(() => { return !BattleManager.Instance.isCombat; });
            Finish();
        }
    }
}

