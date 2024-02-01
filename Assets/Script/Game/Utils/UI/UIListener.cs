using System;
using System.Collections;
using System.Collections.Generic;
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

	#region AutoLocal

	static Dictionary<string, string[]> _fonts = new Dictionary<string, string[]>();

	private static string GetLanguage()
	{
		return "en";
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
		return string.Format(Local(format), args);
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
			gObject.text = txt;
			var com = gObject.asCom;
			if (com != null)
			{
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
			if (!string.IsNullOrEmpty(pkg))
				icon = UIPackage.GetItemURL(pkg, icon);
			gObject.icon = icon;
			var com = gObject.asCom;
			if (com != null)
			{
				var o = com.GetChild("__icon");
				if (o != null) o.icon = icon;
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

	static public void SetControllerSelect(GObject gObject, string ctr, int index)
	{
		if (!string.IsNullOrEmpty(ctr) && gObject != null && gObject is GComponent com)
		{
			var c = com.GetController(ctr);
			if (c != null && c.pageCount > index)
				c.selectedIndex = index;
			else
				throw new Exception($"{gObject} 不存在 {ctr} 控制器或者{index}越界");
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
				gObject = com.GetChild("__body") ?? gObject;

			var listItem = gObject.asCom?.GetChild("__list") ?? gObject;
			var inputItem = gObject.asCom?.GetChild("__textinput") ?? gObject;
			var sliderItem = gObject.asCom?.GetChild("__slider") ?? gObject;
			var comboItem = gObject.asCom?.GetChild("__combo") ?? gObject;
			var clickItem = gObject.asCom?.GetChild("__btn") ?? gObject.asCom?.GetChild("click");

			if (clickItem == null)
			{
				var g = gObject.asCom?.GetChildByPath("icon.__btn")
					?? gObject.asCom?.GetChildByPath("icon.close");
				if (g != null)
					clickItem = gObject.asCom?.GetChild("icon");
				else
					clickItem = gObject;
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
		if (gObject != null && gObject is GComponent com)
		{
			var icon = com.GetChild("close")
				?? com.GetChild("Close")
				?? com.GetChild("Mask")
				?? com.GetChild("mask");
			if (icon != null && icon is GButton btn)
				Listener(btn, method, remove: remove);
		}
	}

}

public static class UIListenerExt
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

}