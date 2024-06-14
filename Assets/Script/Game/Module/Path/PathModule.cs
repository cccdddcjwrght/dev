using System.Collections;
using System.Collections.Generic;
using log4net;
using UnityEngine;
using Unity.Mathematics;

namespace SGame
{
    public class PathModule : Singleton<PathModule>
    {
        private static ILog log = LogManager.GetLogger("game.path");
        
        /// <summary>
        /// 通过距离找到在路径点上的位置
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="roads"></param>
        /// <returns>返回路径位置</returns>
        public static PathIndex FindDistanceIndex(float distance, List<Vector3> roads)
        {
            if (roads == null || roads.Count < 2)
                return new PathIndex() { Index = -1 };

            int index = 0;
            float current = 0;
            for (int i = 1; i < roads.Count; i++)
            {
                Vector3 dir = roads[i] - roads[i - 1];
                float len = dir.magnitude;
                current += len;

                if (math.distance(current, distance) < 0.001f)
                {
                    // 距离太小, 直接落在点位上 
                    return new PathIndex() { Index = i, distance = 0, targetPoint = roads[i] };
                }

                if (current >= distance)
                {
                    float moveLen = current - distance;
                    var targetPos = roads[i] + dir.normalized * moveLen;
                    return new PathIndex() { Index = i, distance = len - moveLen, targetPoint = roads[i] };
                }
            }
            
            return new PathIndex() { Index = roads.Count - 1, distance = 0, targetPoint = roads[roads.Count - 1] };
        }

        /// <summary>
        /// 获得索引的距离
        /// </summary>
        /// <param name="index"></param>
        /// <param name="roads"></param>
        /// <returns></returns>
        public static float GetDistance(int index, List<Vector3> roads)
        {
            if (index >= roads.Count)
            {
                log.Error("index out of range index=" + index + " road length=" + roads.Count);
                return 0;
            }
            
            float distance = 0;
            for (int i = 1; i <= index; i++)
            {
                distance += Vector3.Distance(roads[i], roads[i - 1]);
            }
            return distance;
        }

        /// <summary>
        /// 找到最近的Index
        /// </summary>
        /// <param name="point"></param>
        /// <param name="roads"></param>
        /// <returns>失败返回-1</returns>
        public static int FindCloseIndex(Vector3 point, List<Vector3> roads)
        {
            int findCount = -1;
            float compareDistance = float.MaxValue;
            for (int i = 0; i < roads.Count; i++)
            {
                float distance = Vector3.Distance(roads[i], point);
                if (distance < compareDistance)
                {
                    findCount = i;
                    compareDistance = distance;
                }
            }

            return findCount;
        }

        /// <summary>
        /// 获取关卡路径点信息
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static bool GetLevelPathInfo(string tag, out GameConfigs.LevelPathRowData config)
        {
            int curLevelID = DataCenter.Instance.GetUserData().scene;
            return ConfigSystem.Instance.TryGet((GameConfigs.LevelPathRowData item) => item.Id == curLevelID && item.PathName == tag, out config);
        }
    }
}