using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinHowe_CSharp_Render.Render
{
    /// <summary>
    /// 渲染阶段
    /// </summary>
    abstract class RenderStage
    {
        public virtual RenderStage ChangeState()
        {
            return null;
        }
    }

    /// <summary>
    /// 应用程序阶段
    /// </summary>
    partial class ApplicationStage : RenderStage
    {
        public override RenderStage ChangeState()
        {
            return new GeometricStage();
        }
    }

    /// <summary>
    /// 几何阶段
    /// </summary>
    partial class GeometricStage :RenderStage
    {
        public override RenderStage ChangeState()
        {
            return new RasterizationStage();
        }
    }

    /// <summary>
    /// 光栅化阶段
    /// </summary>
    partial class RasterizationStage :RenderStage
    {

    }
}
