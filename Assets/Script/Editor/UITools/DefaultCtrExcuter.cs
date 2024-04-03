using System.Linq;
using System.Text;
using System;
using FairyGUI;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;

public class DefaultCtrExcuter : IUIExcute
{
	public string step => "ui";
	public Type type = typeof(Controller);
	public string typeName = "Controller";

	public void Excute(string type, string name, string parentType, StringBuilder init, StringBuilder uninit, StringBuilder call)
	{
		var flag2 = Condition(type, name);
		if (flag2)
		{

			var newName = UIImportUtils.GetMethodName(name, parentType);
			if (newName.Contains("__")) return;

			var callMethod = $"\t\tvoid _On{newName}Changed(EventContext data){{\n\t\t\tOn{newName}Changed(data);\n\t\t}}";
			var pMethod = $"\t\tpartial void On{newName}Changed(EventContext data);";
			var sMethod = $"\t\tvoid Switch{newName}Page(int index)=>m_view.{name}.selectedIndex=index;";
			init.AppendLine($"\t\t\tm_view.{name}.onChanged.Add(new EventCallback1(_On{newName}Changed));");
			uninit.AppendLine($"\t\t\tm_view.{name}.onChanged.Remove(new EventCallback1(_On{newName}Changed));");
			call.AppendLine(callMethod);
			call.AppendLine(pMethod);
			call.AppendLine(sMethod);

		}
	}

	private bool Condition(string type, string name)
	{
		var n = name.Split('.').Last();
		if (n == "m_button") return false;
		if (type == typeName)
			return true;
		var select = UIImportUtils.GetTypeByName(type);
		if (select != null)
			return this.type.IsAssignableFrom(select);

		return false;
	}

}