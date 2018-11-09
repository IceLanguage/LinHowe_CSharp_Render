
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
            Rendering_pipeline._models.Clear();
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
            VertexShader.Init(Rendering_pipeline._models);

            Rendering_pipeline._stage = GeometricStage.instance;
        }
    }
}
