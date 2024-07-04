using SGame;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TestExcute : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    [ContextMenu("Excute")]
    public void Excute() 
    {
        //List<ItemData.Value> m_DropItem = new List<ItemData.Value>();
        //m_DropItem.Add(new ItemData.Value()
        //{
        //    id = 2001,
        //    num = 1
        //});
        //SGame.UIUtils.OpenUI("frament", m_DropItem);

        //Debug.Log(DataCenter.Instance.accountData.playerID);
        //var ws = DataCenter.MachineUtil.GetWorktables((w) => !w.isTable);
        //if (ws?.Count > 0) ws.ForEach(w => w.lv = w.maxlv);
        //UIUtils.CloseAllUI("mainui", "flight", "lockred", "SystemTip",
        //    "Redpoint", "ordertip", "progress", "FoodTip");
        //GuideManager.Instance.StartGuide(2);
        SGame.UIUtils.OpenUI("hotfood");
    }
}
