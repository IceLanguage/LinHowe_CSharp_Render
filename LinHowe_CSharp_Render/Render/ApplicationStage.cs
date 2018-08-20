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
            RenderStage._models.Add(newmesh);
        }

        /// <summary>
        /// 输入摄像头信息
        /// </summary>
        /// <param name="camera"></param>
        public void AddCamera(Camera camera)
        {
            RenderStage._camera = camera;
        }
    }
}
