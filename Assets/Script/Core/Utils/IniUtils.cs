using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.Reflection;
using libx;

namespace SGame
{
    public class IniUtils
    {
        private static Dictionary<string, string> m_localIni = null;

        static public Dictionary<string, string> GetLocalIni()
        {
            var asset = Resources.Load<TextAsset>("local");
            Dictionary<string, string> iniCfs = new Dictionary<string, string>();
            if (asset)
            {
                var local = IniParserBySplit(asset.text, Environment.NewLine);
                if (local != null && local.Count > 0)
                {
                    foreach (var item in local)
                        iniCfs[item.Key] = item.Value;
                }
            }
            return iniCfs;
        }

        public static string GetLocalValue(string key)
        {
            if (m_localIni == null)
                m_localIni = GetLocalIni();

            var ini = m_localIni;
            if (ini.TryGetValue(key, out string ret))
            {
                return ret;
            }

            return "";
        }

        static public Dictionary<string, string> IniParser(params string[] lines)
        {
            if (lines != null && lines.Length > 0)
            {
                var datas = lines.Where(Line => !string.IsNullOrEmpty(Line) && !Line.StartsWith("#") && Line.IndexOf('=') > 0)
                    .Select(Line => Line.Split(new char[] { '=' }, 2, StringSplitOptions.RemoveEmptyEntries))
                    .Where(ls => ls.Length > 1)
                    .ToDictionary(Parts => Parts[0].Trim(), Parts => Parts.Length > 1 ? Parts[1].Trim() : null);
                return datas;
            }
            return null;
        }

        static public Dictionary<string, string> IniParserBySplit(string text, string splitChar)
        {
            if (!string.IsNullOrEmpty(text))
            {
                return IniParser(text.Split(new string[] { splitChar }, StringSplitOptions.RemoveEmptyEntries));
            }
            return null;
        }
        
        static public Assembly GetAssemblyByName(string assemblyName = "Assembly-CSharp")
        {
            return AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.GetName().Name == assemblyName);
        }

        static public IReadOnlyList<string> GetAotFiles()
        {
            Assembly assembly = GetAssemblyByName();
            var t = assembly.GetType("AOTGenericReferences");
            if (t == null)
            {
                Debug.LogError("not found class AOTGenericReferences");
                return null;
            }

            var field = t.GetField("PatchedAOTAssemblyList");
            if (field == null)
            {
                var member = t.GetMember("PatchedAOTAssemblyList");
                Debug.LogError("not found field PatchedAOTAssemblyList");
                return null;
            }

            var dataList = field.GetValue(null) as IReadOnlyList<string>;
            return dataList;
        }

        public static GameVersion LoadLocalVersion()
        {
            #if UNITY_EDITOR
                // 没开启更新流程直接进入游戏
                if (!Assets.useVersionUpdate || !System.IO.File.Exists("Assets/" + GameVersion.FileName))
                {
                    return GameVersion.LoadFile("Assets/" + GameVersion.FileName);
                }
            #endif

            if (!System.IO.File.Exists("Assets/" + GameVersion.FileName))
                return new GameVersion() { buildNo = 0, codeVer = 0, protoVer = 0, ver = "er0.0.1" };
            
            return GameVersion.LoadFile(Assets.basePath + GameVersion.FileName);
        }
    }
}