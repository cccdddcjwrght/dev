using System;

// 位运算操作
namespace SGame
{
    public class BitOperator
    {
        public static int Set(int MaskValue, int index, bool value = true)
        {
            if (index < 0 || index >= 30)
                throw new Exception("out of length");

            if (value)
                MaskValue |= 1 << index;
            else
                MaskValue &= ~(1 << index);

            return MaskValue;
        }

        public static bool Get(int MaskValue, int index)
        {
            if (index < 0 || index >= 30)
                throw new Exception("out of length");

            return (MaskValue & (1 << index)) > 0;
        }

        /// <summary>
        /// 直接获取MASK
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static int GetMask(params int[] args)
        {
            int mask = 0;
            for (int i = 0; i < args.Length; i++)
            {
                mask = Set(mask, args[i]);
            }

            return mask;
        }
    }
}