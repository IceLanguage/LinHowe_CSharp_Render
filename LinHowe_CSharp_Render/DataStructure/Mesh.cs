using System;
using LinHowe_CSharp_Render.Math;

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
        /// 是否消隐
        /// </summary>
        public bool[] Blankings { get; set; }
        /// <summary>
        /// 顶点数组
        /// </summary>
        public Point[] Vertices { get; private set; }

        /// <summary>
        /// 材质
        /// </summary>
        public Material material { get; private set; }
        public void Reset()
        {
            int len = Vertices.Length;
            for(int i = 0;i < len;++i)
            {
                Cuts[i] = false;       
                Vertices[i].Reset();
            }
            len = Blankings.Length;
            for(int i = 0;i < len;++i)
            {
                Blankings[i] = false;
            }
      
        }
        public Mesh(Vector3[] pointList, int[] indexs, Vector3[] normals,Color[] colors,Material mat,Tuple<float,float>[] uvs)
        {
            Vertices = new Point[indexs.Length];
            Cuts = new bool[indexs.Length];
            Blankings = new bool[indexs.Length / 3];
            for (int i = 0; i < indexs.Length; i++)
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
               
                Vertices[i].v_shader = Vertices[i].v_trans;
            }
            material = mat;
        }
    }
}
