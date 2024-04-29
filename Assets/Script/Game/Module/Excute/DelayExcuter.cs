using System;
using System.Collections.Generic;
using System.Linq;
using FairyGUI;
using SGame.UI;

namespace SGame
{
	public partial class DelayExcuter : ECSSystem<DelayExcuter>
	{
		const string C_NULL_KEY = "_";

		struct Item : IEquatable<Item>
		{
			public string ui;
			public int uiID;
			public int funcID;
			public int priority;
			public bool isui;
			public object[] args;
			public Action call;

			public bool Equals(Item other)
			{
				return other.ui != null && ui != null && other.ui == ui;
			}
		}

		private string[] C_DELAY_UI = new string[]{
			"confirmtips","rewardtips","loadingui","rewardflytips",
		};

		private List<string> C_IGNORE_UI = new List<string>() {
			"gmui","mask","guidefinger","guideui","scenedecorui","hud"
		};

		private Dictionary<string, List<Item>> m_delayQueues = new Dictionary<string, List<Item>>();


		private List<Item> m_currentDelayQueues = new List<Item>();
		private List<string> m_uiStack = new List<string>();
		private Action<string> m_waitOpen;
		private Action<string> m_waitClose;
		private string m_currentUI = null;
		private Action m_handler = null;


		protected override void OnCreate()
		{
			base.OnCreate();
			EventManager.Instance.Reg<string>(((int)GameEvent.UI_SHOW), OnUIOpen);
			EventManager.Instance.Reg<string>(((int)GameEvent.UI_HIDE), OnUIClose);
		}

		public void DelayOpen(int id, string waitui, bool addtofirst = false, Action call = null, params object[] args)
		{

			if (!string.IsNullOrEmpty(waitui) && id > 0)
			{
				var d = new Item() { funcID = id, args = args, call = call };
				InsertWaitlist(waitui, d, addtofirst);
			}

		}

		public void DelayOpen(string ui, string waitui, bool addtofirst = false, Action call = null, params object[] args)
		{

			if (!string.IsNullOrEmpty(waitui) && ui != waitui && (!string.IsNullOrEmpty(ui) || call != null))
			{
				var d = new Item() { ui = ui, args = args, call = call, uiID = ui == null || ui.StartsWith("@") ? 0 : UIUtils.GetUI(ui) };
				InsertWaitlist(waitui, d, addtofirst);
			}
		}

		public void OnlyWaitUI(string ui, Action callback, bool once = false)
		{
			if (string.IsNullOrEmpty(ui) || callback == null) return;
			void Call(string v)
			{
				if (v != ui) return;
				if (once) m_waitOpen -= Call;
				callback();
			}
			m_waitOpen += Call;
		}

		public void OnlyWaitUIClose(string ui, Action callback, bool once = false)
		{
			if (string.IsNullOrEmpty(ui) || callback == null) return;
			void Call(string v)
			{
				if (v != ui) return;
				if (once) m_waitClose -= Call;
				callback();
			}
			m_waitClose += Call;
		}

