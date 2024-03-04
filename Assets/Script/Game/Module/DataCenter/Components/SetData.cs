using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameConfigs;
using UnityEngine;
using UnityEngine.Serialization;

namespace SGame
{
    public class SetData
    {
        [System.NonSerialized]
        public bool isInited;
        //设置栏数据
        public class SetItemData
        {
            public string id;
            public int sort;
            public string iconPath;
            public string titlePath;
            public int type;
            public int methodType;
            public string argsUI;
            public string label;//字符串
            public int val;//默认选项
        }

        public List<SetItemData> setItemDataList = new List<SetItemData>();

        /// <summary>
        /// 初始化设置表格内容
        /// </summary>
        public void InitItemDataDic()
        {
            if (isInited)
            {
                return;
            }
            setItemDataList?.Clear();
            var settingListConfig = ConfigSystem.Instance.LoadConfig<GameConfigs.SettingConfig>(); //设置列表
            var len = settingListConfig.DatalistLength;
            for (int i = 0; i < len; i++)
            {
                var rowData = settingListConfig.Datalist(i);
                setItemDataList.Add(new SetItemData()
                {
                    id=rowData.Value.Id,
                    sort=rowData.Value.Sort,
                    iconPath = rowData.Value.Icon,
                    titlePath= rowData.Value.Title,
                    type= rowData.Value.Type,
                    methodType=rowData.Value.Method,
                    argsUI=rowData.Value.Args,
                    label=rowData.Value.Label,
                    val=rowData.Value.Val
                    
                });
            }

            isInited = true;
        }

        
        public int GetIntItemData(string id)
        {
            SetItemData targetItem = setItemDataList.FirstOrDefault(item => item.id == id);
            return targetItem.val;
        }
        
        public void SetIntItemData(string id,int val)
        {
            SetItemData targetItem = setItemDataList.FirstOrDefault(item => item.id == id);
            targetItem.val = val;
            EventManager.Instance.Trigger(((int)GameEvent.SETTING_UPDATE_INT), id,val);
        }
        
        
        public SetItemData GetItemDataIndex(int index)
        {
            if (setItemDataList[index] == null)
            {
                return null;
            }
            return setItemDataList[index];
        }
       
      
        
    }
}

