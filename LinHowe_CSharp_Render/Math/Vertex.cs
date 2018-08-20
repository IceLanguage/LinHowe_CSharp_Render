using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinHowe_CSharp_Render.Math
{
    /// <summary>
    /// 顶点信息
    /// </summary>
    struct Vertex
    {
        /// <summary>
        /// 1/z，用于顶点信息的透视校正
        /// </summary>
        public float onePerZ;

        ///// <summary>
        ///// 纹理坐标
        ///// </summary>
        //public float u;
        //public float v;

        //位置
        public Vector3 position;

        //法线
        public Vector3 normal;

        //颜色
        public Color color;

        /// <summary>
        /// 光照颜色
        /// </summary>
        public Color lightingColor;

        public Vertex(Vector3 point, Vector3 normal, Color color)
        {
            this.position = point;
            this.normal = normal;
            onePerZ = this.position.w = 1;
            this.color = color;
            this.lightingColor = Color.White;
        }
    }

    /// <summary>
    /// 双顶点数据
    /// </summary>
    struct Vertex2
    {
        public Vertex v;
        public Vertex save;
    }
}
