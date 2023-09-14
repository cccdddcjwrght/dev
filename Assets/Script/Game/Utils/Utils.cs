using System.Collections.Generic;
using System;
using System.Linq;
using GameConfigs;
using Unity.Entities;

namespace SGame
{
    public class Utils
    {
        static public Dictionary<string, string> IniParser(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                if (System.IO.File.Exists(path))
                {
                    return IniParser(System.IO.File.ReadAllLines(path));
                }
            }
            return null;
        }

        static public Dictionary<string, string> IniParser(params string[] lines)
        {
            if (lines != null && lines.Length > 0)
            {
                var datas = lines.Where(Line => !string.IsNullOrEmpty(Line) && !Line.StartsWith("#") && Line.IndexOf('=') > 0)
                    .Select(Line => Line.Split(new char[] { '=' }, 2, StringSplitOptions.RemoveEmptyEntries))
                    .Where(Lines => lines.Length > 1)
                    .ToDictionary(Parts => Parts[0].Trim(), Parts => Parts.Length > 1 ? Parts[1].Trim() : null);
                return datas;
            }
            return null;
        }
        
        static public Dictionary<string, string> IniParserBySplit(string text, string splitChar)
        {
            if (!string.IsNullOrEmpty(text))
            {
                return IniParser(text.Split(new string[] { splitChar }, StringSplitOptions.RemoveEmptyEntries));
            }
            return null;
        }

        public static long Clamp(long value, long min, long max)
        {
            if (value >= max)
                value = max;

            if (value <= min)
                value = min;

            return value;
        }

        /// <summary>
        /// 时间倒计时
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string TimeFormat(float time)
        {
            if (time <= 0)
                time = 0;
            double t = time;
            t *= TimeSpan.TicksPerSecond;
            DateTime dt = new DateTime((long)t);
            if (dt.Hour > 0)
                return dt.ToString("HH:mm:ss");
            return dt.ToString("mm:ss");
        }

        /// <summary>
        /// 获得场景中数据单列
        /// </summary>
        /// <param name="entityManager"></param>
        /// <param name="data"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool TryGetSingletonData<T>(EntityManager entityManager, out T data) where T : struct, IComponentData
        {
            data = default;
            EntityQuery query = entityManager.CreateEntityQuery(typeof(T));
            if (query.CalculateEntityCount() == 0)
                return false;

            data = query.GetSingleton<T>();
            return true;
        }
        
        /// <summary>
        /// 获得场景中数据单列
        /// </summary>
        /// <param name="entityManager"></param>
        /// <param name="data"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool TryGetSingletonObject<T>(EntityManager entityManager, out T data) where T : class, IComponentData
        {
            data = default;
            EntityQuery query = entityManager.CreateEntityQuery(typeof(T));
            if (query.CalculateEntityCount() == 0)
                return false;

            data = query.GetSingleton<T>();
            return true;
        }

        // 根据等级获取 building event id
        public static int GetBuildingEventId(int buildingId, int level)
        {
            if (!ConfigSystem.Instance.TryGet(buildingId, out Event_BuildRowData buildData))
            {
                return -1;
            }

            if (level <= 0 || level > buildData.BuildIdLength)
            {
                return -1;
            }

            return buildData.BuildId(level - 1);
        }
        
        /// <summary>
        /// 判断玩家是否正在移动
        /// </summary>
        /// <returns></returns>
        public static bool PlayerIsMoving(EntityManager mgr)
        {
            var userData = DataCenter.Instance.GetUserData();

            if (mgr.Exists(userData.player) == false)
                return false;

            CharacterMover mover = mgr.GetComponentObject<CharacterMover>(userData.player);
            if (mover == null)
                return false;

            return !mover.isFinish;
        }
        
        /// <summary>
        /// 激活或禁止ENTITY
        /// </summary>
        /// <param name="mgr"></param>
        /// <param name="e"></param>
        /// <param name="enable"></param>
        /// <returns></returns>
        public static bool EnableEntity(EntityManager mgr, Entity e, bool enable)
        {
            if (!mgr.Exists(e))
            {
                return false;
            }

            if (enable && mgr.HasComponent<Disabled>(e))
            {
                mgr.RemoveComponent<Disabled>(e);
                return true;
            }

            if (!enable && !mgr.HasComponent<Disabled>(e))
            {
                mgr.AddComponent<Disabled>(e);
                return true;
            }

            return false;
        }
    }
}