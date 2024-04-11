using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SGame
{
    /// <summary>
    /// 分拆与主组装角色部件信息
    /// </summary>
    public class CharacterPartGen
    {
        public string                     Character;        // 基础角色
        public Dictionary<string, string> Values;           // 部件KEY VALUE


        /// <summary>
        /// 解析字符串
        /// </summary>
        /// <param name="part"></param>
        /// <returns></returns>
        public static CharacterPartGen ParseString(string part)
        {
            part = part.ToLower();
            string[] settings = part.Split('|');
            var currentCharacter = settings[0];
            var keyvalue = new Dictionary<string, string>();
            string value = "";

            for (int i = 1; i < settings.Length;)
            {
                string categoryName = settings[i++];
                string elementName = settings[i++];
                keyvalue.Add(categoryName, elementName);
            }

            CharacterPartGen ret = new CharacterPartGen()
            {
                Character = currentCharacter,
                Values = keyvalue
            };
            return ret;
        }

        /// <summary>
        /// 将对象转成字符串
        /// </summary>
        /// <returns></returns>
        public string ToPartString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Character);
            foreach (var item in Values)
            {
                sb.Append("|");
                sb.Append(item.Key);
                sb.Append("|");
                sb.Append(item.Value);
            }

            return sb.ToString();
        }

        /// <summary>
        /// 通过键值获取值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<string> GetValues(string key)
        {
            List<string> ret = new List<string>();
            foreach (var item in Values)
            {
                if (item.Key.StartsWith(key))
                {
                    ret.Add(item.Value);
                }
            }

            return ret;
        }

        /// <summary>
        /// 删除相关键值
        /// </summary>
        /// <param name="key"></param>
        public void RemoveDatas(string key)
        {
            List<string> keys = new List<string>();
            foreach (var item in Values)
            {
                if (item.Key.StartsWith(key))
                {
                    keys.Add(item.Key);
                }
            }

            foreach (var k in keys)
                Values.Remove(k);
        }
    }
}
