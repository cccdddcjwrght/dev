using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ս����ɫ״̬
/// </summary>
public class CharacterState
{
    List<BaseState> stateList = new List<BaseState>();
    /// <summary>
    /// ���״̬
    /// </summary>
    /// <param name="state"></param>
    public void ApplyState(BaseState state) 
    {
        stateList.Add(state);
        if(state.isImmediately) 
            state.Exctue();
    }

    /// <summary>
    /// ����״̬
    /// </summary>
    public void UpdateState() 
    {
        for (int i = stateList.Count - 1; i >= 0; i--)
        {
            var state = stateList[i];
            state.Exctue();
            state.Reduce();

            if (state.IsExpired()) 
            {
                state.Dispose();
                stateList.RemoveAt(i);
            }  
        }
    }

    public bool IsHasState(BattleStateType type) 
    {
        return stateList.Find((v) => v.type == type) != null;
    }

    public void Reset() 
    {
        stateList.ForEach((v) => v.Dispose());
        stateList.Clear();
    }
}
