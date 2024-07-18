using log4net;
using Unity.Entities;

// UI生成系统
namespace SGame.UI
{
    // UI加载完毕
    public struct UIInitalized : IComponentData
    {
    }

    [UpdateInGroup(typeof(UIGroup))]
    public partial class SpawnUISystem : SystemBase
    {
        // UI包加载请求
        private ResourceLoader<UIPackageRequest>        m_packageRequest;
        private EndSimulationEntityCommandBufferSystem  m_commandSystem;
        private UIScriptFactory                         m_scriptFactory;
        private GameWorld                               m_gameWorld;
        private IPreprocess                             m_preprocess;

        private static ILog log = LogManager.GetLogger("xl.ui");
        private bool isInit = false;

        protected override void OnCreate()
        {
            base.OnCreate();
            m_packageRequest = new ResourceLoader<UIPackageRequest>(PackageRequestFactory);
            m_commandSystem = World.GetOrCreateSystemManaged<EndSimulationEntityCommandBufferSystem>();
            isInit = false;
        }

        public void Initalize( GameWorld gameWorld, UIScriptFactory factory, IPreprocess preprocess)
        {
            m_gameWorld     = gameWorld;
            m_scriptFactory = factory;
            m_preprocess    = preprocess;
            isInit = true;
        }

        // 创建UIPackageRequest
        UIPackageRequest PackageRequestFactory()
        {
            var ret = new UIPackageRequest(LoadPackage);
            return ret;
        }
        
        /// <summary>
        /// 通过包名加载 FairyGUI 的UI包
        /// </summary>
        /// <param name="uiPackage">ui包名</param>
        /// <return>加载句柄</return>
        public UIPackageRequest LoadPackage(string uiPackage)
        {
            return m_packageRequest.Load(uiPackage);
        }

        UIContext CreateContext(Entity e, FairyGUI.GComponent content, int configId )
        {
            UIContext context = new UIContext();
            context.gameWorld = m_gameWorld;
            context.uiModule = UIModule.Instance;
            context.entity = e;
            context.content = content;
            context.configID = configId;
            return context;
        }

        protected override void OnUpdate()
        {
            if (!isInit)
                return;
            
            //EntityCommandBuffer comamndBuffer = m_commandSystem.CreateCommandBuffer();// new EntityCommandBuffer(Allocator.Temp);

            // 1. 生成Package加载
            Entities.WithNone<UIWindow, UIInitalized, DespawningEntity>().ForEach((Entity e, UIRequest request) =>
            {
                if (request.configId == 0 && (string.IsNullOrEmpty(request.comName) || string.IsNullOrEmpty(request.pkgName)))
                {
                    log.Error("GET UI CONFIG ID IS ZERO");
                    EntityManager.DestroyEntity(e);
                    return;
                }
                
                if (request.configId != 0 && m_preprocess != null)
                {
                    string comName;
                    string pkgName;

                    if (m_preprocess.GetUIInfo(request.configId, out comName, out pkgName))
                    {
                        request.comName = comName;
                        request.pkgName = pkgName;
                    }
                    else
                    {
                        log.Error("Get UIInfo Fail ID=" + request.configId);
                        EntityManager.DestroyEntity(e);
                        return;
                    }
                }
                var ui = new UIWindow()
                {
                    uiPackage = m_packageRequest.Load(request.pkgName)
                };
                EntityManager.AddComponentData(e, ui);
				UIRequestMgr.Remove(request.configId);
            }
            ).WithStructuralChanges().WithoutBurst().Run();
            
            // 2. UI 加载中
            Entities.WithNone<UIInitalized, DespawningEntity>().ForEach((Entity e, UIRequest request, UIWindow window) =>
                {
                    // 1. 判断包是否加载成功
                    if (!string.IsNullOrEmpty(window.uiPackage.error))
                    {
                        log.Error("Load UI Package Fail=" + window.uiPackage.error);
                        EntityManager.AddComponent<DespawningEntity>(e);
                        return;
                    }

                    // 2. 等待UIPackage 加载完成
                    if (!window.uiPackage.isDone)
                        return;

                    // 3. 加载GObject
                    if (window.gObject == null)
                    {
                        window.gObject = window.uiPackage.uiPackage.CreateObject(request.comName);
                        if (window.gObject == null)
                        {
                            log.Error("Package " + request.pkgName + " ui=" + request.comName + " not found");
                            EntityManager.DestroyEntity(e);
                            return;
                        }
                    }
                    
                    // 4. 创建WINDOW
                    var fui = new FairyWindow();
                    fui.bringToFontOnClick = false; // 点击不会改变顺序
                    var gCom = window.gObject.asCom;
                    fui.contentPane = gCom;
                    
                    IUIScript script = m_scriptFactory.Create(new UIInfo() { comName = request.comName, pkgName = request.pkgName });
                    UIContext context = CreateContext(e, gCom, request.configId);
                    context.window = fui;
                    window.Value = fui;
                    window.entity = e;
                    
                    // UI初始化前的预处理, 比如配置表相关的设置
                    fui.Initalize(script, context);
                    if (m_preprocess != null)
                    {
                        m_preprocess.Init(context);
                        fui.Show();
                        m_preprocess.AfterShow(context);
                    }
                    else
                    {
                        fui.Show();
                    }

                    // 5. 设置加载完成标记
                    EntityManager.RemoveComponent<UIRequest>(e);
                    EntityManager.AddComponent<UIInitalized>(e);
                }
            ).WithStructuralChanges().WithoutBurst().Run();
            
            // 3. 更新 FairyGUI 包的加载
            m_packageRequest.Update();
        }

    }
}