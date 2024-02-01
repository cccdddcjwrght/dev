using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FairyGUI;
using Unity.Entities;
using UnityEngine;

namespace SGame
{
	public partial class LocalSystem : SystemBase
	{
		private const string ROOT_PATH = "Assets/BuildAsset/";

		private string m_lang;

		protected override void OnCreate()
		{
			base.OnCreate();
			EventManager.Instance.Reg<string>(((int)GameEvent.LANGUAGE_CHANGE), ChangeLanguage);
			//当ui对象创建时，进行本地化处理
			UIPackage.onObjectCreate += OnUICreate;
		}

		void OnUICreate(GObject obj)
		{
			UIListener.LocalAllChild(obj, true);
		}


		protected override void OnUpdate()
		{
		}


		// 更改语言
		public void ChangeLanguage(string lang)
		{
			if (ConfigSystem.Instance.TryGet(lang, out GameConfigs.languageRowData config) == false)
				return;
			m_lang = lang;
			// 多语言文字
			string lang_file = config.LanguageText;
			// 分支名字
			string lang_branch = config.BranchName;
			// 多语言默认字体
			string lang_font = config.DefaultFont;
			/*// 多语言文字
			var lang_req = libx.Assets.LoadAsset(ROOT_PATH + lang_file, typeof(TextAsset));
			var fileContent = (lang_req.asset as TextAsset)?.text;
			lang_req.Release();
			if (!string.IsNullOrEmpty(fileContent))
			{
				// 设置XML多语言
				FairyGUI.Utils.XML xml = new FairyGUI.Utils.XML(fileContent);
				UIPackage.SetStringsSource(xml);
			}*/
			// 设置包的分支
			UIPackage.branch = lang_branch;
			// 设置默认字体
			UIConfig.defaultFont = lang_font;

		}

	}
}
