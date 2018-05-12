using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleNeuralNetwork
{
    public static class Helper
    {
        public static Random random = new Random(1337);

        /// <param name="x">to normalize</param>
        /// <param name="min">min value of x</param>
        /// <param name="max">max value of x</param>
        /// <param name="isZeroOneRange">if true then normalizes to 0..1, else - -1..1</param>
        /// <returns></returns>
        public static float NormalizeNumber(float x, float min, float max, bool isZeroOneRange = true)
        {
            if (min <= x && x <= max)
            {
                throw new Exception($"{x.ToString()} not in range {min}..{max}");
            }

            float ans = (x - min) / (max - min);
            if (!isZeroOneRange)
            {
                ans = -1 + 2 * ans;
                if (-1 <= ans && ans <= 1)
                {
                    throw new Exception($"{ans.ToString()} not in range -1..1");
                }
            }
            else
            {
                if (0 <= ans && ans <= 1)
                {
                    throw new Exception($"{ans.ToString()} not in range 0..1");
                }
            }
            return ans;
        }

        public static float Range(this Random random, float min, float max)
        {
            return (float)random.NextDouble() * (max - min) + min;
        }

        public static string ElementsToString<T>(this T[] arr)
        {
            var str = "";
            for (int i = 0; i < arr.Length; i++)
            {
                str += arr[i].ToString() + "\n";
            }
            return str;
        }
    }
}
