using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using FairyGUI;
using GameConfigs;
using SGame;
using UnityEngine;

public class UIListener
{
	public enum EventType
	{
		Click,
		RightClick,
		ClickItem,
		TouchBegin,
		TouchMove,
		TouchEnd,
		Add,
		Remove,
		DragStart,
		DragMove,
		DragEnd,
		ItemRender,
		PlayEnd,
		Change,
		Submit,
	}

	static string C_REGEX_PATTERN = "%(.*)%";

	static private List<string> _icon_Pkg = new List<string>() {
		"Icon",
		"IconEquip",
		"Common",
		"IconTech",
		"IconLevel",

	};

	#region AutoLocal

	static Dictionary<string, string[]> _fonts = new Dictionary<string, string[]>();

	private static string GetLanguage()
	{
		return LanguageUtil.GetGameLanguage();
	}

	static public void LocalFont(GObject parent)
	{
		if (parent != null)
		{
			var field = parent.asTextField;
			if (field == null)
				return;
			var lan = GetLanguage();
			if (!_fonts.TryGetValue(lan, out var font))
			{
				font = new string[0];
				if (ConfigSystem.Instance.TryGet<languageRowData>(lan, out var cfg))
					font = new string[] { cfg.DefaultFont, cfg.OtherFont };
				_fonts[lan] = font;
			}

			if (font.Length == 0) return;

			var format = field.textFormat;
			var ofont = format.font;
			var dfont = font[0];

			if (!string.IsNullOrEmpty(ofont) && ofont != dfont)
			{
				ofont = ofont.ToLower();
				if (ofont.EndsWith("sdf"))
					ofont = dfont;
				else
					ofont = font[1];
				format.font = ofont;
				field.textFormat = format;
			}
		}
	}

	static public void LocalAllChild(GObject gObject, bool b_localfont)
	{
		if (gObject != null)
		{
			var parent = gObject as GComponent;
			if (parent != null && parent.GetController("__disablelocal") != null) return;
			SetText(gObject, null);
			if (b_localfont) LocalFont(gObject);
			if (parent != null && parent.numChildren > 0)
			{
				var childs = parent.GetChildren();
				foreach (var item in childs)
					LocalAllChild(item, b_localfont);
			}
		}
	}
	#endregion

	static public void RegPkg(params string[] pkg)
	{
		if (pkg?.Length > 0)
		{
			for (int i = 0; i < pkg.Length; i++)
			{
				if (_icon_Pkg.Contains(pkg[i])) continue;
				_icon_Pkg.Add(pkg[i]);
			}
		}
	}

	static public string GetUIRes(string pkg, string res, string defpkg = null)
	{
		var url = !string.IsNullOrEmpty(pkg) ? UIPackage.GetItemURL(pkg, res) : UIPackage.NormalizeURL(res);
		if (string.IsNullOrEmpty(url) && defpkg != null)
			url = UIPackage.GetItemURL(defpkg, res);
		return url;
	}

	static public string GetUIResFromPkgs(string res)
	{
		if (string.IsNullOrEmpty(res) || _icon_Pkg.Count == 0) return default;
		for (int i = 0; i < _icon_Pkg.Count; i++)
		{
			var url = GetUIRes(_icon_Pkg[i], res);
			if (url != null) return url;
		}
		return default;
	}

	static public string MatchReplace(string key)
	{
		return key;
	}

	static public void DoTriggerEvent(GObject gObject, int id)
	{
		Debug.Log(id.ToString());
	}

	static public T Get<T>(GObject gObject) where T : GObject
	{
		if (gObject != null)
		{
			if (gObject is T t) return t;
			var name = "__" + typeof(T).Name.ToLower().Substring(1);
			var com = gObject.asCom;
			if (com != null)
			{
				var ret = com.GetChild(name) as T;
				if (ret != null) return ret;
				if (name == "__button")
					return com.GetChild("__btn") as T;
				if (name == "__textinput")
					return com.GetChild("__input") as T;
			}
		}
		return default;
	}

	static public string RegexReplace(string context, string pattern, Func<string, string> match)
	{
		if (context == null || pattern == null || match == null) return context;
		return Regex.Replace(context, pattern, (m) => match(m.Groups[1].Value), RegexOptions.IgnoreCase);
	}

