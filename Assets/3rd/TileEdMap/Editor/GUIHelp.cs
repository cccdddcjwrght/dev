#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using System.IO;

public delegate bool OnCheckPath(ref string path);

[Serializable]
public static partial class GUIHelp
{


	#region Member
	public static readonly string keyString = "ZCF-GroupIDSelect";

	public static readonly GUIStyle G_NULL = new GUIStyle()
	{

	};

	public static readonly GUIStyle G_baseStyle = new GUIStyle()
	{
		fontSize = 18,
		fontStyle = FontStyle.Normal,
		alignment = TextAnchor.MiddleCenter,
		normal = new GUIStyleState()
		{
			textColor = Color.white,
			background = CreateCheckerTex(Color.gray, Color.gray),
		},
	};

	public static readonly GUIStyle G_baseBg = new GUIStyle(G_baseStyle)
	{
		normal = new GUIStyleState()
		{
			background = CreateCheckerTex(Color.white * 0.5f, Color.white * 0.5f),
		}
	};

	public static readonly GUIStyle G_toggleStyle = new GUIStyle(G_baseStyle)
	{
		fontSize = 18,
		fontStyle = FontStyle.Normal,
		alignment = TextAnchor.MiddleCenter,
	};

	public static readonly GUIStyle G_BaseButtonStyle = new GUIStyle(G_baseStyle)
	{
		fixedHeight = 0,
		fixedWidth = 0,
		padding = new RectOffset(2, 2, 0, 0),
		margin = new RectOffset(2, 2, 2, 1),
		normal = new GUIStyleState()
		{
			textColor = Color.white,
			background = CreateCheckerTex(Color.gray, Color.gray),
		},
		active = new GUIStyleState()
		{
			textColor = Color.gray,
			background = CreateCheckerTex(new Color(0.3f, 0.3f, 0.3f, 1f), new Color(0.3f, 0.3f, 0.3f, 1f)),
		},

	};

	public static readonly GUIStyle G_NoBgButtonStyle = new GUIStyle(GUI.skin.button)
	{
		fixedHeight = 0,
		fixedWidth = 0,
		padding = new RectOffset(2, 2, 0, 0),
		margin = new RectOffset(2, 2, 2, 1),
	};

	public static readonly GUIStyle G_ButtonStyle = new GUIStyle(G_BaseButtonStyle)
	{
		fontSize = 16,
		fixedHeight = 22,
	};

	public static readonly GUIStyle G_AutoButtonStyle = new GUIStyle(G_ButtonStyle)
	{
		fixedHeight = 64,
	};

	public static readonly GUIStyle G_Head = new GUIStyle(G_ButtonStyle)
	{
		fontSize = 20,
		fixedHeight = 24,
		fontStyle = FontStyle.Normal,
		alignment = TextAnchor.MiddleCenter,
		padding = new RectOffset(2, 2, 0, 0),
		margin = new RectOffset(2, 2, 2, 1),
		normal = new GUIStyleState()
		{
			textColor = Color.white,
			background = CreateCheckerTex(Color.grey, Color.gray),
		},
	};

	public static readonly GUIStyle G_Line_Style = new GUIStyle(G_ButtonStyle)
	{
		fontSize = 22,
		fixedHeight = 0,
		fixedWidth = 0,
		padding = new RectOffset(0, 0, 0, 0),
		margin = new RectOffset(0, 0, 0, 0),
		normal = new GUIStyleState()
		{
			textColor = Color.green,
		},
		active = new GUIStyleState()
		{
			textColor = Color.gray,
		},
		focused = new GUIStyleState()
		{
			textColor = Color.gray
		}
	};

	public static readonly GUIStyle G_labelStyle_ml = new GUIStyle(G_baseStyle)
	{
		alignment = TextAnchor.MiddleLeft,
	};

	public static readonly GUIStyle G_labelStyle_mr = new GUIStyle(G_baseStyle)
	{
		alignment = TextAnchor.MiddleRight,
	};

	public static readonly GUIStyle G_labelStyle_mc = new GUIStyle(G_baseStyle)
	{
		alignment = TextAnchor.MiddleCenter,
	};

	public static readonly GUIStyle G_labelStyle_ll = new GUIStyle(G_baseStyle)
	{
		alignment = TextAnchor.LowerLeft,
	};

	public static readonly GUIStyle G_popupStyle = new GUIStyle()
	{
		fontStyle = FontStyle.Normal,
		fontSize = 16,
		alignment = TextAnchor.MiddleCenter,
		normal = new GUIStyleState()
		{
			textColor = Color.white,
			background = CreateCheckerTex(Color.gray, Color.gray),
		}
	};

	public static readonly GUIStyle G_normal1 = new GUIStyle()
	{

		fontSize = 18,
		fontStyle = FontStyle.Normal,
		alignment = TextAnchor.MiddleCenter,
		normal = new GUIStyleState()
		{
			textColor = Color.white,
			background = cyanTexture,
		},
	};

	public static readonly GUIStyle G_normal2 = new GUIStyle()
	{

		fontSize = 18,
		fontStyle = FontStyle.Normal,
		alignment = TextAnchor.MiddleCenter,
		normal = new GUIStyleState()
		{
			textColor = Color.white,
			background = greyTexture,
		},
	};

	public static readonly GUIStyle G_hover = new GUIStyle()
	{
		fontSize = 18,
		fontStyle = FontStyle.Normal,
		alignment = TextAnchor.MiddleCenter,
		normal = new GUIStyleState()
		{
			textColor = Color.yellow,
			background = greenTexture,
		},
	};


	public static readonly GUIStyle G_LabelTitle = new GUIStyle(GUI.skin.label)
	{
		alignment = TextAnchor.MiddleLeft,
		richText = true,
	};

	public static readonly GUIStyle G_TabLabelTitle = new GUIStyle(GUI.skin.label)
	{
		alignment = TextAnchor.MiddleLeft,
		margin = new RectOffset(2, 2, 2, 2),
		richText = true,
	};

	public static readonly GUIStyle G_TextField = new GUIStyle(GUI.skin.textField)
	{
		alignment = TextAnchor.MiddleLeft,
		fixedHeight = 23,
		richText = true,
		margin = new RectOffset(2, 2, 2, 2)
	};

	public static readonly GUIStyle G_switch_false = new GUIStyle()
	{
		fontSize = 18,
		fixedHeight = 21,
		fontStyle = FontStyle.Normal,
		alignment = TextAnchor.MiddleCenter,
		fixedWidth = 60,
		normal = new GUIStyleState()
		{
			background = ToggleTexFalse,
		},
		margin = new RectOffset(2, 2, 2, 1),

	};

	public static readonly GUIStyle G_switch_true = new GUIStyle()
	{
		fontSize = 18,
		fixedHeight = 21,
		fontStyle = FontStyle.Normal,
		alignment = TextAnchor.MiddleCenter,
		fixedWidth = 60,
		normal = new GUIStyleState()
		{
			background = ToggleTexTrue,
		},
		margin = new RectOffset(2, 2, 2, 1),

	};

	public static readonly GUIStyle G_Mask_Style = new GUIStyle()
	{
		fontSize = 19,
		fontStyle = FontStyle.Normal,
		alignment = TextAnchor.MiddleCenter,
		normal = new GUIStyleState()
		{
			background = CreateNormalTex(new Color(1f, 1f, 1f, 0.01f))
		},
	};

	public static GUIStyle G_labTitleusebg = new GUIStyle(GUIHelp.G_LabelTitle)
	{
		fixedHeight = 22,
		margin = new RectOffset(2, 2, 3, 2),
		normal = new GUIStyleState()
		{
			background = whiteTexture
		}
	};

