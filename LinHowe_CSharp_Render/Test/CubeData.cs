using LinHowe_CSharp_Render.Math;

namespace LinHowe_CSharp_Render.Test
{
    /// <summary>
    /// 立方体网格数据
    /// </summary>
    class CubeData
    {
        
        

        //法线
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
        //顶点坐标
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
        //三角形顶点索引 12个面
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
        //顶点颜色
        //public static Color[] vertColors = 
        //{
        //    new Color( 0, 1, 0), new Color( 0, 0, 1), new Color( 1, 0, 0),
        //    new Color( 0, 1, 0), new Color( 1, 0, 0), new Color( 0, 0, 1),
        //    new Color( 0, 1, 0), new Color( 0, 0, 1), new Color( 1, 0, 0),
        //    new Color( 0, 1, 0), new Color( 1, 0, 0), new Color( 0, 0, 1),
        //    new Color( 0, 1, 0), new Color( 0, 0, 1), new Color( 1, 0, 0),
        //    new Color( 0, 1, 0), new Color( 1, 0, 0), new Color( 0, 0, 1),
        //    new Color( 0, 1, 0), new Color( 0, 0, 1), new Color( 1, 0, 0),
        //    new Color( 0, 1, 0), new Color( 1, 0, 0), new Color( 0, 0, 1),
        //    new Color( 0, 1, 0), new Color( 0, 0, 1), new Color( 1, 0, 0),
        //    new Color( 0, 1, 0), new Color( 1, 0, 0), new Color( 0, 0, 1),
        //    new Color( 0, 1, 0), new Color( 0, 0, 1), new Color( 1, 0, 0),
        //    new Color( 0, 1, 0), new Color( 1, 0, 0), new Color( 0, 0, 1)
        //};
        public static Color[] vertColors =
        {
            new Color( 0, 1, 0), new Color( 0, 0, 1), new Color( 1, 0, 0),
            new Color( 1, 1, 0), new Color( 1, 0, 1), new Color( 0, 1, 1),
            new Color( 0.5f, 1, 0), new Color( 1, 0.5f, 0)
    
        };
    }
}
