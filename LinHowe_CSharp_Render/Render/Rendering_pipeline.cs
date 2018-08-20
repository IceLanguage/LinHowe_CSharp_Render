using LinHowe_CSharp_Render.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinHowe_CSharp_Render.Render
{
    /// <summary>
    /// 渲染管线
    /// </summary>
    static class Rendering_pipeline
    {
        //模型图元数据
        public static List<Mesh> _models = new List<Mesh>();
        public static bool RenderEnd = false;
        //摄像头
        public static Camera _camera;

        public static RenderStage _stage;

        //模型-世界，世界-视图,投影
        public static Matrix4x4 m = Matrix4x4.Identity, v, p;
    }
}
