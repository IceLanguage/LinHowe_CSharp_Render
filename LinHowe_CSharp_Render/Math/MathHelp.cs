
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

        public static Vector3 Lerp(Vector3 a, Vector3 b, float t)
        {
            Vector3 res  = Vector3.zero;
            res.x = Lerp(a.x, b.x, t);
            res.y = Lerp(a.y, b.y, t);
            res.z = Lerp(a.z, b.z, t);
            res.w = Lerp(a.w, b.w, t);
            return res;
        }
        /// <summary>
        /// 颜色插值
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Color Lerp(Color a, Color b, float t)
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

        public static Vertex Lerp(Vertex a,Vertex b,float t)
        {
            Vertex res = new Vertex
            {
                u = MathHelp.Lerp(a.u, b.u, t),
                v = MathHelp.Lerp(a.v, b.v, t),

                color = MathHelp.Lerp(a.color, b.color, t),

                lightingColor = MathHelp.Lerp(a.lightingColor, b.lightingColor, t),
                position = MathHelp.Lerp(a.position, b.position, t),
                normal = MathHelp.Lerp(a.normal, b.normal, t)
            };
            return res;
        }
    }
}
