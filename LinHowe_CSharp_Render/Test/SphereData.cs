using LinHowe_CSharp_Render.Math;
using System;

namespace LinHowe_CSharp_Render.Test
{
    static class SphereData
    {
        /// <summary>
        /// 法线
        /// </summary>
        public static readonly Vector3[] norlmas;
        /// <summary>
        /// 三角形顶点索引 12个面
        /// </summary>
        public static readonly int[] indexs;
        /// <summary>
        /// 顶点坐标
        /// </summary>
        public static readonly Vector3[] pointList;
        /// <summary>
        /// 顶点颜色
        /// </summary>
        public static readonly Color[] vertColors;

        public static int index = 0;
        /// <summary>
        /// 材质
        /// </summary>
        public static readonly Material mat = new Material
        (
            emissive: new Color(0, 0, 0f),
            ka: 0.1f,
            diffuse: new Color(0.3f, 0.3f, 0.3f),
            specular: new Color(1, 1, 1),
            shininess: 9
        );
        /// <summary>
        /// uv坐标
        /// </summary>
        public static readonly Tuple<float, float>[] uvs;
        static SphereData()
        {
            Vector3 va = new Vector3(0.0f, 0.0f, -1.0f, 1);
            Vector3 vb = new Vector3(0.0f, 0.942809f, 0.333333f, 1);
            Vector3 vc = new Vector3(-0.816497f, -0.471405f, 0.333333f, 1);
            Vector3 vd = new Vector3(0.816497f, -0.471405f, 0.333333f, 1);
            int numTimesToSubdivide = 4;
            int len = 3 * (int)System.Math.Pow(4, numTimesToSubdivide + 1);
            
            pointList = new Vector3[len];
            norlmas = new Vector3[len];
            indexs = new int[len];
            vertColors = new Color[len];
            uvs = new Tuple<float, float>[len];
            Tetrahedron(va, vb, vc, vd, numTimesToSubdivide);
        }
        static void Tetrahedron(Vector3 a, Vector3 b, Vector3 c, Vector3 d, int n)
        {
            DivideTriangle(a, b, c, n,1,1);
            DivideTriangle(d, c, b, n,1,0);
            DivideTriangle(a, d, b, n,0,0);
            DivideTriangle(a, c, d, n,0,1);
        }
        static void DivideTriangle(Vector3 a, Vector3 b, Vector3 c, int n,float u,float v)
        {
            if (n > 0)
            {

                Vector3 ab = MathHelp.Lerp(a, b, 0.5f).Normalize();
                Vector3 ac = MathHelp.Lerp(a, c, 0.5f).Normalize();
                Vector3 bc = MathHelp.Lerp(b, c, 0.5f).Normalize();


                DivideTriangle(a, ab, ac, n - 1, u, v);
                DivideTriangle(ab, b, bc, n - 1, u, v/2);
                DivideTriangle(bc, c, ac, n - 1, u/2, v/2);
                DivideTriangle(ab, bc, ac,n - 1, u/2, v);
            }
            else
            {
                Triangle(a, b, c,u,v);
            }
        }
        static void Triangle(Vector3 a, Vector3 b, Vector3 c,float u,float v)
        {
            pointList[index] = a;
            pointList[index+1] = b;
            pointList[index+2] = c;

            norlmas[index] = a;
            norlmas[index+1] = b;
            norlmas[index+2] = c;

            indexs[index] = index;
            indexs[index+1] = index+1;
            indexs[index+2] = index+2;

            vertColors[index] = new Color(255, 255, 0);
            vertColors[index+1] = new Color(255, 255, 0);
            vertColors[index+2] = new Color(255, 255, 0);

            uvs[index] = new Tuple<float, float>(u, v);
            uvs[index+1] = new Tuple<float, float>(u/2, v);
            uvs[index+2] = new Tuple<float, float>(u, v/2);
            index += 3;
        }
    }
}
