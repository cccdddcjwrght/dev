﻿
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.Entities;

namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Setting;
	
	public partial class UISetting
	{
		private UIContext        m_context;
		private SetData          _setData;
		private List<SetData.SetItemData> _setItemDataList;
		private GList setList;

		partial void BeforeInit(UIContext context)
		{
			m_context			= context;
			_setData = DataCenter.Instance.setData;
			_setData.InitItemDataDic();
			_setItemDataList = _setData.setItemDataList;
		}
		
		partial void InitLogic(UIContext context){
			setList = m_view.m_list;
			setList.itemRenderer = RenderListItem;
			setList.numItems = _setItemDataList.Count;
			EventManager.Instance.Reg<int>(((int)GameEvent.SETTING_UPDATE_INT), OnLanSetting);
			EventManager.Instance.Reg<string>(((int)GameEvent.SETTING_UPDATE_NAME), OnNameSetting);
			EventManager.Instance.Reg(((int)GameEvent.SETTING_UPDATE_HEAD), OnHeadSetting);

		}

		private void OnHeadSetting()
		{
			
		}

		private void OnNameSetting(string name)
		{
			m_view.m_name.m_title.text = name;
		}

		private void OnLanSetting(int val)
		{
			setList.numItems = _setItemDataList.Count;
		}

		EntityManager EntityManager
		{
			get
			{
				return m_context.gameWorld.GetEntityManager();
			}
		}
		
		void RenderListItem(int index, GObject obj)
		{
			var setItemData = _setItemDataList[index];
			var setItem = obj as UI_SetItem;
			var body = setItem.m_body;
			body.m_icon.url = string.Format("ui://Setting/{0}",setItemData.iconPath);
			body.m_title.text = UIListener.Local(setItemData.titlePath);
			Controller itemController = body.asCom.GetController("btn");
			itemController.selectedIndex = setItemData.type;
			int type = setItemData.type;
			if (type == 0)
			{
				body.m_toggle.selected = setItemData.val == 1;
				body.m_del.visible = setItemData.val == 0;
				body.m_toggle.onClick.Set(() =>
				{
					_setData.SetIntItemData(setItemData.id,body.m_toggle.selected ? 1 : 0);
					body.m_del.visible = setItemData.val == 0;
				});
			}else if (type == 1)
			{
				if (setItemData.label != null)
				{
					string title=Regex.Replace(setItemData.label,  @"\*", setItemData.val.ToString());
					body.m_nomal.m_title.text=UIListener.Local(title);
				}
				body.m_nomal.onClick.Set(() =>
				{
					OnSetItemClick(setItemData.methodType,setItemData.argsUI);
				});
			}else if (type == 2)
			{
				body.m_nomal.onClick.Set(() =>
				{
					OnSetItemClick(setItemData.methodType,setItemData.argsUI);
				});
			}
			
		}

		private void OnSetItemClick(int method,string arg)
		{
		
			if (method == 0)
			{
				
			}else if (method == 1)//打开界面
			{
				if (arg != null)
				{
					Entity openUI = UIRequest.Create(EntityManager, SGame.UIUtils.GetUI(arg));
				}
			}else if (method == 2)
			{
				
			}

		}

		partial void UnInitLogic(UIContext context){

		}
	}
}