	[SerializeField]
	static Texture2D mBlankTexture;
	[SerializeField]
	static Texture2D mBackdropTex;
	[SerializeField]
	static Texture2D mContrastTex;
	[SerializeField]
	static Texture2D mGradientTex;
	[SerializeField]
	static GameObject mPrevious;
	[SerializeField]
	static Texture2D mMaskTex;
	[SerializeField]
	static Texture2D mWhiteTex;
	[SerializeField]
	static Texture2D mCyanTex;
	[SerializeField]
	static Texture2D mGreenTex;
	[SerializeField]
	static Texture2D mGreyTex;
	[SerializeField]
	static Texture2D mHalfATex;
	[SerializeField]
	static Texture2D mToggleTexFalse;
	[SerializeField]
	static Texture2D mToggleTexTrue;

	static public Texture2D blankTexture
	{
		get
		{
			if (mBlankTexture == null)
			{
				mBlankTexture = CreateNormalTex(Color.gray);
			}
			return mBlankTexture;
		}
	}
	static public Texture2D whiteTexture
	{
		get
		{
			if (mWhiteTex == null)
			{
				mWhiteTex = CreateNormalTex(Color.white);
			}
			return mWhiteTex;
		}
	}
	static public Texture2D cyanTexture
	{
		get
		{
			if (mCyanTex == null)
			{
				mCyanTex = CreateNormalTex(Color.cyan);
			}
			return mCyanTex;
		}
	}
	static public Texture2D greenTexture
	{
		get
		{
			if (mGreenTex == null)
			{
				mGreenTex = CreateNormalTex(Color.green);
			}
			return mGreenTex;
		}
	}
	static public Texture2D greyTexture
	{
		get
		{
			if (mGreyTex == null)
			{
				mGreyTex = CreateNormalTex(Color.grey);
			}
			return mGreyTex;
		}
	}

	static public Texture2D halfATexture
	{
		get
		{
			if (mHalfATex == null)
				mHalfATex = CreateNormalTex(new Color(0.6f, 0.6f, 0.6f, 1));
			return mHalfATex;
		}
	}
	static public Texture2D MaskTex
	{
		get
		{
			if (mMaskTex == null)
			{
				mMaskTex = CreateNormalTex(new Color(1f, 1f, 1f, 0.5f));
			}
			return mMaskTex;
		}
	}
	static public Texture2D ToggleTexFalse
	{
		get
		{
			if (mToggleTexFalse == null)
			{
				mToggleTexFalse = CreateToggleTex(Color.red, Color.gray);
			}
			return mToggleTexFalse;
		}
	}
	static public Texture2D ToggleTexTrue
	{
		get
		{
			if (mToggleTexTrue == null)
			{
				mToggleTexTrue = CreateToggleTex(Color.gray, Color.green);
			}
			return mToggleTexTrue;
		}
	}
	static public Texture2D backdropTexture
	{
		get
		{
			if (mBackdropTex == null) mBackdropTex = CreateCheckerTex(
				new Color(0.1f, 0.1f, 0.1f, 0.5f),
				new Color(0.2f, 0.2f, 0.2f, 0.5f));
			return mBackdropTex;
		}
	}
	static public Texture2D contrastTexture
	{
		get
		{
			if (mContrastTex == null) mContrastTex = CreateCheckerTex(
				new Color(0f, 0.0f, 0f, 0.5f),
				new Color(1f, 1f, 1f, 0.5f));
			return mContrastTex;
		}
	}
	static public Texture2D gradientTexture
	{
		get
		{
			if (mGradientTex == null) mGradientTex = CreateGradientTex(Color.white, Color.black);
			return mGradientTex;
		}
	}

	static public Color G_BG_NORMAL_COLOR = Color.black;
	static public Color G_BTN_NORMAL_COLOR = Color.black;
	static public Color G_LAB_NORMAL_COLOR = Color.white;
	static public Color G_File_PATH_Color = new Color(0.8f, 0.8f, 0.8f, 1);
	static public float normalSize = 20;

	public static GUIStyle G_BStyle;
	public static int G_BFSize;

	#endregion

	#region Drawbase

	delegate void FlowCall(Action draw, GUIStyle style = null, params GUILayoutOption[] ops);

	static FlowCall G_DH = new FlowCall(GUI_DH);
	static FlowCall G_DV = new FlowCall(GUI_DV);


	static public void GUI_DH(System.Action draw, GUIStyle style = null, params GUILayoutOption[] ops)
	{
		style = style == null ? GUIStyle.none : style;
		GUILayout.BeginHorizontal(style, ops);
		try
		{
			if (draw != null) draw();
		}
		catch (Exception e)
		{
			if (e.GetType() != typeof(ExitGUIException))
			{
				Debug.LogException(e);
			}
		}
		GUILayout.EndHorizontal();
	}

	static public void GUI_DV(System.Action draw, GUIStyle style = null, params GUILayoutOption[] ops)
	{
		style = style == null ? GUIStyle.none : style;
		GUILayout.BeginVertical(style, ops);
		try
		{
			if (draw != null) draw();
		}
		catch (Exception e)
		{
			if (e.GetType() != typeof(ExitGUIException))
			{
				Debug.LogException(e);
			}
		}
		GUILayout.EndVertical();
	}

	static public void GUI_DVSpace(System.Action draw, GUIStyle style = null, int space = 0, params GUILayoutOption[] ops)
	{
		style = style == null ? GUIStyle.none : style;
		GUILayout.BeginVertical(style, ops);
		try
		{
			if (draw != null) draw();
		}
		catch (Exception e)
		{
			if (e.GetType() != typeof(ExitGUIException))
			{
				Debug.LogException(e);
			}
		}
		GUILayout.EndVertical();
		if (space > 0) GUILayout.Space(space);
	}

	static public Vector2 GUI_DS(System.Action draw, ref Vector2 position, bool allowH = true, bool allowV = true, GUIStyle styleH = null, GUIStyle styleV = null, params GUILayoutOption[] ops)
	{
		styleH = styleH == null ? GUIStyle.none : styleH;
		styleV = styleV == null ? GUIStyle.none : styleV;
		position = GUILayout.BeginScrollView(position, allowH, allowV, styleH, styleV, ops);
		if (draw != null) draw();
		GUILayout.EndScrollView();
		return position;
	}

	static public void GUI_DrawEnable(bool enable, Action body)
	{
		GUI.enabled = enable;
		if (body != null) body();
		GUI.enabled = true;
	}

	#endregion

	static public void DrawHead(string str, int size = 20, FontStyle fontStyle = FontStyle.Bold)
	{
		DrawButton(str, null, null, null, size, fontStyle);
	}

	static public void DrawTitle(string title, System.Action content = null, string tips = null, int size = 14, int textHeight = 20, GUIStyle style = null, params GUILayoutOption[] ops)
	{
		GUI_DH(() =>
		{
			if (!string.IsNullOrEmpty(title))
			{
				tips = string.IsNullOrEmpty(tips) ? title : tips;
				if (ops.IsNull()) ops = new GUILayoutOption[2] { GUILayout.Height(textHeight), GUILayout.Width(150) };
				GUILayout.Label(new GUIContent("<size=" + size + ">" + title + "</size>", tips), style ?? G_LabelTitle, ops);
			}
			if (content != null) content();
		});
	}

	static public bool DrawButton(string label, System.Action<object> callBack, object key = null, GUIStyle style = null, int size = 14, FontStyle fontStyle = FontStyle.Normal, params GUILayoutOption[] options)
	{
		style = style == null ? G_BStyle == null ? G_ButtonStyle : G_BStyle : style;
		style = new GUIStyle(style);
		style.fontSize = G_BFSize > 0 ? G_BFSize : size;
		style.fontStyle = fontStyle;
		style.richText = true;
		try
		{
			if (GUILayout.Button(label, style, options))
			{
				if (callBack != null) callBack(key);
				return true;
			}
		}
		catch (Exception e)
		{
			Debug.Log(label);
			Debug.LogException(e);
		}
		return false;
	}

	static public bool DrawButtonWithTitle(string title, string label, System.Action<object> callBack, object key = null, GUIStyle style = null, int size = 14, FontStyle fontStyle = FontStyle.Normal, params GUILayoutOption[] options)
	{
		var s = false;
		DrawBgTitle(title, () =>
		{
			s = DrawButton(label, callBack, key, style, size, fontStyle, options);
		});
		return s;
	}

