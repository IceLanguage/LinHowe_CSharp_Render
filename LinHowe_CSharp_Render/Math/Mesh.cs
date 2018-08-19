using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinHowe_CSharp_Render.Math
{
    /// <summary>
    /// 网格
    /// </summary>
    class Mesh
    {
        /// <summary>
        /// 顶点数组
        /// </summary>
        public Vertex[] Vertices { get; }

        public Mesh(Vector3[] pointList, int[] indexs, Vector3[] normals)
        {
            Vertices = new Vertex[indexs.Length];

            for (int i = 0; i < indexs.Length; i++)
            {
                int pointIndex = indexs[i];
                Vertices[i].position = pointList[pointIndex];
                Vertices[i].normal = normals[pointIndex];
            }
        }
    }
}
