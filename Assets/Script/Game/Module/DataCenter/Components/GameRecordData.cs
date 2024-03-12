using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

namespace SGame
{
    /// <summary>
    /// 游戏数据统计
    /// </summary>
    [Serializable]
    public class GameRecordData 
    {
        [Serializable]
        public struct RoleData
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
            public int roleType;
        }
        
        // 顾客数量
        public int             customerNum;

        // 大厨数量
        public List<RoleData>  chefInfo;
        
        // 服务员数量
        public int             waiterNum;
        
        public void Initalize()
        {
            
        }
        
        /// <summary>
        /// 统计角色信息
        /// </summary>
        /// <param name="roleType">角色类型, 是顾客, 服务员</param>
        /// <param name="num"></param>
        /// <param name="pos"></param>
        public void RecordRole(int roleType, int num, int pos)
        {
            
        }
    }
}