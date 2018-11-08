
namespace LinHowe_CSharp_Render.Render
{
    partial class RasterizationStage
    {
        public override void ChangeState()
        {
            Draw.Rasterization();
            Rendering_pipeline._stage = null;
            Rendering_pipeline.RenderEnd = true;
        }
    }
}
