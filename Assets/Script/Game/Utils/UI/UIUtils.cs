using SGame.UI;
using Unity.Entities;
namespace SGame
{
    public class UIUtils
    {
        public static int GetUI(string name)
        {
            if (ConfigSystem.Instance.TryGet<GameConfigs.ui_resRowData>((GameConfigs.ui_resRowData conf) =>
            {
                return conf.Name == name;
            }, out GameConfigs.ui_resRowData conf))
            {
                return conf.Id;
            }

            // 找不到ID
            return 0;
        }

        /// <summary>
        /// 关闭UI
        /// </summary>
        /// <param name="mgr"></param>
        /// <param name="ui">UI 的Entity对象</param>
        public static void CloseUI(EntityManager mgr, Entity ui)
        {
            if (mgr.HasComponent<UIWindow>(ui))
            {
                UIWindow window = mgr.GetComponentData<UIWindow>(ui);
                if (window.Value != null)
                {
                    window.Value.Close();
                }
            }
        }
    }
}