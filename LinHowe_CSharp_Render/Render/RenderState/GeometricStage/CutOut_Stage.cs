﻿
namespace LinHowe_CSharp_Render.Render
{
    partial class CutOut_Stage
    {
        public override void ChangeState()
        {
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
        }

        private bool checkCut(Vertex v)
        {
            return (v.position.x >= -v.position.w && v.position.x <= v.position.w &&
                        v.position.y >= -v.position.w && v.position.y <= v.position.w &&
                        v.position.z >= 0f && v.position.z <= v.position.w);
        }
    }
}
