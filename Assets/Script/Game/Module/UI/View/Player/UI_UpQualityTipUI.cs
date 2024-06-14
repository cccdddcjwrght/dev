/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Player
{
    public partial class UI_UpQualityTipUI : GLabel
    {
        public GGraph m_maskbg;
        public UI_UpQualityTipBody m_body;
        public const string URL = "ui://cmw7t1elzdz650";

        public static UI_UpQualityTipUI CreateInstance()
        {
            return (UI_UpQualityTipUI)UIPackage.CreateObject("Player", "UpQualityTipUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_maskbg = (GGraph)GetChildAt(0);
            m_body = (UI_UpQualityTipBody)GetChildAt(1);
        }
    }
}