	/// <summary>
	/// 生成Tab控件
	/// </summary>
	/// <param name="btus">按钮列表</param>
	/// <param name="select">选中按钮</param>
	/// <param name="otherColor">非选择颜色</param>
	/// <param name="selectColor">选择颜色</param>
	/// <param name="defaultSelect">默认选择</param>
	/// <param name="isGrounp">是否组</param>
	/// <param name="cond">选择判断回调</param>
	/// <param name="isIgnoreReClick">是否忽略重复点击</param>
	/// <param name="labelCB">label显示回调</param>
	/// <returns>是否有操作</returns>
	static public bool DrawTab(
		string title,
		IList btus,
		ref object select,
		Color otherColor,
		Color selectColor,
		bool defaultSelect = true,
		bool isGrounp = true,
		int xCount = -1,
		System.Func<object, bool> cond = null,
		bool isIgnoreReClick = true,
		System.Func<object, string> labelCB = null,
		System.Func<object, bool> ignoreCB = null,
		int page = -1,
		int pageCount = -1,
		int xLen = -1
		)
	{
		bool b = false, isTrue = false;
		if (btus != null && btus.Count > 0)
		{
			int i = 0;
			while (select == null && defaultSelect && i < btus.Count)
			{
				b = true;
				select = btus[i++];
				if (ignoreCB != null && ignoreCB(select)) select = null;
			}
			object[] os = new object[1] { select };
			isTrue = DrawMultipleTab(title, btus, ref os, otherColor, selectColor,
				isGrounp, xCount, true, false, cond, labelCB, ignoreCB, page, pageCount, xLen: xLen);
			b = b ? b : isTrue;
			select = os.Length == 0 ? null : os[0];
			select = select ?? string.Empty;
		}
		return b;
	}

	static public int DrawButtonList(
		string title, IList list, Color color, int xCount = -1,
		System.Func<object, bool> condition = null,
		System.Func<object, string> labelCB = null,
		System.Func<object, bool> ignoreCB = null,
		int xLen = -1,
		GUIStyle style = null
		)
	{
		object select = null;
		if (DrawTab(title, list, ref select, color, color, false, false, xCount, cond: condition, labelCB: labelCB, ignoreCB: ignoreCB, xLen: xLen))
		{
			return list.IndexOf(select);
		}
		return -1;
	}

	static List<object> _tempBtns = new List<object>();
	static List<object> _tempBtns2 = new List<object>();
	static public bool DrawMultipleTab(
		string title,
		IList btus,
		ref object[] select,
		Color otherColor,
		Color selectColor,
		bool isGrounp = true,
		int xCount = -1,
		bool noMultiple = false,
		bool reClick = false,
		System.Func<object, bool> condition = null,
		System.Func<object, string> labelCB = null,
		System.Func<object, bool> ignoreCB = null,
		int page = -1,
		int pageCount = -1,
		int xLen = -1
		)
	{
		bool b = false;
		var bc = GUI.backgroundColor;
		if (btus != null && btus.Count > 0)
		{
			List<object> ss = null;
			if (!isGrounp || select == null) ss = new List<object>();
			else ss = new List<object>(select);
			xCount = xCount == -1 ? btus.Count : xCount;
			int yCount = Mathf.CeilToInt((float)btus.Count / xCount);
			int index = 0;
			string sName = null;
			var bs = new List<object>();
			var xl = xLen > 0 ? GUILayout.Width(xLen) : null;

			_tempBtns.Clear();
			_tempBtns2.Clear();
			if (ignoreCB != null)
			{
				_tempBtns.Clear();
				foreach (var v in btus)
				{
					if (!ignoreCB(v))
						_tempBtns.Add(v);
				}
				btus = _tempBtns;
			}

			if (page >= 0 && pageCount > 0)
			{
				_tempBtns2.Clear();
				if (pageCount < btus.Count)
				{
					var s = page * pageCount;
					var e = s + pageCount;
					e = e > btus.Count ? btus.Count : e;
					for (int i = s; i < e; i++)
						_tempBtns2.Add(btus[i]);
					btus = _tempBtns2;
				}
			}

			System.Action ac = () =>
			{
				GUI_DV(() =>
				{
					for (int y = 0; y < yCount; y++)
					{
						GUI_DH(() =>
						{

							for (int x = 0; x < xCount; x++)
							{
								index = y * xCount + x;
								if (btus.Count > index)
								{
									object v = btus[index];
									if (v != null)
									{
										if (isGrounp)
										{
											if (ss.Contains(v)) GUI.backgroundColor = selectColor;
											else GUI.backgroundColor = otherColor;
										}
										sName = labelCB != null ? labelCB(v) : v == null ? string.Empty : v.ToString();
										var s = xl == null ? DrawButton(sName, null, sName) : DrawButton(sName, null, sName, options: xl);

										if (s)
										{
											if (!isGrounp)
											{
												b = true;
												ss.Add(v);
											}
											else if (condition == null || condition(v))
											{
												if (noMultiple)
												{
													if (reClick && ss.Contains(v)) ss.Clear();
													else
													{
														if (!ss.Contains(v))
														{
															ss.Clear();
															ss.Add(v);
															b = true;
														}
													}
												}
												else
												{
													if (ss.Contains(v)) ss.Remove(v);
													else ss.Add(v);
													b = true;
												}
											}
										}
									}
								}
							}
						});
					}
				});
			};
			if (!string.IsNullOrEmpty(title))
			{
				//DrawTitle(title, ac, title, 14, 24, G_TabLabelTitle);
				DrawBgTitle(title, ac);
			}
			else
				ac();
			select = ss.ToArray();
		}
		GUI.backgroundColor = bc;
		return b;
	}

	static public string DrawField(string title, string text, bool enable = true, int textWidth = 100, int textHeight = 25, int size = 20, GUIStyle style = null)
	{
		string o = text;
		GUI_DH(() =>
		{
			if (!string.IsNullOrEmpty(title))
				GUILayout.Label("<size=" + size + ">" + title + "</size>", G_LabelTitle, GUILayout.Height(textHeight));
			var st = style ?? G_TextField;
			int os = st.fontSize;
			st.fontSize = size;
			text = GUILayout.TextField(text, st, GUILayout.Width(textWidth), GUILayout.Height(textHeight));
			st.fontSize = os;
		}, null, GUILayout.Width(10));
		return enable ? text == null ? "0" : text : o;
	}

	static public T DrawObjectField<T>(string title, T v, bool enable = true, int textWidth = 250, int textHeight = -1, int size = 16, GUIStyle style = null)
	{
		try
		{
			return DrawObject(v, title, enable, size, valuewidth: textWidth, valueheight: textHeight);
		}
		catch (System.Exception e)
		{
			return v;
		}
	}

	static public bool DrawToolbar(ref int selectIndex, string[] btus, string title = null, bool enable = true, System.Func<int, bool> cond = null, string tips = null)
	{
		int o = selectIndex;
		int v = selectIndex;
		GUI_DH(() =>
		{
			title = !string.IsNullOrEmpty(tips) ? title + "(" + tips + ")" : title;
			if (!string.IsNullOrEmpty(title)) GUILayout.Label(new GUIContent(title, title), G_LabelTitle, GUILayout.Height(25));
			v = GUILayout.Toolbar(v, btus, GUILayout.Height(25));
			if (enable && v != o)
			{
				enable = true;
				if (cond != null && !cond(v))
				{
					v = o;
					enable = false;
				}
			}
			else enable = false;
		}, null, GUILayout.Width(10));
		GUILayout.Space(2);
		if (enable) selectIndex = v;
		return enable;
	}

