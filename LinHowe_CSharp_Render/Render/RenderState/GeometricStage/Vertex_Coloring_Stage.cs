﻿
using System;

namespace LinHowe_CSharp_Render.Render
{
    partial class Vertex_Coloring_Stage
    {
        public override void ChangeState()
        {
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            int count = Rendering_pipeline._models.Count;
            for(int i = 0;i < count;++i)
            {
                Mesh mesh = Rendering_pipeline._models[i].mesh;
                if (mesh.CullFlag)
                    continue;
                
                int size = mesh.Vertices.Length;
                for (int j = 0; j < size; ++j)
                {  
                    VertexShader.Lighting(Rendering_pipeline._models[i], Rendering_pipeline.MainCamera.pos, ref mesh.Vertices[j].v_shader);
                    mesh.Vertices[j].v_trans.lightingColor = mesh.Vertices[j].v_shader.lightingColor;
                }
            }
            GeometricStage.CurStage = Projection_Stage.instance;

            watch.Stop();
            TimeSpan timespan = watch.Elapsed;
            System.Diagnostics.Debug.WriteLine("2-顶点着色执行时间：{0}(毫秒)", timespan.TotalMilliseconds);
        }


    }
}
