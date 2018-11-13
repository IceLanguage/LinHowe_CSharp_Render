using System;
using LinHowe_CSharp_Render.Math;

namespace LinHowe_CSharp_Render
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

        /// <summary>
        /// 纹理坐标
        /// </summary>
        public float u;
        public float v;

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

        public Vertex(Vector3 point, Vector3 normal, Color color,Tuple<float,float> uv)
        {
            this.position = point;
            this.normal = normal;
            onePerZ = this.position.w = 1;
            this.color = color;
            this.lightingColor = Color.Black;
            u = uv.Item1;
            v = uv.Item2;
        }
    }

    /// <summary>
    /// 点数据
    /// </summary>
    class Point
    {
        public readonly Vertex m_vertex;

        /// <summary>
        /// 透明度
        /// </summary>
        public float alpha = 1;

        /// <summary>
        /// 用于差值和坐标变换
        /// </summary>
        public Vertex v_trans;

        /// <summary>
        /// 用于顶点着色器和像素着色器的数据传输
        /// </summary>
        public Vertex v_shader;

        public Point(Vertex v)
        {
            m_vertex = v_shader = v_trans = v;
        }
        public Point()
        {

        }
        public void Reset()
        {
            v_shader = v_trans = m_vertex;
        }
    }
}
