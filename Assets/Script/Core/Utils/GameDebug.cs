using System;
using UnityEngine;
using log4net;

public static class GameDebug
{
    static bool isInit = false;
    static ILog log = LogManager.GetLogger("GameSystem");

    public static void Init(string logfilePath)
    {
        isInit = true;
    }

    public static void Log(string message)
    {
        #if UI_DEBUG
        if (GlobalTime.isInitalize)
            message = string.Format("time ={0} msg={1}", GlobalTime.passTime, message);
          #endif  
        
        if (!isInit)
            Debug.Log(message);
        else
            log.Info(message);
    }


    public static void LogError(string message)
    {
        if (!isInit)
            Debug.LogError(message);
        else
            log.Error(message);

    }

    public static void LogWarning(string message)
    {
        if (!isInit)
            Debug.LogWarning(message);
        else
            log.Warn(message);
    }

    public static void Assert(bool condition)
    {
        if (!condition)
            throw new ApplicationException("GAME ASSERT FAILED");
    }

    public static void Assert(bool condition, string msg)
    {
        if (!condition)
            throw new ApplicationException("GAME ASSERT FAILED : " + msg);
    }

    public static void Assert<T>(bool condition, string format, T arg1)
    {
        if (!condition)
            throw new ApplicationException("GAME ASSERT FAILED : " + string.Format(format, arg1));
    }

    public static void Assert<T1, T2>(bool condition, string format, T1 arg1, T2 arg2)
    {
        if (!condition)
            throw new ApplicationException("GAME ASSERT FAILED : " + string.Format(format, arg1, arg2));
    }

    public static void Assert<T1, T2, T3>(bool condition, string format, T1 arg1, T2 arg2, T3 arg3)
    {
        if (!condition)
            throw new ApplicationException("GAME ASSERT FAILED : " + string.Format(format, arg1, arg2, arg3));
    }

    public static void Assert<T1, T2, T3, T4>(bool condition, string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
    {
        if (!condition)
            throw new ApplicationException("GAME ASSERT FAILED : " + string.Format(format, arg1, arg2, arg3, arg4));
    }

    public static void Assert<T1, T2, T3, T4, T5>(bool condition, string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
    {
        if (!condition)
            throw new ApplicationException("GAME ASSERT FAILED : " + string.Format(format, arg1, arg2, arg3, arg4, arg5));
    }
}
