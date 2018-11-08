using LinHowe_CSharp_Render.Math;
using System;

namespace LinHowe_CSharp_Render.Test
{
    /// <summary>
    /// 立方体网格数据
    /// </summary>
    class CubeData
    {
        /// <summary>
        /// 法线
        /// </summary>
        public static Vector3[] norlmas = 
        {
            new Vector3( 0, 0,-1), new Vector3( 0, 0,-1), new Vector3( 0, 0,-1),
            new Vector3( 0, 0,-1), new Vector3( 0, 0,-1), new Vector3( 0, 0,-1),
            new Vector3( 0, 0, 1), new Vector3( 0, 0, 1), new Vector3( 0, 0, 1),
            new Vector3( 0, 0, 1), new Vector3( 0, 0, 1), new Vector3( 0, 0, 1),
            new Vector3(-1, 0, 0), new Vector3(-1, 0, 0), new Vector3(-1, 0, 0),
            new Vector3(-1, 0, 0), new Vector3(-1, 0, 0), new Vector3(-1, 0, 0),
            new Vector3( 0,-1, 0), new Vector3( 0,-1, 0), new Vector3( 0,-1, 0),
            new Vector3( 0,-1, 0), new Vector3( 0,-1, 0), new Vector3( 0,-1, 0),
            new Vector3( 1, 0, 0), new Vector3( 1, 0, 0), new Vector3( 1, 0, 0),
            new Vector3( 1, 0, 0), new Vector3( 1, 0, 0), new Vector3( 1, 0, 0),
            new Vector3( 0, 1, 0), new Vector3( 0, 1, 0), new Vector3( 0, 1, 0),
            new Vector3( 0, 1, 0), new Vector3( 0, 1, 0), new Vector3( 0, 1, 0)
        };

        /// <summary>
        /// 三角形顶点索引 12个面
        /// </summary>
        public static int[] indexs =
        {
            0,1,2,
            0,2,3,
            7,6,5,
            7,5,4,
            0,4,5,
            0,5,1,
            1,5,6,
            1,6,2,
            2,6,7,
            2,7,3,
            3,7,4,
            3,4,0
        };

        /// <summary>
        /// 顶点坐标
        /// </summary>
        public static Vector3[] pointList =
        {
            new Vector3(-1, 1,-1),
            new Vector3(-1,-1,-1),
            new Vector3( 1,-1,-1),
            new Vector3( 1, 1,-1),

            new Vector3(-1, 1, 1),
            new Vector3(-1,-1, 1),
            new Vector3( 1,-1, 1),
            new Vector3( 1, 1, 1)
        };

        /// <summary>
        /// 顶点颜色
        /// </summary>
        public static Color[] vertColors =
        {
            new Color( 0, 1, 0), new Color( 0, 0, 1),
            new Color( 1, 0, 0), new Color( 1, 1, 0),
            new Color( 1, 0, 1), new Color( 0, 1, 1),
            new Color( 0.5f, 0.2f, 0), new Color( 1, 0.5f, 0.4f)

        };

        /// <summary>
        /// 材质
        /// </summary>
        public static Material mat = new Material
        (
            emissive : new Color(0, 0, 0f),
            ka : 0.1f,
            diffuse : new Color(0.3f,0.3f, 0.3f),
            specular : new Color(1, 1, 1),
            shininess : 99
        );

        /// <summary>
        /// uv坐标
        /// </summary>
        public static Tuple<float,float>[] uvs ={
            new Tuple<float,float>(0, 0),new Tuple<float,float>(0, 1),new Tuple<float,float>(1, 1),
            new Tuple<float,float>(0, 0),new Tuple<float,float>(1, 1),new Tuple<float,float>(1, 0),
            new Tuple<float,float>(0, 0),new Tuple<float,float>(0, 1),new Tuple<float,float>(1, 1),
            new Tuple<float,float>(0, 0),new Tuple<float,float>(1, 1),new Tuple<float,float>(1, 0),
            new Tuple<float,float>(0, 0),new Tuple<float,float>(0, 1),new Tuple<float,float>(1, 1),
            new Tuple<float,float>(0, 0),new Tuple<float,float>(1, 1),new Tuple<float,float>(1, 0),
            new Tuple<float,float>(0, 0),new Tuple<float,float>(0, 1),new Tuple<float,float>(1, 1),
            new Tuple<float,float>(0, 0),new Tuple<float,float>(1, 1),new Tuple<float,float>(1, 0),
            new Tuple<float,float>(0, 0),new Tuple<float,float>(0, 1),new Tuple<float,float>(1, 1),
            new Tuple<float,float>(0, 0),new Tuple<float,float>(1, 1),new Tuple<float,float>(1, 0),
            new Tuple<float,float>(0, 0),new Tuple<float,float>(0, 1),new Tuple<float,float>(1, 1),
            new Tuple<float,float>(0, 0),new Tuple<float,float>(1, 1),new Tuple<float,float>(1, 0)
        };
    }
}
