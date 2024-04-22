/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Pet
{
    public partial class UI_RandomSelectUI : GLabel
    {
        public GList m_list;
        public Transition m_show;
        public const string URL = "ui://srlw77ob835x4d";

        public static UI_RandomSelectUI CreateInstance()
        {
            return (UI_RandomSelectUI)UIPackage.CreateObject("Pet", "RandomSelectUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_list = (GList)GetChildAt(1);
            m_show = GetTransitionAt(0);
        }
    }
}