	/// <summary>
	/// 获取本地化文本
	/// </summary>
	/// <param name="str"></param>
	/// <param name="def"></param>
	/// <returns></returns>
	public static string Local(string str, string def)
	{
		if (!string.IsNullOrEmpty(str))
		{
			str = str.Trim();
			LanagueSystem.Instance.TryGetValue(str, out var ret);
			return ret ?? def ?? str;
		}
		return str;
	}

	/// <summary>
	/// 获取本地化文本
	/// </summary>
	/// <param name="txt"></param>
	/// <returns></returns>
	static public string Local(string txt)
	{
		return Local(txt, null);
	}

	/// <summary>
	/// 本地化并格式化
	/// </summary>
	/// <param name="format"></param>
	/// <param name="args"></param>
	/// <returns></returns>
	static public string LocalFormat(string format, params object[] args)
	{
		return args?.Length > 0 ? string.Format(Local(format), args) : Local(format);
	}

	/// <summary>
	/// 根据前缀自动本地化
	/// </summary>
	/// <param name="txt"></param>
	/// <returns></returns>
	static public string AutoLocal(string txt)
	{
		if (string.IsNullOrEmpty(txt)) return default;
		if (txt[0] == '@')
			return RegexReplace(Local(txt.Substring(1)), C_REGEX_PATTERN, MatchReplace);
		return txt;
	}

	static public void SetTextByKey(GObject gObject, string txt, params object[] args)
	{
		if (args?.Length > 0)
			SetText(gObject, LocalFormat(txt, args), false);
		else
			SetText(gObject, Local(txt), false);
	}

	static public void SetText(GObject gObject, string txt, bool local = true)
	{
		if (gObject != null)
		{
			txt = local ? AutoLocal(string.IsNullOrEmpty(txt) ? gObject.text : txt) : txt;
			var com = gObject.asCom;
			if (com != null && com.GetChild("title") == null)
				com = com.GetChild("body")?.asCom ?? com;
			if (com != null)
			{
				if (gObject != com)
					txt = local ? AutoLocal(string.IsNullOrEmpty(txt) ? com.text : txt) : txt;
				com.text = txt;
				var shadow = com.GetChild("shadow");
				var outline = com.GetChild("outline");
				var o1 = com.GetChild("__text");
				var o2 = com.GetChild("__title");
				var o3 = com.GetChild("iconTitle");

				if (shadow != null) shadow.text = txt;
				if (outline != null) outline.text = txt;
				if (o1 != null) o1.text = txt;
				if (o2 != null) o2.text = txt;
				if (o3 != null) o3.text = txt;
			}
			else
				gObject.text = txt;

		}
	}

	static public string GetText(GObject gObject, string def = default)
	{
		if (gObject != null)
		{
			var txt = gObject.text;
			if (gObject is GLabel || gObject is GButton || gObject is GTextField || gObject is GTextInput)
				txt = gObject.text;
			else if (gObject.asCom != null)
			{
				var com = gObject.asCom;
				var t = com.GetChild("title") ?? com.GetChild("__title") ?? com.GetChild("__text");
				if (t != null) txt = t.text;
			}
			return txt ?? def;
		}
		return def;
	}

	static public void SetValue(GObject gObject, float val)
	{
		if (gObject != null)
		{
			if (gObject is GProgressBar p) p.value = val;
			else if (gObject is GSlider s) s.value = val;
			else if (gObject is GComponent c)
			{
				p = c.GetChild("__progress").asProgress;
				s = c.GetChild("__slider").asSlider;
				if (p != null) { p.value = val; }
				else if (s != null) { s.value = val; }
			}
		}
	}

	static public float GetValue(GObject gObject, float def = 0)
	{

		if (gObject != null)
		{
			var v = 0.0;
			if (gObject is GProgressBar p) v = p.value;
			else if (gObject is GSlider s) v = s.value;
			else if (gObject is GComponent c)
			{
				p = c.GetChild("__progress").asProgress;
				s = c.GetChild("__slider").asSlider;
				if (p != null) { v = p.value; }
				else if (s != null) { v = s.value; }
			}
			return (float)v;
		}
		return def;

	}

