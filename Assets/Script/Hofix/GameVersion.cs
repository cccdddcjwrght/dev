﻿using System;
using UnityEngine;
using System.IO;

namespace SGame
{
    // 游戏版本信息
    [Serializable]
    public class GameVersion
    {
        public string   ver;          // 显示版本号
        public int      buildNo;      // 编译版本(资源版本不用走热更新)
        public int      codeVer;      // 代码版本(代码版本不同要走大更新)
        public int      protoVer;     // 协议版本(协议版本用于服务器验证)
        public bool     server_close; // 服务器是否关闭, 若是显示停服公告
        public string[] resource_url; // 代码更新资源地址

        // 版本文件默认名字
        public const string FileName = "GameVersion.json";

        public GameVersion()
        {
            ver           = "";
            buildNo       = 0;
            codeVer       = 0;
            protoVer      = 0;
            server_close  = false;  
            resource_url  = new string[] { };
        }

        public void Copy(GameVersion other)
        {
            ver              = other.ver;
            codeVer          = other.codeVer;
            buildNo          = other.buildNo;
            protoVer         = other.protoVer;
            server_close     = other.server_close;
            resource_url     = new string[other.resource_url.Length];
        }
        
        /// <summary>
        /// 将数据转换成json字符串
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            return JsonUtility.ToJson(this, true);
        }

        /// <summary>
        /// 将json文件加载到版本信息中去
        /// </summary>
        /// <param name="jsonStr"></param>
        /// <returns></returns>
        public static GameVersion LoadJson(string jsonStr)
        {
            try
            {
                return JsonUtility.FromJson<GameVersion>(jsonStr);
            } catch(Exception e)
            {
                Debug.LogError("Parse GameVersion Json Fail Error=" + e.Message + " json=" + jsonStr);
            }
            return null;
        }

        /// <summary>
        /// 加载版本文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static GameVersion LoadFile(string filePath)
        {
            string loadStr = null;
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        loadStr = sr.ReadToEnd();
                    }
                }
            } catch(Exception e)
            {
                Debug.LogError("Load File Fail=" + e.Message);
                return null;
            }


            //JsonMapper
            return LoadJson(loadStr);
        }

        /// <summary>
        /// 版本写入文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool ToFile(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    string json = ToJson();
                    sw.Write(json);
                }
            }
            
            return true;
        }
    }
}
