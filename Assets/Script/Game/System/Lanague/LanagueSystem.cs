using System.Collections.Generic;
using GameConfigs;
using log4net;

namespace SGame
{
    public class LanagueSystem : Singleton<LanagueSystem>
    {
        // 多语言数据
        private         Dictionary<string, string>  m_Values    = null;                                         
        private static  ILog                        log         = LogManager.GetLogger("Core.LanagueSystem");

        /// <summary>
        /// 初始化多语言系统
        /// </summary>
        /// <param name="langName">多语言名字</param>
        /// <returns></returns>
        public bool Initalize(string langName)
        {
            if (!ConfigSystem.Instance.TryGet<languageRowData>(langName, out languageRowData lang))
            {
                log.Error("Lang Not Found = " + langName);
                return false;
            }
            
            //lang.
            var tableName = lang.TableName;
            var configs = ConfigSystem.Instance.LoadConfigFromTableName<Language_CHN>(tableName);
            if (configs.IsValid() == false)
            {
                log.Error("lang file not found=" + tableName);
                return false;
            }
                
            m_Values    = new Dictionary<string, string>(configs.DatalistLength);
            for (int i = 0; i < configs.DatalistLength; i++)
            {
                var value = configs.Datalist(i);
                var lan = value.Value.Value;
                if (m_Values.TryAdd(value.Value.StringId, lan) == false)
                {
                    log.Error("Lanague Key Repeate=" + value.Value.StringId);
                }
            }

            EventManager.Instance.Trigger((int)GameEvent.LANGUAGE_CHANGE, langName);
            return true;
        }
        
        
        // 获得文本
        public string GetValue(string key)
        {
            if (m_Values == null)
            {
                log.Error("Lanauge Not Initialize!");
                return null;
            }

            string text;
            if (m_Values.TryGetValue(key, out text))
            {
                return text;
            }

            return key;
        }

        // 尝试获得文本
        public bool TryGetValue(string key, out string text)
        {
            text = key;
            if (m_Values == null)
            {
                log.Error("Lanauge Not Initialize!");
                return false;
            }
            return m_Values.TryGetValue(key, out text);
        }
    }
}