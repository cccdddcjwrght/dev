//
// AssetsMenuItem.cs
//
// Author:
//       fjy <jiyuan.feng@live.com>
//
// Copyright (c) 2020 fjy
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;
using System.IO;
using libx;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using HybridCLR.Editor.Installer;
using UnityEditor.VersionControl;

// 热更新资源生成
namespace SGame
{
    public static class HotfixenuItems
    {
        private const string kMakeHotfixResource        = "Assets/Bundles/BuildHotfix";
        private const string kOneKeyHotfixResource      = "Assets/Bundles/OneKeyBuildHotfix";
        private const string HOTFIX_PATH                = "DLC/HDLC";
        private const string BUNDLE_PATH                = "DLC";
        private const string PROJECT_PATH               = "Assets/";
        private const string HYBRID_AOT_PATH                   = "Assets/BuildAsset/AOTMeta/";
        private const string HYBRID_DLL_PATH                   = "Assets/BuildAsset/Code/";

        
        static string GetBuildTargetName(BuildTarget build)
        {
            switch (build)
            {
                case BuildTarget.Android:
                    return "Android";
                case BuildTarget.iOS:
                    return "iOS";
                case BuildTarget.StandaloneWindows:
                   case BuildTarget.StandaloneWindows64:
                    return "Windows";
                case BuildTarget.StandaloneOSX:
                    return "iOS";
            }
            return null;
        }

        [MenuItem(kMakeHotfixResource)]
        private static void CallMakeHotfixResource()
        {
            MakeHotfixResource();
        }

        // 给文明重命名
        private static void MakeHotfixResource(int code = 0)
        {
            var watch = new Stopwatch();
            watch.Start();
            Debug.Log("Copy Files ...");

            // 目标目录
            string dstPath      = Path.Combine(HOTFIX_PATH, GetBuildTargetName(EditorUserBuildSettings.activeBuildTarget));
            string srcPath      = Path.Combine(BUNDLE_PATH, GetBuildTargetName(EditorUserBuildSettings.activeBuildTarget));
            string streamAssetPath = Application.streamingAssetsPath;
            
            string versionPath      = Path.Combine(srcPath, Versions.Filename);
            string gameVersionPath  = Path.Combine(PROJECT_PATH, GameVersion.FileName);
            
            int buildNo             = Versions.LoadVersion(versionPath);
            IList<VFile> files      = Versions.LoadVersions(versionPath);
            GameVersion  gameVersion = GameVersion.LoadFile(gameVersionPath);
            foreach (var f in files)
            {
                string dst_name = Path.Combine(dstPath, UpdateUtils.GetUpdateFilePath(f.name, f.hash));
                string src_name = Path.Combine(srcPath, f.name);
                FileOperator.CopyFile(src_name, dst_name, true);
            }
            FileOperator.CopyFile(versionPath, Path.Combine(dstPath, UpdateUtils.GetVersionName(buildNo)));
            

            // 更新游戏版本信息
            gameVersion.buildNo = buildNo;
            if (code > 0)
            {
                gameVersion.codeVer = code;
            }
            
            // 下载资源路径
            var resource_url = SGame.IniUtils.GetLocalValue("@resource_url");
            if (!string.IsNullOrEmpty(resource_url))
            {
                // 填充资源路径
                string[] resource_urls = resource_url.Split("|");
                gameVersion.resource_url = resource_urls;
            }

            gameVersion.ver = PlayerSettings.bundleVersion;
            gameVersion.ToFile(gameVersionPath);
            gameVersion.ToFile(Path.Combine(dstPath, GameVersion.FileName));
            gameVersion.ToFile(Path.Combine(srcPath, GameVersion.FileName));
            gameVersion.ToFile(Path.Combine(streamAssetPath, GameVersion.FileName));

            watch.Stop();
            Debug.Log("MakeHotfixResource " + watch.ElapsedMilliseconds + " ms.");
        }

        [MenuItem(kOneKeyHotfixResource)]
        public static void OneKeyBuildHotfixResource() //int ver = 0)
        {
            // 打包提取资源更新
            OneKeyBuildHotfix(0, 0);
        }

