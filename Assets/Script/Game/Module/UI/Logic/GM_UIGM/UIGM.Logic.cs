#if !SVR_RELEASE

namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.GM;
	using System.Collections.Generic;
	using GameConfigs;
	using System;
	using System.Linq;

	public partial class UIGM
	{
		partial void InitLogic(UIContext context)
		{
			m_view.m_excute.GetChild("title").onClick.Add(OnExcuteItemClick);
			AddCmdItems(GetCmdsByType(1));
			AddCmdItems(GetCmdsByType(2));
			AddCmdItems(GetCmdsByType(3));
			AddCmdItems(GetCmdsByType(4));
		}

		partial void UnInitLogic(UIContext context)
		{

		}


		void AddCmdItems(List<CmdConfigRowData> cmds, Action<int, object, GObject> call = null)
		{
			SGame.UIUtils.AddListItems(m_view.m_list, cmds, OnAddCmdItem, null, new Func<object, string>(OnGetRes));
		}

		string OnGetRes(object data)
		{
			if (data is CmdConfigRowData c)
			{
				if (!string.IsNullOrEmpty(c.Res)) return c.Res;
				return $"ui://GM/C{c.Type}";
			}
			return default;
		}

		void OnAddCmdItem(int index, object data, GObject gObject)
		{

			var cfg = (CmdConfigRowData)data;
			if (cfg.IsValid())
			{
				gObject.SetText(cfg.Desc.Local());
				if (cfg.Type == 1)
					gObject.onClick.Add(() => OnClick(cfg, gObject));
				else
				{
					var item = gObject.asCom;
					item.GetChildByPath("title").onClick.Add(() => OnClick(cfg, gObject));
					if (cfg.Type > 2 && !string.IsNullOrEmpty(cfg.Cfg))
					{
						Type t = Type.GetType("GameConfigs." + cfg.Cfg);
						var count = ConfigSystem.Instance.GetConfigCount(t);
						if (count > 0)
						{
							var nField = default(System.Reflection.PropertyInfo);
							var idField = default(System.Reflection.PropertyInfo);

							var select = item.GetChild("select").asComboBox;
							var ls = select.itemList;
							var vs = select.valueList;
							select.title = "Click To Select";
							ls.Clear();
							vs.Clear();

							var cfgs = ConfigSystem.Instance.GetConfigs(cfg.Cfg, (c) => true);
							for (int i = 0; i < count; i++)
							{
								var c = cfgs[i];
								if (c.IsValid())
								{
									if (idField == null) idField = GetIDField(c.GetType());
									if (nField == null) nField = GetNameField(c.GetType());
									if (idField == null || nField == null) return;

									ls.Add(nField.GetGetMethod()?.Invoke(c, Array.Empty<object>()).ToString());
									vs.Add(idField.GetGetMethod()?.Invoke(c, Array.Empty<object>()).ToString());
								}
								else
									log.Info($"不存在表名为{cfg.Cfg}的表");
							}

						}
					}
				}
			}
		}

		void OnClick(CmdConfigRowData cfg, GObject gObject)
		{
			if (gObject is GComponent item)
			{
				var sitem = item.GetChild("select").asComboBox;
				var iinput = item.GetChild("input").asCom;

				string select = default;
				if (sitem != null) select = sitem.value;
				var ipt = m_view.m_excute.m_input;
				switch (cfg.Type)
				{
					case 1:
						ipt.title = cfg.Cmd;
						break;
					case 2:
						ipt.title = string.Format("{0} {1}", cfg.Cmd, iinput.text);
						break;
					case 3:
						ipt.title = string.Format("{0} {1} {2}", cfg.Cmd, select, string.IsNullOrEmpty(iinput.text) ? "1" : iinput.text);
						break;
					case 4:
						ipt.title = string.Format("{0} {1}", cfg.Cmd, select);
						break;
				}
			}
		}

		System.Reflection.PropertyInfo GetNameField(Type type)
		{
			return type.GetProperty("name") ?? type.GetProperty("Name");
		}

		System.Reflection.PropertyInfo GetIDField(Type type)
		{
			return type.GetProperty("id")
				?? type.GetProperty("Id")
				?? type.GetProperty("ID")
				?? type.GetProperty("ItemID")
				?? type.GetProperty("ItemId");
		}


		List<GameConfigs.CmdConfigRowData> GetCmdsByType(int type)
		{
			return ConfigSystem.Instance.Finds<CmdConfigRowData>((c) => c.Type == type);
		}

		private void OnExcuteItemClick(EventContext data)
		{
			var cmd = m_view.m_excute.m_input.text;
			if (string.IsNullOrEmpty(cmd)) return;
			var ss = cmd.Split(" ", StringSplitOptions.RemoveEmptyEntries);

			var key = ss[0];
			switch (key.ToLower())
			{
				case "event":
					DoEvent(ss);
					break;
				case "item":
					DoItem(ss);
					break;
			}


		}

		private void DoItem(string[] ss)
		{
			var id = ss.Val<int>(1);
			var num = ss.Val<int>(2, 1);
			if (id > 0)
				PropertyManager.Instance.Update(1, id, num);
		}

		private void DoEvent(string[] ss)
		{


			if (ss.Length > 1)
			{
				if (int.TryParse(ss[1], out var eid))
				{
					if (eid != 0)
					{
						var args = new List<int>();
						for (int i = 1; i < ss.Length; i++)
							if (!string.IsNullOrEmpty(ss[i]) && int.TryParse(ss[i], out var r)) args.Add(r);
						try
						{
							EventManager.Instance.AsyncTrigger(eid, args.Select(i => (object)i).ToArray());
						}
						catch (Exception e)
						{
							log.Error(e.Message);
						}
					}
				}
			}

		}

	}
}

#endif