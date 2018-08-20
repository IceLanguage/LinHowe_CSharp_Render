using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinHowe_CSharp_Render.Math
{
    static class MathHelp
    {
        public static float Range(float v, float min, float max)
        {
            if (v <= min)
            {
                return min;
            }
            if (v >= max)
            {
                return max;
            }
            return v;
        }

        /// <summary>
        /// 线性插值
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static float Lerp(float a, float b, float t)
        {
            if (t <= 0)
            {
                return a;
            }
            else if (t >= 1)
            {
                return b;
            }
            else
            {
                return b * t + (1 - t) * a;
            }
        }

        /// <summary>
        /// 颜色插值
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Math.Color Lerp(Math.Color a, Math.Color b, float t)
        {
            if (t <= 0)
            {
                return a;
            }
            else if (t >= 1)
            {
                return b;
            }
            else
            {
                return t * b + (1 - t) * a;
            }
        }
    }
}
