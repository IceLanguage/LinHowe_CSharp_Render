using LinHowe_CSharp_Render.Math;

namespace LinHowe_CSharp_Render.Render
{
    struct Camera
    {
        /// <summary>
        /// 长宽比
        /// </summary>
        public float aspect;
        public Vector3 pos;
        public Vector3 lookAt;
        public Vector3 up;

    }
}
