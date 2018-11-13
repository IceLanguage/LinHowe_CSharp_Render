using System;
using System.Drawing;
using LinHowe_CSharp_Render.Math;
using System.Collections;

namespace LinHowe_CSharp_Render
{
    /// <summary>
    /// 网格
    /// </summary>
    class Mesh
    {
        /// <summary>
        /// 是否裁剪 
        /// </summary>
        public bool[] Cuts { get; set; }

        /// <summary>
        /// 是否剔除
        /// </summary>
        public bool CullFlag = false;

        /// <summary>
        /// 是否深度写入
        /// </summary>
        public bool ZWrite = true;

        /// <summary>
        /// 是否进行透明度混合
        /// </summary>
        public bool flagBlendAlpha = false;
        /// <summary>
        /// 顶点数组
        /// </summary>
        public Point[] Vertices { get; private set; }

        /// <summary>
        /// 材质
        /// </summary>
        public Material Mat { get; private set; }
        /// <summary>
        /// 纹理
        /// </summary>
        public Bitmap Texture { get;set; }

        public bool IsRenderTexture = false;
        public void Reset()
        {
            CullFlag = false;

            int len = Vertices.Length;
            Array.Clear(Cuts, 0, len);

            for (int i = 0;i < len;++i)
            {    
                Vertices[i].Reset();
            }
      
        }

        public void SetAlpha(float a)
        {
            foreach (Point p in Vertices)
                p.alpha = a;
        }
        public Mesh(Vector3[] pointList, int[] indexs, Vector3[] normals,Color[] colors,Material mat,Tuple<float,float>[] uvs)
        {
            int len = indexs.Length;
            Vertices = new Point[len];
            Cuts = new bool[len];
            for (int i = 0; i < len; i++)
            {
                int pointIndex = indexs[i];
                Vertices[i] = new Point
                    (
                        new Vertex
                        (
                            pointList[pointIndex],
                            normals[i], 
                            colors[pointIndex], 
                            uvs[i]
                        )
                    );
            }
            Mat = mat;
        }
    }
}
