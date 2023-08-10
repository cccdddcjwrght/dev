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
            var clickBtn = context.content.GetChildByPath("battle.icon").asButton;
            clickBtn.onClick.Add(OnClick);
        }

        public static IUIScript Create() { return new UIMain(); }

        void OnClick()
        {
            EventManager.Instance.Trigger((int)GameEvent.PLAYER_ROTE_DICE);
        }
        

        private void onUpdate(UIContext context)
        {
            
        }
    }
}
