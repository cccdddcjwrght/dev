using System;
using System.Collections;
using System.Xml;
using GameConfigs;
using SGame.UI;
using Unity.Entities;

namespace SGame
{
	public partial class FunctionSystem : Singleton<FunctionSystem>
	{
		public object[] args;

		public bool IsOpened(int id, bool enableTips = false, string tips = null)
		{
			return IsOpened(id, out _, enableTips, tips);
		}

		public bool IsOpened(string id, bool enableTips = false, string tips = null)
		{
			if (!string.IsNullOrEmpty(id))
			{
				if (ConfigSystem.Instance.TryGet<FunctionConfigRowData>(id, out var cfg))
				{
					if (cfg.IsValid())
						return IsOpened(cfg.Id, enableTips, tips);
					else if (enableTips)
						TipsDontOpen(id);
				}
			}
			return false;
		}

		public bool Goto(int id, bool enableTips = true, string tips = null)
		{
			if (IsOpened(id, out var cfg, enableTips, tips))
			{
				if (!string.IsNullOrEmpty(cfg.Ui))
				{
					//网络要求
					/*if (cfg.Net != 0)
					{
						if (ReconnectSystem.Instance.Reconnect((s) =>
						{
							if (s)
								Goto(id, enableTips, tips);
							else
							{
								switch (cfg.Net)
								{
									case 1:
										TipsSystem.Instance.ConfirmTips(
											 string.IsNullOrEmpty(cfg.Nettips) ? "@ui_network_3" : "@" + cfg.Nettips,
											 null,
											 "@ui_network_1", null,
											 "@ui_network_2");
										break;
								}
							}
						}, false))
							return false;
					}*/

					if (cfg.Ui.StartsWith("@"))
					{
						if (int.TryParse(cfg.Ui.Substring(1), out var eid))
							EventManager.Instance.Trigger(eid, cfg.Id);
					}
					else
						SGame.UIUtils.OpenUI(cfg.Ui);
				}
				else
					GameDebug.LogError($"功能系统配置没有给功能{cfg.Name}配置界面");
				return true;
			}
			return false;
		}

		public bool Goto(string uniqid, bool enableTips = true, string tips = null)
		{
			if (!string.IsNullOrEmpty(uniqid))
			{
				var cfg = FindFunction(uniqid);
				if (cfg.ByteBuffer != null && cfg.Id > 0)
					return Goto(cfg.Id, enableTips, tips);
				else if (enableTips)
					TipsDontOpen(uniqid);
			}
			return false;
		}

		public bool Has(string uniqid)
		{
			var cfg = FindFunction(uniqid);
			return cfg.IsValid();
		}

		public void SetArgs(params object[] args)
		{
			this.args = args;
		}

		private bool IsOpened(int id, out GameConfigs.FunctionConfigRowData cfg, bool enableTips = false, string tips = null)
		{
			cfg = default;
			if (id > 0)
			{
				cfg = FindFunction(id);

				if (cfg.ByteBuffer == null || cfg.Id == 0 || cfg.OpenType < 0)
				{
					if (enableTips)
						TipsDontOpen(id);
					return false;
				}
				var ret = true;

				if (enableTips && string.IsNullOrEmpty(tips) && !string.IsNullOrEmpty(cfg.Tips))
					tips = cfg.Tips.AutoLocal();

				var cv = Math.Abs(cfg.FixConditionVal);
				if (cv > 0)
				{
					//工作台是否解锁
					if (ConfigSystem.Instance.TryGet<RoomMachineRowData>(cv, out var machine))
					{
						var room = DataCenter.Instance.roomData.current.id;
						if (room < machine.Scene || (room == machine.Scene && DataCenter.MachineUtil.IsActived(cv)) )
						{
							ret = false;
							if (enableTips)
								tips = tips ?? "ui_system_dont_open".AutoLocal();
						}
					}
				}

				if (ret && cfg.OpenType > 0)
				{
					switch (cfg.OpenType)
					{

						case 5://关卡点位数量
							if( DataCenter.Instance.roomData.current.worktableCount < cfg.OpenVal(0))
							{
								ret = false;
								tips = "ui_machine_enable_not_enough";
							}
							break;
					}
				}


				if (!ret && enableTips)
					tips.Tips();

				return ret;
			}
			return false;
		}

		static GameConfigs.FunctionConfigRowData FindFunction(string key)
		{
			return ConfigSystem.Instance.Find<GameConfigs.FunctionConfigRowData>((c) =>
			{

				var f = (FunctionConfigRowData)c;
				if (f.Id > 0 && f.Uniqid == key) return true;
				return false;

			});
		}

		static GameConfigs.FunctionConfigRowData FindFunction(int id)
		{
			ConfigSystem.Instance.TryGet<GameConfigs.FunctionConfigRowData>(id, out var d);
			return d;
		}

		static private void TipsDontOpen(object val)
		{
			UIListener.LocalFormat("ui_system_dont_open", val).Tips();
		}

	}

	public static class FunctionSystemExtend
	{
		static public bool IsOpend(this string key, bool showtips = true, string tips = default) => FunctionSystem.Instance.IsOpened(key, showtips, tips);

		static public bool IsOpend(this int key, bool showtips = true, string tips = default) => FunctionSystem.Instance.IsOpened(key, showtips, tips);

		static public bool Goto(this string key, bool showtips = true, string tips = default) => FunctionSystem.Instance.Goto(key, showtips, tips);

		static public bool Goto(this int key, bool showtips = true, string tips = default) => FunctionSystem.Instance.Goto(key, showtips, tips);

	}
}