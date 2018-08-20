using LinHowe_CSharp_Render.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinHowe_CSharp_Render.Render
{
    partial class CutOut_Stage
    {
        public override void ChangeState()
        {
            foreach (Mesh mesh in Rendering_pipeline._models)
            {
                int size = mesh.Vertices.Length;
                for (int i = 0; i < size; ++i)
                {
                    Vertex v = mesh.Vertices[i];

                    if (mesh.Cuts[i])
                        continue;
                    if (v.position.x >= -v.position.w && v.position.x <= v.position.w &&
                        v.position.y >= -v.position.w && v.position.y <= v.position.w &&
                        v.position.z >= 0f && v.position.z <= v.position.w)
                    {
                        continue;
                    }

                    mesh.Cuts[i] = true;
                }

            }
            GeometricStage._smallStage = Screen_Mapping_Stage.instance;
        }
    }
}
