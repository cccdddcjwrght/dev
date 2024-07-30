
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Hud;
	using System.Linq;

	public partial class UIGetWorkerTips
	{
		private SGame.Worktable worktable;

		partial void InitLogic(UIContext context)
		{
			context.onUpdate += OnUpdate;
			worktable = DataCenter.MachineUtil.GetWorktables((w) => w.type == 4)?.FirstOrDefault();
			if (worktable == null)
				SGame.UIUtils.CloseUI(context.entity);
			else
			{
				EventManager.Instance.Reg<int, int>(((int)GameEvent.WORK_TABLE_UPLEVEL), OnWorktableUpLv);
				Refresh();
			}
		}

		void OnWorktableUpLv(int id, int lv)
		{
			if (id == worktable.id)
				Refresh();
		}

		void Refresh()
		{
			if (worktable.objCfg.IsValid())
			{
				//StaticDefine.G_GET_WORKER_TYPE = worktable.objLvCfg.ShowType;
				m_view.m_type.selectedIndex = StaticDefine.G_GET_WORKER_TYPE;
			}
		}

		partial void UnInitLogic(UIContext context)
		{
			context.onUpdate -= OnUpdate;
			StaticDefine.G_IN_VIEW_GET_WORKER = false;
			EventManager.Instance.UnReg<int, int>(((int)GameEvent.WORK_TABLE_UPLEVEL), OnWorktableUpLv);
		}

		void OnUpdate(UIContext context)
		{
			var pos = m_view.TransformPoint(Vector2.zero, GRoot.inst);
			var state = false;
			if (pos.x > 0 && pos.x < GRoot.inst.width && pos.y > 150 && pos.y < GRoot.inst.height)
				state = true;
			StaticDefine.G_IN_VIEW_GET_WORKER = state;
		}

	}
}
