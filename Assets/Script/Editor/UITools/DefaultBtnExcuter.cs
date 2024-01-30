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
		var flag3 = name.ToLower().EndsWith("body") || name.ToLower().EndsWith("body");

		if (flag2 || flag || flag3 )
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
			var mname = $"On{newName}Click";
			var callMethod = default(string) ;
            var pMethod = default(string);
			if (!flag3)
			{

				callMethod = $"\t\tvoid _{mname}(EventContext data){{\n\t\t\t{mname}(data);\n{estr}\t\t}}";
				pMethod = $"\t\tpartial void {mname}(EventContext data);";

				init.AppendLine($"\t\t\tUIListener.{method}(m_view.{name}, new EventCallback1(_{mname}));");
				uninit.AppendLine($"\t\t\tUIListener.{method}(m_view.{name}, new EventCallback1(_{mname}),remove:true);");
			}
			else
			{
				var space = "\n\t\t\t ";
				var bstr = $"{space}bool __closestate = true;";
				mname = "OnUICloseClick";
				method = "ListenerClose";
				pMethod = $"\t\tpartial void {mname}(ref bool state);";
				callMethod = $"\t\tvoid DoCloseUIClick(EventContext data){{{bstr}{space}{mname}(ref __closestate);{space}if(__closestate)SGame.UIUtils.CloseUIByID(__id);{space}{estr}\n\t\t}}";
				
				init.AppendLine($"\t\t\tUIListener.{method}(m_view.{name}, new EventCallback1(DoCloseUIClick));");
				uninit.AppendLine($"\t\t\tUIListener.{method}(m_view.{name}, new EventCallback1(DoCloseUIClick),remove:true);");
			}
            call.AppendLine(callMethod);
            call.AppendLine(pMethod);

        }
    }

    private bool Condition(string type, string name)
    {
        if (type == typeName )
            return true;
        if (type.ToLower().EndsWith(nameFilter) || name.ToLower().EndsWith(nameFilter) )
            return true;

        var select = UIImportUtils.GetTypeByName(type);
        if (select != null)
            return this.type.IsAssignableFrom(select);

        return false;
    }

}