	static public void DrawTiledTexture(Rect rect, Texture tex)
	{
		GUI.BeginGroup(rect);
		{
			int width = Mathf.RoundToInt(rect.width);
			int height = Mathf.RoundToInt(rect.height);

			for (int y = 0; y < height; y += tex.height)
			{
				for (int x = 0; x < width; x += tex.width)
				{
					GUI.DrawTexture(new Rect(x, y, tex.width, tex.height), tex);
				}
			}
		}
		GUI.EndGroup();
	}

	const string c_open_str = "▼";
	const string c_close_str = "►";

	static public bool DrawFadeOut(bool open, string title, System.Action<bool> bodyCallBack)
	{
		GUILayout.BeginVertical();
		GUILayout.BeginHorizontal();
		if (DrawButton(title, null, null, null, 16, FontStyle.Bold))
		{
			open = !open;
		}
		var r = GUILayoutUtility.GetLastRect();
		r = new Rect(r.min.x - 3, r.min.y, 30, 30);
		GUI.Label(r, open ? c_open_str : c_close_str, G_Muti_Title_Style_Nobg);
		GUILayout.EndHorizontal();
		if (bodyCallBack != null) bodyCallBack(open);
		GUILayout.EndVertical();
		return open;
	}

	static public T DrawEnum<T>(string title, T value, int xCount = 6, bool isflags = false) where T : struct
	{
		var bc = GUI.backgroundColor;
		if (typeof(T).IsEnum)
		{
			string[] ns = System.Enum.GetNames(typeof(T));
			int[] vs = (int[])System.Enum.GetValues(typeof(T));
			List<object> os = new List<object>();
			value.ParseEnumValue<T>(os);
			object[] ss = os.ToArray();
			if (DrawMultipleTab(title, vs, ref ss, Color.white, Color.green, true, xCount, !isflags, isflags, null, (v) => { return ((T)v).ToString(); }))
			{
				if (!ss.IsNull())
				{
					int l = ss.Length;
					int v = 0;
					vs = new int[l];
					for (int i = 0; i < l; i++) v += (int)ss[i];
					value = (T)System.Enum.ToObject(typeof(T), v);
				}
				else value = default(T);
			}
		}
		GUI.backgroundColor = bc;
		return value;
	}

	static public bool DrawToggle(string title, bool v, bool enable = true, params GUILayoutOption[] options)
	{
		return DrawObject<bool>(v, title, enable, options: options);
	}

	static public bool DrawToggle(string title, bool v, int titleWidth, bool enable = true, params GUILayoutOption[] options)
	{
		return DrawObject<bool>(v, title, enable, titlewidth: titleWidth, options: options);
	}

	static public void DrawOutline(Rect rect)
	{
		if (Event.current.type == EventType.Repaint)
		{
			Texture2D tex = contrastTexture;
			GUI.color = Color.white;
			DrawTiledTexture(new Rect(rect.xMin, rect.yMax, 1f, -rect.height), tex);
			DrawTiledTexture(new Rect(rect.xMax, rect.yMax, 1f, -rect.height), tex);
			DrawTiledTexture(new Rect(rect.xMin, rect.yMin, rect.width, 1f), tex);
			DrawTiledTexture(new Rect(rect.xMin, rect.yMax, rect.width, 1f), tex);
		}
	}

	static public void DrawOutline(Rect rect, Color color, float size = 1f)
	{
		if (Event.current.type == EventType.Repaint)
		{
			Texture2D tex = blankTexture;
			GUI.color = color;
			GUI.DrawTexture(new Rect(rect.xMin - size, rect.yMin - size, size, rect.height + size * 2), tex);
			GUI.DrawTexture(new Rect(rect.xMax, rect.yMin - size, size, rect.height + size * 2), tex);
			GUI.DrawTexture(new Rect(rect.xMin, rect.yMin - size, rect.width, size), tex);
			GUI.DrawTexture(new Rect(rect.xMin, rect.yMax, rect.width, size), tex);
			GUI.color = Color.white;
		}
	}

	static public void DrawOutline(Rect rect, Rect relative, Color color)
	{
		if (Event.current.type == EventType.Repaint)
		{
			// Calculate where the outer rectangle would be
			float x = rect.xMin + rect.width * relative.xMin;
			float y = rect.yMax - rect.height * relative.yMin;
			float width = rect.width * relative.width;
			float height = -rect.height * relative.height;
			relative = new Rect(x, y, width, height);

			// Draw the selection
			DrawOutline(relative, color);
		}
	}

	static public void DrawOutline(Rect rect, Rect relative)
	{
		if (Event.current.type == EventType.Repaint)
		{
			// Calculate where the outer rectangle would be
			float x = rect.xMin + rect.width * relative.xMin;
			float y = rect.yMax - rect.height * relative.yMin;
			float width = rect.width * relative.width;
			float height = -rect.height * relative.height;
			relative = new Rect(x, y, width, height);

			// Draw the selection
			DrawOutline(relative);
		}
	}

	static public void DrawOutline(Rect rect, Rect outer, Rect inner)
	{
		if (Event.current.type == EventType.Repaint)
		{
			Color green = new Color(0.4f, 1f, 0f, 1f);

			DrawOutline(rect, new Rect(outer.x, inner.y, outer.width, inner.height));
			DrawOutline(rect, new Rect(inner.x, outer.y, inner.width, outer.height));
			DrawOutline(rect, outer, green);
		}
	}

	static public void DrawMask(Rect rect, string tips)
	{
		GUI.Button(rect, tips, G_Mask_Style);
	}

	static readonly string[] __ver = new string[] { "0", "0", "0" };
	static public string DrawVersion(string title, string version, bool enable = true, string spchar = ".", int size = 40)
	{
		var vs = string.IsNullOrEmpty(version) ? __ver : version.Split(new string[] { spchar }, StringSplitOptions.RemoveEmptyEntries);
		DrawBgTitle(title, () =>
		{
			GUI_DH(() =>
			{
				for (int i = 0; i < vs.Length; i++)
					vs[i] = DrawObject(vs[i], i != 0 ? spchar : string.Empty, enable, 22, 22, 14, valuewidth: size, nobg: true);
			}, ops: GUILayout.Width(vs.Length * (size + 14)));
			version = string.Join(spchar, vs);
		});
		return version;
	}

	static public void DrawEmpty(int w = 0, int h = 0)
	{
		if (w > 0 && h == 0)
			GUILayout.Button(String.Empty, G_NULL, GUILayout.Width(w));
		else if (w == 0 && h > 0)
			GUILayout.Button(String.Empty, G_NULL, GUILayout.Height(h));
		else if (w > 0 && h > 0)
			GUILayout.Button(String.Empty, G_NULL, GUILayout.Width(w), GUILayout.Height(h));
		else
			GUILayout.Button(String.Empty, G_NULL);
	}

	#region Create

	static public Texture2D CreateCheckerTex(Color c0, Color c1)
	{
		Texture2D tex = new Texture2D(16, 16);
		tex.name = "[Generated] Checker Texture";
		tex.hideFlags = HideFlags.DontSave;

		for (int y = 0; y < 8; ++y) for (int x = 0; x < 8; ++x) tex.SetPixel(x, y, c1);
		for (int y = 8; y < 16; ++y) for (int x = 0; x < 8; ++x) tex.SetPixel(x, y, c0);
		for (int y = 0; y < 8; ++y) for (int x = 8; x < 16; ++x) tex.SetPixel(x, y, c0);
		for (int y = 8; y < 16; ++y) for (int x = 8; x < 16; ++x) tex.SetPixel(x, y, c1);

		tex.Apply();
		tex.filterMode = FilterMode.Point;
		return tex;
	}

	static public Texture2D CreateToggleTex(Color c0, Color c1)
	{
		Texture2D tex = new Texture2D(16, 16);
		tex.name = "[Generated] Checker Texture";
		tex.hideFlags = HideFlags.DontSave;

		for (int y = 0; y < 16; ++y)
		{
			for (int x = 0; x < 8; ++x) tex.SetPixel(x, y, c0);
			for (int x = 8; x < 16; ++x) tex.SetPixel(x, y, c1);
		}

		tex.Apply();
		tex.filterMode = FilterMode.Point;
		return tex;
	}

