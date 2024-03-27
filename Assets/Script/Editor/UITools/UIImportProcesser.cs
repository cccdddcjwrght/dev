using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.AssetImporters;
using System.Linq;
using System.IO;
using System.Text;
using System;
using System.Text.RegularExpressions;

public interface IUIExcute
{
	string step { get; }
	void Excute(string type, string name, string parentType, StringBuilder init, StringBuilder uninit, StringBuilder call);
}

public struct UInfo
{
	public string package;//包名
	public string ui;//ui模块名

	public string file;//view名
	public string com;//资源组件名
	public string cls;//输出类名
	public string dir;//输出文件夹路径

	public string[] ToArray()
	{
		return new string[] { package, com, cls, UIImportUtils.C_UI_SPACE + "." + package + "." + package + "Binder.BindAll();" };
	}

}

public static class UIImportUtils
{
	static string C_EVENT_PATTERN = "_e([0-9]*)";


	static public string C_UI_SPACE = "SGame.UI";
	static public string C_UINAME_FORMAT = "UI_{0}UI";
	static public string FILTER_REX = "UI_(.*)UI.cs";
	static public string BASE_PATH = "Assets/Script/Game/Module/UI/View/";
	static public string OUTPUT_PATH = "Assets/Script/Game/Module/UI/Logic/";
	static public string CS_PIX = "UI_";
	static private Dictionary<string, Type> TYPE_CACHES = new Dictionary<string, Type>();

	static public List<string> G_MEMBER_CALL_STRS = new List<string>();

	static public Type GetTypeByName(string name)
	{
		if (!string.IsNullOrEmpty(name))
		{
			if (!TYPE_CACHES.TryGetValue(name, out var type))
			{
				type = AppDomain
							.CurrentDomain
							.GetAssemblies()
							.SelectMany(a => a.GetTypes())
							.FirstOrDefault(a => (a.FullName.Contains(C_UI_SPACE) || a.FullName.Contains("FairyGUI")) && a.Name == name);
				TYPE_CACHES[name] = type;

			}
			return type;
		}
		return default;
	}

	static public string GetMethodName(string name, string parentName)
	{
		var newName = name.Split('.').Last().Replace("m_", "");
		if (name.LastIndexOf(".") != name.IndexOf("."))
			newName = name.Replace(".", "_").Replace("m_", "");
		
		newName = (string.IsNullOrEmpty(parentName) ? "" : parentName + "_") + newName[0].ToString().ToUpper() + newName.Substring(1);
		return RemoveMatchStr(newName.Replace("UI_", ""));
	}

	static public string RemoveMatchStr(string str)
	{
		if (!string.IsNullOrEmpty(str))
			return Regex.Replace(str, C_EVENT_PATTERN, "");
		return str;
	}

	static public int[] MatchEventID(string name)
	{
		var ms = Regex.Matches(name, C_EVENT_PATTERN);
		if (ms != null && ms.Count > 0)
		{
			var m = default(Match);
			return ms
				.Select(i => (m = (Match)i).Success && int.TryParse(m.Groups[1].Value, out var v) ? v : default)
				.Where(i => i > 0).ToArray();
		}
		return default;
	}

	static public bool IsFileMatch(string path, out UInfo info, bool checkexists = true)
	{
		string package = default;
		string ui = default;
		string output = default;
		info = default(UInfo);
		if ((!checkexists || File.Exists(path)) && path.EndsWith(".cs") && path.Contains(BASE_PATH))
		{
			var match = System.Text.RegularExpressions.Regex.Match(Path.GetFileName(path), FILTER_REX);
			if (match.Success)
			{
				package = Path.GetFileName(Path.GetDirectoryName(path));
				ui = match.Groups[1].Value;
				output = Path.Combine(OUTPUT_PATH, package + "_" + ui);

				info.package = package;
				info.ui = ui;
				info.com = ui + "UI";
				info.file = string.Format(C_UINAME_FORMAT, ui);
				info.cls = "UI" + ui[0].ToString().ToUpper() + ui.Substring(1);
				info.dir = Path.Combine(OUTPUT_PATH, package + "_" + info.cls);

				return true;
			}
		}
		return false;
	}

