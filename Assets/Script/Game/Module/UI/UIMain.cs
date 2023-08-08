using SGame.UI;
using FairyGUI;
using log4net;
namespace SGame
{
    public class UIMain : IUIScript
    {
        private static ILog log = LogManager.GetLogger("xl.gameui");
        
        public void OnInit(UIContext context)
        {
            context.onUpdate += onUpdate;
            var clickBtn = context.content.GetChild("battle").asButton;
            clickBtn.onClick.Add(OnClick);
        }

        public static IUIScript Create() { return new UIMain(); }

        void OnClick()
        {
            log.Info("On Click");
            
        }
        

        private void onUpdate(UIContext context)
        {
            
        }
    }
}
