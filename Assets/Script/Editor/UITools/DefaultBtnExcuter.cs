using System.Linq;
using System.Text;
using System;
using FairyGUI;

public class DefaultBtnExcuter : IUIExcute
{
    public string step => "ui";
    public Type type = typeof(GButton);
    public string typeName = "GButton";
    public string nameFilter = "btn";

    public void Excute(string type, string name, string parentType, StringBuilder init, StringBuilder uninit, StringBuilder call)
    {
        var flag = type.StartsWith(UIImportUtils.CS_PIX);
        var flag2 = Condition(type, name);
        if (flag2 || flag)
        {
            var newName = name.Split('.').Last().Replace("m_", "");
            var method = flag2 ? "Listener" : "ListenerIcon";
            var es = UIImportUtils.MatchEventID(newName);
            var estr = "";

			if (es!=null && es.Length > 0)
            {
                foreach (var item in es)
                {
                    if (item > 0)
                        estr += $"\t\t\tUIListener.DoTriggerEvent(m_view.{name},{item});\n";
                }
            }
			newName = UIImportUtils.GetMethodName(name, parentType);
			var callMethod = $"\t\tvoid _On{newName}Click(EventContext data){{\n\t\t\tOn{newName}Click(data);\n{estr}\t\t}}";
            var pMethod = $"\t\tpartial void On{newName}Click(EventContext data);";
            init.AppendLine($"\t\t\tUIListener.{method}(m_view.{name}, new EventCallback1(_On{newName}Click));");
            uninit.AppendLine($"\t\t\tUIListener.{method}(m_view.{name}, new EventCallback1(_On{newName}Click),remove:true);");
            call.AppendLine(callMethod);
            call.AppendLine(pMethod);

        }
    }

    private bool Condition(string type, string name)
    {
        if (type == typeName)
            return true;
        if (type.ToLower().EndsWith(nameFilter) || name.ToLower().EndsWith(nameFilter))
            return true;

        var select = UIImportUtils.GetTypeByName(type);
        if (select != null)
            return this.type.IsAssignableFrom(select);

        return false;
    }

}