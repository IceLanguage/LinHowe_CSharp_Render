using LinHowe_CSharp_Render.Math;

namespace LinHowe_CSharp_Render
{
    /// <summary>
    /// 颜色
    /// </summary>
    struct Color
    {
        private float _r;
        private float _b;
        private float _g;
        public readonly static Color White = new Color(1, 1, 1);
        public readonly static Color Black = new Color(0, 0, 0);
        public Color(float r, float g, float b)
        {
            this._r = MathHelp.Range(r, 0, 1);
            this._g = MathHelp.Range(g, 0, 1);
            this._b = MathHelp.Range(b, 0, 1);
        }

        public float R
        {
            get { return MathHelp.Range(_r, 0, 1); }
            set { _r = MathHelp.Range(value, 0, 1); }
        }

        public float G
        {
            get { return MathHelp.Range(_g, 0, 1); }
            set { _g = MathHelp.Range(value, 0, 1); }
        }

        public float B
        {
            get { return MathHelp.Range(_b, 0, 1); }
            set { _b = MathHelp.Range(value, 0, 1); }
        }
        

        public static Color operator *(float a, Color b)
        {
            Color c = new Color
            {
                R = a * b.R,
                G = a * b.G,
                B = a * b.B
            };
            return c;
        }
        public static Color operator *(Color a, float b)
        {
            Color c = new Color
            {
                R = a.R * b,
                G = a.G * b,
                B = a.B * b
            };
            return c;
        }
        public static Color operator /(Color a, float b)
        {
            Color c = new Color
            {
                R = a.R / b,
                G = a.G / b,
                B = a.B / b
            };
            return c;
        }
        public static Color operator +(Color a, Color b)
        {
            Color c = new Color
            {
                R = a.R + b.R,
                G = a.G + b.G,
                B = a.B + b.B
            };
            return c;
        }

        /// <summary>
        /// 转换为系统的color
        /// </summary>
        /// <returns></returns>
        public System.Drawing.Color TransFormToSystemColor()
        {
            float r = this.R * 255;
            float g = this.G * 255;
            float b = this.B * 255;
            return System.Drawing.Color.FromArgb((int)r, (int)g, (int)b);
        }

        public static Color TransformToRenderColor(System.Drawing.Color color)
        {
            float r = color.R / 255f;
            float g = color.G / 255f;
            float b = color.B / 255f;
            return new Color(r, g, b);
        }

        /// <summary>
        /// 颜色乘法，用于颜色混合，实际叫做Modulate（调制）
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Color operator *(Color a, Color b)
        {
            Color c = new Color
            {
                R = a.R * b.R,
                G = a.G * b.G,
                B = a.B * b.B
            };
            return c;
        }
    }
}
