using LinHowe_CSharp_Render.Math;
using System;

namespace LinHowe_CSharp_Render.Test
{
    static class PlaneData
    {
        /// <summary>
        /// 顶点坐标
        /// </summary>
        public static readonly Vector3[] pointList =
        {
            new Vector3(-1, 0,-1),
            new Vector3(-1, 0, 1),
            new Vector3( 1, 0, 1),
            new Vector3( 1, 0,-1),
        };

        /// <summary>
        /// 三角形顶点索引 12个面
        /// </summary>
        public static readonly int[] indexs =
        {
            0,2,1,
            0,3,2,
        };

        /// <summary>
        /// 顶点颜色
        /// </summary>
        public static readonly Color[] vertColors =
        {
            Color.White,
            Color.White,
            Color.White,
            Color.White,
        };
        /// <summary>
        /// 法线
        /// </summary>
        public static readonly Vector3[] norlmas =
        {
            new Vector3(0,1,0),
            new Vector3(0,1,0),
            new Vector3(0,1,0),
            new Vector3(0,1,0),
            new Vector3(0,1,0),
            new Vector3(0,1,0),
        };

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
        public static readonly Tuple<float, float>[] uvs ={
            new Tuple<float,float>(0, 0),
            new Tuple<float,float>(1, 1),
            new Tuple<float,float>(0, 1),
            new Tuple<float,float>(0, 0),
            new Tuple<float,float>(1, 0),
            new Tuple<float,float>(1, 1),
        };
    }
}
