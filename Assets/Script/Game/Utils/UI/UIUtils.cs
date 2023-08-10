using SGame.UI;

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
    }
}