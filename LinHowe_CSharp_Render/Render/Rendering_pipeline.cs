using LinHowe_CSharp_Render.Math;
using System.Collections.Generic;

namespace LinHowe_CSharp_Render.Render
{
    /// <summary>
    /// 渲染管线
    /// </summary>
    static class Rendering_pipeline
    {
        //模型图元数据
        public static List<Mesh> _models = new List<Mesh>();

        //光源
        public static List<Light> _lights = new List<Light>();

        //渲染流程是否结束
        public static bool RenderEnd = false;

        //渲染阶段
        public static RenderStage _stage;

        //模型-世界  4x4矩阵
        public static Matrix4x4 m = Matrix4x4.Identity;

        //摄像头
        public static Camera _camera;

        //环境光
        public static Color _ambientColor = Color.White;

        public static void Render()
        {
            RenderStage.Render();
        }
    }
}
