
using System;

namespace LinHowe_CSharp_Render.Render
{
    partial class RasterizationStage
    {
        public override void ChangeState()
        {
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            Draw.Rasterization();
            Rendering_pipeline._stage = null;
            Rendering_pipeline.RenderEnd = true;

            watch.Stop();
            TimeSpan timespan = watch.Elapsed;
            System.Diagnostics.Debug.WriteLine("光栅化执行时间：{0}(毫秒)", timespan.TotalMilliseconds);
        }
    }
}
