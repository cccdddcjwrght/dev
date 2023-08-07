using log4net;
using Unity.Collections;
using Unity.Entities;

// UI生成系统
namespace SGame.UI
{
    public struct UIInitalized : IComponentData
    {
    }

    [DisableAutoCreation]
    public partial class SpawnUISystem : SystemBase
    {
        // UI包加载请求
        private ResourceLoader<UIPackageRequest>        m_packageRequest;
        private EndSimulationEntityCommandBufferSystem  m_commandSystem;

        private static ILog log = LogManager.GetLogger("xl.ui");

        protected override void OnCreate()
        {
            base.OnCreate();
            m_packageRequest = new ResourceLoader<UIPackageRequest>(PackageRequestFactory);
            m_commandSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
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
        UIPackageRequest LoadPackage(string uiPackage)
        {
            return m_packageRequest.Load(uiPackage);
        }

        protected override void OnUpdate()
        {
            EntityCommandBuffer comamndBuffer = new EntityCommandBuffer(Allocator.Temp);

            // 1. 生成Package加载
            Entities.WithNone<UIWindow, UIInitalized>().ForEach((Entity e, UIRequest request) =>
            {
                var ui = new UIWindow()
                {
                    uiPackage = m_packageRequest.Load(request.m_uiPackageName)
                };
                comamndBuffer.AddComponent<UIWindow>(e);
                comamndBuffer.SetComponent(e, ui);
            }
            ).WithStructuralChanges().WithoutBurst().Run();
            
            // 2. UI 加载中
            Entities.WithNone<UIInitalized>().ForEach((Entity e, UIRequest request, UIWindow window) =>
                {
                    // 1. 判断包是否加载成功
                    if (string.IsNullOrEmpty(window.uiPackage.error))
                    {
                        log.Error("Load UI Package Fail=" + window.uiPackage.error);
                        comamndBuffer.DestroyEntity(e);
                        return;
                    }

                    // 2. 等待UIPackage 加载完成
                    if (!window.uiPackage.isDone)
                        return;

                    // 3. 加载GObject
                    if (window.gObject == null)
                    {
                        window.gObject = window.uiPackage.uiPackage.CreateObject(request.m_uiName);
                        if (window.gObject == null)
                        {
                            log.Error("Package " + request.m_uiPackageName + " ui=" + request.m_uiName + " not found");
                            comamndBuffer.DestroyEntity(e);
                            return;
                        }
                    }
                    
                    // 4. 创建WINDOW
                    var fui = new FairyWindow();
                    fui.bringToFontOnClick = false; // 点击不会改变顺序
                    fui.contentPane = window.gObject.asCom;
                    fui.Initalize();

                    // 5. 设置加载完成标记
                    comamndBuffer.RemoveComponent<UIRequest>(e);
                    comamndBuffer.AddComponent<UIInitalized>(e);
                }
            ).WithStructuralChanges().WithoutBurst().Run();
            
            // 3. 更新 FairyGUI 包的加载
            m_packageRequest.Update();
            
            // m_commandSystem.AddJobHandleForProducer(Dependency);
        }

    }
}