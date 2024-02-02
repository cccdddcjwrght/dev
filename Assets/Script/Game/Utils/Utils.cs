using System.Collections.Generic;
using System;
using System.Linq;
using GameConfigs;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace SGame
{
    public partial class Utils
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

        public static Entity Vector2Entity(Vector2Int v)
        {
            return new Entity { Index = v.x, Version = v.y };
        }

        public static Vector2Int Entity2Vector(Entity e)
        {
            return new Vector2Int(e.Index, e.Version);
        }

        static public Dictionary<string, string> IniParser(params string[] lines)
        {
            if (lines != null && lines.Length > 0)
            {
				var datas = lines.Where(Line => !string.IsNullOrEmpty(Line) && !Line.StartsWith("#") && Line.IndexOf('=') > 0)
                    .Select(Line => Line.Split(new char[] { '=' }, 2, StringSplitOptions.RemoveEmptyEntries))
                    .Where(ls => ls.Length > 1)
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
    
		//把数值转成专用字符串表示
		public static string ConvertNumberStr(double number) {

			if (number > 1000)
			{
				var s = number.ToString();
			}
			return number.ToString();
		}


        /// <summary>
        /// 通过角色类型获得目标类型
        /// </summary>
        /// <param name="roleType"></param>
        /// <returns></returns>
        public static EnumTarget GetTargetFromRoleType(int roleType)
        {
            RoleType r = (RoleType)roleType;
            switch (roleType)
            {
                case (int)RoleType.CHEF:
                    return EnumTarget.Cook;
                case (int)RoleType.WAITER:
                    return EnumTarget.Waiter;
                case (int)RoleType.CUSTOMER:
                case (int)RoleType.CAR:
                    return EnumTarget.Customer;
            }

            return EnumTarget.Player;
        }

        /// <summary>
        /// 通过角色类型获得位置标签
        /// </summary>
        /// <param name="roleType"></param>
        /// <returns></returns>
        public static string GetMapTagFromRoleType(int roleType)
        {
            switch (roleType)
            {
                case (int)RoleType.CHEF:
                case (int)RoleType.PLAYER:
                    return "born_0";
                case (int)RoleType.WAITER:
                    return "born_1";
                case (int)RoleType.CUSTOMER:
                    return "born_3";
            }

            return "";
        }

        /// <summary>
        /// 添加子节点
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="child"></param>
        public static void AddEntityChild(Entity parent, Entity child)
        {
            var EntityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

            // 父节点设置
            DynamicBuffer<Child> childBuffer;
            if (!EntityManager.HasComponent<Child>(parent))
            {
                childBuffer = EntityManager.AddBuffer<Child>(parent);
            }
            else
            {
                childBuffer = EntityManager.GetBuffer<Child>(parent);
            }
            childBuffer.Add(new Child() { Value = child });

            // 关联子节点, 必须的又LocalToParent
            if (!EntityManager.HasComponent<Parent>(child))
            {
                EntityManager.AddComponent<Parent>(child);
            }
            EntityManager.SetComponentData(child, new Parent() { Value = parent });
            if (EntityManager.HasComponent<LocalToWorld>(child) && !EntityManager.HasComponent<LocalToParent>(child))
            {
                EntityManager.AddComponent<LocalToParent>(child);
            }
        }

        /// <summary>
        /// 删除子节点
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="Child"></param>
        public static void RemoveEntityChild(Entity parent, Entity child)
        {
            var EntityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            
            // 删除父节点中的子节点
            if (EntityManager.HasComponent<Child>(parent))
            {
                var childBuffer = EntityManager.AddBuffer<Child>(parent);
                for (int i = 0; i < childBuffer.Length; i++)
                {
                    if (childBuffer[i].Value == child)
                    {
                        childBuffer.RemoveAtSwapBack(i);
                        break;
                    }
                }
            }

            if (EntityManager.HasComponent<Parent>(child))
            {
                // 防止经常删除添加, 这里直接使用赋值
                EntityManager.RemoveComponent<Parent>(child);
            }

            if (EntityManager.HasComponent<LocalToParent>(child))
            {
                EntityManager.RemoveComponent<LocalToParent>(child);
            }
        }
    }
}