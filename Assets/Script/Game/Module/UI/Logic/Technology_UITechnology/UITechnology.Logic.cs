
using System;
using Unity.Entities;

namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Technology;
    using GameConfigs;

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
			bgUI.GetChild("closeBg").onClick.Add(OnClickClose);
		}
		
		private void OnClickClose()
		{
			// 关闭UI
			UIModule.Instance.CloseUI(m_entity);
		}

		private void RenderListItem(int index, GObject item)
		{
			item.name = index.ToString();
			var listData = m_AbilityData.GetAbilityList();
			int levelIndex = listData[index].LevelIndex;
			//图标
			GLoader loader = item.asCom.GetChild("icon").asLoader;
			loader.SetIcon(listData[index].VaultIcon);
			//loader.url = string.Format("ui://Technology/{0}", listData[index].VaultIcon);
			//等级
			GTextField levelTxt = item.asCom.GetChild("level").asTextField;
			levelTxt.text=string.Format(UIListener.Local("ui_main_btn_upgradelevel"),listData[index].abilitLevelList[levelIndex].level.ToString());
			//说明
			GTextField desTxt =  item.asCom.GetChild("Description").asTextField;
			//当前值
			GTextField update1Txt= item.asCom.GetChild("update1").asTextField;
			//下一级值
			GTextField update2Txt= item.asCom.GetChild("update2").asTextField;

			int type =listData[index].ShowText;
			if (type == 1)
			{
				update1Txt.text=string.Format("{0}%",listData[index].abilitLevelList[levelIndex].CurLevelValue.ToString());
				update2Txt.text = string.Format("{0}%", listData[index].abilitLevelList[levelIndex].NextLevelValue.ToString());
			}
			else
			{
				
				update1Txt.text= Utils.ConvertNumberStr(listData[index].abilitLevelList[levelIndex].CurLevelValue);
				update2Txt.text = Utils.ConvertNumberStr(listData[index].abilitLevelList[levelIndex].NextLevelValue);
			}
			desTxt.text=String.Format(UIListener.Local(listData[index].VaultDes), update1Txt.text);

			GTextField unLockTxt = item.asCom.GetChild("unLock").asTextField;
			unLockTxt.text = UIListener.Local("ui_vault_unlock");

			//按钮
			GButton    techBtn=item.asCom.GetChild("techBtn").asButton;
			//GButton    techMaxBtn=item.asCom.GetChild("techMaxBtn").asButton;
			GTextField buyTxt= techBtn.GetChild("iconTitle").asTextField;
			Controller techController = item.asCom.GetController("state");
			Controller iconController = item.asCom.GetController("iconImage");
			iconController.selectedIndex = 1;
			int itemNum = 0;
			int LevelValue = 0;
			if (listData[index].IsLock)
			{
				techController.selectedIndex = 0;
				techBtn.GetController("hasIcon").selectedIndex = 0;
				buyTxt.text=listData[index].abilitLevelList[levelIndex].BuyData[1].ToString();
				itemNum = listData[index].abilitLevelList[levelIndex].BuyData[1];
				LevelValue = listData[index].abilitLevelList[levelIndex].NextLevelValue;
			}
			else
			{
				techController.selectedIndex = 1;
				if (listData[index].LockData[2] == 0)
				{
					techBtn.GetController("hasIcon").selectedIndex = 1;
					techBtn.SetTextByKey("ui_vault_free");
				}
				else 
				{
					techBtn.GetController("hasIcon").selectedIndex = 0;
					buyTxt.text = listData[index].LockData[2].ToString();
				};
				itemNum = listData[index].LockData[2];
				LevelValue = listData[index].abilitLevelList[levelIndex].CurLevelValue;
			}
			
			if (levelIndex >= listData[index].abilitLevelList.Count-1)
			{
				techController.selectedIndex = 2;
				levelTxt.text = UIListener.Local("tips_lv_max");
				//GTextField maxTxt= techMaxBtn.GetChild("title").asTextField;
				//UIListener.SetTextByKey(maxTxt,"ui_main_btn_upgrademax");
				//techMaxBtn.enabled = false;
			}

			techBtn.onClick.Set(()=>
			{
				if (PropertyManager.Instance.GetGroup(PropertyGroup.ITEM).GetNum((int)ItemID.DIAMOND) >= itemNum)
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
						LevelValue,
						listData[index].ID
					);
					EventManager.Instance.Trigger((int)GameEvent.TECH_LEVEL, listData[index].ID, listData[index].abilitLevelList[listData[index].LevelIndex].level);
					PropertyManager.Instance.GetGroup(PropertyGroup.ITEM).AddNum((int)ItemID.DIAMOND, -itemNum);
					m_view.m_techList.numItems = m_AbilityData.len; 
				}
				else
				{
					if ("shop".IsOpend())
					{
						"@error_4".Tips();
						//SGame.UIUtils.OpenUI("shopui", 15);
					}
				}
					
				
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
