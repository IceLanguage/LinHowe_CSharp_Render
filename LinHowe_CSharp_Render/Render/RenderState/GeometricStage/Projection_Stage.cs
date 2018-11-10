using LinHowe_CSharp_Render.Math;

namespace LinHowe_CSharp_Render.Render
{
    //原理 http://www.cnblogs.com/graphics/archive/2012/07/25/2582119.html
    partial class Projection_Stage
    {

        public override void ChangeState()
        {
            GetProjection(ref Rendering_pipeline.MainCamera);
            foreach (GameObject go in Rendering_pipeline._models)
            {
                Mesh mesh = go.mesh;
                if (mesh.CullFlag)
                    continue;

                int size = mesh.Vertices.Length;
                for (int i = 0; i < size; ++i)
                {

                    SetProjectionTransform(Rendering_pipeline.MainCamera.ViewToProjectionMatrix, ref mesh.Vertices[i].v_trans);
                }
            }
            GeometricStage.CurStage = CutOut_Stage.instance;
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

            //vertex.u *= vertex.onePerZ;
            //vertex.v *= vertex.onePerZ;


            //vertex.lightingColor *= vertex.onePerZ;
        }

        /// <summary>
        /// 获取投影矩阵 dx风格， cvv为 x-1,1  y-1,1  z0,1
        /// </summary>
        /// <param name="fov">观察角，弧度</param>
        /// <param name="aspect">长宽比</param>
        /// <param name="zn">近裁z</param>
        /// <param name="zf">远裁z</param>
        /// <returns></returns>
        public static void GetProjection(ref Camera camera)
        {
            float fov = camera.fov;
            float aspect = camera.aspect;
            float zn = camera.zn;
            float zf = camera.zf;

            Matrix4x4 p = new Matrix4x4();

            p[0, 0] = (float)(1 / (System.Math.Tan(fov * 0.5f) * aspect));
            p[1, 1] = (float)(1 / System.Math.Tan(fov * 0.5f));
            p[2, 2] = zf / (zf - zn);
            p[2, 3] = 1f;
            p[3, 2] = (zn * zf) / (zn - zf);
            camera.ViewToProjectionMatrix = p;
        }
    }
}
