using LinHowe_CSharp_Render.Math;
using System;

namespace LinHowe_CSharp_Render.Render
{
    //原理链接 http://www.cnblogs.com/graphics/archive/2012/07/12/2476413.html
    partial class Model_View_Transformation_Stage
    {
        public override void ChangeState()
        {
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            //模型视图变换
            Rendering_pipeline.MainCamera.WorldToViewMatrix = GetView();
            foreach(GameObject go in Rendering_pipeline._models)
            {
                Mesh mesh = go.mesh;
                int size = mesh.Vertices.Length;

                Vector3 SphereCenterPos = go.position;

                //本地模型空间到世界空间
                for (int i = 0;i < size;++i)
                {
                    SetMTransform(go,ref mesh.Vertices[i].v_trans.position);
                }

                //物体剔除-包围球测试             
                if (CullObject(go, SphereCenterPos))
                    continue;

                //世界空间到相机空间
                for (int i = 0; i < size; ++i)
                {
                    SetVTransform(ref mesh.Vertices[i].v_trans.position);
                }

                //裁剪算法
                CullObject(go);
            }

            //背面消隐 
            RemoveBackFace();


            GeometricStage.CurStage = Vertex_Coloring_Stage.instance;

            watch.Stop();
            TimeSpan timespan = watch.Elapsed;
            System.Diagnostics.Debug.WriteLine("1-模型视图变换执行时间：{0}(毫秒)", timespan.TotalMilliseconds);
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
            GameObject go,
            ref Vector3 pos)
        {
            pos.x *= go.scale.x;
            pos.y *= go.scale.y;
            pos.z *= go.scale.z;
            pos *= go.ObjectToWorldMatrix;
           
        }
        /// <summary>
        /// 进行v矩阵变换，从世界空间到相机空间
        /// </summary>
        private static void SetVTransform(
            ref Vector3 pos)
        {
            pos *= Rendering_pipeline.MainCamera.WorldToViewMatrix;
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
                int len = mesh.Vertices.Length;
                for (int i = 0; i + 2 < len; i += 3)
                {
                    if (!BackFaceCulling(mesh.Vertices[i].v_trans, mesh.Vertices[i + 1].v_trans, mesh.Vertices[i + 2].v_trans))
                    {
                        mesh.Cuts[i + 2] = mesh.Cuts[i + 1] = mesh.Cuts[i] = true;
                    }
                }
            }
        }
        private void CullObject(GameObject go)
        {
            if (go.mesh.CullFlag)
                return ;
            Camera camera = Rendering_pipeline.MainCamera;

            Mesh mesh = go.mesh;
            int len = mesh.Vertices.Length;
            for (int i = 0; i + 2 < len; i += 3)
            {

                //左右裁剪面
                if (Cull_LR(mesh, camera, i))
                    continue;

                //上下裁剪面
                if (Cull_UD(mesh, camera, i))
                    continue;

                //远近裁剪面
                if (Cull_FN(mesh, camera, i))
                    continue;

            }
        }

        /// <summary>
        /// 远近裁剪面裁剪
        /// 这里的裁剪算法可以增加一部分，用裁剪面裁剪三角形，重新定义定义顶点结构,
        /// 不过重新定义定义顶点结构这需要改动很大一部分代码，
        /// 暂时不加人这一部分算法
        /// 算法来源《3D游戏编程大师技巧下册》P681 
        /// </summary>
        /// <param name="mesh"></param>
        /// <param name="camera"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        private bool Cull_FN(Mesh mesh, Camera camera, int i)
        {
            int test = 0;
            if (camera.zf < mesh.Vertices[i].v_trans.position.z)
                test += 1;//远裁剪面
            else if (camera.zn> mesh.Vertices[i].v_trans.position.z)
                test -= 1; //近裁剪面
            else
                return false;
            if (test != 1 && camera.zf < mesh.Vertices[i + 1].v_trans.position.z)
                return false;
            else if (test != -1 && camera.zn > mesh.Vertices[i + 1].v_trans.position.z)
                return false;
            if (test != 1 && camera.zf < mesh.Vertices[i + 2].v_trans.position.z)
                return false;
            else if (test != -1 && camera.zn > mesh.Vertices[i + 2].v_trans.position.z)
                return false;
            if (test != 0)
            {
                mesh.Cuts[i] = mesh.Cuts[i + 1] = mesh.Cuts[i + 2] = true;
                return true;
            }
            return false;
        }

        //左右裁剪面裁剪
        private bool Cull_LR(Mesh mesh,Camera camera,int i)
        {
            float z_factor = camera.ScreenHeight / camera.FocalLength;
            float z_test = z_factor * mesh.Vertices[i].v_trans.position.z;
            int test = 0;
            if (z_test < mesh.Vertices[i].v_trans.position.y)
                test += 1;//上裁剪面
            else if (-z_test > mesh.Vertices[i].v_trans.position.y)
                test -= 1; //下裁剪面
            else
                return false;
            z_test = z_factor * mesh.Vertices[i + 1].v_trans.position.z;
            if (test != 1 && z_test < mesh.Vertices[i + 1].v_trans.position.y)
                return false;
            else if (test != -1 && -z_test > mesh.Vertices[i + 1].v_trans.position.y)
                return false;
            z_test = z_factor * mesh.Vertices[i + 2].v_trans.position.z;
            if (test != 1 && z_test < mesh.Vertices[i + 2].v_trans.position.y)
                return false;
            else if (test != -1 && -z_test > mesh.Vertices[i + 2].v_trans.position.y)
                return false;

            if (test != 0)
            {
                mesh.Cuts[i] = mesh.Cuts[i + 1] = mesh.Cuts[i + 2] = true;
                return true;
            }
            return false;
        }

        //上下裁剪面裁剪
        private bool Cull_UD(Mesh mesh, Camera camera, int i)
        {
            float z_factor = camera.Width / camera.FocalLength;
            float z_test = z_factor * mesh.Vertices[i].v_trans.position.z;
            int test = 0;
            if (z_test < mesh.Vertices[i].v_trans.position.x)
                test += 1;//右裁剪面
            else if (-z_test > mesh.Vertices[i].v_trans.position.x)
                test -= 1; //左裁剪面
            else
                return false;
            z_test = z_factor * mesh.Vertices[i + 1].v_trans.position.z;
            if (test != 1 && z_test < mesh.Vertices[i + 1].v_trans.position.x)
                return false;
            else if (test != -1 && -z_test > mesh.Vertices[i + 1].v_trans.position.x)
                return false;
            z_test = z_factor * mesh.Vertices[i + 2].v_trans.position.z;
            if (test != 1 && z_test < mesh.Vertices[i + 2].v_trans.position.x)
                return false;
            else if (test != -1 && -z_test > mesh.Vertices[i + 2].v_trans.position.x)
                return false;

            if (test != 0)
            {
                mesh.Cuts[i] = mesh.Cuts[i + 1] = mesh.Cuts[i + 2] = true;
                return true;
            }
            return false;
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
