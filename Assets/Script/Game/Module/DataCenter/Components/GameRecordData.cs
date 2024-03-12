using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

namespace SGame
{
    [Serializable]
    public struct RecordRoleData
    {
        /// <summary>
        /// 电梯位置
        /// </summary>
        public int pos;

        /// <summary>
        /// 数量
        /// </summary>
        public int Num;

        /// <summary>
        /// 角色类型
        /// </summary>
        public EnumRole roleType;
    }
    
    /// <summary>
    /// 游戏数据统计
    /// </summary>
    [Serializable]
    public class GameRecordData 
    {
        // 保存角色信息
        public List<RecordRoleData>  roleDatas = new List<RecordRoleData>();

        public bool HasRecord() { return roleDatas.Count > 0; }
        
        public void Initalize()
        {
            EventManager.Instance.Reg((int)GameEvent.PREPARE_LEVEL_ROOM, OnPerpareLeaveRoom);
        }
        
        /// <summary>
        /// 统计角色信息
        /// </summary>
        /// <param name="roleType">角色类型, 是顾客, 服务员</param>
        /// <param name="num"></param>
        /// <param name="pos"></param>
        public void RecordRole(int roleType, int num, int pos)
        {
            roleDatas.Add(new RecordRoleData(){roleType = (EnumRole)roleType, Num = num, pos = pos});
        }

        /// <summary>
        /// 按类型获取角色信息
        /// </summary>
        /// <param name="roleType"></param>
        /// <returns></returns>
        public List<RecordRoleData> GetData(EnumRole roleType)
        {
            List<RecordRoleData> ret = new List<RecordRoleData>();
            foreach (var item in roleDatas)
            {
                if (item.roleType == roleType)
                    ret.Add(item);
            }
            return ret;
        }

        /// <summary>
        /// 准备离开场景去下一关
        /// </summary>
        void OnPerpareLeaveRoom()
        {
            //Convert.ToInt32()
            roleDatas.Clear();
        }
    }
}