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
			//��ui���󴴽�ʱ�����б��ػ�����
			UIPackage.onObjectCreate += OnUICreate;
		}

		void OnUICreate(GObject obj)
		{
			UIListener.LocalAllChild(obj, true);
		}


		protected override void OnUpdate()
		{
		}


		// ��������
		public void ChangeLanguage(string lang)
		{
			if (ConfigSystem.Instance.TryGet(lang, out GameConfigs.languageRowData config) == false)
				return;
			m_lang = lang;
			// ����������
			string lang_file = config.LanguageText;
			// ��֧����
			string lang_branch = config.BranchName;
			// ������Ĭ������
			string lang_font = config.DefaultFont;
			/*// ����������
			var lang_req = libx.Assets.LoadAsset(ROOT_PATH + lang_file, typeof(TextAsset));
			var fileContent = (lang_req.asset as TextAsset)?.text;
			lang_req.Release();
			if (!string.IsNullOrEmpty(fileContent))
			{
				// ����XML������
				FairyGUI.Utils.XML xml = new FairyGUI.Utils.XML(fileContent);
				UIPackage.SetStringsSource(xml);
			}*/
			// ���ð��ķ�֧
			UIPackage.branch = lang_branch;
			// ����Ĭ������
			UIConfig.defaultFont = lang_font;

		}

	}
}