	public static Texture2D CreateGradientTex(Color color1, Color color2)
	{
		Texture2D tex = new Texture2D(1, 16);
		tex.name = "[Generated] Gradient Texture";
		tex.hideFlags = HideFlags.DontSave;


		for (int i = 0; i < 16; ++i)
		{
			float f = Mathf.Abs((i / 15f) * 2f - 1f);
			f *= f;
			tex.SetPixel(0, i, Color.Lerp(color1, color2, f));
		}

		tex.Apply();
		tex.filterMode = FilterMode.Bilinear;
		return tex;
	}

	public static Texture2D CreateNormalTex(Color color)
	{
		Texture2D tex = new Texture2D(1, 16);
		tex.name = "[Generated] Gradient Texture";
		tex.hideFlags = HideFlags.DontSave;


		for (int i = 0; i < 16; ++i)
		{
			float f = Mathf.Abs((i / 15f) * 2f - 1f);
			f *= f;
			tex.SetPixel(0, i, color);
		}

		tex.Apply();
		tex.filterMode = FilterMode.Bilinear;
		return tex;
	}

	#endregion

	#region NewGUI

	public static Rect G_Silder_Rect { get; private set; }

	public static GUIStyle G_Muti_Title_Style = new GUIStyle(GUIHelp.G_LabelTitle)
	{
		fixedHeight = 22,
		margin = new RectOffset(2, 2, 2, 2),
		normal = new GUIStyleState()
		{
			background = whiteTexture
		}
	};

	public static GUIStyle G_Muti_Title_Style_Nobg = new GUIStyle(GUIHelp.G_LabelTitle)
	{
		fixedHeight = 22,
		margin = new RectOffset(2, 2, 2, 2),
		fontSize = 25,
		normal = new GUIStyleState()
		{
			background = null,
			textColor = Color.white,
		}
	};


	public static GUIStyle G_Muti_Label_NoBg_Style = new GUIStyle(GUIHelp.G_LabelTitle)
	{
		fixedHeight = 22,
		fontSize = 15,
		margin = new RectOffset(2, 2, 2, 2),
		alignment = TextAnchor.MiddleCenter,
		fontStyle = FontStyle.Bold,
	};

	public static readonly GUIStyle G_Muti_TextField = new GUIStyle(GUI.skin.textField)
	{
		alignment = TextAnchor.MiddleLeft,
		fixedHeight = 22,
		margin = new RectOffset(2, 2, 2, 2),
		richText = true,
	};

	public static readonly GUIStyle G_Muti_switch_false = new GUIStyle()
	{
		fixedHeight = 21,
		fixedWidth = 60,
		normal = new GUIStyleState()
		{
			background = ToggleTexFalse,
		},
		margin = new RectOffset(2, 2, 2, 2),

	};

	public static readonly GUIStyle G_Muti_switch_true = new GUIStyle()
	{
		fixedHeight = 21,
		fontStyle = FontStyle.Normal,
		fixedWidth = 60,
		normal = new GUIStyleState()
		{
			background = ToggleTexTrue,
		},
		margin = new RectOffset(2, 2, 2, 2),

	};

	private static readonly GUIStyle _G_Muti_Title_Style = new GUIStyle(G_Muti_Title_Style);
	private static readonly GUIStyle _G_Muti_TextField = new GUIStyle(G_Muti_TextField);
	private static readonly GUIStyle G_Label_Style = new GUIStyle(GUI.skin.label)
	{
		alignment = TextAnchor.MiddleLeft,
		fixedHeight = 22,
		richText = true,
		margin = new RectOffset(2, 2, 2, 2)
	};

	static public T DrawObject<T>(
		T value, string title, bool enable = true,
		int textsize = 16, int height = 0,
		int titlewidth = 150, int valuewidth = 250, string tips = null, bool textArea = false, int valueheight = -1, bool nobg = false
		, params GUILayoutOption[] options

	)
	{

		var vs = _G_Muti_TextField;
		var ts = _G_Muti_Title_Style;
		vs.fontSize = ts.fontSize = textsize;
		height = height >= 0 ? height : textsize > (int)vs.fixedHeight ? textsize + 4 : 0;
		vs.fixedHeight = ts.fixedHeight = height >= 0 ? height : G_Muti_TextField.fixedHeight;
		vs.fixedWidth = valuewidth >= 0 ? valuewidth : G_Muti_TextField.fixedWidth;
		ts.fixedWidth = titlewidth >= 0 ? titlewidth : _G_Muti_Title_Style.fixedWidth;
		ts.normal = nobg ? G_Muti_Label_NoBg_Style.normal : G_Muti_Title_Style.normal;
		vs.wordWrap = textArea;
		vs.alignment = textArea ? TextAnchor.UpperLeft : TextAnchor.MiddleLeft;
		if (valueheight == 0) vs.fixedHeight = 0;
		else if (valueheight > 0) vs.fixedHeight = vs.fixedHeight > valueheight ? vs.fixedHeight : valueheight;
		else vs.fixedHeight = G_Muti_TextField.fixedHeight;

		value = DrawObjectStyle(value, title, enable, vs, ts, tips, textArea, options);
		ts.normal = G_Muti_Title_Style.normal;

		return value;
	}

	static public T DrawObjectStyle<T>(
		T value, string title, bool enable = true,
		GUIStyle valueStyle = null, GUIStyle titleStyle = null
		, string tips = null, bool textArea = false, params GUILayoutOption[] options
	)
	{
		var ov = value;
		valueStyle = valueStyle ?? G_Muti_TextField;
		if (!string.IsNullOrEmpty(title))
		{
			titleStyle = titleStyle ?? G_Muti_Title_Style;
			var len = titleStyle.fixedWidth + valueStyle.fixedWidth + 10;
			GUI_DH(() =>
			{
				GUILayout.Label(new GUIContent(title, tips), titleStyle, options);
				value = DrawObject(value, enable, valueStyle, textArea);
			}, null);
		}
		else
		{
			value = DrawObject(value, enable, valueStyle, textArea, options);
		}
		return value;
	}

	static private Action _dtCallNoArg;
	static private Func<int> _dtCall = () =>
	{
		if (_dtCallNoArg != null) _dtCallNoArg();
		return 0;
	};

	static public void DrawBgTitle(
			string title, Action content = null, int textsize = 16,
			int titlewidth = 150, string tips = null, bool titleCenter = false, bool nobg = false, int flow = 0)
	{
		int rf = 0;
		_dtCallNoArg = content;
		DrawBgTitle(title, ref rf, _dtCall, textsize, titlewidth, tips, titleCenter, nobg, flow);
		_dtCallNoArg = null;
	}

	static public void DrawBgTitle(
		string title, ref int height, Func<int> content = null, int textsize = 16,
		int titlewidth = 150, string tips = null, bool titleCenter = false, bool nobg = false, int flow = 0)
	{
		if (!string.IsNullOrEmpty(title))
		{
			var ts = _G_Muti_Title_Style;
			ts.fontSize = textsize;
			var h = height > 0 ? height : textsize > (int)G_Muti_Title_Style.fixedHeight ? textsize + 4 : 0;
			ts.fixedHeight = h > 0 ? h : G_Muti_Title_Style.fixedHeight;
			ts.fixedWidth = titlewidth >= 0 ? titlewidth : G_Muti_Title_Style.fixedWidth;
			ts.alignment = titleCenter ? TextAnchor.MiddleCenter : G_Muti_Title_Style.alignment;
			ts.normal = nobg ? G_Muti_Label_NoBg_Style.normal : G_Muti_Title_Style.normal;
			h = height;
			GUI_DH(() =>
			{
				GUILayout.Label(new GUIContent(title, tips), ts);
				try
				{
					if (content != null)
					{
						if (flow == 0)
							h = content();
						else
						{
							var call = flow > 0 ? G_DH : G_DV;
							call(() =>
							{
								h = content();
							});
						}
					}
				}
				catch (Exception e)
				{
					Debug.LogException(e);
				}
			});
			ts.normal = G_Muti_Title_Style.normal;
			height = h;
		}
		else if (content != null)
			content();
	}


