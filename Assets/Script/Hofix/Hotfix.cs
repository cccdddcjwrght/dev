using System.Collections;
using System.Collections.Generic;
using log4net;
using UnityEngine;

using SGame.Hotfix;
public class Hotfix
{
    private static ILog log = LogManager.GetLogger("Hofix.Entery");
    
    public static IEnumerator Main()
    {
        #if ENABLE_HOTFIX
            log.Info("Hello, I am In!");
            yield return HotfixModule.Instance.RunHotfix();
        #else
            yield return null;
        #endif
    }
}