	static public void ReadMemberByTypeName(string typeName, ref List<string[]> members, string append = default)
	{
		if (string.IsNullOrEmpty(typeName))
			return;
		var file = Directory.GetFiles(BASE_PATH, typeName + ".cs", SearchOption.AllDirectories)?.FirstOrDefault();
		ReadMemberFromFile(file, ref members, append, typeName);

	}

	static public void ReadMemberFromFile(string file, ref List<string[]> members, string append = default, string typeName = default)
	{
		if (string.IsNullOrEmpty(file) || !File.Exists(file))
			return;

		var ms = File.ReadAllLines(file)
				.ToList()
				.Where(l => l.Contains("m_") && !l.Contains("="))
				.Select(l => l.Replace("public", "").Replace(";", " " + typeName).Trim().Split(' '))
				.ToList();
		if (ms == null || ms.Count == 0) return;
		var rets = members ?? new List<string[]>();
		var count = 0;
		ms.ForEach(m =>
		{
			var field = string.IsNullOrEmpty(append) ? m[1] : append + m[1];

			if (m[0].StartsWith(CS_PIX) && !m[1].Contains("_nd"))
			{
				count++;
				ReadMemberByTypeName(m[0], ref rets, field + ".");
			}
			m[1] = field;
			rets.Add(m);
		});
		members = rets;
	}
}

public class UIImportProcesser : AssetPostprocessor
{

	#region Templete

	static string C_UI_SPACE = UIImportUtils.C_UI_SPACE;

	const string C_SPACE = "__SPACE__";
	const string C_NAME = "__NAME__";
	const string C_VIEW = "__VIEW__";
	const string C_PACK = "__PACKAGE__";
	const string C_INIT = "//init";
	const string C_UNINIT = "//uninit";
	const string C_CALL = "//call";



	const string C_MAIN_TEMP = @"
namespace __SPACE__{
	using log4net;
	using SGame;
	using SGame.UI.__PACKAGE__;
	
	public partial class __NAME__ : IUIScript
	{
		private static ILog log = LogManager.GetLogger(""ui."" + nameof(__NAME__));

		private __VIEW__ m_view;

		public void OnInit(UIContext context)
		{
			context.onClose += OnClose;
			context.onShown += OnShow;
			m_view = context.content as __VIEW__;
			BeforeInit(context);
			InitUI(context);
			InitEvent(context);
			InitLogic(context);
			AfterInit(context);
		}

		private void OnClose(UIContext context)
		{
			context.onClose -= OnClose;
			context.onShown -= OnShow;
			UnInitUI(context);
			UnInitEvent(context);
			UnInitLogic(context);
			AfterUnInit(context);
			m_view = default;
		}

		partial void BeforeInit(UIContext context);
		partial void InitUI(UIContext context);
		partial void InitEvent(UIContext context);
		partial void InitLogic(UIContext context);
		partial void AfterInit(UIContext context);
		partial void UnInitUI(UIContext context);
		partial void UnInitEvent(UIContext context);
		partial void UnInitLogic(UIContext context);
		partial void AfterUnInit(UIContext context);

		private void OnShow(UIContext context) => DoShow(context);
		partial void DoShow(UIContext context);
	}
}

";

	const string C_UI_TEMP = @"
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace __SPACE__{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.__PACKAGE__;
	
	public partial class __NAME__
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
//init
		}
		partial void UnInitUI(UIContext context){
//uninit
		}
//call
	}
}
";

	const string C_EVENT_TEMP = @"
namespace __SPACE__{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.__PACKAGE__;
	
	public partial class __NAME__
	{
		partial void InitEvent(UIContext context){
//init
		}
		partial void UnInitEvent(UIContext context){
//uninit
		}
	}
}
";

	const string C_LOGIC_TEMP = @"
namespace __SPACE__{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.__PACKAGE__;
	
	public partial class __NAME__
	{
		partial void InitLogic(UIContext context){
//init
		}
		partial void UnInitLogic(UIContext context){
//uninit
		}
	}
}
";

	const string C_REG_TEMP = @"
namespace __SPACE__{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI;
	
