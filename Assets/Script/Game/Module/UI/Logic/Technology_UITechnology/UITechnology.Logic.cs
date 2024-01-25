
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
			GTextField levelTxt =  item.asCom.GetChild("level").asTextField;
			levelTxt.text=levelIndex >= listData[index].abilitLevelList.Count-1?
				ConstDefine.MAX: listData[index].abilitLevelList[levelIndex].level.ToString();
			//说明
			GTextField desTxt =  item.asCom.GetChild("Description").asTextField;
			desTxt.text=listData[index].VaultDes;
			//当前值
			GTextField update1Txt= item.asCom.GetChild("update1").asTextField;
			update1Txt.text=string.Format("{0}%>",listData[index].abilitLevelList[levelIndex].CurLevelValue.ToString());
			//下一级值
			GTextField update2Txt= item.asCom.GetChild("update2").asTextField;
			update2Txt.text = string.Format("{0}%", listData[index].abilitLevelList[levelIndex].NextLevelValue.ToString());
			//按钮
			GButton    techBtn=item.asCom.GetChild("techBtn").asButton;
			GButton    techMaxBtn=item.asCom.GetChild("techMaxBtn").asButton;
			GTextField buyTxt= techBtn.GetChild("iconTitle").asTextField;
			buyTxt.text=listData[index].abilitLevelList[levelIndex].BuyData[1].ToString();
			Controller techController = item.asCom.GetController("state");
			
			//------------test
			if (index > 5)
			{
				techController.selectedIndex = 1;
				listData[index].IsLock = true;
				techBtn.enabled = false;
			}
			else
			{
				if (levelIndex >= listData[index].abilitLevelList.Count-1)
				{
					techController.selectedIndex = 2;
					GTextField maxTxt= techMaxBtn.GetChild("title").asTextField;
					maxTxt.text = ConstDefine.MAX;
					techMaxBtn.enabled = false;

				}
			}
			//test------------
			
			
			
			techBtn.onClick.Set(()=>
			{
				if (listData[index].IsLock == false)
				{
					OnClickTechBtn(index);
				}
				
			});
			
		
		}

		public void OnClickTechBtn(int index)
		{
			m_AbilityData.UpgradeLevel(index);
			m_view.m_techList.numItems = m_AbilityData.len; 
		}

		partial void UnInitLogic(UIContext context){

		}
	}
}
