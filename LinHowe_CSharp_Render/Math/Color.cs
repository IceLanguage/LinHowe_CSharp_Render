using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinHowe_CSharp_Render.Math
{
    struct Color
    {
        private float _r;
        private float _b;
        private float _g;

        public Color(float r, float g, float b)
        {
            this._r = Range(r, 0, 1);
            this._g = Range(g, 0, 1);
            this._b = Range(b, 0, 1);
        }

        public float r
        {
            get { return Range(_r, 0, 1); }
            set { _r = Range(value, 0, 1); }
        }

        public float g
        {
            get { return Range(_g, 0, 1); }
            set { _g = value; }
        }

        public float b
        {
            get { return Range(_b, 0, 1); }
            set { _b = value; }
        }
        private static float Range(float v, float min, float max)
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

        public static Color operator *(float a, Color b)
        {
            Color c = new Color();
            c.r = a * b.r;
            c.g = a * b.g;
            c.b = a * b.b;
            return c;
        }
        public static Color operator *(Color a, float b)
        {
            Color c = new Color();
            c.r = a.r * b;
            c.g = a.g * b;
            c.b = a.b * b;
            return c;
        }

        public static Color operator +(Color a, Color b)
        {
            Color c = new Color();
            c.r = a.r + b.r;
            c.g = a.g + b.g;
            c.b = a.b + b.b;
            return c;
        }
    }
}
