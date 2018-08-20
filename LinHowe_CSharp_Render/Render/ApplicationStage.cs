using LinHowe_CSharp_Render.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinHowe_CSharp_Render.Render
{
    /// <summary>
    /// 应用程序阶段
    /// </summary>
    partial class ApplicationStage
    {
        /// <summary>
        /// 输入模型图元
        /// </summary>
        /// <param name="newmesh"></param>
        public void AddMesh(Mesh newmesh)
        {
            Rendering_pipeline._models.Add(newmesh);
        }

        /// <summary>
        /// 输入摄像头信息
        /// </summary>
        /// <param name="camera"></param>
        public void AddCamera(Camera camera)
        {
            Rendering_pipeline._camera = camera;
        }

        public void AddLight(Light light)
        {
            Rendering_pipeline._lights.Add(light);
        }

        public override void ChangeState()
        {
            int count = Rendering_pipeline._models.Count;

            VertexShader.Vertices = new List<Vertex[]>(count);
            for (int i = 0; i < count; ++i)
            {
                int size = Rendering_pipeline._models[i].Vertices.Length;
                Vertex[] vs = new Vertex[size];

                for (int j = 0; j < size; ++j)
                {
                    vs[j] = Rendering_pipeline._models[i].Vertices[j];
                    vs[j].lightingColor = Color.Black;
                }
                VertexShader.Vertices.Add(vs);
            }

            Rendering_pipeline._stage = GeometricStage.instance;
            //Console.WriteLine("进入几何阶段");
        }
    }
}
