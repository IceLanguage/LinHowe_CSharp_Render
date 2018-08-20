using LinHowe_CSharp_Render.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LinHowe_CSharp_Render.Render
{
    

    /// <summary>
    /// 渲染阶段
    /// </summary>
    abstract class RenderStage
    { 
        //模型图元数据
        public static List<Mesh> _models = new List<Mesh>();
        public static bool RenderEnd = false;
        //摄像头
        protected static Camera _camera;

        public static RenderStage _stage ;

        //模型-世界，世界-视图,投影
        protected static Matrix4x4 m = new Matrix4x4(), v, p;

        public static void Render()
        {
            if (null == _stage)
            {
                _stage = ApplicationStage.instance;
                RenderEnd = false;
            }
            else
            {
                _stage.ChangeState();
            }
        }
        public virtual void ChangeState()
        {
                
        }
    }

    /// <summary>
    /// 应用程序阶段
    /// </summary>
    partial class ApplicationStage : RenderStage
    {
        public static readonly ApplicationStage instance = new ApplicationStage();
        public override void ChangeState()
        {
            _stage = GeometricStage.instance;
            //Console.WriteLine("进入几何阶段");
        }
        
    }

    /// <summary>
    /// 几何阶段
    /// </summary>
    partial class GeometricStage :RenderStage
    {
        public static readonly GeometricStage instance = new GeometricStage();
        
    }

    /// <summary>
    /// 光栅化阶段
    /// </summary>
    partial class RasterizationStage :RenderStage
    {
        public static readonly RasterizationStage instance = new RasterizationStage();
        public override void ChangeState()
        {
            Draw.Rasterization();
            _stage = null;
            RenderEnd = true;
           
        }
    }

    /// <summary>
    /// 渲染的小阶段
    /// </summary>
    abstract class SmallStage:RenderStage
    {

    }
}