	public partial class UIReg
	{
		partial void Register(UIContext context){
//init
		}
	}
}
";

	#endregion

	#region Excutes

	private static Dictionary<string, List<IUIExcute>> _excutes;

	static void InitExcute()
	{

		if (_excutes == null)
		{
			_excutes = AppDomain.CurrentDomain
				.GetAssemblies()
				.SelectMany(a => a.GetTypes())
				.Where(t => t.IsClass && !t.IsGenericType && !t.IsAbstract && !t.Name.StartsWith("<"))
				.Where(t => typeof(IUIExcute).IsAssignableFrom(t))
				.Select(t => Activator.CreateInstance(t, true) as IUIExcute)
				.GroupBy(v => v.step)
				.ToDictionary(group => group.Key, group => group.ToList());
		}
	}

	#endregion

	static public string BASE_PATH = UIImportUtils.BASE_PATH;
	static public string OUTPUT_PATH = UIImportUtils.OUTPUT_PATH;
	static private string FILTER_REX = UIImportUtils.FILTER_REX;
	static private string C_UINAME_FORMAT = UIImportUtils.C_UINAME_FORMAT;
	static private string C_UIFILE_FORMAT = C_UINAME_FORMAT + ".cs";

	static private List<string[]> _uiinfos = new List<string[]>();

	static private bool G_NEED_REFRESH = false;


	public static void OnPostprocessAllAssets(string[] importedAsset, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
	{
		if (Application.isBatchMode) return;
		_uiinfos?.Clear();
		OnImportUIList(false, importedAsset);
		OnDeleteAssets(false, deletedAssets);
		DoRefresh(true);
	}

	static void OnImportUIList(bool refresh, params string[] assets)
	{
		_uiinfos?.Clear();
		if (assets != null && assets.Length > 0)
			assets.ToList().ForEach(asset => OnImportUI(asset));
		if (_uiinfos != null && _uiinfos.Count > 0)
		{
			List<string> lines = default;
			_uiinfos.ForEach(info => WriteUIReg(ref lines, info[0], info[1], info[2], info[3], write: false));
			WriteUIReg(ref lines, null, null, null, null, write: true);
			_uiinfos.Clear();
		}
		if (refresh)
			DoRefresh(refresh);
	}


	static void OnDeleteAssets(bool refresh, params string[] assets)
	{
		if (assets != null && assets.Length > 0)
		{
			var flag = false;
			_uiinfos = _uiinfos ?? new List<string[]>();
			_uiinfos.Clear();
			assets.ToList().ForEach(a =>
			{

				if (UIImportUtils.IsFileMatch(a, out var info, false))
				{
					if (Directory.Exists(info.dir))
					{
						flag = true;
						Directory.Delete(info.dir, true);
						_uiinfos.Add(info.ToArray());
					}
				}

			});
			if (flag)
			{
				G_NEED_REFRESH = true;
				var list = default(List<string>);
				_uiinfos.ForEach(info => WriteUIReg(ref list, info[0], info[1], info[2], null, true, false));
				WriteUIReg(ref list, null, null, null, null, write: true);
				_uiinfos?.Clear();
			}
		}
		if (refresh)
			DoRefresh(refresh);
	}

	static void DoRefresh(bool refresh = true)
	{
		if (refresh && G_NEED_REFRESH)
		{
			G_NEED_REFRESH = false;
			AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
		}
	}


	static void OnImportUI(string asset)
	{
		if (UIImportUtils.IsFileMatch(asset, out var info))
			GenUIModuleCode(info);

	}

	static private void GenUIModuleCode(UInfo info)
	{
		if (string.IsNullOrEmpty(info.package) || string.IsNullOrEmpty(info.ui)) return;
		InitExcute();
		var package = info.package;
		string ui = info.cls;
		var assetname = info.file;
		var assetfile = Path.Combine(BASE_PATH, package, assetname + ".cs");
		var mainfile = Path.Combine(info.dir, ui + ".Main.cs");
		var isnew = !File.Exists(mainfile);
		var dir = Path.GetDirectoryName(mainfile);
		if (!Directory.Exists(dir))
			Directory.CreateDirectory(dir);

		if (isnew)
		{
			WriteFile(ui + ".Main.cs", C_MAIN_TEMP.Replace(C_VIEW, assetname), package, ui);
			WriteFile(ui + ".Event.cs", C_EVENT_TEMP, package, ui);
			WriteFile(ui + ".Logic.cs", C_LOGIC_TEMP, package, ui);
			_uiinfos.Add(info.ToArray());
		}
		var content = GenUIContent(assetfile);
		WriteFile(ui + ".UI.cs", content, package, ui);
		G_NEED_REFRESH = G_NEED_REFRESH || isnew;
	}

	static private string GenUIContent(string assetfile)
	{
		if (File.Exists(assetfile))
		{
			var uicontent = C_UI_TEMP;
			var members = new List<string[]>();
			UIImportUtils.ReadMemberFromFile(assetfile, ref members);
			UIImportUtils.G_MEMBER_CALL_STRS.Clear();
			if (members.Count == 0 || members.FindIndex(m => m[0] == "Controller" && m[1].EndsWith("__disable")) > -1) return default;
			if (MemberHandler(members, out var init, out var uninit, out var call))
			{
				return uicontent
					.Replace(C_INIT, init.ToString())
					.Replace(C_UNINIT, uninit.ToString())
					.Replace(C_CALL, call.ToString());
			}
		}
		return default;
	}

	static private bool MemberHandler(List<string[]> members, out StringBuilder initsb, out StringBuilder uninitsb, out StringBuilder callsb)
	{
		initsb = default;
		uninitsb = default;
		callsb = default;
		if (members != null && members.Count > 0)
		{
			var s1 = new StringBuilder();
			var s2 = new StringBuilder();
			var s3 = new StringBuilder();
			members.ForEach(m =>
			{
				if (_excutes.TryGetValue("ui", out var list) && list.Count > 0)
					list.ForEach(e => e.Excute(m[0], m[1], m.Length > 2 ? m[2] : default, s1, s2, s3));
			});
			initsb = s1;
			uninitsb = s2;
			callsb = s3;
			return true;
		}
		return false;
	}

	static private void WriteFile(string fileName, string content, string package, string ui)
	{
		if (string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(content)) return;
		var path = Path.Combine(OUTPUT_PATH, package + "_" + ui, fileName);
		content = content
				.Replace(C_SPACE, C_UI_SPACE)
				.Replace(C_NAME, ui)
				.Replace(C_PACK, package)
				.Replace(C_INIT, "")
				.Replace(C_UNINIT, "")
				.Replace(C_CALL, "");
		File.WriteAllText(path, content, System.Text.Encoding.UTF8);
	}

	static private void WriteUIReg(ref List<string> list, string package, string com, string cls, string binder, bool remove = false, bool write = false)
	{
		var file = Path.Combine(OUTPUT_PATH, "UIReg.Binder.cs");
		if (!string.IsNullOrEmpty(package))
		{
			var index = -1;
			if (list == null && File.Exists(file))
				list = File.ReadAllLines(file).Where(l => !string.IsNullOrEmpty(l) && l.Contains("context.uiModule.")).ToList();
			list = list ?? new List<string>();
			if (list.Count > 0 && (index = list.FindIndex(l => l.Contains(cls))) >= 0) list.RemoveAt(index);

			if (!remove)
			{
				var line = @$"			context.uiModule.Reg(""{com}"", ""{package}"", ()=>new {cls}());";
				if (list.FindIndex(l => l.Contains(binder)) < 0) line += binder + ";";
				list.Add(line);
			}
		}
		if (write && list != null)
		{
			var str = C_REG_TEMP.Replace(C_SPACE, C_UI_SPACE).Replace(C_INIT, string.Join("\n", list));
			File.WriteAllText(file, str, Encoding.UTF8);
		}
	}

	[MenuItem("[Tools]/UI/GenUICode")]
	[MenuItem("Assets/[UI]/GenUICode", priority = -999)]
	static private void GenCodeUseSelect()
	{
		if (Selection.activeObject)
		{
			var path = AssetDatabase.GetAssetPath(Selection.activeObject);
			if (Directory.Exists(path))
				OnImportUIList(true, Directory.GetFiles(path, "*UI.cs"));
		}
	}

}
