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

        public static void Render()
        {
            if (null == Rendering_pipeline. _stage)
            {
                
                Rendering_pipeline._stage = ApplicationStage.instance;
                Rendering_pipeline.RenderEnd = false;
            }
            else
            {
                Rendering_pipeline._stage.ChangeState();
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
            Rendering_pipeline._stage = null;
            Rendering_pipeline.RenderEnd = true;
           
        }
    }

    /// <summary>
    /// 渲染的小阶段
    /// </summary>
    abstract class SmallStage:RenderStage
    {

    }
}
