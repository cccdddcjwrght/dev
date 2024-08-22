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
            Debug.Log("生成眩晕特效");
            //生成特效
        }

        public override void Dispose()
        {
            if (stateShow)
            {
                //删除眩晕特效
                Debug.Log("删除眩晕特效");
            }

        }
    }
}

