using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using SGame;
using SGame.UI.Hud;
using UnityEngine;

public class UIBankValue : MonoBehaviour
{
    public int              m_buildId;
    public  UIPanel         m_panel;
    private UI_HudGold      m_gold;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.Instance.Reg((int)GameEvent.ENTER_GAME, OnGameStart);
        EventManager.Instance.Reg<int,long,int,int>((int)GameEvent.PROPERTY_BANK, OnBankValueChnage);
    }

    void OnGameStart()
    {
        m_panel.CreateUI();
        m_panel.container.renderCamera = Camera.main;
        m_gold = m_panel.ui as UI_HudGold;
        var data = BuildingModule.Instance.GetBuildingData<BuildingBankData>(m_buildId);
        m_gold.m_gold.max = data.Value;
        m_gold.m_gold.value = data.Value;
    }

    private void OnBankValueChnage(int add,long v,int buildingEvent, int playerId)
    {
        var data = BuildingModule.Instance.GetBuildingData<BuildingBankData>(m_buildId);
		if (m_gold == null) return;
        m_gold.m_gold.max = data.Value;
        m_gold.m_gold.value = data.Value;
    }//PROPERTY_BANK				= 1001, // 银行存款或取款  (int add_gold, long new_value, int buildingId, int player_id)
}
