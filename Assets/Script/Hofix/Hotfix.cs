using System.Collections;
using System.Collections.Generic;
using log4net;
using UnityEngine;

// 
public class Hotfix
{
    private static ILog log = LogManager.GetLogger("Hofix.Entery");
    
    public static IEnumerator Main()
    {
        log.Info("Hello, I am In!");
        yield return null;
    }
}
