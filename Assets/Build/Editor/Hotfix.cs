﻿//
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
        
        public static void OneKeyBuildHotfix(int ver = 0 , int core = 0) //int ver = 0)
        {
            // 打包提取资源更新
            BuildScript.BuildBundleAndCopyToStream(ver);
            MakeHotfixResource(core);
        }

    }
}