using System;
using System.Collections;
using System.Collections.Generic;
using SGame;
using UnityEngine;

public class QuatilyActive : MonoBehaviour
{
    public int activeQuatilyLevel = 0;

    void UpdateQuatily(int quatily)
    {
        gameObject.SetActive(activeQuatilyLevel <= quatily);
    }
    
    public void Start()
    {
        EventManager.Instance.Reg<int>((int)GameEvent.QUATILY_CHANGE, UpdateQuatily);
        UpdateQuatily(GameQuatilySetting.Instance.quatily);
    }

    private void OnDestroy()
    {
        EventManager.Instance.UnReg<int>((int)GameEvent.QUATILY_CHANGE, UpdateQuatily);
    }

}
