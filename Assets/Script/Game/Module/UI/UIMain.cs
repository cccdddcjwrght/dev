using SGame.UI;

namespace SGame
{
    public class UIMain : IUIScript
    {
        public void OnInit(UIContext context)
        {
            context.onUpdate += onUpdate;
        }

        private void onUpdate(UIContext context)
        {
            
        }
    }
}
