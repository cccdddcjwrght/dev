using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using System.IO;
using System.Text;

namespace ZEditors
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class ZEditorAttribute : Attribute
	{
		/// <summary>
		/// 模块名
		/// </summary>
		public string module;
		public string desc;
		/// <summary>
		/// 菜单路径
		/// </summary>
		public string menu;
		/// <summary>
		/// 显示名
		/// </summary>
		public string name;

		public ZEditorAttribute() { }

		public ZEditorAttribute(string module)
		{
			this.module = module;
		}
	}

	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class ZEditorCmdAttribute : Attribute
	{
		/// <summary>
		/// 命令key
		/// </summary>
		public string key;
		/// <summary>
		/// 是否标识符
		/// </summary>
		public string check;
		/// <summary>
		/// 执行的操作
		/// </summary>
		public string cmd;
		/// <summary>
		/// 显示名
		/// </summary>
		public string name;
		/// <summary>
		/// 菜单路径
		/// </summary>
		public string menu;
		/// <summary>
		/// 宏
		/// </summary>
		public string symbol;
		/// <summary>
		/// 优先级
		/// </summary>
		public int priority;
		/// <summary>
		/// 条件显示
		/// </summary>
		public bool hasConditon;
		public object[] args;
		/// <summary>
		/// 需要刷新
		/// </summary>
		public bool refresh;

		public ZEditorCmdAttribute(string key)
		{

			this.key = key;
		}

		public ZEditorCmdAttribute(string key, string cmd)
		{
			this.key = key;
			this.cmd = cmd;
		}

	}

	public class ZEditorSymbol : Attribute
	{
		public string key;
		public string symbol;
		public string title;
		public string desc;
		public ZEditorSymbol(string key)
		{
			this.key = key;
		}
	}

	public interface IZEditor { }

	public static class ZEditorUtil
	{

		#region Temp

		const string C_TOOLS_METHOD = @"
namespace ZEditors
{
	using System.Collections.Generic;
	using UnityEditor;
	using System.Linq;

	public static partial class EditorTools
	{

		public const string C_EDITOR_BASE_STR = ""Tools/"";


	static public void ChangeSymbol(string symbol, bool refresh = false, bool removed = false)
		{
			if (string.IsNullOrEmpty(symbol)) return;
			var tg = GetGroup();
			var s = PlayerSettings.GetScriptingDefineSymbolsForGroup(tg);
			var ss = string.IsNullOrEmpty(s) ? new List<string>() : s.Split(';').ToList();
			var ads = symbol.Split(';');
			ss = ss.Except(ads).ToList();
			if (!removed) ss.AddRange(ads);
			s = string.Join(';', ss);
			PlayerSettings.SetScriptingDefineSymbolsForGroup(tg, s);
			if (refresh)
			{
				AssetDatabase.SaveAssets();
				AssetDatabase.Refresh();
			}
		}

		static public bool CheckHasSymbol(string symbol)
		{
			if (string.IsNullOrEmpty(symbol)) return true;
			var tg = GetGroup();
			var s = PlayerSettings.GetScriptingDefineSymbolsForGroup(tg);
			return !string.IsNullOrEmpty(s) && s.Contains(symbol);
		}

		static public BuildTargetGroup GetGroup()
		{
			return EditorUserBuildSettings.selectedBuildTargetGroup;
		}
	} 
}

";

		#endregion

		const string C_PATH = "Assets/!Auto/Editor/";
		const string C_SPACE_METHOD = "\t\t";
		const string C_FLAG = "__editormenuflag";

		static private bool G_NEED_UPDATE = false;
		static private StringBuilder SBuilder = new StringBuilder();
		static private System.Reflection.BindingFlags Flag = System.Reflection.BindingFlags.Static
			| System.Reflection.BindingFlags.NonPublic
			| System.Reflection.BindingFlags.Public
			| System.Reflection.BindingFlags.IgnoreCase
			| System.Reflection.BindingFlags.FlattenHierarchy;

		[UnityEditor.Callbacks.DidReloadScripts]
		[InitializeOnLoadMethod]
		static void Init()
		{
			if (!Directory.Exists(C_PATH))
			{
				Directory.CreateDirectory(C_PATH);
				CreateBaseCmdClass();
			}
			var f = EditorPrefs.GetBool(C_FLAG, false);
			if (!f)
			{
				var ts = AppDomain
					.CurrentDomain
					.GetAssemblies()
					.SelectMany(a => a.GetTypes())
					.Where(t => !t.IsAbstract
					&& !t.IsGenericType
					&& typeof(IZEditor).IsAssignableFrom(t)
					&& t.GetCustomAttributes(typeof(ZEditorAttribute), true).Length > 0)
					.ToList();

				ts.ForEach(InitEditor);

				if (G_NEED_UPDATE)
				{
					G_NEED_UPDATE = false;
					EditorPrefs.SetBool(C_FLAG, true);
					AssetDatabase.Refresh();
					AssetDatabase.Refresh();
				}
			}
			else
				EditorPrefs.SetBool(C_FLAG, false);
		}

		static void InitEditor(Type type)
		{
			if (type != null)
			{
				var a = Attribute.GetCustomAttribute(type, typeof(ZEditorAttribute)) as ZEditorAttribute;
				if (a == null) return;
				a.module = a.module ?? type.Name;
				a.menu = a.menu ?? "[Tools]/" + (a.name ?? a.module);

				var dName = a.module;
				var dir = C_PATH + dName + "/";
				var cmds = (type.GetCustomAttributes(typeof(ZEditorCmdAttribute), true) as ZEditorCmdAttribute[]).ToList();
				var symbols = type.GetCustomAttributes(typeof(ZEditorSymbol), false) as ZEditorSymbol[];
				var flag = false;

				if (symbols != null && symbols.Length > 0)
				{
					foreach (var item in symbols)
						cmds.AddRange(CreateSymbolCmdMenu(type, item, a, dir));
				}

				if (cmds != null && cmds.Count > 0)
				{
					foreach (var item in cmds)
						flag = CreateMenu(type, a, item, dir) || flag;
				}
				else if (Directory.Exists(dir))
				{
					Directory.Delete(dir, true);
					flag = true;
				}
				G_NEED_UPDATE = G_NEED_UPDATE || flag;
			}
		}

		static ZEditorCmdAttribute[] CreateSymbolCmdMenu(Type type, ZEditorSymbol symbol, ZEditorAttribute zEditor, string dir)
		{

			if (symbol != null && !string.IsNullOrEmpty(symbol.key))
			{
				var p = symbol.key.GetHashCode();
				var cmd = new ZEditorCmdAttribute(symbol.key + "_Add")
				{
					cmd = $"EditorTools.ChangeSymbol(\"{symbol.symbol}\" , true)",
					hasConditon = true,
					symbol = "!" + symbol.symbol,
					name = "[Add]" + (symbol.title ?? symbol.key),
					priority = p
				};
				var cmd1 = new ZEditorCmdAttribute(symbol.key + "_Remove")
				{
					cmd = $"EditorTools.ChangeSymbol(\"{symbol.symbol}\" , true , true)",
					hasConditon = true,
					symbol = symbol.symbol,
					name = "[Remove]" + (symbol.title ?? symbol.key),
					priority = p
				};
				return new ZEditorCmdAttribute[] { cmd, cmd1 };
			}
			return null;
		}

		static bool CreateMenu(Type type, ZEditorAttribute zEditor, ZEditorCmdAttribute cmd, string dir)
		{
			if (cmd != null)
			{
				var fileName = zEditor.module + "/EditorMenus." + zEditor.module + "." + cmd.key;
				var methodName = "Do_" + zEditor.module + "_" + cmd.key;
				var beforeName = "Before" + methodName;
				var afterName = "After" + methodName;
				var fieldName = "toggle_" + zEditor.module + "_" + cmd.key;
				var conditonName = methodName + "Condition";
				var sb = SBuilder;
				var menu = cmd.menu ?? Path.Combine(zEditor.menu, cmd.name ?? cmd.key).Replace('\\', '/');
				var sy = !string.IsNullOrEmpty(cmd.symbol);
				var sign = (sy && cmd.symbol[0] != '!') ? "" : "!";
				if (sy) cmd.symbol = cmd.symbol.Replace("!", "");
				sb.Clear()
					.AppendLine($"{C_SPACE_METHOD}[MenuItem(\"{menu}\",false,{cmd.priority})]")
					.AppendLine($"{C_SPACE_METHOD}static void {methodName}(){{")
					.AppendLine($"{C_SPACE_METHOD}{C_SPACE_METHOD}{beforeName}();")
					.Append($"{C_SPACE_METHOD}{C_SPACE_METHOD}Excute(")
					.Append($"\"{zEditor.module}\",")
					.Append($"\"{cmd.key}\",")
					.Append($"_{methodName}")
					.AppendLine($");")
					.AppendLine($"{C_SPACE_METHOD}{C_SPACE_METHOD}{afterName}();")
					.AppendLine(cmd.refresh ? $"{C_SPACE_METHOD}{C_SPACE_METHOD}AssetDatabase.Refresh();" : "")
					.AppendLine($"{C_SPACE_METHOD}}}")
					.AppendLine("")
					.AppendLine($"{C_SPACE_METHOD}static partial void {beforeName}();")
					.AppendLine($"{C_SPACE_METHOD}static partial void {afterName}();");


				sb.AppendLine($"{C_SPACE_METHOD}static void _{methodName}(){{");
				if (!string.IsNullOrEmpty(cmd.check))
				{
					if (sy)
						sb.AppendLine($"{C_SPACE_METHOD}{C_SPACE_METHOD}{cmd.check}={sign}EditorTools.CheckHasSymbol(\"{cmd.symbol}\");");
					sb.AppendLine($"{C_SPACE_METHOD}{C_SPACE_METHOD}{cmd.check}=!{cmd.check};");
					sb.AppendLine($"{C_SPACE_METHOD}{C_SPACE_METHOD}Menu.SetChecked(\"{menu}\", {cmd.check}); ");
					if (sy)
						sb.AppendLine($"{C_SPACE_METHOD}{C_SPACE_METHOD}EditorTools.ChangeSymbol(\"{cmd.symbol}\", true,!{cmd.check}); ");
				}
				else
					CreateExcuteMethod(sb, type, zEditor, cmd);

				sb.AppendLine($"{C_SPACE_METHOD}}}");
				if (cmd.hasConditon)
				{
					sb.AppendLine($"{C_SPACE_METHOD}[MenuItem(\"{menu}\", true,{cmd.priority})]");
					sb.AppendLine($"{C_SPACE_METHOD}static bool {conditonName}(){{");
					if (sy) sb.AppendLine($"{C_SPACE_METHOD}{C_SPACE_METHOD}return {sign}EditorTools.CheckHasSymbol(\"{cmd.symbol}\");");
					else CreateExcuteMethod(sb, type, zEditor, cmd, null, true);
					sb.AppendLine($"{C_SPACE_METHOD}}}");
				}

				CreateEditorClass(fileName, sb.ToString());
				return true;
			}
			return false;
		}

		static void CreateExcuteMethod(StringBuilder sb, Type type, ZEditorAttribute editor, ZEditorCmdAttribute cmd, string space = null, bool condi = false)
		{
			if (sb == null) return;
			var body = "";
			space += C_SPACE_METHOD + C_SPACE_METHOD;
			System.Reflection.MethodInfo method = default;
			var args = string.Empty;

			if (cmd.args != null && cmd.args.Length > 0)
			{
				foreach (var item in cmd.args)
				{
					if (item is string s) args += "\"" + s + "\",";
					else if (item == null) args += "null,";
					else args += item + ",";
				}
				args = args.Substring(0, args.Length - 1);
			}


			if (condi || string.IsNullOrEmpty(cmd.cmd) || (!cmd.cmd.Contains(".") && !cmd.cmd.EndsWith(")")))
			{
				var mName = cmd.cmd ?? "Do" + cmd.key;
				if (condi) mName += "Condition";
				method = type.GetMethod(mName, Flag);
				if (method == null)
				{
					if (condi)
						sb.AppendLine($"{space}reture true;");
					return;
				}
				if (condi && !(method.ReturnType != null && method.ReturnType == typeof(bool)))
				{
					sb.AppendLine($"{space}reture true;");
					return;
				}

				if (!method.IsPublic)
				{
					sb.AppendLine($"{space}var method = typeof({type.FullName}).GetMethod(\"{mName}\", (System.Reflection.BindingFlags){(int)Flag});");
					sb.AppendLine($"{space}var ret = method?.Invoke(null,new object[]{{{args}}});");
					if (condi)
						sb.AppendLine($"{space}if(ret is bool v) return v; return true;");
					return;
				}
				body = type.FullName + "." + method.Name;
			}
			else
			{
				if (cmd.cmd.EndsWith(")"))
				{
					if (cmd.cmd.Contains('.')) sb.AppendLine($"{space}{cmd.cmd};");
					else sb.AppendLine($"{space}{type.FullName + "." + cmd.cmd};");
					return;
				}
				body = cmd.cmd;

			}
			if (!body.Contains('.'))
				body = type.FullName + "." + body;
			body += $"({args})";
			sb.AppendLine($"{space}{body};");

		}

		static void CreateEditorClass(string fileName = null, string body = null, bool needref = false)
		{
			var sb = SBuilder;
			sb.Clear();
			fileName = fileName ?? "EditorMenus";
			sb.AppendLine("using System;");
			sb.AppendLine("using System.Collections.Generic;");
			sb.AppendLine("using System.Linq;");
			sb.AppendLine("using UnityEngine;");
			sb.AppendLine("using UnityEditor;");
			if (needref)
				sb.AppendLine("using System.Reflection; ");
			sb.AppendLine("namespace ZEditors.Cmds{");
			sb.AppendLine("\tpublic static partial class EditorMenus{");
			if (!string.IsNullOrEmpty(body))
				sb.AppendLine(body);
			sb.AppendLine("\t}");
			sb.AppendLine("}");
			var file = C_PATH + fileName + ".cs";
			var dir = Path.GetDirectoryName(file);
			if (!Directory.Exists(dir))
				Directory.CreateDirectory(dir);
			File.WriteAllText(file, sb.ToString(), Encoding.UTF8);
		}

		static void CreateBaseCmdClass()
		{
			var body = C_TOOLS_METHOD;
			var file = C_PATH + "EditorTools.cs";
			var dir = Path.GetDirectoryName(file);
			if (!Directory.Exists(dir))
				Directory.CreateDirectory(dir);
			File.WriteAllText(file, body, Encoding.UTF8);

		}
	}


}

namespace ZEditors.Cmds
{
	public static partial class EditorMenus
	{
		static void Excute(string module, string cmd, Action excute, Action before = null, Action after = null)
		{
			try
			{
				before?.Invoke();
				excute?.Invoke();
				after?.Invoke();
			}
			catch (Exception e)
			{
				Debug.LogError($"[{module}.{cmd}] Error!!! \n------\n{e.Message}\n------\n{e.StackTrace}");
			}

		}
	}



}