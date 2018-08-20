using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinHowe_CSharp_Render.Math
{
    /// <summary>
    /// 颜色
    /// </summary>
    struct Color
    {
        private float _r;
        private float _b;
        private float _g;

        public Color(float r, float g, float b)
        {
            this._r = MathHelp.Range(r, 0, 1);
            this._g = MathHelp.Range(g, 0, 1);
            this._b = MathHelp.Range(b, 0, 1);
        }

        public float r
        {
            get { return MathHelp.Range(_r, 0, 1); }
            set { _r = MathHelp.Range(value, 0, 1); }
        }

        public float g
        {
            get { return MathHelp.Range(_g, 0, 1); }
            set { _g = MathHelp.Range(value, 0, 1); }
        }

        public float b
        {
            get { return MathHelp.Range(_b, 0, 1); }
            set { _b = MathHelp.Range(value, 0, 1); }
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
