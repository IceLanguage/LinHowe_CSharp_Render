using LinHowe_CSharp_Render.Math;

namespace LinHowe_CSharp_Render
{
    class Camera
    {
        /// <summary>
        /// 观察角，弧度
        /// </summary>
        public float fov;
        /// <summary>
        /// 宽纵比
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

        /// <summary>
        /// 屏幕宽度
        /// </summary>
        public int ScreenHeight;

        /// <summary>
        /// 世界-视图 4x4矩阵
        /// </summary>
        public Matrix4x4 WorldToViewMatrix;
        /// <summary>
        /// 视图-投影 4x4矩阵
        /// </summary>
        public Matrix4x4 ViewToProjectionMatrix;

        public Vector3 pos;
        public Vector3 lookAt;
        public Vector3 up;
        public float Width
        {
            get
            {
                return ScreenHeight * aspect;
            }
        }
        /// <summary>
        /// 焦距
        /// </summary>
        public float FocalLength
        {
            get
            {
                return (float)(1f/ System.Math.Tan(fov * 0.5f) * ScreenHeight/2);
            }
        }


    }
}
