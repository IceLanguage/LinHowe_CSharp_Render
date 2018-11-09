
namespace LinHowe_CSharp_Render.Render
{
    partial class Vertex_Coloring_Stage
    {
        public override void ChangeState()
        {
            int count = Rendering_pipeline._models.Count;
            for(int i = 0;i < count;++i)
            {
                Mesh mesh = Rendering_pipeline._models[i];
                int size = mesh.Vertices.Length;
                for (int j = 0; j < size; ++j)
                {
                   
                    VertexShader.Lighting(mesh, Rendering_pipeline._camera.pos, ref mesh.Vertices[j].v_shader);
                    mesh.Vertices[j].v_trans.lightingColor = mesh.Vertices[j].v_shader.lightingColor;
                }

            }
            GeometricStage._smallStage = Projection_Stage.instance;
        }


    }
}
