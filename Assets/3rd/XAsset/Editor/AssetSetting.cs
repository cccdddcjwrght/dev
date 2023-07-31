using System.IO;
using UnityEditor;
using libx;
using UnityEngine;

#if UNITY_EDITOR
    public class AssetSetting
    {
        const string kRuntimeMode   = "XASSET/UseBundle";        // 是否使用Bundle
        const string kUpdateVersion = "XASSET/UseUpdateVersion"; // 是否使用版本更新功能
        
        // 按下处理
        [MenuItem(kRuntimeMode)]
        public static void ToggleRuntimeMode()
        {
            libx.Assets.runtimeMode = !libx.Assets.runtimeMode;
        }

        // 显示处理
        [MenuItem(kRuntimeMode, true)]
        public static bool ToggleRuntimeModeValidate()
        {
            Menu.SetChecked(kRuntimeMode, libx.Assets.runtimeMode);
            return true;
        }

        [MenuItem(kUpdateVersion)]
        public static void ToggleUpdateVersion()
        {
            Assets.useVersionUpdate = !Assets.useVersionUpdate;
        }
        [MenuItem(kUpdateVersion, true)]
        public static bool ToggleUpdateVersionValidate()
        {
            Menu.SetChecked(kUpdateVersion, Assets.useVersionUpdate);
            return true;
        }

        [MenuItem("XASSET/Clean")]
        public static void CleanPrestineDirectory()
        {
            Directory.Delete(Application.persistentDataPath, true);
            Debug.Log("Remove Directory Sucess=" + Application.persistentDataPath);
        }
    }
#endif