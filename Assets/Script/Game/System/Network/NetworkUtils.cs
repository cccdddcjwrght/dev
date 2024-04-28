using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NetworkUtils
{
    //ºÏ≤‚ «∑Ò”–Õ¯¬Á
    public static bool IsNetworkReachability()
    {
        switch (Application.internetReachability)
        {
            case NetworkReachability.ReachableViaLocalAreaNetwork:
                return true;
            case NetworkReachability.ReachableViaCarrierDataNetwork:
                return true;
            default:
                return false;
        }
    }
}
