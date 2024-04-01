using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodePipeline
{
    private const float PROCESS_DUR_SEC = 0.016f;  //1s/60;
    /// <summary>
    /// 当前帧度过的时间
    /// </summary>
    private static float _startSecOnCurTicker;
    /// <summary>
    /// 调用队列
    /// </summary>
    private static List<Action> _lstCallee = new List<Action>();

    private static bool _hasInit;

    public static void Push4Invoke(Action callee)
    {
        _lstCallee.Add(callee);
    }

    public static void Init(GameObject rootObj)
    {
        if (_hasInit)
        {
            return;
        }

        MonoBehaviour mono = rootObj.GetComponent<MonoBehaviour>();
        if (mono == null)
            return;

        mono.StartCoroutine(ProcessTicker());
    }

    private static IEnumerator ProcessTicker()
    {
        while(true)
        {
            _startSecOnCurTicker = Time.realtimeSinceStartup;
            ProcessPopCallee();
            yield return null;
        }
    }

    private static void ProcessPopCallee()
    {
        if (_lstCallee.Count == 0)
        {
            return;
        }
        while((Time.realtimeSinceStartup - _startSecOnCurTicker) < PROCESS_DUR_SEC)
        {
            if (_lstCallee.Count == 0)
            {
                return;
            }
            Action callee = _lstCallee[0];
            _lstCallee.RemoveAt(0);
            callee.Invoke();
        }
    }
}
