using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;

namespace SGame
{
    /// <summary>
    /// 貪吃色數據, 主要保存了色的每個關節的數據 
    /// </summary>
    [GenerateAuthoringComponent]
    public class SnakeData : IComponentData
    {
        // 名字
        public string           m_name;
        
        // 蛇的身體, 每個關節額位置信息
        public List<float2>     m_paths;

        // 蛇頭
        public int               m_head;

        // 獲得貪吃色的節點縂數量
        public int               count  {  get  { return m_paths.Count; } }

        // 獲得貪吃蛇當前的位置
        public float2 position
        {
            get  {  return GetBonePosition(0); }
        }

        /// <summary>
        /// 通過蛇的節點獲得色數據索引
        /// </summary>
        /// <param name="pos">蛇的第幾個關節</param>
        /// <returns>獲得位置數據的索引</returns>
        public int GetBoneIndex(int pos)
        {
            return (m_head + pos) % m_paths.Count;
        }

        // 獲得色尾的索引
        public int tailIndex
        {
            get
            {
                return GetBoneIndex(count - 1);
            }
        }

        /// <summary>
        /// 獲取關節位置信息
        /// </summary>
        /// <param name="pos">蛇的第幾個關節</param>
        /// <returns>返回關節的位置信息</returns>
        public float2 GetBonePosition(int pos)
        {
            return m_paths[GetBoneIndex(pos)];
        }

        /// <summary>
        /// 蛇頭移動到新位置, 實際是
        /// </summary>
        /// <param name="movement"></param>
        public void Move(Vector2 movement)
        {
            // 獲取頭部位置信息, 並計算新位置
            Vector2 headPos = GetBonePosition(0);
            headPos += movement;
            
            // 將新位置寫入尾部
            int     tail    = tailIndex;
            m_paths[tail]   = headPos;
            
            // 更新蛇頭索引
            m_head = tail;
        }

        /// <summary>
        /// 添加蛇的尾部
        /// </summary>
        /// <param name="num">添加尾部數量</param>param
        public void AddTail(int num = 1)
        {
            List<float2> backup = new List<float2>(m_paths.Count);

            // 整理數據, 然後將蛇頭加到末尾
            for (int i = 0; i < count; i++)
            {
                backup.Add(GetBonePosition(i));
            }
            // 獲得最後的位置
            float2 lastPosition = GetBonePosition(count - 1);
            
            // 把數據寫回去
            m_paths.Clear();
            m_paths.AddRange(backup);
            for (int i = 0; i < num; i++)
                m_paths.Add(lastPosition);

            // 第一個節點從新置零
            m_head = 0;
        }

        public void Initalize(float2 startPos, int num)
        {
            m_paths = new List<float2>(num);
            m_head = 0;
            for (int i = 0;i  < num; i++)
                m_paths.Add(startPos);
        }

        /// <summary>
        /// 創建貪吃蛇
        /// </summary>
        /// <param name="startPos">貪吃蛇起始位置</param>
        /// <param name="num">身體數量</param>
        /// <returns>貪吃蛇數據</returns>
        public static SnakeData Create(float2 startPos, int num)
        {
            SnakeData data = new SnakeData();
            data.Initalize(startPos, num);
            return data;
        }
    }


}