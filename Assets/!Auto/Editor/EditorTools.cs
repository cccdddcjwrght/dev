
namespace ZEditors
{
	using System.Collections.Generic;
	using UnityEditor;
	using System.Linq;

	public static partial class EditorTools
	{

		public const string C_EDITOR_BASE_STR = "Tools/";


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