        static string GetAotPath()
        {
            return Path.Combine(HybridCLR.Editor.Settings.HybridCLRSettings.Instance.strippedAOTDllOutputRootDir,
                GetBuildTargetName(EditorUserBuildSettings.activeBuildTarget));
        }
        
        static string GetHotfixPath()
        {
            return Path.Combine(HybridCLR.Editor.Settings.HybridCLRSettings.Instance.hotUpdateDllCompileOutputRootDir,
                GetBuildTargetName(EditorUserBuildSettings.activeBuildTarget));
        }
        
        static  HybridCLR.Editor.Installer.InstallerController s_hybridClr = new InstallerController();

        static List<string> GetAotFiles()
        {
            const string AOT_PATH = "Assets/HybridCLRGenerate/AOTGenericReferences.cs";
            if (!File.Exists(AOT_PATH))
            {
                Debug.LogError("BuildHybridclr AOTGenericReferences NOT EXISTS");
                return null;
            }
            
            string fileText = File.ReadAllText(AOT_PATH);
            string parten = @"(?<="")\b.+dll";
            List<string> ret = new List<string>();
            foreach (Match match in Regex.Matches(fileText, parten))
            {
                Debug.Log("mach value=" + match.Value);
                ret.Add(match.Value);
            }

            return ret;
        }
        
        /// <summary>
        /// 编译华佗热更
        /// </summary>
        static void BuildHybridclr()
        {
            if (!s_hybridClr.HasInstalledHybridCLR())
            {
                Debug.Log("InstallDefaultHybridCLR install first");
				s_hybridClr.InstallDefaultHybridCLR();
			}

			HybridCLR.Editor.Commands.PrebuildCommand.GenerateAll();
            AssetDatabase.Refresh();


            // 生成所有DLL
            var aotfiles = GetAotFiles();
            if (aotfiles == null)
            {
                Debug.LogError("BuildHybridclr CODE NOT FRESH!!!!");
            }
            else
            {
                Debug.Log("BuildHybridclr CODE FRESH SUCCESS!");
            }
            
            // 拷贝AOT DLL
            // 查看AOT文件类别
            var aotRootPath = GetAotPath();
            if (Directory.Exists(aotRootPath) == false)
            {
                // 目标目录不存在
                Directory.CreateDirectory(aotRootPath);
            }
            foreach (var aot in aotfiles)
            {
                var path = Path.Combine(aotRootPath, aot);
                var dst = Path.Combine(HYBRID_AOT_PATH, aot) + ".bytes";
                if (!FileOperator.CopyFile(path, dst))
                    Debug.LogError("copy file fail src=" + path + " dst=" + dst);
            }
            
            // 拷贝热更 DLL
            var hotfix_files = HybridCLR.Editor.Settings.HybridCLRSettings.Instance.hotUpdateAssemblyDefinitions;
            var hotfix_root = GetHotfixPath();
            if (Directory.Exists(hotfix_root) == false)
            {
                // 目标目录不存在
                Directory.CreateDirectory(hotfix_root);
            }
            foreach (var hot in hotfix_files)
            {
                var name = hot.name;
                var path = Path.Combine(hotfix_root, name) + ".dll";
                var dst = Path.Combine(HYBRID_DLL_PATH, name) + ".dll.bytes";
                if (!FileOperator.CopyFile(path, dst))
                    Debug.LogError("copy file fail src=" + path + " dst=" + dst);
            }

            AssetDatabase.Refresh();
        }
        
        public static void OneKeyBuildHotfix(int ver = 0 , int core = 0) //int ver = 0)
        {
            Debug.Log("hot2====>" + HybridCLR.Editor.SettingsUtil.Enable);
            #if ENABLE_HOTFIX
                HybridCLR.Editor.SettingsUtil.Enable = true;
                // 生成代码热更
                BuildHybridclr();
            #else
                HybridCLR.Editor.SettingsUtil.Enable = false;
            #endif
            Debug.Log("hot3====>" + HybridCLR.Editor.SettingsUtil.Enable);

            // 打包提取资源更新
            BuildScript.BuildBundleAndCopyToStream(ver);
            MakeHotfixResource(core);
        }

    }
}