	const string c_true_str = "(<color=green>ON</color>)";
	const string c_false_str = "(<color=green>OFF</color>)";
	const string c_format_str = "<size={0}>{1}</size>";

	static private T DrawObject<T>(T value, bool enable = true, GUIStyle style = null, bool textArea = false, params GUILayoutOption[] options)
	{
		try
		{
			var ty = typeof(T);
			if (ty == typeof(bool))
			{
				var b = (bool)(object)value;
				var s = b ? c_true_str : c_false_str;
				style = style ?? _G_Muti_TextField;
				GUI_DH(() =>
				{
					b = GUILayout.Toggle(b, string.Empty, b ? G_Muti_switch_true : G_Muti_switch_false, options);
					GUILayout.Label(new GUIContent(string.Format(c_format_str, style.fontSize, s)), G_LabelTitle, GUILayout.Width(50));
				}, null, GUILayout.Width(100));
				if (enable) value = (T)(object)b;
			}
			else if (ty.IsPrimitive || ty == typeof(string))
			{
				if (!textArea)
				{
					if (enable)
					{
						var v = TextField(value == null ? string.Empty : value.ToString(), style ?? G_Muti_TextField, options);
						value = (T)Convert.ChangeType(v, typeof(T));
					}
					else
						GUILayout.TextField(value == null ? string.Empty : value.ToString(), style ?? G_Muti_TextField);
				}
				else
				{
					if (enable)
						value = (T)System.Convert.ChangeType(GUILayout.TextArea(value == null ? string.Empty : value.ToString(), style ?? G_Muti_TextField, options), typeof(T));
					else
						GUILayout.TextArea(value == null ? string.Empty : value.ToString(), style ?? G_Muti_TextField, options);
				}
			}
#if UNITY_EDITOR
			else if (typeof(UnityEngine.Object).IsAssignableFrom(ty))
			{
				var o = (UnityEngine.Object)(object)value;
				GUILayout.Space(-2);
				o = EditorGUILayout.ObjectField(o, ty, true, GUILayout.Height(style.fixedHeight));
				if (enable) value = (T)(object)o;
			}
			else if (value is Color)
			{
				var c = (Color)(object)value;
				c = EditorGUILayout.ColorField(c, GUILayout.Height(style.fixedHeight));
				if (enable) value = (T)(object)c;
			}else if(value is SerializedProperty serialized)
			{
				var c = serialized;
				EditorGUILayout.ObjectField(c, GUILayout.Height(style.fixedHeight));
				if (enable) value = (T)(object)c;
			}
#endif
		}
		catch (Exception e)
		{
			if (!(e is ExitGUIException))
				Debug.LogException(e);
		}
		return value;
	}

	static public void DrawNormalField<T>(
		T value, string title,
		int textsize = 16, int height = 0,
		int titlewidth = 150, int valuewidth = 250, bool wrap = false, int valueheight = -1,
		bool nobg = false
		)
	{

		var vs = G_Label_Style;
		var ts = _G_Muti_Title_Style;

		height = height > 0 ? height : textsize > (int)vs.fixedHeight ? textsize + 4 : 0;
		vs.fontSize = textsize;
		vs.fixedHeight = height > 0 ? height : G_Muti_TextField.fixedHeight;
		vs.fixedWidth = valuewidth > 0 ? valuewidth : G_Muti_TextField.fixedWidth;
		if (valueheight == 0) vs.fixedHeight = 0;
		if (valueheight > 0) vs.fixedHeight = vs.fixedHeight > valueheight ? vs.fixedHeight : valueheight;
		vs.wordWrap = wrap;
		vs.alignment = wrap ? TextAnchor.UpperLeft : TextAnchor.MiddleLeft;

		DrawBgTitle(title, ref height, () =>
		{
			GUILayout.Label(value == null ? string.Empty : value.ToString(), vs);
			return height;
		}, textsize, titlewidth, null, nobg: nobg);
	}

	static public readonly GUIStyle G_Progress_Bg_Style = new GUIStyle(GUI.skin.horizontalSlider)
	{
		normal = new GUIStyleState()
		{
			background = halfATexture
		},
		margin = new RectOffset(2, 2, 2, 2),
		fixedHeight = 22,
		fixedWidth = 300,
		border = new RectOffset(),
		padding = new RectOffset(),
		overflow = new RectOffset(),
	};

	static public readonly GUIStyle G_Progress_Label_Style = new GUIStyle(GUI.skin.label)
	{
		fixedHeight = 22,
		margin = new RectOffset(2, 2, 2, 2),
		alignment = TextAnchor.MiddleCenter,
		richText = true,
		normal = new GUIStyleState()
		{
			textColor = Color.white,
		},
		fontSize = 18,
	};

	static private readonly GUIStyle _G_Progress_Bg_Style = new GUIStyle(G_Progress_Bg_Style);

	static private readonly GUIStyle G_Progress_Top_Style = new GUIStyle(G_Progress_Bg_Style)
	{
		normal = new GUIStyleState()
		{
			background = CreateNormalTex(Color.white) //CreateGradientTex(new Color(0f, 0.8f, 1f, 1), new Color(0f, 0.3f, 1f, 1))
		},
	};

	static public void DrawProgress(
		string title, float value, float minvalue, float maxvalue,
		int width = -1, int height = -1,
		string progrestips = null,
		bool titleNobg = false
		)
	{
		var h = 0;
		var w = width >= 0 ? width : G_Progress_Bg_Style.fixedWidth;
		_G_Progress_Bg_Style.fixedWidth = w;
		_G_Progress_Bg_Style.fixedHeight = height >= 0 ? height : G_Progress_Bg_Style.fixedHeight;
		G_Progress_Top_Style.fixedHeight = height >= 0 ? height : G_Progress_Bg_Style.fixedHeight;
		minvalue = Mathf.Max(minvalue, float.MinValue);
		maxvalue = Mathf.Min(maxvalue, float.MaxValue);
		var p = (value - minvalue) / (maxvalue - minvalue);
		progrestips = progrestips ?? string.Format("当前进度{0}%", (p * 100).ToString("f2"));
		DrawBgTitle(title, ref h, () =>
		{
			GUI_DH(() =>
			{
				GUILayout.Button(string.Empty, _G_Progress_Bg_Style);
				var rect = GUILayoutUtility.GetLastRect();
				G_Progress_Top_Style.fixedWidth = Mathf.Max(rect.width * ((value - minvalue) / (maxvalue - minvalue)), 0.01f);
				GUI.Label(rect, string.Empty, G_Progress_Top_Style);
				if (!string.IsNullOrEmpty(progrestips))
					GUI.Label(rect, progrestips, G_Progress_Label_Style);
			});
			return 0;
		}, nobg: titleNobg);
	}

