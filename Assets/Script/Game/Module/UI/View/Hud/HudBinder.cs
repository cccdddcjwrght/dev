/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;

namespace SGame.UI.Hud
{
    public class HudBinder
    {
        public static void BindAll()
        {
            UIObjectFactory.SetPackageItemExtension(UI_Update.URL, typeof(UI_Update));
            UIObjectFactory.SetPackageItemExtension(UI_HudUI.URL, typeof(UI_HudUI));
            UIObjectFactory.SetPackageItemExtension(UI_FloatText.URL, typeof(UI_FloatText));
            UIObjectFactory.SetPackageItemExtension(UI_OrderTip.URL, typeof(UI_OrderTip));
            UIObjectFactory.SetPackageItemExtension(UI_Progress.URL, typeof(UI_Progress));
            UIObjectFactory.SetPackageItemExtension(UI_FoodTipUI.URL, typeof(UI_FoodTipUI));
        }
    }
}