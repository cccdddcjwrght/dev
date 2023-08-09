
using SGame.UI;
namespace SGame
{
    public class UIPreprocess : IPreprocess
    {
        public void Init(UIContext context)
        {
            
        }

        public bool GetUIInfo(int configId, out string comName, out string pkgName)
        {
            comName = null;
            pkgName = null;
            return false;
        }
    }
}