	static public float DrawSilder(string title, float value,
		float minvalue, float maxvalue,
		bool enable = true, bool toint = true,
		int width = -1, int height = -1,
		bool showValue = true,
		int valueWidth = -1,
		string tips = null, bool titleNobg = false,
		int thickness = 6,
		int ow = 0,
		int precision = 2,
		int titleWidth = -1
		)
	{
		var h = 0;
		var w = width >= 0 ? width : G_Progress_Bg_Style.fixedWidth;
		_G_Progress_Bg_Style.fixedWidth = w;
		_G_Progress_Bg_Style.fixedHeight = height > 0 ? height : G_Progress_Bg_Style.fixedHeight;
		G_Progress_Top_Style.fixedHeight = height > 0 ? height : G_Progress_Bg_Style.fixedHeight;

		minvalue = Mathf.Max(minvalue, float.MinValue);
		maxvalue = Mathf.Min(maxvalue, float.MaxValue);
		G_Progress_Top_Style.fixedWidth = thickness;

		valueWidth = valueWidth >= 0 ? valueWidth : 100;
		DrawBgTitle(title, ref h, () =>
		{
			GUI_DH(() =>
			{
				GUILayout.Label(string.Empty, G_TextField);
				var rect = GUILayoutUtility.GetLastRect();
				G_Silder_Rect = new Rect(rect.min.x, rect.min.y, w > 0 ? _G_Progress_Bg_Style.fixedWidth : rect.width + ow, _G_Progress_Bg_Style.fixedHeight);
				var v = GUI.HorizontalSlider(G_Silder_Rect, value, minvalue, maxvalue, _G_Progress_Bg_Style, G_Progress_Top_Style);
				if (toint) v = (int)v;
				else v = precision > 0 ? float.Parse(v.ToString("f" + precision)) : v;
				if (!string.IsNullOrEmpty(tips))
				{
					GUI.color = Color.red;
					GUI.Label(rect, tips, G_Muti_Label_NoBg_Style);
					GUI.color = G_LAB_NORMAL_COLOR;
				}
				if (showValue)
				{
					v = DrawObject(v, null, enable, valuewidth: valueWidth);
				}
				if (enable) value = v;
			});
			return 0;
		}, nobg: titleNobg, titlewidth: titleWidth);
		return Mathf.Clamp(value, minvalue, maxvalue);
	}

	#endregion

	#region UnityEditor

#if UNITY_EDITOR

	static public GUIStyle G_Popup_Style = new GUIStyle(EditorStyles.popup)
	{
		normal = new GUIStyleState()
		{
			background = CreateGradientTex(Color.grey, Color.black),
			textColor = Color.white,
		},
		focused = new GUIStyleState()
		{
			background = CreateGradientTex(Color.white, Color.black),
			textColor = Color.black,
		},
		fixedHeight = 20,
		stretchWidth = false,
		alignment = TextAnchor.MiddleCenter,
		fontSize = 14,
		padding = new RectOffset(0, 0, 0, 0),
		margin = new RectOffset(2, 2, 2, 6)
	};

	static public GUIStyle G_PopupFlag_Style = new GUIStyle(GUI.skin.label)
	{
		fontSize = 20,
		normal = new GUIStyleState()
		{
			textColor = Color.black
		}
	};


	static public Vector2 EditorGUI_DS(System.Action draw, ref Vector2 position, bool allowH = true, bool allowV = true, GUIStyle styleH = null, GUIStyle styleV = null, params GUILayoutOption[] ops)
	{
		styleH = styleH == null ? GUIStyle.none : styleH;
		styleV = styleV == null ? GUIStyle.none : styleV;
		position = UnityEditor.EditorGUILayout.BeginScrollView(position, allowH, allowV, ops);
		if (draw != null) draw();
		EditorGUILayout.EndScrollView();
		return position;
	}

	static readonly Dictionary<int, Vector2> _scrollVecs = new Dictionary<int, Vector2>();

	static public void Editor_DS(int key, System.Action draw, bool allowH = true, bool allowV = true, GUIStyle styleH = null, GUIStyle styleV = null, params GUILayoutOption[] ops)
	{
		Vector2 vector = Vector2.zero;
		_scrollVecs.TryGetValue(key, out vector);
		GUI_DV(() =>
		{
			EditorGUI_DS(draw, ref vector, allowH, allowV, styleH, styleV);
		}, null, ops);
		_scrollVecs[key] = vector;
	}

	static public void DrawProgress(string title, float progress, string text, params GUILayoutOption[] options)
	{
		DrawTitle(title, () =>
		{
			DrawButton(null, null, null, null, 14, FontStyle.Normal, options);
			Rect r = GUILayoutUtility.GetLastRect();
			r.height += 3;
			r.width += 3;
			r.x -= 1;
			EditorGUI.ProgressBar(r, progress, text);
		});

	}

	private static object _tempObj;
	static public bool DrawFileSelect(
		string title, ref string path, string filter = "*.*",
		int titleWidth = 100, OnCheckPath condition = null)
	{
		if (DrawTab(title, new string[1] { string.IsNullOrEmpty(path) ? "点击选择" : path }, ref _tempObj, G_File_PATH_Color, G_File_PATH_Color, false))
		{
			_tempObj = null;
			var p = EditorUtility.OpenFilePanel("", path, filter);
			if (string.IsNullOrEmpty(p)) return false;
			if ((condition == null || condition(ref p)) && p != path)
			{
				path = p;
				return true;
			}
		}
		return false;
	}

	static public bool DrawFolderSelect(
		string title, ref string path, string defName = null, OnCheckPath condition = null)
	{
		if (DrawTab(title, new string[1] { string.IsNullOrEmpty(path) ? "点击选择" : path }, ref _tempObj, G_File_PATH_Color, G_File_PATH_Color, false))
		{
			_tempObj = null;
			var p = EditorUtility.OpenFolderPanel("", path, defName);

			if (string.IsNullOrEmpty(p)) return false;
			if ((condition == null || condition(ref p)) && p != path)
			{
				path = p;
				return true;
			}
		}
		return false;
	}


	static public bool DrawFoldersSelect<T>(
		string title, ref T path, bool needAddBtn = true, string defName = null, OnCheckPath condition = null) where T : IList
	{
		if (path == null) return false;
		var state = false;

		if (path.Count > 0)
		{
			_tempObj = null;
			if (DrawTab(title, path, ref _tempObj, G_File_PATH_Color, G_File_PATH_Color, false, false, 1))
			{
				if (path.Contains(_tempObj))
				{
					if (EditorUtility.DisplayDialog("Warring", "是否删除该路径???", "删除"))
						path.Remove(_tempObj);
				}
			}
		}

		if (path.Count == 0 || needAddBtn)
		{
			string addPath = null;
			if (DrawFolderSelect(title, ref addPath, defName, condition))
			{
				if (!path.Contains(addPath))
				{
					path.Add(addPath);
					state = true;
				}
			}
		}
		_tempObj = null;
		return state;
	}

	static public T DrawPopup<T>(string title, T select, IList<T> items, bool nobg = false, params GUILayoutOption[] ops)
	{

		//if (!string.IsNullOrEmpty(title)) DrawTitle(title, null, title, 15, 20, null, GUILayout.Width(120));
		int rh = 0;
		DrawBgTitle(title, ref rh, () =>
		{
			if (items != null && items.Count > 0)
			{
				var idx = 0;
				var ls = new string[items.Count];
				for (int i = 0; i < items.Count; i++)
				{
					if (items[i].Equals(select)) idx = i;
					ls[i] = items[i].ToString();
				}
				idx = EditorGUILayout.Popup(idx, ls, G_Popup_Style, ops);
				select = items[idx];
			}
			else
			{
				EditorGUILayout.Popup(0, new string[0], G_Popup_Style, ops);
				select = default(T);
			}
			var r = GUILayoutUtility.GetLastRect();
			r = new Rect(r.max.x - 25, r.min.y - 2, 30, 30);
			GUI.Label(r, "▼", G_PopupFlag_Style);

			return rh;
		}, nobg: nobg);

		return select;
	}

	static public bool ConfimDialog(string title, string msg, Action okcall = null, Action cancelcall = null, string ok = null, string cancel = null)
	{

		if (EditorUtility.DisplayDialog(title, msg, ok ?? "确定", cancel ?? "取消"))
		{
			if (okcall != null) okcall();
			return true;
		}
		else
		{
			if (cancelcall != null) cancelcall();
			return false;
		}
	}

#endif

	#endregion

	#region Method

