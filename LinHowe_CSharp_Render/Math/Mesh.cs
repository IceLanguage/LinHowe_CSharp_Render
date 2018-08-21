﻿using System;
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
        /// 是否裁剪
        /// </summary>
        public bool[] Cuts { get; set; }
        /// <summary>
        /// 顶点数组
        /// </summary>
        public Vertex2[] Vertices { get; private set; }

        /// <summary>
        /// 材质
        /// </summary>
        public Material material { get; private set; }
        public Mesh(Vector3[] pointList, int[] indexs, Vector3[] normals,Color[] colors,Material mat)
        {
            Vertices = new Vertex2[indexs.Length];
            Cuts = new bool[indexs.Length];
            for (int i = 0; i < indexs.Length; i++)
            {
                int pointIndex = indexs[i];
                Vertices[i] = new Vertex2();
                Vertices[i].v = new Vertex(pointList[pointIndex], normals[i], colors[pointIndex]);
                Vertices[i].save = Vertices[i].v;
            }
            material = mat;
        }
    }
}
