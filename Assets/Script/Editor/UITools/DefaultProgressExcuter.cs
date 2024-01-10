using System.Linq;
using System.Text;
using System;
using FairyGUI;
using System.Collections.Generic;

public class DefaultProgressExcuter : IUIExcute
{
    public string step => "ui";
    public static Type[] types = { typeof(GProgressBar), typeof(GSlider)  };
    public static List<string> typeNames = types.Select(t => t.Name).ToList();
    public static string nameFilter="progress";

    public void Excute(string type, string name, string parentType, StringBuilder init, StringBuilder uninit, StringBuilder call)
    {
        var flag2 = Condition(type, name);
        if (flag2)
        {

			var newName = UIImportUtils.GetMethodName(name, parentType);
			var callMethod = $"\t\tvoid Set{newName}Value(float data)=>UIListener.SetValue(m_view.{name},data);";
			var callMethod1 = $"\t\tfloat Get{newName}Value()=>UIListener.GetValue(m_view.{name});";

			call.AppendLine(callMethod);
			call.AppendLine(callMethod1);

		}
	}

    private bool Condition(string type, string name)
    {
        if (typeNames.Contains(type))
            return true;
        if (type.ToLower().EndsWith(nameFilter) || name.ToLower().EndsWith(nameFilter))
            return true;
        var select = UIImportUtils.GetTypeByName(type);
        if (select != null)
            return types.Any(t=>t.IsAssignableFrom(select));

        return false;
    }

}