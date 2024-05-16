using Http;
using log4net;
using SGame.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame 
{
    public class TimeSyncModule
    {
        private static ILog log = LogManager.GetLogger("game.servertime");
        public static IEnumerator GetServerTime() 
        {
            HttpPackage pkg = new HttpPackage();
            var result = HttpSystem.Instance.Post("timestamp", pkg.ToJson());
            yield return result;

            if (!string.IsNullOrEmpty(result.error))
            {
                GameServerTime.Instance.Update((int)DateTimeOffset.Now.ToUnixTimeSeconds(), -1);
                log.Warn("get serverTime data fail=" + result.error);
                yield break;
            }

            pkg = JsonUtility.FromJson<HttpPackage>(result.data);
            GameServerTime.Instance.Update(int.Parse(pkg.data), -1);
        }
    }
}

