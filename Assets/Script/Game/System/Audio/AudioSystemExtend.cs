using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;

namespace SGame
{
	partial class AudioSystem
	{
		private int _normal_id = 2;

		/// <summary>
		/// 关闭按钮名列表
		/// </summary>
		private List<string> _closeBtnNames = new List<string>()
		{
			"close",
			"btnclose",
			"return",
			"btnreturn",
			"__close",
			"__return"
		};

		/// <summary>
		/// 绑定特定按钮名音效
		/// </summary>
		private Dictionary<string, int> _btnBindAudioID = new Dictionary<string, int>()
		{
			//{"close",0 }
		};

		partial void OnInit()
		{
			GRoot.inst.onClick.Add(OnUIClick);
		}


		void OnUIClick(EventContext context)
		{
			var btn = GetParentBtn((context.initiator as Container)?.gOwner);
			if (btn == null) return;
			var name = btn.name.ToLower();
			_btnBindAudioID.TryGetValue(name, out var id);
			if(id != 0)
				this.Play(id);
			else if (isCloseBtn(name) && _btnBindAudioID.TryGetValue("close", out id))
				this.Play(id);
			else
				this.Play(_normal_id);
		}

		GButton GetParentBtn(GObject gObject, int layer = 5)
		{
			for (int i = 0; i < layer; i++)
			{
				if (gObject == null) return null;
				var btn = gObject.asButton;
				if (btn != null) return btn;
				gObject = gObject.parent;
			}
			return default;
		}

		bool isCloseBtn(string name)
		{
			return _closeBtnNames.Contains(name);
		}

	}
}
