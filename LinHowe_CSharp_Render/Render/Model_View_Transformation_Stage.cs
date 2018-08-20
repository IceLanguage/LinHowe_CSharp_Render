using LinHowe_CSharp_Render.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinHowe_CSharp_Render.Render
{
    //原理链接 http://www.cnblogs.com/graphics/archive/2012/07/12/2476413.html
    partial class Model_View_Transformation_Stage
    {
        public override void ChangeState()
        {
            Rendering_pipeline.v = GetView();
            foreach(Mesh mesh in Rendering_pipeline._models)
            {
                int size = mesh.Vertices.Length;
                for (int i = 0;i < size;++i)
                {
                    SetMVTransform(Rendering_pipeline.m, Rendering_pipeline.v, ref mesh.Vertices[i].v);
                   
                }
                
            }
            GeometricStage._smallStage = Vertex_Coloring_Stage.instance;
        }
        /// <summary>
        /// 获取视矩阵
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="lookAt"></param>
        /// <param name="up"></param>
        /// <returns></returns>
        private static Matrix4x4 GetView()
        {
            Camera _camera = Rendering_pipeline._camera;
            Vector3 pos = _camera.pos;
            Vector3 lookAt = _camera.lookAt;
            Vector3 up = _camera.up;

            //视线方向
            Vector3 dir = lookAt - pos;
            Vector3 right = Vector3.Cross(up, dir);
      
            right.Normalize();
            //平移部分
            Matrix4x4 t = new Matrix4x4
            (
                1, 0, 0, 0,
                0, 1, 0, 0,
                0, 0, 1, 0,
                -pos.x, -pos.y, -pos.z, 1
            );
            //旋转部分
            Matrix4x4 r = new Matrix4x4
            (
                right.x, up.x, dir.x, 0,
                right.y, up.y, dir.y, 0,
                right.z, up.z, dir.z, 0,
                0, 0, 0, 1
            );
            return t * r;
        }

        /// <summary>
        /// 进行mv矩阵变换，从本地模型空间到世界空间，再到相机空间
        /// </summary>
        private static void SetMVTransform(Matrix4x4 m, Matrix4x4 v, ref Vertex vertex)
        {
            vertex.position = vertex.position * m ;
        }
    }
}
