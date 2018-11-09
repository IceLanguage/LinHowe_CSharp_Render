using LinHowe_CSharp_Render.Math;

namespace LinHowe_CSharp_Render.Render
{
    //原理链接 http://www.cnblogs.com/graphics/archive/2012/07/12/2476413.html
    partial class Model_View_Transformation_Stage
    {
        public override void ChangeState()
        {
            //模型视图变换
            Rendering_pipeline.MainCamera.WorldToViewMatrix = GetView();
            foreach(GameObject go in Rendering_pipeline._models)
            {
                Mesh mesh = go.mesh;
                int size = mesh.Vertices.Length;

                Vector3 SphereCenterPos = go.position;
                SetMTransform(go.ObjectToWorldMatrix,
                        ref SphereCenterPos);

                //本地模型空间到世界空间
                for (int i = 0;i < size;++i)
                {
                    SetMTransform(go.ObjectToWorldMatrix,
                        ref mesh.Vertices[i].v_trans.position);

             
                }

                //物体剔除-包围球测试             
                if (CullObject(go, SphereCenterPos))
                    return;

                //世界空间到相机空间
                for (int i = 0; i < size; ++i)
                {                   
                    SetVTransform(Rendering_pipeline.MainCamera.WorldToViewMatrix,
                        ref mesh.Vertices[i].v_trans.position);
                }
            }

            //背面消隐 
            RemoveBackFace();

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
            Camera MainCamera = Rendering_pipeline.MainCamera;
            Vector3 pos = MainCamera.pos;
            Vector3 lookAt = MainCamera.lookAt;
            Vector3 up = MainCamera.up;

            //视线方向
            Vector3 viewdir = lookAt - pos;
            Vector3 right = Vector3.Cross(up, viewdir);
      
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
                right.x, up.x, viewdir.x, 0,
                right.y, up.y, viewdir.y, 0,
                right.z, up.z, viewdir.z, 0,
                0, 0, 0, 1
            );
            return t * r;
        }

        /// <summary>
        /// 进行m矩阵变换，从本地模型空间到世界空间
        /// </summary>
        private static void SetMTransform(
            Matrix4x4 m,
            ref Vector3 pos)
        {
            pos *= m;
        }
        /// <summary>
        /// 进行v矩阵变换，从世界空间到相机空间
        /// </summary>
        private static void SetVTransform(
            Matrix4x4 v,
            ref Vector3 pos)
        {
            pos *= v;
        }
        /// <summary>
        /// 背面消隐
        /// 原理 https://blog.csdn.net/sixdaycoder/article/details/72637527
        /// </summary>
        /// <returns>是否通过背面消隐测试</returns>
        private bool BackFaceCulling(Vertex p1, Vertex p2, Vertex p3)
        {
            Vector3 v1 = p2.position - p1.position;
            Vector3 v2 = p3.position - p1.position;
            Vector3 normal = Vector3.Cross(v1, v2);
            //由于在视空间中，所以相机点就是（0,0,0）
            Vector3 viewDir = p1.position - Vector3.zero;
            if (Vector3.Dot(normal, viewDir) > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 背面消隐
        /// </summary>
        private void RemoveBackFace()
        {
            foreach (GameObject go in Rendering_pipeline._models)
            {
                Mesh mesh = go.mesh;
                for (int i = 0; i + 2 < mesh.Vertices.Length; i += 3)
                {
                    if (!BackFaceCulling(mesh.Vertices[i].v_trans, mesh.Vertices[i + 1].v_trans, mesh.Vertices[i + 2].v_trans))
                    {
                        mesh.Cuts[i + 2] = mesh.Cuts[i + 1] = mesh.Cuts[i] = true;
                    }
                }
            }
        }

        /// <summary>
        /// 物体剔除-包围球测试
        /// </summary>
        private bool CullObject(GameObject go,Vector3 SphereCenterPos)
        {
            if (go.mesh.CullFlag)
                return true;
            Camera camera = Rendering_pipeline.MainCamera;

            //远近裁剪面裁剪
            if (SphereCenterPos.z - go.max_radius> camera.zf||
               SphereCenterPos.z + go.max_radius < camera.zn)
            {
                return go.mesh.CullFlag = true;
            }

            float FocalLength = camera.FocalLength;

            //左右裁剪面剔除
            float z_test = 0.5f * camera.aspect * camera.ScreenHeight *
                SphereCenterPos.z / FocalLength;
            if (SphereCenterPos.x - go.max_radius > z_test ||
               SphereCenterPos.x + go.max_radius < -z_test)
            {
                return go.mesh.CullFlag = true;
            }

            //上下裁剪面剔除
            z_test = 0.5f * camera.ScreenHeight *
                SphereCenterPos.z / FocalLength;
            if (SphereCenterPos.y - go.max_radius > z_test ||
              SphereCenterPos.y + go.max_radius < -z_test)
            {
                return go.mesh.CullFlag = true;
            }

            return go.mesh.CullFlag = false;
        }
    }
}
