/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Cookbook
{
    public partial class UI_WorkerCollectPropertyUI : GLabel
    {
        public GLabel m_body;
        public GList m_list;
        public const string URL = "ui://n2tgmsyuadfx2o";

        public static UI_WorkerCollectPropertyUI CreateInstance()
        {
            return (UI_WorkerCollectPropertyUI)UIPackage.CreateObject("Cookbook", "WorkerCollectPropertyUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_body = (GLabel)GetChildAt(0);
            m_list = (GList)GetChildAt(1);
        }
    }
}