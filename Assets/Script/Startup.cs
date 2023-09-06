using UnityEngine;
using System.Reflection;
using System.Linq;
using HybridCLR;
using System;
using System.Collections;
using log4net;
using System.IO;
using libx;
using SGame;
using Sirenix.OdinInspector;
using UnityEngine.UI;

// 游戏启动类, 包括调用与加载热更新代码
/// <summary>
/// 游戏启动类, 启动核心逻辑与游戏等逻辑
/// </summary>
[DefaultExecutionOrder(-1000)]
public class Startup : MonoBehaviour
{
    [Serializable]
    public struct Module
    {
        public string m_dllName;
        public string m_className;
        public string m_funcName;
    }

    [ListDrawerSettings(ShowIndexLabels = true, ListElementLabelName = "m_dllName")]
    public Module[]     m_modules;
#if UNITY_EDITOR
    public bool         m_useHotfixCode = false;
#endif
    // 热更新资源路径
    public const string HOTFIX_PATH  = "Assets/BuildAsset/Code/";

    const  string       LogInitPath = "log4net.xml";

    // 初始日志
    private static ILog log = LogManager.GetLogger("startup");
    
    // 逻辑, 有三中模式
    // 1. 编辑器下直接使用改代码
    // 2. 编辑器下使用
    // Start is called before the first frame update
    void Start()
    {
        FiberCtrl.Pool.Run(Run());
    }

    // 基础模块初始化
    IEnumerator BaseModuleInitalize()
    {
        SetupAssetPath();
        
        Debug.Log("call 1!!!");

        
        // 1. 初始化日志
        TextAsset logAsset = Resources.Load<TextAsset>(LogInitPath);
        if (logAsset != null && logAsset.bytes != null)
        {
            using (MemoryStream ms = new MemoryStream(logAsset.bytes))
            {
                Debug.Log("logConfig bytes = " + logAsset.bytes.Length);
                log4net.Config.XmlConfigurator.Configure(ms);
            }
        }
        else
        {
            Debug.LogError("Log Init Fail!!!!!");
        }

        // 2. 初始化加载器
        // 资源加载初始化
        libx.ManifestRequest assetRequest = libx.Assets.Initialize();
        yield return assetRequest;
        if (!string.IsNullOrEmpty(assetRequest.error))
        {
            log.Error("Asset Bundle Load Fail=" + assetRequest.error);
            yield break;
        }
        log.Info("Assets InitSuccess");
        yield return null;
    }

    IEnumerator LoadModules()
    {
        // 先处理热更新
        foreach (var module in m_modules)
        {
            Debug.Log("module =" + module.m_className);

            
            log.Info("Moudle Start Load =" + module.m_dllName);
            Type t = LoadDll(module.m_dllName, module.m_className);
            if (t == null)
            {
                log.Error("Module Load Fail=" + module.m_dllName);
                break;
            }
            MethodInfo method = t.GetMethod(module.m_funcName);
            if (method == null)
            {
                log.Error("Method Not Found=" + module.m_dllName);
                break;
            }

            IEnumerator iter = method.Invoke(null, null) as IEnumerator;
            if (iter == null)
            {
                log.Error("Method Return Value Is Not IEnumerator Module Name=" + module.m_dllName);
                break;
            }
            yield return iter;
            
            log.Info("Moudle Call Finish =" + module.m_dllName);
        }
    }

    IEnumerator LoadModulesNoHotfix()
    {
        log.Info("Hotfix Module Load");
        yield return Hotfix.Main();

        log.Info("Main Module Load");
        yield return Game.Main();
    }

    // 加载startup
    IEnumerator Run()
    {
        // 初始化基础共
        yield return BaseModuleInitalize();
        
        log.Info("BaseModuleInitalize Finish");
        
        Debug.Log("call 2!!!");
        
        if (useHotfix)
            LoadAOTMeta();

        if (useHotfix)
            yield return LoadModules();
        else
            yield return LoadModulesNoHotfix();
        
        log.Info("LoadAOTMeta Finish");
        
        Debug.Log("LoadAOTMeta game scene!!!");


       

        // 销毁startup
        Destroy(gameObject);
    }

    bool useHotfix
    {
        get
        {
            /*
            #if UNITY_EDITOR
            if (!m_useHotfixCode)
                return false;
            #endif
            
            return true;
            */
            return false;
        }
    }
    
    Type LoadDll(string module, string className)
    {
        Assembly hotUpdateAss = null;
        if (useHotfix)
        {
            //Assembly hotUpdateAss = Assembly.Load(File.ReadAllBytes($"{Application.streamingAssetsPath}/HotUpdate.dll.bytes"));
            AssetRequest req = libx.Assets.LoadAsset(HOTFIX_PATH + module + ".dll.bytes", typeof(TextAsset));
            if (req == null || req.asset == null)
                return null;
            var asset = req.asset as TextAsset;
            if (asset == null || asset.bytes == null)
                return null;
            hotUpdateAss = Assembly.Load(asset.bytes);
        }
        else
        {
            hotUpdateAss = System.AppDomain.CurrentDomain.GetAssemblies().First(a => a.GetName().Name == module);
        }

        Type t = hotUpdateAss.GetType(className);
        return t;
    }

    /// <summary>
    /// 加载AOT 原数据
    /// </summary>
    void LoadAOTMeta()
    {
        //#if !UNITY_EDITOR 
            const string AOTBasePath = "Assets/BuildAsset/AOTMeta/";
            foreach (var aotDllName in AOTGenericReferences.PatchedAOTAssemblyList)
            {
                string dllPath = AOTBasePath + aotDllName + ".bytes";
                var req = Assets.LoadAsset(dllPath, typeof(TextAsset));
                var asset = req.asset as TextAsset;
                byte[] dllBytes = asset.bytes;
                LoadImageErrorCode err = HybridCLR.RuntimeApi.LoadMetadataForAOTAssembly(dllBytes, HomologousImageMode.SuperSet);
                if (err != LoadImageErrorCode.OK)
                {
                    log.Error("Load Dll Meta Fail File=" + aotDllName + " err=" + err);
                }
                req.Release();
            }
        //#endif
    }
    
    // 设置加载路径
    static public void SetupAssetPath()
    {
#if UNITY_EDITOR
        // 没开启更新流程直接进入游戏
        //if (Assets.useVersionUpdate == false)
        if (true)
        {
            return;
        }
#endif
        Assets.updatePath = GetUpdatePath();
    }
    
    public static string GetUpdatePath()
    {
        return string.Format("{0}{1}DLC{1}", Application.persistentDataPath, Path.DirectorySeparatorChar);
    }
    
    // 解决场景初始话可能导致失败的问题
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
#if UNITY_DISABLE_AUTOMATIC_SYSTEM_BOOTSTRAP
        log.Info("UNITY_DISABLE_AUTOMATIC_SYSTEM_BOOTSTRAP Init!");
        Unity.Entities.DefaultWorldInitialization.Initialize("Default World", false);
#endif
    }
}
