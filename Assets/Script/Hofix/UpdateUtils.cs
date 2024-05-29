using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using libx;
using UnityEngine.Networking;
using System;
using System.IO;

namespace SGame
{
    public class UpdateUtils
    {
        // 判断是否使用手机
        public static bool IsCellNetwork()
        {
            return Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork;
        }

        /// <summary>
        /// 判断是否联网
        /// </summary>
        /// <returns></returns>
        public static bool HasNetwork()
        {
            return Application.internetReachability != NetworkReachability.NotReachable;
        }
        
        public static void OpenUrl(string url)
        {
            Application.OpenURL(url);
        }

        public static bool IsIos()
        {
            #if UNITY_IPHONE && !UNITY_EDITOR
                return true;
            #endif
            return false;
        }

        // 退出应用程序
        public static void ExitApp()
        {
            Application.Quit();
        }

        public static bool IsAndroid()
        {
            #if UNITY_ANDROID && !UNITY_EDITOR
                return true;
             #endif

            return false;
        }
        
        // 获得版本描述文件
        public static string GetVersionName(int buildNo)
        {
            return string.Format("{0}_{1}", Versions.Filename, buildNo);
        }
        
        // 获得更新文件名
        public static string GetUpdateFilePath(string filePath, string hash)
        {
            string filename = Path.GetFileName(filePath);
            string dirname = filePath.Substring(0, filePath.Length - filename.Length);
            string basename = Path.GetFileNameWithoutExtension(filename);
            string extname  = Path.GetExtension(filename);
            string new_name = string.Format("{0}_{1}{2}", basename, hash, extname);
            return dirname + new_name;
        }
        
        // 获得平台名称
        public static string GetPlatformForAssetBundles(RuntimePlatform target)
        {
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (target)
            {
                case RuntimePlatform.Android:
                    return "Android";
                case RuntimePlatform.IPhonePlayer:
                    return "iOS";
                case RuntimePlatform.WebGLPlayer:
                    return "WebGL";
                case RuntimePlatform.WindowsPlayer:
                case RuntimePlatform.WindowsEditor:
                    return "Windows";
                case RuntimePlatform.OSXEditor:
                case RuntimePlatform.OSXPlayer:
                    return "iOS"; // OSX
                default:
                    return null;
            }
        }
        
        public static string GetDownloadFileName(string url, string baseName, string hashValue)
        {
            if (!string.IsNullOrEmpty(hashValue))
                return string.Format("{0}{1}/{2}", url, UpdateUtils.GetPlatformForAssetBundles(), GetUpdateFilePath(baseName, hashValue));
            
            return string.Format("{0}{1}/{2}", url, UpdateUtils.GetPlatformForAssetBundles(), baseName);
        }
        
        public static string GetPlatformForAssetBundles()
        {
#if UNITY_ANDROID
            return "Android";
#elif UNITY_IPHONE
        return "iOS";
#elif UNITY_EDITOR
		return "Windows";
#endif
            return "Unknown";
        }

        // 获得更新路径
        public static string GetUpdatePath()
        {
            return string.Format("{0}{1}DLC{1}", Application.persistentDataPath, Path.DirectorySeparatorChar);
        }

        // 获得下载路径
        public static string GetDownloadPath()
        {
            return string.Format("{0}{1}DownloadFiles{1}", Application.persistentDataPath, Path.DirectorySeparatorChar);
        }

        // 获得更新资源路径
        public static string[] GetResourceUrl()
        {
            string value = IniUtils.GetLocalValue("@resource_url");
            string[] rets = value.Split("|",StringSplitOptions.RemoveEmptyEntries);
            return rets;
        }
        
        // 获得StreamAssets Path
        public static string GetStreamingAssetsPath()
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                return Application.streamingAssetsPath + Path.DirectorySeparatorChar;
            }

            if (Application.platform == RuntimePlatform.WindowsPlayer ||
                Application.platform == RuntimePlatform.WindowsEditor)
            {
                return "file:///" + Application.streamingAssetsPath + Path.DirectorySeparatorChar;
            }

            return "file://" + Application.streamingAssetsPath + Path.DirectorySeparatorChar;
        }

        // 判断是否是新文件
        public static bool IsNewFile(VFile file, IList<VFile> files)
        {
            foreach (var f in files)
            {
                if (file.len == f.len &&
                    string.Equals(file.hash, f.hash, StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
            }

            return true;
        }

        // 根据远端文件列表与本第文件列表获得更新列表
        public static IList<VFile> GetUpdateList(IList<VFile> remoteFiles, IList<VFile> localFiles)
        {
            List<VFile> files = new List<VFile>();
            foreach (var file in remoteFiles)
            {
                if (IsNewFile(file, localFiles))
                    files.Add(file);
            }
            
            return files;
        }

        // 获得所有文件的大小
        public static long GetFilesSize(IList<VFile> files)
        {
            long size = 0;
            foreach (var f in files)
            {
                size += f.len;
            }
            
            return size;
        }

        public class StrReturn
        {
            public string Value;
        }

        public static IEnumerator DownloadFile(string src, string dst, StrReturn err)
        {
            var request = UnityWebRequest.Get(src);
            request.downloadHandler = new DownloadHandlerFile(dst);
            var req = request.SendWebRequest();
            yield return req;
            err.Value = request.error;
            request.Dispose();
            if (!string.IsNullOrEmpty(err.Value))
                yield break;
            request.Dispose();
        }
        


        // 下载文件
        public static IEnumerator DownloadFiles(string[] srcFiles, string[] dstFiles, StrReturn err)
        {
            for (int i = 0; i < srcFiles.Length; i++)
            {
                var request = UnityWebRequest.Get(srcFiles[i]);
                request.downloadHandler = new DownloadHandlerFile(dstFiles[i]);
                var req = request.SendWebRequest();
                yield return req;
                err.Value = request.error;
                request.Dispose();
                if (!string.IsNullOrEmpty(err.Value))
                    yield break;
            }
        }
        

        // 从Stream Assets 中将文件拷贝出来
        public static IEnumerator UpdateCopyFromStreamAssets(string savePath, 
            IList<VFile> versions, 
            string basePath, 
            Action<float> OnProgress = null)
        {
            var version = versions[0];
            if (version.name.Equals(Versions.Dataname))
            {
                var request = UnityWebRequest.Get(basePath + version.name);
                request.downloadHandler = new DownloadHandlerFile(savePath + version.name);
                var req = request.SendWebRequest();
                while (!req.isDone)
                {
                    //OnMessage?.Invoke("正在复制文件");
                    OnProgress?.Invoke(req.progress);
                    yield return null;
                }

                request.Dispose();
            }
            else
            {
                for (var index = 0; index < versions.Count; index++)
                {
                    var item = versions[index];
                    var request = UnityWebRequest.Get(basePath + item.name);
                    request.downloadHandler = new DownloadHandlerFile(savePath + item.name);
                    yield return request.SendWebRequest();
                    request.Dispose();
                    //OnMessage(string.Format("正在复制文件：{0}/{1}", index, versions.Count));
                    OnProgress(index * 1f / versions.Count);
                }
            }
        }
    }
}