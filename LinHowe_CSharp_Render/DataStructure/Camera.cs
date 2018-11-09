using LinHowe_CSharp_Render.Math;

namespace LinHowe_CSharp_Render
{
    struct Camera
    {
        /// <summary>
        /// 观察角，弧度
        /// </summary>
        public float fov;
        /// <summary>
        /// 长宽比
        /// </summary>
        public float aspect;
        /// <summary>
        /// 近裁平面
        /// </summary>
        public float zn;
        /// <summary>
        /// 远裁平面
        /// </summary>
        public float zf;
        public Vector3 pos;
        public Vector3 lookAt;
        public Vector3 up;


        /// <summary>
        /// 世界-视图,视图-投影 4x4矩阵
        /// </summary>
        public Matrix4x4 WorldToViewMatrix, ViewToProjectionMatrix;

    }
}
