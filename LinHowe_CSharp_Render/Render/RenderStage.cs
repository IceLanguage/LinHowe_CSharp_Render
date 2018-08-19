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
    }

    /// <summary>
    /// 应用程序阶段
    /// </summary>
    partial class ApplicationStage : RenderStage
    {
    }

    /// <summary>
    /// 几何阶段
    /// </summary>
    partial class GeometricStage :RenderStage
    {

    }

    /// <summary>
    /// 光栅化阶段
    /// </summary>
    partial class RasterizationStage :RenderStage
    {

    }
}
