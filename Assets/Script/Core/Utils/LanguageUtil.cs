using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEngine;

namespace SGame
{
	public static class LanguageUtil
	{
		private static List<string> c_lan_list;

		private static readonly string c_lan_file = Application.persistentDataPath + "/lan.bytes";

		public static IEnumerator InitLanguage()
		{
			var req = libx.Assets.LoadAsset("Assets/BuildAsset/Log/lan.bytes", typeof(TextAsset));
			yield return req;
			if (string.IsNullOrEmpty(req.error))
			{
				c_lan_list = req.text?.Split('|', System.StringSplitOptions.RemoveEmptyEntries).ToList();
				File.WriteAllText(c_lan_file, req.text);
			}
		}

		public static string GetGameLanguage(string def = default)
		{
			def = null;
			if (c_lan_list == null)
			{
				string context = default;
				if (File.Exists(c_lan_file))
					context = File.ReadAllText(c_lan_file);
				if (string.IsNullOrEmpty(context))
				{
					var txt = Resources.Load<TextAsset>("lan");
					if (txt != null)
					{
						context = txt.text;
						File.WriteAllText(c_lan_file, context);
					}
				}
				if (!string.IsNullOrEmpty(context))
					c_lan_list = context.Split('|', System.StringSplitOptions.RemoveEmptyEntries).ToList();
				else
					c_lan_list = new List<string>();
			}

			var lan = PlayerPrefs.GetString("__language");
			if (string.IsNullOrEmpty(lan))
			{
				var ms = GetLanguage();
				if (c_lan_list.Contains(ms.ToLower())) lan = ms;
				else lan = def ?? c_lan_list.FirstOrDefault();
				PlayerPrefs.SetString("__language", lan);
			}
			return lan;
		}


		//±æª˙”Ô—‘
		public static string MachineLanguage()
		{
			switch (Application.systemLanguage)
			{
				case SystemLanguage.Afrikaans:
					return "af";
				case SystemLanguage.Arabic:
					return "ar";
				case SystemLanguage.Basque:
					return "eu";
				case SystemLanguage.Belarusian:
					return "be";
				case SystemLanguage.Bulgarian:
					return "bg";
				case SystemLanguage.Catalan:
					return "ca";
				case SystemLanguage.Chinese:
					return "chs";
				case SystemLanguage.Czech:
					return "cs";
				case SystemLanguage.Danish:
					return "da";
				case SystemLanguage.Dutch:
					return "nl";
				case SystemLanguage.English:
					return "en";
				case SystemLanguage.Estonian:
					return "et";
				case SystemLanguage.Faroese:
					return "fo";
				case SystemLanguage.Finnish:
					return "fu";
				case SystemLanguage.French:
					return "fr";
				case SystemLanguage.German:
					return "de";
				case SystemLanguage.Greek:
					return "el";
				case SystemLanguage.Hebrew:
					return "he";
				case SystemLanguage.Icelandic:
					return "is";
				case SystemLanguage.Indonesian:
					return "id";
				case SystemLanguage.Italian:
					return "it";
				case SystemLanguage.Japanese:
					return "ja";
				case SystemLanguage.Korean:
					return "ko";
				case SystemLanguage.Latvian:
					return "lv";
				case SystemLanguage.Lithuanian:
					return "lt";
				case SystemLanguage.Norwegian:
					return "nn";
				case SystemLanguage.Polish:
					return "pl";
				case SystemLanguage.Portuguese:
					return "pt";
				case SystemLanguage.Romanian:
					return "ro";
				case SystemLanguage.Russian:
					return "ru";
				case SystemLanguage.SerboCroatian:
					return "sr";
				case SystemLanguage.Slovak:
					return "sk";
				case SystemLanguage.Slovenian:
					return "sl";
				case SystemLanguage.Spanish:
					return "es";
				case SystemLanguage.Swedish:
					return "sv";
				case SystemLanguage.Thai:
					return "th";
				case SystemLanguage.Turkish:
					return "tr";
				case SystemLanguage.Ukrainian:
					return "uk";
				case SystemLanguage.Vietnamese:
					return "vi";
				case SystemLanguage.ChineseSimplified:
					return "chs";
				case SystemLanguage.ChineseTraditional:
					return "zh";
				case SystemLanguage.Hungarian:
					return "hu";
				case SystemLanguage.Unknown:
					return "unknown";

			}
			;
			return default;
		}

		public static string GetLanguage()
		{
			var info = CultureInfo.CurrentCulture;
			if (info != null)
			{
				var lan = info.ToString().ToLower();
				if (lan.StartsWith("zh"))
				{
					switch (lan)
					{
						case "zh":
						case "zh-cn":
						case "zh-sg":
							return "chs";
						case "zh-tw":
						case "zh-hk":
						case "zh-mo":
						case "zh-hant":
							return "zh";
					}
				}
				return lan.Split('-')[0];
			}
			return MachineLanguage();
		}


	}
}
