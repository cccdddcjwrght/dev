using SGame.Dining;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame 
{
    public class StepRegion : Step
    {
        public override IEnumerator Excute()
        {
            yield return Wait();
            Finish();
        }


        //�ȴ�ĳ���ӹ�̨�ﵽ���ټ�
        public IEnumerator Wait() 
        {
            var region = (int)m_Config.FloatParam(0);
            var needLevel = m_Config.FloatParam(1);
            var table = DataCenter.MachineUtil.GetWorktable(region);
            while (true) 
            {
                if (table.level >= needLevel) yield break;
                yield return null;
            }
        }
    }

}