	static public void SetIcon(GObject gObject, string icon, string pkg = null)
	{
		if (gObject != null)
		{
			const string def_pkg = "Icon";
			var url = GetUIRes(pkg ?? gObject.packageItem?.owner?.name, icon, def_pkg) ?? GetUIResFromPkgs(icon);
			if (string.IsNullOrEmpty(url)) return;
			gObject.icon = url;
			var com = gObject.asCom;
			if (com != null)
			{
				var o = com.GetChild("__icon") ?? com.GetChild("icon") ?? com.GetChild("body");
				if (o != null) o.icon = url;
			}
		}
	}

	static public void SetIconIndex(GObject gObject, int index)
	{
		if (gObject != null && gObject is GComponent com)
		{
			var state = com.GetController("price")
			?? com.GetController("__price")
			?? com.GetController("icon")
			?? com.GetController("iconImage");

			if (state != null && state.pageCount > index)
				state.selectedIndex = index;
		}
	}

	static public void SetIconName(GObject gObject, string name)
	{
		if (gObject != null && gObject is GComponent com)
		{
			var state = com.GetController("price")
			?? com.GetController("__price")
			?? com.GetController("icon")
			?? com.GetController("iconImage");

			if (state != null && state.HasPage(name))
				state.selectedPage = name;
		}
	}

	static public void SetControllerSelect(GObject gObject, string ctr, int index, bool throwex = true)
	{
		if (!string.IsNullOrEmpty(ctr) && gObject != null && gObject is GComponent com)
		{
			var c = com.GetController(ctr);
			if (c != null && c.pageCount > index)
				c.selectedIndex = index;
			else if (throwex)
				throw new Exception($"{gObject} 不存在 {ctr} 控制器或者{index}越界");
		}
	}

	static public void SetControllerName(GObject gObject, string ctr, string name, bool throwex = true)
	{
		if (!string.IsNullOrEmpty(ctr) && gObject != null && gObject is GComponent com)
		{
			var c = com.GetController(ctr);
			if (c != null)
				c.selectedPage = name;
			else if (throwex)
				throw new Exception($"{gObject} 不存在 {ctr} 控制器");
		}
	}


	static public void SetTextWithName(GObject gObject, string labelName, string text, bool local = true)
	{
		if (!string.IsNullOrEmpty(labelName) && gObject is GComponent com)
		{
			var c = com.GetChildByPath(labelName);
			if (c != null) SetText(c, text, local);
		}
	}

	static public void SetIconWithName(GObject gObject, string iconName, string res)
	{
		if (!string.IsNullOrEmpty(iconName) && gObject is GComponent com)
		{
			var c = com.GetChildByPath(iconName);
			if (c != null) SetIcon(c, res);
		}
	}

	static public GObject Listener(GObject gObject, object method, EventType eventType = EventType.Click, bool remove = false)
	{
		var cache = gObject;
		if (gObject != null && method != null)
		{

			var call = method as EventCallback1;
			var itemRenderer = method as ListItemRenderer;
			var com = gObject.asCom;
			if (com != null)
			{
				gObject = com.GetChild("__body") ?? gObject;
				com = gObject.asCom;
			}
			var listItem = com?.GetChild("__list") ?? gObject;
			var inputItem = com?.GetChild("__textinput") ?? gObject;
			var sliderItem = com?.GetChild("__slider") ?? gObject;
			var comboItem = com?.GetChild("__combo") ?? gObject;
			var clickItem = com?.GetChild("__btn") ?? gObject.asCom?.GetChild("click");

			if (clickItem == null)
			{
				if (com != null)
				{
					var g = com?.GetChildByPath("icon.__btn")
						?? com?.GetChildByPath("icon.close");
					if (g != null)
						clickItem = com?.GetChild("icon");
					else
						clickItem = gObject;
				}
				else clickItem = gObject;
			}



			EventListener eventListener = default;
			switch (eventType)
			{
				case EventType.RightClick:
					eventListener = clickItem.onRightClick;
					break;
				case EventType.TouchBegin:
					eventListener = gObject.onTouchBegin;
					break;
				case EventType.TouchMove:
					eventListener = gObject.onTouchMove;
					break;
				case EventType.TouchEnd:
					eventListener = gObject.onTouchEnd;
					break;
				case EventType.DragStart:
					eventListener = gObject.onDragStart;
					break;
				case EventType.DragMove:
					eventListener = gObject.onDragMove;
					break;
				case EventType.DragEnd:
					eventListener = gObject.onDragEnd;
					break;
				case EventType.Add:
					eventListener = gObject.onAddedToStage;
					break;
				case EventType.Remove:
					eventListener = gObject.onRemovedFromStage;
					break;
				case EventType.PlayEnd:
					if (gObject is GMovieClip m) eventListener = m.onPlayEnd;
					break;
				case EventType.ClickItem:
					eventListener = listItem.asList?.onClickItem;
					break;
				case EventType.Submit:
					eventListener = inputItem.asTextInput?.onSubmit;
					break;
				case EventType.Change:
					eventListener = inputItem.asTextInput?.onChanged
						?? comboItem.asComboBox?.onChanged
						?? sliderItem.asSlider?.onChanged
						?? gObject.asButton?.onChanged;
					break;
				case EventType.ItemRender:
					if (listItem is GList list)
					{
						list.itemRenderer -= itemRenderer;
						list.itemRenderer += itemRenderer;
					}
					break;
				case EventType.Click:
				default:
					eventListener = clickItem.onClick;
					break;
			}
			if (call != null && eventListener != null)
			{
				eventListener.Remove(call);
				if (!remove)
					eventListener.Add(call);
			}
		}
		return cache;
	}

