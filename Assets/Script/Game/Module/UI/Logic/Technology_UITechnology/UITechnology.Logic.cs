
using System;
using Unity.Entities;

namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Technology;
	
	public partial class UITechnology
	{
		private Entity  m_entity;
		partial void InitLogic(UIContext context){
			var techList = m_view.m_techList;
			var bgUI = m_view.m_techFrame;
			
			techList.itemRenderer = RenderListItem;
			techList.numItems = m_AbilityData.len; 
			m_entity = context.entity;
			bgUI.GetChild("close").asButton.onClick.Add(OnClickClose);
		}
		
		private void OnClickClose()
		{
			// 关闭UI
			UIModule.Instance.CloseUI(m_entity);
		}

		private void RenderListItem(int index, GObject item)
		{
			var listData = m_AbilityData.GetAbilityList();
			int levelIndex = listData[index].LevelIndex;
			//图标
			GLoader loader = item.asCom.GetChild("icon").asLoader;
			loader.url = string.Format("ui://Technology/{0}", listData[index].VaultIcon);
			//等级
			GTextField levelTxt = item.asCom.GetChild("level").asTextField;
			levelTxt.text=string.Format(UIListener.Local("ui_main_btn_upgradelevel"),listData[index].abilitLevelList[levelIndex].level.ToString());
			//说明
			GTextField desTxt =  item.asCom.GetChild("Description").asTextField;
			//当前值
			GTextField update1Txt= item.asCom.GetChild("update1").asTextField;
			//下一级值
			GTextField update2Txt= item.asCom.GetChild("update2").asTextField;
			int type = m_AbilityData.GetValueType(listData[index].abilitLevelList[levelIndex].BuffType);
			if (type == 1)
			{
				update1Txt.text=string.Format("{0}%",listData[index].abilitLevelList[levelIndex].CurLevelValue.ToString());
				update2Txt.text = string.Format("{0}%", listData[index].abilitLevelList[levelIndex].NextLevelValue.ToString());
			}
			else
			{
				update1Txt.text=listData[index].abilitLevelList[levelIndex].CurLevelValue.ToString();
				update2Txt.text = listData[index].abilitLevelList[levelIndex].NextLevelValue.ToString();
			}
			desTxt.text=String.Format(UIListener.Local(listData[index].VaultDes), update1Txt.text);
			//按钮
			GButton    techBtn=item.asCom.GetChild("techBtn").asButton;
			GButton    techMaxBtn=item.asCom.GetChild("techMaxBtn").asButton;
			GTextField buyTxt= techBtn.GetChild("iconTitle").asTextField;
			Controller techController = item.asCom.GetController("state");
			Controller iconController = item.asCom.GetController("iconImage");
			iconController.selectedIndex = 1;
			int itemNum = 0; 
			if (listData[index].IsLock)
			{
				techController.selectedIndex = 0;
				buyTxt.text=listData[index].abilitLevelList[levelIndex].BuyData[1].ToString();
				itemNum = listData[index].abilitLevelList[levelIndex].BuyData[1];
			}
			else
			{
				techController.selectedIndex = 1;
				buyTxt.text=listData[index].LockData[2].ToString();
				itemNum = listData[index].LockData[2];
			}
			
			if (levelIndex >= listData[index].abilitLevelList.Count-1)
			{
				techController.selectedIndex = 2;
				GTextField maxTxt= techMaxBtn.GetChild("title").asTextField;
				UIListener.SetTextByKey(maxTxt,"ui_main_btn_upgrademax");
				techMaxBtn.enabled = false;
			}

			techBtn.onClick.Set(()=>
			{
		
				if (listData[index].IsLock == false)
				{
					listData[index].IsLock = true;
				}
				else
				{
					m_AbilityData.UpgradeLevel(index);
				}
				OnClickTechBtn(
					listData[index].abilitLevelList[levelIndex].BuffType,
					listData[index].abilitLevelList[levelIndex].NextLevelValue,
					listData[index].ID
				);
				
				
				PropertyManager.Instance.GetGroup(PropertyGroup.ITEM).AddNum((int)ItemID.DIAMOND, -itemNum);
				m_view.m_techList.numItems = m_AbilityData.len; 
			});
			
		
		}

		/// <summary>
		/// 点击触发Buff
		/// </summary>
		/// <param name="index"></param>
		/// <param name="buffID"></param>
		/// <param name="buffValue"></param>
		/// <param name="from"></param>
		public void OnClickTechBtn(int buffID,int buffValue,int from)
		{
			EventManager.Instance.Trigger(((int)GameEvent.BUFF_TRIGGER), new BuffData(buffID, buffValue) { from = from });
		}

		partial void UnInitLogic(UIContext context){

		}
	}
}
