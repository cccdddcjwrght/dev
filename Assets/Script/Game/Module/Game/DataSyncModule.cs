using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using SGame.Http;
using Http;
using log4net;
namespace SGame
{
    public class DataSyncModule
    {
        private static ILog log = LogManager.GetLogger("game.datasync");
        
        /// <summary>
        /// 用户数据同步功能, 首次登录, 若服务器有数据, 就使用服务器数据
        /// </summary>
        /// <returns></returns>
        public static IEnumerator GetDataFromServer(string userName)
        {
            var playerID = (long)userName.GetHashCode();
            if (!DataCenter.Instance.isFirst)
            {
                // 本地有数据, 并且账号账号一样
                if (playerID == DataCenter.Instance.accountData.playerID)
                {
                    yield break;
                }
            }

            // 唯一化用户ID
            DataCenter.Instance.accountData.playerID = playerID;
			
            // 请求服务器
            HttpPackage pkg = new HttpPackage();
            pkg.data = playerID.ToString();
            var result = HttpSystem.Instance.Post("getData", pkg.ToJson());
            yield return result;
            if (!string.IsNullOrEmpty(result.error))
            {
                log.Error("get user sync data fail=" + result.error);
                yield break;
            }
			
            pkg = JsonUtility.FromJson<HttpPackage>(result.data);
            if (string.IsNullOrEmpty(pkg.data))
            {
                // 新用户, 不用管
                yield break;
            }
			
            // 老用户, 将数据同步回来, 并重新加载
            DataCenterExtension.SaveStrToDisk(pkg.data);
            DataCenter.Instance.Load();
            log.Info("Recovert Player Success=" + userName + " lasttime=" + DataCenter.Instance.accountData.lasttime);
        }

        public static void SendDataToServer()
        {
            var dataStr = DataCenterExtension.GetStrFromDisk();
            HttpPackage pkg = new HttpPackage() { data = dataStr };
            var sendData = JsonUtility.ToJson(pkg);
            HttpSystem.Instance.Post("uploadData", sendData);
        }
    }
}