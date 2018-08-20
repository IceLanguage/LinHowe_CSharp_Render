using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinHowe_CSharp_Render.Math
{
    struct Light
    {
        /// <summary>
        /// 灯光世界坐标
        /// </summary>
        public Vector3 worldPosition;
        /// <summary>
        /// 灯光颜色
        /// </summary>
        public Color lightColor;

        public Light(Vector3 worldPosition, Color lightColor)
        {
            this.worldPosition = worldPosition;
            this.lightColor = lightColor;
        }
    }
}
