
namespace LinHowe_CSharp_Render.Render
{
    /// <summary>
    /// 应用程序阶段
    /// </summary>
    partial class ApplicationStage
    {
        public void EnableOrDisableRenderTexture()
        {
            Rendering_pipeline.IsRenderTexture = !Rendering_pipeline.IsRenderTexture;
        }
        /// <summary>
        /// 输入模型图元
        /// </summary>
        /// <param name="newmesh"></param>
        public void AddGameObject(GameObject go)
        {
            Rendering_pipeline._models.Add(go);
        }

        /// <summary>
        /// 输入摄像头信息
        /// </summary>
        /// <param name="camera"></param>
        public void SetMainCamera(Camera camera)
        {
            Rendering_pipeline.MainCamera = camera;
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
