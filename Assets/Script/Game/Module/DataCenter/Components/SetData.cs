using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameConfigs;
using UnityEngine;
using UnityEngine.Serialization;
using Avatar = GameConfigs.Avatar;

namespace SGame
{
    [Serializable]
    public class SetData
    {
   
        public bool isInited;

        public string name;
        
        //设置栏数据
        [Serializable]
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


        public class HeadFrameData
        {
            public int id;
            public int type;
            public string icon;
            public bool isCheck;
            public bool isLock;
        }

        public List<HeadFrameData> headDataList = new List<HeadFrameData>();
        public List<HeadFrameData> freamDataList = new List<HeadFrameData>();



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

        public void InitHeadFrameData()
        {
            if (ConfigSystem.Instance.TryGets(item => (item).Type == 1,
                    out List<AvatarRowData> headConfigs))
            {
                foreach (var item in headConfigs)
                {
                    headDataList.Add(new HeadFrameData()
                    {
                        id=item.AvatarId,
                        icon = item.Icon,
                        isCheck = item.Rank==1?true:false,
                        isLock =  item.Rank==1?true:false,
                    });
                }
                
            }else if (ConfigSystem.Instance.TryGets(item => (item).Type == 2,
                          out List<AvatarRowData> freamConfigs))
            {
                foreach (var item in freamConfigs)
                {
                    freamDataList.Add(new HeadFrameData()
                    {
                        id=item.AvatarId,
                        icon = item.Icon,
                        isCheck = item.Rank==1,
                        isLock =  item.Rank==1
                    });
                }
            }
        }
       
        public void SetHeadFrame(int type,int id,bool isLock)
        {
            if (type == 1)
            {
                headDataList[id].isLock = isLock;
            }else if(type == 2)
            {
                freamDataList[id].isLock = isLock;
            }
        }

    }
}

