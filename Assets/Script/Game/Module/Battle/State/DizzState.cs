using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame 
{
    public class DizzState : BaseState
    {
        public DizzState()
        {
            round = BattleConst.dizziness_inning + 1;
            isImmediately = true;
            type = BattleStateType.DIZZ;
        }

        public override void ShowEffect()
        {
            Debug.Log("����ѣ����Ч");
            //������Ч
        }

        public override void Dispose()
        {
            if (stateShow)
            {
                //ɾ��ѣ����Ч
                Debug.Log("ɾ��ѣ����Ч");
            }

        }
    }
}

