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
        public static List<GameObject> _models = new List<GameObject>();

        //光源
        public static List<Light> _lights = new List<Light>();

        //渲染流程是否结束
        public static bool RenderEnd = false;

        //渲染阶段
        public static RenderStage _stage;

        //摄像头
        public static Camera MainCamera;

        //环境光
        public static Color _ambientColor = Color.White;

        public static bool IsRenderTexture = false;

        public static void Render()
        {
            RenderStage.Render();
        }
    }
}
