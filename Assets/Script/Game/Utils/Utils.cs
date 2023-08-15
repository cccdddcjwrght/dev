using System.Collections.Generic;
using System;
using System.Linq;

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
    }
}