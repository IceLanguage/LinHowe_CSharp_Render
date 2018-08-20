using LinHowe_CSharp_Render.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinHowe_CSharp_Render.Render
{
    //原理 http://www.cnblogs.com/graphics/archive/2012/07/25/2582119.html
    partial class Projection_Stage
    {

        public override void ChangeState()
        {
            Camera _camera = Rendering_pipeline._camera;
            Rendering_pipeline.p = GetProjection(_camera.fov, _camera.aspect, _camera.zn, _camera.zf);
            foreach (Mesh mesh in Rendering_pipeline._models)
            {
                int size = mesh.Vertices.Length;
                for (int i = 0; i < size; ++i)
                {
                    SetProjectionTransform(Rendering_pipeline.p, ref mesh.Vertices[i]);
                }
            }
            GeometricStage._smallStage = CutOut_Stage.instance;
        }
        /// <summary>
        /// 投影变换，从相机空间到齐次剪裁空间
        /// </summary>
        /// <param name="p"></param>
        /// <param name="vertex"></param>
        private void SetProjectionTransform(Matrix4x4 p, ref Vertex vertex)
        {
            vertex.position = vertex.position * p;

            vertex.onePerZ = 1 / vertex.position.w;
            
            vertex.u *= vertex.onePerZ;
            vertex.v *= vertex.onePerZ;


            vertex.lightingColor *= vertex.onePerZ;
        }

        /// <summary>
        /// 获取投影矩阵 dx风格， cvv为 x-1,1  y-1,1  z0,1
        /// </summary>
        /// <param name="fov">观察角，弧度</param>
        /// <param name="aspect">长宽比</param>
        /// <param name="zn">近裁z</param>
        /// <param name="zf">远裁z</param>
        /// <returns></returns>
        public static Matrix4x4 GetProjection(float fov, float aspect, float zn, float zf)
        {
            Matrix4x4 p = new Matrix4x4();

            p[0, 0] = (float)(1 / (System.Math.Tan(fov * 0.5f) * aspect));
            p[1, 1] = (float)(1 / System.Math.Tan(fov * 0.5f));
            p[2, 2] = zf / (zf - zn);
            p[2, 3] = 1f;
            p[3, 2] = (zn * zf) / (zn - zf);
            return p;
        }
    }
}
