using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace SGame
{
	public static class LanguageUtil
	{


		public static string GetGameLanguage(string def = default)
		{
			var lan = PlayerPrefs.GetString("__language", def ?? GetLanguage());
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