	static public void ListenerIcon(GObject gObject, object method, bool remove = false)
	{
		if (gObject != null && gObject is GComponent com)
		{
			var icon = com.GetChild("icon");
			if (icon != null && icon is GButton btn)
				Listener(btn, method, remove: remove);
		}
	}

	static public void ListenerClose(GObject gObject, object method, bool remove = false)
	{
		if (gObject != null)
		{
			if (gObject.name == "close" || gObject.name == "mask")
				Listener(gObject, method, remove: remove);
			else if (gObject is GComponent com)
			{
				var icon = com.GetChild("close")
					?? com.GetChild("Close")
					?? com.GetChild("Mask")
					?? com.GetChild("mask");
				if (icon != null && icon is GButton btn)
					Listener(btn, method, remove: remove);
				var mask = com.GetChild("closeBg");
				if (mask != null)
					Listener(mask, method, remove: remove);
				if (icon == null && mask == null && com.GetChild("body") != null)
					ListenerClose(com.GetChild("body"), method, remove);
			}
		}
	}



}

public static partial class UIListenerExt
{
	public static GObject SetText(this GObject gObject, string text, bool local = true)
	{
		UIListener.SetText(gObject, text, local);
		return gObject;
	}

	public static GObject SetTextByKey(this GObject gObject, string text, params object[] args)
	{
		UIListener.SetTextByKey(gObject, text, args);
		return gObject;
	}

	public static GObject SetIcon(this GObject gObject, string icon, string pkg = null)
	{

		UIListener.SetIcon(gObject, icon, pkg);
		return gObject;
	}

	public static GObject SetIconIndex(this GObject gObject, int icon)
	{

		UIListener.SetIconIndex(gObject, icon);
		return gObject;
	}

	public static string Local(this string txt, string pix = null, params object[] args)
	{
		if (string.IsNullOrEmpty(txt)) return txt;
		return UIListener.LocalFormat(pix + txt, args);
	}

	public static string AutoLocal(this string txt, params object[] args)
	{
		if (string.IsNullOrEmpty(txt)) return txt;
		txt = UIListener.AutoLocal(txt);
		if (args?.Length > 0)
			txt = string.Format(txt, args);
		return txt;
	}

	public static void SetBaseItem(this GObject gObject, params int[] args)
	{
		if (gObject != null && args?.Length > 1)
		{
			if (args.Length == 2)
			{
				gObject.SetIcon(Utils.GetItemIcon(1, args[0]));
				gObject.SetText(Utils.ConvertNumberStr(args[1]));
			}
			else
			{
				gObject.SetIcon(Utils.GetItemIcon(args[0], args[1]));
				gObject.SetText(Utils.ConvertNumberStr(args[2]));
			}
		}
	}

	public static void SetItem(this GObject gObject, params double[] args)
	{
		if (gObject != null && args?.Length > 1)
		{
			if (args.Length == 2)
			{
				gObject.SetIcon(Utils.GetItemIcon(1, (int)args[0]));
				gObject.SetText(Utils.ConvertNumberStr(args[1]));
			}
			else
			{
				gObject.SetIcon(Utils.GetItemIcon((int)args[0], (int)args[1]));
				gObject.SetText(Utils.ConvertNumberStr(args[2]));
			}
		}
	}
}