		private void InsertWaitlist(string waitui, Item item, bool addtofirst = false)
		{
			var d = item;
			if (!m_delayQueues.TryGetValue(waitui, out var list))
			{
				list = new List<Item>();
				m_delayQueues[waitui] = list;
			}
			if (list.Contains(d))
				list.Remove(d);
			var ui = d.ui;
			var priority = list.Count;
			var state = (m_currentUI == waitui || m_currentUI == null) && list.Count == 0;
			var last = list.LastOrDefault();
			d.isui = !string.IsNullOrEmpty(ui) && !ui.StartsWith("@");
			if (addtofirst)
			{
				//将优先级提高
				d.priority = -priority - 1;
				list.Insert(0, d);
			}
			else
			{
				priority = Math.Max(0, last.priority) + 1;
				var n = d.funcID != 0 ? Math.Abs(d.funcID) : !string.IsNullOrEmpty(ui) && ui.StartsWith("@") ? int.Parse(ui.Substring(startIndex: 1)) : 0;
				if (d.isui && ConfigSystem.Instance.TryGet<GameConfigs.ui_resRowData>(d.uiID, out var cfg))
					priority = cfg.Priority == 0 ? priority : cfg.Priority;
				else if (n != 0
					&& ConfigSystem.Instance.TryGet<GameConfigs.FunctionConfigRowData>(n, out var f)
					&& !string.IsNullOrEmpty(f.Ui)
					&& ConfigSystem.Instance.TryGet<GameConfigs.ui_resRowData>(UIUtils.GetUI(f.Ui), out cfg))
				{
					priority = cfg.Priority == 0 ? priority : cfg.Priority;
				}
				d.priority = priority;
				list.Add(d);
			}
			if (list.Count > 1)
				list.Sort((a, b) => a.priority.CompareTo(b.priority));

			if (!string.IsNullOrEmpty(ui))
				GameDebug.Log("<color=red>██████</color>ui queue add ui:" + ui);

			if (state && SGame.UIUtils.CheckUIIsOpen(waitui))
				OnUIOpen(waitui);
		}

		private void DelayOpen()
		{
			m_handler?.Invoke();
			var flag = true;
			UILockManager.Instance.Require("DelayOpen");
			this.Delay(() =>
			{
				UILockManager.Instance.Release("DelayOpen");
				if (flag) PopUI();
			}, 300);
			m_handler = new Action(() => flag = false);
		}

		private void PopUI()
		{
			if (m_currentUI != null && m_currentDelayQueues != null && m_currentDelayQueues.Count > 0)
			{
				m_handler = null;
				var cui = m_currentUI;
				if (Check())
				{
					var ui = m_currentDelayQueues[0];
					m_currentDelayQueues.RemoveAt(0);
					var flag = false;

					if (ui.isui)
						SGame.UIUtils.OpenUI(ui.ui, ui.args);
					else if (ui.funcID > 0)
						FunctionSystem.Instance.Goto(ui.funcID);
					else
						flag = m_currentDelayQueues.Count > 0;
					ui.call?.Invoke();
					ui.call = null;
					GameDebug.Log($"pop:{m_currentUI} - {ui.ui ?? ui.funcID.ToString()}");
					if (flag)
						DelayOpen();
				}
				else
					m_currentUI = null;
			}
		}

		private bool UpdateStack(bool add = true, string ui = null)
		{
			ui = ui ?? m_currentUI;
			var s = false;
			if (m_uiStack.Contains(ui))
			{
				m_uiStack.Remove(ui);
				s = true;
			}
			if (add)
			{
				m_uiStack.Add(ui);
				s = true;
			}
			return s;
		}

		private bool isSystemOpenUI = true;
		private void OnUIOpen(string ui)
		{
			if (C_IGNORE_UI.Contains(ui)) return;
			var list = GetDelayByUI(ui);
			m_currentUI = null;
			UpdateStack(true, ui);
			if (list != null && list.Count > 0)
			{
				m_currentDelayQueues = list;
				m_currentUI = ui;
				DelayOpen();
			}
			if (isSystemOpenUI)
				m_waitOpen?.Invoke(ui);
			isSystemOpenUI = true;
		}

		private void OnUIClose(string ui)
		{
			var state = UpdateStack(false, ui);
			var temp = m_uiStack != null && m_uiStack.Count > 0 ? m_uiStack.Last() : null;
			if (m_currentUI == ui) m_currentUI = null;
			if (temp != null && temp != m_currentUI)
			{
				isSystemOpenUI = false;
				OnUIOpen(temp);
			}
			m_waitClose?.Invoke(ui);
		}

		private List<Item> GetDelayByUI(string ui)
		{
			if (string.IsNullOrEmpty(ui)) return null;
			m_delayQueues.TryGetValue(ui, out var list);
			return list;
		}


		private bool Check()
		{
			if (C_DELAY_UI.Length > 0)
			{
				foreach (var item in C_DELAY_UI)
				{
					if (UIUtils.CheckUIIsOpen(item))
						return false;
				}
			}
			return true;
		}

	}
}