	public static string HandleCopyPaste(int controlID)
	{
		if (controlID == GUIUtility.keyboardControl)
		{
			if (Event.current.type == UnityEngine.EventType.KeyUp && (Event.current.modifiers == EventModifiers.Control || Event.current.modifiers == EventModifiers.Command))
			{
				if (Event.current.keyCode == KeyCode.C)
				{
					Event.current.Use();
					TextEditor editor = (TextEditor)GUIUtility.GetStateObject(typeof(TextEditor), GUIUtility.keyboardControl);
					editor.Copy();
				}
				else if (Event.current.keyCode == KeyCode.V)
				{
					Event.current.Use();
					TextEditor editor = (TextEditor)GUIUtility.GetStateObject(typeof(TextEditor), GUIUtility.keyboardControl);
					editor.Paste();
#if UNITY_5_3_OR_NEWER || UNITY_5_3
					return editor.text; //以及更高的unity版本中editor.content.text已经被废弃，需使用editor.text代替
#else
                    return editor.content.text;
#endif
				}
				else if (Event.current.keyCode == KeyCode.A)
				{
					Event.current.Use();
					TextEditor editor = (TextEditor)GUIUtility.GetStateObject(typeof(TextEditor), GUIUtility.keyboardControl);
					editor.SelectAll();
				}
			}
		}
		return null;
	}

	public static string TextField(string value, GUIStyle style, params GUILayoutOption[] options)
	{
		int textFieldID = GUIUtility.GetControlID("TextField".GetHashCode(), FocusType.Keyboard) + 1;
		if (textFieldID == 0) return value;
		//处理复制粘贴的操作
		//var s = HandleCopyPaste(textFieldID);
		try
		{
			value = GUILayout.TextField(value, style, options);
		}
		catch (Exception e)
		{
			Debug.LogException(e);
		}
		return value;
	}

	#endregion
}

public static partial class CommonUtil
{
	#region Extend

	static public bool IsNull(this IList list)
	{
		bool b = true;
		if (list != null && list.Count > 0) return false;
		return b;
	}

	static public T[] ParseEnumValue<T>(this T e, IList list = null) where T : struct
	{
		List<T> ts = null;
		Type t = typeof(T);
		if (typeof(T).IsEnum)
		{
			ts = new List<T>();
			if (t.GetCustomAttributes(typeof(FlagsAttribute), true).Length > 0)
			{
				ts = new List<T>();
				string[] ss = e.ToString().Split(',');
				if (ss.Length > 1)
				{
					foreach (var v in ss)
					{
						T vv = (T)Enum.Parse(t, v);
						ts.Add(vv);
						if (list != null) list.Add(vv);
					}
				}
				else
				{
					ts.Add(e);
					if (list != null) list.Add(e);
				}
			}
			else
			{
				ts.Add(e);
				if (list != null) list.Add(e);
			}
		}
		return ts == null ? null : ts.ToArray();
	}

	#region File

	static public void CreateDir(string path)
	{
		if (!string.IsNullOrEmpty(path))
		{
			DirectoryInfo info = new DirectoryInfo(path);
			if (!info.Exists) info.Create();
		}
	}

	static public bool Exists(string path, out bool isDir)
	{
		bool b = false;
		isDir = true;
		if (!string.IsNullOrEmpty(path))
		{
			b = Directory.Exists(path);
			if (!b)
			{
				isDir = false;
				b = File.Exists(path);
			}
		}
		return b;
	}

	static public List<string> GetDirFiles(string dir, string pattern = "*", bool deeps = false)
	{
		List<string> files = new List<string>();
		bool ex = false, isDir = false;
		ex = Exists(dir, out isDir);
		if (ex && isDir)
		{
			string[] ps = string.IsNullOrEmpty(pattern) ? new string[] { "*" } : pattern.Split('|');
			for (int i = 0; i < ps.Length; i++)
			{
				files.AddRange(Directory.GetFiles(dir, ps[i], deeps ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly));
			}
		}
		return files;
	}

	static public void ClearFiles(string dir, string ignorepattern = null, bool deeps = false, string onlydeletepix = null)
	{
		var isDir = false;
		if (Exists(dir, out isDir) && isDir)
		{
			string[] ps = string.IsNullOrEmpty(ignorepattern) ? null : ignorepattern.Split('|');
			var fs = GetDirFiles(dir, "*", true);
			if (fs.Count > 0)
			{
				foreach (var v in fs)
				{
					var e = Path.GetExtension(v);
					var n = Path.GetFileNameWithoutExtension(v);
					if ((ps == null || !ps.Contains(e)) || (onlydeletepix != null && n.StartsWith(onlydeletepix)))
						DeleteFile(v);
				}
			}
		}
	}

	static public void ClearFiles(string dir, IList<string> deletefiles, string ext = null)
	{
		if (string.IsNullOrEmpty(dir) || deletefiles == null || deletefiles.Count == 0) return;
		foreach (var v in deletefiles)
		{
			var f = Path.Combine(dir, v) + ext;
			DeleteFile(f);
		}

	}

	static public bool DeleteFile(string path, bool check = true)
	{
		bool b = true;
		if (check)
		{
			if (!string.IsNullOrEmpty(path))
			{
				bool ex = false, isFile = true;
				ex = Exists(path, out isFile);
				b = ex && !isFile;
			}
		}
		if (b) File.Delete(path);
		return b;
	}

	static public void CopyFiles(string spath, string dpath, string pattern = "*", bool deep = false, bool keepdir = false, string ext = null)
	{
		var fs = GetDirFiles(spath, pattern, deep);
		if (fs != null && fs.Count > 0)
			CopyFiles(fs, dpath, keepdir ? spath : null, ext);
	}

	static public void CopyFiles(List<string> files, string dir, string olddir = null, string ext = null)
	{
		if (string.IsNullOrEmpty(dir) || files == null || files.Count == 0) return;
		CreateDir(dir);
		bool e = !string.IsNullOrEmpty(ext);
		bool s = !string.IsNullOrEmpty(olddir);
		foreach (var f in files)
		{
			var ss = f.Split('|');
			var v = ss[0];
			var other = ss.Length > 1 ? ss[1] : null;
			var n = s ? v.Replace(olddir, dir) : Path.Combine(dir, Path.GetFileName(v));

			if (e)
			{
				if (Path.HasExtension(n)) n = Path.ChangeExtension(n, ext);
				else n += "." + ext;
			}

			if (!string.IsNullOrEmpty(other))
			{
				ext = Path.GetExtension(n);
				n = n.Replace(Path.GetFileName(n), other + ext);
			}

			if (s) CreateDir(Path.GetDirectoryName(n));
			File.Copy(v, n, true);
		}
	}

	static public List<string> GetMatchPath(string dir, string matchString)
	{
		bool isDir = false;
		if (Exists(dir, out isDir) && isDir)
		{
			string[] fl = Directory.GetFiles(dir, matchString);
			if (!fl.IsNull())
			{
				return fl.ToList();
			}
		}
		return null;
	}

	static public byte[] ReadFile(string path)
	{
		if (!string.IsNullOrEmpty(path))
		{
			FileStream fileStream = new FileStream(path, FileMode.Open);
			byte[] bs = new byte[fileStream.Length];
			fileStream.Read(bs, 0, bs.Length);
			fileStream.Flush();
			fileStream.Close();
			fileStream.Dispose();
			return bs;
		}
		return null;
	}

	static public long GetFileLen(string path)
	{
		if (File.Exists(path))
		{
			using (var f = File.OpenRead(path))
			{
				if (f != null)
				{
					var len = f.Length;
					f.Flush();
					f.Close();
					return len;
				}
			}
		}
		return 0;
	}

	static public void WriteFile(string path, byte[] buffer, FileMode mode = FileMode.Append)
	{
		if (!string.IsNullOrEmpty(path))
		{
			try
			{
				FileStream fileStream = new FileStream(path, mode);
				fileStream.Write(buffer, 0, buffer.Length);
				fileStream.Flush();
				fileStream.Close();
				fileStream.Dispose();
			}
			catch
			{

			}
		}
	}

	static public string FormatPath(string path)
	{
		if (!string.IsNullOrEmpty(path))
			path = path.Replace(@"\\", "/").Replace(@"\", "/");
		return path;
	}

	#endregion


	#endregion

}
