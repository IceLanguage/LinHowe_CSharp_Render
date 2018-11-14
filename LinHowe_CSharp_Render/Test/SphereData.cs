using LinHowe_CSharp_Render.Math;
using System;

namespace LinHowe_CSharp_Render.Test
{
    /// <summary>
    /// 球体网格数据
    /// </summary>
    static class SphereData
    {
        /// <summary>
        /// 法线
        /// </summary>
        public static readonly Vector3[] normals;
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
            ka: 0.3f,
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
            normals = new Vector3[len];
            indexs = new int[len];
            vertColors = new Color[len];
            uvs = new Tuple<float, float>[len];
            Tetrahedron(va, vb, vc, vd, numTimesToSubdivide);

        }
        
        
        static void Tetrahedron(Vector3 a, Vector3 b, Vector3 c, Vector3 d, int n)
        {
            DivideTriangle(a, b, c, n);
            DivideTriangle(d, c, b, n);
            DivideTriangle(a, d, b, n);
            DivideTriangle(a, c, d, n);
        }
        static void DivideTriangle(Vector3 a, Vector3 b, Vector3 c, int n)
        {
            if (n > 0)
            {

                Vector3 ab = MathHelp.Lerp(a, b, 0.5f).Normalize();
                Vector3 ac = MathHelp.Lerp(a, c, 0.5f).Normalize();
                Vector3 bc = MathHelp.Lerp(b, c, 0.5f).Normalize();


                DivideTriangle(a, ab, ac, n - 1);
                DivideTriangle(ab, b, bc, n - 1);
                DivideTriangle(bc, c, ac, n - 1);
                DivideTriangle(ab, bc, ac,n - 1);
            }
            else
            {
                Triangle(a, b, c);
            }
        }
        static void Triangle(Vector3 a, Vector3 b, Vector3 c)
        {
            a = a.Normalize();
            b = b.Normalize();
            c = c.Normalize();

            pointList[index] = a;
            pointList[index + 1] = b;
            pointList[index + 2] = c;

            normals[index] = a;
            normals[index + 1] = b;
            normals[index + 2] = c;

            indexs[index] = index;
            indexs[index + 1] = index + 1;
            indexs[index + 2] = index + 2;

            vertColors[index] = new Color(255, 0, 0);
            vertColors[index + 1] = new Color(255, 0, 0);
            vertColors[index + 2] = new Color(255, 0, 0);

            
            uvs[index] = GetUV(a);
            uvs[index + 1] = GetUV(b);
            uvs[index + 2] = GetUV(c);
            index += 3;
        }
        //static readonly float TWOPI = (float)(System.Math.PI * 2);
        private static Tuple<float,float> GetUV(Vector3 normal)
        {
            return new Tuple<float, float>(0,0);
        }
        //static void DrawSphere2()
        //{
        //    int num = 20;
        //    int len1 = (num + 1) * (num + 1);
        //    pointList = new Vector3[len1];
        //    vertColors = new Color[len1];

        //    float TWOPI = (float)(System.Math.PI * 2);
        //    //float HALFPI = (float)(System.Math.PI / 2);
        //    for (int i = 0; i <= num; ++i)
        //    {
        //        float theta1 = i * TWOPI / num;
        //        for (int j = 0; j <= num; ++j)
        //        {
        //            float theta2 = j * TWOPI / num;
        //            float ex = (float)(System.Math.Cos(theta1) * System.Math.Cos(theta2));
        //            float ey = (float)System.Math.Sin(theta1);
        //            float ez = (float)(System.Math.Cos(theta1) * System.Math.Sin(theta2));
        //            index = i * num + j;
        //            pointList[index] = new Vector3(ex, ey, ez);//.Normalize();
        //            vertColors[index] = new Color(255, 0, 0);
        //        }
        //    }
        //    int len2 = (num) * (num) * 6;
        //    uvs = new Tuple<float, float>[len2];
        //    normals = new Vector3[len2];
        //    indexs = new int[len2];
        //    for (int i = 0; i < num; ++i)
        //    {
        //        for (int j = 0; j < num; ++j)
        //        {
        //            index = (i * (num) + j) * 6;
        //            indexs[index] = i * (num + 1) + j;
        //            indexs[index + 1] = (i + 1) * (num + 1) + j;
        //            indexs[index + 2] = i * (num + 1) + j + 1;
        //            indexs[index + 3] = i * (num + 1) + j + 1;
        //            indexs[index + 4] = ((i + 1) * (num + 1) + j);
        //            indexs[index + 5] = (i + 1) * (num + 1) + j + 1;
        //            uvs[index] = new Tuple<float, float>(j / num, i / num);
        //            uvs[index + 1] = new Tuple<float, float>(j / num, (i + 1) / num);
        //            uvs[index + 2] = new Tuple<float, float>((j + 1) / num, i / num);
        //            uvs[index + 3] = new Tuple<float, float>((j + 1) / num, i / num);
        //            uvs[index + 4] = new Tuple<float, float>(j / num, (i + 1) / num);
        //            uvs[index + 5] = new Tuple<float, float>((j + 1) / num, (i + 1) / num);
        //        }
        //    }
        //    for (int i = 0; i < len2; ++i)
        //        normals[i] = pointList[indexs[i]];
        //}
    }
}
