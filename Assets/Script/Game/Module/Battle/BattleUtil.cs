using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BattleUtil 
{
    public static bool TriggerProbability(int value) 
    {
        var num = Random.Range(0, 10000) + 1;
        if (num < value)
            return true;
        return false;
    }
}
