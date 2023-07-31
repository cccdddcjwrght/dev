//
// BuildScript.cs
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
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace libx
{
	public static class BuildScript
	{
		public static string outputPath = "DLC/" + GetPlatformName();

		public static void ClearAssetBundles()
		{
			var allAssetBundleNames = AssetDatabase.GetAllAssetBundleNames();
			for (var i = 0; i < allAssetBundleNames.Length; i++)
			{
				var text = allAssetBundleNames[i];
				if (EditorUtility.DisplayCancelableProgressBar(
									string.Format("Clear AssetBundles {0}/{1}", i, allAssetBundleNames.Length), text,
									i * 1f / allAssetBundleNames.Length))
					break;

				AssetDatabase.RemoveAssetBundleName(text, true);
			}
			EditorUtility.ClearProgressBar();
		}

		internal static void ApplyBuildRules()
		{
			var rules = GetBuildRules();
			rules.Apply();
		}

		internal static BuildRules GetBuildRules()
		{
			return GetAsset<BuildRules>("Assets/Rules.asset");
		}

		public static void CopyAssetBundlesTo(string path)
		{
			var files = new[] {
				Versions.Dataname,
				Versions.Filename,
			};
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
			foreach (var item in files)
			{
				var src = outputPath + "/" + item;
				var dest = Application.streamingAssetsPath + "/" + item;
				if (File.Exists(src))
				{
					File.Copy(src, dest, true);
				}
			}
		}

		public static string GetPlatformName()
		{
			return GetPlatformForAssetBundles(EditorUserBuildSettings.activeBuildTarget);
		}

		private static string GetPlatformForAssetBundles(BuildTarget target)
		{
			// ReSharper disable once SwitchStatementMissingSomeCases
			switch (target)
			{
				case BuildTarget.Android:
					return "Android";
				case BuildTarget.iOS:
					return "iOS";
				case BuildTarget.WebGL:
					return "WebGL";
				case BuildTarget.StandaloneWindows:
				case BuildTarget.StandaloneWindows64:
					return "Windows";
#if UNITY_2017_3_OR_NEWER
				case BuildTarget.StandaloneOSX:
					return "OSX";
#else
                case BuildTarget.StandaloneOSXIntel:
                case BuildTarget.StandaloneOSXIntel64:
                case BuildTarget.StandaloneOSXUniversal:
                    return "OSX";
#endif
				default:
					return null;
			}
		}

		private static string[] GetLevelsFromBuildSettings()
		{
			List<string> scenes = new List<string>();
			foreach (var item in GetBuildRules().scenesInBuild)
			{
				var path = AssetDatabase.GetAssetPath(item);
				if (!string.IsNullOrEmpty(path))
				{
					scenes.Add(path);
				}
			}

			return scenes.ToArray();
		}

		private static string GetAssetBundleManifestFilePath()
		{
			var relativeAssetBundlesOutputPathForPlatform = Path.Combine("Asset", GetPlatformName());
			return Path.Combine(relativeAssetBundlesOutputPathForPlatform, GetPlatformName()) + ".manifest";
		}

		public static void BuildStandalonePlayer()
		{
			var outputPath =
				Path.Combine(Environment.CurrentDirectory,
					"Build/" + GetPlatformName()
						.ToLower()); //EditorUtility.SaveFolderPanel("Choose Location of the Built Game", "", "");
			if (outputPath.Length == 0)
				return;

			var levels = GetLevelsFromBuildSettings();
			if (levels.Length == 0)
			{
				Debug.Log("Nothing to build.");
				return;
			}

			var targetName = GetBuildTargetName(EditorUserBuildSettings.activeBuildTarget);
			if (targetName == null)
				return;
#if UNITY_5_4 || UNITY_5_3 || UNITY_5_2 || UNITY_5_1 || UNITY_5_0
			BuildOptions option = EditorUserBuildSettings.development ? BuildOptions.Development : BuildOptions.None;
			BuildPipeline.BuildPlayer(levels, path + targetName, EditorUserBuildSettings.activeBuildTarget, option);
#else
			var buildPlayerOptions = new BuildPlayerOptions
			{
				scenes = levels,
				locationPathName = outputPath + targetName,
				assetBundleManifestPath = GetAssetBundleManifestFilePath(),
				target = EditorUserBuildSettings.activeBuildTarget,
				options = EditorUserBuildSettings.development ? BuildOptions.Development : BuildOptions.None
			};
			BuildPipeline.BuildPlayer(buildPlayerOptions);
#endif
		}

		public static string CreateAssetBundleDirectory()
		{
			// Choose the output path according to the build target.
			if (!Directory.Exists(outputPath))
				Directory.CreateDirectory(outputPath);

			return outputPath;
		}

		public static void BuildAssetBundles(int ver = 0)
		{
			// Choose the output path according to the build target.
			var outputPath = CreateAssetBundleDirectory();
			const BuildAssetBundleOptions options = BuildAssetBundleOptions.ChunkBasedCompression;
			var targetPlatform = EditorUserBuildSettings.activeBuildTarget;
			var rules = GetBuildRules();
			var builds = rules.GetBuilds();
			var assetBundleManifest = BuildPipeline.BuildAssetBundles(outputPath, builds, options, targetPlatform);
			if (assetBundleManifest == null)
			{
				return;
			}

			var manifest = GetManifest();
			var dirs = new List<string>();
			var assets = new List<AssetRef>();
			var bundles = assetBundleManifest.GetAllAssetBundles();
			var bundle2Ids = new Dictionary<string, int>();
			for (var index = 0; index < bundles.Length; index++)
			{
				var bundle = bundles[index];
				bundle2Ids[bundle] = index;
			}

			var bundleRefs = new List<BundleRef>();
			for (var index = 0; index < bundles.Length; index++)
			{
				var bundle = bundles[index];
				var deps = assetBundleManifest.GetAllDependencies(bundle);
				var path = string.Format("{0}/{1}", outputPath, bundle);
				if (File.Exists(path))
				{
					using (var stream = File.OpenRead(path))
					{
						bundleRefs.Add(new BundleRef
						{
							name = bundle,
							id = index,
							deps = Array.ConvertAll(deps, input => bundle2Ids[input]),
							len = stream.Length,
							hash = assetBundleManifest.GetAssetBundleHash(bundle).ToString(),
						});
					}
				}
				else
				{
					Debug.LogError(path + " file not exsit.");
				}
			}

			for (var i = 0; i < rules.ruleAssets.Length; i++)
			{
				var item = rules.ruleAssets[i];
				var path = item.path;
				var dir = Path.GetDirectoryName(path).Replace("\\", "/");
				var index = dirs.FindIndex(o => o.Equals(dir));
				if (index == -1)
				{
					index = dirs.Count;
					dirs.Add(dir);
				}

				var asset = new AssetRef { bundle = bundle2Ids[item.bundle], dir = index, name = Path.GetFileName(path) };
				assets.Add(asset);
			}

			manifest.dirs = dirs.ToArray();
			manifest.assets = assets.ToArray();
			manifest.bundles = bundleRefs.ToArray();

			EditorUtility.SetDirty(manifest);
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();

			var manifestBundleName = "manifest.unity3d";
			builds = new[] {
				new AssetBundleBuild {
					assetNames = new[] { AssetDatabase.GetAssetPath (manifest), },
					assetBundleName = manifestBundleName
				}
			};

			BuildPipeline.BuildAssetBundles(outputPath, builds, options, targetPlatform);
			ArrayUtility.Add(ref bundles, manifestBundleName);

			if (ver <= 0)
			{
				// 老的版本逻辑
				Versions.BuildVersions(outputPath, bundles, GetBuildRules().AddVersion());
			}
			else
			{
				// 由打包几传入
				GetBuildRules().SetVersion(ver);
				Versions.BuildVersions(outputPath, bundles, ver);
			}
		}

		private static string GetBuildTargetName(BuildTarget target)
		{
			var time = DateTime.Now.ToString("yyyyMMdd-HHmmss");
			var name = PlayerSettings.productName + "-v" + PlayerSettings.bundleVersion + ".";
			switch (target)
			{
				case BuildTarget.Android:
					return string.Format("/{0}{1}-{2}.apk", name, GetBuildRules().version, time);

				case BuildTarget.StandaloneWindows:
				case BuildTarget.StandaloneWindows64:
					return string.Format("/{0}{1}-{2}.exe", name, GetBuildRules().version, time);

#if UNITY_2017_3_OR_NEWER
				case BuildTarget.StandaloneOSX:
					return "/" + name + ".app";

#else
                case BuildTarget.StandaloneOSXIntel:
                case BuildTarget.StandaloneOSXIntel64:
                case BuildTarget.StandaloneOSXUniversal:
                    return "/" + path + ".app";

#endif

				case BuildTarget.WebGL:
				case BuildTarget.iOS:
					return "";
				// Add more build targets for your own.
				default:
					Debug.Log("Target not implemented.");
					return null;
			}
		}

		private static T GetAsset<T>(string path) where T : ScriptableObject
		{
			var asset = AssetDatabase.LoadAssetAtPath<T>(path);
			if (asset == null)
			{
				asset = ScriptableObject.CreateInstance<T>();
				AssetDatabase.CreateAsset(asset, path);
				AssetDatabase.SaveAssets();
			}

			return asset;
		}

		public static Manifest GetManifest()
		{
			return GetAsset<Manifest>(Assets.ManifestAsset);
		}

		public static void RemovePath(string path)
		{
			//var directories = Directory.GetDirectories(path, "*", SearchOption.AllDirectories);
			//foreach (var d in directories)
			//         {
			//	//File.Delete()
			//	Debug.Log("dir=" + d);
			//         }


			//var  files     = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);

			//path = "D:/Work/Git/sparkle_client/Assets/StreamingAssets/DLC/Android/assets/buildasset/ui/pvp.unity3d.manifest";
			//bool dirExits  = Directory.Exists(path);
			//bool fileExits = File.Exists(path);

			//foreach (var f in files)
			//         {
			//	Debug.Log("file=" + f);
			//         }
			//Debug.Log("dir exits=" + dirExits.ToString());
			//Debug.Log("file exist=" + fileExits.ToString());
			//Directory.Delete(path, true);

			//
			Directory.Delete(path, true);
		}

		// 拷贝路径
		public static void CopyPath(string src, string dst)
		{
			if (Directory.Exists(src) == false)
			{
				Debug.Log("Src Directory Not Found=" + src);
				return;
			}


			// 创建目录
			var directories = Directory.GetDirectories(src, "*", SearchOption.AllDirectories);
			foreach (var subDir in directories)
			{
				// 获得相对路径
				var relativePath = Path.GetRelativePath(src, subDir);
				var dstPath = Path.Combine(dst, relativePath);

				// 创建目录
				if (Directory.Exists(dstPath) == false)
					Directory.CreateDirectory(dstPath);
			}

			// 拷贝文件
			var files = Directory.GetFiles(src, "*", SearchOption.AllDirectories);
			foreach (var file in files)
			{
				// 创建文件
				var relativeFile = Path.GetRelativePath(src, file);
				var dstFile = Path.Combine(dst, relativeFile);
				File.Copy(file, dstFile, true);
			}
		}

		public static void BuildBundleAndCopyToStream(int ver = 0)
		{
			ApplyBuildRules();
			BuildAssetBundles(ver);
			string srcPath = Path.Combine("DLC", GetPlatformName());
			string dstPath = Path.Combine(Application.streamingAssetsPath);
			if (Directory.Exists(dstPath))
				Directory.Delete(dstPath, true);
			CopyPath(srcPath, dstPath);
			/*
			string versionFileName = "GameVersion.json";
			string dstFile = Path.Combine(dstPath, versionFileName);
			//GetBuildRules().version
			if (!File.Exists(dstFile))
			{
				File.Copy(Path.Combine("Assets", versionFileName),
					Path.Combine(dstPath, versionFileName));
			}
			*/

		}
	}
}