
using System;

namespace LinHowe_CSharp_Render.Render
{
    partial class CutOut_Stage
    {
        public override void ChangeState()
        {
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            foreach (GameObject go in Rendering_pipeline._models)
            {
                Mesh mesh = go.mesh;
                if (mesh.CullFlag)
                    continue;
                int len = mesh.Vertices.Length;
                for (int i = 0; i + 2 < len; i += 3)
                {
                    if (mesh.Cuts[i])
                        continue;
                   
                    Vertex v = mesh.Vertices[i].v_trans;
                    if (checkCut(v))
                        continue;
                    v = mesh.Vertices[i + 1].v_trans;
                    if (checkCut(v))
                        continue;
                    v = mesh.Vertices[i + 2].v_trans;
                    if (checkCut(v))
                        continue;
                    mesh.Cuts[i] = mesh.Cuts[i + 1] = mesh.Cuts[i + 2] = true;
                }
                

            }
            GeometricStage.CurStage = Screen_Mapping_Stage.instance;

            watch.Stop();
            TimeSpan timespan = watch.Elapsed;
            System.Diagnostics.Debug.WriteLine("4-裁剪阶段执行时间：{0}(毫秒)", timespan.TotalMilliseconds);
        }

        private bool checkCut(Vertex v)
        {
            return (v.position.x >= -v.position.w && v.position.x <= v.position.w &&
                        v.position.y >= -v.position.w && v.position.y <= v.position.w &&
                        v.position.z >= 0f && v.position.z <= v.position.w);
        }
    }
}
