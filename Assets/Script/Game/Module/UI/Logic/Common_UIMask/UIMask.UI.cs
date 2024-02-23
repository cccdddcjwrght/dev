using System.Collections.Generic;
using Unity.Entities;

//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
/*
 * 1. 配置里面的mask 标签
 * 2. mask 事件处理
 * context.window.AddEventListener("OnMaskClick", () =>
			{
				log.Info("UITechnology OnMaskClick");
			});
 */
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Common;
	
	public partial class UIMask
	{
		private int __id;
		private EventHandleContainer	m_eventHandler;
		private int						m_maskCount;
		private Dictionary<Entity, int> m_ordering;
		private FairyWindow				m_window;
		private Entity					m_currentUI;

		partial void InitUI(UIContext context){
			__id = context.configID;
			m_window = context.window;

			// 默认先隐藏
			m_view.visible = false;
			m_maskCount = 0;
			m_eventHandler = new EventHandleContainer();
			m_eventHandler += EventManager.Instance.Reg<UIContext>((int)GameEvent.ON_UI_MASK_SHOW, OnMaskUIShow);
			m_eventHandler += EventManager.Instance.Reg<UIContext>((int)GameEvent.ON_UI_MASK_HIDE, OnMaskUIHide);
			m_ordering = new Dictionary<Entity, int>();
			
			m_view.m_bg.onClick.Add(OnClick);
		}
		
		partial void UnInitUI(UIContext context){
			m_eventHandler.Close();
		}

		void OnClick()
		{
			if (m_currentUI != Entity.Null)
			{
				SGame.UIUtils.TriggerUIEvent(m_currentUI, "OnMaskClick", null);
			}
		}

		Entity GetLastUI()
		{
			int order = -1;
			Entity e = Entity.Null;
			foreach (var item in m_ordering)
			{
				if (item.Value > order)
				{
					order = item.Value;
					e = item.Key;
				}
			}

			return e;
		}

		void OnMaskUIShow(UIContext otherUI)
		{
			if (!m_ordering.TryAdd(otherUI.entity, otherUI.window.sortingOrder))
			{
				log.Error("mutil entity window=" + otherUI.window.uiname);
				return;
			}

			m_maskCount++;
			UpdateMask();
		}

		void OnMaskUIHide(UIContext otherUI)
		{
			if (!m_ordering.Remove(otherUI.entity))
			{
				return;
			}
			
			m_maskCount--;
			UpdateMask();
		}

		void UpdateMask()
		{
			m_currentUI = GetLastUI();
			if (m_currentUI != Entity.Null)
			{
				m_window.sortingOrder = m_ordering[m_currentUI] - 1;
			}
			m_view.visible = m_maskCount > 0;
		}
	}
}
