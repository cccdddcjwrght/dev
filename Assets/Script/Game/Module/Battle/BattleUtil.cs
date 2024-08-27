using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame 
{
    public static class BattleUtil
    {
        public static bool TriggerProbability(int value)
        {
            var num = SGame.Randoms.Random._R.Next(0,10000);
            if (num < value)
                return true;
            return false;
        }
    }
}

